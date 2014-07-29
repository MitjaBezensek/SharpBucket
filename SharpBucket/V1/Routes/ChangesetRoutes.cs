using System.Collections.Generic;
using NServiceKit.ServiceHost;
using SharpBucket.POCOs;

namespace SharpBucket.V1.Routes{
    public class ChangesRoutes{
        // CHANGESET COMMENTS
        // TODO: Completly missing

        // CHANGESETS DONE
        [Route("repositories/{AccountName}/{RepositorySlug}/changesets")]
        public class ListChangesets : IReturn<ChangesetInfo>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
            public string start { get; set; }
            public int? limit { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/changesets/{Node}")]
        public class GetChangeset : IReturn<Changeset>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
            public string Node { get; set; }
        }

        [Route("repositories/{AccountName}/{RepositorySlug}/changesets/{Node}/diffstat")]
        public class GetChangesetDiffstat : IReturn<List<DiffstatInfo>>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
            public string Node { get; set; }
        }

        // TODO Serialization problems
        [Route("repositories/{AccountName}/{RepositorySlug}/changesets/{Node}/diff")]
        public class GetChangesetDiff : IReturn<Changeset>{
            public string AccountName { get; set; }
            public string RepositorySlug { get; set; }
            public string Node { get; set; }
        }

        // CHANGESET FILE HISTORY

        // PULLREQUEST COMMENTS

        // CHANGE REQUEST PARTICIPANTS

        // REVISION
    }
}