using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints
{
    /// <summary>
    /// Use the user endpoints to gets information related to the currently authenticated user. 
    /// It is useful for OAuth or other in situations where the username is unknown. 
    /// This endpoint returns information about an individual or team account. 
    /// Individual and team accounts both have the same set of user fields:
    /// More info here:
    /// https://confluence.atlassian.com/display/BITBUCKET/user+Endpoint
    /// </summary>
    public class UserEndPoint
    {
        private readonly SharpBucketV1 _sharpBucketV1;
        private readonly string _baseUrl;

        public UserEndPoint(SharpBucketV1 sharpBucketV1)
        {
            _sharpBucketV1 = sharpBucketV1;
            _baseUrl = "user/";
        }

        /// <summary>
        /// Gets the basic information associated with an account and a list of all of the repositories owned by the user.
        /// </summary>
        /// <returns></returns>
        public UserInfo GetInfo()
        {
            return _sharpBucketV1.Get(new UserInfo(), _baseUrl);
        }

        /// <summary>
        /// List the account-level privileges for an individual or team account. 
        /// Use this call to locate the accounts that the currently authenticated accountname has access to. 
        /// An account can have admin or collaborator (member) privileges. The accountname always has admin privileges on itself. 
        /// </summary>
        /// <returns></returns>
        public Privileges ListPrivileges()
        {
            var overrideUrl = _baseUrl + "privileges";
            return _sharpBucketV1.Get(new Privileges(), overrideUrl);
        }

        /// <summary>
        /// List the details of the repositories that the individual or team account follows. 
        /// This call returns the full data about the repositories including if the repository is a fork of another repository. 
        /// An account always "follows" its own repositories. 
        /// </summary>
        /// <returns></returns>
        public List<Repository> ListFollows()
        {
            var overrideUrl = _baseUrl + "follows";
            return _sharpBucketV1.Get(new List<Repository>(), overrideUrl);
        }

        /// <summary>
        /// List the details of the repositories that the user owns or has at least read access to. 
        /// Use this if you're looking for a full list of all of the repositories associated with a user.
        /// </summary>
        /// <returns></returns>
        public List<Repository> ListRepositories()
        {
            var overrideUrl = _baseUrl + "repositories";
            return _sharpBucketV1.Get(new List<Repository>(), overrideUrl);
        }

        /// <summary>
        /// List the repositories the account follows.  This is the same list that appears on the Following tab on your account dashboard.
        /// </summary>
        /// <returns></returns>
        public RepositoriesOverview RepositoriesOverview()
        {
            var overrideUrl = _baseUrl + "repositories/overview";
            return _sharpBucketV1.Get(new RepositoriesOverview(), overrideUrl);
        }

        // TODO: Fix serialization
        /// <summary>
        /// List the repositories from the account's dashboard.
        /// </summary>
        /// <returns></returns>
        public object GetRepositoryDasboard()
        {
            var overrideUrl = _baseUrl + "repositories/dashboard";
            return _sharpBucketV1.Get(new object(), overrideUrl);
        }
    }
}