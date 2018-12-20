using LibGit2Sharp;

namespace SharpBucketTests.GitHelpers
{
    /// <summary>
    /// Interface that match the <see cref="LibGit2Sharp.Handlers.CredentialsHandler"/>
    /// </summary>
    internal interface IGitCredentialsProvider
    {
        Credentials GetCredentials(string url, string usernameFromUrl, SupportedCredentialTypes types);
    }
}