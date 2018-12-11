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
        private const string TEAM_NAME = "atlassian";

        [SetUp]
        public void Init()
        {
            sharpBucket = TestHelpers.SharpBucketV2;
            teamsEndPoint = sharpBucket.TeamsEndPoint(TEAM_NAME);
        }

        [Test]
        public void GetProfile_FromTeamAtlassian_ReturnsAtlassianProfile()
        {
            teamsEndPoint.ShouldNotBe(null);
            var profile = teamsEndPoint.GetProfile();
            profile.display_name.ShouldBe("Atlassian");
        }

        [Test]
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
        public void ListFollowers_FromTeamAtlassian_ShouldReturnManyFollowers()
        {
            teamsEndPoint.ShouldNotBe(null);
            var followers = teamsEndPoint.ListFollowers(8);
            followers.Count.ShouldBe(8);
            followers[0].display_name.ShouldBe("Hector Miuler Malpica Gallegos");
        }

        [Test]
        public void GetTeams_FromLoggedUser_ShouldReturnManyTeams()
        {
            teamsEndPoint.ShouldNotBe(null);
            var teams = teamsEndPoint.GetUserTeams();
            teams.Count.ShouldBeGreaterThan(0);
        }

        [Test]
        public void ListRepositories_FromUserAdminTeamAfterHavingCreateOneRepoInside_ShouldReturnAtLeastTheCreatedOne()
        {
            teamsEndPoint.ShouldNotBe(null);
            var team = teamsEndPoint.GetUserTeamsWithAdminRole()[0];

            var repositoryName = Guid.NewGuid().ToString().Replace("-", string.Empty);
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
            catch (Exception)
            {
                teamRepoResource.DeleteRepository();
                throw;
            }
            
        }
    }
}