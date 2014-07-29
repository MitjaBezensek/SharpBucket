using System;
using System.Collections.Generic;
using System.Linq;
using SharpBucket.POCOs;
using SharpBucket.V1.Routes;
using Version = SharpBucket.POCOs.Version;

namespace SharpBucket.V1.EndPoints{
    public class RepositoryEndPointV1{
        private readonly string _accountName;
        private readonly string _repository;
        private readonly SharpBucketV1 _sharpBucketV1;
        private readonly string _baserUrl;
        private readonly string _issuesUrl;
        private readonly string _issuesIdUrl;

        public RepositoryEndPointV1(string accountName, string repository, SharpBucketV1 sharpBucketV1){
            _accountName = accountName;
            _repository = repository;
            _sharpBucketV1 = sharpBucketV1;
            _baserUrl = "repositories/" + accountName + "/" + repository + "/";
            _issuesUrl = "repositories/" + accountName + "/" + repository + "/issues";
            _issuesIdUrl = "repositories/" + accountName + "/" + repository + "/issues/{0}/";
        }

        public IssuesEndPoint Issues(){
            return new IssuesEndPoint(this);
        }

        public IssuesInfo ListIssues(){
            return _sharpBucketV1.Get(new IssuesRoutes.ListIssues{AccountName = _accountName, RepositorySlug = _repository}, _issuesUrl);
        }

        public Issue GetIssue(int? issueId){
            var overrideUrl = String.Format(_issuesIdUrl, issueId);
            return _sharpBucketV1.Get(new IssuesRoutes.GetIssue{AccountName = _accountName, RepositorySlug = _repository, Local_id = issueId}, overrideUrl);
        }

        public Issue PostIssue(Issue issue){
            var overrideUrl =_issuesUrl;
            return _sharpBucketV1.Post(new IssuesRoutes.PostIssue{AccountName = _accountName, RepositorySlug = _repository, Title = issue.Title, Content = issue.Content, Status = issue.Status}, overrideUrl);
        }

        public Issue DeleteIssue(int? issueId){
            var overrideUrl = String.Format(_issuesIdUrl, issueId);
            return _sharpBucketV1.Delete(new IssuesRoutes.PostIssue{AccountName = _accountName, RepositorySlug = _repository, Local_id = issueId}, overrideUrl);
        }

        public Issue PutIssue(Issue issue){
            var overrideUrl = String.Format(_issuesIdUrl, issue.Local_id);
            return _sharpBucketV1.Put(new IssuesRoutes.GetIssue{AccountName = _accountName, RepositorySlug = _repository, Title = issue.Title, Content = issue.Content}, overrideUrl);
        }

        public List<User> ListIssueFollowers(int issueId){
            var overrideUrl = String.Format(_issuesIdUrl + "followers", issueId);
            return _sharpBucketV1.Get(new IssuesRoutes.ListIssueFollowers{AccountName = _accountName, RepositorySlug = _repository, Id = issueId}, overrideUrl);
        }

        public List<Comment> ListIssueComments(int issueId){
            var overrideUrl = String.Format(_issuesIdUrl + "comments", issueId);
            return _sharpBucketV1.Get(new IssuesRoutes.ListIssueComments{AccountName = _accountName, RepositorySlug = _repository, Id = issueId}, overrideUrl);
        }

        public Comment PostIssueComment(int issueId, Comment comment){
            var overrideUrl = String.Format(_issuesIdUrl + "comments", issueId);
            return _sharpBucketV1.Post(new IssuesRoutes.PostIssueComment{AccountName = _accountName, RepositorySlug = _repository, Id = issueId, content = comment.Content}, overrideUrl);
        }

        public Comment GetIssueComment(int issueId, int? commentId){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issueId, commentId);
            return _sharpBucketV1.Get(new IssuesRoutes.GetIssueComment{AccountName = _accountName, RepositorySlug = _repository, Id = issueId, CommentId = commentId}, overrideUrl);
        }

