using SharpBucket.V2.Pocos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/deployments_config/environments/%7Benvironment_uuid%7D/variables
    /// </summary>
    public class EnvironmentVariablesResource : EndPoint
    {
        internal EnvironmentVariablesResource(DeploymentsConfigEnvironmentResource resource)
            : base(resource, "variables")
        {
        }

        /// <summary>
        /// List variables.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<EnvironmentVariable> ListVariables(int max = 0)
        {
            return SharpBucketV2.GetPaginatedValues<EnvironmentVariable>(BaseUrl, max);
        }

        /// <summary>
        /// Enumerate variables, doing requests page by page.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        public IEnumerable<EnvironmentVariable> EnumerateVariables(int? pageLen = null)
        {
            return SharpBucketV2.EnumeratePaginatedValues<EnvironmentVariable>(BaseUrl, pageLen: pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate variables asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<EnvironmentVariable> EnumerateVariablesAsync(CancellationToken token = default)
            => EnumerateVariablesAsync(null, token);

        /// <summary>
        /// Enumerate variables asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<EnvironmentVariable> EnumerateVariablesAsync(int? pageLen, CancellationToken token = default)
        {
            return SharpBucketV2.EnumeratePaginatedValuesAsync<EnvironmentVariable>(BaseUrl, null, pageLen, token);
        }
#endif

        public EnvironmentVariable PostVariable(EnvironmentVariable variable)
        {
            return SharpBucketV2.Post(variable, BaseUrl);
        }

        public Task<EnvironmentVariable> PostVariableAsync(EnvironmentVariable variable, CancellationToken token = default)
        {
            return SharpBucketV2.PostAsync(variable, BaseUrl, token);
        }

        public EnvironmentVariableResource VariableResource(string variableUuid)
        {
            return new EnvironmentVariableResource(this, variableUuid);
        }
    }
}