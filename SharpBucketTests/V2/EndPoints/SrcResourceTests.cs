using System;
using System.Linq;
using SharpBucket.Utility;
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
            var currentRoot = testRepo.RepositoryResource.SrcResource(null);

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
            var readMeFile = treeEntries.FirstOrDefault(treeEntry => treeEntry.Type == "commit_file");
            readMeFile.ShouldNotBeNull();
            readMeFile.Path.ShouldBe("readme.md");
            var srcDir = treeEntries.FirstOrDefault(treeEntry => treeEntry.Type == "commit_directory");
            srcDir.ShouldNotBeNull();
            srcDir.Path.ShouldBe("src");
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
            treeEntries.Count(treeEntry => treeEntry.Type == "commit_file").ShouldBe(3, "3 elements should be files");
            treeEntries.Count(treeEntry => treeEntry.Type == "commit_directory").ShouldBe(1, "1 element should be a directory");
        }

        [Test]
        public void ListTreeEntries_ASubPathAtRootOfFirstCommit_GetListingOfThatSubPath()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntries = rootOfFirstCommit.ListTreeEntries("src");

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(4, "4 elements should be listed");
            treeEntries.Count(treeEntry => treeEntry.Type == "commit_file").ShouldBe(3, "3 elements should be files");
            treeEntries.Count(treeEntry => treeEntry.Type == "commit_directory").ShouldBe(1, "1 element should be a directory");
        }

        [TestCase("readme.md")]
        [TestCase("src/fileToChange.txt")]
        public void GetTreeEntry_OfAFileInCurrentPath_GetFileMetadata(string filePath)
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntry = rootOfFirstCommit.GetTreeEntry(filePath);

            treeEntry.ShouldNotBeNull();
            treeEntry.Type.ShouldBe("commit_file");
            treeEntry.Path.ShouldBe(filePath);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("src")]
        public void GetTreeEntry_OfADirectoryInRootPath_GetDirectoryMetadata(string directoryPath)
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntry = rootOfFirstCommit.GetTreeEntry(directoryPath);

            treeEntry.ShouldNotBeNull();
            treeEntry.Type.ShouldBe("commit_directory");
            treeEntry.Path.ShouldBe(directoryPath ?? string.Empty);
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

            treeEntry.ShouldNotBeNull();
            treeEntry.Type.ShouldBe("commit_directory");
            treeEntry.Path.ShouldBe(UrlHelper.ConcatPathSegments("src", directoryPath));
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