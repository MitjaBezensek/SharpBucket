namespace SharpBucketTests.GitHelpers
{
    /// <summary>
    /// Class that provides known info relative to the test repository built with <see cref="TestRepositoryBuilder"/>.
    /// </summary>
    public class TestRepositoryInfo
    {
        public string AccountName { get; set; }
        public string RepositoryName { get; set; }
        public string FirstCommit { get; set; }
        public string MainBranchLastCommit { get; set; }
    }
}
