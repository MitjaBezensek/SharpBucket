using System.IO;
using SharpBucket.V1;
using SharpBucket.V1.Pocos;
using SharpBucket.V2;
using SharpBucket.V2.Pocos;
using Comment = SharpBucket.V1.Pocos.Comment;
using Link = SharpBucket.V1.Pocos.Link;
using Repository = SharpBucket.V2.Pocos.Repository;

namespace ConsoleTests{
    internal class Program{
        private static string email;
        private static string password;
        private static string consumerKey;
        private static string consumerSecretKey;
        private static string accountName;
        private static string repository;

        private static void Main(){
            // Decide which version you wish to test
            TestApiV1();
            //TestApiV2();
        }

        private static void TestApiV1(){
            var sharpBucket = new SharpBucketV1();
            // Decide on which authentication you wish to use
            //ReadTestDataBasic();
            //sharpBucket.BasicAuthentication(email, password);

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
            TestUserEndPoint(sharpBucket);
            TestIssuesEndPoint(sharpBucket);
            TestRepositoriesEndPoint(sharpBucket);
            TestUsersEndPoint(sharpBucket);
            TestPrivilegesEndPoint(sharpBucket);
        }

        private static void TestApiV2(){
            var sharpBucket = new SharpBucketV2();
            ReadTestDataOauth();
            sharpBucket.OAuth2LeggedAuthentication(consumerKey, consumerSecretKey);
            //TestUsersEndPoint(sharpBucket);
            //TestTeamsEndPoint(sharpBucket);
            TestRestRepositoriesEndPoint(sharpBucket);
        }

        private static void ReadTestDataOauth(){
            // Reads test data information from a file, you should structure it like this:
            // By default it reads from c:\
            // ApiKey:yourApiKey
            // SecretApiKey:yourSecretApiKey
            // AccountName:yourAccountName
            // Repository:testRepository
            var lines = File.ReadAllLines("c:\\TestInformationOauth.txt");
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
            var lines = File.ReadAllLines("c:\\TestInformation.txt");
            email = lines[0].Split(':')[1];
            password = lines[1].Split(':')[1];
            ReadAccoutNameAndRepository(lines);
        }

        private static void ReadAccoutNameAndRepository(string[] lines){
            accountName = lines[2].Split(':')[1];
            repository = lines[3].Split(':')[1];
        }

        private static void TestUserEndPoint(SharpBucketV1 sharpBucket){
            var userEndPoint = sharpBucket.UserEndPoint();
            var info = userEndPoint.GetInfo();
            var privileges = userEndPoint.ListPrivileges();
            var follows = userEndPoint.ListFollows();
            var userRepos = userEndPoint.ListRepositories();
            var userReposOverview = userEndPoint.RepositoriesOverview();
            var userRepositoryDashboard = userEndPoint.GetRepositoryDasboard();
        }

