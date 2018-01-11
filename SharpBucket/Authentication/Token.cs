namespace SharpBucket.Authentication
{
    internal class Token
    {
        public string AccessToken { get; set; }
        public string Scopes { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
    }
}