using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Eap;

namespace ExtEap
{
    public class DecisionTableDesigner : StatementDesigner
    {
        //UnexpandedBlock UnexpandedDecisionTable
        public int[] SplitPosition;
        public static Bitmap UnexpandedDecisionTableBitmap = new Bitmap("..\\Image\\UnexpandedBlock.bmp");
        private System.Drawing.Size m_sizeTitle;
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;
        public DecisionTableDesigner(ComponentDesignDocumentView componentdesigndocumentview, DecisionTable decisiontable) : base(componentdesigndocumentview, decisiontable)
        {
            OnConstruct();
        }
        public DecisionTableDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, DecisionTable decisiontable) : base(componentdesigndocumentview, componentdesignspace, decisiontable)
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
        public DecisionTable DecisionTable
        {
            get { return (DecisionTable)Statement; }
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
            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);

            for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(GetUnnamedComponentDesigner(nIndex));
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
            for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                ((DecisionTableMemberDesigner)GetUnnamedComponentDesigner(nIndex)).ProvideAccessoryDescription(arraylistDescription, nType);
        }

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            m_sizeComment = System.Drawing.Size.Empty;

            if (IsExpanded)
            {
                SplitPosition = new int[2];
                m_sizeTitle = MeasureString(graphics, DecisionTable.GetType().Name);
                System.Drawing.Size size = new System.Drawing.Size(m_sizeTitle.Width + ComponentGraphicsDesignView.ExpandedBitmap.Width + 1, m_sizeTitle.Height);

                for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                {
                    DecisionTableMemberDesigner decisiontablememberdesigner = (DecisionTableMemberDesigner)GetUnnamedComponentDesigner(nIndex);
                    decisiontablememberdesigner.Left = 0;
                    decisiontablememberdesigner.Top = 0;
                    decisiontablememberdesigner.Arrange(graphics);

                    SplitPosition[0] = Math.Max(decisiontablememberdesigner.FactorContainerDesigner.Width, SplitPosition[0]);
                    SplitPosition[1] = Math.Max(decisiontablememberdesigner.RuleBlockDesigner.Width, SplitPosition[1]);
                    
                    size.Width = Math.Max(size.Width, SplitPosition[0] + SplitPosition[1]);
                    size.Height += decisiontablememberdesigner.Height + 1;
                }

                for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                {
                    DecisionTableMemberDesigner decisiontablememberdesigner = (DecisionTableMemberDesigner)GetUnnamedComponentDesigner(nIndex);
                    decisiontablememberdesigner.Size = new System.Drawing.Size(SplitPosition[0] + SplitPosition[1], decisiontablememberdesigner.Height);
                    decisiontablememberdesigner.FactorContainerDesigner.Move(SplitPosition[0] / 2 - decisiontablememberdesigner.FactorContainerDesigner.Width / 2, decisiontablememberdesigner.Height / 2 - decisiontablememberdesigner.FactorContainerDesigner.Height / 2);
                    decisiontablememberdesigner.RuleBlockDesigner.Move(SplitPosition[0] + (SplitPosition[1] / 2 - decisiontablememberdesigner.RuleBlockDesigner.Width / 2), decisiontablememberdesigner.Height / 2 - decisiontablememberdesigner.RuleBlockDesigner.Height / 2);
                }

                if (IsCommentVisible)
                {
                    ArrangeComment(graphics, size.Width);

                    size.Width = Math.Max(size.Width, m_sizeComment.Width);
                    size.Height += m_sizeComment.Height;
                }

                Size = size;
                int nTop = m_sizeTitle.Height;

                for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                {
                    DecisionTableMemberDesigner decisiontablememberdesigner = (DecisionTableMemberDesigner)GetUnnamedComponentDesigner(nIndex);
                    decisiontablememberdesigner.Move(Width / 2 - decisiontablememberdesigner.Width / 2, nTop);
                    nTop += decisiontablememberdesigner.Height + 1;
                }
            }
            else if (!IsStatementVisible)
            {
                ArrangeComment(graphics, 0);
                Size = m_sizeComment;
            }
            else
            {
                System.Drawing.Size size = new System.Drawing.Size(Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Width, UnexpandedDecisionTableBitmap.Width), Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Height, UnexpandedDecisionTableBitmap.Height));

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
            if (IsExpanded)
            {
                graphics.DrawRectangle(StatementSyntaxError != null ? WarningPen : Pens.WhiteSmoke, Left, Top, Width - 1, Height - 1);

                graphics.FillRectangle(Brushes.Gainsboro, Left, Top + m_sizeComment.Height, Width, m_sizeTitle.Height);
                graphics.DrawImage(ComponentGraphicsDesignView.ExpandedBitmap, Left, Top + m_sizeComment.Height);

                DrawString(graphics, DecisionTable.GetType().Name, Brushes.Black, Left + ComponentGraphicsDesignView.ExpandedBitmap.Width + 1, Top + m_sizeComment.Height);

                for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                {
                    DecisionTableMemberDesigner decisiontablememberdesigner = (DecisionTableMemberDesigner)GetUnnamedComponentDesigner(nIndex);
                    decisiontablememberdesigner.Render(graphics);
                }
            }
            else if (IsStatementVisible)
            {
                graphics.DrawRectangle(IsChildParsedSyntaxError || StatementSyntaxError != null ? WarningPen : Pens.WhiteSmoke, Left, Top, Width - 1, Height - 1);
                graphics.DrawImage(UnexpandedDecisionTableBitmap, Left + (Width - Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Width, UnexpandedDecisionTableBitmap.Width)) / 2, Top + m_sizeComment.Height);
                graphics.DrawImage(ComponentGraphicsDesignView.UnexpandedBitmap, Left, Top + m_sizeComment.Height);
            }

            RenderComment(graphics);
        }
        public override ComponentGraphicsDesigner FindComponentGraphicsDesigner(System.Drawing.Point point)
        {
            return m_admlclassdesignerhelper.FindComponentGraphicsDesigner(point, true);
        }

        public override bool OnMouseDown(object objSender, MouseEventArgs mouseeventargs)
        {
            return m_admlclassdesignerhelper.MouseDown(objSender, mouseeventargs, true, new System.Drawing.Point(Left, Top + m_sizeComment.Height));
        }
        public override void OnKeyDown(object objSender, KeyEventArgs keyeventargs)
        {
            if (!m_admlclassdesignerhelper.KeyDown(objSender, keyeventargs, true))
                base.OnKeyDown(objSender, keyeventargs);
        }

        public override void Navigate(KeyEventArgs keyeventargs)
        {
            m_admlclassdesignerhelper.Navigate(keyeventargs, true, true);
        }
        protected override void OnUpdate(Component componentPath, Component componentUpdated)
        {
            base.OnUpdate(componentPath, componentUpdated);

            if (m_admlclassdesignerhelper != null)
            {
                m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Clear();
                
                for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                    m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(GetUnnamedComponentDesigner(nIndex));
            }
        }
    }
}
