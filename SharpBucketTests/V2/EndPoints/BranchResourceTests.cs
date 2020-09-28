using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using System.Linq;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;

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
            var branches = SampleRepositories.GitMirrorRepository.BranchesResource.ListBranches();
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
            var branches = SampleRepositories.GitMirrorRepository.BranchesResource.EnumerateBranches();
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
            var branches = await SampleRepositories.GitMirrorRepository.BranchesResource.EnumerateBranchesAsync()
                .ToListAsync();
            branches.ShouldNotBeEmpty("There is at least the main branch on a non empty repository");
        }

        [Test]
        public void PostBranch_NewBranch_BranchIsCreated()
        {
            var branchResource = SampleRepositories.TestRepository.RepositoryResource.BranchesResource;
            var initialBranches = branchResource.ListBranches();
            var newBranch = new Branch
            {
                name = "newPostedBranch",
                target = new Commit
                {
                    hash = SampleRepositories.TestRepository.RepositoryInfo.FirstCommit
                }
            };
            
            var createdBranch = branchResource.PostBranch(newBranch);

            createdBranch.ShouldBeFilled();
            createdBranch.name.ShouldBe(newBranch.name);
            createdBranch.target.hash.ShouldBe(newBranch.target.hash);

            var currentBranches = branchResource.ListBranches();
            currentBranches.Count.ShouldBe(initialBranches.Count + 1);
        }

        [Test]
        public void PostBranchAsync_NewBranch_BranchIsCreated()
        {
            var branchResource = SampleRepositories.TestRepository.RepositoryResource.BranchesResource;
            var initialBranches = branchResource.ListBranches();
            var newBranch = new Branch
            {
                name = "newPostedBranchAsync",
                target = new Commit
                {
                    hash = SampleRepositories.TestRepository.RepositoryInfo.FirstCommit
                }
            };

            var createdBranch = branchResource.PostBranch(newBranch);

            createdBranch.ShouldBeFilled();
            createdBranch.name.ShouldBe(newBranch.name);
            createdBranch.target.hash.ShouldBe(newBranch.target.hash);

            var currentBranches = branchResource.ListBranches();
            currentBranches.Count.ShouldBe(initialBranches.Count + 1);
        }

        [Test]
        public void DeleteBranch_ExistingBranch_BranchCouldNotBeListedAnymore()
        {
            var branchResource = SampleRepositories.TestRepository.RepositoryResource.BranchesResource;
            var initialBranches = branchResource.ListBranches();

            branchResource.DeleteBranch("branchToDelete");

            var remainingBranches = branchResource.ListBranches();
            remainingBranches.Count.ShouldBe(initialBranches.Count - 1);
        }

        [Test]
        public async Task DeleteBranchAsync_ExistingBranch_BranchCouldNotBeListedAnymore()
        {
            var branchResource = SampleRepositories.TestRepository.RepositoryResource.BranchesResource;
            var initialBranches = branchResource.ListBranches();

            await branchResource.DeleteBranchAsync("branchToDeleteAsync");

            var remainingBranches = branchResource.ListBranches();
            remainingBranches.Count.ShouldBe(initialBranches.Count - 1);
        }
    }
}
