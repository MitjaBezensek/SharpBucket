using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;

namespace SharpBucketTests.V2
{
    public class SampleOpenedPullRequest
    {
        public PullRequestResource PullRequestResource { get; }

        public PullRequest PullRequest { get; }

        public PullRequestComment GlobalComment { get; }

        public PullRequestComment ResponseComment { get; }

        private SampleOpenedPullRequest()
        {
            var pullRequestsResource = SampleRepositories.TestRepository.RepositoryResource.PullRequestsResource();

            // create the sample pull request
            var pullRequest = new PullRequest
            {
                title = "a good work to approve",
                source = new Source { branch = new Branch { name = "branchToAccept" } }
            };
            this.PullRequest = pullRequestsResource.PostPullRequest(pullRequest);
            this.PullRequestResource = pullRequestsResource.PullRequestResource(this.PullRequest.id.GetValueOrDefault());

            // create some comments on it
            var pullRequestCommentsResource = this.PullRequestResource.CommentsResource;
            this.GlobalComment = pullRequestCommentsResource.PostComment("This PR is just for testing purposes.");
            this.ResponseComment = pullRequestCommentsResource.PostComment("OK I understand", GlobalComment.id);
        }

        private static SampleOpenedPullRequest _instance;

        public static SampleOpenedPullRequest Get => _instance ??= new SampleOpenedPullRequest();
    }
}