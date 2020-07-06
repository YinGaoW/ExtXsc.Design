using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eap;

namespace ExtEap
{
    public class RuleConditionFactorDesigner : FactorDesigner
    {
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;
        private FactorContainerDesigner m_factorcontainerdesignerRuleConditionValueFactor;
        public RuleConditionFactorDesigner(ComponentDesignDocumentView componentdesigndocumentview, RuleConditionFactor ruleconditionfactor) : base(componentdesigndocumentview, ruleconditionfactor)
        {
        }

        public RuleConditionFactorDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, RuleConditionFactor ruleconditionfactor) : base(componentdesigndocumentview, componentdesignspace, ruleconditionfactor)
        {
        }

        private void OnConstruct()
        {
            SetDesigner("RuleConditionValueFactorComponentContainer", m_factorcontainerdesignerRuleConditionValueFactor = new FactorContainerDesigner(ComponentDesignDocumentView, ComponentDesignSpace, GetComponentDesigner("RuleConditionValueFactorComponentContainer").Component));

            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);

            for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
            {
                m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(GetUnnamedComponentDesigner(nIndex));
            }
        }
    }
}
