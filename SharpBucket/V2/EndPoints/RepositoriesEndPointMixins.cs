using System;
using SharpBucket.V2.EndPoints.Extras;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    public static class RepositoriesEndPointMixins
    {
        public static RepositoryResource CreateRepository(this RepositoriesEndPoint endpoint, string accountName,
            string repository, Action<IRepositoriesCreationConfigurationExpressions> configureCreation)
        {
            var configurator = configureCreation ?? ((_) => { });            
            var parameters = new RepositoryCreationParameters();
            var configurationParameters = new RepositoriesCreationConfigurationExpressions(parameters);
            configurator(configurationParameters);
            return endpoint.CreateRepository(accountName, repository, parameters);
        }
    }
}