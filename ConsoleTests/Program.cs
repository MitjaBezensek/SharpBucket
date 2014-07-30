using SharpBucket;
using SharpBucket.V1;
using SharpBucket.V1.Pocos;
using SharpBucket.V2;
using Version = SharpBucket.V1.Pocos.Version;

namespace ConsoleTests{
    internal class Program{
        private static string email;
        private static string password;
        private static string consumerKey;
        private static string consumerSecretKey;
        private static string accountName;
        private static string repository;

        private static void Main(){
            TestApiV1();
            //TestApiV2();
        }

        private static void TestApiV1(){
            var sharpBucket = new SharpBucketV1();

            // Do basic auth
            //ReadTestDataBasic();
            //sharpBucket.BasicAuthentication(email, password)

            // Or OAuth
            ReadTestDataOauth();
            // Two legged OAuth, just supply the consumerKey and the consumerSecretKey and you are done
            sharpBucket.OAuth2LeggedAuthentication(consumerKey, consumerSecretKey);

            // Three legged OAuth. We can supply our own callback url to which bitbucket will send our pin
            // If we use "oob" as the callback url we will get the bitbuckets url address which will have our pin
            //var authenticator = sharpBucket.OAuth3LeggedAuthentication(consumerKey, consumerSecretKey, "oob");
            //var uri = authenticator.StartAuthentication();
            //Process.Start(uri);
            //var pin = Console.ReadLine();
            // we can now do the final step by using the pin to get our access tokens
            //authenticator.AuthenticateWithPin(pin);

            // of if you saved the tokens you can simply use those
            // var authenticator = sharpBucket.OAuth3LeggedAuthentication(consumerKey, consumerSecretKey, "oauthtoken", "oauthtokensecret");

            //TestUserEndPoint(sharpBucket);
            TestIssuesEndPoint(sharpBucket);
            TestRepositoryEndPoint(sharpBucket);
            TestUsersEndPoint(sharpBucket);
        }

        private static void TestApiV2(){
            var sharpBucket = new SharpBucketV2();
            ReadTestDataOauth();
            sharpBucket.OAuth2LeggedAuthentication(consumerKey, consumerSecretKey);
            TestUsersEndPointV2(sharpBucket);
        }

        private static void ReadTestDataOauth(){
            // Reads test data information from a file, you should structure it like this:
            // By default it reads from c:\
            // ApiKey:yourApiKey
            // SecretApiKey:yourSecretApiKey
            // AccountName:yourAccountName
            // Repository:testRepository
            var lines = System.IO.File.ReadAllLines("c:\\TestInformationOauth.txt");
            consumerKey = lines[0].Split(':')[1];
            consumerSecretKey = lines[1].Split(':')[1];
            ReadAccoutNameAndRepository(lines);
        }

        private static void ReadTestDataBasic(){
            // Reads test data information from a file, you should structure it like this:
            // By default it reads from c:\
            // Username:yourUsername
            // Password:yourPassword
            // AccountName:yourAccountName
            // Repository:testRepository
            var lines = System.IO.File.ReadAllLines("c:\\TestInformation.txt");
            email = lines[0].Split(':')[1];
            password = lines[1].Split(':')[1];
            ReadAccoutNameAndRepository(lines);
        }

        private static void ReadAccoutNameAndRepository(string[] lines){
            accountName = lines[2].Split(':')[1];
            repository = lines[3].Split(':')[1];
        }

        private static void TestUserEndPoint(SharpBucketV1 sharpBucket){
            var userEP = sharpBucket.User();
            var info = userEP.GetInfo();
            var privileges = userEP.GetPrivileges();
            var follows = userEP.ListFollows();
            var userRepos = userEP.ListRepositories();
            var userReposOverview = userEP.RepositoriesOverview();
            var userRepositoryDashboard = userEP.GetRepositoryDasboard();
        }

