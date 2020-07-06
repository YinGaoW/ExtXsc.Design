using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using XtremeCommandBars;
using XtremePropertyGrid;
using Eap;

namespace ExtEap
{
    public class SelectDesigner : StatementDesigner, IBlockyDesigner
    {
        public static Bitmap UnexpandedSelectBitmap = new Bitmap("..\\Image\\UnexpandedSelect.bmp");

        private AdmlClassDesignerHelper m_admlclassdesignerhelper;
        private BlockDesigner m_blockdesignerTrue;
        private BlockDesigner m_blockdesignerFalse;
        private ContentDesignerProperty m_contentdesignerproperty;
        private FactorContainerDesigner m_factorcontainerdesignerCondition;

        public SelectDesigner(ComponentDesignDocumentView componentdesigndocumentview, Select select) : base(componentdesigndocumentview, select)
        {
            OnConstruct();
        }
        public SelectDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, Select select) : base(componentdesigndocumentview, componentdesignspace, select)
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
        public Select Select
        {
            get { return (Select)Statement; }
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

        ContentDesignerProperty IBlockyDesigner.ContentDesignerProperty => throw new NotImplementedException();

        private void OnConstruct()
        {
            SetDesigner("ConditionFactorComponentContainer", m_factorcontainerdesignerCondition = new FactorContainerDesigner(ComponentDesignDocumentView, ComponentDesignSpace, GetComponentDesigner("ConditionFactorComponentContainer").Component));
            m_contentdesignerproperty = new ContentDesignerProperty();
            m_blockdesignerTrue = (BlockDesigner)GetDesigner("TrueBlock");
            m_blockdesignerFalse = (BlockDesigner)GetDesigner("FalseBlock");

            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(null, this);
            m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_factorcontainerdesignerCondition);
            m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_blockdesignerTrue);
            m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_blockdesignerFalse);
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnConstruct();
        }

        protected override void OnExpandInitially()
        {
            IsExpanded = ComponentDesignDocumentView.DesignControl is ComponentDesignDocumentView;
        }

        public override void ProvideAccessoryDescription(ArrayList arraylistDescription, int nType)
        {
            m_blockdesignerTrue.ProvideAccessoryDescription(arraylistDescription, nType);
            m_blockdesignerFalse.ProvideAccessoryDescription(arraylistDescription, nType);
        }

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            m_sizeComment = System.Drawing.Size.Empty;

            if (IsExpanded)
            {
                int nFontSize = AdmlClassDesignerHelper.CommentFont.Height;
                bool bConditionSelected = m_factorcontainerdesignerCondition.GetSelectedComponentDesigner() != null;

                m_factorcontainerdesignerCondition.Left = 0;
                m_factorcontainerdesignerCondition.Top = 0;
                m_factorcontainerdesignerCondition.Arrange(graphics);

                m_blockdesignerTrue.Left = 0;
                m_blockdesignerTrue.Top = 0;
                m_blockdesignerTrue.Arrange(graphics);

                m_blockdesignerFalse.Left = 0;
                m_blockdesignerFalse.Top = 0;
                m_blockdesignerFalse.Arrange(graphics);

                System.Drawing.Size size = new System.Drawing.Size(m_blockdesignerTrue.Width + m_blockdesignerFalse.Width + 1, Math.Max(m_blockdesignerTrue.Height, m_blockdesignerFalse.Height));

                if (m_blockdesignerTrue.IsExpanded)
                    m_blockdesignerTrue.Height = Math.Max(m_blockdesignerTrue.Height, m_blockdesignerFalse.Height);

                if (m_blockdesignerFalse.IsExpanded)
                    m_blockdesignerFalse.Height = Math.Max(m_blockdesignerTrue.Height, m_blockdesignerFalse.Height);

                size = new System.Drawing.Size(Math.Max(Math.Max(m_factorcontainerdesignerCondition.Width, nFontSize * 3), size.Width), ComponentGraphicsDesignView.ExpandedBitmap.Height + 1 + m_factorcontainerdesignerCondition.Height + nFontSize + nFontSize / 2 + 1 + size.Height + BottomInterval);
                size.Width = Math.Max(size.Width + ComponentGraphicsDesignView.ExpandedBitmap.Width, m_factorcontainerdesignerCondition.Width + ComponentGraphicsDesignView.ExpandedBitmap.Width + 1);

                if (IsCommentVisible)
                {
                    ArrangeComment(graphics, size.Width);

                    size.Width = Math.Max(size.Width, m_sizeComment.Width);
                    size.Height += m_sizeComment.Height;
                }

                Size = size;

                m_factorcontainerdesignerCondition.Move(Width / 2 - m_factorcontainerdesignerCondition.Width / 2, ComponentGraphicsDesignView.ExpandedBitmap.Height + m_sizeComment.Height + 1);
                m_blockdesignerTrue.Move((Width - m_blockdesignerTrue.Width - m_blockdesignerFalse.Width) / 2, ComponentGraphicsDesignView.ExpandedBitmap.Height + m_sizeComment.Height + 1 + m_factorcontainerdesignerCondition.Height + nFontSize + nFontSize / 2 + 1);
                m_blockdesignerFalse.Move(m_blockdesignerTrue.Rectangle.Right - Left + 1, ComponentGraphicsDesignView.ExpandedBitmap.Height + m_sizeComment.Height + 1 + m_factorcontainerdesignerCondition.Height + nFontSize + nFontSize / 2 + 1);
            }
            else if (!IsStatementVisible)
            {
                ArrangeComment(graphics, 0);
                Size = m_sizeComment;
            }
            else
            {
                System.Drawing.Size size = new System.Drawing.Size(Math.Max(ComponentGraphicsDesignView.ExpandedBitmap.Width, UnexpandedSelectBitmap.Width), Math.Max(ComponentGraphicsDesignView.ExpandedBitmap.Height, UnexpandedSelectBitmap.Height));

                if (IsCommentVisible)
                {
                    ArrangeComment(graphics, size.Width);

                    size.Width = Math.Max(size.Width, m_sizeComment.Width);
                    size.Height += m_sizeComment.Height;
                }

                Size = size;
            }
        }
        public override void Render(System.Drawing.Graphics graphics)
        {
            //ContentDesignerProperty.IsPenetrable = IsExpanded;
        }
    }
}
