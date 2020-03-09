using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// A "Virtual" resource that offers easier manipulation of the issue.
    /// </summary>
    public class IssueResource
    {
        private readonly int _issueId;
        private readonly string _slug;
        private readonly string _accountName;
        private readonly RepositoriesEndPoint _repositoriesEndPoint;

        public IssueResource(string accountName, string repoSlugOrName, int pullRequestId, RepositoriesEndPoint repositoriesEndPoint)
        {
            _issueId = pullRequestId;
            _slug = repoSlugOrName.ToSlug();
            _accountName = accountName.GuidOrValue();
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// Gets the <see cref="Issue"/>
        /// </summary>
        public Issue GetIssue()
        {
            return _repositoriesEndPoint.GetIssue(_accountName, _slug, _issueId);
        }

        /// <summary>
        /// Gets the <see cref="Issue"/>
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<Issue> GetIssueAsync(CancellationToken token = default)
        {
            return await _repositoriesEndPoint.GetIssueAsync(_accountName, _slug, _issueId, token);
        }
    }
}
