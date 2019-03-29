using Sitecore.Data;

namespace ZacharyKniebel.Foundation.OutputCaching
{
    public interface IOutputCachingConfiguration
    {
        string VarianceRuleListCacheMaxSize { get; }
        ID VarianceRuleFolderFieldID { get; }
    }
}
