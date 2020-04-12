using System.Linq;
using System.Net;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
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
            var exception = Should.Throw<BitbucketV2Exception>(() => NotExistingRepository.ListIssues());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
