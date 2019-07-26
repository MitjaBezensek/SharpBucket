using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class TeamLinksAssertions
    {
        public static TeamLinks ShouldBeFilled(this TeamLinks teamLinks)
        {
            teamLinks.ShouldNotBeNull();
            teamLinks.self.ShouldBeFilled();
            teamLinks.html.ShouldBeFilled();
            teamLinks.avatar.ShouldBeFilled();

            return teamLinks;
        }

        public static TeamLinks ShouldBeEquivalentTo(this TeamLinks teamLinks, TeamLinks expectedTeamLinks)
        {
            if (expectedTeamLinks == null)
            {
                teamLinks.ShouldBeNull();
            }
            else
            {
                teamLinks.ShouldNotBeNull();
                teamLinks.self.ShouldBeEquivalentTo(expectedTeamLinks.self);
                teamLinks.html.ShouldBeEquivalentTo(expectedTeamLinks.html);
                teamLinks.avatar.ShouldBeEquivalentTo(expectedTeamLinks.avatar);
            }

            return teamLinks;
        }
    }
}
