using Sitecore.Diagnostics;
using ZacharyKniebel.Foundation.Rules;
using ZacharyKniebel.Foundation.Rules.Actions;

namespace ZacharyKniebel.Foundation.OutputCaching.Rules.Actions
{
    public abstract class BaseCacheVarianceRuleAction<TRuleContext> : BaseRuleAction<TRuleContext> 
        where TRuleContext : RuleContext<CacheVarianceRuleArgs>
    {
        protected override void ApplyRule(TRuleContext ruleContext)
        {
            Assert.IsNotNull(ruleContext.Args, "ruleContext.Args != null");

            var key = GetKey(ruleContext);
            var value = GetValue(ruleContext);

            var cacheKeyPart = GenerateCacheKeyPart(ruleContext, key, value);
            UpdateCacheKey(ruleContext, cacheKeyPart);
        }

        private void UpdateCacheKey(TRuleContext ruleContext, string cacheKeyPart)
        {
            if (string.IsNullOrEmpty(cacheKeyPart))
            {
                return;
            }

            ruleContext.Args.CacheKey.Append(cacheKeyPart);
        }

        protected virtual string GenerateCacheKeyPart(TRuleContext ruleContext, string key, string value)
        {
            return $"_#{key}:{value}";
        }

        protected abstract string GetKey(TRuleContext ruleContext);
        protected abstract string GetValue(TRuleContext ruleContext);
    }
}