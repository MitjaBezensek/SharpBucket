using System.Collections.Generic;
using NServiceKit.ServiceHost;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.Routes{
    public class UsersRoutes{
        [Route("users/{AccountName}")]
        public class GetProfile : IReturn<Profile>{
            public string AccountName { get; set; }
        }

        [Route("users/{AccountName}/followers")]
        public class ListFollowers : IReturn<ListOfUsers>{
            public string AccountName { get; set; }
        }

        [Route("users/{AccountName}/following")]
        public class ListFollowing : IReturn<ListOfUsers>{
            public string AccountName { get; set; }
        }

        [Route("repositories/{AccountName}")]
        public class ListRepositories : IReturn<List<Repository>>{
            public string AccountName { get; set; }
        }
    }
}