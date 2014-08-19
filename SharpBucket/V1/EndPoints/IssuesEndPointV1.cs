using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints{
    /// <summary>
    /// A "Virtual" End Point that offers easier manipulation of issues of a repository.
    /// </summary>
    public class IssuesEndPointV1{
        private readonly RepositoriesEndPointV1 _repositoriesEndPointV1;

        /// <summary>
        /// Get the Issue End Point.
        /// BitBucket does not have this End Point so this is a "Virtual" end point
        /// which offers easier access to issue specific information.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose End Point you wish to get.</param>
        /// <returns></returns>
        public IssueEndPointV1 Issue(int issueId){
            return new IssueEndPointV1(_repositoriesEndPointV1, issueId);
        }

        public IssuesEndPointV1(RepositoriesEndPointV1 repositoriesEndPointV1){
            _repositoriesEndPointV1 = repositoriesEndPointV1;
        }

        /// <summary>
        /// List all the issues for the current repository.
        /// </summary>
        /// <returns></returns>
        public IssuesInfo ListIssues(){
            return _repositoriesEndPointV1.ListIssues();
        }

        /// <summary>
        /// Post a new issue to the current repository.
        /// </summary>
        /// <param name="issue">The issue that you wish to post.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Issue PostIssue(Issue issue){
            return _repositoriesEndPointV1.PostIssue(issue);
        }

        /// <summary>
        /// Get a specific issue from the current repository.
        /// </summary>
        /// <param name="issueId">The Id of the issue you wish to get.</param>
        /// <returns></returns>
        public Issue GetIssue(int? issueId){
            return _repositoriesEndPointV1.GetIssue(issueId);
        }

        /// <summary>
        /// Update an issue of the current repository.
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
        /// Delete and issue from the current repository.
        /// </summary>
        /// <param name="issueId">The Id of the issue that you wish to delete.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Issue DeleteIssue(int? issueId){
            return _repositoriesEndPointV1.DeleteIssue(issueId);
        }

        /// <summary>
        /// List all the followers of the selected issue.
        /// </summary>
        /// <param name="issue">The issue whose followers you wish to get.</param>
        /// <returns></returns>
        public IssueFollowers ListIssueFollowers(Issue issue){
            return _repositoriesEndPointV1.ListIssueFollowers(issue);
        }

        /// <summary>
        /// List all the followers of the selected issue.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose followers you wish to get.</param>
        /// <returns></returns>
        public IssueFollowers ListIssueFollowers(int? issueId){
            return _repositoriesEndPointV1.ListIssueFollowers(issueId);
        }

        /// <summary>
        /// List all the comments for the selected issue.
        /// </summary>
        /// <param name="issue">The issue whose comments you wish to get.</param>
        /// <returns></returns>
        public List<Comment> ListIssueComments(Issue issue){
            return _repositoriesEndPointV1.ListIssueComments(issue);
        }

        /// <summary>
        /// List all the comments for the selected issue.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose comments you wish to get.</param>
        /// <returns></returns>
        public List<Comment> ListIssueComments(int issueId){
            return _repositoriesEndPointV1.ListIssueComments(issueId);
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
        /// <param name="issueId">The Id of the issue to which you wish to post a comment.</param>
        /// <param name="comment">The comment you wish to post.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Comment PostIssueComment(int issueId, Comment comment){
            return _repositoriesEndPointV1.PostIssueComment(issueId, comment);
        }

        /// <summary>
        /// Get a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue whose comment you wish to get.</param>
        /// <param name="commentId">The id of the comment you wish to get.</param>
        /// <returns></returns>
        public Comment GetIssueComment(Issue issue, int? commentId){
            return _repositoriesEndPointV1.GetIssueComment(issue, commentId);
        }

        /// <summary>
        /// Get a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose comment you wish to get.</param>
        /// <param name="commentId">The id of the comment you wish to get.</param>
        /// <returns></returns>
        public Comment GetIssueComment(int issueId, int? commentId){
            return _repositoriesEndPointV1.GetIssueComment(issueId, commentId);
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
        /// <param name="issueId">The Id of the issue whose comment you wish to update.</param>
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
        /// <param name="commentId">The Id of the comment you wish to delete.</param>
        /// <returns>The response of BitBucket API.</returns>
        public Comment DeleteIssueComment(Issue issue, int? commentId){
            return _repositoriesEndPointV1.DeleteIssueComment(issue, commentId);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose comment you wish to delete.</param>
        /// <param name="commentId">The Id of the comment you wish to delete.</param>
        /// <returns>The response of BitBucket API.</returns>
        public Comment DeleteIssueComment(int? issueId, int? commentId){
            return _repositoriesEndPointV1.DeleteIssueComment(issueId, commentId);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose comment you wish to delete.</param>
        /// <param name="comment">The comment that you wish to delete.</param>
        /// <returns>The response of BitBucket API.</returns>
        public Comment DeleteIssueComment(int? issueId, Comment comment){
            return _repositoriesEndPointV1.DeleteIssueComment(issueId, comment);
        }

        /// <summary>
        /// List the components of the current repository.
        /// </summary>
        /// <returns></returns>
        public List<Component> ListComponents(){
            return _repositoriesEndPointV1.ListComponents();
        }

        /// <summary>
        /// Post a new component to the current repository.
        /// </summary>
        /// <param name="component"></param>
        /// <returns>The response from the BitBucket API.</returns>
        public Component PostComponent(Component component){
            return _repositoriesEndPointV1.PostComponent(component);
        }

        /// <summary>
        /// Get a specific component of the current repository.
        /// </summary>
        /// <param name="componentId">The Id of the component you wish to get.</param>
        /// <returns></returns>
        public Component GetComponent(int? componentId){
            return _repositoriesEndPointV1.GetComponent(componentId);
        }

        /// <summary>
        /// Update a specific component.
        /// </summary>
        /// <param name="component">The component that you wish to update.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Component PutComponent(Component component){
            return _repositoriesEndPointV1.PutComponent(component);
        }

        /// <summary>
        /// Delete a specific component.
        /// </summary>
        /// <param name="component">The component that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Component DeleteComponent(Component component){
            return _repositoriesEndPointV1.DeleteComponent(component);
        }

        /// <summary>
        /// Delete a specific component.
        /// </summary>
        /// <param name="componentId">The Id of the component that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Component DeleteComponent(int? componentId){
            return _repositoriesEndPointV1.DeleteComponent(componentId);
        }

        /// <summary>
        /// List all the milestones of the current repository.
        /// </summary>
        /// <returns></returns>
        public List<Milestone> ListMilestones(){
            return _repositoriesEndPointV1.ListMilestones();
        }

        /// <summary>
        /// Add a new milestone to the current repository.
        /// </summary>
        /// <param name="milestone">The milestone that you wish to add.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone PostMilestone(Milestone milestone){
            return _repositoriesEndPointV1.PostMilestone(milestone);
        }

        /// <summary>
        /// Get a specific milestone of the current repositotry.
        /// </summary>
        /// <param name="milestoneId">The Id of the milestone you wish to get.</param>
        /// <returns></returns>
        public Milestone GetMilestone(int? milestoneId){
            return _repositoriesEndPointV1.GetMilestone(milestoneId);
        }

        /// <summary>
        /// Update a specific milestone of the current repository.
        /// </summary>
        /// <param name="milestone">The milestone you wish to update.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone PutMilestone(Milestone milestone){
            return _repositoriesEndPointV1.PutMilestone(milestone);
        }

        /// <summary>
        /// Delete a specific milestone of the current repository.
        /// </summary>
        /// <param name="milestone">The milestone that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone DeleteMilestone(Milestone milestone){
            return _repositoriesEndPointV1.DeleteMilestone(milestone);
        }

        /// <summary>
        /// Delete a specific milestone of the current repository.
        /// </summary>
        /// <param name="milestoneId">The Id of the milestone that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone DeleteMilestone(int? milestoneId){
            return _repositoriesEndPointV1.DeleteMilestone(milestoneId);
        }

        /// <summary>
        /// List all the versions for the current repository.
        /// </summary>
        /// <returns></returns>
        public List<Version> ListVersions(){
            return _repositoriesEndPointV1.ListVersions();
        }

        /// <summary>
        /// Add a new version to the current repository.
        /// </summary>
        /// <param name="version">The version you wish to add.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version PostVersion(Version version){
            return _repositoriesEndPointV1.PostVersion(version);
        }

        /// <summary>
        /// Get a specific version of the current repository.
        /// </summary>
        /// <param name="versionId">The Id of the version you wish to get.</param>
        /// <returns></returns>
        public Version GetVersion(int? versionId){
            return _repositoriesEndPointV1.GetVersion(versionId);
        }

        /// <summary>
        /// Update a specific version.
        /// </summary>
        /// <param name="version">The version you wish to update.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version PutVersion(Version version){
            return _repositoriesEndPointV1.PutVersion(version);
        }

        /// <summary>
        /// Delete a specific version.
        /// </summary>
        /// <param name="version">The version that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version DeleteVersion(Version version){
            return _repositoriesEndPointV1.DeleteVersion(version);
        }

        /// <summary>
        /// Delete a specific version.
        /// </summary>
        /// <param name="versionId">The Id of the version that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version DeleteVersion(int? versionId){
            return _repositoriesEndPointV1.DeleteVersion(versionId);
        }
    }
}