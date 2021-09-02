namespace SharpBucket.V2.EndPoints
{
    public class DeploymentsConfigResource : EndPoint
    {
        internal DeploymentsConfigResource(RepositoryResource repositoryResource)
            : base(repositoryResource, "deployments_config")
        {
        }

        private DeploymentsConfigEnvironmentsResource _environmentsResource;

        public DeploymentsConfigEnvironmentsResource EnvironmentsResource
            => this._environmentsResource ??= new DeploymentsConfigEnvironmentsResource(this);
    }
}