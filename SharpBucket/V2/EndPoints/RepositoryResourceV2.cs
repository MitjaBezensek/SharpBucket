using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints{
    /// <summary>
    /// Use this resource to get information associated with an individual repository. 
    /// You can use these calls with public or private repositories. 
    /// Private repositories require the caller to authenticate with an account that has the appropriate authorization.
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/repository+Resource
    /// </summary>
    public class RepositoryResourceV2{
        private readonly RepositoriesEndPointV2 _repositoriesEndPointV2;
        private readonly string _accountName;
        private readonly string _repository;

        public RepositoryResourceV2(string accountName, string repository, RepositoriesEndPointV2 repositoriesEndPointV2){
            _repository = repository;
            _accountName = accountName;
            _repositoriesEndPointV2 = repositoriesEndPointV2;
        }

        /// <summary>
        /// Get the Pull Requests Resource for the current repository.
        /// </summary>
        /// <returns></returns>
        public PullRequestsResourceV2 PullRequests(){
            return new PullRequestsResourceV2(_accountName, _repository, _repositoriesEndPointV2);
        }

        /// <summary>
        /// Delete the current repository.
        /// </summary>
        /// <returns></returns>
        public Repository DeleteRepository(){
            return _repositoriesEndPointV2.DeleteRepository(new Repository(), _accountName, _repository);
        }

        /// <summary>
        /// List all the watchers of the repository.
        /// </summary>
        /// <returns></returns>
        public List<Watcher> ListWatchers(){
            return _repositoriesEndPointV2.ListWatchers(_accountName, _repository);
        }

        /// <summary>
        /// List all the forks of the repository.
        /// </summary>
        /// <returns></returns>
        public List<Fork> ListForks(){
            return _repositoriesEndPointV2.ListForks(_accountName, _repository);
        }

        /// <summary>
        /// List all the branch restrictions of the repository.
        /// </summary>
        /// <returns></returns>
        public object ListBranchRestrictions(){
            return _repositoriesEndPointV2.ListBranchRestrictions(_accountName, _repository);
        }

        /// <summary>
        /// Get a branch restriction of the repository.
        /// </summary>
        /// <param name="restrictionId">The Id of the branch restriction.</param>
        /// <returns></returns>
        public object GetBranchRestriction(int restrictionId){
            return _repositoriesEndPointV2.GetBranchRestriction(_accountName, _repository, restrictionId);
        }

        /// <summary>
        /// Add a branch restriction to the repository.
        /// </summary>
        /// <param name="restriction">The branch restriction that you wish to add.</param>
        /// <returns></returns>
        public BranchRestriction PostBranchRestriction(BranchRestriction restriction){
            return _repositoriesEndPointV2.PostBranchRestriction(_accountName, _repository, restriction);
        }

        /// <summary>
        /// Delete a branch restriction of the repository.
        /// </summary>
        /// <param name="restrictionId">The Id of the branch restriction that you wish to delete.</param>
        /// <returns></returns>
        public object DeleteBrachRestriction(int restrictionId){
            return _repositoriesEndPointV2.DeleteBranchRestriction(_accountName, _repository, restrictionId);
        }

        /// <summary>
        /// Get the diff for the repository.
        /// </summary>
        /// <param name="options">The diff options.</param>
        /// <returns></returns>
        public object GetDiff(object options){
            return _repositoriesEndPointV2.GetDiff(new Repository(), _accountName, _repository, options);
        }

        /// <summary>
        /// Get the patch for the repository.
        /// </summary>
        /// <param name="options">The patch options.</param>
        /// <returns></returns>
        public object GetPatch(object options){
            return _repositoriesEndPointV2.GetPatch(new Repository(), _accountName, _repository, options);
        }

        /// <summary>
        /// List all the commits of the repository.
        /// </summary>
        /// <returns></returns>
        public object ListCommits(){
            return _repositoriesEndPointV2.ListCommits(new Repository(), _accountName, _repository);
        }

        /// <summary>
        /// Get a commit of the repository.
        /// </summary>
        /// <param name="commitId">The Id of the commit that you wish to get.</param>
        /// <returns></returns>
        public object GetCommit(string commitId){
            return _repositoriesEndPointV2.GetCommit(_accountName, _repository, commitId);
        }

        /// <summary>
        /// List all the comments for a commit of the repository.
        /// </summary>
        /// <param name="commitId">The Id of the commit whose comments you wish to get.</param>
        /// <returns></returns>
        public object ListCommitComments(string commitId){
            return _repositoriesEndPointV2.ListCommitComments(_accountName, _repository, commitId);
        }

        /// <summary>
        /// Get a comment of a commit of the repository.
        /// </summary>
        /// <param name="commitId">The Id of the commit whose comment you wish to get.</param>
        /// <param name="commentId">The Id of the comment that you wish to get.</param>
        /// <returns></returns>
        public object GetCommitComment(string commitId, int commentId){
            return _repositoriesEndPointV2.GetCommitComment(_accountName, _repository, commitId, commentId);
        }

        /// <summary>
        /// Approve a commit of the repository.
        /// </summary>
        /// <param name="commitId">The Id of the commit that you wish to approve.</param>
        /// <returns></returns>
        public object ApproveCommit(string commitId){
            return _repositoriesEndPointV2.ApproveCommit(_accountName, _repository, commitId);
        }

        /// <summary>
        /// Delete the approval of a commit of the repository.
        /// </summary>
        /// <param name="commitId">The Id of the commits whose approval you wish to delete.</param>
        /// <returns></returns>
        public object DeleteCommitApproval(string commitId){
            return _repositoriesEndPointV2.DeleteCommitApproval(_accountName, _repository, commitId);
        }
    }
}