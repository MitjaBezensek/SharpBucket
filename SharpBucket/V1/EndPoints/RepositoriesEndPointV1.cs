using System;
using System.Collections.Generic;
using SharpBucket.V1.Pocos;
using Version = SharpBucket.V1.Pocos.Version;

namespace SharpBucket.V1.EndPoints{
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

        public IssuesEndPoint Issues(){
            return new IssuesEndPoint(this);
        }

        public IssuesInfo ListIssues(){
            return _sharpBucketV1.Get(new IssuesInfo(), _issuesUrl);
        }

        public Issue PostIssue(Issue issue){
            return _sharpBucketV1.Post(issue, _issuesUrl);
        }

        public Issue GetIssue(Issue issue){
            var overrideUrl = String.Format(_issuesIdUrl, issue.local_id);
            return _sharpBucketV1.Get(issue, overrideUrl);
        }

        public Issue GetIssue(int? issueId){
            return GetIssue(new Issue{local_id = issueId});
        }

        public Issue PutIssue(Issue issue){
            var overrideUrl = String.Format(_issuesIdUrl, issue.local_id);
            return _sharpBucketV1.Put(issue, overrideUrl);
        }

        public Issue DeleteIssue(Issue issue){
            var overrideUrl = String.Format(_issuesIdUrl, issue.local_id);
            return _sharpBucketV1.Delete(issue, overrideUrl);
        }

        public Issue DeleteIssue(int? issueId){
            return DeleteIssue(new Issue{local_id = issueId});
        }

        public IssueFollowers ListIssueFollowers(Issue issue){
            var overrideUrl = String.Format(_issuesIdUrl + "followers", issue.local_id);
            return _sharpBucketV1.Get(new IssueFollowers(), overrideUrl);
        }

        public IssueFollowers ListIssueFollowers(int? issueId){
            return ListIssueFollowers(new Issue{local_id = issueId});
        }

        public List<Comment> ListIssueComments(Issue issue){
            var overrideUrl = String.Format(_issuesIdUrl + "comments", issue.local_id);
            return _sharpBucketV1.Get(new List<Comment>(), overrideUrl);
        }

        public List<Comment> ListIssueComments(int issueId){
            return ListIssueComments(new Issue{local_id = issueId});
        }

        public Comment PostIssueComment(Issue issue, Comment comment){
            var overrideUrl = String.Format(_issuesIdUrl + "comments", issue.local_id);
            return _sharpBucketV1.Post(comment, overrideUrl);
        }

        public Comment PostIssueComment(int issueId, Comment comment){
            return PostIssueComment(new Issue{local_id = issueId}, comment);
        }

