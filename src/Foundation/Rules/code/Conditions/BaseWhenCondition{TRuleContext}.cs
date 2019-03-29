using System;
using Sitecore.Diagnostics;
using Sitecore.Rules;

namespace ZacharyKniebel.Foundation.Rules.Conditions
{
    /// <summary>
    /// The base class for the rule engine
    /// </summary>
    /// <typeparam name="TRuleContext">
    /// Instance of Sitecore.Rules.Conditions.RuleContext.
    /// </typeparam>
    public abstract class BaseWhenCondition<TRuleContext> : Sitecore.Rules.Conditions.WhenCondition<TRuleContext>
        where TRuleContext : RuleContext
    {
        /// <summary>
        /// Executes the specified rule context.
        /// </summary>
        /// <param name="ruleContext">
        /// The rule context.
        /// </param>
        /// <returns>
        /// <c>True</c>, if the condition succeeds, otherwise <c>false</c>.
        /// </returns>
        protected sealed override bool Execute(TRuleContext ruleContext)
        {
            Assert.IsNotNull(ruleContext, "ruleContext");
            if (ruleContext.IsAborted)
            {
                return false;
            }

            try
            {
                Log.Debug("RuleCondition " + GetType().Name + " started", this);

                var result = ExecuteCondition(ruleContext);
                return result;
            }
            catch (Exception exception)
            {
                Log.Error("RuleCondition " + GetType().Name + " failed.", exception, this);
            }

            Log.Debug("RuleCondition " + GetType().Name + " ended", this);

            return false;
        }

        /// <summary>
        /// The execute rule.
        /// </summary>
        /// <param name="ruleContext">The rule context.</param>
        /// <returns>
        /// <c>True</c>, if the condition succeeds, otherwise <c>false</c>.
        /// </returns>
        protected abstract bool ExecuteCondition(TRuleContext ruleContext);
    }
}