        private static void TestIssuesEndPoint(SharpBucketV1 sharpBucket){
            var issuesResource = sharpBucket.RepositoriesEndPoint(accountName, repository).IssuesResource();

            int ISSUE_ID = 5;
            // Issues
            var issues = issuesResource.ListIssues();
            var newIssue = new Issue{title = "Let's add a new issue", content = "Some issue content", status = "new", priority = "trivial", kind = "bug"};
            var newIssueResult = issuesResource.PostIssue(newIssue);
            var issue = issuesResource.GetIssue(newIssueResult.local_id);
            var changedIssue = new Issue{title = "Completely new title", content = "Hi!", status = "new", local_id = issue.local_id};
            var changedIssueResult = issuesResource.PutIssue(changedIssue);
            issuesResource.DeleteIssue(changedIssueResult.local_id);

            // Issue comments 
            var issueResource = issuesResource.IssueResource(ISSUE_ID);
            var issueComments = issueResource.ListComments();
            var newComment = new Comment{content = "This bug is really annoying!"};
            var newCommentResult = issueResource.PostComment(newComment);
            var comment = issueResource.GetIssueComment(newCommentResult.comment_id);
            comment.content = "The bug is still annoying";
            var updatedCommentRes = issueResource.PutIssueComment(comment);
            issueResource.DeleteIssueComment(updatedCommentRes.comment_id);

            // Issue followers
            var issueFollowers = issueResource.ListFollowers();

            // Components
            var components = issuesResource.ListComponents();
            var newComponent = new Component{name = "Awesome component"};
            var newComponentRes = issuesResource.PostComponent(newComponent);
            var component = issuesResource.GetComponent(newComponentRes.id);
            component.name = "Even more awesome component";
            var updatedComponent = issuesResource.PutComponent(component);
            issuesResource.DeleteComponent(updatedComponent.id);

            // Milestones
            var milestones = issuesResource.ListMilestones();
            var newMilestone = new Milestone{name = "Awesome milestone"};
            var newMilestoneRes = issuesResource.PostMilestone(newMilestone);
            var milestone = issuesResource.GetMilestone(newMilestoneRes.id);
            milestone.name = "Even more awesome milestone";
            var updatedMilestone = issuesResource.PutMilestone(milestone);
            issuesResource.DeleteMilestone(updatedMilestone.id);

            // Versions
            var versions = issuesResource.ListVersions();
            var newVersion = new Version{name = "Awesome version"};
            var newVersionRes = issuesResource.PostVersion(newVersion);
            var version = issuesResource.GetVersion(newVersionRes.id);
            version.name = "Even more awesome version";
            var updatedversion = issuesResource.PutVersion(version);
            issuesResource.DeleteVersion(updatedversion.id);
        }

        private static void TestRepositoriesEndPoint(SharpBucketV1 sharpBucket){
            var repositoriesEndPoint = sharpBucket.RepositoriesEndPoint(accountName, repository);
            var tags = repositoriesEndPoint.ListTags();
            var branches = repositoriesEndPoint.ListBranches();
            var mainBranch = repositoriesEndPoint.GetMainBranch();
            string WIKI_PAGE = "";
            var wiki = repositoriesEndPoint.GetWiki(WIKI_PAGE);
            var newPage = new Wiki{data = "Hello to my new page"};
            var newWiki = repositoriesEndPoint.PostWiki(newPage, "NewPage");
            var changeSet = repositoriesEndPoint.ListChangeset();
            var change = changeSet.changesets[4];
            var getChange = repositoriesEndPoint.GetChangeset(change.node);
            var diffStats = repositoriesEndPoint.GetChangesetDiffstat(change.node);
            var repoEvents = repositoriesEndPoint.ListEvents();
            var links = repositoriesEndPoint.ListLinks();
            var newLink = new Link{id = 100};
            var newLinkResponse = repositoriesEndPoint.PostLink(newLink);
            var link = repositoriesEndPoint.GetLink(newLinkResponse.id);
            newLinkResponse.handler.name = "sfsdf";
            var updatedLink = repositoriesEndPoint.PutLink(newLinkResponse);
            repositoriesEndPoint.DeleteLink(updatedLink);
        }

        private static void TestUsersEndPoint(SharpBucketV1 sharpBucket){
            var usersEndPoint = sharpBucket.UsersEndPoint(accountName);
            //var userEvents = usersEP.ListUserEvents();
            //var userPrivileges = usersEP.ListUserPrivileges();
            var invitations = usersEndPoint.ListInvitations();
            var email = "example@example.com";
            var invitationsForEmail = usersEndPoint.GetInvitationsFor(email);
            var followers = usersEndPoint.ListFollowers();
            var consumers = usersEndPoint.ListConsumers();
            int? CONSUMER_ID = consumers[0].id;
            var consumer = usersEndPoint.GetConsumer(CONSUMER_ID);
            var ssh_keys = usersEndPoint.ListSSHKeys();
            int? PK = ssh_keys[0].pk;
            var getSSH = usersEndPoint.GetSSHKey(PK);
        }

