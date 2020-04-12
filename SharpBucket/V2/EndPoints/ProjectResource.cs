using System;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/%7Bproject_key%7D
    /// </summary>
    public class ProjectResource : EndPoint
    {
        private string ProjectKey { get; }
        private string ProjectUrl { get; }

        [Obsolete("Prefer new TeamsEndPoint(sharpBucketV2).TeamResource(teamName).ProjectResource(projectKey) or sharpBucketV2.TeamsEndPoint().TeamResource(teamName).ProjectResource(projectKey)")]
        public ProjectResource(ISharpBucketRequesterV2 sharpBucketV2, string teamName, string projectKey)
            : this(new TeamsEndPoint(sharpBucketV2).TeamResource(teamName), projectKey)
        {
        }

        internal ProjectResource(TeamResource teamResource, string projectKey)
            : base(teamResource, $"projects/{projectKey.CheckIsNotNullNorEmpty(nameof(projectKey)).GuidOrValue()}/")
        {
            this.ProjectKey = projectKey.GuidOrValue();

            // Doing request with that trailif slash cause issues on that resource
            // Maybe it's a good example that should made us change the pattenr used for the _baseUrl in EndPoint
            // for one that ensure that there is no trailing slash.
            this.ProjectUrl = _baseUrl.TrimEnd('/');
        }

        /// <summary>
        /// Get the full project object located at this resource location
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/%7Bproject_key%7D#get
        /// </summary>
        public Project GetProject()
        {
            return _sharpBucketV2.Get<Project>(ProjectUrl);
        }

        /// <summary>
        /// Get the full project object located at this resource location
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/%7Bproject_key%7D#get
        /// </summary>
        /// <param name="token">The cancellation token</param>
        public Task<Project> GetProjectAsync(CancellationToken token = default)
        {
            return _sharpBucketV2.GetAsync<Project>(ProjectUrl, token);
        }

        /// <summary>
        /// Create or update a project at this resource location.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/%7Bproject_key%7D
        /// </summary>
        /// <param name="project">The project object to create or update</param>
        public Project PutProject(Project project)
        {
            var updateableFields = CreatePutProjectInstance(project);
            return _sharpBucketV2.Put(updateableFields, ProjectUrl);
        }

        /// <summary>
        /// Create or update a project at this resource location.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/%7Bproject_key%7D
        /// </summary>
        /// <param name="project">The project object to create or update</param>
        /// <param name="token">The cancellation token</param>
        public Task<Project> PutProjectAsync(Project project, CancellationToken token = default)
        {
            var updateableFields = CreatePutProjectInstance(project);
            return _sharpBucketV2.PutAsync(updateableFields, ProjectUrl, token);
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
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/%7Bproject_key%7D#delete
        /// </summary>
        public void DeleteProject()
        {
            _sharpBucketV2.Delete(ProjectUrl);
        }

        /// <summary>
        /// Delete the project located at this resource location.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/%7Bproject_key%7D#delete
        /// </summary>
        /// <param name="token">The cancellation token</param>
        public Task DeleteProjectAsync(CancellationToken token = default)
        {
            return _sharpBucketV2.DeleteAsync(ProjectUrl, token);
        }
    }
}
