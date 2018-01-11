using System;
using SharpBucket.V1;

namespace SharBucketTests
{
    internal partial class TestHelpers
    {
        public static SharpBucketV1 GetV1ClientAuthenticatedWithOAuth()
        {
            //get the environment variable from OS registry key for the current user
            var consumerKey = Environment.GetEnvironmentVariable(SbConsumerKey);
            var consumerSecretKey = Environment.GetEnvironmentVariable(SbConsumerSecretKey);

            var sharpbucket = new SharpBucketV1();
            sharpbucket.OAuth2LeggedAuthentication(consumerKey, consumerSecretKey);
            return sharpbucket;
        }

        public static string GetAccountName()
        {
            return Environment.GetEnvironmentVariable(SbAccountName);
        }
    }
}