        public Comment GetIssueComment(Issue issue, int? commentId){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issue.local_id, commentId);
            return _sharpBucketV1.Get(new Comment{comment_id = commentId}, overrideUrl);
        }

        public Comment GetIssueComment(int issueId, Comment comment){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issueId, comment.comment_id);
            return _sharpBucketV1.Get(comment, overrideUrl);
        }

        public Comment GetIssueComment(int issueId, int? commentId){
            return GetIssueComment(issueId, new Comment{comment_id = commentId});
        }

        public Comment PutIssueComment(Issue issue, Comment comment){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issue.local_id, comment.comment_id);
            return _sharpBucketV1.Put(comment, overrideUrl);
        }

        public Comment PutIssueComment(int issueId, Comment comment){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issueId, comment.comment_id);
            return _sharpBucketV1.Put(comment, overrideUrl);
        }

        public Comment DeleteIssueComment(Issue isssue, Comment comment){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", isssue.local_id, comment.comment_id);
            return _sharpBucketV1.Delete(comment, overrideUrl);
        }

        public Comment DeleteIssueComment(Issue isssue, int? commentId){
            return DeleteIssueComment(isssue, new Comment{comment_id = commentId});
        }

        public Comment DeleteIssueComment(int? issueId, Comment comment){
            return DeleteIssueComment(new Issue{local_id = issueId}, comment);
        }

        public Comment DeleteIssueComment(int? issueId, int? commentId){
            return DeleteIssueComment(issueId, new Comment{comment_id = commentId});
        }

        public List<Component> ListComponents(){
            var overrideUrl = _issuesUrl + "components/";
            return _sharpBucketV1.Get(new List<Component>(), overrideUrl);
        }

        public Component PostComponent(Component component){
            var overrideUrl = _issuesUrl + "components/";
            return _sharpBucketV1.Post(component, overrideUrl);
        }

        public Component GetComponent(Component component){
            var overrideUrl = _issuesUrl + "components/" + component.id;
            return _sharpBucketV1.Get(component, overrideUrl);
        }

        public Component GetComponent(int? componentId){
            return GetComponent(new Component{id = componentId});
        }

        public Component PutComponent(Component component){
            var overrideUrl = _issuesUrl + "components/" + component.id;
            return _sharpBucketV1.Put(component, overrideUrl);
        }

        public Component DeleteComponent(Component component){
            var overrideUrl = _issuesUrl + "components/" + component.id;
            return _sharpBucketV1.Delete(component, overrideUrl);
        }

        public Component DeleteComponent(int? componentId){
            return DeleteComponent(new Component{id = componentId});
        }

        public List<Milestone> ListMilestones(){
            var overrideUrl = _issuesUrl + "milestones/";
            return _sharpBucketV1.Get(new List<Milestone>(), overrideUrl);
        }

        public Milestone PostMilestone(Milestone milestone){
            var overrideUrl = _issuesUrl + "milestones/";
            return _sharpBucketV1.Post(milestone, overrideUrl);
        }

        public Milestone GetMilestone(Milestone milestone){
            var overrideUrl = _issuesUrl + "milestones/" + milestone.id;
            return _sharpBucketV1.Get(milestone, overrideUrl);
        }

        public Milestone GetMilestone(int? milestoneId){
            return GetMilestone(new Milestone{id = milestoneId});
        }

        public Milestone DeleteMilestone(Milestone milestone){
            var overrideUrl = _issuesUrl + "milestones/" + milestone.id;
            return _sharpBucketV1.Delete(milestone, overrideUrl);
        }

        public Milestone DeleteMilestone(int? milestoneId){
            return DeleteMilestone(new Milestone{id = milestoneId});
        }

        public Milestone PutMilestone(Milestone milestone){
            var overrideUrl = _issuesUrl + "milestones/" + milestone.id;
            return _sharpBucketV1.Put(milestone, overrideUrl);
        }

        public List<Version> ListVersions(){
            var overrideUrl = _issuesUrl + "versions/";
            return _sharpBucketV1.Get(new List<Version>(), overrideUrl);
        }

        public Version PostVersion(Version version){
            var overrideUrl = _issuesUrl + "versions/";
            return _sharpBucketV1.Post(version, overrideUrl);
        }

        public Version GetVersion(Version version){
            var overrideUrl = _issuesUrl + "versions/" + version.id;
            return _sharpBucketV1.Get(version, overrideUrl);
        }

        public Version GetVersion(int? versionId){
            return GetVersion(new Version{id = versionId});
        }

        public Version PutVersion(Version version){
            var overrideUrl = _issuesUrl + "versions/" + version.id;
            return _sharpBucketV1.Put(version, overrideUrl);
        }

        public Version DeleteVersion(Version version){
            var overrideUrl = _issuesUrl + "versions/" + version.id;
            return _sharpBucketV1.Delete(version, overrideUrl);
        }

        public Version DeleteVersion(int? versionId){
            return DeleteVersion(new Version{id = versionId});
        }

        public IDictionary<string, Tag> ListTags(){
            var overrideUrl = _baserUrl + "tags/";
            return _sharpBucketV1.Get(new Dictionary<string, Tag>(), overrideUrl);
        }

        public Dictionary<string, BranchInfo> ListBranches(){
            var overrideUrl = _baserUrl + "branches/";
            return _sharpBucketV1.Get(new Dictionary<string, BranchInfo>(), overrideUrl);
        }

        public MainBranch GetMainBranch(){
            var overrideUrl = _baserUrl + "main-branch/";
            return _sharpBucketV1.Get(new MainBranch(), overrideUrl);
        }

        public Wiki GetWiki(string page){
            var overrideUrl = _baserUrl + "wiki/" + page;
            return _sharpBucketV1.Get(new Wiki(), overrideUrl);
        }

        // TODO:  Doesnt work, 500 server error, same for put
        public Wiki PostWiki(Wiki newPage, string location){
            var overrideUrl = _baserUrl + "wiki/" + location;
            return _sharpBucketV1.Post(newPage, overrideUrl);
        }

        public Wiki PutWiki(Wiki newPage, string location){
            var overrideUrl = _baserUrl + "wiki/" + location;
            return _sharpBucketV1.Put(newPage, overrideUrl);
        }

        public ChangesetInfo ListChangeset(){
            var overrideUrl = _baserUrl + "changesets/";
            return _sharpBucketV1.Get(new ChangesetInfo(), overrideUrl);
        }

        public Changeset GetChangeset(Changeset changeset){
            var overrideUrl = _baserUrl + "changesets/" + changeset.node;
            return _sharpBucketV1.Get(changeset, overrideUrl);
        }

        public Changeset GetChangeset(string node){
            return GetChangeset(new Changeset{node = node});
        }

        public List<DiffstatInfo> GetChangesetDiffstat(Changeset changeset){
            var overrideUrl = _baserUrl + "changesets/" + changeset.node + "/diffstat/";
            return _sharpBucketV1.Get(new List<DiffstatInfo>(), overrideUrl);
        }

        public List<DiffstatInfo> GetChangesetDiffstat(string node){
            return GetChangesetDiffstat(new Changeset{node = node});
        }

        public Changeset GetChangesetDiff(Changeset changeset){
            var overrideUrl = _baserUrl + "changesets/" + changeset.node + "/diff/";
            return _sharpBucketV1.Get(changeset, overrideUrl);
        }

        public Changeset GetChangesetDiff(string node){
            return GetChangesetDiff(new Changeset{node = node});
        }

        public EventInfo ListEvents(){
            var overrideUrl = _baserUrl + "events/";
            return _sharpBucketV1.Get(new EventInfo(), overrideUrl);
        }

        public List<Link> ListLinks(){
            var overrideUrl = _baserUrl + "links/";
            return _sharpBucketV1.Get(new List<Link>(), overrideUrl);
        }

        public Link PostLink(Link link){
            var overrideUrl = _baserUrl + "links/";
            return _sharpBucketV1.Post(link, overrideUrl);
        }

        public Link GetLink(int? linkId){
            var overrideUrl = _baserUrl + "links/" + linkId + "/";
            return _sharpBucketV1.Get(new Link(), overrideUrl);
        }

        public Link PutLink(Link link){
            var overrideUrl = _baserUrl + "links/" + link.id + "/";
            return _sharpBucketV1.Put(link, overrideUrl);
        }

        public Link DeleteLink(Link link){
            var overrideUrl = _baserUrl + "links/" + link.id + "/";
            return _sharpBucketV1.Delete(link, overrideUrl);
        }

        public List<SSH> ListDeployKeys(){
            var overrideUrl = _baserUrl + "deploy-keys/";
            return _sharpBucketV1.Get(new List<SSH>(), overrideUrl);
        }

        public SSH PostDeployKey(SSH key){
            var overrideUrl = _baserUrl + "deploy-keys/";
            return _sharpBucketV1.Post(key, overrideUrl);
        }

        public SSH GetDeployKey(int? pk){
            var overrideUrl = _baserUrl + "deploy-keys/" + pk + "/";
            return _sharpBucketV1.Get(new SSH(), overrideUrl);
        }

        public SSH DeleteDeployKey(SSH key){
            var overrideUrl = _baserUrl + "deploy-keys/" + key.pk + "/";
            return _sharpBucketV1.Delete(key, overrideUrl);
        }
    }
}