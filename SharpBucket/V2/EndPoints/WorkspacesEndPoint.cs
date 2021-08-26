using System;
using System.Collections.Generic;
using System.Threading;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// The Workspaces End Point gets data on accessible workspaces.
    /// More info:
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/workspaces
    /// </summary>
    public class WorkspacesEndPoint : EndPoint
    {
        public WorkspacesEndPoint(ISharpBucketRequesterV2 sharpBucketV2)
            : base(sharpBucketV2, "workspaces/")
        {
        }

        /// <summary>
        /// Returns a list of workspaces accessible by the authenticated user.
        /// </summary>
        public List<Workspace> ListWorkspaces()
        {
            return ListWorkspaces(new ListWorkspacesParameters());
        }

        /// <summary>
        /// Returns a list of workspaces accessible by the authenticated user.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        public List<Workspace> ListWorkspaces(ListWorkspacesParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return GetPaginatedValues<Workspace>(_baseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate all the workspaces accessible by the authenticated user.
        /// </summary>
        public IEnumerable<Workspace> EnumerateWorkspaces()
            => EnumerateWorkspaces(new EnumerateWorkspacesParameters());

        /// <summary>
        /// Enumerate all the workspaces accessible by the authenticated user.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public IEnumerable<Workspace> EnumerateWorkspaces(EnumerateWorkspacesParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return _sharpBucketV2.EnumeratePaginatedValues<Workspace>(
                _baseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate all the workspaces accessible by the authenticated user.
        /// </summary>
        /// <param name="token">The cancellation token</param>
        public IAsyncEnumerable<Workspace> EnumerateWorkspacesAsync(CancellationToken token = default)
            => EnumerateWorkspacesAsync(new EnumerateWorkspacesParameters(), token);

        /// <summary>
        /// Enumerate all the workspaces accessible by the authenticated user.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        /// <param name="token">The cancellation token</param>
        public IAsyncEnumerable<Workspace> EnumerateWorkspacesAsync(
            EnumerateWorkspacesParameters parameters, CancellationToken token = default)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Workspace>(
                _baseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        /// <summary>
        /// Gets a <see cref="WorkspaceResource"/> for a specified workspace.
        /// </summary>
        /// <param name="workspaceSlugOrUuid">The slug or UUID of the workspace.</param>
        public WorkspaceResource WorkspaceResource(string workspaceSlugOrUuid)
        {
            return new WorkspaceResource(this, workspaceSlugOrUuid);
        }
    }
}
