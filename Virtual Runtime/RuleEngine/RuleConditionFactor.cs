using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eap;

namespace ExtEap
{
    public class RuleConditionFactor : Factor
    {
        private ComponentContainer m_componentcontainerConditionValueFactor;
        private ComponentArrayListDecorator m_componentarraylistdecoratorChildConditionValueFactor;
        public RuleConditionFactor(Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }

        public Factor ConditionValueFactor
        {
            get { return (Factor)m_componentcontainerConditionValueFactor.Component; }
        }
        public int ChildConditionValueFactorConut
        {
            get { return m_componentarraylistdecoratorChildConditionValueFactor.ComponentCount; }
        }
        private void OnConstruct()
        {
            SetComponent("RuleConditionValueFactorComponentContainer", m_componentcontainerConditionValueFactor = new ComponentContainer(ComponentFactory, typeof(Factor), new object[] { this }));
            m_componentarraylistdecoratorChildConditionValueFactor = new ComponentArrayListDecorator(this, ComponentFactory, typeof(RuleConditionFactor), new object[] { this, Method });
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_componentarraylistdecoratorChildConditionValueFactor.Initialize();

            OnConstruct();
        }

        public RuleConditionFactor GetChildConditionValueFactor(int nIndex)
        {
            return (RuleConditionFactor)m_componentarraylistdecoratorChildConditionValueFactor.GetComponent(nIndex);
        }
        public void SetChildConditionValueFactor(int nIndex, RuleConditionFactor ruleconditionfactor, bool bIsInserting)
        {
            m_componentarraylistdecoratorChildConditionValueFactor.SetComponent(nIndex, ruleconditionfactor, bIsInserting);
        }
        public void SetChildConditionValueFactor(int nIndex, RuleConditionFactor ruleconditionfactor)
        {
            m_componentarraylistdecoratorChildConditionValueFactor.SetComponent(nIndex, ruleconditionfactor, false);
        }

        protected override Eap.Object OnExecute(AdmlObject admlobject, ExecutionStack executionstack)
        {
            return base.OnExecute(admlobject, executionstack);
        }

        protected override string OnParse(SyntaxErrorList syntaxerrorlist)
        {
            return base.OnParse(syntaxerrorlist);
        }
    }
}
