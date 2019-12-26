using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using System.Linq;

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

        [Test]
        public void EnumerateBranches_EmptyRepository_ReturnEmpty()
        {
            var branches = SampleRepositories.EmptyTestRepository.BranchesResource.EnumerateBranches();
            branches.ShouldBeEmpty("There is no branch on an empty repository. At least one commit is mandatory to have a branch.");
        }

        [Test]
        public void EnumerateBranches_NotEmptyRepository_ReturnAtLeastMainBranch()
        {
            var branches = SampleRepositories.MercurialRepository.BranchesResource.EnumerateBranches();
            branches.ShouldNotBeEmpty("There is at least the main branch on a non empty repository");
        }

        [Test]
        public async Task EnumerateBranchesAsync_EmptyRepository_ReturnEmpty()
        {
            var branches = await SampleRepositories.EmptyTestRepository.BranchesResource.EnumerateBranchesAsync()
                .ToListAsync();
            branches.ShouldBeEmpty("There is no branch on an empty repository. At least one commit is mandatory to have a branch.");
        }

        [Test]
        public async Task EnumerateBranchesAsync_NotEmptyRepository_ReturnAtLeastMainBranch()
        {
            var branches = await SampleRepositories.MercurialRepository.BranchesResource.EnumerateBranchesAsync()
                .ToListAsync();
            branches.ShouldNotBeEmpty("There is at least the main branch on a non empty repository");
        }

        [Test]
        public void DeleteBranch_ExistingBranch_BrancCouldNotBeListedAnymore()
        {
            var branchResource = SampleRepositories.TestRepository.RepositoryResource.BranchesResource;
            var initialBranches = branchResource.ListBranches();

            branchResource.DeleteBranch("branchToDelete");

            var remainingBranches = branchResource.ListBranches();
            remainingBranches.Count.ShouldBe(initialBranches.Count - 1);
        }

        [Test]
        public async Task DeleteBranchAsync_ExistingBranch_BrancCouldNotBeListedAnymore()
        {
            var branchResource = SampleRepositories.TestRepository.RepositoryResource.BranchesResource;
            var initialBranches = branchResource.ListBranches();

            await branchResource.DeleteBranchAsync("branchToDeleteAsync");

            var remainingBranches = branchResource.ListBranches();
            remainingBranches.Count.ShouldBe(initialBranches.Count - 1);
        }
    }
}
