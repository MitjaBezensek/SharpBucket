namespace SharpBucket.Authentication{
    internal interface IAuthenticate{
        string GetResponse(string url, string method, string body);
    }
}
