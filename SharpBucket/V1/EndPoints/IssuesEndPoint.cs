using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints {
    public class IssuesEndPoint {
        private readonly RepositoryEndPointV1 _repositoryEndPointV1;
        
        public IssueEndPoint Issue(int issueId){
            return new IssueEndPoint(_repositoryEndPointV1, issueId);
        }

        public IssuesEndPoint(RepositoryEndPointV1 repositoryEndPointV1) {
            _repositoryEndPointV1 = repositoryEndPointV1;
        }

        public IssuesInfo ListIssues() {
            return _repositoryEndPointV1.ListIssues();
        }

        public Issue PostIssue(Issue issue) {
            return _repositoryEndPointV1.PostIssue(issue);
        }

        public Issue GetIssue(int? issueId) {
            return _repositoryEndPointV1.GetIssue(issueId);
        }

        public Issue PutIssue(Issue issue) {
            return _repositoryEndPointV1.PutIssue(issue);
        }

        public Issue DeleteIssue(Issue issue) {
            return _repositoryEndPointV1.DeleteIssue(issue);
        }

        public Issue DeleteIssue(int? issueId) {
            return _repositoryEndPointV1.DeleteIssue(issueId);
        }

        public IssueFollowers ListIssueFollowers(Issue issue) {
            return _repositoryEndPointV1.ListIssueFollowers(issue);
        }

        public IssueFollowers ListIssueFollowers(int? issueId) {
            return _repositoryEndPointV1.ListIssueFollowers(issueId);
        }

        public List<Comment> ListIssueComments(Issue issue) {
            return _repositoryEndPointV1.ListIssueComments(issue);
        }

        public List<Comment> ListIssueComments(int issueId) {
            return _repositoryEndPointV1.ListIssueComments(issueId);
        }

        public Comment PostIssueComment(int issueId, Comment comment) {
            return _repositoryEndPointV1.PostIssueComment(issueId, comment);
        }

        public Comment PostIssueComment(Issue issue, Comment comment) {
            return _repositoryEndPointV1.PostIssueComment(issue, comment);
        }

        public Comment GetIssueComment(Issue issue, int? commentId) {
            return _repositoryEndPointV1.GetIssueComment(issue, commentId);
        }

        public Comment GetIssueComment(int issueId, int? commentId) {
            return _repositoryEndPointV1.GetIssueComment(issueId, commentId);
        }

        public Comment PutIssueComment(Issue issue, Comment comment) {
            return _repositoryEndPointV1.PutIssueComment(issue, comment);
        }

        public Comment PutIssueComment(int issudId, Comment comment) {
            return _repositoryEndPointV1.PutIssueComment(issudId, comment);
        }

        public Comment DeleteIssueComment(Issue issue, Comment comment) {
            return _repositoryEndPointV1.DeleteIssueComment(issue, comment);
        }

        public Comment DeleteIssueComment(Issue issue, int? commentId) {
            return _repositoryEndPointV1.DeleteIssueComment(issue, commentId);
        }

        public Comment DeleteIssueComment(int? issueId, int? commentId) {
            return _repositoryEndPointV1.DeleteIssueComment(issueId, commentId);
        }

        public Comment DeleteIssueComment(int? issueId, Comment comment) {
            return _repositoryEndPointV1.DeleteIssueComment(issueId, comment);
        }

        public List<Component> ListComponents() {
            return _repositoryEndPointV1.ListComponents();
        }

        public Component PostComponent(Component component) {
            return _repositoryEndPointV1.PostComponent(component);
        }

        public Component GetComponent(int? componentId) {
            return _repositoryEndPointV1.GetComponent(componentId);
        }

        public Component PutComponent(Component component) {
            return _repositoryEndPointV1.PutComponent(component);
        }

        public Component DeleteComponent(Component component) {
            return _repositoryEndPointV1.DeleteComponent(component);
        }

        public Component DeleteComponent(int? componentId) {
            return _repositoryEndPointV1.DeleteComponent(componentId);
        }

        public List<Milestone> ListMilestones() {
            return _repositoryEndPointV1.ListMilestones();
        }

        public Milestone PostMilestone(Milestone milestone) {
            return _repositoryEndPointV1.PostMilestone(milestone);
        }

        public Milestone GetMilestone(int? milestoneId) {
            return _repositoryEndPointV1.GetMilestone(milestoneId);
        }

        public Milestone PutMilestone(Milestone milestone) {
            return _repositoryEndPointV1.PutMilestone(milestone);
        }

        public Milestone DeleteMilestone(Milestone milestone) {
            return _repositoryEndPointV1.DeleteMilestone(milestone);
        }

        public Milestone DeleteMilestone(int? milestoneId) {
            return _repositoryEndPointV1.DeleteMilestone(milestoneId);
        }

        public List<Version> ListVersions() {
            return _repositoryEndPointV1.ListVersions();
        }

        public Version PostVersion(Version version) {
            return _repositoryEndPointV1.PostVersion(version);
        }

        public Version GetVersion(int? versionId) {
            return _repositoryEndPointV1.GetVersion(versionId);
        }

        public Version PutVersion(Version version) {
            return _repositoryEndPointV1.PutVersion(version);
        }

        public Version DeleteVersion(Version version) {
            return _repositoryEndPointV1.DeleteVersion(version);
        }

        public Version DeleteVersion(int? versionId) {
            return _repositoryEndPointV1.DeleteVersion(versionId);
        }
    }
}