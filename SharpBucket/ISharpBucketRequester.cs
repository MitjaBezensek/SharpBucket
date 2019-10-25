namespace SharpBucket
{
    /// <summary>
    /// Interface that expose the methods that an end point should use to perform calls to the BitBucket API.
    /// </summary>
    public interface ISharpBucketRequester
    {
        string Get(string relativeUrl, object requestParameters = null);

        T Get<T>(string relativeUrl, object requestParameters = null)
            where T : new();

        T Post<T>(T body, string relativeUrl)
            where T : new();

        T Put<T>(T body, string relativeUrl)
            where T : new();

        void Delete(string relativeUrl);
    }
}