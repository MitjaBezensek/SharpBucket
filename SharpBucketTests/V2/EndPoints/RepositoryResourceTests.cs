using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    internal class RepositoryResourceTests
    {
        [Test]
        public void GetRepository_FromMercurialRepo_CorrectlyFetchesTheRepoInfo()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            repositoryResource.ShouldNotBe(null);
            var testRepository = repositoryResource.GetRepository();

            testRepository.ShouldBeFilled();
            testRepository.name.ShouldBe(SampleRepositories.MERCURIAL_REPOSITORY_NAME);
            testRepository.website.ShouldNotBeNullOrEmpty(); // this repository is an example of one where website is filled
        }

        [Test]
        public async Task GetRepositoryAsync_FromMercurialRepo_CorrectlyFetchesTheRepoInfo()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            repositoryResource.ShouldNotBe(null);
            var testRepository = await repositoryResource.GetRepositoryAsync();

            testRepository.ShouldBeFilled();
            testRepository.name.ShouldBe(SampleRepositories.MERCURIAL_REPOSITORY_NAME);
            testRepository.website.ShouldNotBeNullOrEmpty(); // this repository is an example of one where website is filled
        }

        [Test]
        public void ListWatchers_FromMercurialRepo_ShouldReturnMoreThan10UniqueWatchers()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            var watchers = repositoryResource.ListWatchers();
            watchers.Count.ShouldBeGreaterThan(10);

            var uniqueNames = new HashSet<string>();
            foreach (var watcher in watchers)
            {
                watcher.ShouldBeFilled();
                uniqueNames.Add(watcher.uuid).ShouldBe(true, $"value ${watcher.uuid} is not unique");
            }
        }

        [Test]
        public void EnumerateWatchers_FromMercurialRepo_ShouldReturnMoreThan10UniqueWatchers()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            var watchers = repositoryResource.EnumerateWatchers();

            var uniqueNames = new HashSet<string>();
            foreach (var watcher in watchers)
            {
                watcher.ShouldBeFilled();
                uniqueNames.Add(watcher.uuid).ShouldBe(true, $"value ${watcher.uuid} is not unique");
            }
            uniqueNames.Count.ShouldBeGreaterThan(10);
        }

        [Test]
        public async Task EnumerateWatchersAsync_FromMercurialRepo_ShouldReturnMoreThan10UniqueWatchers()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            var watchers = repositoryResource.EnumerateWatchersAsync();

            var uniqueNames = new HashSet<string>();
            await foreach (var watcher in watchers)
            {
                watcher.ShouldBeFilled();
                uniqueNames.Add(watcher.uuid).ShouldBe(true, $"value ${watcher.uuid} is not unique");
            }
            uniqueNames.Count.ShouldBeGreaterThan(10);
        }

        [Test]
        public void ListForks_FromMercurialRepo_ShouldReturnMoreThan10UniqueForks()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            var forks = repositoryResource.ListForks();
            forks.Count.ShouldBeGreaterThan(10);

            var uniqueNames = new HashSet<string>();
            foreach (var fork in forks)
            {
                fork.ShouldBeFilled();

                // since they are forks of mercurial, their parent should be mercurial
                fork.parent.ShouldBeFilled();
                fork.parent.name.ShouldBe(SampleRepositories.MERCURIAL_REPOSITORY_NAME);

                uniqueNames.Add(fork.full_name).ShouldBe(true, $"value ${fork.full_name} is not unique");
            }
        }

        [Test]
        public void EnumerateForks_FromMercurialRepo_ShouldReturnMoreThan10UniqueForks()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            var forks = repositoryResource.EnumerateForks();

            var uniqueNames = new HashSet<string>();
            foreach (var fork in forks)
            {
                fork.ShouldBeFilled();

                // since they are forks of mercurial, their parent should be mercurial
                fork.parent.ShouldBeFilled();
                fork.parent.name.ShouldBe(SampleRepositories.MERCURIAL_REPOSITORY_NAME);

                uniqueNames.Add(fork.full_name).ShouldBe(true, $"value ${fork.full_name} is not unique");
            }
            uniqueNames.Count.ShouldBeGreaterThan(10);
        }

        [Test]
        public async Task EnumerateForksAsync_FromMercurialRepo_ShouldReturnMoreThan10UniqueForks()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            var forks = repositoryResource.EnumerateForksAsync();

            var uniqueNames = new HashSet<string>();
            await foreach (var fork in forks)
            {
                fork.ShouldBeFilled();

                // since they are forks of mercurial, their parent should be mercurial
                fork.parent.ShouldBeFilled();
                fork.parent.name.ShouldBe(SampleRepositories.MERCURIAL_REPOSITORY_NAME);

                uniqueNames.Add(fork.full_name).ShouldBe(true, $"value ${fork.full_name} is not unique");
            }
            uniqueNames.Count.ShouldBeGreaterThan(10);
        }

        [TestCase(3)]
        [TestCase(103)]
        [Test]
        public void ListCommits_FromMercurialRepoWithSpecifiedMax_ShouldReturnSpecifiedNumberOfCommits(int max)
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            repositoryResource.ShouldNotBe(null);
            var commits = repositoryResource.ListCommits(max: max);
            commits.Count.ShouldBe(max);
        }

        [Test]
        public void ListCommits_OnASpecifiedBranch_ShouldReturnTheRightNumberOfCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var allCommits = repositoryResource.ListCommits();
            var commitsOnMaster = repositoryResource.ListCommits("master");
            var commitsOnToAccept = repositoryResource.ListCommits("branchToAccept");
            var commitsOnToDecline = repositoryResource.ListCommits("branchToDecline");

            allCommits.Count.ShouldBe(5);
            commitsOnMaster.Count.ShouldBe(2);
            commitsOnToAccept.Count.ShouldBe(4);
            commitsOnToDecline.Count.ShouldBe(3);
        }

        [Test]
        public void ListCommits_ExcludingABranch_ShouldReturnTheRightNumberOfCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var commits = repositoryResource.ListCommits(new ListCommitsParameters { Excludes = { "master" } });

            commits.Count.ShouldBe(3);
        }

        [Test]
        public void ListCommits_CombiningAnIncludeAndAnExclude_ShouldReturnTheRightNumberOfCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var commits = repositoryResource.ListCommits(new ListCommitsParameters { Includes = { "branchToDecline" }, Excludes = { "master" } });

            commits.Count.ShouldBe(1);
        }

        [Test]
        public void ListCommits_OfABranchExcludingMaster_ShouldReturnOnlyBranchCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var commits = repositoryResource.ListCommits("branchToAccept", new ListCommitsParameters { Excludes = { "master" } });

            commits.Count.ShouldBe(2);
        }

        [Test]
        public void ListCommits_WithAPath_ShouldReturnTheRightNumberOfCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var commits = repositoryResource.ListCommits(new ListCommitsParameters { Path = "src/" });

            commits.Count.ShouldBe(4); // only the bad commit in branchToDecline do not change anything in src path
        }

        [Test]
        public void EnumerateCommits_OnASpecifiedBranch_ShouldReturnTheRightNumberOfCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var allCommits = repositoryResource.EnumerateCommits();
            var commitsOnMaster = repositoryResource.EnumerateCommits("master");

            allCommits.Count().ShouldBe(5);
            commitsOnMaster.Count().ShouldBe(2);
        }

        [Test]
        public void EnumerateCommits_ExcludingABranch_ShouldReturnTheRightNumberOfCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var commits = repositoryResource.EnumerateCommits(new EnumerateCommitsParameters { Excludes = { "master" } });

            commits.Count().ShouldBe(3);
        }

        [Test]
        public void EnumerateCommits_CombiningAnIncludeAndAnExclude_ShouldReturnTheRightNumberOfCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var commits = repositoryResource.EnumerateCommits(new EnumerateCommitsParameters { Includes = { "branchToDecline" }, Excludes = { "master" } });

            commits.Count().ShouldBe(1);
        }

        [Test]
        public void EnumerateCommits_WithAPath_ShouldReturnTheRightNumberOfCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var commits = repositoryResource.EnumerateCommits(new EnumerateCommitsParameters { Path = "src/" });

            commits.Count().ShouldBe(4); // only the bad commit in branchToDecline do not change anything in src path
        }

        [Test]
        public async Task EnumerateCommitsAsync_OnASpecifiedBranch_ShouldReturnTheRightNumberOfCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var allCommits = repositoryResource.EnumerateCommitsAsync();
            var commitsOnMaster = repositoryResource.EnumerateCommitsAsync("master");

            (await allCommits.CountAsync()).ShouldBe(5);
            (await commitsOnMaster.CountAsync()).ShouldBe(2);
        }

        [Test]
        public async Task EnumerateCommitsAsync_ExcludingABranch_ShouldReturnTheRightNumberOfCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var commits = repositoryResource.EnumerateCommitsAsync(new EnumerateCommitsParameters { Excludes = { "master" } });

            (await commits.CountAsync()).ShouldBe(3);
        }

        [Test]
        public async Task EnumerateCommitsAsync_CombiningAnIncludeAndAnExclude_ShouldReturnTheRightNumberOfCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var commits = repositoryResource.EnumerateCommitsAsync(new EnumerateCommitsParameters { Includes = { "branchToDecline" }, Excludes = { "master" } });

            (await commits.CountAsync()).ShouldBe(1);
        }

        [Test]
        public async Task EnumerateCommitsAsync_WithAPath_ShouldReturnTheRightNumberOfCommits()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;

            var commits = repositoryResource.EnumerateCommitsAsync(new EnumerateCommitsParameters { Path = "src/" });

            (await commits.CountAsync()).ShouldBe(4); // only the bad commit in branchToDecline do not change anything in src path
        }

        [Test]
        public void GetCommit_AKnownHashOnMercurialRepository_ShouldReturnCorrectData()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;

            var commit = repositoryResource.GetCommit("abae1eb695c077fa21b6ef0b7056f36d63cf0302");

            commit.ShouldNotBeNull();
            commit.hash.ShouldBe("abae1eb695c077fa21b6ef0b7056f36d63cf0302");
            commit.date.ShouldNotBeNullOrWhiteSpace();
            commit.message.ShouldNotBeNullOrWhiteSpace();
            commit.author.raw.ShouldNotBeNullOrWhiteSpace();
            commit.author.user.ShouldBeFilled();
            commit.links.ShouldNotBeNull();
            commit.parents[0].ShouldBeFilled();
            commit.repository.uuid.ShouldNotBeNullOrWhiteSpace();
            commit.repository.full_name.ShouldNotBeNullOrWhiteSpace();
            commit.repository.name.ShouldNotBeNullOrWhiteSpace();
            commit.repository.links.ShouldNotBeNull();
            commit.summary.ShouldBeFilled();
        }

        [Test]
        public async Task GetCommitAsync_AKnownHashOnMercurialRepository_ShouldReturnCorrectData()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;

            var commit = await repositoryResource.GetCommitAsync("abae1eb695c077fa21b6ef0b7056f36d63cf0302");

            commit.ShouldNotBeNull();
            commit.hash.ShouldBe("abae1eb695c077fa21b6ef0b7056f36d63cf0302");
            commit.date.ShouldNotBeNullOrWhiteSpace();
            commit.message.ShouldNotBeNullOrWhiteSpace();
            commit.author.raw.ShouldNotBeNullOrWhiteSpace();
            commit.author.user.ShouldBeFilled();
            commit.links.ShouldNotBeNull();
            commit.parents[0].ShouldBeFilled();
            commit.repository.uuid.ShouldNotBeNullOrWhiteSpace();
            commit.repository.full_name.ShouldNotBeNullOrWhiteSpace();
            commit.repository.name.ShouldNotBeNullOrWhiteSpace();
            commit.repository.links.ShouldNotBeNull();
            commit.summary.ShouldBeFilled();
        }

        [Test]
        public void PostRepository_NewPublicRepository_CorrectlyCreatesTheRepository()
        {
            var accountName = TestHelpers.AccountName;
            var repositoryName = Guid.NewGuid().ToString("N");
            var repositoryResource = SampleRepositories.RepositoriesEndPoint.RepositoryResource(accountName, repositoryName);
            var repository = new Repository
            {
                name = repositoryName,
                language = "c#",
                scm = "git"
            };

            var repositoryFromPost = repositoryResource.PostRepository(repository);
            repositoryFromPost.name.ShouldBe(repositoryName);
            repositoryFromPost.scm.ShouldBe("git");
            repositoryFromPost.language.ShouldBe("c#");

            var repositoryFromGet = repositoryResource.GetRepository();
            repositoryFromGet.name.ShouldBe(repositoryName);
            repositoryFromGet.scm.ShouldBe("git");
            repositoryFromGet.language.ShouldBe("c#");

            repositoryFromPost.full_name.ShouldBe(repositoryFromGet.full_name);
            repositoryFromPost.uuid.ShouldBe(repositoryFromGet.uuid);

            repositoryResource.DeleteRepository();
        }

        [Test]
        public async Task PostRepositoryAsync_NewPublicRepository_CorrectlyCreatesTheRepository()
        {
            var accountName = TestHelpers.AccountName;
            var repositoryName = Guid.NewGuid().ToString("N");
            var repositoryResource = SampleRepositories.RepositoriesEndPoint.RepositoryResource(accountName, repositoryName);
            var repository = new Repository
            {
                name = repositoryName,
                language = "c#",
                scm = "git"
            };

            var repositoryFromPost = await repositoryResource.PostRepositoryAsync(repository);
            repositoryFromPost.name.ShouldBe(repositoryName);
            repositoryFromPost.scm.ShouldBe("git");
            repositoryFromPost.language.ShouldBe("c#");

            var repositoryFromGet = await repositoryResource.GetRepositoryAsync();
            repositoryFromGet.name.ShouldBe(repositoryName);
            repositoryFromGet.scm.ShouldBe("git");
            repositoryFromGet.language.ShouldBe("c#");

            repositoryFromPost.full_name.ShouldBe(repositoryFromGet.full_name);
            repositoryFromPost.uuid.ShouldBe(repositoryFromGet.uuid);

            await repositoryResource.DeleteRepositoryAsync();
        }

        [Test]
        public void PostRepository_InATeamWhereIHaveNoRights_ThrowAnException()
        {
            var repositoryName = Guid.NewGuid().ToString("N");
            var repositoryResource = SampleRepositories.RepositoriesEndPoint.RepositoryResource(SampleRepositories.MERCURIAL_ACCOUNT_NAME, repositoryName);
            var repository = new Repository
            {
                name = repositoryName
            };

            var exception = Assert.Throws<BitbucketV2Exception>(() => repositoryResource.PostRepository(repository));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.Forbidden);
            exception.ErrorResponse.error.message.ShouldBe("You cannot administer personal accounts of other users.");
        }

        [Test]
        public void PostRepositoryAsync_InATeamWhereIHaveNoRights_ThrowAnException()
        {
            var repositoryName = Guid.NewGuid().ToString("N");
            var repositoryResource = SampleRepositories.RepositoriesEndPoint.RepositoryResource(SampleRepositories.MERCURIAL_ACCOUNT_NAME, repositoryName);
            var repository = new Repository
            {
                name = repositoryName
            };

            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await repositoryResource.PostRepositoryAsync(repository));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.Forbidden);
            exception.ErrorResponse.error.message.ShouldBe("You cannot administer personal accounts of other users.");
        }

        [Test]
        public void ApproveCommitAndDeleteCommitApproval_TestRepository_CommitStateChangedCorrectly()
        {
            var currentUser = TestHelpers.AccountName;
            var testRepository = SampleRepositories.TestRepository;
            var repositoryResource = testRepository.RepositoryResource;
            var firstCommit = testRepository.RepositoryInfo.FirstCommit;
            var initialCommit = repositoryResource.GetCommit(firstCommit);
            initialCommit?.participants.Any(p => p.User.nickname == currentUser && p.Approved).ShouldBe(false, "Initial state should be: 'not approved'");

            var userRole = repositoryResource.ApproveCommit(firstCommit);
            var approvedCommit = repositoryResource.GetCommit(firstCommit);
            repositoryResource.DeleteCommitApproval(firstCommit);
            var notApprovedCommit = repositoryResource.GetCommit(firstCommit);

            userRole.Approved.ShouldBe(true);
            userRole.User.nickname.ShouldBe(currentUser);
            userRole.Role.ShouldBe("PARTICIPANT");
            approvedCommit?.participants.Any(p => p.User.nickname == currentUser && p.Approved).ShouldBe(true, "Commit should be approved after call to ApproveCommit");
            notApprovedCommit?.participants.Any(p => p.User.nickname == currentUser && p.Approved).ShouldBe(false, "Commit should not be approved after call to DeleteCommitApproval");
        }

        [Test]
        public async Task ApproveAsyncCommitAsyncAndDeleteCommitApprovalAsync_TestRepository_CommitStateChangedCorrectly()
        {
            var currentUser = TestHelpers.AccountName;
            var testRepository = SampleRepositories.TestRepository;
            var repositoryResource = testRepository.RepositoryResource;
            var firstCommit = testRepository.RepositoryInfo.FirstCommit;
            var initialCommit = await repositoryResource.GetCommitAsync(firstCommit);
            initialCommit?.participants.Any(p => p.User.nickname == currentUser && p.Approved).ShouldBe(false, "Initial state should be: 'not approved'");

            var userRole = await repositoryResource.ApproveCommitAsync(firstCommit);
            var approvedCommit = await repositoryResource.GetCommitAsync(firstCommit);
            await repositoryResource.DeleteCommitApprovalAsync(firstCommit);
            var notApprovedCommit = await repositoryResource.GetCommitAsync(firstCommit);

            userRole.Approved.ShouldBe(true);
            userRole.User.nickname.ShouldBe(currentUser);
            userRole.Role.ShouldBe("PARTICIPANT");
            approvedCommit?.participants.Any(p => p.User.nickname == currentUser && p.Approved).ShouldBe(true, "Commit should be approved after call to ApproveCommit");
            notApprovedCommit?.participants.Any(p => p.User.nickname == currentUser && p.Approved).ShouldBe(false, "Commit should not be approved after call to DeleteCommitApproval");
        }

        [Test]
        public void BuildStatusInfo_AddGetChangeOnFirstCommit_ShouldWork()
        {
            var testRepository = SampleRepositories.TestRepository;
            var repositoryResource = testRepository.RepositoryResource;
            var firstCommit = testRepository.RepositoryInfo.FirstCommit;

            var firstBuildStatus = new BuildInfo
            {
                key = "FooBuild42",
                state = BuildInfoState.INPROGRESS,
                url = "https://foo.com/builds/{repository.full_name}",
                name = "Foo Build #42",
                description = "fake build status from a fake build server"
            };
            var buildInfo = repositoryResource.AddNewBuildStatus(firstCommit, firstBuildStatus);
            buildInfo.ShouldNotBeNull();
            buildInfo.state.ShouldBe(firstBuildStatus.state);
            buildInfo.name.ShouldBe(firstBuildStatus.name);
            buildInfo.description.ShouldBe(firstBuildStatus.description);

            var getBuildInfo = repositoryResource.GetBuildStatusInfo(firstCommit, "FooBuild42");
            getBuildInfo.ShouldNotBeNull();
            getBuildInfo.state.ShouldBe(BuildInfoState.INPROGRESS);

            getBuildInfo.state = BuildInfoState.SUCCESSFUL;
            var changedBuildInfo = repositoryResource.ChangeBuildStatusInfo(firstCommit, "FooBuild42", getBuildInfo);
            changedBuildInfo.ShouldNotBeNull();
            changedBuildInfo.state.ShouldBe(BuildInfoState.SUCCESSFUL);
        }

        [Test]
        public async Task BuildStatusInfo_AddGetChangeOnFirstCommitAsync_ShouldWork()
        {
            var testRepository = SampleRepositories.TestRepository;
            var repositoryResource = testRepository.RepositoryResource;
            var firstCommit = testRepository.RepositoryInfo.FirstCommit;

            var firstBuildStatus = new BuildInfo
            {
                key = "FooBuild42",
                state = BuildInfoState.INPROGRESS,
                url = "https://foo.com/builds/{repository.full_name}",
                name = "Foo Build #42",
                description = "fake build status from a fake build server"
            };
            var buildInfo = await repositoryResource.AddNewBuildStatusAsync(firstCommit, firstBuildStatus);
            buildInfo.ShouldNotBeNull();
            buildInfo.state.ShouldBe(firstBuildStatus.state);
            buildInfo.name.ShouldBe(firstBuildStatus.name);
            buildInfo.description.ShouldBe(firstBuildStatus.description);

            var getBuildInfo = await repositoryResource.GetBuildStatusInfoAsync(firstCommit, "FooBuild42");
            getBuildInfo.ShouldNotBeNull();
            getBuildInfo.state.ShouldBe(BuildInfoState.INPROGRESS);

            getBuildInfo.state = BuildInfoState.SUCCESSFUL;
            var changedBuildInfo = await repositoryResource.ChangeBuildStatusInfoAsync(firstCommit, "FooBuild42", getBuildInfo);
            changedBuildInfo.ShouldNotBeNull();
            changedBuildInfo.state.ShouldBe(BuildInfoState.SUCCESSFUL);
        }

        [Test]
        public void GetMainBranchRevision_TestRepository_GetShaOfTheLastCommitOnMainBranch()
        {
            var testRepository = SampleRepositories.TestRepository;
            var repositoryResource = testRepository.RepositoryResource;

            var revision = repositoryResource.GetMainBranchRevision();

            revision.ShouldBe(testRepository.RepositoryInfo.MainBranchLastCommit);
        }

        [Test]
        public async Task GetMainBranchRevisionAsync_TestRepository_GetShaOfTheLastCommitOnMainBranch()
        {
            var testRepository = SampleRepositories.TestRepository;
            var repositoryResource = testRepository.RepositoryResource;

            var revision = await repositoryResource.GetMainBranchRevisionAsync();

            revision.ShouldBe(testRepository.RepositoryInfo.MainBranchLastCommit);
        }

        [Test]
        public void GetMainBranchRevision_FromARepositoryThatDoNotExists_ThrowAnException()
        {
            var notExistingRepo = TestHelpers.SharpBucketV2.RepositoriesEndPoint().RepositoryResource("foo", "bar");

            var exception = Assert.Throws<BitbucketV2Exception>(() => notExistingRepo.GetMainBranchRevision());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            exception.Message.ShouldBe("Repository foo/bar not found");
        }

        [Test]
        public void GetMainBranchRevisionAsync_FromARepositoryThatDoNotExists_ThrowAnException()
        {
            var notExistingRepo = TestHelpers.SharpBucketV2.RepositoriesEndPoint().RepositoryResource("foo", "bar");

            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await notExistingRepo.GetMainBranchRevisionAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            exception.Message.ShouldBe("Repository foo/bar not found");
        }
    }
}