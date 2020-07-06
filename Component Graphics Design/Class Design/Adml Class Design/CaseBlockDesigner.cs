using System;
using System.Drawing;
using System.Windows.Forms;
using Eap;

namespace ExtEap
{
    public class CaseBlockDesigner : Eap.BlockDesigner
    {
        private System.Drawing.Size m_sizeCaseValue;
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;
        private DeclarationListDesigner m_declarationlistdesigner;

        public CaseBlockDesigner(ComponentDesignDocumentView componentdesigndocumentview, CaseBlock caseblock) : base(componentdesigndocumentview, caseblock)
        {
            OnConstruct();
        }
        public CaseBlockDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, CaseBlock caseblock) : base(componentdesigndocumentview, componentdesignspace, caseblock)
        {
            OnConstruct();
        }

        CaseBlock CaseBlock
        {
            get { return (CaseBlock)Statement; }
        }

        private void OnConstruct()
        {
            m_declarationlistdesigner = (DeclarationListDesigner)GetDesigner("DeclarationList");
            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(null, this);
            ResetAdmlClassDesignerHelperChild();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnConstruct();
        }

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            m_sizeComment = System.Drawing.Size.Empty;
            m_sizeCaseValue = MeasureString(graphics, CaseBlock.Value);

            if (IsExpanded)
            {
                System.Drawing.Size size = System.Drawing.Size.Empty;

                if (m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count > 0)
                {
                    size = m_admlclassdesignerhelper.ArrangeBlockChild(graphics, Interval);

                    size.Width += ComponentGraphicsDesignView.ExpandedBitmap.Width + 2;
                    size.Height += ComponentGraphicsDesignView.ExpandedBitmap.Height * 2;

                    if (IsCommentVisible)
                    {
                        ArrangeComment(graphics, size.Width);

                        size.Width = Math.Max(size.Width, m_sizeComment.Width);
                        size.Height += m_sizeComment.Height;
                    }

                    size.Width = Math.Max(size.Width, m_sizeCaseValue.Width);
                    size.Height += m_sizeCaseValue.Height;
                    Size = size;

                    m_admlclassdesignerhelper.MoveBlockChild(ComponentGraphicsDesignView.ExpandedBitmap.Height + m_sizeComment.Height + m_sizeCaseValue.Height, Interval);
                }
                else
                {
                    size = MeasureString(graphics, Block.GetType().Name);
                    size.Width += ComponentGraphicsDesignView.ExpandedBitmap.Width + 2;
                    size.Height = Math.Max(size.Height, ComponentGraphicsDesignView.ExpandedBitmap.Height);

                    if (IsCommentVisible)
                    {
                        ArrangeComment(graphics, size.Width);
                        size.Width = Math.Max(size.Width, m_sizeComment.Width);
                        size.Height += m_sizeComment.Height;
                    }

                    size.Width = Math.Max(size.Width, m_sizeCaseValue.Width);
                    size.Height += m_sizeCaseValue.Height;
                    Size = size;
                }
            }

            else if (!IsStatementVisible)
            {
                ArrangeComment(graphics, 0);
                Size = new System.Drawing.Size(Math.Max(m_sizeCaseValue.Width, m_sizeComment.Width), m_sizeCaseValue.Height + m_sizeComment.Height);
            }

