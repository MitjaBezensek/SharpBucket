using System;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.EndPoints.Extras;
using Shouldly;

namespace SharBucketTests.V2.EndPoints.Extras{
   [TestFixture]
   internal class RepositoriesEndPointMixinsTests{
      private SharpBucketV2 sharpBucket;
      private RepositoriesEndPoint repositoriesEndPoint;
      private const string ACCOUNT_NAME = "mirror";

      [SetUp]
      public void Init(){
         sharpBucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth(); 
         repositoriesEndPoint = sharpBucket.RepositoriesEndPoint();
      }

      [Test]
      public void CanCreatePrivateNonForkableCSharpRepository(){
         var repositoryToCreate = Guid.NewGuid().ToString().Replace("-", string.Empty);
          var repositoryResource = repositoriesEndPoint.CreateRepository(ACCOUNT_NAME, repositoryToCreate, c =>
          {
              c.MakePrivate();
              c.SetLanguage("c#");
              c.SetForkPolicy(ForkWord.NoForks);
          });
          var repositoryFromSut = repositoryResource.GetRepository();
          repositoryFromSut.is_private.ShouldBe(true);
          repositoryFromSut.fork_policy.ShouldBe("no_forks");
          repositoryFromSut.language.ShouldBe("c#");

          TestHelpers.SwallowException(() => repositoryResource.DeleteRepository());
        }
    }
}