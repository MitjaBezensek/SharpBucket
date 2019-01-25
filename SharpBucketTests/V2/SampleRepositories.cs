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

        private static RepositoriesEndPoint _repositoriesEndPoint;

        public static RepositoriesEndPoint RepositoriesEndPoint => _repositoriesEndPoint
                                                                   ?? (_repositoriesEndPoint = TestHelpers.SharpBucketV2.RepositoriesEndPoint());

        private static RepositoryResource _emptyTestRepository;

        public static RepositoryResource EmptyTestRepository => _emptyTestRepository
                                                                ?? (_emptyTestRepository = CreateTestRepository("Empty").RepositoryResource);

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
                    using (var testRepositoryBuilder = TestHelpers.GetTestRepositoryBuilder(testRepository.AccountName, testRepository.RepositoryName))
                    {
                        _testRepository.RepositoryInfo = testRepositoryBuilder.FillRepository();
                        _testRepository.RepositoryInfo.AccountName = testRepository.AccountName;
                        _testRepository.RepositoryInfo.RepositoryName = testRepository.RepositoryName;
                    }
                }

                return _testRepository;
            }
        }

        private static RepositoryResource _mercurialRepository;

        public static RepositoryResource MercurialRepository => _mercurialRepository ??
                                                             (_mercurialRepository = RepositoriesEndPoint.RepositoryResource(MERCURIAL_ACCOUNT_NAME, MERCURIAL_REPOSITORY_NAME));

        private static RepositoryResource _notExistingRepository;

        public static RepositoryResource NotExistingRepository => _notExistingRepository ??
                                                                (_notExistingRepository = RepositoriesEndPoint.RepositoryResource(TestHelpers.GetAccountName(), "not_existing_repository"));

        private static RepositoryResourceWithArgs CreateTestRepository(string repositoryNamePrefix)
        {
            var accountName = TestHelpers.GetAccountName();
            var repositoryName = $"{repositoryNamePrefix}_{Guid.NewGuid():N}";
            var repositoryResource = RepositoriesEndPoint.RepositoryResource(accountName, repositoryName);
            var repository = new Repository
            {
                name = repositoryName,
                language = "c#",
                scm = "git"
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
            _testRepository?.RepositoryResource.DeleteRepository();
        }
    }
}
