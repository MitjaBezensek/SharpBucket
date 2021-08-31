using System.Collections.Generic;
using System.Linq;

namespace SharpBucket.V2.Pocos
{
    /// <summary>
    /// A non documented error response format observe at least while trying to post and invalid environment
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/environments/#post
    /// </summary>
    internal class ErrorResponse2
    {
        public string key { get; set; }
        public string message { get; set; }
        public Dictionary<string, string> arguments { get; set; }

        public ErrorResponse ToErrorResponse()
        {
            return new ErrorResponse
            {
                type = "ErrorResponse2",
                error = new Error
                {
                    id = key,
                    message = message,
                    fields = arguments?.ToDictionary(k => k.Key, v => new[] {v.Value}),
                },
            };
        }
    }
}