using System;
using Sitecore.Diagnostics;
using Sitecore.Rules;

namespace ZacharyKniebel.Foundation.Rules.Conditions
{
    /// <summary>
    /// The base class for the rules engine condition
    /// </summary>
    /// <typeparam name="TRuleContext">
    /// Type providing rule context.
    /// </typeparam>
    public abstract class BaseOperatorCondition<TRuleContext> : Sitecore.Rules.Conditions.OperatorCondition<TRuleContext>
        where TRuleContext : RuleContext
    { 
        /// <summary>
        /// Condition implementation.
        /// </summary>
        /// <param name="ruleContext">The rule context.</param>
        /// <returns>
        /// True if the item has layout details for the default device,
        /// otherwise False.
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