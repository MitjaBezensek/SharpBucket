<<<<<<< HEAD
﻿namespace SharpBucket.Authentication{
    internal interface IAuthenticate{
        string GetResponse(string url, string method, string body);
=======
﻿using NServiceKit.ServiceHost;

namespace SharpBucket.Authentication{
    internal interface IAuthenticate{
        string GetResponse<T>(string url, string method, IReturn<T> body);
>>>>>>> Oauth3Legged
    }
}
