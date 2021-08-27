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

        public List<Environment> ListEnvironments()
        {
            return GetPaginatedValues<Environment>(BaseUrl + "/");   //NOTE: '/' sign need to be at the end. Why? Because Atlasian
        }

        public Environment PostEnvironment(Environment branch)
        {
            return SharpBucketV2.Post(branch, BaseUrl + "/");    //NOTE: '/' sign need to be at the end. Why? Because Atlasian
        }

        public EnvironmentResource EnvironmentResource(string envUuid)
        {
            return new EnvironmentResource(this, envUuid);
        }
    }
}