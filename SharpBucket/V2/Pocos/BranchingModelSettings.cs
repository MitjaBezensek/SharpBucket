using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBucket.V2.Pocos
{

    /// <summary>
    /// Settings to manage the Branching model for a repository.
    /// More info:
    ///     https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/branching-model/settings
    /// </summary>
    public class BranchingModelSettings
    {
        public BranchingModelDevelopmentSettings development { get; set; }
        public BranchingModelProductionSettings production { get; set; }
        public List<BranchingModelSettingsBranchType> branch_types { get; set; }
        public string type { get; set; }
        public BranchingModelLinks links { get; set; }
    }

}
