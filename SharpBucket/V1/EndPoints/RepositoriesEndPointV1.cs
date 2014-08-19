using System;
using System.Collections.Generic;
using SharpBucket.V1.Pocos;
using Version = SharpBucket.V1.Pocos.Version;

namespace SharpBucket.V1.EndPoints{
    /// <summary>
    /// The Repositories End Point is your main End Point for getting information of the
    /// specified repository. This includes information like: issues, issue comments, commits,...
    /// This end point contains access to a few other end points, like Issues End point.
    /// More info here:
    /// https://confluence.atlassian.com/display/BITBUCKET/repositories+Endpoint+-+1.0
    /// </summary>
    public class RepositoriesEndPointV1{
        private readonly SharpBucketV1 _sharpBucketV1;
        private readonly string _baserUrl;
        private readonly string _issuesUrl;
        private readonly string _issuesIdUrl;

        public RepositoriesEndPointV1(string accountName, string repository, SharpBucketV1 sharpBucketV1){
            _sharpBucketV1 = sharpBucketV1;
            _baserUrl = "repositories/" + accountName + "/" + repository + "/";
            _issuesUrl = _baserUrl + "issues/";
            _issuesIdUrl = _issuesUrl + "{0}/";
        }

        /// <summary>
        /// Get the Issues End Point.
        /// BitBucket does not have this End Point so this is a "Virtual" end point
        /// which offers easier manipulation of issues.
        /// </summary>
        /// <returns></returns>
        public IssuesEndPointV1 Issues(){
            return new IssuesEndPointV1(this);
        }

        /// <summary>
        /// List he issues of the current repository.
        /// </summary>
        /// <returns></returns>
        public IssuesInfo ListIssues(){
            return _sharpBucketV1.Get(new IssuesInfo(), _issuesUrl);
        }

        /// <summary>
        /// Add an issue to the current repository.
        /// </summary>
        /// <param name="issue">The issue that you wish to add.</param>
        /// <returns></returns>
        public Issue PostIssue(Issue issue){
            return _sharpBucketV1.Post(issue, _issuesUrl);
        }

        /// <summary>
        /// Get a specific issue of the repository.
        /// </summary>
        /// <param name="issue">The issue that you wish to get.</param>
        /// <returns></returns>
        public Issue GetIssue(Issue issue){
            var overrideUrl = String.Format(_issuesIdUrl, issue.local_id);
            return _sharpBucketV1.Get(issue, overrideUrl);
        }

        /// <summary>
        /// Get a specific issue of the current repository.
        /// </summary>
        /// <param name="issueId">The Id of the issue you wish to get.</param>
        /// <returns></returns>
        public Issue GetIssue(int? issueId){
            return GetIssue(new Issue{local_id = issueId});
        }

        /// <summary>
        /// Update a specific issue of the current repository.
        /// </summary>
        /// <param name="issue">The issue that you wish to update.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Issue PutIssue(Issue issue){
            var overrideUrl = String.Format(_issuesIdUrl, issue.local_id);
            return _sharpBucketV1.Put(issue, overrideUrl);
        }

