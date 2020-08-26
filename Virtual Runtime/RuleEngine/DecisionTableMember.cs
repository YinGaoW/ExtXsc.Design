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
        public int Priority
        {
            get { return RuleBlock.Priority; }
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
            string strExecutionName = executionstack.GetExecutionName();

            if (strExecutionName != null)
            {
                if (strExecutionName == "RuleConditionValueFactor")
                    goto RuleConditionValueFactor;

                if (strExecutionName == "RuleBlock")
                    goto RuleBlock;
            }

            executionstack.PushExecutionName("RuleConditionValueFactor");

            RuleConditionValueFactor:

            Object objectRuleConditionValue = RuleConditionValueFactor.Execute(admlobject, executionstack);
            executionstack.PopExecutionName();
            executionstack.PushObject(objectRuleConditionValue);

            executionstack.PushExecutionName("RuleBlock");

            RuleBlock:
            
            if ((bool)(((IBaseData)executionstack.GetTopObject(0)).BaseData))
            {
                Object objectReturn = RuleBlock.Execute(admlobject, executionstack);

                executionstack.PopObject();
                executionstack.PopExecutionName();

                return objectReturn;
            }

            return base.OnExecute(admlobject, executionstack);
        }
        protected override void OnParse(SyntaxErrorList syntaxerrorlist)
        {
            if (RuleConditionValueFactor == null)
                syntaxerrorlist.SetSyntaxError(new StatementSyntaxError("规则条件因子不能为空", this));

            if (BooleanClass.NameStatic != RuleConditionValueFactor.Parse(syntaxerrorlist))
                syntaxerrorlist.SetSyntaxError(new StatementSyntaxError("值因子必须为" + BooleanClass.NameStatic + "类型", this));

            RuleBlock.Parse(syntaxerrorlist);
        }
    }
}
