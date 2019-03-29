using System;
using Microsoft.Extensions.DependencyInjection;
using Sitecore;
using Sitecore.DependencyInjection;
using ZacharyKniebel.Foundation.OutputCaching.Rules;

namespace ZacharyKniebel.Foundation.OutputCaching
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IOutputCachingConfiguration>(GetOutputCachingConfiguration);
            serviceCollection.AddSingleton<ICacheKeyVarianceRuleListCache>(CreateCacheKeyVarianceRuleListCache);
        }

        private static ICacheKeyVarianceRuleListCache CreateCacheKeyVarianceRuleListCache(IServiceProvider provider)
        {
            var configuration = provider.GetService<IOutputCachingConfiguration>();
            return new CacheKeyVarianceRuleListCache(StringUtil.ParseSizeString(configuration.VarianceRuleListCacheMaxSize));
        }

        private static OutputCachingConfiguration GetOutputCachingConfiguration(IServiceProvider provider)
        {
            return Sitecore.Configuration.Factory.CreateObject("outputCaching", true) as OutputCachingConfiguration;
        }
    }
}