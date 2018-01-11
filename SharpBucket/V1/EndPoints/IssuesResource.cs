using System.Collections.Generic;
using SharpBucket.V1.Pocos;
using System.Dynamic;

namespace SharpBucket.V1.EndPoints
{
    /// <summary>
    /// The issues resource provides functionality for getting information on issues in an issue tracker, 
    /// creating new issues, updating them and deleting them. 
    /// You can access public issues without authentication, but you will only receive a subset of information, 
    /// and you can't gain access to private repositories' issues. By authenticating, you will get a more detailed set of information, 
    /// the ability to create issues, as well as access to updating data or deleting issues you have access to.
    /// </summary>
    public class IssuesResource
    {
        private readonly RepositoriesEndPoint _repositoriesEndPoint;

        #region Issues Resource

        public IssuesResource(RepositoriesEndPoint repositoriesEndPoint)
        {
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// Gets the list of issues in the repository.
        /// If you issue this call without filtering parameters, the count value contains the total number of issues in the repository's tracker.  
        /// If you filter this call, the count value contains the total number of issues that meet the filter criteria.
        /// </summary>
        /// <returns></returns>
        public IssuesInfo ListIssues(IssueSearchParameters parameters = null)
        {
            return _repositoriesEndPoint.ListIssues(parameters);
        }

        /// <summary>
        /// Gets in individual issue from a repository. 
        /// Authorization is not required for public repositories with a public issue tracker. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access. 
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <returns></returns>
        public Issue GetIssue(int? issueId)
        {
            return _repositoriesEndPoint.GetIssue(issueId);
        }

        /// <summary>
        /// Creates a new issue in a repository. This call requires authentication. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// The authenticated user is used for the issue's reported_by field.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Issue PostIssue(Issue issue)
        {
            return _repositoriesEndPoint.PostIssue(issue);
        }

        /// <summary>
        /// Updates an existing issue. Updating the title or content fields requires that the caller authenticate as a user with write access. 
        /// For other fields, the caller must authenticate as a user with read access. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access. 
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Issue PutIssue(Issue issue)
        {
            return _repositoriesEndPoint.PutIssue(issue);
        }

        /// <summary>
        /// Deletes the specified issue_id. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access. 
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Issue DeleteIssue(int? issueId)
        {
            return _repositoriesEndPoint.DeleteIssue(issueId);
        }

        /// <summary>
        /// List the components associated with the issue tracker. 
        /// </summary>
        /// <returns></returns>
        public List<Component> ListComponents()
        {
            return _repositoriesEndPoint.ListComponents();
        }

        /// <summary>
        /// Gets an individual component in an issue tracker. 
        /// To get a component, private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="componentId">The component identifier.</param>
        /// <returns></returns>
        public Component GetComponent(int? componentId)
        {
            return _repositoriesEndPoint.GetComponent(componentId);
        }

        /// <summary>
        /// Creates a new component in an issue tracker. 
        /// You must supply a name value in the form of a string. 
        /// The server creates the id for you and it appears in the return value. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Component PostComponent(Component component)
        {
            return _repositoriesEndPoint.PostComponent(component);
        }

        /// <summary>
        /// Updates an existing component in an issue tracker. 
        /// You must supply a name value in the form of a string. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Component PutComponent(Component component)
        {
            return _repositoriesEndPoint.PutComponent(component);
        }

        /// <summary>
        /// Deletes a component in an issue tracker. Keep in mind that the component can be in use on existing issues. 
        /// To delete a component, public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="componentId">The component identifier.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Component DeleteComponent(int? componentId)
        {
            return _repositoriesEndPoint.DeleteComponent(componentId);
        }

        /// <summary>
        /// List all the versions associated with the issue tracker.
        /// </summary>
        /// <returns></returns>
        public List<Version> ListVersions()
        {
            return _repositoriesEndPoint.ListVersions();
        }

        /// <summary>
        /// Gets an individual version in an issue tracker. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="versionId">The version identifier.</param>
        /// <returns></returns>
        public Version GetVersion(int? versionId)
        {
            return _repositoriesEndPoint.GetVersion(versionId);
        }

        /// <summary>
        /// Creates a new version in an issue tracker. You must supply a name value in the form of a string. 
        /// The server creates the id for you and it appears in the return value. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version PostVersion(Version version)
        {
            return _repositoriesEndPoint.PostVersion(version);
        }

        /// <summary>
        /// Updates an existing version in an issue tracker. You must supply a name value in the form of a string. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version PutVersion(Version version)
        {
            return _repositoriesEndPoint.PutVersion(version);
        }

        /// <summary>
        /// Deletes a version in an issue tracker. Keep in mind that the version can be in use on existing issues. 
        /// To delete a version, public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="versionId">The version identifier.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version DeleteVersion(int? versionId)
        {
            return _repositoriesEndPoint.DeleteVersion(versionId);
        }

        /// <summary>
        /// List all the milestones associated with the issue tracker.
        /// </summary>
        /// <returns></returns>
        public List<Milestone> ListMilestones()
        {
            return _repositoriesEndPoint.ListMilestones();
        }

        /// <summary>
        /// Gets an individual milestone in an issue tracker. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation.
        /// </summary>
        /// <param name="milestoneId">The milestone identifier.</param>
        /// <returns></returns>
        public Milestone GetMilestone(int? milestoneId)
        {
            return _repositoriesEndPoint.GetMilestone(milestoneId);
        }

        /// <summary>
        /// Creates a new milestone in an issue tracker. You must supply a name value in the form of a string. 
        /// The server creates the id for you and it appears in the return value. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="milestone">The milestone.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone PostMilestone(Milestone milestone)
        {
            return _repositoriesEndPoint.PostMilestone(milestone);
        }

        /// <summary>
        /// Updates an existing milestone in an issue tracker. 
        /// You must supply a name value in the form of a string. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation.
        /// </summary>
        /// <param name="milestone">The milestone.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone PutMilestone(Milestone milestone)
        {
            return _repositoriesEndPoint.PutMilestone(milestone);
        }

        /// <summary>
        /// Deletes a milestone in an issue tracker. Keep in mind that the milestone can be in use on existing issues. 
        /// To delete a milestone, public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="milestoneId">The milestone identifier.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone DeleteMilestone(int? milestoneId)
        {
            return _repositoriesEndPoint.DeleteMilestone(milestoneId);
        }

        #endregion

        #region Issue Resource

        /// <summary>
        /// Get the issue resource.
        /// BitBucket does not have this resource so this is a "virtual" resource
        /// which offers easier access manipulation of the specified issue.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose resource you wish to get.</param>
        /// <returns></returns>
        public IssueResource IssueResource(int issueId)
        {
            return new IssueResource(_repositoriesEndPoint, issueId);
        }

        internal List<Comment> ListIssueComments(Issue issue)
        {
            return _repositoriesEndPoint.ListIssueComments(issue);
        }

        /// <summary>
        /// List all the comments on the specified issue. 
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <returns></returns>
        internal List<Comment> ListIssueComments(int issueId)
        {
            return _repositoriesEndPoint.ListIssueComments(issueId);
        }

        /// <summary>
        /// Get a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        internal Comment GetIssueComment(Issue issue, int? commentId)
        {
            return _repositoriesEndPoint.GetIssueComment(issue, commentId);
        }

        /// <summary>
        /// Get a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        internal Comment GetIssueComment(int issueId, int? commentId)
        {
            return _repositoriesEndPoint.GetIssueComment(issueId, commentId);
        }

        /// <summary>
        /// Post a comment to the selected issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>Response from the BitBucket API.</returns>
        internal Comment PostIssueComment(Issue issue, Comment comment)
        {
            return _repositoriesEndPoint.PostIssueComment(issue, comment);
        }

        /// <summary>
        /// Post a comment to the selected issue.
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>Response from the BitBucket API.</returns>
        internal Comment PostIssueComment(int issueId, Comment comment)
        {
            return _repositoriesEndPoint.PostIssueComment(issueId, comment);
        }

        /// <summary>
        /// Update a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>The response of BitBucket API.</returns>
        internal Comment PutIssueComment(Issue issue, Comment comment)
        {
            return _repositoriesEndPoint.PutIssueComment(issue, comment);
        }

        /// <summary>
        /// Update a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>The response of BitBucket API.</returns>
        internal Comment PutIssueComment(int issueId, Comment comment)
        {
            return _repositoriesEndPoint.PutIssueComment(issueId, comment);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>The response of BitBucket API.</returns>
        internal Comment DeleteIssueComment(Issue issue, Comment comment)
        {
            return _repositoriesEndPoint.DeleteIssueComment(issue, comment);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns>The response of BitBucket API.</returns>
        internal Comment DeleteIssueComment(Issue issue, int? commentId)
        {
            return _repositoriesEndPoint.DeleteIssueComment(issue, commentId);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns>The response of BitBucket API.</returns>
        internal Comment DeleteIssueComment(int? issueId, int? commentId)
        {
            return _repositoriesEndPoint.DeleteIssueComment(issueId, commentId);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>The response of BitBucket API.</returns>
        internal Comment DeleteIssueComment(int? issueId, Comment comment)
        {
            return _repositoriesEndPoint.DeleteIssueComment(issueId, comment);
        }

        #endregion
    }
}