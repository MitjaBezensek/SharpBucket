# SharpBucket
SharpBucket is a .Net wrapper for the Bitbucket's REST API. It is written in in C#. With it you can have all the data of your repositories / issues at your fingertips.

## How to get started
### Installation
To install SharpBucket, run the following command in the Package Manager Console:

    PM> Install-Package SharpBucket

### Usage
See the [SharpBucketCli Project](https://github.com/MitjaBezensek/SharpBucket/blob/master/SharpBucketCli/Program.cs) or the [unit tests](https://github.com/MitjaBezensek/SharpBucket/tree/master/SharpBucketTests) to see how to use the wrapper.

Here's just a brief demo:

First lets set your entry point to the API
```CSharp
// your main entry to the Bitbucket API, this one is for V2
var sharpBucket = new SharpBucketV2();
// authenticate with OAuth2 keys
sharpBucket.OAuth2ClientCredentials(consumerKey, consumerSecretKey);
```

There are various end points you can use. Lets take a look at Users end point:
```CSharp
// getting the User end point (accountName can be the name of a user or a team)
var userEndPoint = sharpBucket.UsersEndPoint("accountName");
// querying the Bitbucket API for various info
var userProfile = userEndPoint.GetProfile();
var followers = user.ListFollowers();
var follows = user.ListFollowing();
var userRepos = user.ListRepositories();
```

Sub end points are named *Resource* but are pretty similar. Lets look at the repository resource:
```CSharp
// getting the repositories end point
var repositoriesEndPoint = sharpBucket.RepositoriesEndPoint();
// getting the Repository resource for a specific repository
var repositoryResource = repositoriesEndPoint.RepositoryResource("accountName", "repoSlugOrName");
// getting the list of all the commits of the repository
var commits = repositoryResource.ListCommits();
```

Sending information is just as easy.
```CSharp
var newRepository = new Repository
                    {
                        name = "Sample",
                        language = "c#",
                        scm = "git"
                    };
var newRepositoryResult = repositoryResource.PostRepository(newRepository);
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

### OAuth 1.0 authentication
With OAuth you can choose between [2 legged and 3 legged authentication](http://cakebaker.42dh.com/2011/01/10/2-legged-vs-3-legged-oauth/).

**Two legged** is as simple as basic authentication:
```CSharp
// authenticate with OAuth keys
sharpBucket.OAuth1TwoLeggedAuthentication(consumerKey, consumerSecretKey);
```
**The three legged** one requires an additional step for getting the pin / verifier from the Bitbucket. If you do not supply a callback url (or use "oob") you will get a Bitbucket's url that will promt your user to allow access for your application and supply you with the pin / verifier. Here is a simple example of how you could manually copy paste the pin from the browser:
```CSharp
var authenticator = sharpBucket.OAuth1ThreeLeggedAuthentication(consumerKey, consumerSecretKey, "oob");
var uri = authenticator.StartAuthentication();
Process.Start(uri);
var pin = Console.ReadLine();
// we can now do the final step by using the pin to get our access tokens
authenticator.AuthenticateWithPin(pin);
```
If you had a server waiting from Bitbucket's response, you would simply use your server's url as the callback and then wait for Bitbucket to send you the pin to that address.

If you already have the tokens (those returned by AuthenticateWithPin method) you can simply skip the authentication process:
```CSharp
var authenticator = sharpBucket.OAuth1ThreeLeggedAuthentication(consumerKey, consumerSecretKey, oauthToken, oauthTokenSecret);
```

### OAuth2 authentication
OAuth 2.0 offer a large choice of scenarios ([bitbucket OAuth 2.0](https://developer.atlassian.com/bitbucket/api/2/reference/meta/authentication))  
But they are not yet all implemented.

**Client credentials Grant** is similar to OAuth1 two legged authentication:
```CSharp
// authenticate with OAuth keys
sharpBucket.OAuth2ClientCredentials(consumerKey, consumerSecretKey);
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