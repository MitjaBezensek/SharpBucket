using System.Collections.Generic;
using NServiceKit.ServiceHost;
using SharpBucket.POCOs;

namespace SharpBucket.V1.Routes{
    public class UsersRoutes{
        // USERS EVENTS DONE
        [Route("users/{AccountName}/events")]
        public class ListUserEvents : IReturn<EventInfo>{
            public string AccountName { get; set; }
        }

        // USERS

        // PLAN

        // AD

        // TEAM PRIVILEGES
        // Missing 
        [Route("users/{AccountName}/privileges")]
        public class ListPrivileges : IReturn<Privileges>{
            public string AccountName { get; set; }
        }

        // ACCOUNT INVITES
        // Missing
        [Route("users/{AccountName}/invitations")]
        public class ListInvitations : IReturn<InvitationsInfo>{
            public string AccountName { get; set; }
        }

        [Route("users/{AccountName}/invitations/{Email}")]
        public class ListInvitationsForEmail : IReturn<InvitationsInfo>{
            public string AccountName { get; set; }
            public string Email { get; set; }
        }

        // USERS FOLLOWERS DONE
        [Route("users/{AccountName}/followers")]
        public class ListFollowers : IReturn<List<User>>{
            public string AccountName { get; set; }
        }

        // CONSUMERS
        // Missing
        [Route("users/{AccountName}/consumers")]
        public class ListConsumers : IReturn<List<Consumer>>{
            public string AccountName { get; set; }
        }

        [Route("users/{AccountName}/consumers/{Id}")]
        public class GetConsumer : IReturn<Consumer>{
            public string AccountName { get; set; }
            public string Id { get; set; }
        }

        // SSH KEYS
        // Missing
        [Route("users/{AccountName}/ssh-keys")]
        public class ListSSHKeys : IReturn<List<SSH>>{
            public string AccountName { get; set; }
        }

        [Route("users/{AccountName}/ssh-keys/{Id}")]
        public class GetSSHKey : IReturn<SSHDetailed>{
            public string AccountName { get; set; }
            public string Id { get; set; }
        }

        // EMAILS
        // Missing
        [Route("users/{AccountName}/emails/")]
        public class ListEmails : IReturn<List<EmailInfo>>{
            public string AccountName { get; set; }
        }

        [Route("users/{AccountName}/emails/{Email}")]
        public class GetEmail : IReturn<EmailInfo>{
            public string AccountName { get; set; }
            public string Email { get; set; }
        }
    }
}