using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Manage the branching model for a repository.
    /// More info:
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/refs/tags
    /// </summary>
    public class BranchingModelResource : EndPoint
    {
        internal BranchingModelResource(RepositoryResource repositoryResource)
         : base(repositoryResource, "branching-model")
        {
        }

        /// <summary>
        /// Returns the branching model as applied to the repository. This view is read-only.
        /// More info:
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/branching-model
        /// </summary>
        public BranchingModel GetBranchingModel()
        {
            return SharpBucketV2.Get<BranchingModel>(BaseUrl);
        }

        /// <summary>
        /// Return the branching model configuration for a repository. 
        /// More info: 
        ///     https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/branching-model/settings#get
        /// </summary>
        public BranchingModelSettings GetSettings()
        {
            return SharpBucketV2.Get<BranchingModelSettings>(BaseUrl + "/settings");
        }

        /// <summary>
        /// Update the branching model configuration for a repository.
        /// More info: 
        ///     https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/branching-model/settings#put
        /// </summary>
        public BranchingModelSettings PutSettings(BranchingModelSettings branchingModelSettings)
        {
            return SharpBucketV2.Put(branchingModelSettings, BaseUrl + "/settings");
        }

    }
}
