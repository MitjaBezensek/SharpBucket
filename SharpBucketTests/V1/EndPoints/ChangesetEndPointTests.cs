using NUnit.Framework;
using SharpBucket.V1;
using SharpBucket.V1.EndPoints;
using Shouldly;
using System;
using System.Linq;

namespace SharBucketTests.V1.EndPoints
{
    [TestFixture]
    public class ChangesetEndPointTests
    {
        private SharpBucketV1 sharpBucket;
        private RepositoriesEndPoint repositoriesEndPoint;
        private string accountName;

        private const string REPOSITORY_NAME = "mercurial";

        [SetUp]
        public void Init()
        {
            Environment.SetEnvironmentVariable("SB_ACCOUNT_NAME", "mirror");
            Environment.SetEnvironmentVariable("SB_CONSUMER_SECRET_KEY", "yedGsmuE6q4gzh3rqppvE66mfSqnEAKg");
            Environment.SetEnvironmentVariable("SB_CONSUMER_KEY", "5Y6cBVjaunnx5cS65S");

            sharpBucket = TestHelpers.GetV1ClientAuthenticatedWithOAuth();
            accountName = TestHelpers.GetAccountName();
            repositoriesEndPoint = sharpBucket.RepositoriesEndPoint(accountName, REPOSITORY_NAME);
        }


        [Test]
        public void GetChangesetDiffstat_when_limit_is_specified_should_return_that_number_of_diffsets()
        {
            repositoriesEndPoint.ShouldNotBe(null);

            var result = repositoriesEndPoint.ListChangeset();
            result.ShouldNotBe(null);
            result.changesets.Count.ShouldNotBe(0);

            var commit = result.changesets.First();
            
            var stats = repositoriesEndPoint.GetChangesetDiffstat(commit.node, 1);

            stats.Count().ShouldBe(1);
        }

        [Test]
        public void GetChangesetDiffstat_when_start_is_specified_should_return_from_that_index()
        {
            repositoriesEndPoint.ShouldNotBe(null);

            var result = repositoriesEndPoint.ListChangeset();
            result.ShouldNotBe(null);
            result.changesets.Count.ShouldNotBe(0);

            var commit = result.changesets.First();

            var stats = repositoriesEndPoint.GetChangesetDiffstat(commit.node, 1);

            var firstFile = stats.First().file;

            stats = repositoriesEndPoint.GetChangesetDiffstat(commit.node, 1, 1);

            var skippedFile = stats.First().file;

            skippedFile.ShouldNotBe(firstFile);
        }

    }

}
