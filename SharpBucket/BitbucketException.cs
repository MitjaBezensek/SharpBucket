using System;
using System.Net;
using System.Runtime.InteropServices;

namespace SharpBucket
{
    /// <summary>
    /// Exception raised when the response of a call to the bitbucket API describe an error.
    /// </summary>
    /// <remarks>
    /// This class is really similar to HttpException,
    /// but using HttpException would need to add a dependency on System.Web
    /// which is not included by default in NetStandard2.0.
    /// </remarks>
    public class BitbucketException : ExternalException
    {
        public HttpStatusCode HttpStatusCode { get; }

        public BitbucketException(HttpStatusCode httpStatusCode, string message)
            : this(httpStatusCode, message, null)
        {
        }

        public BitbucketException(HttpStatusCode httpStatusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