        private static void TestPrivilegesEndPoint(SharpBucketV1 sharpBucket){
            var privilegesEndPoint = sharpBucket.PrivilegesEndPoint(accountName);
            var privileges = privilegesEndPoint.ListRepositoryPrivileges(repository);
            var privilege = privilegesEndPoint.GetPrivilegesForAccount(repository, accountName);
        }

        private static void TestRestRepositoriesEndPoint(SharpBucketV2 sharpBucket){
            var repositoriesEndPoint = sharpBucket.RepositoriesEndPoint();
            var repositories = repositoriesEndPoint.ListRepositories(accountName);
            var publicRepositories = repositoriesEndPoint.ListPublicRepositories();
            var repositoryResource = repositoriesEndPoint.RepositoryResource(accountName, repository);
            //var repoInfo = new RepositoryInfo();
            //var newRepository = repositoriesEndPoint.PutRepository(repo, accountName, repository);
            //var deletedRepository = repositoriesEndPoint.DeleteRepository(newRepository, accountName, repository);
            var watchers = repositoryResource.ListWatchers();
            var forks = repositoryResource.ListForks();
            var branchRestictions = repositoryResource.ListBranchRestrictions();
            var newRestrictions = repositoryResource.PostBranchRestriction(new BranchRestriction());
            int restrictionId = 1;
            var restriction = repositoryResource.GetBranchRestriction(restrictionId);
            var newRestiction = repositoryResource.PutBranchRestriction(new BranchRestriction());
            var deletedRestiction = repositoryResource.DeleteBranchRestriction(restrictionId);
            var diff = repositoryResource.GetDiff(null);
            var patch = repositoryResource.GetPatch(null);
            var commits = repositoryResource.ListCommits();
            string commitId = "sdfsdf";
            var commit = repositoryResource.GetCommit(commitId);
            var commitComments = repositoryResource.ListCommitComments(commitId);
            int commentId = 10;
            var commitComment = repositoryResource.GetCommitComment(commitId, commentId);
            var commitApproval = repositoryResource.ApproveCommit(commitId);
            var deleteApproval = repositoryResource.DeleteCommitApproval(commitId);

            var targetUsername = "";
            var defaultReviewer = repositoryResource.PutDefaultReviewer(targetUsername);

            var r = repositoriesEndPoint.RepositoryResource(accountName, repository);
            var dr = r.DeleteRepository();
            var w = r.ListWatchers();
            var f = r.ListForks();
            var br = r.ListBranchRestrictions();
            var gr = r.GetBranchRestriction(restrictionId);
            var nr = r.PostBranchRestriction(new BranchRestriction());
            var dbr = r.DeleteBranchRestriction(restrictionId);
            var diff2 = r.GetDiff(new object());
            var patch2 = r.GetPatch(new object());
            var commits2 = r.ListCommits();
            var commit2 = r.GetCommit(commitId);
            var commitComments2 = r.ListCommitComments(commitId);
            var commitComment2 = r.GetCommitComment(commitId, commentId);
            var commitApproval2 = r.ApproveCommit(commitId);
            var deleteApproval2 = r.DeleteCommitApproval(commitId);

            var pullRequestsResource = r.PullRequestsResource();
            var pullRequests = pullRequestsResource.ListPullRequests();
            var source = new Source{
                branch = new Branch{name = "develop"},
                repository = new Repository{
                    full_name = repository
                }
            };
            var destination = new Source{
                branch = new Branch{name = "master"},
                commit = new Commit{hash = "56c3aca"}
            };
            var newRequest = new PullRequest{
                title = "testing new one",
                description = "My new description",
                source = source,
                destination = destination
            };
            //var newPullRequest = repositoriesEndPoint.PostPullRequest(accountName, repository, newRequest);
            //var updatedPullRequest = repositoriesEndPoint.PutPullRequest(accountName, repository, new PullRequest());
            //var pullRequestId = 1;
            //var pullRequest = repositoriesEndPoint.GetPullRequest(accountName, repository, pullRequestId);
            //pullRequest.title = "HEHEHEH";
            //var updatedPullRequest = repositoriesEndPoint.PutPullRequest(accountName, repository, pullRequest);

            //var commitsInPullRequest = repositoriesEndPoint.ListPullRequestCommits(accountName, repository, pullRequestId);
            //var postPullRequestApproval = repositoriesEndPoint.ApprovePullRequest(accountName, repository, pullRequestId);
            //var deletePullRequest = repositoriesEndPoint.RemovePullRequestApproval(accountName, repository, pullRequestId);
            //var getDiffForPullRequest = repositoriesEndPoint.GetDiffForPullRequest(accountName, repository, pullRequestId);
            //var pullRequestLog = repositoriesEndPoint.GetPullRequestLog(accountName, repository);
            //var pullRequestActivity = repositoriesEndPoint.GetPullRequestActivity(accountName, repository, pullRequestId);
            //var acceptPullRequest = repositoriesEndPoint.AcceptAndMergePullRequest(accountName, repository, pullRequestId);
            //var declinePullRequest = repositoriesEndPoint.DeclinePullRequest(accountName, repository, pullRequestId);
            //var pullRequestComments = repositoriesEndPoint.ListPullRequestComments(accountName, repository, pullRequestId);
            //var commentId = 10;
            //var pullRequestComment = repositoriesEndPoint.GetPullRequestComment(accountName, repository, pullRequestId, commentId);

            //var pullRequestsEP = repositoriesEndPoint.PullReqestsResource(accountName, repository);
            //var PRs = pullRequestsEP.ListPullRequests();
            //var newPR = pullRequestsEP.PostPullRequest(new PullRequest());
            //var updatedPR = pullRequestsEP.PutPullRequest(new PullRequest());
            //var PRId = 10;
            //var PR = pullRequestsEP.GetPullRequest(PRId);
            //var commitsInPR = pullRequestsEP.ListPullRequestCommits(PRId);
            //var postPRApproval = pullRequestsEP.ApprovePullRequest(PRId);
            //var deletePR = pullRequestsEP.RemovePullRequestApproval(PRId);
            //var getDiffForPR = pullRequestsEP.GetDiffForPullRequest(PRId);
            //var PRLog = pullRequestsEP.GetPullRequestLog();
            //var PRActivity = pullRequestsEP.GetPullRequestActivity(PRId);
            //var acceptPR = pullRequestsEP.AcceptAndMergePullRequest(PRId);
            //var declinePR = pullRequestsEP.DeclinePullRequest(PRId);
            //var PRComments = pullRequestsEP.ListPullRequestComments(PRId);
            //var cId = 10;
            //var PRComment = pullRequestsEP.GetPullRequestComment(PRId, cId);

            //var PRId2 = 10;
            //var pullRequestEP = pullRequestsEP.PullRequestEndPoint(PRId);
            //var PR2 = pullRequestEP.GetPullRequest();
            //var commitsInPR2 = pullRequestEP.ListPullRequestCommits();
            //var postPR2Approval = pullRequestEP.ApprovePullRequest();
            //var deletePR2 = pullRequestEP.RemovePullRequestApproval();
            //var getDiffForPR2 = pullRequestEP.GetDiffForPullRequest();
            //var PR2Activity = pullRequestEP.GetPullRequestActivity();
            //var acceptPR2 = pullRequestEP.AcceptAndMergePullRequest();
            //var declinePR2 = pullRequestEP.DeclinePullRequest();
            //var PR2Comments = pullRequestEP.ListPullRequestComments();
            //var cId2 = 10;
            //var PR2Comment = pullRequestEP.GetPullRequestComment(cId2);
        }
    }
}
