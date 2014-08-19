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

        /// <summary>
        /// List the events for the account.
        /// </summary>
        /// <returns></returns>
        public EventInfo ListUserEvents(){
            var overrideUrl = _baseUrl + "events/";
            return _sharpBucketV1.Get(new EventInfo(), overrideUrl);
        }

        /// <summary>
        /// List the privileges for the account.
        /// </summary>
        /// <returns></returns>
        public Privileges ListUserPrivileges(){
            var overrideUrl = _baseUrl + "privileges/";
            return _sharpBucketV1.Get(new Privileges(), overrideUrl);
        }

        /// <summary>
        /// List the invitations for the account.
        /// </summary>
        /// <returns></returns>
        public InvitationsInfo ListInvitations(){
            var overrideUrl = _baseUrl + "invitations/";
            return _sharpBucketV1.Get(new InvitationsInfo(), overrideUrl);
        }

        // TODO: Serialization
        /// <summary>
        /// List the invitations for the specified email.
        /// </summary>
        /// <param name="email">The email whose invitations you wish to get.</param>
        /// <returns></returns>
        public object GetInvitationsFor(string email){
            var overrideUrl = _baseUrl + "invitations/" + email;
            return _sharpBucketV1.Get(new object(), overrideUrl);
        }

        /// <summary>
        /// List the followers for the account.
        /// </summary>
        /// <returns></returns>
        public Followers ListFollowers(){
            var overrideUrl = _baseUrl + "followers/";
            return _sharpBucketV1.Get(new Followers(), overrideUrl);
        }

        /// <summary>
        /// List the consumers for the account.
        /// </summary>
        /// <returns></returns>
        public List<Consumer> ListConsumers(){
            var overrideUrl = _baseUrl + "consumers/";
            return _sharpBucketV1.Get(new List<Consumer>(), overrideUrl);
        }

        /// <summary>
        /// Get a specific consumer for the account.
        /// </summary>
        /// <param name="consumerId">The Id of the consumer that you wish to get.</param>
        /// <returns></returns>
        public Consumer GetConsumer(int? consumerId){
            var overrideUrl = _baseUrl + "consumers/" + consumerId;
            return _sharpBucketV1.Get(new Consumer(), overrideUrl);
        }

        /// <summary>
        /// List the SSH keys for the account.
        /// </summary>
        /// <returns></returns>
        public List<SSH> ListSSHKeys(){
            var overrideUrl = _baseUrl + "ssh-keys/";
            return _sharpBucketV1.Get(new List<SSH>(), overrideUrl);
        }

        /// <summary>
        /// Get a specific SSH key for the account.
        /// </summary>
        /// <param name="pk">The identification of the key that you wish to get.</param>
        /// <returns></returns>
        public SSHDetailed GetSSHKey(int? pk){
            var overrideUrl = _baseUrl + "ssh-keys/" + pk;
            return _sharpBucketV1.Get(new SSHDetailed(), overrideUrl);
        }

        /// <summary>
        /// List the emails for the account.
        /// </summary>
        /// <returns></returns>
        public List<EmailInfo> ListEmails(){
            var overrideUrl = _baseUrl + "emails/";
            return _sharpBucketV1.Get(new List<EmailInfo>(), overrideUrl);
        }

        /// <summary>
        /// Get a specific email information for the account.
        /// </summary>
        /// <param name="email">The email whose information you wish to get.</param>
        /// <returns></returns>
        public EmailInfo GetEmail(string email){
            var overrideUrl = _baseUrl + "emails/" + email;
            return _sharpBucketV1.Get(new EmailInfo(), overrideUrl);
        }
    }
}