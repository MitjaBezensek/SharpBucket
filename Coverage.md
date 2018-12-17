# [Api V1][1]
## [Users endpoint][2]
### [Account Resource][3]
- GET the account profile **(done)**
- GET the account plan **(done)**
- GET the followers **(done)**
- GET the events **(done)**

### [Consumers Resource][4]
- GET a list of an account's consumers  **(done)**
- GET a consumer  **(done)**
- PUT an updated consumer
- DELETES a consumer

### [Emails Resource][5]
- GET a list of user's email addresses **(done)**
- GET an email address **(done)**
- POST a new email address **(done)**
- Update an email address **(done)**

### [Invitations Resource][6]
### [Privileges Resource][8]
### [Ssh-keys Resource][9]
- GET the list of an accounts SSH keys **(done)**
- POST a new ssh key **(done)**
- GET a ssh key **(done)**
- DELETE a key

## [Repositories endpoint][10]
### [Changesets Resource][11]
- GET a list of changesets **(done)**
- GET an individual changeset **(done)**
- GET participants associated with an individual changeset
- GET statistics associated with an individual changeset
- GET the diff associated with a changeset **(done)**
- GET a list of comments on a changeset 
- DELETE a comment on a changeset
- POST a new comment on a changeset
- PUT an update to an existing changeset comment
- Toggle spam flag on an existing changeset comment

### [Deploy-keys Resource][12]
- GET a list of keys **(done)**
- GET the key's content **(done)**
- POST a new key **(done)**
- DELETE a key **(done)**

### [Events Resources][13]
 - GET a list of events **(done)**

### [Followers Resource][14]
- Get a list of users that are following the repository **(done)**

### [Issues Resource][15]
- GET a list of issues in a repository's tracker **(done)**
- GET an individual issue **(done)**
- GET a list of an issue's followers **(done)**
- POST a new issue **(done)**
- Update an existing issue **(done)**
- DELETE an issue **(done)**
- GET the comments for an issue **(done)**
- GET an individual comment **(done)**
- POST a new comment on the issue **(done)**
- Update a comment **(done)**
- GET the components defined on an issue tracker **(done)**
- GET an individual component **(done)** 
- POST a new component in an issue tracker **(done)**
- Update an existing component **(done)**
- DELETE a component from the issue tracker **(done)**
- GET a list of versions **(done)**
- GET an individual version **(done)**
- POST a new version **(done)**
- PUT an update to a version **(done)**
- DELETE a version **(done)**
- GET the defined milestones **(done)**
- GET an individual milestone **(done)**
- POST a new milestone **(done)**
- PUT an update to milestones **(done)**
- DELETE a milestone **(done)**

### [Links Resources][16]
- GET list of links **(done)**
- GET a link **(done)**
- POST a new link **(done)**
- PUT an update to a link **(done)**
- DELETE a link **(done)**

### [Pullrequests Resource 1.0][17]
### [Repository Resource 1.0][18]
### [Services Resource][19]
### [Src Resources][20]
 - GET a list of repo source (listing of a directory content) **(done)**
 - GET content of a file **(done)**
 - GET raw content of a file

### [Wiki Resources][21]
- GET the raw content of a Wiki page  **(done)**
- POST a new page  **(seems bugged)**
- PUT a page update  **(seems bugged)**

### [Default-Reviewers Resource][29]
- PUT a default reviewer onto a repository

## [User endpoint][22] 
- GET a user profile **(done)**
- Update a user **(missing)**
- GET a list of user privileges**(done)**
- GET a list of repositories an account follows **(done)**
- GET a list of repositories visible to an account **(done)**
- GET a list of repositories the account is following **(done)**
- GET the list of repositories on the dashboard 

## [Privileges endpoint][23] 
## [Groups endpoint][24] 
## [Group privileges endpoint][25]
## [Invitations endpoint][26] 

