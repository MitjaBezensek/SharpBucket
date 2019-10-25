using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    class TeamResourceTests
    {
        private SharpBucketV2 sharpBucket;
        private TeamsEndPoint teamsEndPoint;
        private TeamResource teamResource;

        [OneTimeSetUp]
        public void Init()
        {
            sharpBucket = TestHelpers.SharpBucketV2;
            teamsEndPoint = sharpBucket.TeamsEndPoint();

            var team = teamsEndPoint.GetUserTeamsWithAdminRole()[0];
            teamResource = teamsEndPoint.TeamResource(team.username);
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

            var firstTeamResource = teamsEndPoint.TeamResource(teams[0].uuid);
            var members = firstTeamResource.ListMembers(35);
            members.Count.ShouldBeGreaterThan(0);
            var nickname = sharpBucket.UserEndPoint().GetUser().nickname;
            members.ShouldContain(m => m.nickname == nickname);
        }

        [Test]
        public void ListFollowers_FromTeamAtlassian_ShouldReturnManyFollowers()
        {
            teamsEndPoint.ShouldNotBe(null);
            var followers = teamsEndPoint.TeamResource("atlassian").ListFollowers(8);
            followers.Count.ShouldBe(8);
            followers[0].display_name.ShouldBe("Hector Malpica");
        }

        [Test]
        public void ListRepositories_AfterHavingCreateOneRepoInTeam_ShouldReturnAtLeastTheCreatedRepo()
        {
            var repositoryName = Guid.NewGuid().ToString("N");
            var teamRepository = new Repository
            {
                name = repositoryName,
                language = "c#",
                scm = "git"
            };
            var teamRepoResource = teamResource.RepositoryResource(repositoryName);
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

        [Test]
        public void ListProjects_AfterHavingCreateOneProjectInTeam_ShouldReturnAtLestTheCreatedProject()
        {
            var projectKey = "Test_" + Guid.NewGuid().ToString("N"); // must start by a letter
            var project = new Project
            {
                key = projectKey,
                name = "Name of " + projectKey,
                is_private = true,
                description = "project created by the unit test ListProjects_AfterHavingAddAProject_ShouldReturnAtLestTheCreatedProject"
            };
            project = teamResource.PostProject(project);

            try
            {
                var projects = teamResource.ListProjects();
                projects.ShouldNotBeEmpty();
                projects.Any(r => r.name == project.name).ShouldBe(true);
                projects.Select(p => p.ShouldBeFilled())
                    .Any(r => r.name == project.name).ShouldBe(true);
            }
            finally
            {
                teamResource.ProjectResource(project.key).DeleteProject();
            }
        }

        [Test]
        public void EnumerateSearchCodeSearchResults_SearchStringWordFromTeamAtlassian_ShouldReturnAtLeastOneResult()
        {
            teamsEndPoint.ShouldNotBe(null);

            var searchResults = teamsEndPoint.TeamResource("atlassian").EnumerateSearchCodeSearchResults("string");
            var firstResult = searchResults.FirstOrDefault();
            firstResult.ShouldBeFilled();
        }

        [Test]
        public void EnumerateSearchCodeSearchResults_SearchStringWordFromTeamAtlassianWithPageLenLessThanTheNumberOfEnumeratedResults_RequestsCountShouldIncrementLazily()
        {
            var realSharpBucketRequesterV2 = (ISharpBucketRequesterV2)this.sharpBucket;
            var sharpBucketRequesterV2Mock = new Mock<ISharpBucketRequesterV2>();
            sharpBucketRequesterV2Mock
                .Setup(x => x.Get<IteratorBasedPage<SearchCodeSearchResult>>(It.IsAny<string>(), It.IsAny<object>()))
                .Returns<string, object>((s, o) => realSharpBucketRequesterV2.Get<IteratorBasedPage<SearchCodeSearchResult>>(s, o));
            var teamsEndPointIntercepted = new TeamsEndPoint(sharpBucketRequesterV2Mock.Object);

            var searchResults = teamsEndPointIntercepted.TeamResource("atlassian").EnumerateSearchCodeSearchResults("string", 5);

            sharpBucketRequesterV2Mock.Verify(
                x => x.Get<IteratorBasedPage<SearchCodeSearchResult>>(It.IsAny<string>(), It.IsAny<object>()),
                Times.Never,
                "Building the enumerable should not produce any request");

            var i = 0;
            foreach (var _ in searchResults)
            {
                if (i < 5)
                {
                    sharpBucketRequesterV2Mock.Verify(
                        x => x.Get<IteratorBasedPage<SearchCodeSearchResult>>(It.IsAny<string>(), It.IsAny<object>()),
                        Times.Exactly(1),
                        "Only first page should have been called");
                }
                else
                {
                    sharpBucketRequesterV2Mock.Verify(
                        x => x.Get<IteratorBasedPage<SearchCodeSearchResult>>(It.IsAny<string>(), It.IsAny<object>()),
                        Times.Exactly(2),
                        "Only two pages should have been called");
                    if (i == 9)
                    {
                        break;
                    }
                }

                i++;
            }
        }
    }
}