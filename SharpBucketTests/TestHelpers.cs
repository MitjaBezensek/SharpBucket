using System.IO;
using SharpBucket.V2;

namespace SharBucketTests{
   internal class TestHelpers{
      public static SharpBucketV2 GetV2ClientAuthenticatedWithOAuth(){
         var sharpbucket = new SharpBucketV2();
         // Reads test data information from a file, you should structure it like this:
         // By default it reads from c:\
         // ApiKey:yourApiKey
         // SecretApiKey:yourSecretApiKey
         // AccountName:yourAccountName
         // Repository:testRepository
         var lines = File.ReadAllLines("c:\\TestInformationOauth.txt");
         var consumerKey = lines[0].Split(':')[1];
         var consumerSecretKey = lines[1].Split(':')[1];
         sharpbucket.OAuth2LeggedAuthentication(consumerKey, consumerSecretKey);
         return sharpbucket;
      }

      public static SharpBucketV2 GetV2ClientAuthenticatedWithBasicAuthentication(){
         var sharpbucket = new SharpBucketV2();
         // Reads test data information from a file, you should structure it like this:
         // By default it reads from c:\
         // Username:yourUsername
         // Password:yourPassword
         // AccountName:yourAccountName
         // Repository:testRepository
         var lines = File.ReadAllLines("c:\\TestInformation.txt");
         var email = lines[0].Split(':')[1];
         var password = lines[1].Split(':')[1];
         sharpbucket.BasicAuthentication(email, password);
         return sharpbucket;
      }
   }
}