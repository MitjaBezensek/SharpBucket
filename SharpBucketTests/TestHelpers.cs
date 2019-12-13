using System;
using SharpBucket.V2;
using SharpBucketTests.GitHelpers;

namespace SharpBucketTests
{
    internal class TestHelpers
    {
        private const string SbConsumerKey = "SB_CONSUMER_KEY";
        private const string SbConsumerSecretKey = "SB_CONSUMER_SECRET_KEY";
        private const string SbAccountName = "SB_ACCOUNT_NAME";
        private const string SbUserName = "SB_ACCOUNT_EMAIL";
        private const string SbPassword = "SB_ACCOUNT_PASSWORD";

        private static string _accountName;

        public static string AccountName => _accountName
                                            ??= Environment.GetEnvironmentVariable(SbAccountName);

        private static string _oauthConsumerKey;

        /// <summary>
        /// Gets the OAuth consumer key to use to authenticate to the Bitbucket account to use to run the SharpBucket's tests
        /// </summary>
        public static string OAuthConsumerKey => _oauthConsumerKey
                                              ??= Environment.GetEnvironmentVariable(SbConsumerKey);

        private static string _oauthConsumerSecretKey;

        /// <summary>
        /// Gets the OAuth consumer secret key to use to authenticate to the Bitbucket account to use to run the SharpBucket's tests
        /// </summary>
        public static string OAuthConsumerSecretKey => _oauthConsumerSecretKey
                                                    ??= Environment.GetEnvironmentVariable(SbConsumerSecretKey);

        private static string _userName;

        /// <summary>
        /// Gets the username (email) to use for basic authentication on to the Bitbucket account to use to run the SharpBucket's tests
        /// </summary>
        public static string UserName => _userName
                                         ??= Environment.GetEnvironmentVariable(SbUserName);

        private static string _password;

        /// <summary>
        /// Gets the password to use for basic authentication on to the Bitbucket account to use to run the SharpBucket's tests
        /// </summary>
        public static string Password => _password
                                         ??= Environment.GetEnvironmentVariable(SbPassword);

        private static SharpBucketV2 _sharpBucketV2;

        /// <summary>
        /// Gets a shared authenticated instance of <see cref="SharpBucketV2"/>.
        /// The goal is to avoid to open a new authentication for each individual test.
        /// </summary>
        public static SharpBucketV2 SharpBucketV2 => _sharpBucketV2
                                                  ??= GetV2ClientAuthenticatedWithOAuth2();

        private static SharpBucketV2 GetV2ClientAuthenticatedWithOAuth2()
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