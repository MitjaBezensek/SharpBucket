using System;
using System.Linq;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
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
        [Obsolete]
        public void GetProfile_FromTeamAtlassian_ReturnsAtlassianProfile()
        {
            sharpBucket.ShouldNotBe(null);
            var profile = sharpBucket.TeamsEndPoint("atlassian").GetProfile();
            profile.display_name.ShouldBe("Atlassian");
        }

        [Test]
        [Obsolete]
        public void ListMembers_FromFirstTeamOfLoggedUser_ShouldReturnManyMembers()
        {
            teamsEndPoint.ShouldNotBe(null);
            var teams = teamsEndPoint.GetUserTeams();
            teams.Count.ShouldBeGreaterThan(0);

            var firstTeamEndPoint = sharpBucket.TeamsEndPoint(teams[0].username);
            var members = firstTeamEndPoint.ListMembers(35);
            members.Count.ShouldBeGreaterThan(0);
            var userName = sharpBucket.UserEndPoint().GetUser().username;
            members.ShouldContain(m => m.username == userName);
        }

        [Test]
        [Obsolete]
        public void ListFollowers_FromTeamAtlassian_ShouldReturnManyFollowers()
        {
            sharpBucket.ShouldNotBe(null);
            var followers = sharpBucket.TeamsEndPoint("atlassian").ListFollowers(8);
            followers.Count.ShouldBe(8);
            followers[0].display_name.ShouldBe("Hector Malpica (Miuler)");
        }

        [Test]
        [Obsolete]
        public void ListRepositories_FromUserAdminTeamAfterHavingCreateOneRepoInside_ShouldReturnAtLeastTheCreatedOne()
        {
            teamsEndPoint.ShouldNotBe(null);
            var team = teamsEndPoint.GetUserTeamsWithAdminRole()[0];

            var repositoryName = Guid.NewGuid().ToString("N");
            var teamRepository = new Repository
            {
                name = repositoryName,
                language = "c#",
                scm = "git"
            };
            var teamResource = new TeamsEndPoint(TestHelpers.SharpBucketV2, team.username);
            var teamRepoResource = SampleRepositories.RepositoriesEndPoint.RepositoryResource(team.username, repositoryName);
            teamRepoResource.PostRepository(teamRepository);

            try
            {
                var repositories = teamResource.ListRepositories();
                repositories.ShouldNotBeEmpty();
                repositories.Any(r => r.name == teamRepository.name).ShouldBe(true);
            }
            finally
            {
                teamRepoResource.DeleteRepository();
            }
            
        }
    }
}