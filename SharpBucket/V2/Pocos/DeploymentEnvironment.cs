namespace SharpBucket.V2.Pocos
{
    public class DeploymentEnvironment
    {
        public string type { get; set; }
        public string uuid { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public bool? deployment_gate_enabled { get; set; }
        public bool? environment_lock_enabled { get; set; }
        public bool? hidden { get; set; }
        public int? rank { get; set; }
        public DeploymentEnvironmentCategory category { get; set; }
        public DeploymentEnvironmentType environment_type { get; set; }
        public DeploymentEnvironmentLock @lock { get; set; }
        public DeploymentRestrictionsConfiguration restrictions { get; set; }
    }
}