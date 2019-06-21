using System;

namespace SharpBucket.Authentication
{
    /// <summary>
    /// Class that represent an OAuth2 token
    /// </summary>
    public class OAuth2Token
    {
        /// <summary>
        /// The access token to send with each request to be authenticated.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// a list of scopes separated by spaces.
        /// </summary>
        public string Scopes { get; set; }

        private int _expiresIn;

        /// <summary>
        /// The number of seconds in which the token will expire after it's acquisition.
        /// </summary>
        public int ExpiresIn
        {
            get => _expiresIn;
            set
            {
                _expiresIn = value;
                this.ExpiresAt = DateTime.UtcNow.AddSeconds(value);
            }
        }

        /// <summary>
        /// The UTC date at which the token will expire.
        /// Automatically compute after affectation of the <see cref="ExpiresIn"/> property.
        /// </summary>
        public DateTime ExpiresAt { get; private set; }

        /// <summary>
        /// The refresh token to use to renew this token.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// The type of this token. Expected to be "bearer".
        /// </summary>
        public string TokenType { get; set; }
    }
}