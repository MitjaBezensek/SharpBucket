using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    class UsersEndPointTests
    {
        private SharpBucketV2 sharpBucket;
        private UsersEndpoint usersEndPoint;
        private const string ACCOUNT_NAME = "mirror";

        [SetUp]
        public void Init()
        {
            sharpBucket = TestHelpers.SharpBucketV2;
            usersEndPoint = sharpBucket.UsersEndPoint(ACCOUNT_NAME);
        }

        [Test]
        public void GetProfile_FromMirrorAccount_ShouldReturnTheMirrorProfile()
        {
            usersEndPoint.ShouldNotBe(null);
            var profile = usersEndPoint.GetProfile();

            profile.uuid.ShouldNotBeNullOrEmpty(nameof(profile.uuid));
            profile.account_id.ShouldNotBeNullOrEmpty(nameof(profile.account_id));
            profile.nickname.ShouldBe("mirror", nameof(profile.nickname));
            profile.type.ShouldBe("user", nameof(profile.type));
            profile.display_name.ShouldBe("mirror", nameof(profile.display_name));
            profile.created_on.ShouldBe("2008-06-26T13:58:38+00:00");
            profile.account_status.ShouldBe("active", nameof(profile.account_status));

            // Obsolete properties. Their values are expected to become null or day or another
#pragma warning disable 618
            profile.username.ShouldBe("mirror", nameof(profile.username));
            profile.website.ShouldBe("https://bitbucket.org/mirror/", nameof(profile.website));
            profile.location.ShouldBeNull(nameof(profile.location));
#pragma warning restore 618
        }

        [Test]
        public void ListFollowers_FromMirrorAccount_ShouldReturnMirrorsFollowers()
        {
            usersEndPoint.ShouldNotBe(null);
            var followers = usersEndPoint.ListFollowers(15);
            followers.Count.ShouldBe(15);
            followers[0].display_name.ShouldBe("z19");
        }

        // the test doesn't work anymore because the data is not returned anymore
        // and the endpoint will soon be removed: https://developer.atlassian.com/cloud/bitbucket/bitbucket-api-changes-gdpr/
        // so it's useless to try to maintain it
        ////[Test]
        ////public void ListFollowing_FromMirrorAccount_ShouldReturnMirrorMembers()
        ////{
        ////    usersEndPoint.ShouldNotBe(null);
        ////    var following = usersEndPoint.ListFollowing();
        ////    following.Count.ShouldBe(1);
        ////    following[0].display_name.ShouldBe("Jesper Noehr");
        ////}

        [Test]
        public void ListRepositories_FromMirrorAccount_ShouldReturnMirrorsRepositories()
        {
            usersEndPoint.ShouldNotBe(null);
            var repositories = usersEndPoint.ListRepositories();
            repositories.Count.ShouldBeGreaterThan(10);
            repositories = usersEndPoint.ListRepositories(max: 25);
            repositories.Count.ShouldBe(25);
        }
    }
}