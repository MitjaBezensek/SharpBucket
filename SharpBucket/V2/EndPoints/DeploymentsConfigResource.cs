namespace SharpBucket.V2.EndPoints
{
    public partial class DeploymentsConfigResource : EndPoint
    {
        internal DeploymentsConfigResource(RepositoryResource repositoryResource)
            : base(repositoryResource, "deployments_config")
        {
        }

        private DeploymentsConfigEnvironmentsResource _environmentsResource;

        public DeploymentsConfigEnvironmentsResource EnvironmentsResource => this._environmentsResource ??
                                                (_environmentsResource = new DeploymentsConfigEnvironmentsResource(this));
    }
}