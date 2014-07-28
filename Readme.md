# SharpBucket
SharpBucket is a .Net wrapper for the BitBucket API. It is written in in C#. 

# How to get started
See the ConsoleTestProject to see how to use the wrapper. Here's just a brief demo:

First lets set your entry point to the API
```CSharp
// your main entry to the BitBucket API, this one is for V1
var sharpBucket = new SharpBucketV1();
// authenticate with the username and password (OAuth coming soon)
sharpBucket.BasicAuthentication(email, password);
```

There are various end points you can use. Lets take a look at User end point:
```CSharp
var user = sharpBucketV1.User();
var info = user.GetInfo();
var privileges = user.GetPrivileges();
var follows = user.ListFollows();
var userRepos = user.ListRepositories();
```

Similarly for the Issues end point:

```CSharp
 var issues = sharpBucketV1.Repository(accountName, repository).Issues();

var issues = issues.ListIssues();
var issueComments = issues.ListIssueComments(ISSUE_ID);
```
# Authentication
There are two ways you can authenticate with SharpBucket
- via the Oauth 1.0 (not implemented yet)
- via BitBucket's username and password.

# How much of the API is covered?
While a complete coverage of the API is preferred SharpBucket currently does not support everything yet. But the main functionality is covered and the rest should also get covered sooner or later.
# Contributing
Contributions are always welcome. You can us check the issues or look which api calls [are still missing](https://github.com/MitjaBezensek/SharpBucket/blob/master/Coverage.md).

# Licensing
SharpBucket is licensed with MIT license. It uses NServiceKit for requesting and parsing the data. NServiceKit is also licensed under MIT terms and is a open source fork of StackService.
