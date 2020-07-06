using System.Collections;
using Eap;

namespace ExtEap
{
    public class DomainRow : Component
    {
        private String m_stringDomainName;
        private Boolean m_booleanUpdateDomainIsSelected;
        private Boolean m_booleanIndexDomainIsSelected;
        private String m_stringOperator;

        public DomainRow()
        {
            OnConstruct();
        }
        public DomainRow(string strDomainName)
        {
            OnConstruct(strDomainName);
        }

        public string DomainName
        {
            get { return m_stringDomainName.Value; }
            set { m_stringDomainName.Value = value; }
        }
        public bool UpdateDomainIsSelected
        {
            get { return m_booleanUpdateDomainIsSelected.Value; }
            set { m_booleanUpdateDomainIsSelected.Value = value; }
        }
        public bool IndexDomainIsSelected
        {
            get { return m_booleanIndexDomainIsSelected.Value; }
            set { m_booleanIndexDomainIsSelected.Value = value; }
        }
        public string Operator
        {
            get { return m_stringOperator.Value; }
            set { m_stringOperator.Value = value; }
        }

        private void OnConstruct()
        {
            SetElement("DomainName", m_stringDomainName = new String());
            SetElement("UpdateDomainIsSelected", m_booleanUpdateDomainIsSelected = new Boolean(false));
            SetElement("IndexDomainIsSelected", m_booleanIndexDomainIsSelected = new Boolean(false));
            SetElement("Operator", m_stringOperator = new String("="));
        }
        private void OnConstruct(string strDomainName)
        {
            SetElement("DomainName", m_stringDomainName = new String(strDomainName));
            SetElement("UpdateDomainIsSelected", m_booleanUpdateDomainIsSelected = new Boolean(false));
            SetElement("IndexDomainIsSelected", m_booleanIndexDomainIsSelected = new Boolean(false));
            SetElement("Operator", m_stringOperator = new String("="));
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnConstruct();
        }
    }
    public class DomainTable : Component
    {
        private String m_stringDomainTableTitle;
        private String m_stringDomainTableHeader;
        private ComponentArrayList m_componentarraylistDomainTableData;
        private int m_nCurrentCount;

        public DomainTable()
        {
            OnConstruct();
        }

        public string DomainTableTitle
        {
            get { return m_stringDomainTableTitle.Value; }
            set { m_stringDomainTableTitle.Value = value; }
        }
        public string DomainTableHeader
        {
            get { return m_stringDomainTableHeader.Value; }
            set { m_stringDomainTableHeader.Value = value; }
        }
        public ComponentArrayList TableData
        {
            get { return m_componentarraylistDomainTableData; }
        }
        public int RowCount
        {
            get { return TableData.ComponentCount; }
        }
        public int CurrentCount
        {
            get { return m_nCurrentCount; }
            set { m_nCurrentCount = value; }
        }

        private void OnConstruct()
        {
            SetElement("DomainTableTitle", m_stringDomainTableTitle = new String());
            SetElement("DomainTableHeader", m_stringDomainTableHeader = new String());
            SetComponent("TableData", m_componentarraylistDomainTableData = new ComponentArrayList(typeof(DomainRow)));
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnConstruct();
        }

        public void AddDomainRow(DomainRow domainrow)
        {
            if (domainrow == null)
                return;

            if (RowCount == 0)
                TableData.SetComponent(CurrentCount++, domainrow);

            else
            {
                bool bIsFind = false;

                for (int nIndex = 0; nIndex < RowCount; nIndex++)
                {
                    DomainRow row = (DomainRow)TableData.GetComponent(nIndex);

                    if (row.DomainName == domainrow.DomainName)
                    {
                        row = domainrow;
                        bIsFind = true;
                        break;
                    }
                }

                if (!bIsFind)
                    TableData.SetComponent(CurrentCount++, domainrow);
            }
        }
        public void ClearDomainTable()
        {
            int nCount = RowCount;

            for (int nIndex = nCount - 1; nIndex >= 0; nIndex--)
                TableData.SetComponent(nIndex, null);

            CurrentCount = 0;
            m_stringDomainTableTitle.Value = null;
            m_stringDomainTableHeader.Value = null;
        }
        public void Toggle(string strDomainName, bool bIsToggleIndexDomainIsSelected)
        {
            for (int nIndex = 0; nIndex < RowCount; nIndex++)
            {
                DomainRow row = (DomainRow)TableData.GetComponent(nIndex);

                if (row != null && row.DomainName == strDomainName)
                {
                    if (bIsToggleIndexDomainIsSelected)
                        row.IndexDomainIsSelected = !row.IndexDomainIsSelected;

                    else
                        row.UpdateDomainIsSelected = !row.UpdateDomainIsSelected;

                    return;
                }
            }
        }
        public void ChangeOperator(string strDomainName, string strOperator)
        {
            for (int nIndex = 0; nIndex < RowCount; nIndex++)
            {
                DomainRow row = (DomainRow)TableData.GetComponent(nIndex);

                if (row != null && row.DomainName == strDomainName)
                {
                    row.Operator = strOperator;
                    return;
                }
            }
        }
        public DomainRow GetDomainRowByDomainName(string strDomainName)
        {
            if (strDomainName != null && strDomainName != "")
            {
                for (int nIndex = 0; nIndex < RowCount; nIndex++)
                {
                    DomainRow row = (DomainRow)TableData.GetComponent(nIndex);

                    if (row.DomainName == strDomainName)
                        return row;
                }
            }

            return null;
        }
    }
    public class InvokeMethod : Factor
    {
        public const int CommandForSelectMethod = 66;
        public const int CommandForUpdateMethod = 88;
        public static string[] OperatorValue = new string[] { "=", "!=", "<", "<=", ">=", ">" };
        private ComponentContainerDecorator m_componentcontainerdecoratorFactor;
        private String m_stringMethodName;
        private ComponentArrayList m_componentarraylistParameterFactor;
        private DomainTable m_domaintable;
        public InvokeMethod(Eap.Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }

