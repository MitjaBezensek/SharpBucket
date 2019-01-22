using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class SrcFileAssertions
    {
        public static SrcFile ShouldBeFilled(this SrcFile srcFile)
        {
            srcFile.ShouldNotBeNull();
            srcFile.path.ShouldNotBeNull();
            srcFile.links.ShouldBeFilled();
            srcFile.commit.ShouldBeFilled();
            srcFile.attributes.ShouldNotBeNull();
            srcFile.size.ShouldBeGreaterThanOrEqualTo(0);
            srcFile.mimetype.CouldBeNull();

            return srcFile;
        }
    }
}
