using Sitecore.Data;
using Sitecore.Data.Items;
using ZacharyKniebel.Foundation.Rules.Caching;

namespace ZacharyKniebel.Foundation.OutputCaching.Rules
{
    public class CacheKeyVarianceRuleListCache : BaseRuleListCache<ID>, ICacheKeyVarianceRuleListCache
    {        
        public CacheKeyVarianceRuleListCache(long maxSize)
          : base("rules[cache key variance]", maxSize)
        {
        }

        public override ID GetCacheKey(Item renderingItem)
        {
            return renderingItem.ID;
        }
    }
}