using System;
using System.Collections.Generic;
using SharpBucket.V1.Pocos;
using Version = SharpBucket.V1.Pocos.Version;

namespace SharpBucket.V1.EndPoints
{
    /// <summary>
    /// The repositories endpoint has a number of resources you can use to manage repository resources. 
    /// For all repository resources, you supply a repo_slug that identifies the specific repository.  
    /// For example, the repo_slug for the repository https://bitbucket.org/tortoisehg/thg is thg.  
    /// More info here:
    /// https://confluence.atlassian.com/display/BITBUCKET/repositories+Endpoint+-+1.0
    /// </summary>
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class RepositoriesEndPoint
    {
        private readonly SharpBucketV1 _sharpBucketV1;
        private readonly string _baserUrl;
        private readonly string _issuesUrl;
        private readonly string _issuesIdUrl;

        public RepositoriesEndPoint(string accountName, string repository, SharpBucketV1 sharpBucketV1)
        {
            _sharpBucketV1 = sharpBucketV1;
            _baserUrl = "repositories/" + accountName + "/" + repository + "/";
            _issuesUrl = _baserUrl + "issues/";
            _issuesIdUrl = _issuesUrl + "{0}/";
        }

        #region Repositories End Point

        #region Change Set Resource

        /// <summary>
        /// Gets a list of change sets associated with a repository. By default, this call returns the 15 most recent changesets. 
        /// It also returns the count which is the total number of changesets on the repository. 
        /// Private repositories require the caller to authenticate. 
        /// </summary>
        /// <returns></returns>
        public ChangesetInfo ListChangeset()
        {
            var overrideUrl = _baserUrl + "changesets/";
            return _sharpBucketV1.Get<ChangesetInfo>(overrideUrl);
        }

        /// <summary>
        /// Gets a specific changeset node. Private repositories require the caller to authenticate. 
        /// </summary>
        /// <param name="changeset">The change set.</param>
        /// <returns></returns>
        private Changeset GetChangeset(Changeset changeset)
        {
            var overrideUrl = _baserUrl + "changesets/" + changeset.node;
            return _sharpBucketV1.Get<Changeset>(overrideUrl);
        }

        /// <summary>
        /// Gets a specific changeset  node. Private repositories require the caller to authenticate. 
        /// </summary>
        /// <param name="node">The node changeset identifier.</param>
        /// <returns></returns>
        public Changeset GetChangeset(string node)
        {
            return GetChangeset(new Changeset { node = node });
        }

        /// <summary>
        /// List containing statistics on changed file associated with a particular node in a change set. 
        /// Private repositories require the caller to authenticate. 
        /// </summary>
        /// <param name="changeset">The change set whose diff stat you wish to get.</param>
        /// <param name="limit">An integer representing how many changesets to return. You can specify a limit between 0 and 50.</param>
        /// <param name="start">An integer representing the index of the file to start returning results from.</param>
        /// <returns></returns>
        private List<DiffstatInfo> GetChangesetDiffstat(Changeset changeset, int limit, int start)
        {
            var overrideUrl = _baserUrl + "changesets/" + changeset.node + "/diffstat/";
            return _sharpBucketV1.Get<List<DiffstatInfo>>(overrideUrl, new { limit, start });
        }

        /// <summary>
        /// List containing statistics on changed file associated with a particular node in a change set. 
        /// Private repositories require the caller to authenticate. 
        /// </summary>
        /// <param name="node">The node changeset identifier.</param>
        /// <param name="limit">An integer representing how many changesets to return. You can specify a limit between 0 and 50.</param>
        /// <param name="start">An integer representing the index of the file to start returning results from.</param>
        /// <returns></returns>
        public List<DiffstatInfo> GetChangesetDiffstat(string node, int limit = 15, int start = 0)
        {
            return GetChangesetDiffstat(new Changeset { node = node }, limit, start);
        }

        /// <summary>
        /// Gets the actual diff associated with the changeset node. 
        /// This call returns the output as a string containing JSON. Private repositories require the caller to authenticate.
        /// </summary>
        /// <param name="changeset">The changeset.</param>
        /// <returns></returns>
        private Changeset GetChangesetDiff(Changeset changeset)
        {
            var overrideUrl = _baserUrl + "changesets/" + changeset.node + "/diff/";
            return _sharpBucketV1.Get<Changeset>(overrideUrl);
        }

        /// <summary>
        /// Gets the actual diff associated with the changeset node. 
        /// This call returns the output as a string containing JSON. Private repositories require the caller to authenticate.
        /// </summary>
        /// <param name="node">The node changeset identifier.</param>
        /// <returns></returns>
        public Changeset GetChangesetDiff(string node)
        {
            return GetChangesetDiff(new Changeset { node = node });
        }

        #endregion

        #region Deploy keys Resource

        /// <summary>
        /// List all of the keys associated with an repository.
        /// </summary>
        /// <returns></returns>
        public List<SSH> ListDeployKeys()
        {
            var overrideUrl = _baserUrl + "deploy-keys/";
            return _sharpBucketV1.Get<List<SSH>>(overrideUrl);
        }

        /// <summary>
        /// Gets the content of the specified key_id. This call requires authentication. 
        /// </summary>
        /// <param name="pk">The key identifier assigned by Bitbucket. Use the GET call to obtain this value.</param>
        /// <returns></returns>
        public SSH GetDeployKey(int? pk)
        {
            var overrideUrl = _baserUrl + "deploy-keys/" + pk + "/";
            return _sharpBucketV1.Get<SSH>(overrideUrl);
        }

        /// <summary>
        /// Creates a key on the specified account. You must supply a valid key that is unique across the Bitbucket service. 
        /// A public key contains characters need to be escaped before sending it as a POST data. So, use the proper escaping ( urlencode ), 
        /// if you are testing to add a key via your terminal. This call requires authentication. 
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public SSH PostDeployKey(SSH key)
        {
            var overrideUrl = _baserUrl + "deploy-keys/";
            return _sharpBucketV1.Post(key, overrideUrl);
        }

        /// <summary>
        /// Deletes the key specified by the key_id value. This call requires authentication. 
        /// </summary>
        /// <param name="key">The key identifier assigned by Bitbucket. Use the GET call to obtain this value.</param>
        /// <returns></returns>
        public SSH DeleteDeployKey(SSH key)
        {
            var overrideUrl = _baserUrl + "deploy-keys/" + key.pk + "/";
            return _sharpBucketV1.Delete<SSH>(overrideUrl);
        }

        #endregion

        #region Events resource

        /// <summary>
        /// List all events of a repository's events associated with the specified repo_slug. By default, this call returns the top 25 events. 
        /// </summary>
        /// <returns></returns>
        public EventInfo ListEvents()
        {
            var overrideUrl = _baserUrl + "events/";
            return _sharpBucketV1.Get<EventInfo>(overrideUrl);
        }

        #endregion

        #region Issues Resource

        /// <summary>
        /// The issues resource provides functionality for getting information on issues in an issue tracker, 
        /// creating new issues, updating them and deleting them. 
        /// You can access public issues without authentication, but you will only receive a subset of information, 
        /// and you can't gain access to private repositories' issues. By authenticating, you will get a more detailed set of information, 
        /// the ability to create issues, as well as access to updating data or deleting issues you have access to.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/issues+Resource
        /// </summary>
        /// <returns></returns>
        public IssuesResource IssuesResource()
        {
            return new IssuesResource(this);
        }

        internal IssuesInfo ListIssues(IssueSearchParameters parameters = null)
        {
            return _sharpBucketV1.Get<IssuesInfo>(_issuesUrl, parameters);
        }

        private Issue GetIssue(Issue issue)
        {
            var overrideUrl = String.Format(_issuesIdUrl, issue.local_id);
            return _sharpBucketV1.Get<Issue>(overrideUrl);
        }

        internal Issue GetIssue(int? issueId)
        {
            return GetIssue(new Issue { local_id = issueId });
        }

        internal IssueFollowers ListIssueFollowers(Issue issue)
        {
            var overrideUrl = String.Format(_issuesIdUrl + "followers", issue.local_id);
            return _sharpBucketV1.Get<IssueFollowers>(overrideUrl);
        }

        internal IssueFollowers ListIssueFollowers(int? issueId)
        {
            return ListIssueFollowers(new Issue { local_id = issueId });
        }

        internal Issue PostIssue(Issue issue)
        {
            return _sharpBucketV1.Post(issue, _issuesUrl);
        }

        internal Issue PutIssue(Issue issue)
        {
            var overrideUrl = String.Format(_issuesIdUrl, issue.local_id);
            return _sharpBucketV1.Put(issue, overrideUrl);
        }

        internal Issue DeleteIssue(Issue issue)
        {
            var overrideUrl = String.Format(_issuesIdUrl, issue.local_id);
            return _sharpBucketV1.Delete<Issue>(overrideUrl);
        }

        internal Issue DeleteIssue(int? issueId)
        {
            return DeleteIssue(new Issue { local_id = issueId });
        }

        internal List<Comment> ListIssueComments(Issue issue)
        {
            var overrideUrl = String.Format(_issuesIdUrl + "comments", issue.local_id);
            return _sharpBucketV1.Get<List<Comment>>(overrideUrl);
        }

        internal List<Comment> ListIssueComments(int issueId)
        {
            return ListIssueComments(new Issue { local_id = issueId });
        }

        private Comment GetIssueComment(int issueId, Comment comment)
        {
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issueId, comment.comment_id);
            return _sharpBucketV1.Get<Comment>(overrideUrl);
        }

