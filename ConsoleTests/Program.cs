using SharpBucket;
using SharpBucket.POCOs;

namespace ConsoleTests{
    internal class Program{
        private static string email;
        private static string password;
        private static string apiKey;
        private static string secretApiKey;
        private static string accountName;
        private static string repository;

        private static void Main(){
            var sharpBucket = new SharpBucketV1();

            // Do basic auth
            //ReadTestDataBasic();
            //sharpBucket.BasicAuthentication(email, password);
            
            // Or OAuth
            ReadTestDataOauth();
            sharpBucket.OAuthAuthentication(apiKey, secretApiKey);

            TestUserEndPoint(sharpBucket);
            TestIssuesEndPoint(sharpBucket);
            TestRepositoryEndPoint(sharpBucket);
            TestUsersEndPoint(sharpBucket);
        }

        private static void ReadTestDataOauth(){
            // Reads test data information from a file, you should structure it like this:
            // By default it reads from c:\
            // ApiKey:yourApiKey
            // SecretApiKey:yourSecretApiKey
            // AccountName:yourAccountName
            // Repository:testRepository
            var lines = System.IO.File.ReadAllLines("c:\\TestInformationOauth.txt");
            apiKey = lines[0].Split(':')[1];
            secretApiKey = lines[1].Split(':')[1];
            ReadAccoutNameAndRepository(lines);
        }

        private static void ReadTestDataBasic(){
            // Reads test data information from a file, you should structure it like this:
            // By default it reads from c:\
            // Username:yourUsername
            // Password:yourPassword
            // AccountName:yourAccountName
            // Repository:testRepository
            var lines = System.IO.File.ReadAllLines("c:\\TestInformationOauth.txt");
            email = lines[0].Split(':')[1];
            password = lines[1].Split(':')[1];
            ReadAccoutNameAndRepository(lines);
        }

        private static void ReadAccoutNameAndRepository(string[] lines){
            accountName = lines[2].Split(':')[1];
            repository = lines[3].Split(':')[1];
        }

        private static void TestUserEndPoint(SharpBucketV1 sharpBucketV1){
            var userEP = sharpBucketV1.User();
            var info = userEP.GetInfo();
            var privileges = userEP.GetPrivileges();
            var follows = userEP.ListFollows();
            var userRepos = userEP.ListRepositories();
            var userReposOverview = userEP.RepositoriesOverview();
        }

        private static void TestIssuesEndPoint(SharpBucketV1 sharpBucketV1){
            var issuesEP = sharpBucketV1.Repository(accountName, repository).Issues();
            int ISSUE_ID = 13;

            // Issues
            var issues = issuesEP.ListIssues();
            var newIssue = new Issue{title = "I have this little bug", content = "that is really annoying", status = "new"};
            var newIssueResult = issuesEP.PostIssue(newIssue);
            var changedIssue = new Issue{title = "Completely new title", content = "Hi!", status = "new", local_id = newIssueResult.local_id};
            var changedIssueResult = issuesEP.PutIssue(changedIssue);
            issuesEP.DeleteIssue(changedIssueResult.local_id);

            // Issue followers
            var issueFollowers = issuesEP.ListIssueFollowers(ISSUE_ID);

            // Issue comments

            var issueComments = issuesEP.ListIssueComments(ISSUE_ID);
            var firstComment = issueComments[0];
            var issueComment = issuesEP.GetIssueComment(ISSUE_ID, firstComment.comment_id);
            var newComment = new Comment{content = "This bug is really annoying!"};
            var newCommentResult = issuesEP.PostIssueComment(ISSUE_ID, newComment);
            var updateComment = new Comment{comment_id = newComment.comment_id, content = "updated"};
            var updatedCommentRes = issuesEP.PutIssueComment(ISSUE_ID, newCommentResult.comment_id, updateComment);
            issuesEP.DeleteIssueComment(13, updatedCommentRes.comment_id);

            // Components
            var components = issuesEP.ListComponents();
            var newComponent = new Component{name = "Awesome component"};
            var newComponentRes = issuesEP.PostComponent(newComponent);
            var component = components[0];
            component.name = "New name";
            var updatedComponent = issuesEP.PutComponent(component);
            issuesEP.DeleteComponent(newComponentRes.id);


            // Milestones
            var milestones = issuesEP.ListMilestones();
            var updateMilestone = milestones[0];
            var milestone = issuesEP.GetMilestone(updateMilestone.id);
            var newMilestone = new Milestone(){name = "Awesome milestone"};
            var newMilestoneRes = issuesEP.PostMilestone(newMilestone);
            issuesEP.DeleteMilestone(newMilestoneRes.id);
            updateMilestone.name = "New name";
            var updatedMilestone = issuesEP.PutMilestone(updateMilestone);

            // Versions
            var versions = issuesEP.ListVersions();
            var updateVersion = versions[0];
            var version = issuesEP.GetVersion(updateVersion.id);
            var newVersion = new Version{name = "Completely new"};
            var newVersionRes = issuesEP.PostVersion(newVersion);
            newVersionRes.name = "New name";
            var updatedversion = issuesEP.PutVersion(newVersionRes);
            issuesEP.DeleteVersion(updatedversion.id);
        }

        private static void TestRepositoryEndPoint(SharpBucketV1 sharpBucketV1){
            var repositoryEP = sharpBucketV1.Repository(accountName, repository);
            var tags = repositoryEP.ListTags();
            var branches = repositoryEP.ListBranches();
            var mainBranch = repositoryEP.GetMainBranch();
            string WIKI_PAGE = "";
            var wiki = repositoryEP.GetWiki(WIKI_PAGE);
            var newPage = new Wiki{data = "Hello to my new page"};
            var newWiki = repositoryEP.PostWiki(newPage, "Location");
            var changeSet = repositoryEP.ListChangeset();
            var change = changeSet.changesets[4];
            var smallerChangeSet = repositoryEP.ListChangeset(start: change.node, limit: 5);
            var getChange = repositoryEP.GetChangeset(change.node);
            var diffStats = repositoryEP.GetChangesetDiffstat(change.node);

            var repoEvents = repositoryEP.ListEvents();
        }

        private static void TestUsersEndPoint(SharpBucketV1 sharpBucketV1){
            var usersEP = sharpBucketV1.Users(accountName);
            var userEvents = usersEP.ListUserEvents();
            var userPrivileges = usersEP.ListUserPrivileges();
            var invitations = usersEP.ListInvitations();
            var invitationsForEmail = usersEP.GetInvitationsFor(email);
            var followers = usersEP.ListFollowers();
            var consumers = usersEP.ListConsumers();
            string CONSUMER_ID = "";
            var consumer = usersEP.ListConsumer(CONSUMER_ID);
            var ssh_keys = usersEP.ListSSHKeys();
            string SSH_ID = "";
            var getSSH = usersEP.GetSSHKey(SSH_ID);
        }
    }
}