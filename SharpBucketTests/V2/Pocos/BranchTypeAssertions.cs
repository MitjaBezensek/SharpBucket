using System.Collections.Generic;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class BranchTypeAssertions
    {
        public static TBranchTypes ShouldBeFilled<TBranchTypes>(this TBranchTypes branchTypes)
            where TBranchTypes : IEnumerable<BranchType>
        {
            branchTypes.ShouldNotBeNull();
            foreach (var branchType in branchTypes)
            {
                branchType.ShouldBeFilled();
            }
            return branchTypes;
        }

        public static BranchType ShouldBeFilled(this BranchType branchType)
        {
            branchType.ShouldNotBeNull();
            branchType.kind.ShouldNotBeNullOrWhiteSpace();
            branchType.prefix.ShouldNotBeNullOrWhiteSpace();
            return branchType;
        }
    }
}