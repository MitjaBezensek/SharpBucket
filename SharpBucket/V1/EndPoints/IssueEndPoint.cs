using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints{
    public class IssueEndPoint{
        private readonly RepositoryEndPointV1 _repositoryEndPointV1;
        private readonly int _issueId;

        public IssueEndPoint(RepositoryEndPointV1 repositoryEndPointV1, int issueId){
            _issueId = issueId;
            _repositoryEndPointV1 = repositoryEndPointV1;
        }

        public List<Comment> ListComments(){
            return _repositoryEndPointV1.ListIssueComments(_issueId);
        }

        public Comment PostComment(Comment comment){
            return _repositoryEndPointV1.PostIssueComment(_issueId, comment);
        }

        public Comment GetIssueComment(int? commentId){
            return _repositoryEndPointV1.GetIssueComment(_issueId, commentId);
        }

        public Comment PutIssueComment(Comment comment){
            return _repositoryEndPointV1.PutIssueComment(_issueId, comment);
        }

        public Comment DeleteIssueComment(Comment comment) {
            return _repositoryEndPointV1.DeleteIssueComment(_issueId, comment);
        }

        public Comment DeleteIssueComment(int? commentId){
            return _repositoryEndPointV1.DeleteIssueComment(_issueId, commentId);
        }
    }
}