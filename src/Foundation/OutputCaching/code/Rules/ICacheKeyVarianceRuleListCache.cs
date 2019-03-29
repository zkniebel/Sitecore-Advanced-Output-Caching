using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Rules;

namespace ZacharyKniebel.Foundation.OutputCaching.Rules
{
    public interface ICacheKeyVarianceRuleListCache
    {
        ID GetCacheKey(Item renderingItem);

        void AddRules<TRuleContext>(Item renderingItem, RuleList<TRuleContext> value)
            where TRuleContext : RuleContext;

        RuleList<TRuleContext> GetRules<TRuleContext>(Item renderingItem) where TRuleContext : RuleContext;
    }
}