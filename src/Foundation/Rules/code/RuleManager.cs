using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.SecurityModel;

namespace ZacharyKniebel.Foundation.Rules
{

    /// <summary>
    /// Manages the execution of the rules
    /// </summary>
    public static class RuleManager
	{
        /// <summary>
        /// Runs the rules in the rules folder with the given ID
        /// </summary>
        /// <param name="ruleContext"></param>
        /// <param name="rulesFolderId">The rules folder.</param>
        public static void RunRules<TRuleContext>(TRuleContext ruleContext, ID rulesFolderId)
            where TRuleContext : RuleContext
        {
            try
            {
                Assert.ArgumentNotNull(ruleContext, "ruleContext is null");
                Assert.ArgumentNotNull(rulesFolderId, "rulesFolderId is null");

                Assert.IsNotNull(ruleContext.Item, "ruleContext.Item is null");
                Assert.IsNotNull(ruleContext.Item.Database, "ruleContext.Item.Database is null");
            } 
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, typeof(RuleManager));
                return;
            }

            Item rulesFolderItem;
            using (new SecurityDisabler())
            {
                rulesFolderItem = ruleContext.Item.Database.GetItem(rulesFolderId);
                if (rulesFolderItem == null)
                {
                    return;
                }
            }

            RunRules(ruleContext, rulesFolderItem);
        }

        /// <summary>
        /// Runs the rules in the given rules folder
        /// </summary>
        /// <param name="ruleContext"></param>
        /// <param name="rulesFolderItem">The rules folder.</param>
        public static void RunRules<TRuleContext>(TRuleContext ruleContext, Item rulesFolderItem)
            where TRuleContext : RuleContext
        {
            try
            {
                Assert.ArgumentNotNull(rulesFolderItem, "rulesFolderItem is null");

                var rules = RuleFactory.GetRules<TRuleContext>(rulesFolderItem, "Rule");
                
                RunRules(ruleContext, rules);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message, exception, typeof(RuleManager));
            }
        }

	    public static void RunRules<TRuleContext>(TRuleContext ruleContext, RuleList<TRuleContext> rules)
	        where TRuleContext : RuleContext
	    {
	        try
	        {
	            Assert.ArgumentNotNull(ruleContext, "ruleContext is null");

	            if (rules == null || rules.Count == 0)
	            {
	                return;
	            }

	            rules.Run(ruleContext);
	        }
	        catch (Exception exception)
	        {
	            Log.Error(exception.Message, exception, typeof(RuleManager));
	        }
	    }
    }
}