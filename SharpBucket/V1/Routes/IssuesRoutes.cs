using System.Collections.Generic;
using System.Runtime.Serialization;
using NServiceKit.ServiceHost;
using SharpBucket.POCOs;

namespace SharpBucket.Routes{
    public class IssuesRoutes{
        // ISSUES DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/issues")]
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

            public string status { get; set; }
            public string priority { get; set; }
            public string title { get; set; }
            public User reported_by { get; set; }
            public string utc_last_updated { get; set; }
            public int comment_count { get; set; }
            public Metadata metadata { get; set; }
            public string content { get; set; }
            public string created_on { get; set; }
            public int? local_id { get; set; }
            public int follower_count { get; set; }
            public string utc_created_on { get; set; }
            public string resource_uri { get; set; }
            public bool is_spam { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/{local_id}/")]
        public class GetIssue : IReturn<Issue>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            public string status { get; set; }
            public string priority { get; set; }
            public string title { get; set; }
            public User reported_by { get; set; }
            public string utc_last_updated { get; set; }
            public int comment_count { get; set; }
            public Metadata metadata { get; set; }
            public string content { get; set; }
            public string created_on { get; set; }
            public int? local_id { get; set; }
            public int follower_count { get; set; }
            public string utc_created_on { get; set; }
            public string resource_uri { get; set; }
            public bool is_spam { get; set; }
        }

        // ISSUE FOLLOWERS DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/issues/{Id}/followers")]
        public class ListIssueFollowers : IReturn<List<User>>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
            public int Id { get; set; }
        }

        // ISSUE COMMENTS DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/issues/{Id}/comments")]
        public class ListIssueComments : IReturn<List<Comment>>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
            public int Id { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/{Id}/comments")]
        public class PostIssueComment : IReturn<Comment>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            [IgnoreDataMember]
            public int Id { get; set; }

            public string content { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/{Id}/comments/{CommentId}")]
        public class GetIssueComment : IReturn<Comment>{
            [IgnoreDataMember]
            public string AccountName { get; set; }

            [IgnoreDataMember]
            public string RepositorySlug { get; set; }

            [IgnoreDataMember]
            public int? Id { get; set; }

            public int? CommentId { get; set; }
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

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/components/{Id}")]
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

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/milestones/{Id}")]
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

        [Route("repositories/{AccountName}/{RepositorySlug}/issues/versions/{Id}")]
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