        private static void TestIssuesEndPoint(SharpBucketV1 sharpBucket){
            var issuesEP = sharpBucket.Repository(accountName, repository).Issues();
            int ISSUE_ID = 5;

            // Issues
            var issues = issuesEP.ListIssues();
            var newIssue = new Issue{title = "Let's add a new issue", content = "Some issue content", status = "new", priority = "trivial", kind = "bug"};
            var newIssueResult = issuesEP.PostIssue(newIssue);
            var issue = issuesEP.GetIssue(newIssueResult.local_id);
            var changedIssue = new Issue{title = "Completely new title", content = "Hi!", status = "new", local_id = issue.local_id};
            var changedIssueResult = issuesEP.PutIssue(changedIssue);
            issuesEP.DeleteIssue(changedIssueResult.local_id);

            // Issue followers
            var issueFollowers = issuesEP.ListIssueFollowers(issues.issues[0].local_id);

            // Issue comments
            //var issueComments = issuesEP.ListIssueComments(ISSUE_ID);
            //var newComment = new Comment{content = "This bug is really annoying!"};
            //var newCommentResult = issuesEP.PostIssueComment(ISSUE_ID, newComment);
            //var comment = issuesEP.GetIssueComment(ISSUE_ID, newCommentResult.comment_id);
            //comment.content = "The bug is still annoying";
            //var updatedCommentRes = issuesEP.PutIssueComment(ISSUE_ID, comment);
            //issuesEP.DeleteIssueComment(ISSUE_ID, updatedCommentRes.comment_id);

            //// Issue comments alternative
            var issueEP = issuesEP.Issue(ISSUE_ID);
            var issueComments = issueEP.ListComments();
            var newComment = new Comment{content = "This bug is really annoying!"};
            var newCommentResult = issueEP.PostComment(newComment);
            var comment = issueEP.GetIssueComment(newCommentResult.comment_id);
            comment.content = "The bug is still annoying";
            var updatedCommentRes = issueEP.PutIssueComment(comment);
            issueEP.DeleteIssueComment(updatedCommentRes.comment_id);

            // Components
            var components = issuesEP.ListComponents();
            var newComponent = new Component{name = "Awesome component"};
            var newComponentRes = issuesEP.PostComponent(newComponent);
            var component = issuesEP.GetComponent(newComponentRes.id);
            component.name = "Even more awesome component";
            var updatedComponent = issuesEP.PutComponent(component);
            issuesEP.DeleteComponent(updatedComponent.id);

            // Milestones
            var milestones = issuesEP.ListMilestones();
            var newMilestone = new Milestone{name = "Awesome milestone"};
            var newMilestoneRes = issuesEP.PostMilestone(newMilestone);
            var milestone = issuesEP.GetMilestone(newMilestoneRes.id);
            milestone.name = "Even more awesome milestone";
            var updatedMilestone = issuesEP.PutMilestone(milestone);
            issuesEP.DeleteMilestone(updatedMilestone.id);

            // Versions
            var versions = issuesEP.ListVersions();
            var newVersion = new Version{name = "Awesome version"};
            var newVersionRes = issuesEP.PostVersion(newVersion);
            var version = issuesEP.GetVersion(newVersionRes.id);
            version.name = "Even more awesome version";
            var updatedversion = issuesEP.PutVersion(version);
            issuesEP.DeleteVersion(updatedversion.id);
        }

        private static void TestRepositoryEndPoint(SharpBucketV1 sharpBucket){
            var repositoryEP = sharpBucket.Repository(accountName, repository);
            var tags = repositoryEP.ListTags();
            var branches = repositoryEP.ListBranches();
            var mainBranch = repositoryEP.GetMainBranch();
            string WIKI_PAGE = "";
            var wiki = repositoryEP.GetWiki(WIKI_PAGE);
            var newPage = new Wiki{data = "Hello to my new page"};
            var newWiki = repositoryEP.PostWiki(newPage, "NewPage");
            var changeSet = repositoryEP.ListChangeset();
            var change = changeSet.changesets[4];
            var getChange = repositoryEP.GetChangeset(change.node);
            var diffStats = repositoryEP.GetChangesetDiffstat(change.node);
            var repoEvents = repositoryEP.ListEvents();
        }

        private static void TestUsersEndPoint(SharpBucketV1 sharpBucket){
            var usersEP = sharpBucket.Users(accountName);
            var userEvents = usersEP.ListUserEvents();
            var userPrivileges = usersEP.ListUserPrivileges();
            var invitations = usersEP.ListInvitations();
            var email = "example@example.com";
            var invitationsForEmail = usersEP.GetInvitationsFor(email);
            var followers = usersEP.ListFollowers();
            var consumers = usersEP.ListConsumers();
            int? CONSUMER_ID = consumers[0].id;
            // var consumer = usersEP.ListConsumer(CONSUMER_ID);
            var ssh_keys = usersEP.ListSSHKeys();
            int? PK = ssh_keys[0].pk;
            var getSSH = usersEP.GetSSHKey(PK);
        }

        private static void TestUsersEndPointV2(SharpBucketV2 sharpBucket){
            //var usersEP = sharpBucket.Users(accountName);
            //var profile = usersEP.GetProfile();
            //var followers = usersEP.ListFollowers();
            //var following = usersEP.ListFollowing();
            //var repositories = usersEP.ListRepositories();
        }
    }
}