using System;
using System.Collections.Generic;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    public class TeamResource
    {
        private readonly SharpBucketV2 _sharpBucketV2;

        private readonly string _teamName;

        private readonly string _baseUrl;

        public TeamResource(SharpBucketV2 sharpBucketV2, string teamName)
        {
            _sharpBucketV2 = sharpBucketV2 ?? throw new ArgumentNullException(nameof(sharpBucketV2));

            if (string.IsNullOrEmpty(teamName)) throw new ArgumentNullException(nameof(teamName));
            _teamName = teamName.GuidOrValue();
            _baseUrl = $"teams/{teamName}/";
        }

        /// <summary>
        /// Gets the public information associated with a team. 
        /// If the team's profile is private, the caller must be authenticated and authorized to view this information. 
        /// </summary>
        public Team GetProfile()
        {
            return _sharpBucketV2.Get<Team>(_baseUrl);
        }

        /// <summary>
        /// Gets the team's members.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Team> ListMembers(int max = 0)
        {
            var overrideUrl = _baseUrl + "members/";
            return _sharpBucketV2.GetPaginatedValues<Team>(overrideUrl, max);
        }

        /// <summary>
        /// Gets the list of accounts following the team.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Team> ListFollowers(int max = 0)
        {
            var overrideUrl = _baseUrl + "followers/";
            return _sharpBucketV2.GetPaginatedValues<Team>(overrideUrl, max);
        }

        /// <summary>
        /// Gets a list of accounts the team is following.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Team> ListFollowing(int max = 0)
        {
            var overrideUrl = _baseUrl + "following/";
            return _sharpBucketV2.GetPaginatedValues<Team>(overrideUrl, max);
        }

        /// <summary>
        /// Gets the list of the team's repositories. 
        /// Private repositories only appear on this list if the caller is authenticated and is authorized to view the repository.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public List<Repository> ListRepositories(ListParameters parameters = null)
        {
            // The /teams/{username}/repositories request redirect to the repositories/{username}/ request
            // So to improve performances we directly do the the call to the repositories endpoint
            return _sharpBucketV2.RepositoriesEndPoint().ListRepositories(_teamName, parameters ?? new ListParameters());
        }

        /// <summary>
        /// Gets a <see cref="RepositoryResource"/> for a specified repository name, owned by the team represented by this resource.
        /// </summary>
        /// <param name="repoSlugOrName">The repository slug, name, or UUID.</param>
        public RepositoryResource RepositoryResource(string repoSlugOrName)
        {
            return new RepositoryResource(_teamName, repoSlugOrName, _sharpBucketV2.RepositoriesEndPoint());
        }

        /// <summary>
        /// Gets a list of projects that belong to the team.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/#get
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<Project> ListProjects(int max = 0)
        {
            var overrideUrl = _baseUrl + "projects/";
            return _sharpBucketV2.GetPaginatedValues<Project>(overrideUrl, max);
        }

        /// <summary>
        /// Create a new project.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/
        /// </summary>
        /// <param name="project"></param>
        /// <returns>A new project instance that fully represent the newly created project.</returns>
        public Project PostProject(Project project)
        {
            var overrideUrl = _baseUrl + "projects/";
            return _sharpBucketV2.Post(project, overrideUrl);
        }

        /// <summary>
        /// Gets a <see cref="ProjectResource"/> for a specified project key.
        /// </summary>
        /// <param name="projectKey">This can either be the actual key assigned to the project or the UUID.</param>
        public ProjectResource ProjectResource(string projectKey)
        {
            return new ProjectResource(_sharpBucketV2, _teamName, projectKey);
        }
    }
}
