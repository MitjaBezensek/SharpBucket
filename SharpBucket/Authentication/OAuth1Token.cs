using System;

namespace SharpBucket.Authentication
{
    /// <summary>
    /// Class that represent an OAuth1 token with its secret
    /// </summary>
    public class OAuth1Token
    {
        public string Token { get; }
        public string Secret { get; }

        public OAuth1Token(string oauthToken, string oauthTokenSecret)
        {
            if (string.IsNullOrWhiteSpace(oauthToken)) throw new ArgumentNullException(nameof(oauthToken));
            if (string.IsNullOrWhiteSpace(oauthTokenSecret)) throw new ArgumentNullException(nameof(oauthTokenSecret));

            this.Token = oauthToken;
            this.Secret = oauthTokenSecret;
        }
    }
}
