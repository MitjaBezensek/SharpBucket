using System.Net;

namespace SharpBucket.Authentication {
    interface IAuthenticate{
        void AuthenticateRequest(HttpWebRequest req);
    }
}
