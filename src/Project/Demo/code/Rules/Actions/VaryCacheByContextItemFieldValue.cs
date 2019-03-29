using ZacharyKniebel.Foundation.OutputCaching.Rules;
using ZacharyKniebel.Foundation.OutputCaching.Rules.Actions;
using ZacharyKniebel.Foundation.Rules;

namespace ZacharyKniebel.Project.Demo.Rules.Actions
{
    public class VaryCacheByContextItemFieldValue<TRuleContext> : BaseCacheVarianceRuleAction<TRuleContext> 
        where TRuleContext : RuleContext<CacheVarianceRuleArgs>
    {
        public string FieldName { get; set; }

        protected override string GetKey(TRuleContext ruleContext)
        {
            return $"{Sitecore.Context.Item.ID}_{FieldName}";
        }

        protected override string GetValue(TRuleContext ruleContext)
        {
            return Sitecore.Context.Item[FieldName];
        }
    }
}