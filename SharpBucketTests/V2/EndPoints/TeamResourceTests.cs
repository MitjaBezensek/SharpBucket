using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
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
        private TeamResource AtlassianTeamResource { get; set; }

        private TeamResource UserFirstTeamResource { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            var sharpBucket = TestHelpers.SharpBucketV2;
            var teamsEndPoint = sharpBucket.TeamsEndPoint();

            this.AtlassianTeamResource = teamsEndPoint.TeamResource("atlassian");

            var team = teamsEndPoint.GetUserTeamsWithAdminRole()[0];
            this.UserFirstTeamResource = teamsEndPoint.TeamResource(team.username);
        }

        [Test]
        public void GetProfile_FromTeamAtlassian_ReturnsAtlassianProfile()
        {
            var profile = this.AtlassianTeamResource.GetProfile();
            profile.display_name.ShouldBe("Atlassian");
        }

        [Test]
        public async Task GetProfileAsync_FromTeamAtlassian_ReturnsAtlassianProfile()
        {
            var profile = await this.AtlassianTeamResource.GetProfileAsync();
            profile.display_name.ShouldBe("Atlassian");
        }

        [Test]
        public void ListMembers_FromUserFirstTeam_ShouldReturnLoggedUser()
        {
            var members = this.UserFirstTeamResource.ListMembers();

            var nickname = TestHelpers.AccountName;
            members.ShouldContain(m => m.nickname == nickname);
        }

        [Test]
        public void EnumerateMembers_FromUserFirstTeam_ShouldReturnLoggedUser()
        {
            var members = this.UserFirstTeamResource.EnumerateMembers();

            var nickname = TestHelpers.AccountName;
            members.ShouldContain(m => m.nickname == nickname);
        }

        [Test]
        public async Task EnumerateMembersAsync_FromUserFirstTeam_ShouldReturnLoggedUser()
        {
            var members = await this.UserFirstTeamResource.EnumerateMembersAsync().ToListAsync();

            var nickname = TestHelpers.AccountName;
            members.ShouldContain(m => m.nickname == nickname);
        }

        [Test]
        public void ListFollowers_FromTeamAtlassian_ShouldReturnManyFollowers()
        {
            var followers = this.AtlassianTeamResource.ListFollowers(8);
            followers.Count.ShouldBe(8);
            followers[0].display_name.ShouldBe("Hector Malpica");
        }

        [Test]
        public void EnumerateFollowers_FromTeamAtlassian_ShouldReturnManyFollowers()
        {
            var followers = this.AtlassianTeamResource.EnumerateFollowers(8)
                .Take(8).ToList();
            followers.Count.ShouldBe(8);
            followers[0].display_name.ShouldBe("Hector Malpica");
        }

        [Test]
        public async Task EnumerateFollowersAsync_FromTeamAtlassian_ShouldReturnManyFollowers()
        {
            var followers = await this.AtlassianTeamResource.EnumerateFollowersAsync(8)
                .Take(8).ToListAsync();
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
            var teamRepositoriesResource = UserFirstTeamResource.RepositoriesResource;
            var teamRepoResource = teamRepositoriesResource.RepositoryResource(repositoryName);
            teamRepoResource.PostRepository(teamRepository);

            try
            {
                var repositories = teamRepositoriesResource.ListRepositories();
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
            project = UserFirstTeamResource.PostProject(project);

            try
            {
                var projects = UserFirstTeamResource.ListProjects();
                projects.ShouldNotBeEmpty();
                projects.Any(r => r.name == project.name).ShouldBe(true);
                projects.Select(p => p.ShouldBeFilled())
                    .Any(r => r.name == project.name).ShouldBe(true);

                // also quickly check other sync methods here to avoid to create and delete to much projects
                projects = UserFirstTeamResource.ListProjects(new ListParameters { Filter = "name ~ \"nomatchexpected\"" });
                projects.ShouldBeEmpty();

                projects = UserFirstTeamResource.EnumerateProjects().ToList();
                projects.ShouldNotBeEmpty();
            }
            finally
            {
                UserFirstTeamResource.ProjectResource(project.key).DeleteProject();
            }
        }

        [Test]
        public async Task EnumerateProjectsAsync_AfterHavingCreateOneProjectInTeam_ShouldReturnAtLestTheCreatedProject()
        {
            var projectKey = "Test_" + Guid.NewGuid().ToString("N"); // must start by a letter
            var project = new Project
            {
                key = projectKey,
                name = "Name of " + projectKey,
                is_private = true,
                description = "project created by the unit test ListProjects_AfterHavingAddAProject_ShouldReturnAtLestTheCreatedProject"
            };
            project = await UserFirstTeamResource.PostProjectAsync(project);

            try
            {
                var projects = await UserFirstTeamResource.EnumerateProjectsAsync().ToListAsync();
                projects.ShouldNotBeEmpty();
                projects.Any(r => r.name == project.name).ShouldBe(true);
                projects.Select(p => p.ShouldBeFilled())
                    .Any(r => r.name == project.name).ShouldBe(true);
            }
            finally
            {
                await UserFirstTeamResource.ProjectResource(project.key).DeleteProjectAsync();
            }
        }

        [Test]
        public void EnumerateSearchCodeSearchResults_SearchStringWordFromTeamAtlassian_ShouldReturnAtLeastOneResult()
        {
            var searchResults = this.AtlassianTeamResource.EnumerateSearchCodeSearchResults("string");
            var firstResult = searchResults.FirstOrDefault();
            firstResult.ShouldBeFilled();
        }

        [Test]
        public void EnumerateSearchCodeSearchResults_SearchStringWordFromTeamAtlassianWithPageLenLessThanTheNumberOfEnumeratedResults_RequestsCountShouldIncrementLazily()
        {
            ISharpBucketRequesterV2 realSharpBucketRequesterV2 = TestHelpers.SharpBucketV2;
            var sharpBucketRequesterV2Mock = new Mock<ISharpBucketRequesterV2>();
            Expression<Func<ISharpBucketRequesterV2, IteratorBasedPage<SearchCodeSearchResult>>> sendMethod
                = x => x.Send<IteratorBasedPage<SearchCodeSearchResult>>(It.IsAny<HttpMethod>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<object>());
            sharpBucketRequesterV2Mock
                .Setup(sendMethod)
                .Returns<HttpMethod, object, string, object>((m, b, u, p) => realSharpBucketRequesterV2.Send<IteratorBasedPage<SearchCodeSearchResult>>(m, b, u, p));
            var teamsEndPointIntercepted = new TeamsEndPoint(sharpBucketRequesterV2Mock.Object);

            var searchResults = teamsEndPointIntercepted.TeamResource("atlassian").EnumerateSearchCodeSearchResults("string", 5);

            sharpBucketRequesterV2Mock.Verify(
                sendMethod,
                Times.Never(),
                "Building the enumerable should not produce any request");

            var i = 0;
            foreach (var _ in searchResults)
            {
                if (i < 5)
                {
                    sharpBucketRequesterV2Mock.Verify(
                        sendMethod,
                        Times.Exactly(1),
                        "Only first page should have been called");
                }
                else
                {
                    sharpBucketRequesterV2Mock.Verify(
                        sendMethod,
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