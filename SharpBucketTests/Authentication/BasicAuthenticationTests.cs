using System.Linq;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucketTests.V2;
using Shouldly;

namespace SharpBucketTests.Authentication
{
    [TestFixture]
    internal class BasicAuthenticationTests
    {
        /// <summary>
        /// To avoid having developers to store their password in clear somewhere (environment variable or anything else),
        /// we choose to ignore that test and let developers un-ignore it just the time they need to do a manual run.
        /// Basic authentication is not a common scenario, so it is good enough to allow a developer to enable testing when it impact their usage.
        /// </summary>
        [Test]
        [Ignore("Excluded from automatic runs. We recommend providing your real password in clear only when needed to do a manual run of this test.")]
        public void BasicAuthentication_BasicAuthentication_AllowToListPrivateRepositories()
        {
            var privateRepo = SampleRepositories.PrivateTestRepository.GetRepository();

            var sharpBucket = new SharpBucketV2();
            sharpBucket.BasicAuthentication(TestHelpers.UserName, TestHelpers.Password);
            var accountRepos = sharpBucket.RepositoriesEndPoint()
                .RepositoriesResource(TestHelpers.AccountName).ListRepositories();
            accountRepos.ShouldNotBe(null);
            accountRepos.Any(p => p.is_private == true && p.name == privateRepo.name).ShouldBe(true);
        }
    }
}
