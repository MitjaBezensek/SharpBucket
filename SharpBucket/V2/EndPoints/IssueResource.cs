using System.Threading;
using System.Threading.Tasks;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// A "Virtual" resource that offers easier manipulation of the issue.
    /// </summary>
    public class IssueResource : EndPoint
    {
        internal IssueResource(IssuesResource issueResource, int issueId)
            : base(issueResource, issueId.ToString())
        {
        }

        /// <summary>
        /// Gets the <see cref="Issue"/>
        /// </summary>
        public Issue GetIssue()
        {
            return SharpBucketV2.Get<Issue>(BaseUrl);
        }

        /// <summary>
        /// Gets the <see cref="Issue"/>
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<Issue> GetIssueAsync(CancellationToken token = default)
        {
            return SharpBucketV2.GetAsync<Issue>(BaseUrl, token);
        }
    }
}
