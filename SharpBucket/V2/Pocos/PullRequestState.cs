namespace SharpBucket.V2.Pocos
{
    /// <summary>
    /// Enumeration of the possible states for a pull request.
    /// </summary>
    public enum PullRequestState
    {
        Merged,
        Superseded,
        Open,
        Declined,
    }
}
