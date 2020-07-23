using Eap;
namespace ExtEap
{
    public class RuleBlock : Eap.Block
    {
        private Integer m_integerPriority;
        public RuleBlock(Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }

        public RuleBlock(Statement statementOwner, Method method) : base(statementOwner, method)
        {
            OnConstruct();
        }

        public RuleBlock(Statement statementOwner, bool bSupportComponentModal) : base(statementOwner, bSupportComponentModal)
        {
            OnConstruct();
        }

        public RuleBlock(Statement statementOwner, Method method, bool bSupportComponentModal) : base(statementOwner, method, bSupportComponentModal)
        {
            OnConstruct();
        }

        public int Priority
        {
            get { return m_integerPriority.Value; }
            set { m_integerPriority.Value = value; }
        }

        private void OnConstruct()
        {
            SetElement("Priority", m_integerPriority = new Integer());
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnConstruct();
        }
    }
}
