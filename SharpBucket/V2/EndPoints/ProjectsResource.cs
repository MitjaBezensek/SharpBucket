using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/workspaces/%7Bworkspace%7D/projects
    /// </summary>
    public class ProjectsResource : EndPoint
    {
        internal ProjectsResource(WorkspaceResource workspaceResource)
            : base(workspaceResource, "projects")
        {
        }

        /// <summary>
        /// Gets the list of projects in current workspace.
        /// </summary>
        public List<Project> ListProjects()
        {
            return ListProjects(new ListParameters());
        }

        /// <summary>
        /// Gets the list of projects in current workspace.
        /// </summary>
        public List<Project> ListProjects(ListParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return SharpBucketV2.GetPaginatedValues<Project>(BaseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate projects in current workspace,
        /// doing requests page by page while enumerating.
        /// </summary>
        public IEnumerable<Project> EnumerateProjects()
            => EnumerateProjects(new EnumerateParameters());

        /// <summary>
        /// Enumerate projects in current workspace,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public IEnumerable<Project> EnumerateProjects(EnumerateParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return SharpBucketV2.EnumeratePaginatedValues<Project>(BaseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate projects in current workspace,
        /// doing requests page by page while enumerating.
        /// </summary>
        public IAsyncEnumerable<Project> EnumerateProjectsAsync(CancellationToken token = default)
            => EnumerateProjectsAsync(new EnumerateParameters(), token);

        /// <summary>
        /// Enumerate projects in current workspace,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public IAsyncEnumerable<Project> EnumerateProjectsAsync(EnumerateParameters parameters, CancellationToken token = default)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return SharpBucketV2.EnumeratePaginatedValuesAsync<Project>(BaseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        /// <summary>
        /// Create a new project.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>A new project instance that fully represent the newly created project.</returns>
        public Project PostProject(Project project)
        {
            return SharpBucketV2.Post(project, BaseUrl);
        }

        /// <summary>
        /// Create a new project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A new project instance that fully represent the newly created project.</returns>
        public async Task<Project> PostProjectAsync(Project project, CancellationToken token = default)
        {
            return await SharpBucketV2.PostAsync(project, BaseUrl, token);
        }

        /// <summary>
        /// Gets a <see cref="ProjectResource"/> for a specified project key.
        /// </summary>
        /// <param name="projectKey">This can either be the actual key assigned to the project or the UUID.</param>
        public ProjectResource ProjectResource(string projectKey)
        {
            return new ProjectResource(this, projectKey);
        }
    }
}
