using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eap;

namespace ExtEap
{
    public class CrossDecisionTable : Statement
    {
        private ComponentArrayList m_componentarraylistRuleConditionFactorRow;
        private ComponentArrayList m_componentarraylistRuleConditionFactorColumn;
        private ComponentArrayListDecorator m_componentarraylistdecoratorRuleBlock;
        public CrossDecisionTable(Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }

        public CrossDecisionTable(Statement statementOwner, Method method) : base(statementOwner, method)
        {
            OnConstruct();
        }

        public int RuleConditionFactorRowCount
        {
            get { return m_componentarraylistRuleConditionFactorRow.ComponentCount; }
        }
        public int RuleConditionFactorColumnCount
        {
            get { return m_componentarraylistRuleConditionFactorColumn.ComponentCount; }
        }
        public int RuleBlockCount
        {
            get { return m_componentarraylistdecoratorRuleBlock.ComponentCount; }
        }

        private void OnConstruct()
        {
            SetComponent("RuleConditionFactorRowComponentArrayList", m_componentarraylistRuleConditionFactorRow = new ComponentArrayList(ComponentFactory, typeof(RuleConditionFactor), new object[] { OwnerStatement }));
            SetComponent("RuleConditionFactorColumnComponentArrayList", m_componentarraylistRuleConditionFactorColumn = new ComponentArrayList(ComponentFactory, typeof(RuleConditionFactor), new object[] { OwnerStatement }));
            m_componentarraylistdecoratorRuleBlock = new ComponentArrayListDecorator(this, ComponentFactory, typeof(RuleBlock), new object[] { this, Method });
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_componentarraylistdecoratorRuleBlock.Initialize();

            OnConstruct();
        }
        
        public RuleConditionFactor GetRuleCondtionFactorRow(int nIndex)
        {
            return (RuleConditionFactor)m_componentarraylistRuleConditionFactorRow.GetComponent(nIndex);
        }
        public void SetRuleConditionFactorRow(int nIndex, RuleConditionFactor ruleconditionfactor, bool bIsInserting)
        {
            m_componentarraylistRuleConditionFactorRow.SetComponent(nIndex, ruleconditionfactor, bIsInserting);
        }
        public void SetRuleConditionFactorRow(int nIndex, RuleConditionFactor ruleconditionfactor)
        {
            m_componentarraylistRuleConditionFactorRow.SetComponent(nIndex, ruleconditionfactor, false);
        }
        public RuleConditionFactor GetRuleConditionFactorColumn(int nIndex)
        {
            return (RuleConditionFactor)m_componentarraylistRuleConditionFactorColumn.GetComponent(nIndex);
        }
        public void SetRuleConditionFactorColumn(int nIndex, RuleConditionFactor ruleconditionfactor, bool bIsInserting)
        {
            m_componentarraylistRuleConditionFactorColumn.SetComponent(nIndex, ruleconditionfactor, bIsInserting);
        }
        public void SetRuleConditionFactorColumn(int nIndex, RuleConditionFactor ruleconditionfactor)
        {
            m_componentarraylistRuleConditionFactorColumn.SetComponent(nIndex, ruleconditionfactor, false);
        }
        public RuleBlock GetRuleBlock(int nIndex)
        {
            return (RuleBlock)m_componentarraylistdecoratorRuleBlock.GetComponent(nIndex);
        }
        public void SetRuleBlock(int nIndex, RuleBlock ruleblock, bool bIsInserting)
        {
            m_componentarraylistdecoratorRuleBlock.SetComponent(nIndex, ruleblock, bIsInserting);
        }
        public void SetRuleBlock(int nIndex, RuleBlock ruleblock)
        {
            m_componentarraylistdecoratorRuleBlock.SetComponent(nIndex, ruleblock, false);
        }

        protected override Eap.Object OnExecute(AdmlObject admlobject, ExecutionStack executionstack)
        {
            return base.OnExecute(admlobject, executionstack);
        }

        protected override void OnParse(SyntaxErrorList syntaxerrorlist)
        {
            base.OnParse(syntaxerrorlist);
        }
    }
}
