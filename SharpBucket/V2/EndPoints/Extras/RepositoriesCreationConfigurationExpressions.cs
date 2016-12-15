using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints.Extras
{
    public sealed class RepositoriesCreationConfigurationExpressions : IRepositoriesCreationConfigurationExpressions
    {
        private readonly RepositoryCreationParameters parameters;

        public RepositoriesCreationConfigurationExpressions(RepositoryCreationParameters parameters)
        {
            this.parameters = parameters;
        }

        public IRepositoriesCreationConfigurationExpressions SetSCM(SCMWord scm)
        {
            parameters.scm = scm.Name;
            return this;
        }

        public IRepositoriesCreationConfigurationExpressions MakePrivate()
        {
            parameters.is_private = true;
            return this;
        }

        public IRepositoriesCreationConfigurationExpressions SetDescription(string description)
        {
            parameters.description = description;
            return this;
        }

        public IRepositoriesCreationConfigurationExpressions SetForkPolicy(ForkWord forkPolicy)
        {
            parameters.fork_policy = forkPolicy.Name;
            return this;
        }

        public IRepositoriesCreationConfigurationExpressions SetLanguage(string language)
        {
            parameters.language = language;
            return this;
        }

        public IRepositoriesCreationConfigurationExpressions EnableWiki()
        {
            parameters.has_wiki = true;
            return this;
        }

        public IRepositoriesCreationConfigurationExpressions EnableIssues()
        {
            parameters.has_issues = true;
            return this;
        }
    }
}