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
It is quite limited compared to the API v1. So it has been postponed for later.

## [Authentication][28] 
- Oauth 1.0a **(done)**
- Basic **(done)**


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
  