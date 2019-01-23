using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class ProjectLinksAssertions
    {
        public static ProjectLinks ShouldBeFilled(this ProjectLinks projectLinks)
        {
            projectLinks.ShouldNotBeNull();
            projectLinks.self.href.ShouldNotBeNullOrEmpty();
            projectLinks.html.href.ShouldNotBeNullOrEmpty();
            projectLinks.avatar.href.ShouldNotBeNullOrEmpty();

            return projectLinks;
        }

        public static ProjectLinks ShouldBeEquivalentTo(this ProjectLinks projectLinks, ProjectLinks expectedProjectLinks)
        {
            if (expectedProjectLinks == null)
            {
                projectLinks.ShouldBeNull();
            }
            else
            {
                projectLinks.ShouldNotBeNull();
                projectLinks.self.href.ShouldBe(expectedProjectLinks.self.href);
                projectLinks.html.href.ShouldBe(expectedProjectLinks.html.href);
                projectLinks.avatar.href.ShouldBe(expectedProjectLinks.avatar.href);
            }

            return projectLinks;
        }
    }
}