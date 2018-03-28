using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints
{
    /// <summary>
    /// Use the privileges endpoint to manage the user privileges (permissions). 
    /// It allows you to grant specific users access to read, write and or administer your repositories. 
    /// Only the repository owner, a team account administrator, or an account with administrative rights 
    /// on the repository can can query or modify repository privileges. 
    /// To manage group access to your repositories, use the group-privileges Endpoint 
    /// and to manage privilege settings for team accounts, use the privileges Resource.  
    /// More info here:
    /// https://confluence.atlassian.com/display/BITBUCKET/privileges+Endpoint
    /// </summary>
    public class PrivilegesEndPoint
    {
        private readonly string _baseUrl;
        private readonly SharpBucketV1 _sharpBucketV1;

        public PrivilegesEndPoint(string accountName, SharpBucketV1 sharpBucketV1)
        {
            _sharpBucketV1 = sharpBucketV1;
            _baseUrl = "privileges/" + accountName + "/";
        }

        /// <summary>
        /// Gets a list of the privileges granted on a repository.  
        /// Only the repository owner, a team account administrator, 
        /// or an account with administrative rights on the repository can make this call.  
        /// If a repository has no individual users with privileges, the method returns an [] empty array.  
        /// To get privileges for groups, use the group-privileges Endpoint.
        /// </summary>
        /// <param name="repository">Repository identifier.</param>
        /// <returns></returns>
        public List<RepositoryPrivileges> ListRepositoryPrivileges(string repository)
        {
            var overrideUrl = _baseUrl + repository + "/";
            return _sharpBucketV1.Get(new List<RepositoryPrivileges>(), overrideUrl);
        }

        /// <summary>
        /// Get a list of privileges for an individual account. 
        /// Only the repository owner, a team account administrator, 
        /// or an account with administrative rights on the repository can make this call. 
        /// </summary>
        /// <param name="repository">The repository whose privileges you wish to get.</param>
        /// <param name="accountName">The account name whose privileges you wish to get.</param>
        /// <returns></returns>
        public RepositoryPrivilegesUser GetPrivilegesForAccount(string repository, string accountName)
        {
            var overrideUrl = _baseUrl + repository + "/" + accountName;
            return _sharpBucketV1.Get(new RepositoryPrivilegesUser(), overrideUrl);
        }
    }
}