# [Api v2][27]
This table list [all the routes of the API V2](https://developer.atlassian.com/bitbucket/api/2/reference/resource/), and for each route describe the corresponding methods in SharpBucket and if our implementation is covered with tests.

| Path | Methods | SharpBucket Methods | Tested
| ---- | ------- | ------------------- | ------
| /addon | `PUT` | none | no
| /addon | `DELETE` | none | no
| /addon/linkers | `GET` | none | no
| /addon/linkers/{linker_key} | `GET` | none | no
| /addon/linkers/{linker_key}/values | `GET` | none | no
| /addon/linkers/{linker_key}/values | `POST` | none | no
| /addon/linkers/{linker_key}/values | `PUT` | none | no
| /addon/linkers/{linker_key}/values | `DELETE` | none | no
| /addon/linkers/{linker_key}/values/ | `GET` | none | no
| /addon/linkers/{linker_key}/values/ | `DELETE` | none | no
| /addon/users/{target_user}/events/{event_key} | `POST` | none | no
| /hook_events | `GET` | none | no
| /hook_events/{subject_type} | `GET` | none | no
| /pullrequests/{target_user} | `GET` | none | no
| /repositories | `GET` | RepositoriesEndPoint.ListPublicRepositories(int) | yes
| /repositories/{username} | `GET` | RepositoriesEndPoint.ListRepositories(string,int) | yes
| /repositories/{username}/{repo_slug} | `GET` | RepositoryResource.GetRepository() | yes
| /repositories/{username}/{repo_slug} | `POST` | RepositoryResource.PostRepository(Repository) | yes
| /repositories/{username}/{repo_slug} | `PUT` | none | no
| /repositories/{username}/{repo_slug} | `DELETE` | RepositoryResource.DeleteRepository() | yes
| /repositories/{username}/{repo_slug}/branch-restrictions | `GET` | RepositoryResource.ListBranchRestrictions() | no
| /repositories/{username}/{repo_slug}/branch-restrictions | `POST` | RepositoryResource.PostBranchRestriction(BranchRestriction) | no
| /repositories/{username}/{repo_slug}/branch-restrictions/{id} | `GET` | RepositoryResource.GetBranchRestriction(int) | no
| /repositories/{username}/{repo_slug}/branch-restrictions/{id} | `PUT` | RepositoryResource.PutBranchRestriction(BranchRestriction) | no
| /repositories/{username}/{repo_slug}/branch-restrictions/{id} | `DELETE` | RepositoryResource.DeleteBranchRestriction(int) | no
| /repositories/{username}/{repo_slug}/commit/{node} | `GET` | RepositoryResource.GetCommit(string) | no
| /repositories/{username}/{repo_slug}/commit/{node}/approve | `POST` | RepositoryResource.ApproveCommit(string) | yes
| /repositories/{username}/{repo_slug}/commit/{node}/approve | `DELETE` | RepositoryResource.DeleteCommitApproval(string) | yes
| /repositories/{username}/{repo_slug}/commit/{node}/comments | `GET` | RepositoryResource.ListCommitComments(string) | no
| /repositories/{username}/{repo_slug}/commit/{node}/comments | `POST` | none | no
| /repositories/{username}/{repo_slug}/commit/{node}/comments/{comment_id} | `GET` | RepositoryResource.GetCommitComment(string,int) | no
| /repositories/{username}/{repo_slug}/commit/{node}/statuses | `GET` | none | no
| /repositories/{username}/{repo_slug}/commit/{node}/statuses/build | `POST` | RepositoryResource.AddNewBuildStatus(string,BuildInfo) | no
| /repositories/{username}/{repo_slug}/commit/{node}/statuses/build/{key} | `GET` | RepositoryResource.GetBuildStatusInfo(string,string) | no
| /repositories/{username}/{repo_slug}/commit/{node}/statuses/build/{key} | `PUT` | RepositoryResource.ChangeBuildStatusInfo(string,string,BuildInfo) | no
| /repositories/{username}/{repo_slug}/commits | `GET` | RepositoryResource.ListCommits(int) | yes
| /repositories/{username}/{repo_slug}/commits | `POST` | none | no
| /repositories/{username}/{repo_slug}/commits/{revision} | `GET` | RepositoryResource.ListCommits(string,int) | no
| /repositories/{username}/{repo_slug}/commits/{revision} | `POST` | none | no
| /repositories/{username}/{repo_slug}/components | `GET` | none | no
| /repositories/{username}/{repo_slug}/components/{component_id} | `GET` | none | no
| /repositories/{username}/{repo_slug}/default-reviewers | `GET` | none | no
| /repositories/{username}/{repo_slug}/default-reviewers/{target_username} | `GET` | none | no
| /repositories/{username}/{repo_slug}/default-reviewers/{target_username} | `PUT` | RepositoryResource.PutDefaultReviewer(string) | no
| /repositories/{username}/{repo_slug}/default-reviewers/{target_username} | `DELETE` | none | no
| /repositories/{username}/{repo_slug}/deployments/ | `GET` | none | no
| /repositories/{username}/{repo_slug}/deployments/{deployment_uuid} | `GET` | none | no
| /repositories/{username}/{repo_slug}/diff/{spec} | `GET` | RepositoryResource.GetDiff(object) | no
| /repositories/{username}/{repo_slug}/diffstat/{spec} | `GET` | none | no
| /repositories/{username}/{repo_slug}/downloads | `GET` | none | no
| /repositories/{username}/{repo_slug}/downloads | `POST` | none | no
| /repositories/{username}/{repo_slug}/downloads/{filename} | `GET` | none | no
| /repositories/{username}/{repo_slug}/downloads/{filename} | `DELETE` | none | no
| /repositories/{username}/{repo_slug}/environments/ | `GET` | none | no
| /repositories/{username}/{repo_slug}/environments/{environment_uuid} | `GET` | none | no
| /repositories/{username}/{repo_slug}/environments/{environment_uuid}/changes/ | `POST` | none | no
| /repositories/{username}/{repo_slug}/filehistory/{node}/{path} | `GET` | none | no
| /repositories/{username}/{repo_slug}/forks | `GET` | RepositoryResource.ListForks() | yes
| /repositories/{username}/{repo_slug}/forks | `POST` | none | no
| /repositories/{username}/{repo_slug}/hooks | `GET` | none | no
| /repositories/{username}/{repo_slug}/hooks | `POST` | none | no
| /repositories/{username}/{repo_slug}/hooks/{uid} | `GET` | none | no
| /repositories/{username}/{repo_slug}/hooks/{uid} | `PUT` | none | no
| /repositories/{username}/{repo_slug}/hooks/{uid} | `DELETE` | none | no
| /repositories/{username}/{repo_slug}/issues | `GET` `POST` | none | no
| /repositories/{username}/{repo_slug}/issues/{issue_id} | `GET` `PUT` `DELETE` | none | no
| /repositories/{username}/{repo_slug}/issues/{issue_id}/attachments | `GET` `POST` | none | no
| /repositories/{username}/{repo_slug}/issues/{issue_id}/attachments/{path} | `GET` `DELETE` | none | no
| /repositories/{username}/{repo_slug}/issues/{issue_id}/changes | `GET` `POST` | none | no
| /repositories/{username}/{repo_slug}/issues/{issue_id}/changes/{change_id} | `GET` | none | no
| /repositories/{username}/{repo_slug}/issues/{issue_id}/comments | `GET` `POST` | none | no
| /repositories/{username}/{repo_slug}/issues/{issue_id}/comments/{comment_id} | `GET` `PUT` `DELETE` | none | no
| /repositories/{username}/{repo_slug}/issues/{issue_id}/vote | `GET` `PUT` `DELETE` | none | no
| /repositories/{username}/{repo_slug}/issues/{issue_id}/watch | `GET` `PUT` `DELETE` | none | no
| /repositories/{username}/{repo_slug}/milestones | `GET` | none | no
| /repositories/{username}/{repo_slug}/milestones/{milestone_id} | `GET` | none | no
| /repositories/{username}/{repo_slug}/patch/{spec} | `GET` | RepositoryResource.GetDiff(GetPatch) | no
| /repositories/{username}/{repo_slug}/pipelines/ | `GET` `POST` | none | no
| /repositories/{username}/{repo_slug}/pipelines/{pipeline_uuid} | `GET` | none | no
| /repositories/{username}/{repo_slug}/pipelines/{pipeline_uuid}/steps/ | `GET` | none | no
| /repositories/{username}/{repo_slug}/pipelines/{pipeline_uuid}/steps/{step_uuid} | `GET` | none | no
| /repositories/{username}/{repo_slug}/pipelines/{pipeline_uuid}/steps/{step_uuid}/log | `GET` | none | no
| /repositories/{username}/{repo_slug}/pipelines/{pipeline_uuid}/stopPipeline | `POST` | none | no
| /repositories/{username}/{repo_slug}/pipelines_config | `GET` `PUT` | none | no
| /repositories/{username}/{repo_slug}/pipelines_config/build_number | `PUT` | none | no
| /repositories/{username}/{repo_slug}/pipelines_config/schedules/ | `GET` `POST` | none | no
| /repositories/{username}/{repo_slug}/pipelines_config/schedules/{schedule_uuid} | `GET` `PUT` `DELETE` | none | no
| /repositories/{username}/{repo_slug}/pipelines_config/schedules/{schedule_uuid}/executions/ | `GET` | none | no
| /repositories/{username}/{repo_slug}/pipelines_config/ssh/key_pair | `GET` `PUT` `DELETE` | none | no
| /repositories/{username}/{repo_slug}/pipelines_config/ssh/known_hosts/ | `GET` `POST` | none | no
| /repositories/{username}/{repo_slug}/pipelines_config/ssh/known_hosts/{known_host_uuid} | `GET` `PUT` `DELETE` | none | no
| /repositories/{username}/{repo_slug}/pipelines_config/variables/ | `GET` `POST` | none | no
| /repositories/{username}/{repo_slug}/pipelines_config/variables/{variable_uuid} | `GET` `PUT` `DELETE` | none | no
| /repositories/{username}/{repo_slug}/properties/{app_key}/{property_name} | `GET` `PUT` `DELETE` | none | no
| /repositories/{username}/{repo_slug}/pullrequests | `GET` | PullRequestsResource.ListPullRequests(int) | yes
| /repositories/{username}/{repo_slug}/pullrequests | `POST` | PullRequestsResource.PostPullRequest(PullRequest) | no
| /repositories/{username}/{repo_slug}/pullrequests/activity | `GET` | PullRequestsResource.GetPullRequestLog() | yes
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id} | `GET` | PullRequestResource.GetPullRequest() | yes
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id} | `PUT` | none | no
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/activity | `GET` | PullRequestResource.GetPullRequestActivity() | yes
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/approve | `POST` | PullRequestResource.ApprovePullRequest() | no
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/approve | `DELETE` | PullRequestResource.RemovePullRequestApproval() | no
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/comments | `GET` | PullRequestResource.ListPullRequestComments() | yes
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/comments | `POST` | PullRequestResource.PostPullRequestComment(Comment) | no
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/comments/{comment_id} | `GET` | PullRequestResource.GetPullRequestComment(int) | yes
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/comments/{comment_id} | `PUT` | none | no
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/comments/{comment_id} | `DELETE` | none | no
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/commits | `GET` | PullRequestResource.ListPullRequestCommits() | yes
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/decline | `POST` | PullRequestResource.DeclinePullRequest() | no
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/diff | `GET` | PullRequestResource.GetDiffForPullRequest() | yes
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/diffstat | `GET` | none | no
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/merge | `POST` | PullRequestResource.AcceptAndMergePullRequest() | no
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/patch | `GET` | none | no
| /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/statuses | `GET` | none | no
| /repositories/{username}/{repo_slug}/refs | `GET` | none | no
| /repositories/{username}/{repo_slug}/refs/branches | `GET` | BranchResource.ListBranches() | yes
| /repositories/{username}/{repo_slug}/refs/branches | `POST` | none | no
| /repositories/{username}/{repo_slug}/refs/branches/{name} | `GET` `DELETE` | none | no
| /repositories/{username}/{repo_slug}/refs/tags | `GET` | TagResource.ListTags() | no
| /repositories/{username}/{repo_slug}/refs/tags | `POST` | none | no
| /repositories/{username}/{repo_slug}/refs/tags/{name} | `GET` `DELETE` | none | no
| /repositories/{username}/{repo_slug}/src | `GET` `POST` | none | no
| /repositories/{username}/{repo_slug}/src/{node}/{path} | `GET` | none | no
| /repositories/{username}/{repo_slug}/versions | `GET` | none | no
| /repositories/{username}/{repo_slug}/versions/{version_id} | `GET` | none | no
| /repositories/{username}/{repo_slug}/watchers | `GET` | RepositoryResource.ListWatchers() | yes
| /snippets | `GET` `POST` | none | no
| /snippets/{username} | `GET` `POST` | none | no
| /snippets/{username}/{encoded_id} | `GET` `PUT` `DELETE` | none | no
| /snippets/{username}/{encoded_id}/comments | `GET` `POST` | none | no
| /snippets/{username}/{encoded_id}/comments/{comment_id} | `GET` `PUT` `DELETE` | none | no
| /snippets/{username}/{encoded_id}/commits | `GET` | none | no
| /snippets/{username}/{encoded_id}/commits/{revision} | `GET` | none | no
| /snippets/{username}/{encoded_id}/files/{path} | `GET` | none | no
| /snippets/{username}/{encoded_id}/watch | `GET` `PUT` `DELETE` | none | no
| /snippets/{username}/{encoded_id}/watchers | `GET` | none | no
| /snippets/{username}/{encoded_id}/{node_id} | `GET` `PUT` `DELETE` | none | no
| /snippets/{username}/{encoded_id}/{node_id}/files/{path} | `GET` | none | no
| /snippets/{username}/{encoded_id}/{revision}/diff | `GET` | none | no
| /snippets/{username}/{encoded_id}/{revision}/patch | `GET` | none | no
| /teams | `GET` | TeamsEndPoint.GetUserTeams(int) | yes
| /teams/{username} | `GET` | TeamsEndPoint.GetProfile() | yes
| /teams/{username}/followers | `GET` | TeamsEndPoint.ListFollowers(int) | yes
| /teams/{username}/following | `GET` | TeamsEndPoint.ListFollowing(int) | no
| /teams/{username}/hooks | `GET` `POST` | none | no
| /teams/{username}/hooks/{uid} | `GET` `PUT` `DELETE` | none | no
| /teams/{username}/members | `GET` | TeamsEndPoint.ListMembers(int) | yes
| /teams/{username}/permissions | `GET` | none | no
| /teams/{username}/permissions/repositories | `GET` | none | no
| /teams/{username}/pipelines_config/variables/ | `GET` `POST` | none | no
| /teams/{username}/pipelines_config/variables/{variable_uuid} | `GET` `PUT` `DELETE` | none | no
| /teams/{username}/projects/ | `GET` `POST` | none | no
| /teams/{username}/projects/{project_key} | `GET` `PUT` `DELETE` | none | no
| /teams/{username}/repositories | `GET` | TeamsEndPoint.ListRepositories(int) | yes
| /teams/{username}/search/code | `GET` | none | no
| /user | `GET` | UserEndpoint.GetUser() | yes
| /user/emails | `GET` | none | no
| /user/emails/{email} | `GET` | none | no
| /user/permissions/repositories | `GET` | none | no
| /user/permissions/teams | `GET` | none | no
| /users/{username} | `GET` | UsersEndpoint.GetProfile() | yes
| /users/{username}/followers | `GET` | UsersEndpoint.ListFollowers(int) | yes
| /users/{username}/following | `GET` | UsersEndpoint.ListFollowing(int) | yes
| /users/{username}/hooks | `GET` `POST` | none | no
| /users/{username}/hooks/{uid} | `GET` `PUT` `DELETE` | none | no
| /users/{username}/members | `GET` | none | no
| /users/{username}/pipelines_config/variables/ | `GET` `POST` | none | no
| /users/{username}/pipelines_config/variables/{variable_uuid} | `GET` `PUT` `DELETE` | none | no
| /users/{username}/repositories | `GET` | UsersEndpoint.ListRepositories(int) | yes
| /users/{username}/search/code | `GET` | none | no
| /users/{username}/ssh-keys | `GET` `POST` | none | no
| /users/{username}/ssh-keys/ | `GET` `PUT` `DELETE` | none | no

