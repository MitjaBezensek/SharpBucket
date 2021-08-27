using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    /// <summary>
    /// The branching model as applied to the repository.This view is read-only.
    /// More info:
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/branching-model
    /// </summary>
    public class BranchingModel
    {
        public BranchingModelBranch development { get; set; }
        public BranchingModelBranch production { get; set; }
        public List<BranchType> branch_types { get; set; }
        public string type { get; set; }
        public BranchingModelLinks links { get; set; }
    }
}