namespace SharpBucket.V2.Pocos
{
    internal interface ISrc
    {
        string path { get; }
        CommitInfo commit { get; }
    }
}
