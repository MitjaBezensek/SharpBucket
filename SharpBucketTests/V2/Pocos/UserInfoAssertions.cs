using System;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class UserInfoAssertions
    {
        public static UserInfo ShouldBeFilled(this UserInfo userInfo)
        {
            userInfo.ShouldNotBeNull();
            userInfo.type.ShouldNotBeNullOrWhiteSpace();
            userInfo.uuid.ShouldNotBeNullOrWhiteSpace();
            userInfo.display_name.ShouldNotBeNullOrWhiteSpace();
            userInfo.links.ShouldNotBeNull();

            switch (userInfo.type)
            {
                case "user":
                    userInfo.username.ShouldBeNull();
                    userInfo.account_id?.ShouldNotBeNullOrWhiteSpace(); // may be null, but otherwise should be not empty.
                    userInfo.nickname.ShouldNotBeNullOrWhiteSpace();
                    break;
                case "team":
                    userInfo.username.ShouldNotBeNullOrWhiteSpace();
                    userInfo.account_id.ShouldBeNull();
                    userInfo.nickname.ShouldBeNull();
                    break;
                default:
                    throw new Exception($"user type {userInfo.type} is not managed");
            }

            return userInfo;
        }

        public static UserInfo ShouldBeEquivalentTo(this UserInfo userInfo, UserInfo expectedUserInfo)
        {
            if (expectedUserInfo == null)
            {
                userInfo.ShouldBeNull();
            }
            else
            {
                userInfo.ShouldNotBeNull();
                userInfo.type.ShouldBe(expectedUserInfo.type);
                userInfo.uuid.ShouldBe(expectedUserInfo.uuid);
                userInfo.display_name.ShouldBe(expectedUserInfo.display_name);
                userInfo.account_id.ShouldBe(expectedUserInfo.account_id);
                userInfo.nickname.ShouldBe(expectedUserInfo.nickname);
                ////userInfo.links.ShouldBeEquivalentTo(expectedUserInfo.links);
            }

            return userInfo;
        }
    }
}
