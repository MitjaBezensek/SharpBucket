using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints
{
    /// <summary>
    /// The users endpoints gets information related to an individual or team account. 
    /// Individual and team accounts both have the same set of user fields:
    /// More info here:
    /// https://confluence.atlassian.com/display/BITBUCKET/users+Endpoint+-+1.0
    /// </summary>
    public class UsersEndPoint
    {
        private readonly SharpBucketV1 _sharpBucketV1;
        private readonly string _baseUrl;

        public UsersEndPoint(string accountName, SharpBucketV1 sharpBucketV1)
        {
            _sharpBucketV1 = sharpBucketV1;
            _baseUrl = "users/" + accountName + "/";
        }

        /// <summary>
        /// Gets a count and the list of events associated with an account. 
        /// This call requires authentication.
        /// </summary>
        /// <returns></returns>
        public EventInfo ListUserEvents()
        {
            var overrideUrl = _baseUrl + "events/";
            return _sharpBucketV1.Get(new EventInfo(), overrideUrl);
        }

        /// <summary>
        /// Gets the groups with account privileges defined for a team account. 
        /// </summary>
        /// <returns></returns>
        public Privileges ListUserPrivileges()
        {
            var overrideUrl = _baseUrl + "privileges/";
            return _sharpBucketV1.Get(new Privileges(), overrideUrl);
        }

        /// <summary>
        /// List of pending invitations on a team or individual account. 
        /// This call requires authorization and the caller must have administrative rights on the account.
        /// </summary>
        /// <returns></returns>
        public InvitationsInfo ListInvitations()
        {
            var overrideUrl = _baseUrl + "invitations/";
            return _sharpBucketV1.Get(new InvitationsInfo(), overrideUrl);
        }

        // TODO: Serialization
        /// <summary>
        /// List any pending invitations on a team or individual account for a particular email address. 
        /// Any user with admin access to the account can invite someone to a group. 
        /// This call requires authorization and the caller must have administrative rights on the account.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public object GetInvitationsFor(string email)
        {
            var overrideUrl = _baseUrl + "invitations/" + email;
            return _sharpBucketV1.Get(new object(), overrideUrl);
        }

        /// <summary>
        /// Gets a count and the list of accounts following an account. Use this API to get a list of the individuals following an account. 
        /// Currently, the Bitbucket UI does not list each account, it only displays the count. This call requires authentication.
        /// </summary>
        /// <returns></returns>
        public Followers ListFollowers()
        {
            var overrideUrl = _baseUrl + "followers/";
            return _sharpBucketV1.Get(new Followers(), overrideUrl);
        }

        /// <summary>
        /// List the consumers integrated with the account.
        /// </summary>
        /// <returns></returns>
        public List<Consumer> ListConsumers()
        {
            var overrideUrl = _baseUrl + "consumers/";
            return _sharpBucketV1.Get(new List<Consumer>(), overrideUrl);
        }

        /// <summary>
        /// Get an individual consumer for an account.
        /// </summary>
        /// <param name="consumerId">Identifier for the key.</param>
        /// <returns></returns>
        public Consumer GetConsumer(int? consumerId)
        {
            var overrideUrl = _baseUrl + "consumers/" + consumerId;
            return _sharpBucketV1.Get(new Consumer(), overrideUrl);
        }

        /// <summary>
        /// List all of the keys associated with an account. This call requires authentication. 
        /// </summary>
        /// <returns></returns>
        public List<SSH> ListSSHKeys()
        {
            var overrideUrl = _baseUrl + "ssh-keys/";
            return _sharpBucketV1.Get(new List<SSH>(), overrideUrl);
        }

        /// <summary>
        /// Gets the content of the specified key_id. 
        /// This call requires authentication. 
        /// </summary>
        /// <param name="pk">The key identifier. This is an internal value created by Bitbucket when the key is added.</param>
        /// <returns></returns>
        public SSHDetailed GetSSHKey(int? pk)
        {
            var overrideUrl = _baseUrl + "ssh-keys/" + pk;
            return _sharpBucketV1.Get(new SSHDetailed(), overrideUrl);
        }

        /// <summary>
        /// List of all the email addresses associated with the account. 
        /// This call requires authentication. The possible return fields are the same for both individual and team accounts. 
        /// </summary>
        /// <returns></returns>
        public List<EmailInfo> ListEmails()
        {
            var overrideUrl = _baseUrl + "emails/";
            return _sharpBucketV1.Get(new List<EmailInfo>(), overrideUrl);
        }

        /// <summary>
        /// Get an individual email address associated with an account. 
        /// This call requires authentication. 
        /// </summary>
        /// <param name="email">The email address to get.</param>
        /// <returns></returns>
        public EmailInfo GetEmail(string email)
        {
            var overrideUrl = _baseUrl + "emails/" + email;
            return _sharpBucketV1.Get(new EmailInfo(), overrideUrl);
        }
    }
}