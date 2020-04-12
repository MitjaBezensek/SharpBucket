using System;
using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    public class IssuesResource : EndPoint
    {
        public IssuesResource(RepositoryResource repositoryResource)
            : base(repositoryResource, "issues")
        {
        }

        /// <summary>
        /// List open issues on the repository.
        /// </summary>
        /// <returns></returns>
        public List<Issue> ListIssues()
            => ListIssues(new ListParameters());

        /// <summary>
        /// List issues on the repository.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public List<Issue> ListIssues(ListParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return GetPaginatedValues<Issue>(_baseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate open issues on the repository.
        /// </summary>
        public IEnumerable<Issue> EnumerateIssues()
            => EnumerateIssues(new EnumerateParameters());

        /// <summary>
        /// Enumerate issues on the repository.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        public IEnumerable<Issue> EnumerateIssues(EnumerateParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _sharpBucketV2.EnumeratePaginatedValues<Issue>(_baseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

        #region Issue Resource

        /// <summary>
        /// Get the Issue Resource.
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <returns></returns>
        public IssueResource IssueResource(int issueId)
        {
            return new IssueResource(this, issueId);
        }

        #endregion
    }
}
