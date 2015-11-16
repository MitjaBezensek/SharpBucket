using System;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharBucketTests.V2.EndPoints{
   [TestFixture]
   internal class RepositoriesEndPointTests{
      private SharpBucketV2 sharpBucket;
      private RepositoriesEndPoint repositoriesEndPoint;
      private const string ACCOUNT_NAME = "mirror";

      [SetUp]
      public void Init(){
         sharpBucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth(); 
         repositoriesEndPoint = sharpBucket.RepositoriesEndPoint();
      }

      [Test]
      public void ListRepositories_WithNoMaxSet_ReturnsAtLeast10Repositories(){
         repositoriesEndPoint.ShouldNotBe(null);

         var repositories = repositoriesEndPoint.ListRepositories("mirror");
         repositories.ShouldNotBe(null);
         repositories.Count.ShouldBeGreaterThan(10);
      }

      [Test]
      public void ListPublicRepositories_With30AsMax_Returns30PublicRepositories(){
         var publicRepositories = repositoriesEndPoint.ListPublicRepositories(30);
         publicRepositories.Count.ShouldBe(30);
         publicRepositories[5].full_name.ShouldBe("vetler/fhtmlmps");
      }
        
        [Test]
      public void PostRepository_CreatesAndReturns_Repository(){
        var repositoryToCreate = Guid.NewGuid().ToString().Replace("-", string.Empty);
        var repositoryResource = repositoriesEndPoint.CreateRepository(ACCOUNT_NAME, repositoryToCreate);
        var repositoryFromSut = repositoryResource.GetRepository();
        repositoryFromSut.name.ShouldBe(repositoryToCreate);

        TestHelpers.SwallowException(() => repositoryResource.DeleteRepository());
      }        
    }
}