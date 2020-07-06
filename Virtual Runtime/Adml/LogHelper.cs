using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eap;
namespace ExtEap
{
    public class LogHelper : Factor
    {
        public const string MethodNameLogInfo = "LogInfo";
        public const string MethodNameLogError = "LogError";
        public static string[] MethodNameValue = {MethodNameLogInfo, MethodNameLogInfo };

        private static ValidateStringValue m_validatestringvalue = new ValidateStringValue(new StringEnumeration(MethodNameValue).ValidateStringValue);
        private Eap.String m_stringMethodName;
        private ComponentArrayListDecorator m_componentarraylistdecoratorParameterFactor;

        public LogHelper(Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }

        public int ParameterFactorCount
        {
            get { return m_componentarraylistdecoratorParameterFactor.ComponentCount; }
        }

        private void OnConstruct()
        {
            SetElement("MethodName", m_stringMethodName = new Eap.String(m_validatestringvalue, MethodNameLogInfo));
            m_componentarraylistdecoratorParameterFactor = new ComponentArrayListDecorator(this, ComponentFactory, typeof(Factor), new object[] { OwnerStatement });
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_componentarraylistdecoratorParameterFactor.Initialize();

            OnConstruct();
        }
        public Factor GetParameterFactor(int nIndex)
        {
            return (Factor)m_componentarraylistdecoratorParameterFactor.GetComponent(nIndex);
        }
        public void SetParameterFactor(int nIndex, Factor factor, bool bIsInserting)
        {
            m_componentarraylistdecoratorParameterFactor.SetComponent(nIndex, factor, bIsInserting);
        }
        public void SetParameterFactor(int nIndex, Factor factor)
        {
            m_componentarraylistdecoratorParameterFactor.SetComponent(nIndex, factor, false);
        }
        //public 
    }
}
