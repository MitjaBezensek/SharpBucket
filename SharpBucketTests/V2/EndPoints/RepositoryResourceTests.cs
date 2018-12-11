using System;
using System.Collections.Generic;
using NUnit.Framework;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    internal class RepositoryResourceTests
    {
        [Test]
        public void GetRepository_FromMercurialRepo_CorrectlyFetchesTheRepoInfo()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            repositoryResource.ShouldNotBe(null);
            var testRepository = repositoryResource.GetRepository();
            testRepository.ShouldNotBe(null);
            testRepository.name.ShouldBe(SampleRepositories.MERCURIAL_REPOSITORY_NAME);
        }

        [Test]
        public void ListWatchers_FromMercurialRepo_ShouldReturnMoreThan10UniqueWatchers()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            repositoryResource.ShouldNotBe(null);
            var watchers = repositoryResource.ListWatchers();
            watchers.ShouldNotBe(null);
            watchers.Count.ShouldBeGreaterThan(10);

            var uniqueNames = new HashSet<string>();
            foreach (var watcher in watchers)
            {
                watcher.ShouldNotBe(null);
                string id = watcher.username + watcher.display_name;
                uniqueNames.ShouldNotContain(id);
                uniqueNames.Add(id);
            }
        }

        [Test]
        public void ListForks_FromMercurialRepo_ShouldReturnMoreThan10UniqueForks()
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            repositoryResource.ShouldNotBe(null);
            var forks = repositoryResource.ListForks();
            forks.ShouldNotBe(null);
            forks.Count.ShouldBeGreaterThan(10);

            var uniqueNames = new HashSet<string>();
            foreach (var fork in forks)
            {
                fork.ShouldNotBe(null);
                uniqueNames.ShouldNotContain(fork.full_name);
                uniqueNames.Add(fork.full_name);
            }
        }

        [TestCase(3)]
        [TestCase(103)]
        [Test]
        public void ListCommits_FromMercurialRepoWithSpecifiedMax_ShouldReturnSpecifiedNumberOfCommits(int max)
        {
            var repositoryResource = SampleRepositories.MercurialRepository;
            repositoryResource.ShouldNotBe(null);
            var commits = repositoryResource.ListCommits(max: max);
            commits.Count.ShouldBe(max);
        }

        [Test]
        public void CreateRepository_NewPublicRepository_CorrectlyCreatesTheRepository()
        {
            var accountName = TestHelpers.GetAccountName();
            var repositoryName = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var repositoryResource = SampleRepositories.RepositoriesEndPoint.RepositoryResource(accountName, repositoryName);
            var repository = new Repository
            {
                name = repositoryName,
                language = "c#",
                scm = "git"
            };

            var repositoryFromPost = repositoryResource.PostRepository(repository);
            repositoryFromPost.name.ShouldBe(repositoryName);
            repositoryFromPost.scm.ShouldBe("git");
            repositoryFromPost.scm.ShouldBe("git");

            var repositoryFromGet = repositoryResource.GetRepository();
            repositoryFromGet.name.ShouldBe(repositoryName);
            repositoryFromGet.scm.ShouldBe("git");
            repositoryFromGet.scm.ShouldBe("git");

            repositoryFromPost.full_name.ShouldBe(repositoryFromGet.full_name);
            repositoryFromPost.uuid.ShouldBe(repositoryFromGet.uuid);

            repositoryResource.DeleteRepository();
        }
    }
}