using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;

namespace SharpBucketTests.V2
{
    public class SampleDeclinedPullRequest
    {
        public PullRequestResource PullRequestResource { get; }

        public PullRequest PullRequest { get; }

        private SampleDeclinedPullRequest()
        {
            var pullRequestsResource = SampleRepositories.TestRepository.RepositoryResource.PullRequestsResource();

            // create the sample pull request
            var pullRequest = new PullRequest
            {
                title = "a bad work",
                source = new Source { branch = new Branch { name = "branchToDecline" } }
            };
            this.PullRequest = pullRequestsResource.PostPullRequest(pullRequest);
            this.PullRequestResource = pullRequestsResource.PullRequestResource(this.PullRequest.id.GetValueOrDefault());

            // decline it
            this.PullRequestResource.DeclinePullRequest();
        }

        private static SampleDeclinedPullRequest _instance;

        public static SampleDeclinedPullRequest Get => _instance ??= new SampleDeclinedPullRequest();
    }
}
