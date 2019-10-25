# [Authentication](https://developer.atlassian.com/bitbucket/api/2/reference/meta/authentication)

- Basic
  - Implemented by: `SharpBucket.BasicAuthentication(string username, string password)`
  - Tested: **yes**
- OAuth 1.0 (*warning*: OAuth1 is obsolete and [documentation](https://confluence.atlassian.com/display/BITBUCKET/OAuth+on+Bitbucket) do not exists anymore)
  - 2-LO
    - Implemented by: `SharpBucket.OAuth1TwoLeggedAuthentication(string consumerKey, string consumerSecretKey)`
    - Tested: **yes**
  - 3-LO
    - Implemented by: `SharpBucket.OAuth1ThreeLeggedAuthentication(string consumerKey, string consumerSecretKey, string callback = "oob")` and then `SharpBucket.OAuth1ThreeLeggedAuthentication(string consumerKey, string consumerSecretKey, string oauthToken, string oauthTokenSecret)`
    - Tested: no
- OAuth 2
  1. Authorization Code Grant (4.1)
     - Implemented by: none. Since it's OAuth2 equivalent of the the OAuth1 3-LO we had implemented, we probably need to implement that one too.
     - Tested: no
  2. Implicit Grant (4.2)
     - Seems useful only for Javascript SPA applications and will not fit in a C# client.
  3. Resource Owner Password Credentials Grant (4.3)
     - Implemented by: none. No idea if needed.
     - Tested: no
  4. Client Credentials Grant (4.4)
     - Implemented by: `SharpBucket.OAuth2ClientCredentials(string consumerKey, string consumerSecretKey)`
     - Tested: **yes**
  5. Bitbucket Cloud JWT Grant (urn:bitbucket:oauth2:jwt)
     - Implemented by: none. No idea if needed.
     - Tested: no
  - Repository Cloning
    - This require to expose the access token for external usages (git calls instead of http calls managed by that project).
      It's currently discussed in [#111](https://github.com/MitjaBezensek/SharpBucket/issues/111)

# [Api v2](https://developer.atlassian.com/bitbucket/api/2/reference/)
Here we have listed [all the routes of the API V2](https://developer.atlassian.com/bitbucket/api/2/reference/resource/), and for each route describe the corresponding methods in SharpBucket and if our implementation is covered with tests.

*nb: The names of the end points and resources in this document are an exact reflect of the URLs organization.  
The organization of all that routes may slightly differ in the SharpBucket classes.*

## [Addon endpoint](https://developer.atlassian.com/bitbucket/api/2/reference/resource/addon)
- /addon `PUT`
  - Implemented by: none
  - Tested: no
- /addon `DELETE`
  - Implemented by: none
  - Tested: no
- /addon/linkers `GET`
  - Implemented by: none
  - Tested: no
- /addon/users/{target_user}/events/{event_key} `POST`
  - Implemented by: none
  - Tested: no

### [Linkers ressource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/addon/linkers)
- /addon/linkers/{linker_key} `GET`
  - Implemented by: none
  - Tested: no
- /addon/linkers/{linker_key}/values `GET`
  - Implemented by: none
  - Tested: no
- /addon/linkers/{linker_key}/values `POST`
  - Implemented by: none
  - Tested: no
- /addon/linkers/{linker_key}/values `PUT`
  - Implemented by: none
  - Tested: no
- /addon/linkers/{linker_key}/values `DELETE`
  - Implemented by: none
  - Tested: no

## [Hook events endpoint](https://developer.atlassian.com/bitbucket/api/2/reference/resource/hook_events)
- /hook_events `GET`
  - Implemented by: none
  - Tested: no
- /hook_events/{subject_type} `GET`
  - Implemented by: none
  - Tested: no

## [Pull requests endpoint](https://developer.atlassian.com/bitbucket/api/2/reference/resource/pullrequests)
- /pullrequests/{target_user} `GET`
  - Implemented by: none
  - Tested: no

## [Repositories endpoint](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories)
- /repositories `GET`
  - Implemented by: `RepositoriesEndPoint.ListPublicRepositories(int)`
  - Tested: **yes**
- /repositories/{username} `GET`
  - Implemented by:
    - `RepositoriesEndPoint.ListRepositories(string)`
    - `RepositoriesEndPoint.ListRepositories(string,ListParameters)`
  - Tested: **yes**

### [Repository resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D)
- /repositories/{username}/{repo_slug} `GET`
  - Implemented by: `RepositoryResource.GetRepository()`
  - Tested: **yes**
- /repositories/{username}/{repo_slug} `POST`
  - Implemented by: `RepositoryResource.PostRepository(Repository)`
  - Tested: **yes**
- /repositories/{username}/{repo_slug} `PUT`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug} `DELETE`
  - Implemented by: `RepositoryResource.DeleteRepository()`
  - Tested: **yes**

### [Branch restrictions resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/branch-restrictions)
- /repositories/{username}/{repo_slug}/branch-restrictions `GET`
  - Implemented by: `RepositoryResource.ListBranchRestrictions()`
  - Tested: no
- /repositories/{username}/{repo_slug}/branch-restrictions `POST`
  - Implemented by: `RepositoryResource.PostBranchRestriction(BranchRestriction)`
  - Tested: no
- /repositories/{username}/{repo_slug}/branch-restrictions/{id} `GET`
  - Implemented by: `RepositoryResource.GetBranchRestriction(int)`
  - Tested: no
- /repositories/{username}/{repo_slug}/branch-restrictions/{id} `PUT`
  - Implemented by: `RepositoryResource.PutBranchRestriction(BranchRestriction)`
  - Tested: no
- /repositories/{username}/{repo_slug}/branch-restrictions/{id} `DELETE`
  - Implemented by: `RepositoryResource.DeleteBranchRestriction(int)`
  - Tested: no

### [Commit resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/commit)
- /repositories/{username}/{repo_slug}/commit/{node} `GET`
  - Implemented by: `RepositoryResource.GetCommit(string)`
  - Tested: no
- /repositories/{username}/{repo_slug}/commit/{node}/approve `POST`
  - Implemented by: `RepositoryResource.ApproveCommit(string)`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/commit/{node}/approve `DELETE`
  - Implemented by: `RepositoryResource.DeleteCommitApproval(string)`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/commit/{node}/comments `GET`
  - Implemented by: `RepositoryResource.ListCommitComments(string)`
  - Tested: no
- /repositories/{username}/{repo_slug}/commit/{node}/comments `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/commit/{node}/comments/{comment_id} `GET`
  - Implemented by: `RepositoryResource.GetCommitComment(string,int)`
  - Tested: no
- /repositories/{username}/{repo_slug}/commit/{node}/statuses `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/commit/{node}/statuses/build `POST`
  - Implemented by: `RepositoryResource.AddNewBuildStatus(string,BuildInfo)`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/commit/{node}/statuses/build/{key} `GET`
  - Implemented by: `RepositoryResource.GetBuildStatusInfo(string,string)`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/commit/{node}/statuses/build/{key} `PUT`
  - Implemented by: `RepositoryResource.ChangeBuildStatusInfo(string,string,BuildInfo)`
  - Tested: **yes**

### [Commits resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/commits)
- /repositories/{username}/{repo_slug}/commits `GET`
  - Implemented by:
    - `RepositoryResource.ListCommits()`
    - `RepositoryResource.ListCommits(max:int)`
    - `RepositoryResource.ListCommits(CommitsParameters)`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/commits `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/commits/{revision} `GET`
  - Implemented by:
    - `RepositoryResource.ListCommits(string)`
    - `RepositoryResource.ListCommits(string,int)`
    - `RepositoryResource.ListCommits(string,CommitsParameters)`
  - Tested: no
- /repositories/{username}/{repo_slug}/commits/{revision} `POST`
  - Implemented by: none
  - Tested: no

### [Components resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/components)
- /repositories/{username}/{repo_slug}/components `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/components/{component_id} `GET`
  - Implemented by: none
  - Tested: no

### [Default reviewers resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/default-reviewers)
- /repositories/{username}/{repo_slug}/default-reviewers `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/default-reviewers/{target_username} `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/default-reviewers/{target_username} `PUT`
  - Implemented by: `RepositoryResource.PutDefaultReviewer(string)`
  - Tested: no
- /repositories/{username}/{repo_slug}/default-reviewers/{target_username} `DELETE`
  - Implemented by: none
  - Tested: no

### [Deployments resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/deployments)
- /repositories/{username}/{repo_slug}/deployments/ `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/deployments/{deployment_uuid} `GET`
  - Implemented by: none
  - Tested: no

### [Diff resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/diff)
- /repositories/{username}/{repo_slug}/diff/{spec} `GET`
  - Implemented by:
    - `RepositoryResource.GetDiff(string)` *V0.9.0*
    - `RepositoryResource.GetDiff(string,DiffParameters)` *V0.9.0*
  - Tested: no

### [Diffstat resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/diffstat)
- /repositories/{username}/{repo_slug}/diffstat/{spec} `GET`
  - Implemented by: none
  - Tested: no

### [Downloads resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/downloads)
- /repositories/{username}/{repo_slug}/downloads `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/downloads `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/downloads/{filename} `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/downloads/{filename} `DELETE`
  - Implemented by: none
  - Tested: no

### [Environments resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/environments)
- /repositories/{username}/{repo_slug}/environments/ `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/environments/{environment_uuid} `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/environments/{environment_uuid}/changes/ `POST`
  - Implemented by: none
  - Tested: no

### [File History resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/filehistory)
- /repositories/{username}/{repo_slug}/filehistory/{node}/{path} `GET`
  - Implemented by: none
  - Tested: no

### [Forks resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/forks)
- /repositories/{username}/{repo_slug}/forks `GET`
  - Implemented by: `RepositoryResource.ListForks()`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/forks `POST`
  - Implemented by: none
  - Tested: no

### [hooks resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/hooks)
- /repositories/{username}/{repo_slug}/hooks `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/hooks `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/hooks/{uid} `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/hooks/{uid} `PUT`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/hooks/{uid} `DELETE`
  - Implemented by: none
  - Tested: no

### [Issues resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/issues)
- /repositories/{username}/{repo_slug}/issues `GET` `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/issues/{issue_id} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/issues/{issue_id}/attachments `GET` `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/issues/{issue_id}/attachments/{path} `GET` `DELETE`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/issues/{issue_id}/changes `GET` `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/issues/{issue_id}/changes/{change_id} `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/issues/{issue_id}/comments `GET` `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/issues/{issue_id}/comments/{comment_id} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/issues/{issue_id}/vote `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/issues/{issue_id}/watch `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no

### [Milestones resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/milestones)
- /repositories/{username}/{repo_slug}/milestones `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/milestones/{milestone_id} `GET`
  - Implemented by: none
  - Tested: no

### [Patch resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/patch/%7Bspec%7D)
- /repositories/{username}/{repo_slug}/patch/{spec} `GET`
  - Implemented by:
    - `RepositoryResource.GetPatch(string)` *V0.9.0*
  - Tested: no

### [Pipelines resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/pipelines)
- /repositories/{username}/{repo_slug}/pipelines/ `GET` `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines/{pipeline_uuid} `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines/{pipeline_uuid}/steps/ `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines/{pipeline_uuid}/steps/{step_uuid} `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines/{pipeline_uuid}/steps/{step_uuid}/log `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines/{pipeline_uuid}/stopPipeline `POST`
  - Implemented by: none
  - Tested: no

### [Pipelines config resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/pipelines_config)
- /repositories/{username}/{repo_slug}/pipelines_config `GET` `PUT`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines_config/build_number `PUT`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines_config/schedules/ `GET` `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines_config/schedules/{schedule_uuid} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines_config/schedules/{schedule_uuid}/executions/ `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines_config/ssh/key_pair `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines_config/ssh/known_hosts/ `GET` `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines_config/ssh/known_hosts/{known_host_uuid} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines_config/variables/ `GET` `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pipelines_config/variables/{variable_uuid} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no

### [Property resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/properties/%7Bapp_key%7D/%7Bproperty_name%7D)
- /repositories/{username}/{repo_slug}/properties/{app_key}/{property_name} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no

### [Pull requests resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/pullrequests)
- /repositories/{username}/{repo_slug}/pullrequests `GET`
  - Implemented by: `PullRequestsResource.ListPullRequests(int)`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/pullrequests `POST`
  - Implemented by: `PullRequestsResource.PostPullRequest(PullRequest)`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/pullrequests/activity `GET`
  - Implemented by: `PullRequestsResource.GetPullRequestLog()`
  - Tested: **yes**

### [Pull request resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/pullrequests/%7Bpull_request_id%7D)
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id} `GET`
  - Implemented by: `PullRequestResource.GetPullRequest()`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id} `PUT`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/activity `GET`
  - Implemented by: `PullRequestResource.GetPullRequestActivity()`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/approve `POST`
  - Implemented by: `PullRequestResource.ApprovePullRequest()`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/approve `DELETE`
  - Implemented by: `PullRequestResource.RemovePullRequestApproval()`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/comments `GET`
  - Implemented by: `PullRequestResource.ListPullRequestComments()`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/comments `POST`
  - Implemented by: `PullRequestResource.PostPullRequestComment(Comment)`
  - Tested: no
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/comments/{comment_id} `GET`
  - Implemented by: `PullRequestResource.GetPullRequestComment(int)`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/comments/{comment_id} `PUT`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/comments/{comment_id} `DELETE`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/commits `GET`
  - Implemented by: `PullRequestResource.ListPullRequestCommits()`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/decline `POST`
  - Implemented by: `PullRequestResource.DeclinePullRequest()`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/diff `GET`
  - Implemented by: `PullRequestResource.GetDiffForPullRequest()`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/diffstat `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/merge `POST`
  - Implemented by: `PullRequestResource.AcceptAndMergePullRequest()`
  - Tested: no
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/patch `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/pullrequests/{pull_request_id}/statuses `GET`
  - Implemented by: none
  - Tested: no

### [Ref resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/refs)
- /repositories/{username}/{repo_slug}/refs `GET`
  - Implemented by: none
  - Tested: no

### [Branch resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/refs/branches/%7Bname%7D)
- /repositories/{username}/{repo_slug}/refs/branches `GET`
  - Implemented by: `BranchResource.ListBranches()`
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/refs/branches `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/refs/branches/{name} `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/refs/branches/{name} `DELETE`
  - Implemented by: `BranchResource.DeleteBranch(string)`
  - Tested: **yes**

### [Tag resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/refs/tags/%7Bname%7D)
- /repositories/{username}/{repo_slug}/refs/tags `GET`
  - Implemented by: `TagResource.ListTags()`
  - Tested: no
- /repositories/{username}/{repo_slug}/refs/tags `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/refs/tags/{name} `GET` `DELETE`
  - Implemented by: none
  - Tested: no

### [Src resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/src)
- /repositories/{username}/{repo_slug}/src `POST`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/src `GET`
  - Implemented by:
    - `SrcResource.ctor` (when revision parameter is not specified) *V0.9.0*
  - Tested: **yes**
- /repositories/{username}/{repo_slug}/src/{node}/{path} `GET`
  - Implemented by:
    - `SrcResource.ListSrcEntries(string,ListParameters)` *V0.9.0*
    - `SrcResource.GetSrcEntry(string)` *V0.9.0*
    - `SrcResource.GetSrcFile(string)` *V0.9.0*
    - `SrcResource.GetSrcDirectory(string)` *V0.9.0*
    - `SrcResource.GetFileContent(string)` *V0.9.0*
    - `SrcResource.ListTreeEntries(string,ListParameters)` *V0.9.0*
    - `SrcResource.GetTreeEntry(string)` *V0.9.0*
  - Tested: **yes**

### [Versions resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/versions)
- /repositories/{username}/{repo_slug}/versions `GET`
  - Implemented by: none
  - Tested: no
- /repositories/{username}/{repo_slug}/versions/{version_id} `GET`
  - Implemented by: none
  - Tested: no

### [Watchers resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/watchers)
- /repositories/{username}/{repo_slug}/watchers `GET`
  - Implemented by: `RepositoryResource.ListWatchers()`
  - Tested: **yes**

## [Snippets endpoint](https://developer.atlassian.com/bitbucket/api/2/reference/resource/snippets)
- /snippets `GET` `POST`
  - Implemented by: none
  - Tested: no
- /snippets/{username} `GET` `POST`
  - Implemented by: none
  - Tested: no
- /snippets/{username}/{encoded_id} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /snippets/{username}/{encoded_id}/comments `GET` `POST`
  - Implemented by: none
  - Tested: no
- /snippets/{username}/{encoded_id}/comments/{comment_id} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /snippets/{username}/{encoded_id}/commits `GET`
  - Implemented by: none
  - Tested: no
- /snippets/{username}/{encoded_id}/commits/{revision} `GET`
  - Implemented by: none
  - Tested: no
- /snippets/{username}/{encoded_id}/files/{path} `GET`
  - Implemented by: none
  - Tested: no
- /snippets/{username}/{encoded_id}/watch `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /snippets/{username}/{encoded_id}/watchers `GET`
  - Implemented by: none
  - Tested: no
- /snippets/{username}/{encoded_id}/{node_id} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /snippets/{username}/{encoded_id}/{node_id}/files/{path} `GET`
  - Implemented by: none
  - Tested: no
- /snippets/{username}/{encoded_id}/{revision}/diff `GET`
  - Implemented by: none
  - Tested: no
- /snippets/{username}/{encoded_id}/{revision}/patch `GET`
  - Implemented by: none
  - Tested: no

## [Teams endpoint](https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams)
- /teams `GET`
  - Implemented by:
    - `TeamsEndPoint.GetUserTeams(int)`
    - `TeamsEndPoint.GetUserTeamsWithContributorRole(int)`
    - `TeamsEndPoint.GetUserTeamsWithAdminRole(int)`
  - Tested: **yes**

### [Team Resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D)
- /teams/{username} `GET`
  - Implemented by:
    - `TeamResource.GetProfile()` *v0.9.0*
    - `[obsolete]TeamsEndPoint.GetProfile()`
  - Tested: **yes**
- /teams/{username}/followers `GET`
  - Implemented by:
    - `TeamResource.ListFollowers(int)` *V0.9.0*
    - `[obsolete]TeamsEndPoint.ListFollowers(int)`
  - Tested: **yes**
- /teams/{username}/following `GET`
  - Implemented by:
    - `TeamResource.ListFollowing(int)` *V0.9.0*
    - `[obsolete]TeamsEndPoint.ListFollowing(int)`
  - Tested: no
- /teams/{username}/hooks `GET` `POST`
  - Implemented by: none
  - Tested: no
- /teams/{username}/hooks/{uid} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /teams/{username}/members `GET`
  - Implemented by:
    - `TeamResource.ListMembers(int)` *V0.9.0*
    - `[obsolete]TeamsEndPoint.ListMembers(int)`
  - Tested: **yes**
- /teams/{username}/permissions `GET`
  - Implemented by: none
  - Tested: no
- /teams/{username}/permissions/repositories `GET`
  - Implemented by: none
  - Tested: no
- /teams/{username}/pipelines_config/variables/ `GET` `POST`
  - Implemented by: none
  - Tested: no
- /teams/{username}/pipelines_config/variables/{variable_uuid} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /teams/{username}/projects/ `GET`
  - Implemented by: `ListProjects(int)` *V0.9.0*
  - Tested: **yes**
- /teams/{username}/projects/ `POST`
  - Implemented by: `PostProject(int)` *V0.9.0*
  - Tested: **yes**
- /teams/{username}/repositories `GET`
  - Implemented by:
    - `TeamResource.ListRepositories(ListParameters)` *V0.9.0*
    - `[obsolete]TeamsEndPoint.ListRepositories(int)`
  - Tested: **yes**
- /teams/{username}/search/code `GET`
  - Implemented by: `TeamResource.EnumerateSearchCodeSearchResults(string,int)`
  - Tested: **yes**

### [Project Resource](https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/projects/%7Bproject_key%7D)
- /teams/{username}/projects/{project_key} `GET`
  - Implemented by: `ProjectResource.GetProject()` *V0.9.0*
  - Tested: **yes**
- /teams/{username}/projects/{project_key} `PUT`
  - Implemented by: `ProjectResource.PutProject(Project)` *V0.9.0*
  - Tested: **yes**
- /teams/{username}/projects/{project_key} `DELETE`
  - Implemented by: `ProjectResource.DeleteProject()` *V0.9.0*
  - Tested: **yes**

## [User endpoint](https://developer.atlassian.com/bitbucket/api/2/reference/resource/user)
- /user `GET`
  - Implemented by: `UserEndpoint.GetUser()`
  - Tested: **yes**
- /user/emails `GET`
  - Implemented by: none
  - Tested: no
- /user/emails/{email} `GET`
  - Implemented by: none
  - Tested: no
- /user/permissions/repositories `GET`
  - Implemented by: none
  - Tested: no
- /user/permissions/teams `GET`
  - Implemented by: none
  - Tested: no

## [Users endpoint](https://developer.atlassian.com/bitbucket/api/2/reference/resource/users)
- /users/{username} `GET`
  - Implemented by: `UsersEndpoint.GetProfile()`
  - Tested: **yes**
- /users/{username}/followers `GET`
  - Implemented by: `UsersEndpoint.ListFollowers(int)`
  - Tested: **yes**
- /users/{username}/following `GET`
  - Implemented by: `UsersEndpoint.ListFollowing(int)`
  - Tested: **yes**
- /users/{username}/hooks `GET` `POST`
  - Implemented by: none
  - Tested: no
- /users/{username}/hooks/{uid} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /users/{username}/members `GET`
  - Implemented by: none
  - Tested: no
- /users/{username}/pipelines_config/variables/ `GET` `POST`
  - Implemented by: none
  - Tested: no
- /users/{username}/pipelines_config/variables/{variable_uuid} `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
- /users/{username}/repositories `GET`
  - Implemented by: `UsersEndpoint.ListRepositories(int)`
  - Tested: **yes**
- /users/{username}/search/code `GET`
  - Implemented by: none
  - Tested: no
- /users/{username}/ssh-keys `GET` `POST`
  - Implemented by: none
  - Tested: no
- /users/{username}/ssh-keys/ `GET` `PUT` `DELETE`
  - Implemented by: none
  - Tested: no
