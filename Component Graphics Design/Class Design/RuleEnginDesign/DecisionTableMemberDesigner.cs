using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Eap;
using System.Collections;

namespace ExtEap
{
    public class DecisionTableMemberDesigner : StatementDesigner
    {
        //public static Bitmap UnexpandedDecisionTableMemberBitmap = new Bitmap("");
        //public System.Drawing.Size SizeRuleDonditionValue;
        //public System.Drawing.Size SizeRuleBlock;
        private ContentDesignerProperty m_contentdesignerproperty;
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;
        private RuleBlockDesigner m_ruleblockdesigner;
        private FactorContainerDesigner m_factorcontainerdesignerRuleConditionValue;
        public DecisionTableMemberDesigner(ComponentDesignDocumentView componentdesigndocumentview, DecisionTableMember decisiontablemember) : base(componentdesigndocumentview, decisiontablemember)
        {
            OnConstruct();
        }
        public DecisionTableMemberDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, DecisionTableMember decisiontablemember) : base(componentdesigndocumentview, componentdesignspace, decisiontablemember)
        {
            OnConstruct();
        }

        public override bool HasStatment
        {
            get { return true; }
        }
        private int BottomInterval
        {
            get { return 12; }
        }
        public DecisionTableMember DecisionTableMember
        {
            get { return (DecisionTableMember)Statement; }
        }
        internal FactorContainerDesigner FactorContainerDesigner
        {
            get { return m_factorcontainerdesignerRuleConditionValue; }
        }
        public RuleBlockDesigner RuleBlockDesigner
        {
            get { return m_ruleblockdesigner; }
        }
        public override bool IsStatementVisible
        {
            get
            {
                if (IsExpanded)
                    return true;
                return base.IsStatementVisible;
            }
            set
            {
                bool bValue = IsCommentVisible;

                base.IsStatementVisible = value;

                if (IsExpanded)
                    IsCommentVisible = bValue;
            }
        }
        
        private void OnConstruct()
        {
            SetDesigner("RuleConditionValueFactor", m_factorcontainerdesignerRuleConditionValue = new FactorContainerDesigner(ComponentDesignDocumentView, ComponentDesignSpace, GetComponentDesigner("RuleConditionValueFactor").Component));
            m_ruleblockdesigner = (RuleBlockDesigner)GetDesigner("RuleBlock");

            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);
            m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_factorcontainerdesignerRuleConditionValue);
            m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_ruleblockdesigner);
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnConstruct();
        }

        protected override void OnExpandInitially()
        {
            IsExpanded = ComponentDesignDocumentView.DesignControl is ComponentGraphicsDesignView;
        }

        public override void ProvideAccessoryDescription(ArrayList arraylistDescription, int nType)
        {
            m_ruleblockdesigner.ProvideAccessoryDescription(arraylistDescription, nType);
        }

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            m_sizeComment = System.Drawing.Size.Empty;
            
            int nFontSize = AdmlClassDesignerHelper.CommentFont.Height;
            bool bRuleConditionSelected = m_factorcontainerdesignerRuleConditionValue.GetSelectedComponentDesigner() != null;

            m_factorcontainerdesignerRuleConditionValue.Left = 0;
            m_factorcontainerdesignerRuleConditionValue.Top = 0;
            m_factorcontainerdesignerRuleConditionValue.Arrange(graphics);

            m_ruleblockdesigner.Left = 0;
            m_ruleblockdesigner.Top = 0;
            m_ruleblockdesigner.Arrange(graphics);

            System.Drawing.Size size = new System.Drawing.Size(m_factorcontainerdesignerRuleConditionValue.Width + m_ruleblockdesigner.Width, Math.Max(m_factorcontainerdesignerRuleConditionValue.Height, m_ruleblockdesigner.Height));
            
            if (IsCommentVisible)
            {
                ArrangeComment(graphics, size.Width);

                size.Width = Math.Max(size.Width, m_sizeComment.Width);
                size.Height += m_sizeComment.Height;
            }

            Size = size;

            //m_factorcontainerdesignerRuleConditionValue.Move(1, Height / 2 - m_factorcontainerdesignerRuleConditionValue.Height / 2);
            //m_ruleblockdesigner.Move(1 + m_factorcontainerdesignerRuleConditionValue.Width + 1, Height / 2 - m_ruleblockdesigner.Height / 2);
        }
        public override void Render(System.Drawing.Graphics graphics)
        {
            //ContentDesignerProperty.IsPenetrable = IsExpanded;
            graphics.DrawRectangle(StatementSyntaxError != null ? WarningPen : Pens.WhiteSmoke, Left, Top, Width - 1, Height - 1);
            //graphics.FillRectangle(Brushes.Gainsboro, Left, Top, m_factorcontainerdesignerRuleConditionValue.Width, Height);
            graphics.FillRectangle(Brushes.Gainsboro, Left, Top, ((DecisionTableDesigner)OwnerComponentDesigner).SplitPosition[0], Height);
            m_factorcontainerdesignerRuleConditionValue.Render(graphics);
            m_ruleblockdesigner.ContentDesignerProperty.IsOutDesignerArriving = false;
            m_ruleblockdesigner.Render(graphics);
            RenderComment(graphics);
        }

        public override ComponentGraphicsDesigner FindComponentGraphicsDesigner(System.Drawing.Point point)
        {
            return m_admlclassdesignerhelper.FindComponentGraphicsDesigner(point, true);
        }

        public override bool OnMouseDown(object objSender, MouseEventArgs mouseeventargs)
        {
            if (m_admlclassdesignerhelper.MouseDown(objSender, mouseeventargs, false, new System.Drawing.Point(Left, Top + m_sizeComment.Height)))
                return true;

            return base.OnMouseDown(objSender, mouseeventargs);
        }
        public override void OnKeyDown(object objSender, KeyEventArgs keyeventargs)
        {
            if (!m_admlclassdesignerhelper.KeyDown(objSender, keyeventargs, false))
                base.OnKeyDown(objSender, keyeventargs);
        }
        public ContentDesignerProperty ContentDesignerProperty
        {
            get { return m_contentdesignerproperty; }
        }
        protected override void OnUpdate(Component componentPath, Component componentUpdated)
        {
            base.OnUpdate(componentPath, componentUpdated);

            if (m_admlclassdesignerhelper != null)
            {
                m_ruleblockdesigner = (RuleBlockDesigner)GetDesigner("RuleBlock");
                m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Clear();
                m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_factorcontainerdesignerRuleConditionValue);
                m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_ruleblockdesigner);
            }
        }
    }
}
