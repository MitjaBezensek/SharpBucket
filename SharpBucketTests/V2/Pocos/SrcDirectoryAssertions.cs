using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class SrcDirectoryAssertions
    {
        public static SrcDirectory ShouldBeFilled(this SrcDirectory srcDirectory)
        {
            srcDirectory.ShouldNotBeNull();
            srcDirectory.path.ShouldNotBeNull();
            srcDirectory.links.ShouldBeFilled();
            srcDirectory.commit.ShouldBeFilled();

            return srcDirectory;
        }
    }
}
