using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    class WorkspacesEndPointTests
    {
        private WorkspacesEndPoint WorkspacesEndPoint { get; set; }

        [SetUp]
        public void Init()
        {
            var sharpBucket = TestHelpers.SharpBucketV2;
            WorkspacesEndPoint = sharpBucket.WorkspacesEndPoint();
        }

        [Test]
        public void ListWorkspaces_NoParameters_ShouldReturnAtLeastOneWorkspace()
        {
            var workspaces = WorkspacesEndPoint.ListWorkspaces();
            workspaces.ShouldNotBeEmpty();
            workspaces[0].ShouldBeFilled();
        }

        [Test]
        public void ListWorkspaces_FilteringOnOwnerRole_ShouldReturnAtLeastOneWorkspace()
        {
            var parameters = new ListWorkspacesParameters
            {
                Role = WorkspaceRole.Owner,
            };
            var workspaces = WorkspacesEndPoint.ListWorkspaces(parameters);
            workspaces.ShouldNotBeEmpty();
            workspaces[0].ShouldBeFilled();
        }

        [Test]
        public void EnumerateWorkspaces_NoParameters_ShouldReturnAtLeastOneWorkspace()
        {
            var workspaces = WorkspacesEndPoint.EnumerateWorkspaces().ToList();
            workspaces.ShouldNotBeEmpty();
            workspaces[0].ShouldBeFilled();
        }

        [Test]
        public void EnumerateWorkspaces_FilteringOnOwnerRole_ShouldReturnAtLeastOneWorkspace()
        {
            var parameters = new EnumerateWorkspacesParameters
            {
                Role = WorkspaceRole.Owner,
            };
            var workspaces = WorkspacesEndPoint.EnumerateWorkspaces(parameters).ToList();
            workspaces.ShouldNotBeEmpty();
            workspaces[0].ShouldBeFilled();
        }

        [Test]
        public async Task EnumerateWorkspacesAsync_NoParameters_ShouldReturnAtLeastOneWorkspace()
        {
            var workspaces = await WorkspacesEndPoint.EnumerateWorkspacesAsync().ToListAsync();
            workspaces.ShouldNotBeEmpty();
            workspaces[0].ShouldBeFilled();
        }

        [Test]
        public async Task EnumerateWorkspacesAsync_FilteringOnOwnerRole_ShouldReturnAtLeastOneWorkspace()
        {
            var parameters = new EnumerateWorkspacesParameters
            {
                Role = WorkspaceRole.Owner,
            };
            var workspaces = await WorkspacesEndPoint.EnumerateWorkspacesAsync(parameters).ToListAsync();
            workspaces.ShouldNotBeEmpty();
            workspaces[0].ShouldBeFilled();
        }
    }
}