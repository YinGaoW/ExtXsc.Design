using Eap;

namespace ExtEap
{
    public class DecisionTable : Statement
    {
        private ComponentArrayListDecorator m_componentarraylistdecoratorDecisionTableMember;
        public DecisionTable(Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }
        public DecisionTable(Statement statementOwner, Method method) : base(statementOwner, method)
        {
            OnConstruct();
        }

        public int DecisionTableMemberCount
        {
            get { return m_componentarraylistdecoratorDecisionTableMember.ComponentCount; }
        }

        private void OnConstruct()
        {
            m_componentarraylistdecoratorDecisionTableMember = new ComponentArrayListDecorator(this, ComponentFactory, typeof(DecisionTableMember), new object[] { this, Method });
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_componentarraylistdecoratorDecisionTableMember.Initialize();

            OnConstruct();
        }

        public DecisionTableMember GetDecisionTableMember(int nIndex)
        {
            return (DecisionTableMember)m_componentarraylistdecoratorDecisionTableMember.GetComponent(nIndex);
        }
        public void SetDecisionTableMember(int nIndex, DecisionTableMember decisiontablemember, bool bIsInserting)
        {
            m_componentarraylistdecoratorDecisionTableMember.SetComponent(nIndex, decisiontablemember, bIsInserting);
        }
        public void SetDecisionTableMember(int nIndex, DecisionTableMember decisiontablemember)
        {
            m_componentarraylistdecoratorDecisionTableMember.SetComponent(nIndex, decisiontablemember);
        }

        protected override Object OnExecute(AdmlObject admlobject, ExecutionStack executionstack)
        {
            return base.OnExecute(admlobject, executionstack);
        }
        protected override void OnParse(SyntaxErrorList syntaxerrorlist)
        {
            base.OnParse(syntaxerrorlist);
        }
    }
}
