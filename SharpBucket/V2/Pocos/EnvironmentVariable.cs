namespace SharpBucket.V2.Pocos
{
    public class EnvironmentVariable
    {
        /// <summary>
        /// pipeline_variable
        /// </summary>
        public string type { get; set; }

        public string uuid { get; set; }

        public string key { get; set; }

        /// <summary>
        /// This is null when reading from bitbucket when <see cref="secured"/> is true.
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// if secured value could not be read.
        /// </summary>
        public bool secured { get; set; }
    }
}