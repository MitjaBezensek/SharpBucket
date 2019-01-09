# SharpBucket
SharpBucket is a .Net wrapper for the Bitbucket's REST API. It is written in in C#. With it you can have all the data of your repositories / issues at your fingertips.

## How to get started
### Installation
To install SharpBucket, run the following command in the Package Manager Console:

    PM> Install-Package SharpBucket

### Usage
See the [Console Test Project](https://github.com/MitjaBezensek/SharpBucket/blob/master/ConsoleTests/Program.cs) to see how to use the wrapper. Here's just a brief demo:

First lets set your entry point to the API
```CSharp
// your main entry to the Bitbucket API, this one is for V1
var sharpBucket = new SharpBucketV1();
// authenticate with OAuth keys
sharpBucket.OAuth2LeggedAuthentication(consumerKey, consumerSecretKey);
```

There are various end points you can use. Lets take a look at User end point:
```CSharp
// getting the User end point
var userEndPoint = sharpBucket.UserEndPoint();
// querying the Bitbucket API for various info
var info = userEndPoint.GetInfo();
var privileges = userEndPoint.ListPrivileges();
var follows = userEndPoint.ListFollows();
var userRepos = userEndPoint.ListRepositories();
```

Similarly for the Issues resource, let's get all the issues of a specific repository:

```CSharp
// getting the Repository end point
var repositoryEndPoint = sharpBucket.RepositoriesEndPoint(accountName, repository);
// getting the Issue resource for this specific repository
var issuesResource = respositoryEndPoint.IssuesResource();
// getting the list of all the issues of the repository
var issues = issuesResource.ListIssues();
```
Sending information is just as easy.

```CSharp
var newIssue = new Issue{title = "I have this little bug", 
                         content = "that is really annoying",
                         status = "new"};
var newIssueResult = issuesResource.PostIssue(newIssue);
```

SharpBucket uses a strict naming convention:
- methods starting with List will return a collection of items (ListIssues() returns a list of issues)
- methods starting with Get will return an item (GetIssue(10) will return an issue with the id 10)
- methods starting with Post are used for adding the item
- methods starting with Put are used for updating the item
- methods starting with Delete will delete the item

## Authentication
There are three ways you can authenticate with SharpBucket
- via Oauth 2, which is preferred
- via Oauth 1.0a
- via Bitbucket's username and password

Here is how you can use them:
### Basic authentication
```CSharp
// authenticate with username and password
sharpBucket.BasicAuthentication(email, password);
```

### OAuth authentication
With OAuth you can choose between [2 legged and 3 legged authentication](http://cakebaker.42dh.com/2011/01/10/2-legged-vs-3-legged-oauth/).

**Two legged** is as simple as basic authentication:
```CSharp
// authenticate with OAuth keys
sharpBucket.OAuth2LeggedAuthentication(consumerKey, consumerSecretKey);
```
**The three legged** one requires an additional step for getting the pin / verifier from the Bitbucket. If you dont supply a callback url (or use "oob") you will get a Bitbucket's url that will promt your user to allow access for your application and supply you with the pin / verifier. Here is a simple example of how you could manually copy paste the pin from the browser:
```CSharp
var authenticator = sharpBucket.OAuth3LeggedAuthentication(consumerKey, consumerSecretKey, "oob");
var uri = authenticator.StartAuthentication();
Process.Start(uri);
var pin = Console.ReadLine();
// we can now do the final step by using the pin to get our access tokens
authenticator.AuthenticateWithPin(pin);
```
If you had a server waiting from Bitbucket's response, you would simply use your server's url as the callback and then wait for Bitbucket to send you the pin to that address.

If you already have the tokens you can simply skip the authentication process:
```CSharp
var authenticator = sharpBucket.OAuth3LeggedAuthentication(consumerKey, consumerSecretKey, 
														   oauthToken oauthTokenSecret);
```

## How much of the API is covered?
While a complete coverage of the API is preferred SharpBucket currently does not support everything yet. But the main functionality is [covered](https://github.com/MitjaBezensek/SharpBucket/blob/master/Coverage.md) and the rest should also get covered sooner or later.

## Contributing
Contributions are always welcome! [Here is some short information](https://github.com/MitjaBezensek/SharpBucket/blob/master/Contribution.md) about how and where to get started.

## Continuous Integration from AppVeyor
The project is using [AppVeyor's](http://www.appveyor.com/) Continuous Integration
Service that is free for open source projects. It is enabled for Pull Requests as well as the main branch. Main branch's current status is:

[![Build status](https://ci.appveyor.com/api/projects/status/jtlni3j2fq3j6pxy/branch/master)](https://ci.appveyor.com/project/MitjaBezenek/sharpbucket/branch/master)

## Licensing, Dependencies and Influence
SharpBucket is licensed under [MIT license](https://github.com/MitjaBezensek/SharpBucket/blob/master/LICENSE). 

### Dependencies:
- **RestSharp** for HTTP requests and responses. RestSHarp is [licensed under Apache 2.0 license](https://github.com/restsharp/RestSharp/blob/master/LICENSE.txt) terms.

### Influence
SharpBucket was influenced by ServiceStack's [Stripe api wrapper](https://github.com/ServiceStack/Stripe). The first versions of SharpBucket used ServiceStack's library, but has since moved to RestSharp.