        /// <summary>
        /// Delete a specific issue of the current repository.
        /// </summary>
        /// <param name="issue">The issue that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Issue DeleteIssue(Issue issue){
            var overrideUrl = String.Format(_issuesIdUrl, issue.local_id);
            return _sharpBucketV1.Delete(issue, overrideUrl);
        }

        /// <summary>
        /// Delete a specific issue of the current repository.
        /// </summary>
        /// <param name="issueId">The Id of the issue that you wish to delete.</param>
        /// <returns></returns>
        public Issue DeleteIssue(int? issueId){
            return DeleteIssue(new Issue{local_id = issueId});
        }

        /// <summary>
        /// List the followers of the selected issue of the current repository.
        /// </summary>
        /// <param name="issue">The issue whose followers you wish to get.</param>
        /// <returns></returns>
        public IssueFollowers ListIssueFollowers(Issue issue){
            var overrideUrl = String.Format(_issuesIdUrl + "followers", issue.local_id);
            return _sharpBucketV1.Get(new IssueFollowers(), overrideUrl);
        }

        /// <summary>
        /// List the followers of the selected issue of the current repository.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose followers you wish to get.</param>
        /// <returns></returns>
        public IssueFollowers ListIssueFollowers(int? issueId){
            return ListIssueFollowers(new Issue{local_id = issueId});
        }

        /// <summary>
        /// List the comments of the selected issue of the current repository.
        /// </summary>
        /// <param name="issue">The issue whose comments you wish to get.</param>
        /// <returns></returns>
        public List<Comment> ListIssueComments(Issue issue){
            var overrideUrl = String.Format(_issuesIdUrl + "comments", issue.local_id);
            return _sharpBucketV1.Get(new List<Comment>(), overrideUrl);
        }

        /// <summary>
        /// List the comments of the selected issue of the current repository.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose comments you wish to get.</param>
        /// <returns></returns>
        public List<Comment> ListIssueComments(int issueId){
            return ListIssueComments(new Issue{local_id = issueId});
        }

        /// <summary>
        /// Add a comment to the selected issue of the current repository.
        /// </summary>
        /// <param name="issue">The issue to which you wish to add the comment.</param>
        /// <param name="comment">The comment that you wish to add.</param>
        /// <returns></returns>
        public Comment PostIssueComment(Issue issue, Comment comment){
            var overrideUrl = String.Format(_issuesIdUrl + "comments", issue.local_id);
            return _sharpBucketV1.Post(comment, overrideUrl);
        }

        /// <summary>
        /// Add a comment to the selected issue of the current repository.
        /// </summary>
        /// <param name="issueId">The Id of the issue to which you wish to add the comment.</param>
        /// <param name="comment">The comment that you wish to add.</param>
        /// <returns></returns>
        public Comment PostIssueComment(int issueId, Comment comment){
            return PostIssueComment(new Issue{local_id = issueId}, comment);
        }

        /// <summary>
        /// Get the comments of the selected issue of the current repository.
        /// </summary>
        /// <param name="issue">The issue whose comment you wish to get.</param>
        /// <param name="commentId">The Id of the comment you wish to get.</param>
        /// <returns></returns>
        public Comment GetIssueComment(Issue issue, int? commentId){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issue.local_id, commentId);
            return _sharpBucketV1.Get(new Comment{comment_id = commentId}, overrideUrl);
        }

        /// <summary>
        /// Get the comments of the selected issue of the current repository.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose comment you wish to get.</param>
        /// <param name="comment">The comment you wish to get.</param>
        /// <returns></returns>
        public Comment GetIssueComment(int issueId, Comment comment){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issueId, comment.comment_id);
            return _sharpBucketV1.Get(comment, overrideUrl);
        }

        /// <summary>
        /// Get the comments of the selected issue of the current repository.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose comment you wish to get.</param>
        /// <param name="commentId">The Id of the comment you wish to get.</param>
        /// <returns></returns>
        public Comment GetIssueComment(int issueId, int? commentId){
            return GetIssueComment(issueId, new Comment{comment_id = commentId});
        }

        /// <summary>
        /// Update a comment of the selected issue of the current repository.
        /// </summary>
        /// <param name="issue">The issue whose comment you wish to update.</param>
        /// <param name="comment">The comment that you wish to update.</param>
        /// <returns></returns>
        public Comment PutIssueComment(Issue issue, Comment comment){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issue.local_id, comment.comment_id);
            return _sharpBucketV1.Put(comment, overrideUrl);
        }

        /// <summary>
        /// Update a comment of the selected issue of the current repository.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose comment you wish to update.</param>
        /// <param name="comment">The comment that you wish to update.</param>
        /// <returns></returns>
        public Comment PutIssueComment(int issueId, Comment comment){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issueId, comment.comment_id);
            return _sharpBucketV1.Put(comment, overrideUrl);
        }

        /// <summary>
        /// Delete a comment of the selected issue of the current repository.
        /// </summary>
        /// <param name="issue">The issue whose comment you wish to delete.</param>
        /// <param name="comment">The comment that you wish to delte.</param>
        /// <returns></returns>
        public Comment DeleteIssueComment(Issue issue, Comment comment){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issue.local_id, comment.comment_id);
            return _sharpBucketV1.Delete(comment, overrideUrl);
        }

        /// <summary>
        /// Delete a comment of the selected issue of the current repository.
        /// </summary>
        /// <param name="issue">The issue whose comment you wish to delete.</param>
        /// <param name="commentId">The Id of the comment that you wish to delte.</param>
        /// <returns></returns>
        public Comment DeleteIssueComment(Issue issue, int? commentId){
            return DeleteIssueComment(issue, new Comment{comment_id = commentId});
        }

        /// <summary>
        /// Delete a comment of the selected issue of the current repository.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose comment you wish to delete.</param>
        /// <param name="comment">The comment that you wish to delte.</param>
        /// <returns></returns>
        public Comment DeleteIssueComment(int? issueId, Comment comment){
            return DeleteIssueComment(new Issue{local_id = issueId}, comment);
        }

        /// <summary>
        /// Delete a comment of the selected issue of the current repository.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose comment you wish to delete.</param>
        /// <param name="commentId">The Id of the comment that you wish to delte.</param>
        /// <returns></returns>
        public Comment DeleteIssueComment(int? issueId, int? commentId){
            return DeleteIssueComment(issueId, new Comment{comment_id = commentId});
        }

        /// <summary>
        /// List he components of the current repository.
        /// </summary>
        /// <returns></returns>
        public List<Component> ListComponents(){
            var overrideUrl = _issuesUrl + "components/";
            return _sharpBucketV1.Get(new List<Component>(), overrideUrl);
        }

        /// <summary>
        /// Add a component to the current repository.
        /// </summary>
        /// <param name="component">The component that you wish to add.</param>
        /// <returns></returns>
        public Component PostComponent(Component component){
            var overrideUrl = _issuesUrl + "components/";
            return _sharpBucketV1.Post(component, overrideUrl);
        }

        /// <summary>
        /// Get a specific component of the current repository.
        /// </summary>
        /// <param name="component">The component that you wish to get.</param>
        /// <returns></returns>
        public Component GetComponent(Component component){
            var overrideUrl = _issuesUrl + "components/" + component.id;
            return _sharpBucketV1.Get(component, overrideUrl);
        }

        /// <summary>
        /// Get a specific component of the current repository.
        /// </summary>
        /// <param name="componentId">The Id of the component that you wish to get.</param>
        /// <returns></returns>
        public Component GetComponent(int? componentId){
            return GetComponent(new Component{id = componentId});
        }

        /// <summary>
        /// Update a specific component of the current repository.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public Component PutComponent(Component component){
            var overrideUrl = _issuesUrl + "components/" + component.id;
            return _sharpBucketV1.Put(component, overrideUrl);
        }

        /// <summary>
        /// Delete a specific component of the current repository.
        /// </summary>
        /// <param name="component">The component that you wish to delete.</param>
        /// <returns></returns>
        public Component DeleteComponent(Component component){
            var overrideUrl = _issuesUrl + "components/" + component.id;
            return _sharpBucketV1.Delete(component, overrideUrl);
        }

        /// <summary>
        /// Delete a specific component of the current repository.
        /// </summary>
        /// <param name="componentId">The Id of the component that you wish to delete.</param>
        /// <returns></returns>
        public Component DeleteComponent(int? componentId){
            return DeleteComponent(new Component{id = componentId});
        }

        /// <summary>
        /// List the milestones of the current repository.
        /// </summary>
        /// <returns></returns>
        public List<Milestone> ListMilestones(){
            var overrideUrl = _issuesUrl + "milestones/";
            return _sharpBucketV1.Get(new List<Milestone>(), overrideUrl);
        }

        /// <summary>
        /// Add a new milestone to the current repository.
        /// </summary>
        /// <param name="milestone">The milestone that you wish to add.</param>
        /// <returns></returns>
        public Milestone PostMilestone(Milestone milestone){
            var overrideUrl = _issuesUrl + "milestones/";
            return _sharpBucketV1.Post(milestone, overrideUrl);
        }

        /// <summary>
        /// Get a specific milestone of the current repository.
        /// </summary>
        /// <param name="milestone"></param>
        /// <returns></returns>
        public Milestone GetMilestone(Milestone milestone){
            var overrideUrl = _issuesUrl + "milestones/" + milestone.id;
            return _sharpBucketV1.Get(milestone, overrideUrl);
        }

        /// <summary>
        /// Get a specific milestone of the current repository.
        /// </summary>
        /// <param name="milestoneId">The Id of the milestone you wish to get.</param>
        /// <returns></returns>
        public Milestone GetMilestone(int? milestoneId){
            return GetMilestone(new Milestone{id = milestoneId});
        }

        /// <summary>
        /// Delete a specific milestone of the current repository.
        /// </summary>
        /// <param name="milestone">The milestone that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone DeleteMilestone(Milestone milestone){
            var overrideUrl = _issuesUrl + "milestones/" + milestone.id;
            return _sharpBucketV1.Delete(milestone, overrideUrl);
        }

        /// <summary>
        /// Delete a specific milestone of the current repository.
        /// </summary>
        /// <param name="milestoneId">The Id of the milestone that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone DeleteMilestone(int? milestoneId){
            return DeleteMilestone(new Milestone{id = milestoneId});
        }

        /// <summary>
        /// Update a specific milestone of the current repository.
        /// </summary>
        /// <param name="milestone">The milestone you wish to update.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Milestone PutMilestone(Milestone milestone){
            var overrideUrl = _issuesUrl + "milestones/" + milestone.id;
            return _sharpBucketV1.Put(milestone, overrideUrl);
        }

        /// <summary>
        /// List all the versions for the current repository.
        /// </summary>
        /// <returns></returns>
        public List<Version> ListVersions(){
            var overrideUrl = _issuesUrl + "versions/";
            return _sharpBucketV1.Get(new List<Version>(), overrideUrl);
        }

        /// <summary>
        /// Add a new version to the current repository.
        /// </summary>
        /// <param name="version">The version you wish to add.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version PostVersion(Version version){
            var overrideUrl = _issuesUrl + "versions/";
            return _sharpBucketV1.Post(version, overrideUrl);
        }

        /// <summary>
        /// Get a specific version of the current repository.
        /// </summary>
        /// <param name="versionId">The Id of the version you wish to get.</param>
        /// <returns></returns>
        public Version GetVersion(Version version){
            var overrideUrl = _issuesUrl + "versions/" + version.id;
            return _sharpBucketV1.Get(version, overrideUrl);
        }

        /// <summary>
        /// Get a specific version of the current repository.
        /// </summary>
        /// <param name="versionId">The Id of the version you wish to get.</param>
        /// <returns></returns>
        public Version GetVersion(int? versionId){
            return GetVersion(new Version{id = versionId});
        }

        /// <summary>
        /// Update a specific version.
        /// </summary>
        /// <param name="version">The version you wish to update.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version PutVersion(Version version){
            var overrideUrl = _issuesUrl + "versions/" + version.id;
            return _sharpBucketV1.Put(version, overrideUrl);
        }

        /// <summary>
        /// Delete a specific version.
        /// </summary>
        /// <param name="version">The version that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version DeleteVersion(Version version){
            var overrideUrl = _issuesUrl + "versions/" + version.id;
            return _sharpBucketV1.Delete(version, overrideUrl);
        }

        /// <summary>
        /// Delete a specific version.
        /// </summary>
        /// <param name="version">The version that you wish to delete.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public Version DeleteVersion(int? versionId){
            return DeleteVersion(new Version{id = versionId});
        }

        /// <summary>
        /// List all the tags of the current repository.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, Tag> ListTags(){
            var overrideUrl = _baserUrl + "tags/";
            return _sharpBucketV1.Get(new Dictionary<string, Tag>(), overrideUrl);
        }

        /// <summary>
        /// List all the branches of the current repository.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, BranchInfo> ListBranches(){
            var overrideUrl = _baserUrl + "branches/";
            return _sharpBucketV1.Get(new Dictionary<string, BranchInfo>(), overrideUrl);
        }

        /// <summary>
        /// Get the main branch of the current repository.
        /// </summary>
        /// <returns></returns>
        public MainBranch GetMainBranch(){
            var overrideUrl = _baserUrl + "main-branch/";
            return _sharpBucketV1.Get(new MainBranch(), overrideUrl);
        }

        /// <summary>
        /// Get a specific wiki page.
        /// </summary>
        /// <param name="page">The page that you wish to get.</param>
        /// <returns></returns>
        public Wiki GetWiki(string page){
            var overrideUrl = _baserUrl + "wiki/" + page;
            return _sharpBucketV1.Get(new Wiki(), overrideUrl);
        }

        // TODO:  Doesnt work, 500 server error, same for put
        /// <summary>
        /// Add a new wiki page.
        /// </summary>
        /// <param name="newPage">The page that you wish to add.</param>
        /// <param name="location">The location of the page.</param>
        /// <returns></returns>
        public Wiki PostWiki(Wiki newPage, string location){
            var overrideUrl = _baserUrl + "wiki/" + location;
            return _sharpBucketV1.Post(newPage, overrideUrl);
        }

        /// <summary>
        /// Update a specific wiki page.
        /// </summary>
        /// <param name="updatedPage">The updated page.</param>
        /// <param name="location">The location of the updated page.</param>
        /// <returns></returns>
        public Wiki PutWiki(Wiki updatedPage, string location){
            var overrideUrl = _baserUrl + "wiki/" + location;
            return _sharpBucketV1.Put(updatedPage, overrideUrl);
        }

        /// <summary>
        /// List of all of the change sets for the current repository.
        /// </summary>
        /// <returns></returns>
        public ChangesetInfo ListChangeset(){
            var overrideUrl = _baserUrl + "changesets/";
            return _sharpBucketV1.Get(new ChangesetInfo(), overrideUrl);
        }

        /// <summary>
        /// Get a specific change set for the current repository.
        /// </summary>
        /// <param name="changeset">The change set that you wish to get</param>
        /// <returns></returns>
        public Changeset GetChangeset(Changeset changeset){
            var overrideUrl = _baserUrl + "changesets/" + changeset.node;
            return _sharpBucketV1.Get(changeset, overrideUrl);
        }

        /// <summary>
        /// Get a specific change set for the current repository.
        /// </summary>
        /// <param name="node">The hash of the change set you wish to get.</param>
        /// <returns></returns>
        public Changeset GetChangeset(string node){
            return GetChangeset(new Changeset{node = node});
        }

        /// <summary>
        /// Get the diff stat of a specific change set.
        /// </summary>
        /// <param name="changeset">The change set whose diff stat you wish to get.</param>
        /// <returns></returns>
        public List<DiffstatInfo> GetChangesetDiffstat(Changeset changeset){
            var overrideUrl = _baserUrl + "changesets/" + changeset.node + "/diffstat/";
            return _sharpBucketV1.Get(new List<DiffstatInfo>(), overrideUrl);
        }

        /// <summary>
        /// Get the diff stat of a specific change set.
        /// </summary>
        /// <param name="changeset">The hash of the change set whose diff stat you wish to get.</param>
        /// <returns></returns>
        public List<DiffstatInfo> GetChangesetDiffstat(string node){
            return GetChangesetDiffstat(new Changeset{node = node});
        }

        /// <summary>
        /// Get the diff of a specific change set.
        /// </summary>
        /// <param name="changeset">The change set whose diff you wish to get.</param>
        /// <returns></returns>
        public Changeset GetChangesetDiff(Changeset changeset){
            var overrideUrl = _baserUrl + "changesets/" + changeset.node + "/diff/";
            return _sharpBucketV1.Get(changeset, overrideUrl);
        }

        /// <summary>
        /// Get the diff of a specific change set.
        /// </summary>
        /// <param name="node">The hash of the change set whose diff you wish to get.</param>
        /// <returns></returns>
        public Changeset GetChangesetDiff(string node){
            return GetChangesetDiff(new Changeset{node = node});
        }

        /// <summary>
        /// List of all the events for the current repository.
        /// </summary>
        /// <returns></returns>
        public EventInfo ListEvents(){
            var overrideUrl = _baserUrl + "events/";
            return _sharpBucketV1.Get(new EventInfo(), overrideUrl);
        }

        /// <summary>
        /// List of all the Links for the current repository.
        /// </summary>
        /// <returns></returns>
        public List<Link> ListLinks(){
            var overrideUrl = _baserUrl + "links/";
            return _sharpBucketV1.Get(new List<Link>(), overrideUrl);
        }

        /// <summary>
        /// Get the information for a specific link of the current repository.
        /// </summary>
        /// <param name="link">The link whose information you wish to get.</param>
        /// <returns></returns>
        public Link PostLink(Link link){
            var overrideUrl = _baserUrl + "links/";
            return _sharpBucketV1.Post(link, overrideUrl);
        }

        /// <summary>
        /// Get the information for a specific link of the current repository.
        /// </summary>
        /// <param name="linkId">The Id of the link whose information you wish to get.</param>
        /// <returns></returns>
        public Link GetLink(int? linkId){
            var overrideUrl = _baserUrl + "links/" + linkId + "/";
            return _sharpBucketV1.Get(new Link(), overrideUrl);
        }

        /// <summary>
        /// Update a link of the current repository.
        /// </summary>
        /// <param name="link">The link that you wish to update.</param>
        /// <returns></returns>
        public Link PutLink(Link link){
            var overrideUrl = _baserUrl + "links/" + link.id + "/";
            return _sharpBucketV1.Put(link, overrideUrl);
        }

        /// <summary>
        /// Delete a link of the current repository.
        /// </summary>
        /// <param name="link">The link that you wish to delete.</param>
        /// <returns></returns>
        public Link DeleteLink(Link link){
            var overrideUrl = _baserUrl + "links/" + link.id + "/";
            return _sharpBucketV1.Delete(link, overrideUrl);
        }

        /// <summary>
        /// List all the deploy keys for the current repository.
        /// </summary>
        /// <returns></returns>
        public List<SSH> ListDeployKeys(){
            var overrideUrl = _baserUrl + "deploy-keys/";
            return _sharpBucketV1.Get(new List<SSH>(), overrideUrl);
        }

        /// <summary>
        /// Add a deploy key to the current repository.
        /// </summary>
        /// <param name="key">The key that you wish to add.</param>
        /// <returns></returns>
        public SSH PostDeployKey(SSH key){
            var overrideUrl = _baserUrl + "deploy-keys/";
            return _sharpBucketV1.Post(key, overrideUrl);
        }

        /// <summary>
        /// Get a specific key of the current repository.
        /// </summary>
        /// <param name="pk">The identifier of the deploy key that you wish to get.</param>
        /// <returns></returns>
        public SSH GetDeployKey(int? pk){
            var overrideUrl = _baserUrl + "deploy-keys/" + pk + "/";
            return _sharpBucketV1.Get(new SSH(), overrideUrl);
        }

        /// <summary>
        /// Delete a specific deploy key of the current repository.
        /// </summary>
        /// <param name="key">The key that you wish to delete</param>
        /// <returns></returns>
        public SSH DeleteDeployKey(SSH key){
            var overrideUrl = _baserUrl + "deploy-keys/" + key.pk + "/";
            return _sharpBucketV1.Delete(key, overrideUrl);
        }
    }
}