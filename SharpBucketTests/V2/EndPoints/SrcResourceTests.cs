﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.Utility;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
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
            readMeFile.ShouldBeFilled()
                .And().ShouldBeFile("readme.md");

            var srcDir = treeEntries.FirstOrDefault(treeEntry => treeEntry.type == "commit_directory");
            srcDir.ShouldBeFilled()
                .And().ShouldBeDirectory("src");
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
        public void ListSrcEntries_AtRootOfFirstCommit_GetOneFileAndOneDirectory()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var srcEntries = rootOfFirstCommit.ListSrcEntries();

            srcEntries.ShouldNotBeNull();
            srcEntries.Count.ShouldBe(2);

            var readMeFile = srcEntries.FirstOrDefault(srcEntry => srcEntry.IsFile);
            readMeFile.ShouldBeFilled()
                .And().ShouldBeFile("readme.md");

            var srcDir = srcEntries.FirstOrDefault(treeEntry => treeEntry.IsDirectory);
            srcDir.ShouldBeFilled()
                .And().ShouldBeDirectory("src");
        }

        [Test]
        public void EnumerateTreeEntries_AtRootOfLastCommitOfMainBranch_GetAListing()
        {
            var testRepo = SampleRepositories.TestRepository;
            var currentRoot = testRepo.RepositoryResource.SrcResource();

            var treeEntries = currentRoot.EnumerateTreeEntries().ToList();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBeGreaterThan(0);
        }

        [Test]
        public void EnumerateTreeEntries_AtRootOfFirstCommit_GetOneFileAndOneDirectory()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntries = rootOfFirstCommit.EnumerateTreeEntries().ToList();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(2);

            var readMeFile = treeEntries.FirstOrDefault(treeEntry => treeEntry.type == "commit_file");
            readMeFile.ShouldBeFilled()
                .And().ShouldBeFile("readme.md");

            var srcDir = treeEntries.FirstOrDefault(treeEntry => treeEntry.type == "commit_directory");
            srcDir.ShouldBeFilled()
                .And().ShouldBeDirectory("src");
        }

        [Test]
        public void EnumerateTreeEntries_AtRootOfASubPath_GetListingOfThatSubPath()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);
            var rootOfASubPath = rootOfFirstCommit.SubSrcResource("src");

            var treeEntries = rootOfASubPath.EnumerateTreeEntries().ToList();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(4, "4 elements should be listed");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_file").ShouldBe(3, "3 elements should be files");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_directory").ShouldBe(1, "1 element should be a directory");
        }

        [Test]
        public void EnumerateTreeEntries_ASubPathAtRootOfFirstCommit_GetListingOfThatSubPath()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntries = rootOfFirstCommit.EnumerateTreeEntries("src").ToList();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(4, "4 elements should be listed");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_file").ShouldBe(3, "3 elements should be files");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_directory").ShouldBe(1, "1 element should be a directory");
        }

        [Test]
        public void EnumerateTreeEntries_WithAFilter_GetAFilteredListing()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit, "src");

            var treeEntries = rootOfFirstCommit
                .EnumerateTreeEntries(parameters: new EnumerateParameters { Filter = "path ~ \".txt\"" })
                .ToList();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(3, "3 elements should be listed");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_file").ShouldBe(3, "the 3 elements should be files");
            treeEntries.Count(treeEntry => treeEntry.path.Contains(".txt")).ShouldBe(3, "the 3 elements path should contains '.txt' as requested in the filter query");
        }

        [Test]
        public void EnumerateSrcEntries_AtRootOfFirstCommit_GetOneFileAndOneDirectory()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var srcEntries = rootOfFirstCommit.EnumerateSrcEntries().ToList();

            srcEntries.ShouldNotBeNull();
            srcEntries.Count.ShouldBe(2);

            var readMeFile = srcEntries.FirstOrDefault(srcEntry => srcEntry.IsFile);
            readMeFile.ShouldBeFilled()
                .And().ShouldBeFile("readme.md");

            var srcDir = srcEntries.FirstOrDefault(treeEntry => treeEntry.IsDirectory);
            srcDir.ShouldBeFilled()
                .And().ShouldBeDirectory("src");
        }

        [Test]
        public async Task EnumerateTreeEntriesAsync_AtRootOfLastCommitOfMainBranch_GetAListing()
        {
            var testRepo = SampleRepositories.TestRepository;
            var currentRoot = testRepo.RepositoryResource.SrcResource();

            var treeEntries = await currentRoot.EnumerateTreeEntriesAsync().ToListAsync();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBeGreaterThan(0);
        }

        [Test]
        public async Task EnumerateTreeEntriesAsync_AtRootOfFirstCommit_GetOneFileAndOneDirectory()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntries = await rootOfFirstCommit.EnumerateTreeEntriesAsync().ToListAsync();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(2);

            var readMeFile = treeEntries.FirstOrDefault(treeEntry => treeEntry.type == "commit_file");
            readMeFile.ShouldBeFilled()
                .And().ShouldBeFile("readme.md");

            var srcDir = treeEntries.FirstOrDefault(treeEntry => treeEntry.type == "commit_directory");
            srcDir.ShouldBeFilled()
                .And().ShouldBeDirectory("src");
        }

        [Test]
        public async Task EnumerateTreeEntriesAsync_AtRootOfASubPath_GetListingOfThatSubPath()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);
            var rootOfASubPath = rootOfFirstCommit.SubSrcResource("src");

            var treeEntries = await rootOfASubPath.EnumerateTreeEntriesAsync().ToListAsync();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(4, "4 elements should be listed");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_file").ShouldBe(3, "3 elements should be files");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_directory").ShouldBe(1, "1 element should be a directory");
        }

        [Test]
        public async Task EnumerateTreeEntriesAsync_ASubPathAtRootOfFirstCommit_GetListingOfThatSubPath()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntries = await rootOfFirstCommit.EnumerateTreeEntriesAsync("src").ToListAsync();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(4, "4 elements should be listed");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_file").ShouldBe(3, "3 elements should be files");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_directory").ShouldBe(1, "1 element should be a directory");
        }

        [Test]
        public async Task EnumerateTreeEntriesAsync_WithAFilter_GetAFilteredListing()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit, "src");

            var treeEntries = await rootOfFirstCommit
                .EnumerateTreeEntriesAsync(parameters: new EnumerateParameters { Filter = "path ~ \".txt\"" })
                .ToListAsync();

            treeEntries.ShouldNotBeNull();
            treeEntries.Count.ShouldBe(3, "3 elements should be listed");
            treeEntries.Count(treeEntry => treeEntry.type == "commit_file").ShouldBe(3, "the 3 elements should be files");
            treeEntries.Count(treeEntry => treeEntry.path.Contains(".txt")).ShouldBe(3, "the 3 elements path should contains '.txt' as requested in the filter query");
        }

        [Test]
        public async Task EnumerateSrcEntriesAsync_AtRootOfFirstCommit_GetOneFileAndOneDirectory()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var srcEntries = await rootOfFirstCommit.EnumerateSrcEntriesAsync().ToListAsync();

            srcEntries.ShouldNotBeNull();
            srcEntries.Count.ShouldBe(2);

            var readMeFile = srcEntries.FirstOrDefault(srcEntry => srcEntry.IsFile);
            readMeFile.ShouldBeFilled()
                .And().ShouldBeFile("readme.md");

            var srcDir = srcEntries.FirstOrDefault(treeEntry => treeEntry.IsDirectory);
            srcDir.ShouldBeFilled()
                .And().ShouldBeDirectory("src");
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
        public async Task GetTreeEntryAsync_OfAFileInLastCommitOfMainBranch_GetFileMetadata()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            var treeEntry = await rootOfFirstCommit.GetTreeEntryAsync("readme.md");

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

        [Test]
        public async Task GetTreeEntryAsync_OfAFileInASubPathInLastCommitOfMainBranch_GetFileMetadata()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(path: "src");

            var treeEntry = await rootOfFirstCommit.GetTreeEntryAsync("fileToChange.txt");

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

        [TestCase("readme.md")]
        [TestCase("src/fileToChange.txt")]
        public async Task GetTreeEntryAsync_OfAFileInCurrentPath_GetFileMetadata(string filePath)
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntry = await rootOfFirstCommit.GetTreeEntryAsync(filePath);

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
        [TestCase("src")]
        public async Task GetTreeEntryAsync_OfADirectoryInRootPath_GetDirectoryMetadata(string directoryPath)
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var treeEntry = await rootOfFirstCommit.GetTreeEntryAsync(directoryPath);

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

        [TestCase(null)]
        [TestCase("")]
        [TestCase("subDir")]
        public async Task GetTreeEntryAsync_OfADirectoryInCurrentPath_GetDirectoryMetadata(string directoryPath)
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);
            var rootOfASubPath = rootOfFirstCommit.SubSrcResource("src");

            var treeEntry = await rootOfASubPath.GetTreeEntryAsync(directoryPath);

            treeEntry.ShouldBeFilled()
                .And().ShouldBeDirectory(UrlHelper.ConcatPathSegments("src", directoryPath))
                .And().commit.ShouldBeAReferenceTo(testRepo.RepositoryInfo.FirstCommit);
        }

        [Test]
        public void GetSrcEntry_OfAFile_GetAFileTreeEntry()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            var srcEntry = rootOfFirstCommit.GetSrcEntry("readme.md");

            srcEntry.ShouldBeFilled()
                .And().ShouldBeFile("readme.md");
        }

        [Test]
        public async Task GetSrcEntryAsync_OfAFile_GetAFileTreeEntry()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            var srcEntry = await rootOfFirstCommit.GetSrcEntryAsync("readme.md");

            srcEntry.ShouldBeFilled()
                .And().ShouldBeFile("readme.md");
        }

        [Test]
        public void GetSrcEntry_OfADirectory_GetADirectoryTreeEntry()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            var srcEntry = rootOfFirstCommit.GetSrcEntry("src");

            srcEntry.ShouldBeFilled()
                .And().ShouldBeDirectory("src");
        }

        [Test]
        public async Task GetSrcEntryAsync_OfADirectory_GetADirectoryTreeEntry()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            var srcEntry = await rootOfFirstCommit.GetSrcEntryAsync("src");

            srcEntry.ShouldBeFilled()
                .And().ShouldBeDirectory("src");
        }

        [Test]
        public void GetSrcFile_OfAFile_GetASrcFile()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            var srcFile = rootOfFirstCommit.GetSrcFile("readme.md");

            srcFile.ShouldBeFilled()
                .And().path.ShouldBe("readme.md");
        }

        [Test]
        public async Task GetSrcFileAsync_OfAFile_GetASrcFile()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            var srcFile = await rootOfFirstCommit.GetSrcFileAsync("readme.md");

            srcFile.ShouldBeFilled()
                .And().path.ShouldBe("readme.md");
        }

        [Test]
        public void GetSrcFile_OfADirectory_ThrowsInvalidCastException()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            Assert.That(
                () => rootOfFirstCommit.GetSrcFile("src"),
                Throws.TypeOf<InvalidCastException>().With.Message.Contains("SrcDirectory"));
        }

        [Test]
        public void GetSrcFileAsync_OfADirectory_ThrowsInvalidCastException()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            Assert.That(
                async () => await rootOfFirstCommit.GetSrcFileAsync("src"),
                Throws.TypeOf<InvalidCastException>().With.Message.Contains("SrcDirectory"));
        }

        [Test]
        public void GetSrcFile_OfNull_ThrowsArgumentNullException()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            Assert.That(
                () => rootOfFirstCommit.GetSrcFile(null),
                Throws.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("filePath"));
        }

        [Test]
        public void GetSrcFileAsync_OfNull_ThrowsArgumentNullException()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            Assert.That(
                async () => await rootOfFirstCommit.GetSrcFileAsync(null),
                Throws.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("filePath"));
        }

        [Test]
        public void GetSrcDirectory_OfADirectory_GetASrcDirectory()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            var srcDirectory = rootOfFirstCommit.GetSrcDirectory("src");

            srcDirectory.ShouldBeFilled()
                .And().path.ShouldBe("src");
        }

        [Test]
        public async Task GetSrcDirectoryAsync_OfADirectory_GetASrcDirectory()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            var srcDirectory = await rootOfFirstCommit.GetSrcDirectoryAsync("src");

            srcDirectory.ShouldBeFilled()
                .And().path.ShouldBe("src");
        }

        [Test]
        public void GetSrcDirectory_OfAFile_ThrowsInvalidCastException()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            Assert.That(
                () => rootOfFirstCommit.GetSrcDirectory("readme.md"),
                Throws.TypeOf<InvalidCastException>().With.Message.Contains("SrcFile"));
        }

        [Test]
        public void GetSrcDirectoryAsync_OfAFile_ThrowsInvalidCastException()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            Assert.That(
                async () => await rootOfFirstCommit.GetSrcDirectoryAsync("readme.md"),
                Throws.TypeOf<InvalidCastException>().With.Message.Contains("SrcFile"));
        }

        [Test]
        public void GetSrcDirectory_OfNull_GetRootSrcDirectory()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            var srcDirectory = rootOfFirstCommit.GetSrcDirectory(null);
            srcDirectory.ShouldBeFilled()
                .And().path.ShouldBe(string.Empty);
        }

        [Test]
        public async Task GetSrcDirectoryAsync_OfNull_GetRootSrcDirectory()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource();

            var srcDirectory = await rootOfFirstCommit.GetSrcDirectoryAsync(null);
            srcDirectory.ShouldBeFilled()
                .And().path.ShouldBe(string.Empty);
        }

        [Test]
        public void GetFileContent_OfReadmeFile_GetFileContent()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var fileContent = rootOfFirstCommit.GetFileContent("readme.md");

            fileContent.ShouldBe("This is a test repo generated by the SharpBucket unit tests");
        }

        [Test]
        public async Task GetFileContentAsync_OfReadmeFile_GetFileContent()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var fileContent = await rootOfFirstCommit.GetFileContentAsync("readme.md");

            fileContent.ShouldBe("This is a test repo generated by the SharpBucket unit tests");
        }

        [Test]
        public void GetFileContent_OfAFileThatDoNotExists_ThrowAnException()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var exception = Assert.Throws<BitbucketV2Exception>(() => rootOfFirstCommit.GetFileContent("NotExistingFile.txt"));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            exception.Message.ShouldBe("No such file or directory: NotExistingFile.txt");
        }

        [Test]
        public void GetFileContentAsync_OfAFileThatDoNotExists_ThrowAnException()
        {
            var testRepo = SampleRepositories.TestRepository;
            var rootOfFirstCommit = testRepo.RepositoryResource.SrcResource(testRepo.RepositoryInfo.FirstCommit);

            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await rootOfFirstCommit.GetFileContentAsync("NotExistingFile.txt"));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            exception.Message.ShouldBe("No such file or directory: NotExistingFile.txt");
        }

        [Test]
        public void GetFileContent_OfAFileFromARevisionThatDoNotExists_ThrowAnException()
        {
            var testRepo = SampleRepositories.TestRepository;
            var srcOfNotExistingRevision = testRepo.RepositoryResource.SrcResource("not_existing_revision");

            var exception = Assert.Throws<BitbucketV2Exception>(() => srcOfNotExistingRevision.GetFileContent("AnyFile.txt"));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            exception.Message.ShouldBe("Commit not found");
        }

        [Test]
        public void GetFileContentAsync_OfAFileFromARevisionThatDoNotExists_ThrowAnException()
        {
            var testRepo = SampleRepositories.TestRepository;
            var srcOfNotExistingRevision = testRepo.RepositoryResource.SrcResource("not_existing_revision");

            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await srcOfNotExistingRevision.GetFileContentAsync("AnyFile.txt"));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            exception.Message.ShouldBe("Commit not found");
        }

        [Test]
        public void GetFileContent_OfAFileFromARepositoryThatDoNotExists_ThrowAnException()
        {
            var notExistingRepo = TestHelpers.SharpBucketV2.RepositoriesEndPoint().RepositoryResource("foo", "bar");
            var masterSrcResource = notExistingRepo.SrcResource("master");

            var exception = Assert.Throws<BitbucketV2Exception>(() => masterSrcResource.GetFileContent("AnyFile.txt"));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            exception.Message.ShouldBe("The requested repository either does not exist or you do not have access. If you believe this repository exists and you have access, make sure you're authenticated.");
        }

        [Test]
        public void GetFileContentAsync_OfAFileFromARepositoryThatDoNotExists_ThrowAnException()
        {
            var notExistingRepo = TestHelpers.SharpBucketV2.RepositoriesEndPoint().RepositoryResource("foo", "bar");
            var masterSrcResource = notExistingRepo.SrcResource("master");

            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await masterSrcResource.GetFileContentAsync("AnyFile.txt"));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            exception.Message.ShouldBe("The requested repository either does not exist or you do not have access. If you believe this repository exists and you have access, make sure you're authenticated.");
        }

        [Test]
        public void GetFileContent_OfAFileFromARepositoryThatDoNotExistsWithoutSpecifyingRevision_ThrowAnException()
        {
            var notExistingRepo = TestHelpers.SharpBucketV2.RepositoriesEndPoint().RepositoryResource("foo", "bar");
            var masterSrcResource = notExistingRepo.SrcResource();

            var exception = Assert.Throws<BitbucketV2Exception>(() => masterSrcResource.GetFileContent("AnyFile.txt"));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            exception.Message.ShouldBe("Repository foo/bar not found");
        }

        [Test]
        public void GetFileContentAsync_OfAFileFromARepositoryThatDoNotExistsWithoutSpecifyingRevision_ThrowAnException()
        {
            var notExistingRepo = TestHelpers.SharpBucketV2.RepositoriesEndPoint().RepositoryResource("foo", "bar");
            var masterSrcResource = notExistingRepo.SrcResource();

            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await masterSrcResource.GetFileContentAsync("AnyFile.txt"));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            exception.Message.ShouldBe("Repository foo/bar not found");
        }
    }
}