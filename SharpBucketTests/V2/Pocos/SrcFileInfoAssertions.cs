using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class SrcFileInfoAssertions
    {
        public static SrcFileInfo ShouldBeFilled(this SrcFileInfo srcFileInfo)
        {
            srcFileInfo.ShouldNotBeNull();
            srcFileInfo.path.ShouldNotBeNull();
            srcFileInfo.links.ShouldBeFilled();

            return srcFileInfo;
        }
    }
}
