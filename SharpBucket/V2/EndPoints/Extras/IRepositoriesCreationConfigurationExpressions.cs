namespace SharpBucket.V2.EndPoints.Extras
{
    public interface IRepositoriesCreationConfigurationExpressions
    {        
        IRepositoriesCreationConfigurationExpressions SetSCM(SCMWord scm);
        IRepositoriesCreationConfigurationExpressions MakePrivate();
        IRepositoriesCreationConfigurationExpressions SetDescription(string description);
        IRepositoriesCreationConfigurationExpressions SetForkPolicy(ForkWord forkPolicy);
        IRepositoriesCreationConfigurationExpressions SetLanguage(string language);
        IRepositoriesCreationConfigurationExpressions EnableWiki();
        IRepositoriesCreationConfigurationExpressions EnableIssues();
    }
}