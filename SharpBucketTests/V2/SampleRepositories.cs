using System;
using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;

namespace SharpBucketTests.V2
{
    [SetUpFixture]
    public class SampleRepositories
    {
        public const string MERCURIAL_ACCOUNT_NAME = "mirror";

        public const string MERCURIAL_REPOSITORY_NAME = "mercurial";

        public const string BOT_TEST_REPOSITORY_ACCOUNT_NAME = "penev92";

        public const string BOT_TEST_REPOSITORY_NAME = "bottestrepository";

        private static RepositoriesEndPoint _repositoriesEndPoint;

        public static RepositoriesEndPoint RepositoriesEndPoint => _repositoriesEndPoint
                                                                  ??= TestHelpers.SharpBucketV2.RepositoriesEndPoint();

        private static RepositoryResource _emptyTestRepository;

        public static RepositoryResource EmptyTestRepository => _emptyTestRepository
                                                                ??= CreateTestRepository("Empty").RepositoryResource;

        private static RepositoryResource _privateTestRepository;

        public static RepositoryResource PrivateTestRepository => _privateTestRepository
                                                                  ??= CreateTestRepository("Private", true).RepositoryResource;

        private static TestRepository _testRepository;

        public static TestRepository TestRepository
        {
            get
            {
                if (_testRepository == null)
                {
                    _testRepository = new TestRepository();
                    var testRepository = CreateTestRepository("Test");
                    _testRepository.RepositoryResource = testRepository.RepositoryResource;
                    using var testRepositoryBuilder = TestHelpers.GetTestRepositoryBuilder(testRepository.AccountName, testRepository.RepositoryName);
                    _testRepository.RepositoryInfo = testRepositoryBuilder.FillRepository();
                    _testRepository.RepositoryInfo.AccountName = testRepository.AccountName;
                    _testRepository.RepositoryInfo.RepositoryName = testRepository.RepositoryName;
                }

                return _testRepository;
            }
        }

        private static RepositoryResource _mercurialRepository;

        public static RepositoryResource MercurialRepository => _mercurialRepository
                                                                ??= RepositoriesEndPoint.RepositoryResource(MERCURIAL_ACCOUNT_NAME, MERCURIAL_REPOSITORY_NAME);

        private static RepositoryResource _botTestRepository;

        public static RepositoryResource BotTestRepository => _botTestRepository
            ??= RepositoriesEndPoint.RepositoryResource(BOT_TEST_REPOSITORY_ACCOUNT_NAME, BOT_TEST_REPOSITORY_NAME);

        private static RepositoryResource _notExistingRepository;

        public static RepositoryResource NotExistingRepository => _notExistingRepository
                                                                  ??= RepositoriesEndPoint.RepositoryResource(TestHelpers.AccountName, "not_existing_repository");

        private static RepositoryResourceWithArgs CreateTestRepository(string repositoryNamePrefix, bool isPrivate = false)
        {
            var accountName = TestHelpers.AccountName;
            var repositoryName = $"{repositoryNamePrefix}_{Guid.NewGuid():N}";
            var repositoryResource = RepositoriesEndPoint.RepositoryResource(accountName, repositoryName);
            var repository = new Repository
            {
                name = repositoryName,
                language = "c#",
                scm = "git",
                is_private = isPrivate
            };
            repositoryResource.PostRepository(repository);
            return new RepositoryResourceWithArgs { RepositoryResource = repositoryResource, AccountName = accountName, RepositoryName = repositoryName };
        }

        // small class to return a RepositoryResource with its build arguments
        // its probably something that should be change in the API but lets keep that for another pull request
        private class RepositoryResourceWithArgs
        {
            public RepositoryResource RepositoryResource { get; set; }
            public string AccountName { get; set; }
            public string RepositoryName { get; set; }
        }

        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            _emptyTestRepository?.DeleteRepository();
            _privateTestRepository?.DeleteRepository();
            _testRepository?.RepositoryResource.DeleteRepository();
        }
    }
}
