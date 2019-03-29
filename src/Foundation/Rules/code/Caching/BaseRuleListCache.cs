using Sitecore.Caching;
using Sitecore.Caching.Generics;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Rules;

namespace ZacharyKniebel.Foundation.Rules.Caching
{
    public abstract class BaseRuleListCache<TKey> : CustomCache<TKey>
    {
        protected BaseRuleListCache(string name, long maxSize)
          : base(name, maxSize)
        {
        }

        public virtual void AddRules<TRuleContext>(Item item, RuleList<TRuleContext> value) 
            where TRuleContext : RuleContext
        {
            Assert.ArgumentNotNull(item, nameof(item));
            Assert.ArgumentNotNull(value, nameof(value));
            CacheRules(GetCacheKey(item), value);
        }

        public virtual RuleList<TRuleContext> GetRules<TRuleContext>(Item item) where TRuleContext : RuleContext
        {
            Assert.ArgumentNotNull(item, nameof(item));
            var cacheEntry = GetObject(GetCacheKey(item)) as CacheEntry;
            return cacheEntry?.Value as RuleList<TRuleContext>;
        }

        public abstract TKey GetCacheKey(Item item);

        private void CacheRules<TRuleContext>(TKey cacheKey, RuleList<TRuleContext> result) 
            where TRuleContext : RuleContext
        {
            Assert.ArgumentNotNull(cacheKey, nameof(cacheKey));
            Assert.ArgumentNotNull(result, nameof(result));
            var cacheEntry = new CacheEntry
            {
                Value = result
            };
            SetObject(cacheKey, cacheEntry);
        }

        /// <inheritdoc />
        // ReSharper disable once InconsistentNaming
        private class CacheEntry : ICacheable
        {
            public object Value { get; set; }

            private DataLengthChangedDelegate _dataLengthChanged;
            event DataLengthChangedDelegate ICacheable.DataLengthChanged
            {
                add => _dataLengthChanged += value;
                // ReSharper disable once DelegateSubtraction
                remove => _dataLengthChanged -= value;
            }

            bool ICacheable.Cacheable
            {
                get => true;
                set { }
            }

            bool ICacheable.Immutable => true;

            long ICacheable.GetDataLength()
            {
                return 16384;
            }
        }
    }
}