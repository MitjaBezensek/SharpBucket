using RestSharp;

namespace SharpBucket.Authentication{
    internal interface IAuthenticate{
        string GetResponse<T>(string url, Method method, T body);
    }
}

