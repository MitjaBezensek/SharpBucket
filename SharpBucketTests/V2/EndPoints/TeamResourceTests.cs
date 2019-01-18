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
    class TeamResourceTests
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
        public void GetProfile_FromTeamAtlassian_ReturnsAtlassianProfile()
        {
            teamsEndPoint.ShouldNotBe(null);
            var profile = teamsEndPoint.TeamResource("atlassian").GetProfile();
            profile.display_name.ShouldBe("Atlassian");
        }

        [Test]
        public void ListMembers_FromFirstTeamOfLoggedUser_ShouldReturnManyMembers()
        {
            teamsEndPoint.ShouldNotBe(null);
            var teams = teamsEndPoint.GetUserTeams();
            teams.Count.ShouldBeGreaterThan(0);

            var firstTeamResource = teamsEndPoint.TeamResource(teams[0].username);
            var members = firstTeamResource.ListMembers(35);
            members.Count.ShouldBeGreaterThan(0);
            var userName = sharpBucket.UserEndPoint().GetUser().username;
            members.ShouldContain(m => m.username == userName);
        }

        [Test]
        public void ListFollowers_FromTeamAtlassian_ShouldReturnManyFollowers()
        {
            teamsEndPoint.ShouldNotBe(null);
            var followers = teamsEndPoint.TeamResource("atlassian").ListFollowers(8);
            followers.Count.ShouldBe(8);
            followers[0].display_name.ShouldBe("Hector Miuler Malpica Gallegos");
        }

        [Test]
        public void ListRepositories_FromUserAdminTeamAfterHavingCreateOneRepoInside_ShouldReturnAtLeastTheCreatedOne()
        {
            this.teamsEndPoint.ShouldNotBe(null);
            var team = this.teamsEndPoint.GetUserTeamsWithAdminRole()[0];

            var repositoryName = Guid.NewGuid().ToString("N");
            var teamRepository = new Repository
            {
                name = repositoryName,
                language = "c#",
                scm = "git"
            };
            var teamResource = this.teamsEndPoint.TeamResource(team.username);
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