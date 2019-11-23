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
        string Send(object body, HttpMethod method, string relativeUrl, object requestParameters = null);

        Task<string> SendAsync(object body, HttpMethod method, string relativeUrl, object requestParameters = null, CancellationToken token = default(CancellationToken));

        T Send<T>(object body, HttpMethod method, string relativeUrl, object requestParameters = null)
            where T : new();

        Task<T> SendAsync<T>(object body, HttpMethod method, string relativeUrl, object requestParameters = null, CancellationToken token = default(CancellationToken))
            where T : new();

        Uri GetRedirectLocation(string relativeUrl, object requestParameters = null);

        Task<Uri> GetRedirectLocationAsync(string relativeUrl, object requestParameters = null, CancellationToken token = default(CancellationToken));
    }
}