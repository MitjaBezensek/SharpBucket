using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints{
    public class UsersEndpointV1{
        private readonly SharpBucketV1 _sharpBucketV1;
        private readonly string _baseUrl;

        public UsersEndpointV1(string accountName, SharpBucketV1 sharpBucketV1){
            _sharpBucketV1 = sharpBucketV1;
            _baseUrl = "users/" + accountName + "/";
        }

        public EventInfo ListUserEvents(){
            var overrideUrl = _baseUrl + "events/";
            return _sharpBucketV1.Get(new EventInfo(), overrideUrl);
        }

        public Privileges ListUserPrivileges(){
            var overrideUrl = _baseUrl + "privileges/";
            return _sharpBucketV1.Get(new Privileges(), overrideUrl);
        }

        public InvitationsInfo ListInvitations(){
            var overrideUrl = _baseUrl + "invitations/";
            return _sharpBucketV1.Get(new InvitationsInfo(), overrideUrl);
        }

        // TODO: Serialization
        public object GetInvitationsFor(string email){
            var overrideUrl = _baseUrl + "invitations/" + email;
            return _sharpBucketV1.Get(new object(), overrideUrl);
        }

        public Followers ListFollowers(){
            var overrideUrl = _baseUrl + "followers/";
            return _sharpBucketV1.Get(new Followers(), overrideUrl);
        }

        public List<Consumer> ListConsumers(){
            var overrideUrl = _baseUrl + "consumers/";
            return _sharpBucketV1.Get(new List<Consumer>(), overrideUrl);
        }

        public Consumer GetConsumer(int? consumerId){
            var overrideUrl = _baseUrl + "consumers/" + consumerId;
            return _sharpBucketV1.Get(new Consumer(), overrideUrl);
        }

        public List<SSH> ListSSHKeys(){
            var overrideUrl = _baseUrl + "ssh-keys/";
            return _sharpBucketV1.Get(new List<SSH>(), overrideUrl);
        }

        public SSHDetailed GetSSHKey(int? pk){
            var overrideUrl = _baseUrl + "ssh-keys/" + pk;
            return _sharpBucketV1.Get(new SSHDetailed(), overrideUrl);
        }

        public List<EmailInfo> ListEmails(){
            var overrideUrl = _baseUrl + "emails/";
            return _sharpBucketV1.Get(new List<EmailInfo>(), overrideUrl);
        }

        public EmailInfo GetEmail(string email){
            var overrideUrl = _baseUrl + "emails/" + email;
            return _sharpBucketV1.Get(new EmailInfo(), overrideUrl);
        }
    }
}