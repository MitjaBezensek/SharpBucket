using System.Collections.Generic;
using System.Runtime.Serialization;
using NServiceKit.ServiceHost;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.Routes{
    public class RepositoryRoutes{
        // REPOSITORY TAGS DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/tags")]
        public class ListTags : IReturn<Dictionary<string, Tag>>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
        }

        // REPOSITORY BRANCHES DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/branches")]
        public class ListBranches : IReturn<Dictionary<string, BranchInfo>>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
        }

        // REPOSITORY MAIN BRANCH DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/main-branch")]
        public class GetMainBranch : IReturn<MainBranch>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
        }

        // TODO: Serializations problems
        [Route("repositories/{AccountName}/{RepositorySlug}/tags/{Id}")]
        public class GetTag : IReturn<Tag>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
            public string Id { get; set; }
        }

        // WIKI
        [Route("repositories/{AccountName}/{RepositorySlug}/wiki/{Page}")]
        public class GetWiki : IReturn<Wiki>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            [IgnoreDataMember]
            public string Page { get; set; }

            public string Data { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}")]
        public class GetRepository : IReturn<Repository>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
        }

        // REPOSITORY EVENTS DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/events")]
        public class ListEvents : IReturn<EventInfo> {
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
        }
        // SERVICES

        // REPOSITORIES

        // REPOSITORY LINKS

        // REPOSITORY DEPLOY KEYS

        // FORK REPOSITORY

        // PATCH QUEUE

        // REPOSITORY INVITES

        // REPOSITORY FOLLOWERS
      
    }
}