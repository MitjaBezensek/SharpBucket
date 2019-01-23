using System;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/%7Bproject_key%7D
    /// </summary>
    public class ProjectResource
    {
        private SharpBucketV2 SharpBucketV2 { get; }
        private string ProjectKey { get; }
        private string ProjectUrl { get; }

        public ProjectResource(SharpBucketV2 sharpBucketV2, string teamName, string projectKey)
        {
            this.SharpBucketV2 = sharpBucketV2 ?? throw new ArgumentNullException(nameof(sharpBucketV2));
            if (string.IsNullOrEmpty(teamName)) throw new ArgumentNullException(nameof(teamName));
            if (string.IsNullOrEmpty(projectKey)) throw new ArgumentNullException(nameof(projectKey));
            this.ProjectKey = projectKey.GuidOrValue();
            this.ProjectUrl = $"teams/{teamName.GuidOrValue()}/projects/{this.ProjectKey}";
        }

        /// <summary>
        /// Get the full project object located at this resource location
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/%7Bproject_key%7D#get
        /// </summary>
        public Project GetProject()
        {
            return SharpBucketV2.Get<Project>(ProjectUrl);
        }

        /// <summary>
        /// Create or update a project at this resource location.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/%7Bproject_key%7D
        /// </summary>
        /// <param name="project">The project object to create or update</param>
        public Project PutProject(Project project)
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

            return SharpBucketV2.Put(updateableFields, ProjectUrl);
        }

        /// <summary>
        /// Delete the project located at this resource location.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/%7Bproject_key%7D#delete
        /// </summary>
        public void DeleteProject()
        {
            SharpBucketV2.Delete(ProjectUrl);
        }
    }
}
