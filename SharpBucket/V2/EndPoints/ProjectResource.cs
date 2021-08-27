using System;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/workspaces/%7Bworkspace%7D/projects/%7Bproject_key%7D
    /// </summary>
    public class ProjectResource : EndPoint
    {
        private string ProjectKey { get; }

        internal ProjectResource(ProjectsResource projectsResource, string projectKey)
            : base(projectsResource, projectKey.CheckIsNotNullNorEmpty(nameof(projectKey)).GuidOrValue())
        {
            this.ProjectKey = projectKey.GuidOrValue();
        }

        /// <summary>
        /// Get the full project object located at this resource location
        /// </summary>
        public Project GetProject()
        {
            return SharpBucketV2.Get<Project>(BaseUrl);
        }

        /// <summary>
        /// Get the full project object located at this resource location
        /// </summary>
        /// <param name="token">The cancellation token</param>
        public Task<Project> GetProjectAsync(CancellationToken token = default)
        {
            return SharpBucketV2.GetAsync<Project>(BaseUrl, token);
        }

        /// <summary>
        /// Create or update a project at this resource location.
        /// </summary>
        /// <param name="project">The project object to create or update</param>
        public Project PutProject(Project project)
        {
            var updateableFields = CreatePutProjectInstance(project);
            return SharpBucketV2.Put(updateableFields, BaseUrl);
        }

        /// <summary>
        /// Create or update a project at this resource location.
        /// </summary>
        /// <param name="project">The project object to create or update</param>
        /// <param name="token">The cancellation token</param>
        public Task<Project> PutProjectAsync(Project project, CancellationToken token = default)
        {
            var updateableFields = CreatePutProjectInstance(project);
            return SharpBucketV2.PutAsync(updateableFields, BaseUrl, token);
        }

        private Project CreatePutProjectInstance(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));

            // create an instance that contains only the fields accepted in the PUT operation
            var updateableFields = new Project
            {
                name = project.name,
                description = project.description,
                is_private = project.is_private
            };

            // include the key field only if needed, which should be only when the intent is to change the key itself.
            if (project.key != null && !project.key.Equals(this.ProjectKey, StringComparison.Ordinal))
            {
                updateableFields.key = project.key;
            }

            return updateableFields;
        }

        /// <summary>
        /// Delete the project located at this resource location.
        /// </summary>
        public void DeleteProject()
        {
            SharpBucketV2.Delete(BaseUrl);
        }

        /// <summary>
        /// Delete the project located at this resource location.
        /// </summary>
        /// <param name="token">The cancellation token</param>
        public Task DeleteProjectAsync(CancellationToken token = default)
        {
            return SharpBucketV2.DeleteAsync(BaseUrl, token);
        }
    }
}
