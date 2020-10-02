using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SharpBucket
{
    /// <summary>
    /// Interface that exposes the methods that an end point could use to perform calls to the Bitbucket API.
    /// </summary>
    public interface ISharpBucketRequester
    {
        string Send(HttpMethod method, object body, string relativeUrl, object requestParameters);

        Task<string> SendAsync(HttpMethod method, object body, string relativeUrl, object requestParameters, CancellationToken token);

        T Send<T>(HttpMethod method, object body, string relativeUrl, object requestParameters)
            where T : new();

        Task<T> SendAsync<T>(HttpMethod method, object body, string relativeUrl, object requestParameters, CancellationToken token)
            where T : new();

        Uri GetRedirectLocation(string relativeUrl, object requestParameters);

        Task<Uri> GetRedirectLocationAsync(string relativeUrl, object requestParameters, CancellationToken token);
    }
}