using SharpBucket.V2.Pocos;
using System.Collections.Generic;

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

        public List<EnvironmentVariable> ListVariables()
        {
            return GetPaginatedValues<EnvironmentVariable>(BaseUrl);
        }

        public EnvironmentVariable PostVariable(EnvironmentVariable variable)
        {
            return SharpBucketV2.Post(variable, BaseUrl);
        }

        public EnvironmentVariableResource VariableResource(string variableUuid)
        {
            return new EnvironmentVariableResource(this, variableUuid);
        }
    }
}