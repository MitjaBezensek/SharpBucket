using System;
using System.IO;
using SharpBucket.V2;

namespace SharBucketTests{
    internal class TestHelpers{
        private const string TestInformationPath = "c:\\TestInformation.txt";
        private const string SbConsumerKey = "SB_CONSUMER_KEY";
        private const string SbConsumerSecretKey = "SB_CONSUMER_SECRET_KEY";

        public static SharpBucketV2 GetV2ClientAuthenticatedWithBasicAuthentication(){
            var testInformation = GetTestInformation();
            var sharpbucket = new SharpBucketV2();
            sharpbucket.BasicAuthentication(testInformation.Username, testInformation.Password);
            return sharpbucket;
        }

        public static SharpBucketV2 GetV2ClientAuthenticatedWithOAuth(){
            var consumerKey = Environment.GetEnvironmentVariable(SbConsumerKey);
            var consumerSecretKey = Environment.GetEnvironmentVariable(SbConsumerSecretKey);
            var sharpbucket = new SharpBucketV2();
            sharpbucket.OAuth2LeggedAuthentication(consumerKey, consumerSecretKey);
            return sharpbucket;
        }

        public static SharpBucketV2 GetV2ClientAuthenticatedWithOAuth2(){
            var consumerKey = Environment.GetEnvironmentVariable(SbConsumerKey);
            var consumerSecretKey = Environment.GetEnvironmentVariable(SbConsumerSecretKey);
            var sharpbucket = new SharpBucketV2();
            sharpbucket.OAuthentication2(consumerKey, consumerSecretKey);
            return sharpbucket;
        }

        public static TestInformation GetTestInformation(){
            // Reads test data information from a file, you should structure it like this:
            // By default it reads from c:\
            // Username:yourUsername
            // Password:yourPassword
            // AccountName:yourAccountName
            // Repository:testRepository
            var lines = File.ReadAllLines(TestInformationPath);

            return new TestInformation
            {
                Username = lines[0].Split(':')[1],
                Password = lines[1].Split(':')[1],
                AccountName = lines[2].Split(':')[1],
                Repository = lines[3].Split(':')[1]
            };
        }
    }

    public class TestInformation
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccountName { get; set; }
        public string Repository { get; set; }
    }

}