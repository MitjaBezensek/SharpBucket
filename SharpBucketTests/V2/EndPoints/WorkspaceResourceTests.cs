using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    class WorkspaceResourceTests
    {
        private Workspace FirstWorkspace { get; set; }
        private WorkspaceResource FirstWorkspaceResource { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            var sharpBucket = TestHelpers.SharpBucketV2;
            var workspacesEndPoint = sharpBucket.WorkspacesEndPoint();

            this.FirstWorkspace = workspacesEndPoint
                                .EnumerateWorkspaces(new EnumerateWorkspacesParameters { PageLen = 1 })
                                .First();
            this.FirstWorkspaceResource = workspacesEndPoint.WorkspaceResource(this.FirstWorkspace.slug);
        }

        [Test]
        public void GetWorkspace_FromFirstWorkspaceResource_ReturnsFirstWorkspace()
        {
            var workspace = this.FirstWorkspaceResource.GetWorkspace();
            workspace.ShouldBeFilled();
            workspace.ShouldBeEquivalentTo(this.FirstWorkspace);
        }

        [Test]
        public async Task GetWorkspaceAsync_FromFirstWorkspaceResource_ReturnsFirstWorkspace()
        {
            var workspace = await this.FirstWorkspaceResource.GetWorkspaceAsync();
            workspace.ShouldBeFilled();
            workspace.ShouldBeEquivalentTo(this.FirstWorkspace);
        }
    }
}
