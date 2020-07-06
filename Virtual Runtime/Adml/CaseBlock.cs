using Eap;
namespace ExtEap
{
    public class CaseBlock : Block
    {
        private String m_stringValue;

        public CaseBlock(Statement statementOwner, Method method) : base(statementOwner, method)
        {
            OnConstruct();
        }
        public CaseBlock(Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }
        public CaseBlock(Statement statementOwner, Method method, bool bSupportComponentModal) : base(statementOwner, method, bSupportComponentModal)
        {
            OnConstruct();
        }
        public CaseBlock(Statement statementOwner, bool bSupportComponentModal) : base(statementOwner, bSupportComponentModal)
        {
            OnConstruct();
        }

        public string Value
        {
            get { return m_stringValue.Value == null || m_stringValue.Value == "" || m_stringValue.Value == "[null]" ? "SwitchName[???]" : m_stringValue.Value; }
            set { m_stringValue.Value = value; }
        }

        private void OnConstruct()
        {
            SetElement("Value", m_stringValue = new String());
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnConstruct();
        }

        protected override void OnParse(SyntaxErrorList syntaxerrorlist)
        {
            if (!(OwnerStatement is Switch))
                syntaxerrorlist.SetSyntaxError(new StatementSyntaxError(GetType().Name + "只能存在于"+ typeof(Switch).Name + "语句中", this));

            if (Value == "SwitchName[???]")
                syntaxerrorlist.SetSyntaxError(new StatementSyntaxError(GetType().Name + "的值因子不能为空", this));

            base.OnParse(syntaxerrorlist);
        }
    }
}
