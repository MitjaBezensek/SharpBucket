using SharpBucket.V2.EndPoints;

namespace SharpBucket.V2
{
    public interface ISharpBucketV2 :ISharpBucket
    {
        RepositoriesEndPoint RepositoriesEndPoint();
        TeamsEndPoint TeamsEndPoint(string teamName);
        UserEndpoint UserEndPoint();
        UsersEndpoint UsersEndPoint(string accountName);
    }
}