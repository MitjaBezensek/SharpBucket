using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints{
    public class IssueEndPoint{
        private readonly RepositoriesEndPointV1 _repositoriesEndPointV1;
        private readonly int _issueId;

        public IssueEndPoint(RepositoriesEndPointV1 repositoriesEndPointV1, int issueId){
            _issueId = issueId;
            _repositoriesEndPointV1 = repositoriesEndPointV1;
        }

        public List<Comment> ListComments(){
            return _repositoriesEndPointV1.ListIssueComments(_issueId);
        }

        public Comment PostComment(Comment comment){
            return _repositoriesEndPointV1.PostIssueComment(_issueId, comment);
        }

        public Comment GetIssueComment(int? commentId){
            return _repositoriesEndPointV1.GetIssueComment(_issueId, commentId);
        }

        public Comment PutIssueComment(Comment comment){
            return _repositoriesEndPointV1.PutIssueComment(_issueId, comment);
        }

        public Comment DeleteIssueComment(Comment comment) {
            return _repositoriesEndPointV1.DeleteIssueComment(_issueId, comment);
        }

        public Comment DeleteIssueComment(int? commentId){
            return _repositoriesEndPointV1.DeleteIssueComment(_issueId, commentId);
        }
    }
}