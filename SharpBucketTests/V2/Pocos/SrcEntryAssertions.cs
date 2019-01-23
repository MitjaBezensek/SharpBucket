using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class SrcEntryAssertions
    {
        public static SrcEntry ShouldBeFilled(this SrcEntry srcEntry)
        {
            srcEntry.ShouldNotBeNull();
            srcEntry.IsDirectory.ShouldBe(!srcEntry.IsFile);
            if (srcEntry.IsDirectory)
            {
                srcEntry.SrcDirectory.ShouldBeFilled();
                srcEntry.SrcFile.ShouldBeNull();
                srcEntry.path.ShouldBe(srcEntry.SrcDirectory.path);
                srcEntry.commit.ShouldBe(srcEntry.SrcDirectory.commit);
            }
            else
            {
                srcEntry.SrcDirectory.ShouldBeNull();
                srcEntry.SrcFile.ShouldBeFilled();
                srcEntry.path.ShouldBe(srcEntry.SrcFile.path);
                srcEntry.commit.ShouldBe(srcEntry.SrcFile.commit);
            }

            return srcEntry;
        }

        public static SrcEntry ShouldBeFile(this SrcEntry srcEntry, string filePath)
        {
            srcEntry.IsFile.ShouldBe(true);
            srcEntry.SrcFile.ShouldBeFilled()
                .And().path.ShouldBe(filePath);

            return srcEntry;
        }

        public static SrcEntry ShouldBeDirectory(this SrcEntry srcEntry, string directoryPath)
        {
            srcEntry.IsDirectory.ShouldBe(true);
            srcEntry.SrcDirectory.ShouldBeFilled()
                .And().path.ShouldBe(directoryPath);

            return srcEntry;
        }
    }
}
