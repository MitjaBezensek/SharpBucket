﻿using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/deployments_config/environments/%7Benvironment_uuid%7D/variables/%7Bvariable_uuid%7D
    /// </summary>
    public class EnvironmentVariableResource : EndPoint
    {
        internal EnvironmentVariableResource(EnvironmentVariablesResource resource, string uuid)
            : base(resource, uuid.CheckIsNotNullNorEmpty(nameof(uuid)).GuidOrValue())
        {
        }

        public void DeleteVariable()
        {
            SharpBucketV2.Delete(BaseUrl);
        }

        public Task DeleteVariableAsync(CancellationToken token = default)
        {
            return SharpBucketV2.DeleteAsync(BaseUrl, token);
        }

        public EnvironmentVariable PutVariable(EnvironmentVariable variable)
        {
            return SharpBucketV2.Put(variable, BaseUrl);
        }

        public Task<EnvironmentVariable> PutVariableAsync(EnvironmentVariable variable, CancellationToken token = default)
        {
            return SharpBucketV2.PutAsync(variable, BaseUrl, token);
        }
    }
}