            else
            {
                System.Drawing.Size size = new System.Drawing.Size(Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Width, UnexpandedBlockBitmap.Width), Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Height, UnexpandedBlockBitmap.Height));

                if (IsCommentVisible)
                {
                    ArrangeComment(graphics, size.Width);

                    size.Width = Math.Max(size.Width, m_sizeComment.Width);
                    size.Height += m_sizeComment.Height;
                }

                size.Width = Math.Max(size.Width, m_sizeCaseValue.Width);
                size.Height += m_sizeCaseValue.Height;
                Size = size;
            }
        }

        public override void Render(System.Drawing.Graphics graphics)
        {
            DrawString(graphics, CaseBlock.Value, Brushes.Green, Left + (Width - m_sizeCaseValue.Width) / 2, Top + m_sizeComment.Height + 1);

            if (ComponentDesignExtension.ThumbnailView.IsPainting)
            {
                if (IsExpanded)
                {
                    graphics.DrawRectangle(StatementSyntaxError != null ? WarningPen : Pens.WhiteSmoke, Left, Top, Width - 1, Height - 1);
                    graphics.DrawImage(ComponentGraphicsDesignView.ExpandedBitmap, Left, Top + m_sizeComment.Height + m_sizeCaseValue.Height);

                    ClassRelatedDesigner[] classrelateddesignerChild = m_admlclassdesignerhelper.RenderBlockChild(graphics);

                    if (classrelateddesignerChild[0] != null)
                        ComponentGraphicsDesignView.DrawLine(graphics, IsSelected || ContentDesignerProperty.IsOutDesignerArriving ? 2 : 0, (classrelateddesignerChild[0] is IBlockyDesigner && ((IBlockyDesigner)classrelateddesignerChild[0]).ContentDesignerProperty.IsPenetrable) ? false : true, new System.Drawing.Point(Left + Width / 2, Top + m_sizeComment.Height + m_sizeCaseValue.Height), new System.Drawing.Point(classrelateddesignerChild[0].Left + classrelateddesignerChild[0].Width / 2, classrelateddesignerChild[0].Top));

                    if (OwnerComponentDesigner != null || classrelateddesignerChild[0] != null)
                        ComponentGraphicsDesignView.DrawLine(graphics, ContentDesignerProperty.IsInnerDesignerOuting ? 2 : 0, false, new System.Drawing.Point(Left + Width / 2, classrelateddesignerChild[0] == null ? Top + m_sizeComment.Height + m_sizeCaseValue.Height : classrelateddesignerChild[1].Rectangle.Bottom), new System.Drawing.Point(Left + Width / 2, Rectangle.Bottom));
                }
                else if (IsStatementVisible)
                {
                    if (Width > UnexpandedBlockBitmap.Width)
                        graphics.DrawImage(UnexpandedBlockBitmap, Left, Top + m_sizeComment.Height + m_sizeCaseValue.Height, Width, UnexpandedBlockBitmap.Height);

                    else
                        graphics.DrawImage(UnexpandedBlockBitmap, Left + (Width - Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Width, UnexpandedBlockBitmap.Width)) / 2, Top + m_sizeComment.Height + m_sizeCaseValue.Height);
                    
                    if (IsChildParsedSyntaxError || StatementSyntaxError != null)
                        graphics.DrawRectangle(WarningPen, Left, Top, Width - 1, Height - 1);

                    graphics.DrawImage(ComponentGraphicsDesignView.UnexpandedBitmap, Left, Top + m_sizeComment.Height + m_sizeCaseValue.Height);
                }
            }

            else
            {
                ContentDesignerProperty.IsPenetrable = IsExpanded || m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count == 0;

                if (IsExpanded)
                {
                    graphics.DrawRectangle(StatementSyntaxError != null ? WarningPen : Pens.WhiteSmoke, Left, Top, Width - 1, Height - 1);
                    graphics.DrawImage(ComponentGraphicsDesignView.ExpandedBitmap, Left, Top + m_sizeComment.Height + m_sizeCaseValue.Height);

                    if (m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count > 0)
                    {
                        m_admlclassdesignerhelper.RenderBlockChild(graphics, IsSelected || ContentDesignerProperty.IsOutDesignerArriving);

                        ClassRelatedDesigner classrelateddesignerChildTop = (ClassRelatedDesigner)m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner[0];
                        ComponentGraphicsDesignView.DrawLine(graphics, IsSelected || ContentDesignerProperty.IsOutDesignerArriving ? 2 : 0, (classrelateddesignerChildTop is IBlockyDesigner && ((IBlockyDesigner)classrelateddesignerChildTop).ContentDesignerProperty.IsPenetrable) ? false : true, new System.Drawing.Point(Left + Width / 2, Top + m_sizeComment.Height + m_sizeCaseValue.Height), new System.Drawing.Point(classrelateddesignerChildTop.Left + classrelateddesignerChildTop.Width / 2, classrelateddesignerChildTop.Top));
                    }
                    else
                        ContentDesignerProperty.IsInnerDesignerOuting = ContentDesignerProperty.IsOutDesignerArriving;

                    if (OwnerComponentDesigner != null || m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count > 0)
                        ComponentGraphicsDesignView.DrawLine(graphics, ContentDesignerProperty.IsInnerDesignerOuting ? 2 : 0, false, new System.Drawing.Point(Left + Width / 2, m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count == 0 ? Top + m_sizeComment.Height + m_sizeCaseValue.Height : ((ClassRelatedDesigner)m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner[m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count - 1]).Rectangle.Bottom), new System.Drawing.Point(Left + Width / 2, Rectangle.Bottom));
                }
                else if (IsStatementVisible)
                {
                    ContentDesignerProperty.IsInnerDesignerOuting = m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count == 0 && ContentDesignerProperty.IsOutDesignerArriving;

                    //if (Width > UnexpandedBlockBitmap.Width)
                    //    graphics.DrawImage(UnexpandedBlockBitmap, Left, Top + m_sizeComment.Height + m_sizeCaseValue.Height, Width, UnexpandedBlockBitmap.Height);

                    //else
                        graphics.DrawImage(UnexpandedBlockBitmap, Left + (Width - Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Width, UnexpandedBlockBitmap.Width)) / 2, Top + m_sizeComment.Height + m_sizeCaseValue.Height);

                    if (IsChildParsedSyntaxError || StatementSyntaxError != null)
                        graphics.DrawRectangle(WarningPen, Left, Top, Width - 1, Height - 1);

                    graphics.DrawImage(ComponentGraphicsDesignView.UnexpandedBitmap, Left, Top + m_sizeComment.Height + m_sizeCaseValue.Height);
                }
            }

            RenderComment(graphics);
        }

        public override ComponentGraphicsDesigner FindComponentGraphicsDesigner(System.Drawing.Point point)
        {
            return m_admlclassdesignerhelper.FindComponentGraphicsDesigner(point, true);
        }

        public override bool OnMouseDown(object objSender, MouseEventArgs mouseeventargs)
        {
            return m_admlclassdesignerhelper.MouseDown(objSender, mouseeventargs, true, new System.Drawing.Point(Left, Top + m_sizeComment.Height + m_sizeCaseValue.Height));
        }

        public override void OnKeyDown(object objSender, KeyEventArgs keyeventargs)
        {
            if (!m_admlclassdesignerhelper.KeyDown(objSender, keyeventargs, true))
                base.OnKeyDown(objSender, keyeventargs);
        }

        public override void Navigate(KeyEventArgs keyeventargs)
        {
            m_admlclassdesignerhelper.Navigate(keyeventargs, true, false);
        }

        private void ResetAdmlClassDesignerHelperChild()
        {
            m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Clear();

            if (m_declarationlistdesigner.UnnamedComponentDesignerCount > 0)
                m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_declarationlistdesigner);

            for (int nIndex = NamedDesignerCount; nIndex < DesignerCount; nIndex++)
            {
                Designer designerChild = GetDesigner(nIndex);

                if (designerChild is ClassRelatedDesigner)
                    m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(designerChild);
            }
        }
        protected override void OnUpdate(Component componentPath, Component componentUpdated)
        {
            base.OnUpdate(componentPath, componentUpdated);

            if (m_admlclassdesignerhelper != null)
            {
                m_declarationlistdesigner = (DeclarationListDesigner)GetDesigner("DeclarationList");
                ResetAdmlClassDesignerHelperChild();
            }
        }
    }
}
