using SharpBucket.Authentication;
using SharpBucket.V2.EndPoints;

namespace SharpBucket.V2
{
    public interface ISharpBucketV2
    {
        /// <summary>
        /// Get the Teams End Point for a specific team.
        /// </summary>
        /// <param name="teamName">The team whose team End Point you wish to get.</param>
        /// <returns></returns>
        TeamsEndPoint TeamsEndPoint(string teamName);

        /// <summary>
        /// Get the Repositories End point.
        /// </summary>
        /// <returns></returns>
        RepositoriesEndPoint RepositoriesEndPoint();

        /// <summary>
        /// Get the UsersEndPoint End Point.
        /// </summary>
        /// <param name="accountName">The account for which you wish to get the UsersEndPoint End Point.</param>
        /// <returns></returns>
        UsersEndpoint UsersEndPoint(string accountName);

        /// <summary>
        /// Get the UserEndpoint End Point
        /// </summary>
        /// <returns></returns>
        UserEndpoint UserEndPoint();

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
        /// Use 3 legged OAuth 1.0a authentication. This is the most secure one, but for simple uses it might
        /// be a bit too complex.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/OAuth+on+Bitbucket
        /// </summary>
        /// <param name="consumerKey">Your consumer API key obtained from the BitBucket web page.</param>
        /// <param name="consumerSecretKey">Your consumer secret API key also obtained from the BitBucket web page.</param>
        /// <param name="callback">Callback URL to which BitBucket will send the pin.</param>
        /// <returns></returns>
        OAuthentication3Legged OAuth3LeggedAuthentication(
           string consumerKey,
           string consumerSecretKey,
           string callback = "oob");

        /// <summary>
        /// Use 3 legged OAuth 1.0a authentication. Use this method if you have already obtained the OAuthToken
        /// and OAuthSecretToken. This method can be used so you do not have to go trough the whole 3 legged
        /// process every time. You can save the tokens you receive the first time and reuse them in another session.
        /// </summary>
        /// <param name="consumerKey">Your consumer API key obtained from the BitBucket web page.</param>
        /// <param name="consumerSecretKey">Your consumer secret API key also obtained from the BitBucket web page.</param>
        /// <param name="oauthToken">Your OAuth token that was obtained on a previous session.</param>
        /// <param name="oauthTokenSecret">Your OAuth secret token thata was obtained on a previous session.</param>
        /// <returns></returns>
        OAuthentication3Legged OAuth3LeggedAuthentication(
           string consumerKey,
           string consumerSecretKey,
           string oauthToken,
           string oauthTokenSecret);

        /// <summary>
        /// Use Oauth2 authentication. This is the neweset version and is prefered.
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecretKey"></param>
        /// <returns></returns>
        OAuthentication2 OAuthentication2(string consumerKey, string consumerSecretKey);
    }
}