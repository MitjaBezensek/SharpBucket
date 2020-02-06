using System.Collections.Generic;
using System.Linq;

namespace SharpBucket.V2.EndPoints
{
    public class EnumeratePullRequestsParameters : EnumerateParameters
    {
        /// <summary>
        /// The list of the states of the pull requests to enumerate.
        /// If null or empty, it default on listing only open pull requests.
        /// </summary>
        public IReadOnlyCollection<PullRequestState> States { get; set; }

        internal override IDictionary<string, object> ToDictionary()
        {
            var dictionary = base.ToDictionary();
            dictionary = ListPullRequestsParameters.AddState(dictionary, this.States);
            return dictionary;
        }
    }
}
