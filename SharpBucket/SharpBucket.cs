using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;
using SharpBucket.Authentication;

namespace SharpBucket{
    /// <summary>
    /// A client for the BitBucket API. It supports V1 and V2 of the API.
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/Use+the+Bitbucket+REST+APIs
    /// </summary>
    public class SharpBucket{
        private Authenticate authenticator;

        /// <summary>
        /// The base URL exposing the BitBucket API.
        /// </summary>
        protected string _baseUrl;

        /// <summary>   
        /// Use basic authentication with the BitBucket API. OAuth authentication is preferred over
        /// basic authentication, due to security reasons.
        /// </summary>
        /// <param name="username">Your BitBucket user name.</param>
        /// <param name="password">Your BitBucket password.</param>
        public void BasicAuthentication(string username, string password){
            authenticator = new BasicAuthentication(username, password, _baseUrl);
        }

        /// <summary>
        /// Use 2 legged OAuth 1.0a authentication. This is similar to basic authentication, since
        /// it requires the same number of steps. It is still safer to use than basic authentication, 
        /// since you can revoke the API keys.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/OAuth+on+Bitbucket
        /// </summary>
        /// <param name="consumerKey">Your consumer API key obtained from the BitBucket web page.</param>
        /// <param name="consumerSecretKey">Your consumer secret API key also obtained from the BitBucket web page.</param>
        public void OAuth2LeggedAuthentication(string consumerKey, string consumerSecretKey){
            authenticator = new OAuthentication2Legged(consumerKey, consumerSecretKey, _baseUrl);
        }

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
        public OAuthentication3Legged OAuth3LeggedAuthentication(string consumerKey, string consumerSecretKey,
            string callback = "oob"){
            authenticator = new OAuthentication3Legged(consumerKey, consumerSecretKey, callback, _baseUrl);
            return (OAuthentication3Legged) authenticator;
        }

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
        public OAuthentication3Legged OAuth3LeggedAuthentication(string consumerKey, string consumerSecretKey,
            string oauthToken, string oauthTokenSecret){
            authenticator = new OAuthentication3Legged(consumerKey, consumerSecretKey, oauthToken, oauthTokenSecret,
                _baseUrl);
            return (OAuthentication3Legged) authenticator;
        }

        /// <summary>
        /// Use Oauth2 authentication. This is the neweset version and is prefered.
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecretKey"></param>
        /// <returns></returns>
        public OAuthentication2 OAuthentication2(string consumerKey, string consumerSecretKey){
            authenticator = new OAuthentication2(consumerKey, consumerSecretKey, _baseUrl);
            ((OAuthentication2) authenticator).GetToken();
            return (OAuthentication2) authenticator;
        }

        private T Send<T>(T body, Method method, string overrideUrl = null, Dictionary<string, object> requestParameters = null) {
            var relativeUrl = overrideUrl;
            T response;
            try{
                response = authenticator.GetResponse(relativeUrl, method, body, requestParameters);
            }
            catch (WebException ex){
                Console.WriteLine(ex.Message);
                response = default(T);
            }
            return response;
        }

        internal T Get<T>(T body, string overrideUrl, Dictionary<string, object> requestParameters = null){
            return Send(body, Method.GET, overrideUrl, requestParameters);
        }

        internal T Post<T>(T body, string overrideUrl){
            return Send(body, Method.POST, overrideUrl);
        }

        internal T Put<T>(T body, string overrideUrl){
            return Send(body, Method.PUT, overrideUrl);
        }

        internal T Delete<T>(T body, string overrideUrl){
            return Send(body, Method.DELETE, overrideUrl);
        }
    }
}