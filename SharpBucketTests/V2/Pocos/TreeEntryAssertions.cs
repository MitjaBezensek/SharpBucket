using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class TreeEntryAssertions
    {
        public static TreeEntry ShouldBeFilled(this TreeEntry treeEntry)
        {
            treeEntry.ShouldNotBeNull();
            treeEntry.type.ShouldNotBeNull();
            treeEntry.path.ShouldNotBeNull();
            treeEntry.links.ShouldBeFilled();
            treeEntry.commit.ShouldBeFilled();

            return treeEntry;
        }

        public static TreeEntry ShouldBeDirectory(this TreeEntry treeEntry, string directoryPath)
        {
            treeEntry.type.ShouldBe("commit_directory");
            treeEntry.path.ShouldBe(directoryPath);
            treeEntry.size.ShouldBeNull("size is not send for directories");
            treeEntry.attributes.ShouldBeNull("attributes are not send for directories");
            treeEntry.mimetype.ShouldBeNull("mimetype is not send for directories");
            treeEntry.links.history.ShouldBeNull("directories are not concerned by file history");

            return treeEntry;
        }

        public static TreeEntry ShouldBeFile(this TreeEntry treeEntry, string filePath)
        {
            treeEntry.type.ShouldBe("commit_file");
            treeEntry.path.ShouldBe(filePath);
            treeEntry.size.ShouldNotBeNull();
            treeEntry.size?.ShouldBeGreaterThan(0);
            treeEntry.attributes.ShouldNotBeNull();
            ////treeEntry.mimetype.ShouldNotBeNull(); // mime type is filled only when the extension is recognized by BitBucket, otherwise it's null
            treeEntry.links.history.href.ShouldNotBeNull();

            return treeEntry;
        }
    }
}
