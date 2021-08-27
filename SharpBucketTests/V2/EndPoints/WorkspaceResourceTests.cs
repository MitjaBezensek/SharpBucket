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
    class WorkspaceResourceTests
    {
        private Workspace UserWorkspace { get; set; }
        private WorkspaceResource UserWorkspaceResource { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            var sharpBucket = TestHelpers.SharpBucketV2;
            var workspacesEndPoint = sharpBucket.WorkspacesEndPoint();

            this.UserWorkspace = workspacesEndPoint
                                .EnumerateWorkspaces(new EnumerateWorkspacesParameters { PageLen = 1 })
                                .First();
            this.UserWorkspaceResource = workspacesEndPoint.WorkspaceResource(this.UserWorkspace.slug);
        }

        [Test]
        public void GetWorkspace_ForUserWorkspace_ReturnsUserWorkspace()
        {
            var workspace = this.UserWorkspaceResource.GetWorkspace();
            workspace.ShouldBeFilled();
            workspace.ShouldBeEquivalentTo(this.UserWorkspace);
        }

        [Test]
        public async Task GetWorkspaceAsync_ForUserWorkspace_ReturnsUserWorkspace()
        {
            var workspace = await this.UserWorkspaceResource.GetWorkspaceAsync();
            workspace.ShouldBeFilled();
            workspace.ShouldBeEquivalentTo(this.UserWorkspace);
        }
    }
}