        public Comment PutIssueComment(int issueId, int? commentId, Comment comment){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issueId, commentId);
            return _sharpBucketV1.Put(new IssuesRoutes.GetIssueComment{AccountName = _accountName, RepositorySlug = _repository, Id = issueId, CommentId = commentId, content = comment.Content}, overrideUrl);
        }

        public Comment DeleteIssueComment(int? issueId, int? commentId){
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issueId, commentId);
            return _sharpBucketV1.Delete(new IssuesRoutes.GetIssueComment{AccountName = _accountName, RepositorySlug = _repository, Id = issueId, CommentId = commentId}, overrideUrl);
        }

        public List<Component> ListComponents(){
            var overrideUrl = _issuesUrl + "/components";
            return _sharpBucketV1.Get(new IssuesRoutes.ListComponents{AccountName = _accountName, RepositorySlug = _repository}, overrideUrl);
        }

        public Component PostComponent(Component component){
            var overrideUrl = _issuesUrl + "/components";
            return _sharpBucketV1.Post(new IssuesRoutes.PostComponent{AccountName = _accountName, RepositorySlug = _repository, Name = component.Name}, overrideUrl);
        }

        public Component GetComponent(int componentId){
            var overrideUrl = _issuesUrl + "/components/" + componentId;
            return _sharpBucketV1.Get(new IssuesRoutes.GetComponent{AccountName = _accountName, RepositorySlug = _repository, Id = componentId}, overrideUrl);
        }

        public Component DeleteComponent(int? componentId){
            var overrideUrl = _issuesUrl + "/components/" + componentId;
            return _sharpBucketV1.Delete(new IssuesRoutes.GetComponent{AccountName = _accountName, RepositorySlug = _repository, Id = componentId}, overrideUrl);
        }

        public Component PutComponent(Component component){
            var overrideUrl = _issuesUrl + "/components/" + component.Id;
            return _sharpBucketV1.Put(new IssuesRoutes.GetComponent{AccountName = _accountName, RepositorySlug = _repository, Name = component.Name}, overrideUrl);
        }

        public List<Milestone> ListMilestones(){
            var overrideUrl = _issuesUrl + "/milestones";
            return _sharpBucketV1.Get(new IssuesRoutes.ListMilestones{AccountName = _accountName, RepositorySlug = _repository}, overrideUrl);
        }

        public Milestone PostMilestone(Milestone milestone){
            var overrideUrl = _issuesUrl + "/milestones";
            return _sharpBucketV1.Post(new IssuesRoutes.PostMilestone{AccountName = _accountName, RepositorySlug = _repository, Name = milestone.Name}, overrideUrl);
        }

        public Milestone GetMilestone(int? milestoneId){
            var overrideUrl = _issuesUrl + "/milestones/" + milestoneId;
            return _sharpBucketV1.Get(new IssuesRoutes.GetMilestone{AccountName = _accountName, RepositorySlug = _repository, Id = milestoneId}, overrideUrl);
        }

        public Milestone DeleteMilestone(int? milestoneId){
            var overrideUrl = _issuesUrl + "/milestones/" + milestoneId;
            return _sharpBucketV1.Delete(new IssuesRoutes.GetMilestone{AccountName = _accountName, RepositorySlug = _repository, Id = milestoneId}, overrideUrl);
        }

        public Milestone PutMilestone(Milestone milestone){
            var overrideUrl = _issuesUrl + "/milestones/" + milestone.Id;
            return _sharpBucketV1.Put(new IssuesRoutes.GetMilestone{AccountName = _accountName, RepositorySlug = _repository, Name = milestone.Name}, overrideUrl);
        }

        public List<Version> ListVersions(){
            var overrideUrl = _issuesUrl + "/versions";
            return _sharpBucketV1.Get(new IssuesRoutes.ListVersions{AccountName = _accountName, RepositorySlug = _repository}, overrideUrl);
        }

        public Version PostVersion(Version version){
            var overrideUrl = _issuesUrl + "/versions";
            return _sharpBucketV1.Post(new IssuesRoutes.PostVersion{AccountName = _accountName, RepositorySlug = _repository, Name = version.Name}, overrideUrl);
        }

        public Version GetVersion(int? versionId){
            var overrideUrl = _issuesUrl + "/versions/" + versionId;
            return _sharpBucketV1.Get(new IssuesRoutes.GetVersion{AccountName = _accountName, RepositorySlug = _repository, Id = versionId}, overrideUrl);
        }

        public Version PutVersion(Version version){
            var overrideUrl = _issuesUrl + "/versions/" + version.Id;
            return _sharpBucketV1.Put(new IssuesRoutes.GetVersion{AccountName = _accountName, RepositorySlug = _repository, Name = version.Name}, overrideUrl);
        }

        public Version DeleteVersion(int? versionId){
            var overrideUrl = _issuesUrl + "/versions/" + versionId;
            return _sharpBucketV1.Delete(new IssuesRoutes.GetVersion{AccountName = _accountName, RepositorySlug = _repository, Id = versionId}, overrideUrl);
        }

        public List<Tag> ListTags(){
            var tagsDictionary = _sharpBucketV1.Get(new RepositoryRoutes.ListTags{AccountName = _accountName, RepositorySlug = _repository});
            return tagsDictionary.Values.ToList();
        }

        public Tag GetTag(string tagId){
            return _sharpBucketV1.Get(new RepositoryRoutes.GetTag{AccountName = _accountName, RepositorySlug = _repository, Id = tagId});
        }

        public List<BranchInfo> ListBranches(){
            return _sharpBucketV1.Get(new RepositoryRoutes.ListBranches{AccountName = _accountName, RepositorySlug = _repository}).Values.ToList();
        }

        public MainBranch GetMainBranch(){
            return _sharpBucketV1.Get(new RepositoryRoutes.GetMainBranch{AccountName = _accountName, RepositorySlug = _repository});
        }

        public Wiki GetWiki(string page){
            var overrideUrl = _baserUrl + "wiki/" + page;
            return _sharpBucketV1.Get(new RepositoryRoutes.GetWiki{AccountName = _accountName, RepositorySlug = _repository, Page = page}, overrideUrl);
        }

        // Doesnt work, 500 server error, same for put
        public Wiki PostWiki(Wiki newPage, string location){
            var overrideUrl = _baserUrl + "wiki/" + location;
            return _sharpBucketV1.Post(new RepositoryRoutes.GetWiki { AccountName = _accountName, RepositorySlug = _repository, Page = location, Data = newPage.Data}, overrideUrl);
        }

        public ChangesetInfo ListChangeset(string start = null, int? limit = null){
            return _sharpBucketV1.Get(new ChangesRoutes.ListChangesets{AccountName = _accountName, RepositorySlug = _repository, Start = start, Limit = limit});
        }

        public Changeset GetChangeset(string node){
            var overrideUrl = _baserUrl + "changesets/" + node;
            return _sharpBucketV1.Get(new ChangesRoutes.GetChangeset{AccountName = _accountName, RepositorySlug = _repository, Node = node}, overrideUrl);
        }

        public List<DiffstatInfo> GetChangesetDiffstat(string node){
            return _sharpBucketV1.Get(new ChangesRoutes.GetChangesetDiffstat{AccountName = _accountName, RepositorySlug = _repository, Node = node});
        }

        public Changeset GetChangesetDiff(string node){
            return _sharpBucketV1.Get(new ChangesRoutes.GetChangesetDiff{AccountName = _accountName, RepositorySlug = _repository, Node = node});
        }

        public EventInfo ListEvents(){
            return _sharpBucketV1.Get(new RepositoryRoutes.ListEvents { AccountName = _accountName, RepositorySlug = _repository });
        }
    }
}