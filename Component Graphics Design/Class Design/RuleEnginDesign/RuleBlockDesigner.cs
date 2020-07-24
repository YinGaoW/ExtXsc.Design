using System;
using System.Drawing;
using System.Windows.Forms;
using Eap;

namespace ExtEap
{
    public class RuleBlockDesigner : Eap.BlockDesigner
    {
        private static readonly System.Drawing.Font m_fontPriority = new System.Drawing.Font(WorkbenchFramework.InstanceWorkbenchFramework.Font.FontFamily, WorkbenchFramework.InstanceWorkbenchFramework.Font.Size * 2 / 3, WorkbenchFramework.InstanceWorkbenchFramework.Font.Style, WorkbenchFramework.InstanceWorkbenchFramework.Font.Unit, WorkbenchFramework.InstanceWorkbenchFramework.Font.GdiCharSet);
        private System.Drawing.Size m_sizePriority;
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;
        private DeclarationListDesigner m_declarationlistdesigner;
        public RuleBlockDesigner(ComponentDesignDocumentView componentdesigndocumentview, RuleBlock ruleblock) : base(componentdesigndocumentview, ruleblock)
        {
            OnConstruct();
        }
        public RuleBlockDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, RuleBlock ruleblock) : base(componentdesigndocumentview, componentdesignspace, ruleblock)
        {
            OnConstruct();
        }

        public RuleBlock RuleBlock
        {
            get { return (RuleBlock)Statement; }
        }

