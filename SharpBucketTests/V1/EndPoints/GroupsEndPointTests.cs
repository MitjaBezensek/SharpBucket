using NUnit.Framework;
using SharpBucket.V1;
using SharpBucket.V1.EndPoints;
using SharpBucket.V1.Pocos;
using Shouldly;
using System.Collections.Generic;

namespace SharBucketTests.V1.EndPoints {
    [TestFixture]
    public class GroupsEndPointTests {

        private SharpBucketV1 sharpBucket;
        private GroupsEndPoint groupsEndPoint;
        private const string ACCOUNT_NAME = "mxgod";

        [SetUp]
        public void Init() {
            sharpBucket = TestHelpers.GetV1ClientAuthenticatedWithOAuth();
            groupsEndPoint = sharpBucket.GroupsEndPoint(ACCOUNT_NAME);
        }

        //[OneTimeTearDown]
        //public void Cleanup() {
        //    groupsEndPoint.DeleteGroup("mygroup");
        //    groupsEndPoint.DeleteGroup("admingroup");
        //    groupsEndPoint.DeleteGroup("testgroup");
        //}

        [Test]
        [TestCase("MyGroup")]
        public void CreateGroup_ForLoggedUser_ShouldReturnCreatedGroup(string name) {
            groupsEndPoint.ShouldNotBe(null);

            var group = groupsEndPoint.CreateGroup(name);

            group.ShouldNotBe(null);
            group.ShouldBeOfType(typeof(Group));
            group.name.ShouldBe("MyGroup");
            group.permission.ShouldBe(null); //test that a default group was created
            group.members.Count.ShouldBe(0);  //test that a default group was created

        }

        [Test]
        [TestCase("mygroup")]
        public void DeleteGroup_ShouldNotHaveGroup_WhenGet(string slug) {
            groupsEndPoint.ShouldNotBe(null);

            groupsEndPoint.DeleteGroup(slug);
            var group = groupsEndPoint.GetGroup(slug);
            group.ShouldBe(null);
        }

        [Test]
        public void AddMemberToGroup_ShouldReturnAddedMember() {
            groupsEndPoint.ShouldNotBe(null);

            var group = new Group() { name = "AdminGroup" };
            var new_group = groupsEndPoint.CreateGroup(group.name); //create a new group before adding a member to it

            new_group.ShouldNotBe(null);
            var member = groupsEndPoint.AddMemberToGroup(new_group.slug, ACCOUNT_NAME);

            member.ShouldNotBe(null);
            member.ShouldBeOfType(typeof(User));
            member.username.ShouldBe(ACCOUNT_NAME);
        }

        [Test]
        public void ListGroupMembers_ShouldCorrectlyListAllMembers() {
            groupsEndPoint.ShouldNotBe(null);

            var group = new Group() { name = "TestGroup" };
            var new_group = groupsEndPoint.CreateGroup(group.name); //create a new group before 

            new_group.ShouldNotBe(null);
            var member = groupsEndPoint.AddMemberToGroup(new_group.slug, ACCOUNT_NAME);

            var all_members = groupsEndPoint.ListGroupMembers(new_group.slug);

            all_members.ShouldNotBe(null);
            all_members.ShouldBeOfType(typeof(List<User>));
            all_members.Count.ShouldBeGreaterThan(0);
            all_members.Find(x => x.username == member.username).ShouldNotBe(null); //test the newly created member is listed
        }

       [Test]
        public void ListAllGroups_FromLoggedUser_ShouldReturnAllGroups() {
            groupsEndPoint.ShouldNotBe(null);

            var groups = groupsEndPoint.ListGroups();

            groups.ShouldNotBe(null);
            groups.ShouldBeOfType<List<Group>>();
            groups.Count.ShouldBeGreaterThan(0);
            groups[0].name.ShouldNotBeEmpty();
        }

        [Test]
        public void GetSingleGroup_FromLoggedUser_ShouldReturnSingleGroup() {
            groupsEndPoint.ShouldNotBe(null);

            var singleGroup = groupsEndPoint.GetGroup("admingroup");

            singleGroup.ShouldNotBe(null);
            singleGroup.ShouldBeOfType(typeof(Group));
            singleGroup.name.ShouldBe("AdminGroup");
        }

      
    }
}
