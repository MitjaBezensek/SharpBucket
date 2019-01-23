using System;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class TeamInfoAssertions
    {
        public static TeamInfo ShouldBeFilled(this TeamInfo teamInfo)
        {
            teamInfo.ShouldNotBeNull();
            teamInfo.uuid.ShouldNotBeNullOrEmpty();
            teamInfo.username.ShouldNotBeNullOrEmpty();
            teamInfo.display_name.ShouldNotBeNullOrEmpty();
            teamInfo.links.ShouldBeFilled();

            return teamInfo;
        }

        public static TeamInfo ShouldBeEquivalentTo(this TeamInfo teamInfo, TeamInfo expectedTeamInfo)
        {
            if (expectedTeamInfo == null)
            {
                teamInfo.ShouldBeNull();
            }
            else
            {
                teamInfo.ShouldNotBeNull();
                teamInfo.uuid.ShouldBe(expectedTeamInfo.uuid);
                teamInfo.username.ShouldBe(expectedTeamInfo.username);
                teamInfo.display_name.ShouldBe(expectedTeamInfo.display_name);
                teamInfo.links.ShouldBeEquivalentTo(expectedTeamInfo.links);
            }

            return teamInfo;
        }
    }
}
