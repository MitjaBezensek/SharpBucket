using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using Shouldly;
using Xunit;

namespace SharBucketTests.V2.EndPoints
{
   
    public class TeamsEndPointTests
    {
        private SharpBucketV2 sharpBucket;
        private TeamsEndPoint teamsEndPoint;
        private const string TEAM_NAME = "atlassian";


        public TeamsEndPointTests()
        {
            sharpBucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth();
            teamsEndPoint = sharpBucket.TeamsEndPoint(TEAM_NAME);
        }

        [Fact]
        public void GetProfile_FromTeamAtlassian_ReturnsAtlassianProfile()
        {
            teamsEndPoint.ShouldNotBe(null);
            var profile = teamsEndPoint.GetProfile();
            profile.display_name.ShouldBe("Atlassian");
        }

        [Fact]
        public void ListMembers_FromFirstTeamOfLoggedUser_ShouldReturnManyMembers()
        {
            teamsEndPoint.ShouldNotBe(null);
            var teams = teamsEndPoint.GetUserTeams();
            teams.Count.ShouldBeGreaterThan(0);

            var firstTeamEndPoint = sharpBucket.TeamsEndPoint(teams[0].username);
            var members = firstTeamEndPoint.ListMembers(35);
            members.Count.ShouldBeGreaterThan(0);
            var userName = sharpBucket.UserEndPoint().GetUser().username;
            members.ShouldContain(m => m.username == userName);
        }

        [Fact]
        public void ListFollowers_FromTeamAtlassian_ShouldReturnManyFollowers()
        {
            teamsEndPoint.ShouldNotBe(null);
            var followers = teamsEndPoint.ListFollowers(8);
            followers.Count.ShouldBe(8);
            followers[0].display_name.ShouldBe("Hector Miuler Malpica Gallegos");
        }

        [Fact]
        public void GetTeams_FromLoggedUser_ShouldReturnManyTeams()
        {
            teamsEndPoint.ShouldNotBe(null);
            var teams = teamsEndPoint.GetUserTeams();
            teams.Count.ShouldBeGreaterThan(0);
        }
    }
}