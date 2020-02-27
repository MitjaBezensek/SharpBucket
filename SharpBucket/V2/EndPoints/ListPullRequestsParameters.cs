using System.Collections.Generic;
using System.Linq;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// An object that can be passed on methods that list pull requests.
    /// </summary>
    public class ListPullRequestsParameters : ListParameters
    {
        /// <summary>
        /// The list of the states of the pull requests to enumerate.
        /// If null or empty, it default on listing only open pull requests.
        /// </summary>
        public IReadOnlyCollection<PullRequestState> States { get; set; }

        internal override IDictionary<string, object> ToDictionary()
        {
            var dictionary = base.ToDictionary();
            dictionary = AddState(dictionary, this.States);
            return dictionary;
        }

        internal static IDictionary<string, object> AddState(IDictionary<string, object> dictionary, IReadOnlyCollection<PullRequestState> states)
        {
            if (states != null
                && states.Count > 0
                && (states.Count > 1 || states.First() != PullRequestState.Open))
            {
                if (dictionary == null)
                {
                    dictionary = new Dictionary<string, object>();
                }
                dictionary.Add("state", states.Select(s => s.ToString().ToUpperInvariant()).ToList());
            }

            return dictionary;
        }
    }
}
