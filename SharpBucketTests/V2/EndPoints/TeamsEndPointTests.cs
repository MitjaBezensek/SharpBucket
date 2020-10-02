using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    class TeamsEndPointTests
    {
        private SharpBucketV2 sharpBucket;
        private TeamsEndPoint teamsEndPoint;

        [SetUp]
        public void Init()
        {
            sharpBucket = TestHelpers.SharpBucketV2;
            teamsEndPoint = sharpBucket.TeamsEndPoint();
        }

        [Test]
        public void GetUserTeams_FromLoggedUser_ShouldReturnManyTeams()
        {
            teamsEndPoint.ShouldNotBe(null);
            var teams = teamsEndPoint.GetUserTeams();
            teams.Count.ShouldBeGreaterThan(0);
        }

        [Test]
        public void GetUserTeamsWithAdminRole_FromLoggedUser_ShouldReturnManyTeams()
        {
            teamsEndPoint.ShouldNotBe(null);
            var teams = teamsEndPoint.GetUserTeamsWithAdminRole();
            teams.Count.ShouldBeGreaterThan(0);
        }

        [Test]
        public void GetUserTeamsWithContributorRole_FromLoggedUser_ShouldReturnManyTeams()
        {
            teamsEndPoint.ShouldNotBe(null);
            var teams = teamsEndPoint.GetUserTeamsWithContributorRole();
            teams.Count.ShouldBeGreaterThan(0);
        }

        [Test]
        public void EnumerateUserTeams_FromLoggedUser_ShouldReturnManyTeams()
        {
            teamsEndPoint.ShouldNotBe(null);
            var teams = teamsEndPoint.EnumerateUserTeams();
            teams.ShouldNotBeEmpty();
        }

        [Test]
        public void EnumerateUserTeams_WithAdminRoleFromLoggedUser_ShouldReturnManyTeams()
        {
            teamsEndPoint.ShouldNotBe(null);
            var parameters = new EnumerateTeamsParameters { Role = SharpBucket.V2.Pocos.Role.Admin };
            var teams = teamsEndPoint.EnumerateUserTeams(parameters);
            teams.ShouldNotBeEmpty();
        }

        [Test]
        public void EnumerateUserTeams_WithOwnerRoleFromLoggedUser_ShouldThrowBitbucketException()
        {
            teamsEndPoint.ShouldNotBe(null);
            var parameters = new EnumerateTeamsParameters { Role = SharpBucket.V2.Pocos.Role.Owner };
            Should.Throw<BitbucketV2Exception>(() => teamsEndPoint.EnumerateUserTeams(parameters).First());
        }

        [Test]
        public async Task EnumerateUserTeamsAsync_FromLoggedUser_ShouldReturnManyTeams()
        {
            teamsEndPoint.ShouldNotBe(null);
            var teams = await teamsEndPoint.EnumerateUserTeamsAsync().ToListAsync();
            teams.ShouldNotBeEmpty();
        }
    }
}