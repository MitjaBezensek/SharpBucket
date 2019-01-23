using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class TeamLinksAssertions
    {
        public static TeamLinks ShouldBeFilled(this TeamLinks teamLinks)
        {
            teamLinks.ShouldNotBeNull();
            teamLinks.self.href.ShouldNotBeNullOrEmpty();
            teamLinks.html.href.ShouldNotBeNullOrEmpty();
            teamLinks.avatar.href.ShouldNotBeNullOrEmpty();

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
                teamLinks.self.href.ShouldBe(expectedTeamLinks.self.href);
                teamLinks.html.href.ShouldBe(expectedTeamLinks.html.href);
                teamLinks.avatar.href.ShouldBe(expectedTeamLinks.avatar.href);
            }

            return teamLinks;
        }
    }
}
