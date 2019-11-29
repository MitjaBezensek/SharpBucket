using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SharpBucket
{
    /// <summary>
    /// Interface that expose the methods that an end point should use to perform calls to the BitBucket API.
    /// </summary>
    public interface ISharpBucketRequester
    {
        string Send(object body, HttpMethod method, string relativeUrl, object requestParameters);

        Task<string> SendAsync(object body, HttpMethod method, string relativeUrl, object requestParameters, CancellationToken token);

        T Send<T>(object body, HttpMethod method, string relativeUrl, object requestParameters)
            where T : new();

        Task<T> SendAsync<T>(object body, HttpMethod method, string relativeUrl, object requestParameters, CancellationToken token)
            where T : new();

        Uri GetRedirectLocation(string relativeUrl, object requestParameters);

        Task<Uri> GetRedirectLocationAsync(string relativeUrl, object requestParameters, CancellationToken token);
    }
}