        private void OnConstruct()
        {
            m_declarationlistdesigner = (DeclarationListDesigner)GetDesigner("DeclarationList");
            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);
            ResetAdmlClassDesignerHelperChild();
        }

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
            WorkbenchFramework.InstanceWorkbenchFramework.Font = m_fontPriority;
            m_sizePriority = MeasureString(graphics, RuleBlock.Priority.ToString());
            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;

            base.Arrange(graphics);
        }
        public override void Render(System.Drawing.Graphics graphics)
        {
            if (ComponentDesignExtension.ThumbnailView.IsPainting)
            {
                if (IsExpanded)
                {
                    graphics.DrawRectangle(StatementSyntaxError != null ? WarningPen : Pens.WhiteSmoke, Left, Top, Width - 1, Height - 1);
                    graphics.DrawImage(ComponentGraphicsDesignView.ExpandedBitmap, Left, Top + m_sizeComment.Height);

                    ClassRelatedDesigner[] classrelateddesignerChild = m_admlclassdesignerhelper.RenderBlockChild(graphics);

                    if (classrelateddesignerChild[0] != null)
                        ComponentGraphicsDesignView.DrawLine(graphics, IsSelected || ContentDesignerProperty.IsOutDesignerArriving ? 2 : 0, (classrelateddesignerChild[0] is IBlockyDesigner && ((IBlockyDesigner)classrelateddesignerChild[0]).ContentDesignerProperty.IsPenetrable) ? false : true, new System.Drawing.Point(Left + Width / 2, Top + m_sizeComment.Height), new System.Drawing.Point(classrelateddesignerChild[0].Left + classrelateddesignerChild[0].Width / 2, classrelateddesignerChild[0].Top));

                    if (OwnerComponentDesigner != null || classrelateddesignerChild[0] != null)
                        ComponentGraphicsDesignView.DrawLine(graphics, ContentDesignerProperty.IsInnerDesignerOuting ? 2 : 0, false, new System.Drawing.Point(Left + Width / 2, classrelateddesignerChild[0] == null ? Top + m_sizeComment.Height : classrelateddesignerChild[1].Rectangle.Bottom), new System.Drawing.Point(Left + Width / 2, Rectangle.Bottom));

                    System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                    WorkbenchFramework.InstanceWorkbenchFramework.Font = m_fontPriority;
                    DrawString(graphics, RuleBlock.Priority.ToString(), Brushes.Blue, Rectangle.Right - m_sizePriority.Width, Top);
                    WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
                }
                else if (IsStatementVisible)
                {
                    graphics.DrawImage(UnexpandedBlockBitmap, Left + (Width - Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Width, UnexpandedBlockBitmap.Width)) / 2, Top + m_sizeComment.Height);

                    if (IsChildParsedSyntaxError || StatementSyntaxError != null)
                        graphics.DrawRectangle(WarningPen, Left, Top, Width - 1, Height - 1);

                    graphics.DrawImage(ComponentGraphicsDesignView.UnexpandedBitmap, Left, Top + m_sizeComment.Height);
                }
            }
            else
            {
                ContentDesignerProperty.IsPenetrable = IsExpanded || m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count == 0;

                if (IsExpanded)
                {
                    graphics.DrawRectangle(StatementSyntaxError != null ? WarningPen : Pens.WhiteSmoke, Left, Top, Width - 1, Height - 1);
                    graphics.DrawImage(ComponentGraphicsDesignView.ExpandedBitmap, Left, Top + m_sizeComment.Height);

                    if (m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count > 0)
                    {
                        m_admlclassdesignerhelper.RenderBlockChild(graphics, IsSelected || ContentDesignerProperty.IsOutDesignerArriving);

                        ClassRelatedDesigner classrelateddesignerChildTop = (ClassRelatedDesigner)m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner[0];
                        ComponentGraphicsDesignView.DrawLine(graphics, IsSelected || ContentDesignerProperty.IsOutDesignerArriving ? 2 : 0, (classrelateddesignerChildTop is IBlockyDesigner && ((IBlockyDesigner)classrelateddesignerChildTop).ContentDesignerProperty.IsPenetrable) ? false : true, new System.Drawing.Point(Left + Width / 2, Top + m_sizeComment.Height), new System.Drawing.Point(classrelateddesignerChildTop.Left + classrelateddesignerChildTop.Width / 2, classrelateddesignerChildTop.Top));
                    }
                    else
                        ContentDesignerProperty.IsInnerDesignerOuting = ContentDesignerProperty.IsOutDesignerArriving;

                    if (OwnerComponentDesigner != null || m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count > 0)
                        ComponentGraphicsDesignView.DrawLine(graphics, ContentDesignerProperty.IsInnerDesignerOuting ? 2 : 0, false, new System.Drawing.Point(Left + Width / 2, m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count == 0 ? Top + m_sizeComment.Height : ((ClassRelatedDesigner)m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner[m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count - 1]).Rectangle.Bottom), new System.Drawing.Point(Left + Width / 2, Rectangle.Bottom));

                    System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                    WorkbenchFramework.InstanceWorkbenchFramework.Font = m_fontPriority;
                    DrawString(graphics, RuleBlock.Priority.ToString(), Brushes.Blue, Rectangle.Right - m_sizePriority.Width, Top);
                    WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
                }
                else if (IsStatementVisible)
                {
                    ContentDesignerProperty.IsInnerDesignerOuting = m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count == 0 && ContentDesignerProperty.IsOutDesignerArriving;

                    graphics.DrawImage(UnexpandedBlockBitmap, Left + (Width - Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Width, UnexpandedBlockBitmap.Width)) / 2, Top + m_sizeComment.Height);

                    if (IsChildParsedSyntaxError || StatementSyntaxError != null)
                        graphics.DrawRectangle(WarningPen, Left, Top, Width - 1, Height - 1);

                    graphics.DrawImage(ComponentGraphicsDesignView.UnexpandedBitmap, Left, Top + m_sizeComment.Height);
                    System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                    WorkbenchFramework.InstanceWorkbenchFramework.Font = m_fontPriority;
                    DrawString(graphics, RuleBlock.Priority.ToString(), Brushes.Blue, Rectangle.Right - m_sizePriority.Width, Top);
                    WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
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
            return m_admlclassdesignerhelper.MouseDown(objSender, mouseeventargs, true, new System.Drawing.Point(Left, Top + m_sizeComment.Height));
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
