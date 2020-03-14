using System;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class ProjectLinksAssertions
    {
        public static ProjectLinks ShouldBeFilled(this ProjectLinks projectLinks)
        {
            projectLinks.ShouldNotBeNull();
            projectLinks.self.ShouldBeFilled();
            projectLinks.html.ShouldBeFilled();
            projectLinks.avatar.ShouldBeFilled();

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
                projectLinks.self.ShouldBeEquivalentTo(expectedProjectLinks.self);
                projectLinks.html.ShouldBeEquivalentTo(expectedProjectLinks.html);
                projectLinks.avatar.ShouldBeEquivalentTo(expectedProjectLinks.avatar);
            }

            return projectLinks;
        }

        public static ProjectLinks ShouldBeEquivalentExceptAvatarTimeStampTo(this ProjectLinks projectLinks, ProjectLinks expectedProjectLinks)
        {
            if (expectedProjectLinks == null)
            {
                projectLinks.ShouldBeNull();
            }
            else
            {
                projectLinks.ShouldNotBeNull();
                projectLinks.self.ShouldBeEquivalentTo(expectedProjectLinks.self);
                projectLinks.html.ShouldBeEquivalentTo(expectedProjectLinks.html);
                var noTimmeStampAvatarUri = new Uri(projectLinks.avatar.href).GetLeftPart(UriPartial.Path);
                var expectedNoTimmeStampAvatarUri = new Uri(expectedProjectLinks.avatar.href).GetLeftPart(UriPartial.Path);
                noTimmeStampAvatarUri.ShouldBe(expectedNoTimmeStampAvatarUri);
            }

            return projectLinks;
        }
    }
}