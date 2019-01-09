using LibGit2Sharp;
using SharpBucket.Authentication;

namespace SharpBucketTests.GitHelpers
{
    /// <summary>
    /// Class that allow to perform git authentication on Bitbucket with OAuth2
    /// Specification is defined at the bottom of that page: https://confluence.atlassian.com/bitbucket/oauth-on-bitbucket-cloud-238027431.html
    /// For more details see: https://community.atlassian.com/t5/Bitbucket-questions/hot-to-git-clone-with-oauth-how-to-create-token-for-this-kind-of/qaq-p/686446
    /// </summary>
    internal class OAuth2GitCredentialsProvider : IGitCredentialsProvider
    {
        private OAuth2TokenProvider TokenProvider { get; }

        private UsernamePasswordCredentials OAuth2TokenCredentials { get; set; }

        public OAuth2GitCredentialsProvider(string consumerKey, string consumerSecret)
        {
            this.TokenProvider = new OAuth2TokenProvider(consumerKey, consumerSecret);
        }

        public Credentials GetCredentials(string url, string usernameFromUrl, SupportedCredentialTypes types)
        {
            if (OAuth2TokenCredentials == null)
            {
                // TODO the token should be kept somewhere to implement refresh token scenario one day
                var token = TokenProvider.GetToken();
                OAuth2TokenCredentials = new UsernamePasswordCredentials
                {
                    Username = "x-token-auth",
                    Password = token.AccessToken
                };
            }

            return OAuth2TokenCredentials;
        }
    }
}
