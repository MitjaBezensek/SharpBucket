using System.Linq;
using System.Net;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
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

            NotExistingRepository = SampleRepositories.BotTestRepository.IssuesResource();
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
            var parameters = new ListParameters { Filter = "status=open" };
            var issues = ExistingRepository.ListIssues(parameters);
            issues.ShouldNotBeNull();
            issues.Count.ShouldBeGreaterThan(1, "When we don't limit to open issues we can find more!");
        }

        [Test]
        public void ListIssues_NotExistingRepository_ThrowException()
        {
            var exception = Should.Throw<BitbucketV2Exception>(() => NotExistingRepository.ListIssues());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
