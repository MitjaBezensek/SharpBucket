using System.Collections.Generic;
using SharpBucket.POCOs;
using SharpBucket.Pocos;

namespace SharpBucket.V1.EndPoints{
    public class IssuesEndPoint{
        private readonly RepositoryEndPointV1 _repositoryEndPointV1;

        public IssuesEndPoint(RepositoryEndPointV1 repositoryEndPointV1){
            _repositoryEndPointV1 = repositoryEndPointV1;
        }

        public IssuesInfo ListIssues(){
            return _repositoryEndPointV1.ListIssues();
        }

        public Issue PostIssue(Issue issue){
            return _repositoryEndPointV1.PostIssue(issue);
        }

        public Issue GetIssue(int? issueId){
            return _repositoryEndPointV1.GetIssue(issueId);
        }

        public Issue PutIssue(Issue issue){
            return _repositoryEndPointV1.PutIssue(issue);
        }

        public Issue DeleteIssue(int? issueId){
            return _repositoryEndPointV1.DeleteIssue(issueId);
        }

        public IssueFollowers ListIssueFollowers(int? issueId){
            return _repositoryEndPointV1.ListIssueFollowers(issueId);
        }

        public List<Comment> ListIssueComments(int issueId){
            return _repositoryEndPointV1.ListIssueComments(issueId);
        }

        public Comment PostIssueComment(int issueId, Comment comment){
            return _repositoryEndPointV1.PostIssueComment(issueId, comment);
        }

        public Comment GetIssueComment(int issueId, int? commentId){
            return _repositoryEndPointV1.GetIssueComment(issueId, commentId);
        }

        public Comment PutIssueComment(int issudId, int? commentId, Comment comment){
            return _repositoryEndPointV1.PutIssueComment(issudId, commentId, comment);
        }

        public Comment DeleteIssueComment(int? issueId, int? commentId){
            return _repositoryEndPointV1.DeleteIssueComment(issueId, commentId);
        }

        public List<Component> ListComponents(){
            return _repositoryEndPointV1.ListComponents();
        }

        public Component PostComponent(Component component){
            return _repositoryEndPointV1.PostComponent(component);
        }

        public Component GetComponent(int? componentId){
            return _repositoryEndPointV1.GetComponent(componentId);
        }

        public Component PutComponent(Component component){
            return _repositoryEndPointV1.PutComponent(component);
        }

        public Component DeleteComponent(int? componentId){
            return _repositoryEndPointV1.DeleteComponent(componentId);
        }

        public List<Milestone> ListMilestones(){
            return _repositoryEndPointV1.ListMilestones();
        }

        public Milestone PostMilestone(Milestone milestone){
            return _repositoryEndPointV1.PostMilestone(milestone);
        }

        public Milestone GetMilestone(int? milestoneId){
            return _repositoryEndPointV1.GetMilestone(milestoneId);
        }

        public Milestone DeleteMilestone(int? milestoneId){
            return _repositoryEndPointV1.DeleteMilestone(milestoneId);
        }

        public Milestone PutMilestone(Milestone milestone){
            return _repositoryEndPointV1.PutMilestone(milestone);
        }

        public List<Version> ListVersions(){
            return _repositoryEndPointV1.ListVersions();
        }

        public Version PostVersion(Version version){
            return _repositoryEndPointV1.PostVersion(version);
        }

        public Version GetVersion(int? versionId){
            return _repositoryEndPointV1.GetVersion(versionId);
        }

        public Version PutVersion(Version version){
            return _repositoryEndPointV1.PutVersion(version);
        }

        public Version DeleteVersion(int? versionId){
            return _repositoryEndPointV1.DeleteVersion(versionId);
        }
    }
}