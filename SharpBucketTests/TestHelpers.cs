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

        private static string _oauthConsumerKey;

        /// <summary>
        /// Gets the OAuth consumer key to use to authenticate to the Bitbucket account to use to run the SharpBucket's tests
        /// </summary>
        public static string OAuthConsumerKey => _oauthConsumerKey
                                              ?? (_oauthConsumerKey = Environment.GetEnvironmentVariable(SbConsumerKey));

        private static string _oauthConsumerSecretKey;

        /// <summary>
        /// Gets the OAuth consumer secret key to use to authenticate to the Bitbucket account to use to run the SharpBucket's tests
        /// </summary>
        public static string OAuthConsumerSecretKey => _oauthConsumerSecretKey
                                                    ?? (_oauthConsumerSecretKey = Environment.GetEnvironmentVariable(SbConsumerSecretKey));

        private static SharpBucketV2 _sharpBucketV2;

        /// <summary>
        /// Gets a shared authenticated instance of <see cref="SharpBucketV2"/>.
        /// The goal is to avoid to open a new authentication for each individual test.
        /// </summary>
        public static SharpBucketV2 SharpBucketV2 => _sharpBucketV2
                                                  ?? (_sharpBucketV2 = GetV2ClientAuthenticatedWithOAuth1());

        public static SharpBucketV2 GetV2ClientAuthenticatedWithBasicAuthentication()
        {
            var sharpBucket = new SharpBucketV2();
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

            sharpBucket.BasicAuthentication(email, password);
            return sharpBucket;
        }

        public static SharpBucketV2 GetV2ClientAuthenticatedWithOAuth1()
        {
            var sharpBucket = new SharpBucketV2();
            sharpBucket.OAuth1TwoLeggedAuthentication(OAuthConsumerKey, OAuthConsumerSecretKey);
            return sharpBucket;
        }

        public static SharpBucketV2 GetV2ClientAuthenticatedWithOAuth2()
        {
            var sharpBucket = new SharpBucketV2();
            sharpBucket.OAuth2ClientCredentials(OAuthConsumerKey, OAuthConsumerSecretKey);
            return sharpBucket;
        }

        public static TestRepositoryBuilder GetTestRepositoryBuilder(string repositoryAccountName, string repositoryName)
        {
            var credentialProvider = new OAuth2GitCredentialsProvider(OAuthConsumerKey, OAuthConsumerSecretKey);
            return new TestRepositoryBuilder($"https://bitbucket.org/{repositoryAccountName}/{repositoryName}.git", credentialProvider);
        }
    }
}