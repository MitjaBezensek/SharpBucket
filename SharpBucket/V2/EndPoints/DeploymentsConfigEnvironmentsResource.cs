namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/deployments_config
    /// </summary>
    public class DeploymentsConfigEnvironmentsResource : EndPoint
    {
        internal DeploymentsConfigEnvironmentsResource(DeploymentsConfigResource repositoryResource)
            : base(repositoryResource, "environments")
        {
        }

        public DeploymentsConfigEnvironmentResource EnvironmentResource(string environmentUuid)
        {
            return new DeploymentsConfigEnvironmentResource(this, environmentUuid);
        }
    }
}