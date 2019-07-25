using System;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class UserShortAssertions
    {
        public static UserShort ShouldBeFilled(this UserShort userShort)
        {
            userShort.ShouldNotBeNull();
            userShort.uuid.ShouldNotBeNullOrWhiteSpace();
            userShort.type.ShouldNotBeNullOrWhiteSpace();
            userShort.display_name.ShouldNotBeNullOrWhiteSpace();
            userShort.links.ShouldNotBeNull();

            switch (userShort.type)
            {
                case "user":
                    userShort.account_id?.ShouldNotBeNullOrWhiteSpace(); // may be null, but otherwise should be not empty.
                    userShort.nickname.ShouldNotBeNullOrWhiteSpace();
                    break;
                case "team":
                    userShort.account_id.ShouldBeNull();
                    userShort.nickname.ShouldBeNull();
#pragma warning disable 618 // that field is still valid for teams
                    userShort.username.ShouldNotBeNullOrWhiteSpace();
#pragma warning restore 618
                    break;
                default:
                    throw new Exception($"user type {userShort.type} is not managed");
            }

            return userShort;
        }
    }
}
