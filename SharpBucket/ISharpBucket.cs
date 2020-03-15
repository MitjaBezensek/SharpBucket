using SharpBucket.Authentication;

namespace SharpBucket
{
    /// <summary>
    /// Interface that expose all the methods available on the <see cref="SharpBucket"/> class.
    /// This interface should be used for mocking <see cref="SharpBucket"/>.
    /// </summary>
    /// <remarks>
    /// Since this interface may change in the future (typically to declare new authentication methods),
    /// We recommend to not create wrappers in our code and limit usages to mocking senarios.
    /// If you want to create wrappers or similar things, it's prefered to just do it over the
    /// <see cref="ISharpBucketRequester"/> interface/ which is expected to be more stable in future developments.
    /// </remarks>
    public interface ISharpBucket : ISharpBucketRequester
    {
        /// <summary>
        /// Do not use authentication with the BitBucket API. Only public data can be retrieved.
        /// </summary>
        void NoAuthentication();

        /// <summary>   
        /// Use basic authentication with the BitBucket API. OAuth authentication is preferred over
        /// basic authentication, due to security reasons.
        /// </summary>
        /// <param name="username">Your BitBucket user name.</param>
        /// <param name="password">Your BitBucket password.</param>
        void BasicAuthentication(string username, string password);

        /// <summary>
        /// Use 2 legged OAuth 1.0a authentication. This is similar to basic authentication, since
        /// it requires the same number of steps. It is still safer to use than basic authentication, 
        /// since you can revoke the API keys.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/OAuth+on+Bitbucket
        /// </summary>
        /// <param name="consumerKey">Your consumer API key obtained from the BitBucket web page.</param>
        /// <param name="consumerSecretKey">Your consumer secret API key also obtained from the BitBucket web page.</param>
        void OAuth2LeggedAuthentication(string consumerKey, string consumerSecretKey);

        /// <summary>
        /// Use 2 legged OAuth 1.0a authentication. This is similar to basic authentication, since
        /// it requires the same number of steps. It is still safer to use than basic authentication, 
        /// since you can revoke the API keys.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/OAuth+on+Bitbucket
        /// </summary>
        /// <param name="consumerKey">Your consumer API key obtained from the BitBucket web page.</param>
        /// <param name="consumerSecretKey">Your consumer secret API key also obtained from the BitBucket web page.</param>
        void OAuth1TwoLeggedAuthentication(string consumerKey, string consumerSecretKey);

        /// <summary>
        /// Use 3 legged OAuth 1.0a authentication. This is the most secure one, but for simple uses it might
        /// be a bit too complex.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/OAuth+on+Bitbucket
        /// </summary>
        /// <param name="consumerKey">Your consumer API key obtained from the BitBucket web page.</param>
        /// <param name="consumerSecretKey">Your consumer secret API key also obtained from the BitBucket web page.</param>
        /// <param name="callback">Callback URL to which BitBucket will send the pin.</param>
        /// <returns></returns>
        OAuth1ThreeLeggedAuthentication OAuth1ThreeLeggedAuthentication(
            string consumerKey,
            string consumerSecretKey,
            string callback = "oob");

        /// <summary>
        /// Use 3 legged OAuth 1.0a authentication. Use this method if you have already obtained the OAuthToken
        /// and OAuthSecretToken. This method can be used so you do not have to go through the whole 3 legged
        /// process every time. You can save the tokens you receive the first time and reuse them in another session.
        /// </summary>
        /// <param name="consumerKey">Your consumer API key obtained from the BitBucket web page.</param>
        /// <param name="consumerSecretKey">Your consumer secret API key also obtained from the BitBucket web page.</param>
        /// <param name="oauthToken">Your OAuth token that was obtained on a previous session.</param>
        /// <param name="oauthTokenSecret">Your OAuth secret token that was obtained on a previous session.</param>
        /// <returns></returns>
        void OAuth1ThreeLeggedAuthentication(
            string consumerKey,
            string consumerSecretKey,
            string oauthToken,
            string oauthTokenSecret);

        /// <summary>
        /// Use Oauth2 authentication. This is the newest version and is preferred.
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecretKey"></param>
        /// <returns></returns>
        void OAuth2ClientCredentials(string consumerKey, string consumerSecretKey);
    }
}