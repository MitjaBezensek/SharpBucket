using System.Linq;
using SharpBucket.Utility;
using SharpBucket.V2.EndPoints;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    using NUnit.Framework;

    [TestFixture]
    public class SrcResourceTests
    {
        [Test]
        public void ListTreeEntries_AtRootOfLastCommitOfMainBranch_GetAListing()
        {
            var testRepo = SampleRepositories.TestRepository;
            var currentRoot = testRepo.RepositoryResource.SrcResource();

            var treeEntries = currentRoot.ListTreeEntries();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBeGreaterThan(0);
        }

        [Test]
        public void ListTreeEntries_AtRootOfFirstCommit_GetOneFileAndOneDirectory()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntries = rootOfFirstCommit.ListTreeEntries();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(2);
            var readMeFile = treeEntries.FirstOrDefault(treeEntry => treeEntry.type == "commit_file");
            readMeFile.ShouldNotBeNull();
            readMeFile.path.ShouldBe("readme.md");
            var srcDir = treeEntries.FirstOrDefault(treeEntry => treeEntry.type == "commit_directory");
            srcDir.ShouldNotBeNull();
            srcDir.path.ShouldBe("src");
        }

        [Test]
        public void ListTreeEntries_AtRootOfASubPath_GetListingOfThatSubPath()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);
            var rootOfASubPath = rootOfFirstCommit.SubSrcResource("src");

            var treeEntries = rootOfASubPath.ListTreeEntries();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(4, "4 elements should be listed");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_file").ShouldBe(3, "3 elements should be files");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_directory").ShouldBe(1, "1 element should be a directory");
        }

        [Test]
        public void ListTreeEntries_ASubPathAtRootOfFirstCommit_GetListingOfThatSubPath()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntries = rootOfFirstCommit.ListTreeEntries("src");

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(4, "4 elements should be listed");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_file").ShouldBe(3, "3 elements should be files");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_directory").ShouldBe(1, "1 element should be a directory");
        }

        [Test]
        public void ListTreeEntries_WithAFilter_GetAFilteredListing()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit, "src");

            var treeEntries = rootOfFirstCommit.ListTreeEntries(listParameters:new ListParameters { Filter = "path ~ \".txt\""});

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(3, "3 elements should be listed");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_file").ShouldBe(3, "the 3 elements should be files");
            treeEntries.Count(treeEntry => treeEntry.path.Contains(".txt")).ShouldBe(3, "the 3 elements path should contains '.txt' as requested in the filter query");
        }

        [Test]
        public void GetTreeEntry_OfAFileInLastCommitOfMainBranch_GetFileMetadata()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            var treeEntry = rootOfFirstCommit.GetTreeEntry("readme.md");

            treeEntry.ShouldBeFilled()
                .And().ShouldBeFile("readme.md");
        }

        [Test]
        public void GetTreeEntry_OfAFileInASubPathInLastCommitOfMainBranch_GetFileMetadata()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(path:"src");

            var treeEntry = rootOfFirstCommit.GetTreeEntry("fileToChange.txt");

            treeEntry.ShouldBeFilled()
                .And().ShouldBeFile("src/fileToChange.txt")
                .And().mimetype.ShouldBe("text/plain", "mime type is not always available on files. Take the opportunity to test it here.");
        }

        [TestCase("readme.md")]
        [TestCase("src/fileToChange.txt")]
        public void GetTreeEntry_OfAFileInCurrentPath_GetFileMetadata(string filePath)
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntry = rootOfFirstCommit.GetTreeEntry(filePath);

            treeEntry.ShouldBeFilled()
                .And().ShouldBeFile(filePath)
                .And().commit.ShouldBeAReferenceTo(testRepo.RepositoryInfo.FirstCommit);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("src")]
        public void GetTreeEntry_OfADirectoryInRootPath_GetDirectoryMetadata(string directoryPath)
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntry = rootOfFirstCommit.GetTreeEntry(directoryPath);

            treeEntry.ShouldBeFilled()
                .And().ShouldBeDirectory(directoryPath ?? string.Empty)
                .And().commit.ShouldBeAReferenceTo(testRepo.RepositoryInfo.FirstCommit);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("subDir")]
        public void GetTreeEntry_OfADirectoryInCurrentPath_GetDirectoryMetadata(string directoryPath)
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);
            var rootOfASubPath = rootOfFirstCommit.SubSrcResource("src");

            var treeEntry = rootOfASubPath.GetTreeEntry(directoryPath);

            treeEntry.ShouldBeFilled()
                .And().ShouldBeDirectory(UrlHelper.ConcatPathSegments("src", directoryPath))
                .And().commit.ShouldBeAReferenceTo(testRepo.RepositoryInfo.FirstCommit);
        }

        [Test]
        public void GetFileContent_OfReadmeFile_GetFileContent()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var fileContent = rootOfFirstCommit.GetFileContent("readme.md");

            fileContent.ShouldBe("This is a test repo generated by the SharpBucket unit tests");
        }
    }
}