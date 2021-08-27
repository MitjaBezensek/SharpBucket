using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    class WorkspaceMembersResourceTests
    {
        private WorkspaceMembersResource UserWorkspaceMembersResource { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            var sharpBucket = TestHelpers.SharpBucketV2;
            var workspacesEndPoint = sharpBucket.WorkspacesEndPoint();

            var userWorkspace = workspacesEndPoint
                                .EnumerateWorkspaces(new EnumerateWorkspacesParameters { PageLen = 1 })
                                .First();
            this.UserWorkspaceMembersResource = workspacesEndPoint
                                                .WorkspaceResource(userWorkspace.slug)
                                                .MembersResource;
        }

        [Test]
        public void ListMembers_ForUserWorkspace_ShouldReturnLoggedUser()
        {
            var members = this.UserWorkspaceMembersResource.ListMembers();

            var nickname = TestHelpers.AccountName;
            members.ShouldContain(m => m.user.nickname == nickname);
        }

        [Test]
        public void EnumerateMembers_ForUserWorkspace_ShouldReturnLoggedUser()
        {
            var members = this.UserWorkspaceMembersResource.EnumerateMembers();

            var nickname = TestHelpers.AccountName;
            members.ShouldContain(m => m.user.nickname == nickname);
        }

        [Test]
        public async Task EnumerateMembersAsync_ForUserWorkspace_ShouldReturnLoggedUser()
        {
            var members = await this.UserWorkspaceMembersResource.EnumerateMembersAsync().ToListAsync();

            var nickname = TestHelpers.AccountName;
            members.ShouldContain(m => m.user.nickname == nickname);
        }
    }
}
