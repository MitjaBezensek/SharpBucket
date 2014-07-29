using System.Collections.Generic;
using NServiceKit.ServiceHost;
using SharpBucket.POCOs;

namespace SharpBucket.V1.Routes{
    public class UserRoutes{
        // USER
        [Route("user")]
        public class GetInfo : IReturn<UserInfo>{
        }

        [Route("user/privileges")]
        public class GetPrivileges : IReturn<Privileges>{
        }

        [Route("user/follows")]
        public class ListFollows : IReturn<List<Repository>>{
        }

        [Route("user/repositories")]
        public class ListRepositories : IReturn<List<Repository>>{
        }

        [Route("user/repositories/overview")]
        public class GetRepositoryOverview : IReturn<RepositoriesOverview>{
        }

        // TODO Repository dashboard. Not priority.
        [Route("user/repositories/dashboard")]
        public class GetRepositoryDashboard : IReturn<RepositoriesOverview>{
        }
    }
}