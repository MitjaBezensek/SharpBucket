namespace SharpBucket.V2.Pocos
{
    /// <summary>
    /// The standardize error response like defined here:
    /// https://developer.atlassian.com/bitbucket/api/2/reference/meta/uri-uuid
    /// </summary>
    public class ErrorResponse
    {
        public string type { get; set; }
        public Error error { get; set; }
    }
}