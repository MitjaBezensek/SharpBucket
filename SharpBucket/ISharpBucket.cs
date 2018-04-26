using SharpBucket.Authentication;

namespace SharpBucket
{
    public interface ISharpBucket
    {
        void BasicAuthentication(string username, string password);
        void OAuth2LeggedAuthentication(string consumerKey, string consumerSecretKey);
        OAuthentication3Legged OAuth3LeggedAuthentication(string consumerKey, string consumerSecretKey, string callback = "oob");
        OAuthentication3Legged OAuth3LeggedAuthentication(string consumerKey, string consumerSecretKey, string oauthToken, string oauthTokenSecret);
        OAuthentication2 OAuthentication2(string consumerKey, string consumerSecretKey);
    }
}