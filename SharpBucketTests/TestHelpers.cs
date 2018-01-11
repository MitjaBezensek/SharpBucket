using System;
using System.IO;
using SharpBucket.V2;

namespace SharBucketTests
{
    internal partial class TestHelpers
    {
        private const string TestInformationPath = "c:\\TestInformation.txt";
        private const string SbConsumerKey = "SB_CONSUMER_KEY";
        private const string SbConsumerSecretKey = "SB_CONSUMER_SECRET_KEY";
        private const string SbAccountName = "SB_ACCOUNT_NAME";

        public static SharpBucketV2 GetV2ClientAuthenticatedWithBasicAuthentication()
        {
            var sharpbucket = new SharpBucketV2();
            // Reads test data information from a file, you should structure it like this:
            // By default it reads from c:\
            // Username:yourUsername
            // Password:yourPassword
            // AccountName:yourAccountName
            // Repository:testRepository
            var lines = File.ReadAllLines(TestInformationPath);
            var email = lines[0].Split(':')[1];
            var password = lines[1].Split(':')[1];
            var accountName = lines[2].Split(':')[1];
            var repository = lines[3].Split(':')[1];

            sharpbucket.BasicAuthentication(email, password);
            return sharpbucket;
        }

        public static SharpBucketV2 GetV2ClientAuthenticatedWithOAuth()
        {
            var consumerKey = Environment.GetEnvironmentVariable(SbConsumerKey);
            var consumerSecretKey = Environment.GetEnvironmentVariable(SbConsumerSecretKey);
            var sharpbucket = new SharpBucketV2();
            sharpbucket.OAuth2LeggedAuthentication(consumerKey, consumerSecretKey);
            return sharpbucket;
        }

        public static SharpBucketV2 GetV2ClientAuthenticatedWithOAuth2()
        {
            var consumerKey = Environment.GetEnvironmentVariable(SbConsumerKey);
            var consumerSecretKey = Environment.GetEnvironmentVariable(SbConsumerSecretKey);
            var sharpbucket = new SharpBucketV2();
            sharpbucket.OAuthentication2(consumerKey, consumerSecretKey);
            return sharpbucket;
        }
    }
}