using SharpBucket.V1;
using SharpBucket.V1.Pocos;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
using Comment = SharpBucket.V1.Pocos.Comment;
using Repository = SharpBucket.V2.Pocos.Repository;
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
            //TestApiV1();
            TestApiV2();
        }

        private static void TestApiV1(){
            var sharpBucket = new SharpBucketV1();

            // Do basic auth
            //ReadTestDataBasic();
            //sharpBucket.BasicAuthentication(email, password);

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
            TestRepositoriesEndPoint(sharpBucket);
            TestUsersEndPoint(sharpBucket);
        }

        private static void TestApiV2(){
            var sharpBucket = new SharpBucketV2();
            ReadTestDataOauth();
            sharpBucket.OAuth2LeggedAuthentication(consumerKey, consumerSecretKey);
            //TestUsersEndPointV2(sharpBucket);
            //TestTeamsEndPointV2(sharpBucket);
            RestRepositoriesEndPointV2(sharpBucket);
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
            var issuesEP = sharpBucket.Repositories(accountName, repository).Issues();
            int ISSUE_ID = 5;

            // Issues
            var issues = issuesEP.ListIssues();
            var newIssue = new Issue{Title = "Let's add a new issue", content = "Some issue content", status = "new", priority = "trivial", kind = "bug"};
            var newIssueResult = issuesEP.PostIssue(newIssue);
            var issue = issuesEP.GetIssue(newIssueResult.local_id);
            var changedIssue = new Issue{Title = "Completely new title", content = "Hi!", status = "new", local_id = issue.local_id};
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

        private static void TestRepositoriesEndPoint(SharpBucketV1 sharpBucket){
            var repositoryEP = sharpBucket.Repositories(accountName, repository);
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
            var usersEP = sharpBucket.Users(accountName);
            var profile = usersEP.GetProfile();
            var followers = usersEP.ListFollowers();
            var following = usersEP.ListFollowing();
            var repositories = usersEP.ListRepositories();
        }

        private static void TestTeamsEndPointV2(SharpBucketV2 sharpBucket){
            var TEAM_NAME = "zebrabi";
            var teamsEP = sharpBucket.Teams(TEAM_NAME);
            var teamProfile = teamsEP.GetProfile();
            var teamMembers = teamsEP.ListMembers();
            var teamFollowers = teamsEP.ListFollowers();
            var teamFollowing = teamsEP.ListFollowing();
        }

        private static void RestRepositoriesEndPointV2(SharpBucketV2 sharpBucket){
            var repositoriesEP = sharpBucket.Repositories();
            //var repositories = repositoriesEP.ListRepositories(accountName);
            //var publicRepositories = repositoriesEP.ListPublicRepositories();
            //var repo = repositoriesEP.GetRepository(accountName, repository);
            var repoInfo = new RepositoryInfo();
            //var newRepository = repositoriesEP.PutRepository(repo, accountName, repository);
            //var deletedRepository = repositoriesEP.DeleteRepository(newRepository, accountName, repository);
            //var watchers = repositoriesEP.ListWatchers(accountName, repository);
            //var forks = repositoriesEP.ListForks(repo, accountName, repository);
            //var branchRestictions = repositoriesEP.ListBranchRestrictions(repo, accountName, repository);
            //var newRestrictions = repositoriesEP.PostBranchRestriction(repo, accountName, repository, new BranchRestriction());
            //int restrictionId = 1;
            //var restriction = repositoriesEP.GetBranchRestriction(repo, accountName, repository, branchRestictions[0].id);
            //var newRestiction = repositoriesEP.PutBranchRestriction(repo, accountName, repository, restrictionId, new BranchRestriction());
            //var deletedRestiction = repositoriesEP.DeleteBranchRestriction(repo, accountName, repository, restrictionId);
            //var diff = repositoriesEP.GetDiff(repo, accountName, repository, new object());
            //var patch = repositoriesEP.GetPatch(repo, accountName, repository, new object());
            //var commits = repositoriesEP.ListCommits(repo, accountName, repository);
            //string commitId = "sdfsdf";
            //var commit = repositoriesEP.GetCommit(repo, accountName, repository, commitId);
            //var commitComments = repositoriesEP.ListCommitComments(repo, accountName, repository, commitId);
            //int commentId = 10;
            //var commitComment = repositoriesEP.GetCommitComment(repo, accountName, repository, commitId, commentId);
            //var commitApproval = repositoriesEP.ApproveCommit(repo, accountName, repository, commitId);
            //var deleteApproval = repositoriesEP.DeleteCommitApproval(repo, accountName, repository, commitId);

            //var r = repositoriesEP.Repository(accountName, repository);
            //var dr = r.DeleteRepository();
            //var w = r.ListWatchers();
            //var f = r.ListForks();
            //var br = r.ListBranchRestrictions();
            //var gr = r.GetBranchRestriction(restrictionId);
            //var nr = r.PostBranchRestriction(new BranchRestriction());
            //var dbr = r.DeleteBrachRestriction(restrictionId);
            //var diff2 = r.GetDiff(new object());
            //var patch2 = r.GetPatch(new object());
            //var commits2 = r.ListCommits();
            //var commit2 = r.GetCommit(commitId);
            //var commitComments2 = r.ListCommitComments(commitId);
            //var commitComment2 = r.GetCommitComment(commitId, commentId);
            //var commitApproval2 = r.ApproveCommit(commitId);
            //var deleteApproval2 = r.DeleteCommitApproval(commitId);


            //var pullRequests = repositoriesEP.ListPullRequests(accountName, repository);
            var source = new Source{
                branch = new Branch{name = "develop"},
                repository = new Repository{
                    full_name = "zebra-bi-tester"
                }
            };
            var destination = new Source {
                branch = new Branch { name = "master" },
                commit = new Commit { hash = "56c3aca" }
            };
            var newRequest = new PullRequest{
                title = "testing new one",
                description = "My new description",
                source = source,
                destination = destination
            };
            //var newPullRequest = repositoriesEP.PostPullRequest(accountName, repository, newRequest);
            //var updatedPullRequest = repositoriesEP.PutPullRequest(accountName, repository, new PullRequest());
            var pullRequestId = 1;
            //var pullRequest = repositoriesEP.GetPullRequest(accountName, repository, pullRequestId);
            //pullRequest.title = "HEHEHEH";
            //var updatedPullRequest = repositoriesEP.PutPullRequest(accountName, repository, pullRequest);

            //var commitsInPullRequest = repositoriesEP.ListPullRequestCommits(accountName, repository, pullRequestId);
            //var postPullRequestApproval = repositoriesEP.ApprovePullRequest(accountName, repository, pullRequestId);
            //var deletePullRequest = repositoriesEP.RemovePullRequestApproval(accountName, repository, pullRequestId);
            //var getDiffForPullRequest = repositoriesEP.GetDiffForPullRequest(accountName, repository, pullRequestId);
            //var pullRequestLog = repositoriesEP.GetPullRequestLog(accountName, repository);
            //var pullRequestActivity = repositoriesEP.GetPullRequestActivity(accountName, repository, pullRequestId);
            //var acceptPullRequest = repositoriesEP.AcceptAndMergePullRequest(accountName, repository, pullRequestId);
            //var declinePullRequest = repositoriesEP.DeclinePullRequest(accountName, repository, pullRequestId);
            var pullRequestComments = repositoriesEP.ListPullRequestComments(accountName, repository, pullRequestId);
            var commentId = 10;
            var pullRequestComment = repositoriesEP.GetPullRequestComment(accountName, repository, pullRequestId, commentId);

            var pullRequestsEP = repositoriesEP.PullReqests(accountName, repository);
            var PRs = pullRequestsEP.ListPullRequests();
            var newPR = pullRequestsEP.PostPullRequest(new PullRequest());
            var updatedPR = pullRequestsEP.PutPullRequest(new PullRequest());
            var PRId = 10;
            var PR = pullRequestsEP.GetPullRequest(PRId);
            var commitsInPR = pullRequestsEP.ListPullRequestCommits(PRId);
            var postPRApproval = pullRequestsEP.ApprovePullRequest(PRId);
            var deletePR = pullRequestsEP.RemovePullRequestApproval(PRId);
            var getDiffForPR = pullRequestsEP.GetDiffForPullRequest(PRId);
            var PRLog = pullRequestsEP.GetPullRequestLog();
            var PRActivity = pullRequestsEP.GetPullRequestActivity(PRId);
            var acceptPR = pullRequestsEP.AcceptAndMergePullRequest(PRId);
            var declinePR = pullRequestsEP.DeclinePullRequest(PRId);
            var PRComments = pullRequestsEP.ListPullRequestComments(PRId);
            var cId = 10;
            var PRComment = pullRequestsEP.GetPullRequestComment(PRId, cId);

            var PRId2 = 10;
            var pullRequestEP = pullRequestsEP.PullRequestEndPoint(PRId);
            var PR2 = pullRequestEP.GetPullRequest();
            var commitsInPR2 = pullRequestEP.ListPullRequestCommits();
            var postPR2Approval = pullRequestEP.ApprovePullRequest();
            var deletePR2 = pullRequestEP.RemovePullRequestApproval();
            var getDiffForPR2 = pullRequestEP.GetDiffForPullRequest();
            var PR2Activity = pullRequestEP.GetPullRequestActivity();
            var acceptPR2 = pullRequestEP.AcceptAndMergePullRequest();
            var declinePR2 = pullRequestEP.DeclinePullRequest();
            var PR2Comments = pullRequestEP.ListPullRequestComments();
            var cId2 = 10;
            var PR2Comment = pullRequestEP.GetPullRequestComment(cId2);
        }
    }
}