using SharpBucket.V2.Pocos;
using System.Collections.Generic;

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
            //1. '/' sign need to be at the end. Why? Because Atlasian
            //2. We need to use Get, because GetPaginatedValues don't work broken BB API (with more than 11 items). Don't ask why.
            var list = SharpBucketV2.Get<IteratorBasedPage< DeploymentEnvironment>>(BaseUrl + "/");
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
            return SharpBucketV2.Post(environment, BaseUrl + "/");    //NOTE: '/' sign need to be at the end. Why? Because Atlasian
        }

        public EnvironmentResource EnvironmentResource(string envUuid)
        {
            return new EnvironmentResource(this, envUuid);
        }
    }
}