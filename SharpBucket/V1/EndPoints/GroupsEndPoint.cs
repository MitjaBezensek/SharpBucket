using System;
using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints
{
    /// <summary>
    /// The groups endpoint provides functionality for querying information about user groups, 
    /// creating new ones, updating memberships, and deleting them. Both individual and team accounts can define groups.
    /// To manage group information on an individual account, the caller must authenticate with administrative rights on the account. 
    /// More info here:
    /// https://confluence.atlassian.com/bitbucket/groups-endpoint-296093143.html
    /// </summary>
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class GroupsEndPoint
    {
        private readonly string _baseUrl;
        private readonly SharpBucketV1 _sharpBucketV1;

        public GroupsEndPoint(string accountName, SharpBucketV1 sharpBucketV1)
        {
            _sharpBucketV1 = sharpBucketV1;
            _baseUrl = $"groups/{accountName}/";
        }

        /// <summary>
        /// Get a list of all groups in an account. 
        /// </summary>
        /// <returns></returns>
        public List<Group> ListGroups()
        {
            return _sharpBucketV1.Get<List<Group>>(_baseUrl);
        }

        /// <summary>
        /// Get the details of a group. 
        /// </summary>
        /// <param name="group_slug">The group's slug.</param>
        /// <returns></returns>
        public Group GetGroup(string group_slug)
        {
            var overrideUrl = _baseUrl + group_slug;
            return _sharpBucketV1.Get<Group>(overrideUrl);
        }

        /// <summary>
        /// Creates a new default group 
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <returns></returns>
        public Group CreateGroup(string name)
        {
            var group = new Group { name = name };
            return _sharpBucketV1.Post(group, _baseUrl);
        }

        /// <summary>
        /// Deletes the specified group
        /// </summary>
        /// <param name="group_slug">The group's slug.</param>
        /// <returns></returns>
        public Group DeleteGroup(string group_slug)
        {
            var overrideUrl = _baseUrl + group_slug;
            return _sharpBucketV1.Delete<Group>(overrideUrl);
        }

        /// <summary>
        /// Updates the specified group
        /// </summary>
        /// <param name="group_slug">The group's slug to be updated.</param>
        /// <param name="group">The new group.</param>
        /// <returns></returns>
        public Group UpdateGroup(string group_slug, Group group)
        {
            var overrideUrl = _baseUrl + group_slug;
            return _sharpBucketV1.Put(group, overrideUrl);
        }

        /// <summary>
        /// Adds a member to a group
        /// </summary>
        /// <param name="group_slug">The group's slug</param>
        /// <param name="membername">An individual account name. This can be an account name or the primary email address for the account</param>
        /// <returns></returns>
        public User AddMemberToGroup(string group_slug, string membername)
        {
            var overrideUrl = _baseUrl + group_slug + "/members/" + membername;
            var member = new User() { username = membername };
            return _sharpBucketV1.Put(member, overrideUrl);
        }

        /// <summary>
        /// Gets the membership for a group 
        /// </summary>
        /// <param name="group_slug">The group's slug.</param>
        /// <returns></returns>
        public List<User> ListGroupMembers(string group_slug)
        {
            var overrideUrl = _baseUrl + group_slug + "/members";
            return _sharpBucketV1.Get<List<User>>(overrideUrl);
        }

        /// <summary>
        /// Adds a member to a group
        /// </summary>
        /// <param name="group_slug">The group's slug</param>
        /// <param name="membername">An individual account name. This can be an account name or the primary email address for the account</param>
        /// <returns></returns>
        public User DeleteMemberFromGroup(string group_slug, string membername)
        {
            var overrideUrl = _baseUrl + group_slug + "/members/" + membername;
            return _sharpBucketV1.Delete<User>(overrideUrl);
        }
    }
}