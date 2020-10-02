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
        public void ListPublicRepositories_With30AsMax_Returns30PublicRepositories()
        {
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;

            var publicRepositories = repositoriesEndPoint.ListPublicRepositories(30);

            publicRepositories.Count.ShouldBe(30);
        }

        [Test]
        public void ListPublicRepositories_WithOwnerRoleFilter_ReturnsOnlyPublicTestRepositories()
        {
            // Ensure the test repository is created
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
            // Ensure the test repository is created
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
            // Ensure the test repository is created
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
        public void ListPublicRepositories_Top3WithAfterFilter_ReturnsAKnownList()
        {
            // Ensure the test repository is created
            SampleRepositories.EmptyTestRepository.ShouldNotBeNull();
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;
            var parameters = new ListPublicRepositoriesParameters
            {
                After = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Max = 3,
            };

            var publicRepositories = repositoriesEndPoint.ListPublicRepositories(parameters);

            publicRepositories.ShouldNotBeNull();
            publicRepositories.Count.ShouldBe(3);
            DateTime.Parse(publicRepositories[0].created_on).ShouldBeGreaterThanOrEqualTo(parameters.After.Value);
            DateTime.Parse(publicRepositories[1].created_on).ShouldBeGreaterThanOrEqualTo(parameters.After.Value);
            DateTime.Parse(publicRepositories[2].created_on).ShouldBeGreaterThanOrEqualTo(parameters.After.Value);
        }

        [Test]
        public void EnumeratePublicRepositories_Top3WithAfterFilter_ReturnsAKnownList()
        {
            // Ensure the test repository is created
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

            publicRepositories.ShouldNotBeNull();
            publicRepositories.Count.ShouldBe(3);
            DateTime.Parse(publicRepositories[0].created_on).ShouldBeGreaterThanOrEqualTo(parameters.After.Value);
            DateTime.Parse(publicRepositories[1].created_on).ShouldBeGreaterThanOrEqualTo(parameters.After.Value);
            DateTime.Parse(publicRepositories[2].created_on).ShouldBeGreaterThanOrEqualTo(parameters.After.Value);
        }

        [Test]
        public async Task EnumeratePublicRepositoriesAsync_Top3WithAfterFilter_ReturnsAKnownList()
        {
            // Ensure the test repository is created
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

            publicRepositories.ShouldNotBeNull();
            publicRepositories.Count.ShouldBe(3);
            DateTime.Parse(publicRepositories[0].created_on).ShouldBeGreaterThanOrEqualTo(parameters.After.Value);
            DateTime.Parse(publicRepositories[1].created_on).ShouldBeGreaterThanOrEqualTo(parameters.After.Value);
            DateTime.Parse(publicRepositories[2].created_on).ShouldBeGreaterThanOrEqualTo(parameters.After.Value);
        }
    }
}