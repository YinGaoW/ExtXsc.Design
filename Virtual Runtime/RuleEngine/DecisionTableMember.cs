using Eap;

namespace ExtEap
{
    public class DecisionTableMember : Statement
    {
        private ComponentContainer m_componentcontainerRuleConditionValueFactor;
        private RuleBlock m_ruleblock;
        public DecisionTableMember(Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }

        public DecisionTableMember(Statement statementOwner, Method method) : base(statementOwner, method)
        {
            OnConstruct();
        }

        public Factor RuleConditionValueFactor
        {
            get { return (Factor)m_componentcontainerRuleConditionValueFactor.Component; }
            set { m_componentcontainerRuleConditionValueFactor.Component = value; }
        }
        public RuleBlock RuleBlock
        {
            get { return m_ruleblock; }
        }

        private void OnConstruct()
        {
            SetComponent("RuleConditionValueFactor", m_componentcontainerRuleConditionValueFactor = new ComponentContainer(ComponentFactory, typeof(Factor), new object[] { this }));
            SetComponent("RuleBlock", m_ruleblock = new RuleBlock(this, Method));
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnConstruct();
        }

        protected override Eap.Object OnExecute(AdmlObject admlobject, ExecutionStack executionstack)
        {
            // TODO 待完成执行逻辑
            return base.OnExecute(admlobject, executionstack);
        }
        protected override void OnParse(SyntaxErrorList syntaxerrorlist)
        {
            // TODO 待完成校验逻辑
            base.OnParse(syntaxerrorlist);
        }
    }
}
