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
    internal class IssuesResourceTests
    {
        private IssuesResource ExistingRepository { get; set; }

        private IssuesResource NotExistingRepository { get; set; }

        [OneTimeSetUp]
        protected void Init()
        {
            ExistingRepository = SampleRepositories.BotTestRepository.IssuesResource();

            NotExistingRepository = SampleRepositories.NotExistingRepository.IssuesResource();
        }

        [Test]
        public void ListIssues_ExistingPublicRepositoryWithIssue_ReturnValidInfo()
        {
            var issues = ExistingRepository.ListIssues();

            issues.ShouldNotBeNull();
            issues.Count.ShouldBe(2, "Only two issues are known on BotTestRepository");
        }

        [Test]
        public void ListIssues_ExistingPublicRepositoryWithIssue_OnlyOpen_ReturnValidInfo()
        {
            var parameters = new ListParameters { Filter = "state=\"open\"" };
            var issues = ExistingRepository.ListIssues(parameters);
            issues.ShouldNotBeNull();
            issues.Count.ShouldBe(1, "Only one open issue is known on BotTestRepository");
        }

        [Test]
        public void ListIssues_NotExistingRepository_ThrowException()
        {
            var exception = Should.Throw<BitbucketV2Exception>(
                () => NotExistingRepository.ListIssues());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void EnumerateIssues_ExistingPublicRepositoryWithIssue_ReturnValidInfo()
        {
            var issues = ExistingRepository.EnumerateIssues().ToList();

            issues.ShouldNotBeNull();
            issues.Count.ShouldBe(2, "Only two issues are known on BotTestRepository");
        }

        [Test]
        public void EnumerateIssues_ExistingPublicRepositoryWithIssue_OnlyOpen_ReturnValidInfo()
        {
            var parameters = new EnumerateParameters { Filter = "state=\"open\"" };
            var issues = ExistingRepository.EnumerateIssues(parameters).ToList();
            issues.ShouldNotBeNull();
            issues.Count.ShouldBe(1, "Only one open issue is known on BotTestRepository");
        }

        [Test]
        public void EnumerateIssues_NotExistingRepository_ThrowException()
        {
            var exception = Should.Throw<BitbucketV2Exception>(
                () => NotExistingRepository.EnumerateIssues().ToList());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task EnumerateIssuesAsync_ExistingPublicRepositoryWithIssue_ReturnValidInfo()
        {
            var issues = await ExistingRepository.EnumerateIssuesAsync().ToListAsync();

            issues.ShouldNotBeNull();
            issues.Count.ShouldBe(2, "Only two issues are known on BotTestRepository");
        }

        [Test]
        public async Task EnumerateIssuesAsync_ExistingPublicRepositoryWithIssue_OnlyOpen_ReturnValidInfo()
        {
            var parameters = new EnumerateParameters { Filter = "state=\"open\"" };
            var issues = await ExistingRepository.EnumerateIssuesAsync(parameters).ToListAsync();
            issues.ShouldNotBeNull();
            issues.Count.ShouldBe(1, "Only one open issue is known on BotTestRepository");
        }

        [Test]
        public void EnumerateIssuesAsync_NotExistingRepository_ThrowException()
        {
            var exception = Should.Throw<BitbucketV2Exception>(
                async () => await NotExistingRepository.EnumerateIssuesAsync().ToListAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void PostIssue_NewIssue_IssueIsCreated()
        {
            var issueResource = SampleRepositories.TestRepository.RepositoryResource.IssuesResource();
            var initialIssues = issueResource.ListIssues();
            var newIssue = new Issue
            {
                title = "newIssue",
                kind = IssueKind.Task,
                state = IssueStatus.Open,
                priority = IssuePriority.Minor
            };

            var createdIssue = issueResource.PostIssue(newIssue);
            
            createdIssue.ShouldBeFilled();
            createdIssue.title.ShouldBe(newIssue.title);
            createdIssue.kind.ShouldBe(newIssue.kind);
            createdIssue.state.ShouldBe(newIssue.state);
            createdIssue.priority.ShouldBe(newIssue.priority);
            
            var currentIssues = issueResource.ListIssues();
            currentIssues.Count.ShouldBeGreaterThan(initialIssues.Count);
        }

        [Test]
        public void PostIssue_IssueWithNullTitle_ThrowsException()
        {
            var issueResource = SampleRepositories.TestRepository.RepositoryResource.IssuesResource();
            var newIssue = new Issue
            {
                title = null,
                kind = IssueKind.Task,
                state = IssueStatus.Open,
                priority = IssuePriority.Minor
            };

            Should.Throw<BitbucketV2Exception>(() => issueResource.PostIssue(newIssue));
        }
        
        [Test]
        public async Task PostIssueAsync_NewIssue_IssueIsCreated()
        {
            var issueResource = SampleRepositories.TestRepository.RepositoryResource.IssuesResource();
            var initialIssues = issueResource.ListIssues();
            var newIssue = new Issue
            {
                title = "newIssue",
                kind = IssueKind.Task,
                state = IssueStatus.Open,
                priority = IssuePriority.Minor
            };

            var createdIssue = await issueResource.PostIssueAsync(newIssue);
            
            createdIssue.ShouldBeFilled();
            createdIssue.title.ShouldBe(newIssue.title);
            createdIssue.kind.ShouldBe(newIssue.kind);
            createdIssue.state.ShouldBe(newIssue.state);
            createdIssue.priority.ShouldBe(newIssue.priority);
            
            var currentIssues = issueResource.ListIssues();
            currentIssues.Count.ShouldBeGreaterThan(initialIssues.Count);
        }

        [Test]
        public async Task PostIssueAsync_IssueWithNullTitle_ThrowsException()
        {
            var issueResource = SampleRepositories.TestRepository.RepositoryResource.IssuesResource();
            var newIssue = new Issue
            {
                title = null,
                kind = IssueKind.Task,
                state = IssueStatus.Open,
                priority = IssuePriority.Minor
            };

            Should.Throw<BitbucketV2Exception>(async () => await issueResource.PostIssueAsync(newIssue));
        }
    }
}
