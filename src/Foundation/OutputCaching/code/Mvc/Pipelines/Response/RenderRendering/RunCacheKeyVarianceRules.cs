using System;
using System.Text;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Pipelines.Response.RenderRendering;
using Sitecore.Rules;
using ZacharyKniebel.Foundation.OutputCaching.Rules;
using ZacharyKniebel.Foundation.Rules;

namespace ZacharyKniebel.Foundation.OutputCaching.Mvc.Pipelines.Response.RenderRendering
{
    public class RunCacheKeyVarianceRules : RenderRenderingProcessor
    {
        private readonly ICacheKeyVarianceRuleListCache _ruleListCache;
        private readonly IOutputCachingConfiguration _configuration;

        public RunCacheKeyVarianceRules(ICacheKeyVarianceRuleListCache ruleListCache, IOutputCachingConfiguration configuration)
        {
            _ruleListCache = ruleListCache ?? throw new ArgumentNullException(nameof(ruleListCache));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public override void Process(RenderRenderingArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (args.Rendered || !args.Cacheable)
            {
                return;
            }

            var renderingItem = args.Rendering.RenderingItem;
            var rules = _ruleListCache.GetRules<RuleContext<CacheVarianceRuleArgs>>(renderingItem.InnerItem);
            if (rules == null)
            {
                if (string.IsNullOrEmpty(renderingItem.InnerItem[_configuration.VarianceRuleFolderFieldID]))
                {
                    return;
                }

                rules = RuleFactory.GetRules<RuleContext<CacheVarianceRuleArgs>>(
                    renderingItem.Database.GetItem(renderingItem.InnerItem[_configuration.VarianceRuleFolderFieldID]),
                    "Rule");
                _ruleListCache.AddRules(renderingItem.InnerItem,rules);
            }

            if (rules == null || rules.Count == 0)
            {
                return;
            }
            
            var ruleContextArgs = new CacheVarianceRuleArgs()
            {
                Rendering = args.Rendering,
                CacheKey = new StringBuilder(args.CacheKey)
            };

            var ruleContext = new RuleContext<CacheVarianceRuleArgs>(ruleContextArgs)
            {
                Item = Sitecore.Context.Item
            };

            RuleManager.RunRules(ruleContext, rules);

            args.CacheKey = ruleContext.Args.CacheKey.ToString();
        }
    }
}