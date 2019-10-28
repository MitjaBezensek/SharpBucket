using System.Threading;
using System.Threading.Tasks;

namespace SharpBucket
{
    /// <summary>
    /// Interface that expose the methods that an end point should use to perform calls to the BitBucket API.
    /// </summary>
    public interface ISharpBucketRequester
    {
        string Get(string relativeUrl, object requestParameters = null);

        Task<string> GetAsync(string overrideUrl, object requestParameters = null, CancellationToken token = default(CancellationToken));

        T Get<T>(string relativeUrl, object requestParameters = null)
            where T : new();

        Task<T> GetAsync<T>(string overrideUrl, object requestParameters = null, CancellationToken token = default(CancellationToken))
            where T : new();

        T Post<T>(T body, string relativeUrl)
            where T : new();

        Task<T> PostAsync<T>(T body, string overrideUrl, CancellationToken token = default(CancellationToken))
            where T : new();

        T Put<T>(T body, string relativeUrl)
            where T : new();

        Task<T> PutAsync<T>(T body, string overrideUrl, CancellationToken token = default(CancellationToken))
            where T : new();

        void Delete(string relativeUrl);

        Task DeleteAsync(string overrideUrl, CancellationToken token = default(CancellationToken));
    }
}