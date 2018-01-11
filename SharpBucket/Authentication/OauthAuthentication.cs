namespace SharpBucket.Authentication
{
    public abstract class OauthAuthentication : Authenticate
    {
        protected readonly string ConsumerKey;
        protected readonly string ConsumerSecret;
        protected readonly string _baseUrl;

        protected OauthAuthentication(string consumerKey, string consumerSecret, string baseUrl)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            _baseUrl = baseUrl;
        }
    }
}