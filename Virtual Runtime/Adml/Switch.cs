using Eap;
namespace ExtEap
{
    public class Switch : Statement
    {
        private ComponentContainer m_componentcontainerValueFactor;
        private ComponentArrayListDecorator m_componentarraylistdecoratorCaseBlock;
        
        public Switch(Statement statementOwner, Method method) : base(statementOwner, method)
        {
            OnConstruct();
        }
        public Switch(Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }

        public Factor ValueFactor
        {
            get { return (Factor)m_componentcontainerValueFactor.Component; }
            set { m_componentcontainerValueFactor.Component = value; }
        }
        public int CaseBlockCount
        {
            get { return m_componentarraylistdecoratorCaseBlock.ComponentCount; }
        }

        private void OnConstruct()
        {
            SetComponent("ValueFactorComponentContainer", m_componentcontainerValueFactor = new ComponentContainer(ComponentFactory, typeof(Factor), new object[] { this }));
            m_componentarraylistdecoratorCaseBlock = new ComponentArrayListDecorator(this, ComponentFactory, typeof(CaseBlock), new object[] { this, Method });
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_componentarraylistdecoratorCaseBlock.Initialize();

            OnConstruct();
        }

        public CaseBlock GetCaseBlock(int nIndex)
        {
            return (CaseBlock)m_componentarraylistdecoratorCaseBlock.GetComponent(nIndex);
        }
        public void SetCaseBlock(int nIndex, CaseBlock caseblock, bool bIsInserting)
        {
            m_componentarraylistdecoratorCaseBlock.SetComponent(nIndex, caseblock, bIsInserting);
        }
        public void SetCaseBlock(int nIndex, CaseBlock caseblock)
        {
            m_componentarraylistdecoratorCaseBlock.SetComponent(nIndex, caseblock, false);
        }

        protected override Object OnExecute(AdmlObject admlobject, ExecutionStack executionstack)
        {
            string strExecutionName = executionstack.GetExecutionName();

            if (strExecutionName != null)
            {
                if (strExecutionName == "ValueFactor")
                    goto ValueFactor;

                if (strExecutionName.StartsWith("CaseBlock"))
                    goto CaseBlock;
            }

            executionstack.PushExecutionName("ValueFactor");

            ValueFactor:

            Object objectValue = ValueFactor.Execute(admlobject, executionstack);

            executionstack.PopExecutionName();
            executionstack.PushObject(objectValue);

            executionstack.PushExecutionName("CaseBlock");

            CaseBlock:

            CaseBlock caseblock = FindCaseBlock(executionstack.GetTopObject(0));
            Object objectReturn = caseblock != null ? caseblock.Execute(admlobject, executionstack) : base.OnExecute(admlobject, executionstack);

            executionstack.PopObject();
            executionstack.PopExecutionName();

            return objectReturn;
        }
        protected override void OnParse(SyntaxErrorList syntaxerrorlist)
        {
            if (ValueFactor == null)
                syntaxerrorlist.SetSyntaxError(new StatementSyntaxError("值因子不能为空", this));

            if (CaseBlockCount == 0)
                syntaxerrorlist.SetSyntaxError(new StatementSyntaxError(GetType().Name + "还未添加" + typeof(CaseBlock).Name, this));

            if (StringClass.NameStatic != ValueFactor.Parse(syntaxerrorlist) && IntegerClass.NameStatic != ValueFactor.Parse(syntaxerrorlist) && FloatClass.NameStatic != ValueFactor.Parse(syntaxerrorlist))
                syntaxerrorlist.SetSyntaxError(new StatementSyntaxError("值因子必须为" + StringClass.NameStatic + "类型或" + IntegerClass.NameStatic + "类型或" + FloatClass.NameStatic + "类型", this));
            
            for (int nIndex = 0; nIndex < CaseBlockCount; nIndex++)
                GetCaseBlock(nIndex).Parse(syntaxerrorlist);
        }

        private CaseBlock FindCaseBlock(Object objectValue)
        {
            for (int nIndex = 0; nIndex < CaseBlockCount; nIndex++)
            {
                CaseBlock caseblock = GetCaseBlock(nIndex);

                if (((IBaseData)objectValue).BaseData.ToString() == caseblock.Value)
                    return caseblock;
            }

            return null;
        }
    }
}
