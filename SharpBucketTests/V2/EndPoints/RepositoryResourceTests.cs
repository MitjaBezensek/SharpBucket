using System.Collections.Generic;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharBucketTests.V2.EndPoints{
   [TestFixture]
   internal class RepositoryResourceTests{
      private SharpBucketV2 sharpBucket;

      [SetUp]
      public void Init(){
         sharpBucket = TestHelpers.GetV2ClientAuthenticatedWithBasicAuthentication();
      }

      [Test]
      public void GetRepository_FromMercurialRepo_CorrectlyFetchesTheRepoInfo(){
         var repositoryResource = GetRepositoryResource();
         repositoryResource.ShouldNotBe(null);

         var testRepository = repositoryResource.GetRepository();
         testRepository.ShouldNotBe(null);
         testRepository.name.ShouldBe("mercurial");
      }

      [Test]
      public void ListWatchers_FromMercurialRepo_ShouldReturnMoreThan10UniqueWatchers(){
         var repositoryResource = GetRepositoryResource();

         var watchers = repositoryResource.ListWatchers();
         watchers.ShouldNotBe(null);
         watchers.Count.ShouldBeGreaterThan(10);

         var uniqueNames = new HashSet<string>();
         foreach (var watcher in watchers){
            watcher.ShouldNotBe(null);
            string id = watcher.username + watcher.display_name;
            uniqueNames.ShouldNotContain(id);
            uniqueNames.Add(id);
         }
      }

      [Test]
      public void ListForks_FromMercurialRepo_ShouldReturnMoreThan10UniqueForks(){
         var repositoryResource = GetRepositoryResource();

         var forks = repositoryResource.ListForks();
         forks.ShouldNotBe(null);
         forks.Count.ShouldBeGreaterThan(10);

         var uniqueNames = new HashSet<string>();
         foreach (var fork in forks){
            fork.ShouldNotBe(null);
            uniqueNames.ShouldNotContain(fork.full_name);
            uniqueNames.Add(fork.full_name);
         }
      }

      [TestCase(3)]
      [TestCase(501)]
      [Test]
      public void ListCommits_FromMercurialRepoWithSpecifiedMax_ShouldReturnSpecifiedNumberOfCommits(int max){
         var repositoryResource = GetRepositoryResource();

         var commits = repositoryResource.ListCommits(max: max);
         commits.Count.ShouldBe(max);
      }

      private RepositoryResource GetRepositoryResource(){
         var repositoriesEndPoint = sharpBucket.RepositoriesEndPoint();

         var repositoryResource = repositoriesEndPoint.RepositoryResource("mirror", "mercurial");
         return repositoryResource;
      }
   }
}