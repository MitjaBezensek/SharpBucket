namespace SharpBucket.Authentication{
    public abstract class OauthAuthentication{
        protected readonly string ConsumerKey;
        protected readonly string ConsumerSecret;
        protected const string baseUrl = "https://bitbucket.org/api/1.0/";

        protected OauthAuthentication(string consumerKey, string consumerSecret){
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }
    }
}