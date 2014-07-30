using System.Collections.Generic;
using SharpBucket.POCOs;
using SharpBucket.V1.Routes;

namespace SharpBucket.V1.EndPoints{
    public class UsersEndpointV1{
        private readonly string _accountName;
        private readonly SharpBucketV1 _sharpBucketV1;
        private string _baserUrl;
        private readonly string _invitationsUrl;
        private string _sshKeysUrl;

        public UsersEndpointV1(string accountName, SharpBucketV1 sharpBucketV1){
            _accountName = accountName;
            _sharpBucketV1 = sharpBucketV1;
            _baserUrl = "users/" + accountName + "/";
            _invitationsUrl = "users/" + accountName + "/invitations/";
            _sshKeysUrl = "users/" + accountName + "/ssh-keys/";
        }

        public List<EmailInfo> ListEmails(){
            if (_accountName != null){
                return _sharpBucketV1.Get(new UsersRoutes.ListEmails{AccountName = _accountName});
            }
            return null;
        }

        public EmailInfo GetEmail(string email){
            if (_accountName != null){
                return _sharpBucketV1.Get(new UsersRoutes.GetEmail{AccountName = _accountName, Email = email});
            }
            return null;
        }

        public EventInfo ListUserEvents(){
            return _sharpBucketV1.Get(new UsersRoutes.ListUserEvents{AccountName = _accountName});
        }

        public string ListUserPrivileges(){
            return _sharpBucketV1.Get(new UsersRoutes.ListPrivileges{AccountName = _accountName});
        }

        public string ListInvitations(){
            return _sharpBucketV1.Get(new UsersRoutes.ListInvitations{AccountName = _accountName});
        }

        public string GetInvitationsFor(string email){
            var overrideUrl = _invitationsUrl + email;
            return _sharpBucketV1.Get(new UsersRoutes.ListInvitationsForEmail{Email = email}, overrideUrl);
        }

        public List<User> ListFollowers(){
            return _sharpBucketV1.Get(new UsersRoutes.ListFollowers{AccountName = _accountName});
        }

        public List<Consumer> ListConsumers(){
            return _sharpBucketV1.Get(new UsersRoutes.ListConsumers{AccountName = _accountName});
        }

        public object ListConsumer(int? consumerId){
            return _sharpBucketV1.Get(new UsersRoutes.GetConsumer{AccountName = _accountName, Id = consumerId});
        }

        public List<SSH> ListSSHKeys(){
            return _sharpBucketV1.Get(new UsersRoutes.ListSSHKeys{AccountName = _accountName});
        }

        public SSHDetailed GetSSHKey(int? pk){
            var overrideUrl = _sshKeysUrl + pk;
            return _sharpBucketV1.Get(new UsersRoutes.GetSSHKey{AccountName = _accountName, pk = pk}, overrideUrl);
        }
    }
}