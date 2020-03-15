using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    public class RepositoriesAccountResourceTests
    {
        private RepositoriesAccountResource _mercurialRepositoriesResource;
        private RepositoriesAccountResource _testAccountRepositoriesResource;

        [OneTimeSetUp]
        public void Init()
        {
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;

            _mercurialRepositoriesResource = repositoriesEndPoint
                .RepositoriesResource(SampleRepositories.MERCURIAL_ACCOUNT_NAME);
            _testAccountRepositoriesResource = repositoriesEndPoint
                .RepositoriesResource(TestHelpers.AccountName);
        }

        [Test]
        public void ListRepositories_WithNoMaxSet_ReturnsAtLeast10Repositories()
        {
            var repositories = _mercurialRepositoriesResource.ListRepositories();

            repositories.ShouldNotBe(null);
            repositories.Count.ShouldBeGreaterThan(10);
        }

        [Test]
        public void ListRepositories_WithOwnerRoleFilter_ReturnsNothingOnAPublicAccountButSomeOnCurrentAccount()
        {
            // Ensure that at least that test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();

            var parameters = new ListRepositoriesParameters
            {
                Role = SharpBucket.V2.Pocos.Role.Owner,
                Max = 1,
            };

            var repositories = _mercurialRepositoriesResource.ListRepositories(parameters);

            repositories.ShouldNotBe(null);
            repositories.ShouldBeEmpty();

            repositories = _testAccountRepositoriesResource.ListRepositories(parameters);

            repositories.ShouldNotBe(null);
            repositories.ShouldNotBeEmpty();
        }

        [Test]
        public void EnumerateRepositories_WithOwnerRoleFilter_ReturnsNothingOnAPublicAccountButSomeOnCurrentAccount()
        {
            // Ensure that at least that test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();

            var parameters = new EnumerateRepositoriesParameters
            {
                Role = SharpBucket.V2.Pocos.Role.Owner
            };

            var repositories = _mercurialRepositoriesResource.EnumerateRepositories(parameters);

            repositories.ShouldNotBe(null);
            repositories.ShouldBeEmpty();

            repositories = _testAccountRepositoriesResource.EnumerateRepositories(parameters);

            repositories.ShouldNotBe(null);
            repositories.ShouldNotBeEmpty();
        }

        [Test]
        public async Task EnumerateRepositoriesAsync_WithOwnerRoleFilter_ReturnsNothingOnAPublicAccountButSomeOnCurrentAccount()
        {
            // Ensure that at least that test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();

            var parameters = new EnumerateRepositoriesParameters
            {
                Role = SharpBucket.V2.Pocos.Role.Owner
            };

            var repositories = _mercurialRepositoriesResource.EnumerateRepositoriesAsync(parameters);

            repositories.ShouldNotBe(null);
            (await repositories.AnyAsync()).ShouldBeFalse();

            repositories = _testAccountRepositoriesResource.EnumerateRepositoriesAsync(parameters);

            repositories.ShouldNotBe(null);
            (await repositories.AnyAsync()).ShouldBeTrue();
        }
    }
}
