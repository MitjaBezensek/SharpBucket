using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    public class IssuesResource
    {
        private readonly string _slug;
        private readonly string _accountName;
        private readonly RepositoriesEndPoint _repositoriesEndPoint;

        public IssuesResource(string accountName, string repoSlugOrName, RepositoriesEndPoint repositoriesEndPoint)
        {
            _accountName = accountName.GuidOrValue();
            _slug = repoSlugOrName.ToSlug();
            _repositoriesEndPoint = repositoriesEndPoint;
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
            return _repositoriesEndPoint.ListIssues(_accountName, _slug, parameters);
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
            return _repositoriesEndPoint.EnumerateIssues(_accountName, _slug, parameters);
        }
    }
}
