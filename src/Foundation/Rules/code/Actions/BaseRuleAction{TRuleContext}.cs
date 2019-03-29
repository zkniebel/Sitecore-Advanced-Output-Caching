using System;
using Sitecore.Diagnostics;
using Sitecore.Rules;

namespace ZacharyKniebel.Foundation.Rules.Actions
{
    /// <summary>
    /// The rule action.
    /// </summary>
    /// <typeparam name="TRuleContext">The type of the rule context.</typeparam>
    public abstract class BaseRuleAction<TRuleContext> : Sitecore.Rules.Actions.RuleAction<TRuleContext>
        where TRuleContext : RuleContext
    {
        /// <summary>
        /// Action implementation.
        /// </summary>
        /// <param name="ruleContext">
        /// The rule context.
        /// </param>
        public sealed override void Apply(TRuleContext ruleContext)
        {
            Assert.IsNotNull(ruleContext, "ruleContext");
            if (ruleContext.IsAborted)
            {
                return;
            }

            try
            {
                var msg = "RuleAction " + GetType().Name + " started";

                Log.Debug(msg, this);

                ApplyRule(ruleContext);
            }
            catch (Exception exception)
            {
                var msg = "RuleAction " + GetType().Name + " failed.";
                Log.Error(msg, exception, this);
            }

            var message = "RuleAction " + GetType().Name + " ended";

            Log.Debug(message, this);
        }
        
        /// <summary>
        /// The apply rule.
        /// </summary>
        /// <param name="ruleContext">
        /// The rule context.
        /// </param>
        protected abstract void ApplyRule(TRuleContext ruleContext);
    }
}