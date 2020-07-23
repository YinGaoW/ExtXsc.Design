using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eap;

namespace ExtEap
{
    public class RuleConditionFactor : Factor
    {
        private ComponentContainerDecorator m_componentcontainerdecoratorConditionValueFactor;
        public RuleConditionFactor(Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }

        public Factor ConditionValueFactor
        {
            get { return (Factor)m_componentcontainerdecoratorConditionValueFactor.Component; }
        }
        
        private void OnConstruct()
        {
            m_componentcontainerdecoratorConditionValueFactor = new ComponentContainerDecorator(this, ComponentFactory, typeof(Factor), new object[] { OwnerStatement });
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_componentcontainerdecoratorConditionValueFactor.Initialize();

            OnConstruct();
        }

        protected override Eap.Object OnExecute(AdmlObject admlobject, ExecutionStack executionstack)
        {
            return new BooleanObject(admlobject.ClassLibrary, ((BooleanObject)ConditionValueFactor.Execute(admlobject, executionstack)).Value);
        }

        protected override string OnParse(SyntaxErrorList syntaxerrorlist)
        {
            if (ConditionValueFactor == null || ConditionValueFactor.Parse(syntaxerrorlist) != BooleanClass.NameStatic)
            {
                syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("规则条件参数因子必须填写且参数必须为布尔类型", this));
                return null;
            }

            return BooleanClass.NameStatic;
        }
    }
}
