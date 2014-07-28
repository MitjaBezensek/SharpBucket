using System.Collections.Generic;
using SharpBucket.POCOs;
using SharpBucket.Routes;
using SharpBucket.V1;

namespace SharpBucket.EndPoints{
    public class UsersEndpointV1{
        private readonly string _accountName;
        private readonly SharpBucketV1 _sharpBucketV1;

        public UsersEndpointV1(string accountName, SharpBucketV1 sharpBucketV1){
            _accountName = accountName;
            _sharpBucketV1 = sharpBucketV1;
        }

        public List<Email> ListEmails(){
            if (_accountName != null){
                return _sharpBucketV1.Get(new UsersRoutes.ListEmails{AccountName = _accountName});
            }
            return null;
        }

        public Email GetEmail(string email){
            if (_accountName != null){
                return _sharpBucketV1.Get(new UsersRoutes.GetEmail{AccountName = _accountName, Email = email});
            }
            return null;
        }

        public EventInfo ListUserEvents(){
            return _sharpBucketV1.Get(new UsersRoutes.ListUserEvents{AccountName = _accountName});
        }

        public Privileges ListUserPrivileges(){
            return _sharpBucketV1.Get(new UsersRoutes.ListPrivileges{AccountName = _accountName});
        }

        public InvitationsInfo ListInvitations(){
            return _sharpBucketV1.Get(new UsersRoutes.ListInvitations{AccountName = _accountName});
        }

        public InvitationsInfo GetInvitationsFor(string email){
            return _sharpBucketV1.Get(new UsersRoutes.ListInvitationsForEmail{Email = email});
        }

        public List<User> ListFollowers(){
            return _sharpBucketV1.Get(new UsersRoutes.ListFollowers{AccountName = _accountName});
        }

        public List<Consumer> ListConsumers(){
            return _sharpBucketV1.Get(new UsersRoutes.ListConsumers{AccountName = _accountName});
        }

        public object ListConsumer(string consumerId){
            return _sharpBucketV1.Get(new UsersRoutes.GetConsumer{AccountName = _accountName, Id = consumerId});
        }

        public List<SSH> ListSSHKeys(){
            return _sharpBucketV1.Get(new UsersRoutes.ListSSHKeys{AccountName = _accountName});
        }

        public SSHDetailed GetSSHKey(string id){
            return _sharpBucketV1.Get(new UsersRoutes.GetSSHKey{AccountName = _accountName, Id = id});
        }
    }
}