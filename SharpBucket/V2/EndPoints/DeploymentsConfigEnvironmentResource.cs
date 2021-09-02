using SharpBucket.Utility;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/deployments_config/environments
    /// </summary>
    public class DeploymentsConfigEnvironmentResource : EndPoint
    {
        internal DeploymentsConfigEnvironmentResource(DeploymentsConfigEnvironmentsResource resource, string environmentUuid)
            : base(resource, environmentUuid.CheckIsNotNullNorEmpty(nameof(environmentUuid)).GuidOrValue())
        {
        }


        private EnvironmentVariablesResource _environmentVariablesResource;

        public EnvironmentVariablesResource VariablesResource
            => this._environmentVariablesResource ??= new EnvironmentVariablesResource(this);
    }
}