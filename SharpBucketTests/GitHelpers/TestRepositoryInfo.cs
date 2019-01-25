namespace SharpBucketTests.GitHelpers
{
    /// <summary>
    /// Class that provide known info relative to the test repository build with <see cref="TestRepositoryBuilder"/>
    /// </summary>
    public class TestRepositoryInfo
    {
        public string AccountName { get; set; }
        public string RepositoryName { get; set; }
        public string FirstCommit { get; set; }
    }
}
