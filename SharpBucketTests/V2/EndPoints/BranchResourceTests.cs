using NUnit.Framework;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    internal class BranchResourceTests
    {
        [Test]
        public void ListBranches_EmptyRepository_ReturnEmpty()
        {
            var branches = SampleRepositories.EmptyTestRepository.BranchesResource.ListBranches();
            branches.ShouldBeEmpty("There is no branch on an empty repository. At least one commit is mandatory to have a branch.");
        }

        [Test]
        public void ListBranches_NotEmptyRepository_ReturnAtLeastMainBranch()
        {
            var branches = SampleRepositories.MercurialRepository.BranchesResource.ListBranches();
            branches.ShouldNotBeEmpty("There is at least the main branch on a non empty repository");
        }
    }
}
