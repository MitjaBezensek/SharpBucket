using System;
using System.IO;
using SharpBucket.V2;
using SharpBucketTests.GitHelpers;

namespace SharpBucketTests
{
    internal partial class TestHelpers
    {
        private const string TestInformationPath = "c:\\TestInformation.txt";
        private const string SbConsumerKey = "SB_CONSUMER_KEY";
        private const string SbConsumerSecretKey = "SB_CONSUMER_SECRET_KEY";
        private const string SbAccountName = "SB_ACCOUNT_NAME";

        private static SharpBucketV2 _sharpBucketV2;

        /// <summary>
        /// Gets a shared authenticated instance of <see cref="SharpBucketV2"/>.
        /// The goal is to avoid to open a new authentication for each individual test.
        /// </summary>
        public static SharpBucketV2 SharpBucketV2 => _sharpBucketV2
                                                   ?? (_sharpBucketV2 = GetV2ClientAuthenticatedWithOAuth1());

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

        public static SharpBucketV2 GetV2ClientAuthenticatedWithOAuth1()
        {
            var consumerKey = Environment.GetEnvironmentVariable(SbConsumerKey);
            var consumerSecretKey = Environment.GetEnvironmentVariable(SbConsumerSecretKey);
            var sharpbucket = new SharpBucketV2();
            sharpbucket.OAuth1TwoLeggedAuthentication(consumerKey, consumerSecretKey);
            return sharpbucket;
        }

        public static SharpBucketV2 GetV2ClientAuthenticatedWithOAuth2()
        {
            var consumerKey = Environment.GetEnvironmentVariable(SbConsumerKey);
            var consumerSecretKey = Environment.GetEnvironmentVariable(SbConsumerSecretKey);
            var sharpbucket = new SharpBucketV2();
            sharpbucket.OAuth2ClientCredentials(consumerKey, consumerSecretKey);
            return sharpbucket;
        }

        public static TestRepositoryBuilder GetTestRepositoryBuilder(string repositoryAccountName, string repositoryName)
        {
            var consumerKey = Environment.GetEnvironmentVariable(SbConsumerKey);
            var consumerSecretKey = Environment.GetEnvironmentVariable(SbConsumerSecretKey);
            var credentialProvider = new OAuth2GitCredentialsProvider(consumerKey, consumerSecretKey);
            return new TestRepositoryBuilder($"https://bitbucket.org/{repositoryAccountName}/{repositoryName}.git", credentialProvider);
        }
    }
}