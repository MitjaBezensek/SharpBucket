using System.Net;

namespace SharpBucket {
    interface IAuthenticate{
        void AuthenticateRequest(HttpWebRequest req);
    }
}