# [Authentication][28] 
- Basic **(done)**
- Oauth 1.0a **(done)**
- Oauth 2 **(done)**



  [1]: https://confluence.atlassian.com/display/BITBUCKET/Version+1
  [2]: https://confluence.atlassian.com/display/BITBUCKET/users+Endpoint+-+1.0
  [3]: https://confluence.atlassian.com/display/BITBUCKET/account+Resource
  [4]: https://confluence.atlassian.com/display/BITBUCKET/consumers+Resource
  [5]: https://confluence.atlassian.com/display/BITBUCKET/emails+Resource
  [6]: https://confluence.atlassian.com/display/BITBUCKET/invitations+Resource
  [7]: https://confluence.atlassian.com/display/BITBUCKET/oauth+Resource
  [8]: https://confluence.atlassian.com/display/BITBUCKET/privileges+Resource
  [9]: https://confluence.atlassian.com/display/BITBUCKET/ssh-keys+Resource
  [10]: https://confluence.atlassian.com/display/BITBUCKET/repositories+Endpoint+-+1.0
  [11]: https://confluence.atlassian.com/display/BITBUCKET/changesets+Resource
  [12]: https://confluence.atlassian.com/display/BITBUCKET/deploy-keys+Resource
  [13]: https://confluence.atlassian.com/display/BITBUCKET/events+Resources
  [14]: https://confluence.atlassian.com/display/BITBUCKET/followers+Resource
  [15]: https://confluence.atlassian.com/display/BITBUCKET/issues+Resource
  [16]: https://confluence.atlassian.com/display/BITBUCKET/links+Resources
  [17]: https://confluence.atlassian.com/display/BITBUCKET/pullrequests+Resource+1.0
  [18]: https://confluence.atlassian.com/display/BITBUCKET/repository+Resource+1.0
  [19]: https://confluence.atlassian.com/display/BITBUCKET/services+Resource
  [20]: https://confluence.atlassian.com/display/BITBUCKET/src+Resources
  [21]: https://confluence.atlassian.com/display/BITBUCKET/wiki+Resources
  [22]: https://confluence.atlassian.com/display/BITBUCKET/user+Endpoint
  [23]: https://confluence.atlassian.com/display/BITBUCKET/privileges+Endpoint
  [24]: https://confluence.atlassian.com/display/BITBUCKET/groups+Endpoint
  [25]: https://confluence.atlassian.com/display/BITBUCKET/group-privileges+Endpoint
  [26]: https://confluence.atlassian.com/display/BITBUCKET/invitations+Endpoint
  [27]: https://confluence.atlassian.com/display/BITBUCKET/Version+2
  [28]: https://confluence.atlassian.com/display/BITBUCKET/OAuth+on+Bitbucket
  [29]: https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/default-reviewers/%7Btarget_username%7D
  