using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/environments/%7Benvironment_uuid%7D
    /// </summary>
    public class EnvironmentResource : EndPoint
    {
        internal EnvironmentResource(EnvironmentsResource resource, string uuid)
            : base(resource, uuid.CheckIsNotNullNorEmpty(nameof(uuid)).GuidOrValue())
        {
        }

        public DeploymentEnvironment GetEnvironment()
        {
            return SharpBucketV2.Get<DeploymentEnvironment>(BaseUrl);
        }

        public void DeleteEnvironment()
        {
            SharpBucketV2.Delete(BaseUrl);
        }
    }
}