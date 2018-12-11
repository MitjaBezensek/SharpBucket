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

        private static RepositoryResource _emptyPrivateRepository;

        public static RepositoryResource EmptyPrivateRepository
        {
            get
            {
                if (_emptyPrivateRepository == null)
                {
                    var accountName = TestHelpers.GetAccountName();
                    var repositoryName = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    _emptyPrivateRepository = RepositoriesEndPoint.RepositoryResource(accountName, repositoryName);
                    var repository = new Repository
                    {
                        name = repositoryName,
                        language = "c#",
                        scm = "git"
                    };
                    _emptyPrivateRepository.PostRepository(repository);
                }

                return _emptyPrivateRepository;
            }
        }

        private static RepositoryResource _mercurialRepository;

        public static RepositoryResource MercurialRepository => _mercurialRepository ??
                                                             (_mercurialRepository = RepositoriesEndPoint.RepositoryResource(MERCURIAL_ACCOUNT_NAME, MERCURIAL_REPOSITORY_NAME));

        private static RepositoryResource _notExistingRepository;

        public static RepositoryResource NotExistingRepository => _notExistingRepository ??
                                                                (_notExistingRepository = RepositoriesEndPoint.RepositoryResource(TestHelpers.GetAccountName(), "not_existing_repository"));

        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            _emptyPrivateRepository?.DeleteRepository();
        }
    }
}
