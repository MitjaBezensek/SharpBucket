using System;
using System.Net;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2
{
    /// <summary>
    /// Exception raised when the response of a call to the bitbucket API V2 describe an error.
    /// </summary>
    public class BitbucketV2Exception : BitbucketException
    {
        public ErrorResponse ErrorResponse { get; }

        public BitbucketV2Exception(HttpStatusCode httpStatusCode, ErrorResponse errorResponse)
            : this(httpStatusCode, errorResponse, null)
        {
        }

        public BitbucketV2Exception(HttpStatusCode httpStatusCode, ErrorResponse errorResponse, Exception innerException)
            : base(httpStatusCode, errorResponse.error.message, innerException)
        {
            ErrorResponse = errorResponse;
        }
    }
}