        internal Comment GetIssueComment(Issue issue, int? commentId)
        {
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issue.local_id, commentId);
            return _sharpBucketV1.Get<Comment>(overrideUrl);
        }

        internal Comment GetIssueComment(int issueId, int? commentId)
        {
            return GetIssueComment(issueId, new Comment { comment_id = commentId });
        }

        internal Comment PostIssueComment(Issue issue, Comment comment)
        {
            var overrideUrl = String.Format(_issuesIdUrl + "comments", issue.local_id);
            return _sharpBucketV1.Post(comment, overrideUrl);
        }

        internal Comment PostIssueComment(int issueId, Comment comment)
        {
            return PostIssueComment(new Issue { local_id = issueId }, comment);
        }

        internal Comment PutIssueComment(Issue issue, Comment comment)
        {
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issue.local_id, comment.comment_id);
            return _sharpBucketV1.Put(comment, overrideUrl);
        }

        internal Comment PutIssueComment(int issueId, Comment comment)
        {
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issueId, comment.comment_id);
            return _sharpBucketV1.Put(comment, overrideUrl);
        }

        internal Comment DeleteIssueComment(Issue issue, Comment comment)
        {
            var overrideUrl = String.Format(_issuesIdUrl + "comments/{1}", issue.local_id, comment.comment_id);
            return _sharpBucketV1.Delete<Comment>(overrideUrl);
        }

        internal Comment DeleteIssueComment(Issue issue, int? commentId)
        {
            return DeleteIssueComment(issue, new Comment { comment_id = commentId });
        }

        internal Comment DeleteIssueComment(int? issueId, Comment comment)
        {
            return DeleteIssueComment(new Issue { local_id = issueId }, comment);
        }

        internal Comment DeleteIssueComment(int? issueId, int? commentId)
        {
            return DeleteIssueComment(issueId, new Comment { comment_id = commentId });
        }

        internal List<Component> ListComponents()
        {
            var overrideUrl = _issuesUrl + "components/";
            return _sharpBucketV1.Get<List<Component>>(overrideUrl);
        }

        private Component GetComponent(Component component)
        {
            var overrideUrl = _issuesUrl + "components/" + component.id;
            return _sharpBucketV1.Get<Component>(overrideUrl);
        }

        internal Component GetComponent(int? componentId)
        {
            return GetComponent(new Component { id = componentId });
        }

        internal Component PostComponent(Component component)
        {
            var overrideUrl = _issuesUrl + "components/";
            return _sharpBucketV1.Post(component, overrideUrl);
        }

        internal Component PutComponent(Component component)
        {
            var overrideUrl = _issuesUrl + "components/" + component.id;
            return _sharpBucketV1.Put(component, overrideUrl);
        }

        internal Component DeleteComponent(Component component)
        {
            var overrideUrl = _issuesUrl + "components/" + component.id;
            return _sharpBucketV1.Delete<Component>(overrideUrl);
        }

        internal Component DeleteComponent(int? componentId)
        {
            return DeleteComponent(new Component { id = componentId });
        }

        internal List<Version> ListVersions()
        {
            var overrideUrl = _issuesUrl + "versions/";
            return _sharpBucketV1.Get<List<Version>>(overrideUrl);
        }

        private Version GetVersion(Version version)
        {
            var overrideUrl = _issuesUrl + "versions/" + version.id;
            return _sharpBucketV1.Get<Version>(overrideUrl);
        }

        internal Version GetVersion(int? versionId)
        {
            return GetVersion(new Version { id = versionId });
        }

        internal Version PostVersion(Version version)
        {
            var overrideUrl = _issuesUrl + "versions/";
            return _sharpBucketV1.Post(version, overrideUrl);
        }

        internal Version PutVersion(Version version)
        {
            var overrideUrl = _issuesUrl + "versions/" + version.id;
            return _sharpBucketV1.Put(version, overrideUrl);
        }

        internal Version DeleteVersion(Version version)
        {
            var overrideUrl = _issuesUrl + "versions/" + version.id;
            return _sharpBucketV1.Delete<Version>(overrideUrl);
        }

        internal Version DeleteVersion(int? versionId)
        {
            return DeleteVersion(new Version { id = versionId });
        }

        internal List<Milestone> ListMilestones()
        {
            var overrideUrl = _issuesUrl + "milestones/";
            return _sharpBucketV1.Get<List<Milestone>>(overrideUrl);
        }

        private Milestone GetMilestone(Milestone milestone)
        {
            var overrideUrl = _issuesUrl + "milestones/" + milestone.id;
            return _sharpBucketV1.Get<Milestone>(overrideUrl);
        }

        internal Milestone GetMilestone(int? milestoneId)
        {
            return GetMilestone(new Milestone { id = milestoneId });
        }

        internal Milestone PostMilestone(Milestone milestone)
        {
            var overrideUrl = _issuesUrl + "milestones/";
            return _sharpBucketV1.Post(milestone, overrideUrl);
        }

        internal Milestone PutMilestone(Milestone milestone)
        {
            var overrideUrl = _issuesUrl + "milestones/" + milestone.id;
            return _sharpBucketV1.Put(milestone, overrideUrl);
        }

        internal Milestone DeleteMilestone(Milestone milestone)
        {
            var overrideUrl = _issuesUrl + "milestones/" + milestone.id;
            return _sharpBucketV1.Delete<Milestone>(overrideUrl);
        }

        internal Milestone DeleteMilestone(int? milestoneId)
        {
            return DeleteMilestone(new Milestone { id = milestoneId });
        }

        #endregion

        #region Links Resource

        /// <summary>
        /// List all the links associated with a repository. The caller must authenticate as a user with administrative access to the repository.
        /// </summary>
        /// <returns></returns>
        public List<Link> ListLinks()
        {
            var overrideUrl = _baserUrl + "links/";
            return _sharpBucketV1.Get<List<Link>>(overrideUrl);
        }

        /// <summary>
        /// Gets an individual link on a repository. The caller must authenticate as a user with administrative access to the repository. 
        /// </summary>
        /// <param name="linkId">The link id.</param>
        /// <returns></returns>
        public Link GetLink(int? linkId)
        {
            var overrideUrl = _baserUrl + "links/" + linkId + "/";
            return _sharpBucketV1.Get<Link>(overrideUrl);
        }

        /// <summary>
        /// Creates a new link on the repository. 
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns></returns>
        public Link PostLink(Link link)
        {
            var overrideUrl = _baserUrl + "links/";
            return _sharpBucketV1.Post(link, overrideUrl);
        }

        /// <summary>
        /// Update a repository link. 
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns></returns>
        public Link PutLink(Link link)
        {
            var overrideUrl = _baserUrl + "links/" + link.id + "/";
            return _sharpBucketV1.Put(link, overrideUrl);
        }

        /// <summary>
        /// Deletes the repository link identified by the object_id. The caller must authenticate as a user with administrative access to the repository. 
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns></returns>
        public Link DeleteLink(Link link)
        {
            var overrideUrl = _baserUrl + "links/" + link.id + "/";
            return _sharpBucketV1.Delete<Link>(overrideUrl);
        }

        #endregion

        #region Repository Resource

        /// <summary>
        /// Gets a list of branches associated with a repository. 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, BranchInfo> ListBranches()
        {
            var overrideUrl = _baserUrl + "branches/";
            return _sharpBucketV1.Get<Dictionary<string, BranchInfo>>(overrideUrl);
        }

        /// <summary>
        /// Gets the main-branch associated with the repository. 
        /// You set the main branch from a repository's Repository details page.
        /// </summary>
        /// <returns></returns>
        public MainBranch GetMainBranch()
        {
            var overrideUrl = _baserUrl + "main-branch/";
            return _sharpBucketV1.Get<MainBranch>(overrideUrl);
        }

        /// <summary>
        /// Use this resource to list the tags and branches for a given repository. 
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, Tag> ListTags()
        {
            var overrideUrl = _baserUrl + "tags/";
            return _sharpBucketV1.Get<Dictionary<string, Tag>>(overrideUrl);
        }

        #endregion

        #region Sources Resource

        public SrcDirectory ListSources(BranchInfo branch, string directory)
        {
            return ListSources(branch.branch, directory);
        }

        public SrcDirectory ListSources(MainBranch branch, string directory)
        {
            return ListSources(branch.name, directory);
        }

        public SrcDirectory ListSources(string branch, string directory)
        {
            var overrideUrl = _baserUrl + "src/" + branch + "/" + directory + "/";
            return _sharpBucketV1.Get<SrcDirectory>(overrideUrl);
        }

        public SrcFile GetSrcFile(string branch, string filepath)
        {
            var overrideUrl = _baserUrl + "src/" + branch + "/" + filepath;
            return _sharpBucketV1.Get<SrcFile>(overrideUrl);
        }

        #endregion

        #region Wiki Resource

        /// <summary>
        /// Gets the contents of a wiki page and the current revision. 
        /// You must supply the title of a page to get.  When getting a page, do not include the extension .wiki. 
        /// If you do not supply a page value, the default is the Home page
        /// </summary>
        /// <param name="page">Title of the page.</param>
        /// <returns></returns>
        public Wiki GetWiki(string page)
        {
            var overrideUrl = _baserUrl + "wiki/" + page;
            return _sharpBucketV1.Get<Wiki>(overrideUrl);
        }

        // TODO: Doesn't work, 500 server error, same for put
        /// <summary>
        /// Creates a new wiki page. 
        /// </summary>
        /// <param name="newPage">Title of the page.</param>
        /// <param name="location">Path to the page.</param>
        /// <returns></returns>
        public Wiki PostWiki(Wiki newPage, string location)
        {
            var overrideUrl = _baserUrl + "wiki/" + location;
            return _sharpBucketV1.Post(newPage, overrideUrl);
        }

        /// <summary>
        /// Updates an existing wiki page.
        /// </summary>
        /// <param name="updatedPage">Title of the page.</param>
        /// <param name="location">Path to the page.</param>
        /// <returns></returns>
        public Wiki PutWiki(Wiki updatedPage, string location)
        {
            var overrideUrl = _baserUrl + "wiki/" + location;
            return _sharpBucketV1.Put(updatedPage, overrideUrl);
        }

        #endregion

        #region Revision Resource

        /// <summary>
        /// Lists the source files for a given revision and path.
        /// </summary>
        /// <param name="revision">Revision to get.</param>
        /// <param name="path">File to get.</param>
        /// <returns></returns>
        public List<Revision> GetRevisionSrc(string revision, string path)
        {
            var overrideUrl = _baserUrl + "src/" + revision + "/" + path;
            return _sharpBucketV1.Get<List<Revision>>(overrideUrl);
        }

        /// <summary>
        /// Retrieves file contents. 
        /// </summary>
        /// <param name="revision">Revision to get.</param>
        /// <param name="path">File to get.</param>
        /// <returns></returns>
        public String GetRevisionRaw(string revision, string path)
        {
            var overrideUrl = _baserUrl + "raw/" + revision + "/" + path;
            return _sharpBucketV1.Get(overrideUrl);
        }

        #endregion

        #endregion
    }
}