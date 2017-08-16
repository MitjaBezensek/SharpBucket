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

        [Test]
        [TestCase("MyGroup")]
        public IEnumerable<TestCaseData> CreateDefaultGroup_ForLoggedUser_ShouldReturnCreatedGroup(string name) {
            groupsEndPoint.ShouldNotBe(null);

            var group = groupsEndPoint.CreateGroup(name);

            group.ShouldNotBe(null);
            group.ShouldBeOfType(typeof(Group));
            group.name.ShouldBe(name);
            group.permission.ShouldBe(null); //test that a default group was created
            group.members.Count.ShouldBe(0);  //test that a default group was created

            return new List<TestCaseData>() { new TestCaseData(group, ACCOUNT_NAME) };
        }

        [Test]
        [TestCaseSource("CreateDefaultGroup_ForLoggedUser_ShouldReturnCreatedGroup")]
        public IEnumerable<TestCaseData> AddMemberToGroup_ShouldReturnAddedMember(Group group, string membername) {
            groupsEndPoint.ShouldNotBe(null);

            var member = groupsEndPoint.AddMemberToGroup(group.slug, membername);

            member.ShouldNotBe(null);
            member.ShouldBeOfType(typeof(User));
            member.username.ShouldBe(membername);

            return new List<TestCaseData>() { new TestCaseData(member, group.slug) };
        }

        [Test]
        [TestCaseSource("AddMemberToGroup_ShouldReturnAddedMember")]
        public void ListGroupMembers_ShouldCorrectlyListAllMembers(User member, string group_slug) {
            groupsEndPoint.ShouldNotBe(null);

            var all_members = groupsEndPoint.ListGroupMembers(group_slug);

            all_members.ShouldNotBe(null);
            all_members.ShouldBeOfType(typeof(List<User>));

            all_members.ShouldContain<User>(member); //test the newly created member is listed
        }

       [Test]
        public List<Group> ListAllGroups_FromLoggedUser_ShouldReturnAllGroups() {
            groupsEndPoint.ShouldNotBe(null);

            var groups = groupsEndPoint.ListGroups();

            groups.ShouldNotBe(null);
            groups.ShouldBeOfType<List<Group>>();
            groups.Count.ShouldBeGreaterThan(0);
            groups[0].name.ShouldNotBeEmpty();

            return groups;
        }

        [Test, TestCaseSource("ListAllGroups_FromLoggedUser_ShouldReturnAllGroups")]
        public void GetSingleGroup_FromLoggedUser_ShouldReturnSingleGroup(List<Group> groups) {
            groupsEndPoint.ShouldNotBe(null);

            var singleGroup = groupsEndPoint.GetGroup(groups[0].slug);

            singleGroup.ShouldNotBe(null);
            singleGroup.ShouldBeOfType(typeof(Group));
            singleGroup.name.ShouldBe(groups[0].name);
        }

      
    }
}
