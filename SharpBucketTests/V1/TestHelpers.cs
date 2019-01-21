using System;
using SharpBucket.V1;

namespace SharpBucketTests
{
    internal partial class TestHelpers
    {
        [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
        public static SharpBucketV1 GetV1ClientAuthenticatedWithOAuth()
        {
            //get the environment variable from OS registry key for the current user
            var consumerKey = Environment.GetEnvironmentVariable(SbConsumerKey);
            var consumerSecretKey = Environment.GetEnvironmentVariable(SbConsumerSecretKey);

            var sharpBucket = new SharpBucketV1();
            sharpBucket.OAuth2LeggedAuthentication(consumerKey, consumerSecretKey);
            return sharpBucket;
        }

        public static string GetAccountName()
        {
            return Environment.GetEnvironmentVariable(SbAccountName);
        }
    }
}