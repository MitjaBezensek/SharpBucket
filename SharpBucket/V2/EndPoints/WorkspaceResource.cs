using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    public class WorkspaceResource : EndPoint
    {
        public string WorkspaceSlugOrUuid { get; }

        public WorkspaceMembersResource MembersResource
            => new WorkspaceMembersResource(this);

        public ProjectsResource ProjectsResource
            => new ProjectsResource(this);

        public RepositoriesAccountResource RepositoriesResource
            => new RepositoriesEndPoint(SharpBucketV2).RepositoriesResource(this.WorkspaceSlugOrUuid);

        public SearchCodeResource SearchCodeResource
            => new SearchCodeResource(this);

        internal WorkspaceResource(WorkspacesEndPoint parentEndPoint, string workspaceSlugOrUuid)
            : base(
                parentEndPoint,
                workspaceSlugOrUuid.CheckIsNotNullNorEmpty(nameof(workspaceSlugOrUuid)).GuidOrValue())
        {
            this.WorkspaceSlugOrUuid = workspaceSlugOrUuid;
        }

        /// <summary>
        /// Gets the workspace object.
        /// </summary>
        public Workspace GetWorkspace()
        {
            return this.SharpBucketV2.Get<Workspace>(this.BaseUrl);
        }

        /// <summary>
        /// Gets the workspace object.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<Workspace> GetWorkspaceAsync(CancellationToken token = default)
        {
            return await this.SharpBucketV2.GetAsync<Workspace>(this.BaseUrl, token);
        }
    }
}
