using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eap;
namespace ExtEap
{
    public class RuleBlock : Eap.Block
    {
        private Integer m_integerPriority;
        public RuleBlock(Statement statementOwner) : base(statementOwner)
        {
        }

        public RuleBlock(Statement statementOwner, Method method) : base(statementOwner, method)
        {
        }

        public RuleBlock(Statement statementOwner, bool bSupportComponentModal) : base(statementOwner, bSupportComponentModal)
        {
        }

        public RuleBlock(Statement statementOwner, Method method, bool bSupportComponentModal) : base(statementOwner, method, bSupportComponentModal)
        {
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
