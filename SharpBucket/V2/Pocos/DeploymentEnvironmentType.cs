namespace SharpBucket.V2.Pocos
{
    public class DeploymentEnvironmentType
    {
        /// <summary>
        /// Shortcut to create an <see cref="DeploymentEnvironmentType"/> instance that represent test environment.
        /// </summary>
        public static DeploymentEnvironmentType Test => new DeploymentEnvironmentType { name = "test" };

        /// <summary>
        /// Shortcut to create an <see cref="DeploymentEnvironmentType"/> instance that represent staging environment.
        /// </summary>
        public static DeploymentEnvironmentType Staging => new DeploymentEnvironmentType { name = "staging" };

        /// <summary>
        /// Shortcut to create an <see cref="DeploymentEnvironmentType"/> instance that represent production environment.
        /// </summary>
        public static DeploymentEnvironmentType Production => new DeploymentEnvironmentType { name = "production" };

        public string type { get; set; }

        /// <summary>
        /// supported environment type names are: test, staging, and production.
        /// https://support.atlassian.com/bitbucket-cloud/docs/set-up-and-monitor-deployments/#Step-2--Configure-your-deployment-steps
        /// </summary>
        public string name { get; set; }

        public int? rank { get; set; }
    }
}