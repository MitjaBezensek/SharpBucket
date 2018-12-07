using NUnit.Framework;
using SharpBucket.V1;
using SharpBucket.V1.EndPoints;
using SharpBucket.V1.Pocos;
using Shouldly;
using System;
using System.Collections.Generic;

namespace SharBucketTests.V1.EndPoints
{
    [TestFixture]
    public class GroupsEndPointTests
    {
        private SharpBucketV1 sharpBucket;
        private GroupsEndPoint groupsEndPoint;
        private string accountName;

        [SetUp]
        public void Init()
        {
            sharpBucket = TestHelpers.GetV1ClientAuthenticatedWithOAuth();
            accountName = TestHelpers.GetAccountName();
            groupsEndPoint = sharpBucket.GroupsEndPoint(accountName);
        }

        [Test]
        public void CreateGroup_ForLoggedUser_ShouldReturnCreatedGroup()
        {
            groupsEndPoint.ShouldNotBe(null);

            var name = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var group = groupsEndPoint.CreateGroup(name);

            group.ShouldNotBe(null);
            group.ShouldBeOfType(typeof(Group));
            group.name.ShouldBe(name);
            group.permission.ShouldBe(null); //test that a default group was created
            group.members.Count.ShouldBe(0); //test that a default group was created
            groupsEndPoint.DeleteGroup(name);
        }

        [Test]
        public void DeleteGroup_ShouldNotHaveGroup_WhenGet()
        {
            groupsEndPoint.ShouldNotBe(null);

            var name = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var group = groupsEndPoint.CreateGroup(name);

            groupsEndPoint.DeleteGroup(name);
            group = groupsEndPoint.GetGroup(name);
            group.ShouldBe(null);
        }

        [Test]
        public void AddMemberToGroup_ShouldReturnAddedMember()
        {
            groupsEndPoint.ShouldNotBe(null);

            var name = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var group = new Group() { name = name };
            var new_group = groupsEndPoint.CreateGroup(group.name); //create a new group before adding a member to it

            new_group.ShouldNotBe(null);
            var member = groupsEndPoint.AddMemberToGroup(new_group.slug, accountName);

            member.ShouldNotBe(null);
            member.ShouldBeOfType(typeof(User));
            member.username.ShouldBe(accountName);
            groupsEndPoint.DeleteGroup(name);
        }

        [Test]
        public void ListGroupMembers_ShouldCorrectlyListAllMembers()
        {
            groupsEndPoint.ShouldNotBe(null);
            var name = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var group = new Group() { name = name };
            var new_group = groupsEndPoint.CreateGroup(group.name); //create a new group before 

            new_group.ShouldNotBe(null);
            var member = groupsEndPoint.AddMemberToGroup(new_group.slug, accountName);

            var all_members = groupsEndPoint.ListGroupMembers(new_group.slug);

            all_members.ShouldNotBe(null);
            all_members.ShouldBeOfType(typeof(List<User>));
            all_members.Count.ShouldBeGreaterThan(0);
            all_members.Find(x => x.username == member.username).ShouldNotBe(null); //test the newly created member is listed
            groupsEndPoint.DeleteGroup(name);
        }

        [Test]
        public void ListAllGroups_FromLoggedUser_ShouldReturnAllGroups()
        {
            groupsEndPoint.ShouldNotBe(null);

            var groups = groupsEndPoint.ListGroups();

            groups.ShouldNotBe(null);
            groups.ShouldBeOfType<List<Group>>();
            groups.Count.ShouldBeGreaterThan(0);
            groups[0].name.ShouldNotBeEmpty();
        }

        [Test]
        public void GetSingleGroup_FromLoggedUser_ShouldReturnSingleGroup()
        {
            groupsEndPoint.ShouldNotBe(null);
            var groupName = "AdminGroup";
            var singleGroup = groupsEndPoint.GetGroup(groupName);

            singleGroup.ShouldNotBe(null);
            singleGroup.ShouldBeOfType(typeof(Group));
            singleGroup.name.ShouldBe(groupName);
        }
    }
}