using Sitecore.Data;

namespace ZacharyKniebel.Foundation.OutputCaching
{
    public class OutputCachingConfiguration : IOutputCachingConfiguration
    {
        public string VarianceRuleListCacheMaxSize { get; protected set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private string _varianceRuleFolderFieldID { get; set; }
        private ID _parsedVarianceRuleFolderFieldID;
        public ID VarianceRuleFolderFieldID => _parsedVarianceRuleFolderFieldID ??
                                               (_parsedVarianceRuleFolderFieldID = new ID(_varianceRuleFolderFieldID));
    }
}