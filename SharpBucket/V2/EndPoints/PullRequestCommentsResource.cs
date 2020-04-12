using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/pullrequests/%7Bpull_request_id%7D/comments
    /// and
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/pullrequests/%7Bpull_request_id%7D/comments/%7Bcomment_id%7D#put
    /// </summary>
    public class PullRequestCommentsResource : CommentsResource<PullRequestComment>
    {
        internal PullRequestCommentsResource(PullRequestResource pullRequestResource)
            : base(pullRequestResource)
        {
        }
    }
}
