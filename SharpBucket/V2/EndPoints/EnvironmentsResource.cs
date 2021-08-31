using SharpBucket.V2.Pocos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpBucket.V2.EndPoints
{
    public class EnvironmentsResource : EndPoint
    {
        internal EnvironmentsResource(RepositoryResource repositoryResource)
            : base(repositoryResource, "environments") 
        {
        }

        public List<DeploymentEnvironment> ListEnvironments()
        {
            //NOTE:
            //1. '/' sign need to be at the end. Why? Because Atlassian
            //2. We need to use Get, because GetPaginatedValues don't work broken BB API (with more than 11 items). Don't ask why.
            var list = SharpBucketV2.Get<IteratorBasedPage<DeploymentEnvironment>>(BaseUrl + "/");
            return list.values;
        }

        public async Task<List<DeploymentEnvironment>> ListEnvironmentsAsync(CancellationToken token = default)
        {
            //NOTE:
            //1. '/' sign need to be at the end. Why? Because Atlassian
            //2. We need to use Get, because GetPaginatedValues don't work broken BB API (with more than 11 items). Don't ask why.
            var list = await SharpBucketV2.GetAsync<IteratorBasedPage<DeploymentEnvironment>>(BaseUrl + "/", token);
            return list.values;
        }

        /// <summary>
        /// Create an environment.
        /// Required field are name and environment_type.name.
        /// Other fields will probably be ignored.
        /// </summary>
        /// <param name="environment">The environment object to create.</param>
        public DeploymentEnvironment PostEnvironment(DeploymentEnvironment environment)
        {
            //NOTE: '/' sign need to be at the end. Why? Because Atlassian
            return SharpBucketV2.Post(environment, BaseUrl + "/");
        }

        /// <summary>
        /// Create an environment.
        /// Required field are name and environment_type.name.
        /// Other fields will probably be ignored.
        /// </summary>
        /// <param name="environment">The environment object to create.</param>
        public Task<DeploymentEnvironment> PostEnvironmentAsync(DeploymentEnvironment environment, CancellationToken token = default)
        {
            //NOTE: '/' sign need to be at the end. Why? Because Atlassian
            return SharpBucketV2.PostAsync(environment, BaseUrl + "/", token);
        }

        public EnvironmentResource EnvironmentResource(string envUuid)
        {
            return new EnvironmentResource(this, envUuid);
        }
    }
}