using System.Collections.Specialized;
using System.Text;
using Sitecore.Mvc.Presentation;

namespace ZacharyKniebel.Foundation.OutputCaching.Rules
{
    public class CacheVarianceRuleArgs
    {
        public Rendering Rendering { get; set; }
        public StringBuilder CacheKey { get; set; }
        public NameValueCollection Parameters { get; set; } = new NameValueCollection();
    }
}