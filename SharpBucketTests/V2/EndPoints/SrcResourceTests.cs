using System.Linq;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    using NUnit.Framework;

    [TestFixture]
    public class SrcResourceTests
    {
        [Test]
        public void ListTreeEntries_AtRootOfFirstCommit_GetOneFileAndOneDirectory()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntries = rootOfFirstCommit.ListTreeEntries();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(2);
            var readMeFile = treeEntries.FirstOrDefault(treeEntry => treeEntry.Type == "commit_file");
            readMeFile.ShouldNotBeNull();
            readMeFile.Path.ShouldBe("readme.md");
            var srcDir = treeEntries.FirstOrDefault(treeEntry => treeEntry.Type == "commit_directory");
            srcDir.ShouldNotBeNull();
            srcDir.Path.ShouldBe("src");
        }
    }
}