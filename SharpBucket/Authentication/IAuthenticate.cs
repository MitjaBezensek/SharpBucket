using RestSharp;

namespace SharpBucket.Authentication{
    internal interface IAuthenticate{
        T GetResponse<T>(string url, Method method, T body);
    }
}