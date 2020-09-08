using System;
using System.Threading.Tasks;
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

        [SetUp]
        public void Init()
        {
            sharpBucket = TestHelpers.SharpBucketV2;
            usersEndPoint = sharpBucket.UsersEndPoint(SampleRepositories.MIRROR_ACCOUNT_UUID);
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
            profile.username.ShouldBeNull(nameof(profile.username) + " is no more available due to GDPR concerns");
            profile.website.ShouldBeNull(nameof(profile.website) + " is no more available due to GDPR concerns");
            profile.location.ShouldBeNull(nameof(profile.location) + " is no more available due to GDPR concerns");
#pragma warning restore 618
        }

        [Test]
        public async Task GetProfileAsync_FromMirrorAccount_ShouldReturnTheMirrorProfile()
        {
            usersEndPoint.ShouldNotBe(null);
            var profile = await usersEndPoint.GetProfileAsync();

            profile.uuid.ShouldNotBeNullOrEmpty(nameof(profile.uuid));
            profile.account_id.ShouldNotBeNullOrEmpty(nameof(profile.account_id));
            profile.nickname.ShouldBe("mirror", nameof(profile.nickname));
            profile.type.ShouldBe("user", nameof(profile.type));
            profile.display_name.ShouldBe("mirror", nameof(profile.display_name));
            profile.created_on.ShouldBe("2008-06-26T13:58:38+00:00");
            profile.account_status.ShouldBe("active", nameof(profile.account_status));

            // Obsolete properties. Their values are expected to become null or day or another
#pragma warning disable 618
            profile.username.ShouldBeNull(nameof(profile.username) + " is no more available due to GDPR concerns");
            profile.website.ShouldBeNull(nameof(profile.website) + " is no more available due to GDPR concerns");
            profile.location.ShouldBeNull(nameof(profile.location) + " is no more available due to GDPR concerns");
#pragma warning restore 618
        }

        [Test]
        [Obsolete("Test of an obsolete method")]
        public void ListFollowers_FromMirrorAccount_ShouldThrowExceptionWithDeprecationMessage()
        {
            usersEndPoint.ShouldNotBe(null);

            var exception = Assert.Throws<BitbucketV2Exception>(() => usersEndPoint.ListFollowers(15));
            exception.Message.ShouldContain("Resource not found");
        }

        [Test]
        [Obsolete("Test of an obsolete method")]
        public void ListFollowing_FromMirrorAccount_ShouldThrowExceptionWithDeprecationMessage()
        {
            usersEndPoint.ShouldNotBe(null);

            var exception = Assert.Throws<BitbucketV2Exception>(() => usersEndPoint.ListFollowing());
            exception.Message.ShouldContain("Resource not found");
        }

        [Test]
        [Obsolete("Test of an obsolete method")]
        public void ListRepositories_FromMirrorAccount_ShouldReturnMirrorsRepositories()
        {
            usersEndPoint.ShouldNotBe(null);
            var repositories = usersEndPoint.ListRepositories();
            repositories.Count.ShouldBeGreaterThan(10);
            repositories = usersEndPoint.ListRepositories(max: 5);
            repositories.Count.ShouldBe(5);
        }
    }
}