        public Factor Factor
        {
            get { return (Factor)m_componentcontainerdecoratorFactor.Component; }
            set { m_componentcontainerdecoratorFactor.Component = value; }
        }
        public string MethodName
        {
            get { return m_stringMethodName.Value; }
            set { m_stringMethodName.Value = value; }
        }
        public int ParameterFactorCount
        {
            get { return m_componentarraylistParameterFactor.ComponentCount; }
        }
        public DomainTable DomainTable
        {
            get { return m_domaintable; }
        }

        private void OnConstruct()
        {
            m_componentcontainerdecoratorFactor = new ComponentContainerDecorator(this, ComponentFactory, typeof(Factor), new object[] { OwnerStatement });
            SetElement("MethodName", m_stringMethodName = new String());
            SetComponent("ParameterFactorComponentArrayList", m_componentarraylistParameterFactor = new ComponentArrayList(ComponentFactory, typeof(Factor), new object[] { OwnerStatement }));
            SetComponent("DomainTable", m_domaintable = new DomainTable());
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_componentcontainerdecoratorFactor.Initialize();

            OnConstruct();
        }

        public Factor GetParameterFactor(int nIndex)
        {
            return (Factor)m_componentarraylistParameterFactor.GetComponent(nIndex);
        }
        public void SetParameterFactor(int nIndex, Factor factor, bool bIsInserting)
        {
            m_componentarraylistParameterFactor.SetComponent(nIndex, factor, bIsInserting);
        }
        public void SetParameterFactor(int nIndex, Factor factor)
        {
            SetParameterFactor(nIndex, factor, false);
        }

        protected override Object OnExecute(AdmlObject admlobject, ExecutionStack executionstack)
        {
            int nExecutionIndex = ParameterFactorCount - 1;
            string strExecutionName = executionstack.GetExecutionName();

            if (strExecutionName != null)
            {
                if (strExecutionName == "InvokeMethod")
                    goto InvokeMethod;

                else if (strExecutionName == "Factor")
                    goto Factor;

                else
                {
                    nExecutionIndex = int.Parse(strExecutionName.Substring(16));

                    Object objectParameter = GetParameterFactor(nExecutionIndex).Execute(admlobject, executionstack);

                    executionstack.PopExecutionName();
                    executionstack.PushObject(objectParameter);

                    nExecutionIndex--;
                }
            }

            for (int nIndex = nExecutionIndex; nIndex >= 0; nIndex--)
            {
                executionstack.PushExecutionName("ParameterFactor:" + nIndex);
                Object objectParameter = GetParameterFactor(nIndex).Execute(admlobject, executionstack);

                executionstack.PopExecutionName();
                executionstack.PushObject(objectParameter);
            }

            executionstack.PushExecutionName("Factor");

            Factor:

            Object objectFactor = (Factor != null ? Factor.Execute(admlobject, executionstack) : admlobject);

            executionstack.PopExecutionName();
            executionstack.PushObject(objectFactor);

            for (int nIndex = 0; nIndex < ParameterFactorCount; nIndex++)
                executionstack.PushDeclaredObject(executionstack.GetTopObject(0, 1 + nIndex));

            executionstack.PushExecutionName("InvokeMethod");

            InvokeMethod:

            ArrayList arraylistParameterObject = new ArrayList();

            for (int nIndex = 0; nIndex < ParameterFactorCount; nIndex++)
                arraylistParameterObject.Add(executionstack.GetTopObject(1, 1 + nIndex));

            objectFactor = executionstack.GetTopObject(1);
            Object objectReturn = objectFactor is AdmlObject ? ((AdmlObject)objectFactor).InvokeMethod(MethodName, arraylistParameterObject, executionstack) : objectFactor.InvokeMethod(MethodName, arraylistParameterObject);

            if (objectReturn is ReferableObject)
                executionstack.CreateReference((ReferableObject)objectReturn);

            executionstack.PopExecutionName();
            executionstack.PopDeclaredObject(ParameterFactorCount);
            executionstack.PopObject(ParameterFactorCount + 1);
            return objectReturn;
        }

