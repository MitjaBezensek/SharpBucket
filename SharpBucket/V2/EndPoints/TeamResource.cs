using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    [Obsolete("This endpoint has been deprecated and will stop functioning soon. You should use the WorkspaceResource instead.")]
    public class TeamResource : EndPoint
    {
        private readonly Lazy<RepositoriesAccountResource> _repositoriesResource;

        internal TeamResource(TeamsEndPoint teamsEndPoint, string teamName)
            : base(teamsEndPoint, teamName.CheckIsNotNullNorEmpty(nameof(teamName)).GuidOrValue())
        {
            _repositoriesResource = new Lazy<RepositoriesAccountResource>(
                () => new RepositoriesEndPoint(SharpBucketV2).RepositoriesResource(teamName));
        }

        /// <summary>
        /// Gets the <see cref="RepositoriesAccountResource"/> corresponding to the team of this resource.
        /// </summary>
        /// <remarks>
        /// The /teams/{username}/repositories request redirect to the /repositories/{username} request
        /// It's why providing here a shortcut to the /repositories/{username} resource is valid and equivalent.
        /// </remarks>
        [Obsolete("From WorkspaceResource use RepositoriesResource instead")]
        public RepositoriesAccountResource RepositoriesResource => _repositoriesResource.Value;

        /// <summary>
        /// Gets the public information associated with a team. 
        /// If the team's profile is private, the caller must be authenticated and authorized to view this information. 
        /// </summary>
        [Obsolete("From WorkspaceResource use GetWorkspace() instead")]
        public Team GetProfile()
        {
            throw new NotSupportedException("This has been removed");
        }

        /// <summary>
        /// Gets the public information associated with a team. 
        /// If the team's profile is private, the caller must be authenticated and authorized to view this information. 
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        [Obsolete("From WorkspaceResource use GetWorkspaceAsync(CancellationToken) instead")]
        public Task<Team> GetProfileAsync(CancellationToken token = default)
        {
            throw new NotSupportedException("This has been removed");
        }

        /// <summary>
        /// Gets the team's members.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        [Obsolete("From WorkspaceResource use MembersResource.ListMembers(int) instead")]
        public List<UserInfo> ListMembers(int max = 0)
        {
            var overrideUrl = BaseUrl + "/members";
            return SharpBucketV2.GetPaginatedValues<UserInfo>(overrideUrl, max);
        }

        /// <summary>
        /// Enumerate the team's members,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="pageLen">
        /// The length of a page. If not defined the default page length will be used
        /// </param>
        [Obsolete("From WorkspaceResource use MembersResource.EnumerateMembers(int) instead")]
        public IEnumerable<UserInfo> EnumerateMembers(int? pageLen = null)
        {
            var overrideUrl = BaseUrl + "/members";
            return SharpBucketV2.EnumeratePaginatedValues<UserInfo>(overrideUrl, pageLen: pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate the team's members,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        [Obsolete("From WorkspaceResource use MembersResource.EnumerateMembersAsync(CancellationToken) instead")]
        public IAsyncEnumerable<UserInfo> EnumerateMembersAsync(CancellationToken token = default)
            => EnumerateMembersAsync(null, token);

        /// <summary>
        /// Enumerate the team's members,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="pageLen">
        /// The length of a page. If not defined the default page length will be used
        /// </param>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        [Obsolete("From WorkspaceResource use MembersResource.EnumerateMembersAsync(int?, CancellationToken) instead")]
        public IAsyncEnumerable<UserInfo> EnumerateMembersAsync(int? pageLen, CancellationToken token = default)
        {
            var overrideUrl = BaseUrl + "/members";
            return SharpBucketV2.EnumeratePaginatedValuesAsync<UserInfo>(overrideUrl, null, pageLen, token);
        }
#endif

        /// <summary>
        /// Gets the list of accounts following the team.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        [Obsolete("This endpoint has been deprecated and will stop functioning on August 23rd, 2021. There is no replacement endpoint.")]
        public List<UserInfo> ListFollowers(int max = 0)
        {
            var overrideUrl = BaseUrl + "/followers";
            return SharpBucketV2.GetPaginatedValues<UserInfo>(overrideUrl, max);
        }

        /// <summary>
        /// Enumerate the accounts following the team,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="pageLen">
        /// The length of a page. If not defined the default page length will be used
        /// </param>
        [Obsolete("This endpoint has been deprecated and will stop functioning on August 23rd, 2021. There is no replacement endpoint.")]
        public IEnumerable<UserInfo> EnumerateFollowers(int? pageLen = null)
        {
            var overrideUrl = BaseUrl + "/followers";
            return SharpBucketV2.EnumeratePaginatedValues<UserInfo>(overrideUrl, pageLen: pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate the accounts following the team,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        [Obsolete("This endpoint has been deprecated and will stop functioning on August 23rd, 2021. There is no replacement endpoint.")]
        public IAsyncEnumerable<UserInfo> EnumerateFollowersAsync(CancellationToken token = default)
            => EnumerateFollowersAsync(null, token);

        /// <summary>
        /// Enumerate the accounts following the team,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="pageLen">
        /// The length of a page. If not defined the default page length will be used
        /// </param>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        [Obsolete("This endpoint has been deprecated and will stop functioning on August 23rd, 2021. There is no replacement endpoint.")]
        public IAsyncEnumerable<UserInfo> EnumerateFollowersAsync(int? pageLen, CancellationToken token = default)
        {
            var overrideUrl = BaseUrl + "/followers";
            return SharpBucketV2.EnumeratePaginatedValuesAsync<UserInfo>(overrideUrl, null, pageLen, token);
        }
#endif

        /// <summary>
        /// Gets a list of accounts the team is following.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        [Obsolete("This endpoint has been deprecated and will stop functioning on August 23rd, 2021. There is no replacement endpoint.")]
        public List<UserInfo> ListFollowing(int max = 0)
        {
            var overrideUrl = BaseUrl + "/following";
            return SharpBucketV2.GetPaginatedValues<UserInfo>(overrideUrl, max);
        }

        /// <summary>
        /// Enumerate the accounts the team is following,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="pageLen">
        /// The length of a page. If not defined the default page length will be used
        /// </param>
        [Obsolete("This endpoint has been deprecated and will stop functioning on August 23rd, 2021. There is no replacement endpoint.")]
        public IEnumerable<UserInfo> EnumerateFollowing(int? pageLen = null)
        {
            var overrideUrl = BaseUrl + "/following";
            return SharpBucketV2.EnumeratePaginatedValues<UserInfo>(overrideUrl, pageLen: pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate the accounts the team is following,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        [Obsolete("This endpoint has been deprecated and will stop functioning on August 23rd, 2021. There is no replacement endpoint.")]
        public IAsyncEnumerable<UserInfo> EnumerateFollowingAsync(CancellationToken token = default)
            => EnumerateFollowingAsync(null, token);

        /// <summary>
        /// Enumerate the accounts the team is following,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="pageLen">
        /// The length of a page. If not defined the default page length will be used
        /// </param>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        [Obsolete("This endpoint has been deprecated and will stop functioning on August 23rd, 2021. There is no replacement endpoint.")]
        public IAsyncEnumerable<UserInfo> EnumerateFollowingAsync(int? pageLen, CancellationToken token = default)
        {
            var overrideUrl = BaseUrl + "/following";
            return SharpBucketV2.EnumeratePaginatedValuesAsync<UserInfo>(overrideUrl, null, pageLen, token);
        }
#endif

        /// <summary>
        /// Gets a list of projects that belong to the team.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/#get
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        [Obsolete("From WorkspaceResource use ProjectsResource.ListProjects() instead")]
        public List<Project> ListProjects(int max = 0)
            => ListProjects(new ListParameters { Max = max });

        [Obsolete("From WorkspaceResource use ProjectsResource.ListProjects(ListParameters) instead")]
        public List<Project> ListProjects(ListParameters parameters)
        {
            throw new NotSupportedException("This has been removed");
        }

        /// <summary>
        /// Enumerate projects that belong to the team,
        /// doing requests page by page while enumerating.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/#get
        /// </summary>
        [Obsolete("From WorkspaceResource use ProjectsResource.EnumerateProjects() instead")]
        public IEnumerable<Project> EnumerateProjects()
            => EnumerateProjects(new EnumerateParameters());

        /// <summary>
        /// Enumerate projects that belong to the team,
        /// doing requests page by page while enumerating.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/#get
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        [Obsolete("From WorkspaceResource use ProjectsResource.EnumerateProjects(EnumerateParameters) instead")]
        public IEnumerable<Project> EnumerateProjects(EnumerateParameters parameters)
        {
            throw new NotSupportedException("This has been removed");
        }

#if CS_8
        /// <summary>
        /// Enumerate projects that belong to the team,
        /// doing requests page by page while enumerating.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/#get
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        [Obsolete("From WorkspaceResource use ProjectsResource.EnumerateProjectsAsync(CancellationToken) instead")]
        public IAsyncEnumerable<Project> EnumerateProjectsAsync(CancellationToken token = default)
            => EnumerateProjectsAsync(new EnumerateParameters(), token);

        /// <summary>
        /// Enumerate projects that belong to the team,
        /// doing requests page by page while enumerating.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/#get
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        [Obsolete("From WorkspaceResource use ProjectsResource.EnumerateProjectsAsync(EnumerateParameters, CancellationToken) instead")]
        public IAsyncEnumerable<Project> EnumerateProjectsAsync(EnumerateParameters parameters, CancellationToken token = default)
        {
            throw new NotSupportedException("This has been removed");
        }
#endif

        /// <summary>
        /// Create a new project.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/
        /// </summary>
        /// <param name="project"></param>
        /// <returns>A new project instance that fully represent the newly created project.</returns>
        [Obsolete("From WorkspaceResource use ProjectsResource.PostProject(Project) instead")]
        public Project PostProject(Project project)
        {
            throw new NotSupportedException("This has been removed");
        }

        /// <summary>
        /// Create a new project.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/
        /// </summary>
        /// <param name="project"></param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A new project instance that fully represent the newly created project.</returns>
        [Obsolete("From WorkspaceResource use ProjectsResource.PostProjectAsync(Project, CancellationToken) instead")]
        public Task<Project> PostProjectAsync(Project project, CancellationToken token = default)
        {
            throw new NotSupportedException("This has been removed");
        }

        /// <summary>
        /// Gets a <see cref="ProjectResource"/> for a specified project key.
        /// </summary>
        /// <param name="projectKey">This can either be the actual key assigned to the project or the UUID.</param>
        [Obsolete("From WorkspaceResource use ProjectsResource.ProjectResource(string) instead")]
        public ProjectResource ProjectResource(string projectKey)
        {
            throw new NotSupportedException("This has been removed");
        }

        /// <summary>
        /// Searches for code in team account repositories, and lazily enumerate the search results.
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/search/code
        /// </summary>
        /// <param name="searchQuery">The string that is passed as search query.</param>
        /// <param name="pageLen">The length of a page. If not defined the default page length will be used.</param>
        /// <returns>A lazy enumerable that will request results pages by pages while enumerating the results.</returns>
        [Obsolete("From WorkspaceResource use ProjectsResource.SearchCodeResource.EnumerateSearchResults(string, int?) instead")]
        public IEnumerable<SearchCodeSearchResult> EnumerateSearchCodeSearchResults(
            string searchQuery,
            int? pageLen = null)
        {
            var overrideUrl = $"{BaseUrl}/search/code";
            var requestParameters = new Dictionary<string, object>
            {
                { "search_query", searchQuery }
            };
            return SharpBucketV2.EnumeratePaginatedValues<SearchCodeSearchResult>(overrideUrl, requestParameters, pageLen);
        }
    }
}
