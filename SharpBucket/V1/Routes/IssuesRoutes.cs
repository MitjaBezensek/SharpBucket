using System.Collections.Generic;
using System.Runtime.Serialization;
using NServiceKit.ServiceHost;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.Routes{
    public class IssuesRoutes{
        // ISSUES DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/issues/")]
        public class ListIssues : IReturn<IssuesInfo>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/")]
        public class PostIssue : IReturn<Issue>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            public string title { get; set; }

            public string content { get; set; }

            public string status { get; set; }

            public string priority { get; set; }

            public string kind { get; set; }

            public int? local_id { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/{local_id}")]
        public class GetIssue : IReturn<Issue>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            public string Status { get; set; }
            public string Priority { get; set; }
            public string title { get; set; }
            public User Reported_by { get; set; }
            public string Utc_last_updated { get; set; }
            public int? Comment_count { get; set; }
            public Metadata Metadata { get; set; }
            public string content { get; set; }
            public string Created_on { get; set; }
            public int? local_id { get; set; }
            public int? Follower_count { get; set; }
            public string Utc_created_on { get; set; }
            public string Resource_uri { get; set; }
            public bool? Is_spam { get; set; }
        }

        // ISSUE FOLLOWERS DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/issues/{Id}/followers")]
        public class ListIssueFollowers : IReturn<IssueFollowers>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
            public int? Id { get; set; }
        }

        // ISSUE COMMENTS DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/issues/{Id}/comments")]
        public class ListIssueComments : IReturn<List<Comment>>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
            public int? Id { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/{Id}/comments")]
        public class PostIssueComment : IReturn<Comment>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            [IgnoreDataMember]
            public int? Id { get; set; }

            public string content { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/{Id}/comments/{commentId}")]
        public class GetIssueComment : IReturn<Comment>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            [IgnoreDataMember]
            public int? Id { get; set; }

            public int? commentId { get; set; }

            public string content { get; set; }
        }

        // ISSUE COMPONENTS DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/issues/components")]
        public class ListComponents : IReturn<List<Component>>{
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/components")]
        public class PostComponent : IReturn<Component>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            public string name { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/components/{id}")]
        public class GetComponent : IReturn<Component>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            public int? id { get; set; }

            public string name { get; set; }
        }

        // ISSUE MILESTONES DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/issues/milestones")]
        public class ListMilestones : IReturn<List<Milestone>>{
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/milestones")]
        public class PostMilestone : IReturn<Milestone>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            public string name { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/milestones/{id}")]
        public class GetMilestone : IReturn<Milestone>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            public int? id { get; set; }

            public string name { get; set; }
        }

        // ISSUE VERSIONS DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/issues/versions")]
        public class ListVersions : IReturn<List<Version>>{
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/versions")]
        public class PostVersion : IReturn<Version>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            public string name { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/versions/{id}")]
        public class GetVersion : IReturn<Version>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            public int? id { get; set; }

            public string name { get; set; }
        }
    }
}