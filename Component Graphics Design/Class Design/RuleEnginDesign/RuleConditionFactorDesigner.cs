using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eap;

namespace ExtEap
{
    public class RuleConditionFactorDesigner : Eap.FactorDesigner
    {
        private System.Drawing.Size m_sizeFactorDesigner;
        private FactorDesigner m_factordesigner;
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;
        public RuleConditionFactorDesigner(ComponentDesignDocumentView componentdesigndocumentview, RuleConditionFactor ruleconditionfactor) : base(componentdesigndocumentview, ruleconditionfactor)
        {
            OnConstruct();
        }

        public RuleConditionFactorDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, RuleConditionFactor ruleconditionfactor) : base(componentdesigndocumentview, componentdesignspace, ruleconditionfactor)
        {
            OnConstruct();
        }

        private void OnConstruct()
        {
            m_factordesigner = NamedDesignerCount < DesignerCount ? (FactorDesigner)GetDesigner(NamedDesignerCount) : null;
            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnConstruct();
        }

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            Left = 0;
            Top = 0;

            if (m_factordesigner == null)
                m_sizeFactorDesigner = MeasureString(graphics, "Factor[???]");

            else
            {
                m_factordesigner.Left = 0;
                m_factordesigner.Top = 0;
                m_factordesigner.Arrange(graphics);

                m_sizeFactorDesigner = m_factordesigner.Size;
            }

            Size = new System.Drawing.Size(m_sizeFactorDesigner.Width + 2, m_sizeFactorDesigner.Height);

            if (m_factordesigner != null)
                m_factordesigner.Move(1, (Size.Height - m_factordesigner.Height) / 2);
        }
        public override void Render(System.Drawing.Graphics graphics)
        {
            if (m_factordesigner == null)
                DrawString(graphics, "Factor[???]", KeywordBrush, Left + 1, Top + (Height - m_sizeFactorDesigner.Height) / 2);

            else
                m_factordesigner.Render(graphics);

            if (FactorSyntaxError != null)
                graphics.DrawLine(WarningPen, Left, Rectangle.Bottom - 1, Rectangle.Right, Rectangle.Bottom - 1);
        }

        public override ComponentGraphicsDesigner FindComponentGraphicsDesigner(System.Drawing.Point point)
        {
            if (m_factordesigner != null)
            {
                ComponentGraphicsDesigner componentgraphicsdesigner = m_factordesigner.FindComponentGraphicsDesigner(point);

                if (componentgraphicsdesigner != null)
                    return componentgraphicsdesigner;
            }

            return base.FindComponentGraphicsDesigner(point);
        }
        public override void OnKeyDown(object objSender, KeyEventArgs keyeventargs)
        {
            if (!m_admlclassdesignerhelper.KeyDown(objSender, keyeventargs, false))
                base.OnKeyDown(objSender, keyeventargs);
        }
        public override bool OnPasteUpdate(string stringClipboard)
        {
            return m_admlclassdesignerhelper.PasteUpdate(stringClipboard);
        }
        public override void OnPaste(string strClipboard, int nIndex)
        {
            m_admlclassdesignerhelper.Paste(nIndex, strClipboard);
        }
        public override void Navigate(KeyEventArgs keyeventargs)
        {
            m_admlclassdesignerhelper.Navigate(keyeventargs,false, false);
        }
        protected override void OnUpdate(Component componentPath, Component componentUpdated)
        {
            base.OnUpdate(componentPath, componentUpdated);

            if (m_admlclassdesignerhelper != null)
                OnConstruct();
        }
    }
}
