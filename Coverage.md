# Api V1
## [Users endpoint][1]
### [Account Resource][2]
- GET the account profile **(done)**
- GET the account plan **(done)**
- GET the followers **(done)**
- GET the events **(done)**
### [Consumers Resource][3]
- GET a list of an account's consumers  **(done)**
- GET a consumer  **(done)**
- PUT an updated consumer
- DELETES a consumer
### [Emails Resource][4]
- GET a list of user's email addresses **(done)**
- GET an email address **(done)**
- POST a new email address **(done)**
- Update an email address **(done)**
### Invitations Resource
### Oauth Resource
### Privileges Resource
### [Ssh-keys Resource][5]
- GET the list of an accounts SSH keys **(done)**
- POST a new ssh key **(done)**
- GET a ssh key **(done)**
- DELETE a key
## [Repositories endpoint][6]
### Changesets Resource
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
### Deploy-keys Resource
### [Events Resources][7]
 - GET a list of events **(done)**
### [Followers Resource][8]
- Get a list of users that are following the repository **(done)**
### [Issues Resource][9]
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
### Links Resources
### Pullrequests Resource 1.0
### Repository Resource 1.0
### Services Resource
### Src Resources
### Wiki Resources
- GET the raw content of a Wiki page  **(done)**
- POST a new page  **(seems bugged)**
- PUT a page update  **(seems bugged)**
## [User endpoint][10] 
- GET a user profile **(done)**
- Update a user **(missing)**
- GET a list of user privileges**(done)**
- GET a list of repositories an account follows **(done)**
- GET a list of repositories visible to an account **(done)**
- GET a list of repositories the account is following **(done)**
- GET the list of repositories on the dashboard 


## [Privileges endpoint][11] 
- GET a list of individual user privileges granted on a repository **(missing)**
- GET privileges for an individual **(missing)**
- GET a list of all privileges across all an account's repositories **(missing)**
- PUT a new privilege **(missing)**
- DELETE account privileges from a repository **(missing)**
- DELETE all privileges from a repository **(missing)** 
- DELETE all privileges from all repositories **(missing)** 
## [Groups endpoint][12] (0/7)
- GET a list of matching groups **(missing)**
- GET a list of groups **(missing)**
- POST a new group **(missing)**
- Update a group **(missing)**
- DELETE a group **(missing)**
- GET the group members **(missing)**
- PUT new member into a group **(missing)**
- DELETE a member **(missing)**
## [Group privileges endpoint][13] (0/7)
- GET a list of privileged groups **(missing)**
- GET a list of privileged groups for a repository **(missing)**
- GET a group on a repository **(missing)**
- GET a list of repositories with a specific privilege group **(missing)**
- PUT group privileges on a repository **(missing)**
- DELETE group privileges from a repository **(missing)**
- DELETE privileges for a group across all your repositories **(missing)**

## [Invitations endpoint][14] 
- Sending an invite



# Api v2
It is quite limited compared to the API v1. So it has been postponed for later.

## Authentication 
- Oauth
- Basic (Finished)


  [1]: https://confluence.atlassian.com/display/BITBUCKET/users+Endpoint+-+1.0
  [2]: https://confluence.atlassian.com/display/BITBUCKET/account+Resource
  [3]: https://confluence.atlassian.com/display/BITBUCKET/consumers+Resource
  [4]: https://confluence.atlassian.com/display/BITBUCKET/emails+Resource
  [5]: https://confluence.atlassian.com/display/BITBUCKET/ssh-keys+Resource
  [6]: https://confluence.atlassian.com/display/BITBUCKET/repositories+Endpoint+-+1.0
  [7]: https://confluence.atlassian.com/display/BITBUCKET/events+Resources
  [8]: https://confluence.atlassian.com/display/BITBUCKET/followers+Resource
  [9]: https://confluence.atlassian.com/display/BITBUCKET/issues+Resource
  [10]: https://confluence.atlassian.com/display/BITBUCKET/user+Endpoint
  [11]: https://confluence.atlassian.com/display/BITBUCKET/privileges+Endpoint
  [12]: https://confluence.atlassian.com/display/BITBUCKET/groups+Endpoint
  [13]: https://confluence.atlassian.com/display/BITBUCKET/group-privileges+Endpoint
  [14]: https://confluence.atlassian.com/display/BITBUCKET/invitations+Endpoint