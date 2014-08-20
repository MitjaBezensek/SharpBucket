using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints{
    /// <summary>
    /// The Teams End Point gets a team's profile information.
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/teams+Endpoint
    /// </summary>
    public class TeamsEndPointV2{
        private readonly SharpBucketV2 _sharpBucketV2;
        private readonly string _baseUrl;

        public TeamsEndPointV2(SharpBucketV2 sharpBucketV2, string teamName){
            _sharpBucketV2 = sharpBucketV2;
            _baseUrl = "teams/" + teamName + "/";
        }

        /// <summary>
        /// Gets the public information associated with a team. 
        /// If the team's profile is private, the caller must be authenticated and authorized to view this information. 
        /// </summary>
        /// <returns></returns>
        public Team GetProfile(){
            return _sharpBucketV2.Get(new Team(), _baseUrl);
        }

        /// <summary>
        /// Gets the team's members. By default, this call returns the first 50 members of the team. 
        /// </summary>
        /// <returns></returns>
        public User ListMembers(){
            var overrideUrl = _baseUrl + "members/";
            return _sharpBucketV2.Get(new User(), overrideUrl);
        }

        /// <summary>
        /// Gets the list of accounts following the team.
        /// </summary>
        /// <returns></returns>
        public TeamProfile ListFollowers(){
            var overrideUrl = _baseUrl + "followers/";
            return _sharpBucketV2.Get(new TeamProfile(), overrideUrl);
        }

        /// <summary>
        /// Gets a list of accounts the team is following. 
        /// </summary>
        /// <returns></returns>
        public TeamProfile ListFollowing(){
            var overrideUrl = _baseUrl + "following/";
            return _sharpBucketV2.Get(new TeamProfile(), overrideUrl);
        }

        /// <summary>
        /// Gets the list of the team's repositories. 
        /// Private repositories only appear on this list if the caller is authenticated and is authorized to view the repository.
        /// </summary>
        /// <returns></returns>
        public List<Repository> ListRepositories(){
            var overrideUrl = _baseUrl + "repositories/";
            return _sharpBucketV2.Get(new List<Repository>(), overrideUrl);
        }
    }
}