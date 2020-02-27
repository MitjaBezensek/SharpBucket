using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    internal class RepositoriesEndPointTests
    {
        [Test]
        public void ListRepositories_WithNoMaxSet_ReturnsAtLeast10Repositories()
        {
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;

            var repositories = repositoriesEndPoint.ListRepositories(SampleRepositories.MERCURIAL_ACCOUNT_NAME);

            repositories.ShouldNotBe(null);
            repositories.Count.ShouldBeGreaterThan(10);
        }

        [Test]
        public void ListRepositories_WithOwnerRoleFilter_ReturnsNothingOnAPublicAccountButSomeOnCurrentAccount()
        {
            // Ensure that at least that test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;
            var parameters = new ListRepositoriesParameters
            {
                Role = SharpBucket.V2.Pocos.Role.Owner,
                Max = 1,
            };

            var repositories = repositoriesEndPoint.ListRepositories(
                SampleRepositories.MERCURIAL_ACCOUNT_NAME,
                parameters);

            repositories.ShouldNotBe(null);
            repositories.ShouldBeEmpty();

            repositories = repositoriesEndPoint.ListRepositories(
                TestHelpers.AccountName,
                parameters);

            repositories.ShouldNotBe(null);
            repositories.ShouldNotBeEmpty();
        }

        [Test]
        public void EnumerateRepositories_WithOwnerRoleFilter_ReturnsNothingOnAPublicAccountButSomeOnCurrentAccount()
        {
            // Ensure that at least that test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;
            var parameters = new EnumerateRepositoriesParameters
            {
                Role = SharpBucket.V2.Pocos.Role.Owner
            };

            var repositories = repositoriesEndPoint.EnumerateRepositories(
                SampleRepositories.MERCURIAL_ACCOUNT_NAME,
                parameters);

            repositories.ShouldNotBe(null);
            repositories.ShouldBeEmpty();

            repositories = repositoriesEndPoint.EnumerateRepositories(
                TestHelpers.AccountName,
                parameters);

            repositories.ShouldNotBe(null);
            repositories.ShouldNotBeEmpty();
        }

        [Test]
        public async Task EnumerateRepositoriesAsync_WithOwnerRoleFilter_ReturnsNothingOnAPublicAccountButSomeOnCurrentAccount()
        {
            // Ensure that at least that test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;
            var parameters = new EnumerateRepositoriesParameters
            {
                Role = SharpBucket.V2.Pocos.Role.Owner
            };

            var repositories = repositoriesEndPoint.EnumerateRepositoriesAsync(
                SampleRepositories.MERCURIAL_ACCOUNT_NAME,
                parameters);

            repositories.ShouldNotBe(null);
            (await repositories.AnyAsync()).ShouldBeFalse();

            repositories = repositoriesEndPoint.EnumerateRepositoriesAsync(
                TestHelpers.AccountName,
                parameters);

            repositories.ShouldNotBe(null);
            (await repositories.AnyAsync()).ShouldBeTrue();
        }

        [Test]
        public void ListPublicRepositories_With30AsMax_Returns30PublicRepositories()
        {
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;

            var publicRepositories = repositoriesEndPoint.ListPublicRepositories(30);

            publicRepositories.Count.ShouldBe(30);
            publicRepositories[5].full_name.ShouldBe("vetler/fhtmlmps");
        }

        [Test]
        public void ListPublicRepositories_WithOwnerRoleFilter_ReturnsOnlyPublicTestRepositories()
        {
            // Ensure that at least that test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;
            var parameters = new ListPublicRepositoriesParameters
            {
                Role = SharpBucket.V2.Pocos.Role.Owner,
            };

            var publicRepositories = repositoriesEndPoint.ListPublicRepositories(parameters);

            publicRepositories.ShouldNotBeEmpty();
            publicRepositories.Select(r => r.full_name)
                .ShouldAllBe(name => name.StartsWith(TestHelpers.AccountName));
        }

        [Test]
        public void EnumeratePublicRepositories_WithOwnerRoleFilter_ReturnsOnlyPublicTestRepositories()
        {
            // Ensure that at least that test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;
            var parameters = new EnumeratePublicRepositoriesParameters
            {
                Role = SharpBucket.V2.Pocos.Role.Owner,
            };

            var publicRepositories = repositoriesEndPoint.EnumeratePublicRepositories(parameters)
                .ToList();

            publicRepositories.ShouldNotBeEmpty();
            publicRepositories.Select(r => r.full_name)
                .ShouldAllBe(name => name.StartsWith(TestHelpers.AccountName));
        }

        [Test]
        public async Task EnumeratePublicRepositoriesAsync_WithOwnerRoleFilter_ReturnsOnlyPublicTestRepositories()
        {
            // Ensure that at least that test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;
            var parameters = new EnumeratePublicRepositoriesParameters
            {
                Role = SharpBucket.V2.Pocos.Role.Owner,
            };

            var publicRepositories = await repositoriesEndPoint.EnumeratePublicRepositoriesAsync(parameters)
                .ToListAsync();

            publicRepositories.ShouldNotBeEmpty();
            publicRepositories.Select(r => r.full_name)
                .ShouldAllBe(name => name.StartsWith(TestHelpers.AccountName));
        }

        [Test]
        public void ListPublicRepositories_Top3WithAnOldfterFilter_ReturnsAKnownList()
        {
            // Ensure that at least that test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;
            var parameters = new ListPublicRepositoriesParameters
            {
                After = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Max = 3,
            };

            var publicRepositories = repositoriesEndPoint.ListPublicRepositories(parameters);

            publicRepositories.ShouldNotBeEmpty();
            publicRepositories.Select(r => r.full_name)
                .ShouldBe(new[] { "china/kuaihuo", "jxck/zudosite", "trijezdci/macrocollection" });
        }

        [Test]
        public void EnumeratePublicRepositories_Top3WithAnOldfterFilter_ReturnsAKnownList()
        {
            // Ensure that at least that test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;
            var parameters = new EnumeratePublicRepositoriesParameters
            {
                After = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                PageLen = 3,
            };

            var publicRepositories = repositoriesEndPoint.EnumeratePublicRepositories(parameters)
                .Take(3)
                .ToList();

            publicRepositories.ShouldNotBeEmpty();
            publicRepositories.Select(r => r.full_name)
                .ShouldBe(new[] { "china/kuaihuo", "jxck/zudosite", "trijezdci/macrocollection" });
        }

        [Test]
        public async Task EnumeratePublicRepositoriesAsync_Top3WithAnOldfterFilter_ReturnsAKnownList()
        {
            // Ensure that at least that test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;
            var parameters = new EnumeratePublicRepositoriesParameters
            {
                After = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                PageLen = 3,
            };

            var publicRepositories = await repositoriesEndPoint.EnumeratePublicRepositoriesAsync(parameters)
                .Take(3)
                .ToListAsync();

            publicRepositories.ShouldNotBeEmpty();
            publicRepositories.Select(r => r.full_name)
                .ShouldBe(new[] { "china/kuaihuo", "jxck/zudosite", "trijezdci/macrocollection" });
        }
    }
}