        protected override string OnParse(SyntaxErrorList syntaxerrorlist)
        {
            Class classFactor = AdmlClass;

            if (Factor != null)
            {
                string strClassName = Factor.Parse(syntaxerrorlist);

                if (strClassName == null)
                    return null;

                classFactor = ClassLibrary.GetClass(AdmlClass, strClassName);

                if (classFactor == null)
                {
                    syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("无法识别的对象因子类型", Factor));
                    return null;
                }
            }

            ArrayList arraylistClass = new ArrayList();

            for (int nIndex = 0; nIndex < ParameterFactorCount; nIndex++)
            {
                string strClassName = GetParameterFactor(nIndex).Parse(syntaxerrorlist);
                arraylistClass.Add(strClassName != null ? ClassLibrary.GetClass(AdmlClass, strClassName) : null);
            }

            GlobalMemberDeclarationList globalmemberdeclarationlist = classFactor.CreateGlobalMemberDeclarationList(Method.ExecutionEnvironment);

            for (int nIndex = 0; nIndex < globalmemberdeclarationlist.MemberDeclarationCount; nIndex++)
            {
                MemberDeclaration memberdeclaration = globalmemberdeclarationlist.GetMemberDeclaration(nIndex);

                if (memberdeclaration is MethodDeclaration && memberdeclaration.Name == MethodName)
                {
                    MethodDeclaration methoddeclaration = (MethodDeclaration)memberdeclaration;

                    if (arraylistClass.Count != methoddeclaration.ParameterDeclarationList.DeclarationCount)
                        continue;

                    else
                    {
                        bool bIsError = false;

                        for (int n = 0; n < arraylistClass.Count; n++)
                        {
                            Class classParameterFactor = (Class)arraylistClass[n];
                            string strClassName = methoddeclaration.ParameterDeclarationList.GetDeclaration(n).ClassName;

                            if (classParameterFactor == null || strClassName != string.Empty && classParameterFactor.Name != string.Empty && !classParameterFactor.IsDerivedClass(strClassName))
                            {
                                bIsError = true;
                                break;
                            }
                        }

                        if (bIsError)
                            continue;
                    }

                    if (methoddeclaration.ClassName == null)
                        return "";

                    else if (!methoddeclaration.ClassName.StartsWith("@") || Factor is This)
                        return methoddeclaration.ClassName;

                    else
                    {
                        DeclarationList declarationlist = classFactor is ICollectionClass ? AdmlClass.GenericityDeclarationList : ((ReferableClass)classFactor).GenericityDeclarationList;

                        if (declarationlist == null || declarationlist.DeclarationCount == 0)
                            return null;

                        string strDeclarationName = methoddeclaration.ClassName.Substring(1);
                        string strCollectionType = string.Empty;

                        nIndex = strDeclarationName.IndexOf("[");

                        if (nIndex >= 0)
                        {
                            strCollectionType = strDeclarationName.Substring(nIndex);
                            strDeclarationName = strDeclarationName.Substring(0, nIndex);
                        }

                        for (nIndex = 0; nIndex < declarationlist.DeclarationCount; nIndex++)
                        {
                            Declaration declaration = declarationlist.GetDeclaration(nIndex);

                            if (declaration.Name == strDeclarationName)
                            {
                                Class classGetting = ClassLibrary.GetClass(declaration.ClassName + strCollectionType);

                                if (!(classGetting is ReferableClass))
                                    return null;

                                return classGetting.Name;
                            }
                        }

                        return null;
                    }
                }
            }
            syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("引用未定义的方法", this));
            return null;
        }
        public void InitDomainTable(int nCommand, params DomainRow[] domainrow)
        {
            DomainTable.DomainTableTitle = "域表";

            if (nCommand == CommandForSelectMethod)
                DomainTable.DomainTableHeader = "域名称,索引域是否选择,操作符";

            else if (nCommand == CommandForUpdateMethod)
                DomainTable.DomainTableHeader = "域名称,更新域是否选择,索引域是否选择,操作符";

            for (int nIndex = 0; nIndex < domainrow.Length; nIndex++)
                DomainTable.AddDomainRow(domainrow[nIndex]);
        }
    }
}
