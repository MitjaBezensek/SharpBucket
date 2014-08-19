using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints{
    /// <summary>
    /// The issues resource provides functionality for getting information on issues in an issue tracker, 
    /// creating new issues, updating them and deleting them. 
    /// You can access public issues without authentication, but you will only receive a subset of information, 
    /// and you can't gain access to private repositories' issues. By authenticating, you will get a more detailed set of information, 
    /// the ability to create issues, as well as access to updating data or deleting issues you have access to.
    /// </summary>
    public class IssuesResourceV1{
        private readonly RepositoriesEndPointV1 _repositoriesEndPointV1;

        /// <summary>
        /// Get the issue resource.
        /// BitBucket does not have this resource so this is a "virtual" resource
        /// which offers easier access manipulation of the specified issue.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose resource you wish to get.</param>
        /// <returns></returns>
        public IssueResourceV1 Issue(int issueId){
            return new IssueResourceV1(_repositoriesEndPointV1, issueId);
        }

        public IssuesResourceV1(RepositoriesEndPointV1 repositoriesEndPointV1){
            _repositoriesEndPointV1 = repositoriesEndPointV1;
        }

        /// <summary>
        /// Gets the list of issues in the repository.
        /// If you issue this call without filtering parameters, the count value contains the total number of issues in the repository's tracker.  
        /// If you filter this call, the count value contains the total number of issues that meet the filter criteria.
        /// </summary>
        /// <returns></returns>
        public IssuesInfo ListIssues(){
            return _repositoriesEndPointV1.ListIssues();
        }

        /// <summary>
        /// Gets in individual issue from a repository. 
        /// Authorization is not required for public repositories with a public issue tracker. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access. 
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <returns></returns>
        public Issue GetIssue(int? issueId){
            return _repositoriesEndPointV1.GetIssue(issueId);
        }

        /// <summary>
        /// Gets the followers for an individual issue from a repository. 
        /// authorization is not required for public repositories with a public issue tracker. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access.
        /// </summary>
        /// <param name="issue">The issue whose followers you wish to get.</param>
        /// <returns></returns>
        public IssueFollowers ListIssueFollowers(Issue issue){
            return _repositoriesEndPointV1.ListIssueFollowers(issue);
        }

        /// <summary>
        /// Gets the followers for an individual issue from a repository. 
        /// authorization is not required for public repositories with a public issue tracker. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access.
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <returns></returns>
        public IssueFollowers ListIssueFollowers(int? issueId){
            return _repositoriesEndPointV1.ListIssueFollowers(issueId);
        }

        /// <summary>
        /// Creates a new issue in a repository. This call requires authentication. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// The authenticated user is used for the issue's reported_by field.
        /// </summary>
        /// <param name="issue">The issue that you wish to post.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Issue PostIssue(Issue issue){
            return _repositoriesEndPointV1.PostIssue(issue);
        }

        /// <summary>
        /// Updates an existing issue. Updating the title or content fields requires that the caller authenticate as a user with write access. 
        /// For other fields, the caller must authenticate as a user with read access. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access. 
        /// </summary>
        /// <param name="issue">The issue that you wish to update.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Issue PutIssue(Issue issue){
            return _repositoriesEndPointV1.PutIssue(issue);
        }

        /// <summary>
        /// Delete an issue from the current repository.
        /// </summary>
        /// <param name="issue">The issue that you wish to delete.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Issue DeleteIssue(Issue issue){
            return _repositoriesEndPointV1.DeleteIssue(issue);
        }

        /// <summary>
        /// Deletes the specified issue_id. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access. 
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Issue DeleteIssue(int? issueId){
            return _repositoriesEndPointV1.DeleteIssue(issueId);
        }

        /// <summary>
        /// List all the comments on the specified issue. 
        /// </summary>
        /// <param name="issue">The issue whose comments you wish to get.</param>
        /// <returns></returns>
        public List<Comment> ListIssueComments(Issue issue){
            return _repositoriesEndPointV1.ListIssueComments(issue);
        }

        /// <summary>
        /// List all the comments on the specified issue. 
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <returns></returns>
        public List<Comment> ListIssueComments(int issueId){
            return _repositoriesEndPointV1.ListIssueComments(issueId);
        }

        /// <summary>
        /// Get a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue whose comment you wish to get.</param>
        /// <param name="commentId">The identifier of the comment you wish to get.</param>
        /// <returns></returns>
        public Comment GetIssueComment(Issue issue, int? commentId){
            return _repositoriesEndPointV1.GetIssueComment(issue, commentId);
        }

        /// <summary>
        /// Get a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The identifier of the issue whose comment you wish to get.</param>
        /// <param name="commentId">The identifier of the comment you wish to get.</param>
        /// <returns></returns>
        public Comment GetIssueComment(int issueId, int? commentId){
            return _repositoriesEndPointV1.GetIssueComment(issueId, commentId);
        }

        /// <summary>
        /// Post a comment to the selected issue.
        /// </summary>
        /// <param name="issue">The issue to which you want to post a comment.</param>
        /// <param name="comment">The comment you wish to post.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Comment PostIssueComment(Issue issue, Comment comment){
            return _repositoriesEndPointV1.PostIssueComment(issue, comment);
        }

        /// <summary>
        /// Post a comment to the selected issue.
        /// </summary>
        /// <param name="issueId">The identifier of the issue to which you wish to post a comment.</param>
        /// <param name="comment">The comment you wish to post.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Comment PostIssueComment(int issueId, Comment comment){
            return _repositoriesEndPointV1.PostIssueComment(issueId, comment);
        }

        /// <summary>
        /// Update a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue whose comment you wish to update.</param>
        /// <param name="comment">The comment that you wish to update.</param>
        /// <returns>The response of BitBucket API.</returns>
        public Comment PutIssueComment(Issue issue, Comment comment){
            return _repositoriesEndPointV1.PutIssueComment(issue, comment);
        }

        /// <summary>
        /// Update a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The identifier of the issue whose comment you wish to update.</param>
        /// <param name="comment">The comment that you wish to update.</param>
        /// <returns>The response of BitBucket API.</returns>
        public Comment PutIssueComment(int issueId, Comment comment){
            return _repositoriesEndPointV1.PutIssueComment(issueId, comment);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue whose comment you wish to delete.</param>
        /// <param name="comment">The comment that you wish to delete.</param>
        /// <returns>The response of BitBucket API.</returns>
        public Comment DeleteIssueComment(Issue issue, Comment comment){
            return _repositoriesEndPointV1.DeleteIssueComment(issue, comment);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue whose comment you wish to delete.</param>
        /// <param name="commentId">The identifier of the comment you wish to delete.</param>
        /// <returns>The response of BitBucket API.</returns>
        public Comment DeleteIssueComment(Issue issue, int? commentId){
            return _repositoriesEndPointV1.DeleteIssueComment(issue, commentId);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The identifier of the issue whose comment you wish to delete.</param>
        /// <param name="commentId">The identifier of the comment you wish to delete.</param>
        /// <returns>The response of BitBucket API.</returns>
        public Comment DeleteIssueComment(int? issueId, int? commentId){
            return _repositoriesEndPointV1.DeleteIssueComment(issueId, commentId);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The identifier of the issue whose comment you wish to delete.</param>
        /// <param name="comment">The comment that you wish to delete.</param>
        /// <returns>The response of BitBucket API.</returns>
        public Comment DeleteIssueComment(int? issueId, Comment comment){
            return _repositoriesEndPointV1.DeleteIssueComment(issueId, comment);
        }

        /// <summary>
        /// List the components associated with the issue tracker. 
        /// </summary>
        /// <returns></returns>
        public List<Component> ListComponents(){
            return _repositoriesEndPointV1.ListComponents();
        }

        /// <summary>
        /// Gets an individual component in an issue tracker. 
        /// To get a component, private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="componentId">The Id of the component you wish to get.</param>
        /// <returns></returns>
        public Component GetComponent(int? componentId){
            return _repositoriesEndPointV1.GetComponent(componentId);
        }

        /// <summary>
        /// Creates a new component in an issue tracker. 
        /// You must supply a name value in the form of a string. 
        /// The server creates the id for you and it appears in the return value. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation.
        /// </summary>
        /// <param name="component"></param>
        /// <returns>The response from the BitBucket API.</returns>
        public Component PostComponent(Component component){
            return _repositoriesEndPointV1.PostComponent(component);
        }

        /// <summary>
        /// pdates an existing component in an issue tracker. 
        /// You must supply a name value in the form of a string. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="component">The component that you wish to update.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Component PutComponent(Component component){
            return _repositoriesEndPointV1.PutComponent(component);
        }

        /// <summary>
        /// Deletes a component in an issue tracker. Keep in mind that the component can be in use on existing issues. 
        /// To delete a component, public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="component">The component that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Component DeleteComponent(Component component){
            return _repositoriesEndPointV1.DeleteComponent(component);
        }

        /// <summary>
        /// Deletes a component in an issue tracker. Keep in mind that the component can be in use on existing issues. 
        /// To delete a component, public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="componentId">The Id of the component that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Component DeleteComponent(int? componentId){
            return _repositoriesEndPointV1.DeleteComponent(componentId);
        }

        /// <summary>
        /// List all the versions associated with the issue tracker.
        /// </summary>
        /// <returns></returns>
        public List<Version> ListVersions(){
            return _repositoriesEndPointV1.ListVersions();
        }

        /// <summary>
        /// Gets an individual version in an issue tracker. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="versionId">The Id of the version you wish to get.</param>
        /// <returns></returns>
        public Version GetVersion(int? versionId){
            return _repositoriesEndPointV1.GetVersion(versionId);
        }

        /// <summary>
        /// Creates a new version in an issue tracker. You must supply a name value in the form of a string. 
        /// The server creates the id for you and it appears in the return value. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="version">The version you wish to add.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version PostVersion(Version version){
            return _repositoriesEndPointV1.PostVersion(version);
        }

        /// <summary>
        /// Updates an existing version in an issue tracker. You must supply a name value in the form of a string. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="version">The version you wish to update.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version PutVersion(Version version){
            return _repositoriesEndPointV1.PutVersion(version);
        }

        /// <summary>
        /// Deletes a version in an issue tracker. Keep in mind that the version can be in use on existing issues. 
        /// To delete a version, public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="version">The version that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version DeleteVersion(Version version){
            return _repositoriesEndPointV1.DeleteVersion(version);
        }

        /// <summary>
        /// Deletes a version in an issue tracker. Keep in mind that the version can be in use on existing issues. 
        /// To delete a version, public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="versionId">The Id of the version that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version DeleteVersion(int? versionId){
            return _repositoriesEndPointV1.DeleteVersion(versionId);
        }

        /// <summary>
        /// List all the milestones associated with the issue tracker.
        /// </summary>
        /// <returns></returns>
        public List<Milestone> ListMilestones(){
            return _repositoriesEndPointV1.ListMilestones();
        }

        /// <summary>
        /// Gets an individual milestone in an issue tracker. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation.
        /// </summary>
        /// <param name="milestoneId">The Id of the milestone you wish to get.</param>
        /// <returns></returns>
        public Milestone GetMilestone(int? milestoneId){
            return _repositoriesEndPointV1.GetMilestone(milestoneId);
        }

        /// <summary>
        /// Creates a new milestone in an issue tracker. You must supply a name value in the form of a string. 
        /// The server creates the id for you and it appears in the return value. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="milestone">The milestone that you wish to add.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone PostMilestone(Milestone milestone){
            return _repositoriesEndPointV1.PostMilestone(milestone);
        }

        /// <summary>
        /// Updates an existing milestone in an issue tracker. 
        /// You must supply a name value in the form of a string. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation.
        /// </summary>
        /// <param name="milestone">The milestone you wish to update.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone PutMilestone(Milestone milestone){
            return _repositoriesEndPointV1.PutMilestone(milestone);
        }

        /// <summary>
        /// Deletes a milestone in an issue tracker. Keep in mind that the milestone can be in use on existing issues. 
        /// To delete a milestone, public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="milestone">The milestone that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone DeleteMilestone(Milestone milestone){
            return _repositoriesEndPointV1.DeleteMilestone(milestone);
        }

        /// <summary>
        /// Deletes a milestone in an issue tracker. Keep in mind that the milestone can be in use on existing issues. 
        /// To delete a milestone, public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="milestoneId">The Id of the milestone that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone DeleteMilestone(int? milestoneId){
            return _repositoriesEndPointV1.DeleteMilestone(milestoneId);
        }
    }
}