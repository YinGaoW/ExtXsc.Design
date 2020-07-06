using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;
using XtremeCommandBars;
using XtremePropertyGrid;
using Eap;
namespace ExtEap
{
    internal class BlockDeclarationListDesigner : DeclarationListDesigner
    {
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;

        public BlockDeclarationListDesigner(ComponentDesignDocumentView componentdesigndocumentview, DeclarationList declarationlist) : base(componentdesigndocumentview, declarationlist)
        {
            OnConstruct();
        }
        public BlockDeclarationListDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, DeclarationList declarationlist) : base(componentdesigndocumentview, componentdesignspace, declarationlist)
        {
            OnConstruct();
        }

        private void OnConstruct()
        {
            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnConstruct();
        }

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            if (UnnamedComponentDesignerCount == 0)
            {
                System.Drawing.Size size = MeasureString(graphics, Component.GetType().Name);
                Size = size;
            }
            else if (IsExpanded)
                base.Arrange(graphics);
            
            else
            {
                System.Drawing.Size size = MeasureString(graphics, "Declaration");
                
                size.Width += ComponentGraphicsDesignView.UnexpandedBitmap.Width;
                size.Height = Math.Max(size.Height, ComponentGraphicsDesignView.UnexpandedBitmap.Height);
             
                Size = size;
            }
        }
        public override void Render(System.Drawing.Graphics graphics)
        {
            if (UnnamedComponentDesignerCount == 0)
                DrawString(graphics, Component.GetType().Name, KeywordBrush, Left + ComponentGraphicsDesignView.ExpandedBitmap.Width, Top);

            else if (IsExpanded)
            {
                base.Render(graphics);
                
                graphics.DrawImage(ComponentGraphicsDesignView.ExpandedBitmap, Rectangle.Location);
            }
            else
            {
                graphics.DrawImage(ComponentGraphicsDesignView.UnexpandedBitmap, Rectangle.Location);
                DrawString(graphics, "Declaration", KeywordBrush, Left + ComponentGraphicsDesignView.UnexpandedBitmap.Width, Top);

                if (IsChildParsedSyntaxError)
                    graphics.DrawLine(WarningPen, Left, Rectangle.Bottom - 1, Rectangle.Right - 1, Rectangle.Bottom - 1);
            }
        }

        public override bool OnPasteUpdate(string stringClipboard)
        {
            try
            {
                System.Xml.XmlDocument xmldocument = new System.Xml.XmlDocument();
                xmldocument.LoadXml(stringClipboard);

                if (xmldocument.DocumentElement.Name == typeof(DeclarationList).Name)
                    return true;
            }
            catch
            {
            }

            return base.OnPasteUpdate(stringClipboard);
        }
        public override void OnPaste(string stringClipboard, int nIndex)
        {
            try
            {
                System.Xml.XmlDocument xmldocument = new System.Xml.XmlDocument();
                xmldocument.LoadXml(stringClipboard);

                if (xmldocument.DocumentElement.Name == typeof(DeclarationList).Name)
                {
                    for (int n = 0; n < xmldocument.DocumentElement.ChildNodes.Count; n++)
                        InsertComponentDesigner(UnnamedComponentDesignerCount, xmldocument.DocumentElement.ChildNodes[n]);
                }
                else
                    base.OnPaste(stringClipboard, nIndex);
            }
            catch
            {
            }
        }
        public override void OnPaste(string strClipboard, System.Drawing.Point point)
        {
            OnPaste(strClipboard);
        }

        public override bool OnMouseDown(object objSender, MouseEventArgs mouseeventargs)
        {
            if (m_admlclassdesignerhelper.MouseDown(objSender, mouseeventargs, true))
                return true;

            return base.OnMouseDown(objSender, mouseeventargs);
        }
        public override void OnKeyDown(object objSender, KeyEventArgs keyeventargs)
        {
            if (!m_admlclassdesignerhelper.KeyDown(objSender, keyeventargs, true))
                base.OnKeyDown(objSender, keyeventargs);
        }

        public override void OnCut(ComponentDesigner componentdesigner)
        {
            base.OnCut(componentdesigner);

            if (UnnamedComponentDesignerCount > 0)
                ComponentDesignDocumentView.ComponentDesigner.SetSelectedComponentDesigner(this);

            else if (OwnerComponentDesigner != null)
                ComponentDesignDocumentView.ComponentDesigner.SetSelectedComponentDesigner(OwnerComponentDesigner);

            ComponentDesignDocumentView.Refresh();
        }

        public override void Navigate(KeyEventArgs keyeventargs)
        {
            if (keyeventargs.KeyCode == Keys.Up && NamedDesignerCount < DesignerCount && IsExpanded)
                ((DeclarationDesigner)GetDesigner(DesignerCount - 1)).Navigate(keyeventargs);

            else
                ComponentDesignDocumentView.ComponentDesigner.SetSelectedComponentDesigner(this);
        }

        protected override void OnUpdate(Component componentPath, Component componentUpdated)
        {
            base.OnUpdate(componentPath, componentUpdated);

            OnConstruct();
        }
    }

    public class BlockDesigner : StatementDesigner, IBlockyDesigner
    {
        public static Bitmap UnexpandedBlockBitmap = new Bitmap("..\\Image\\UnexpandedBlock.bmp");

        private int m_nInterval = 16;
        private ContentDesignerProperty m_contentdesignerproperty;
        private BlockDeclarationListDesigner m_blockdeclarationlistdesigner;
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;

        public BlockDesigner(ComponentDesignDocumentView componentdesigndocumentview, Block block) : base(componentdesigndocumentview, block)
        {
            OnConstruct();
        }
        public BlockDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, Block block) : base(componentdesigndocumentview, componentdesignspace, block)
        {
            OnConstruct();
        }

        public override bool HasStatment
        {
            get { return true; }
        }

        protected int Interval
        {
            get { return m_nInterval; }
        }
        public ContentDesignerProperty ContentDesignerProperty
        {
            get { return m_contentdesignerproperty; }
        }

        public Block Block
        {
            get { return (Block)Statement; }
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
            SetDesigner("DeclarationList", m_blockdeclarationlistdesigner = new BlockDeclarationListDesigner(ComponentDesignDocumentView, ComponentDesignSpace, (DeclarationList)GetComponentDesigner("DeclarationList").Component));
            
            m_contentdesignerproperty = new ContentDesignerProperty();
            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(null, this);

            ResetAdmlClassDesignerHelperChild();
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
            for (int nIndex = NamedDesignerCount; nIndex < DesignerCount; nIndex++)
                ((StatementDesigner)GetDesigner(nIndex)).ProvideAccessoryDescription(arraylistDescription, nType);
        }

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            m_sizeComment = System.Drawing.Size.Empty;

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

                    Size = size;

                    m_admlclassdesignerhelper.MoveBlockChild(ComponentGraphicsDesignView.ExpandedBitmap.Height + m_sizeComment.Height, Interval);
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

                    Size = size;
                }
            }
            else if (!IsStatementVisible)
            {
                ArrangeComment(graphics, 0);
                Size = m_sizeComment;
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

                Size = size;
            }
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
                }
                else if (IsStatementVisible)
                {
                    ContentDesignerProperty.IsInnerDesignerOuting = m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Count == 0 && ContentDesignerProperty.IsOutDesignerArriving;

                    graphics.DrawImage(UnexpandedBlockBitmap, Left + (Width - Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Width, UnexpandedBlockBitmap.Width)) / 2, Top + m_sizeComment.Height);

                    if (IsChildParsedSyntaxError || StatementSyntaxError != null)
                        graphics.DrawRectangle(WarningPen, Left, Top, Width - 1, Height - 1);

                    graphics.DrawImage(ComponentGraphicsDesignView.UnexpandedBitmap, Left, Top + m_sizeComment.Height);
                }
            }

            RenderComment(graphics);
        }

        public override ComponentGraphicsDesigner FindComponentGraphicsDesigner(System.Drawing.Point point)
        {
            return m_admlclassdesignerhelper.FindComponentGraphicsDesigner(point, true);
        }

        public override bool OnPasteUpdate(string stringClipboard)
        {
            try
            {
                System.Xml.XmlDocument xmldocument = new System.Xml.XmlDocument();
                xmldocument.LoadXml(stringClipboard);

                if (xmldocument.DocumentElement.Name == typeof(DeclarationList).Name)
                    return true;
            }
            catch
            {
            }

            return m_admlclassdesignerhelper.PasteUpdate(stringClipboard);
        }
        public override void OnPaste(string stringClipboard, int nIndex)
        {
            try
            {
                System.Xml.XmlDocument xmldocument = new System.Xml.XmlDocument();
                xmldocument.LoadXml(stringClipboard);

                if (xmldocument.DocumentElement.Name == typeof(DeclarationList).Name)
                {
                    for (int nindex = 0; nindex < xmldocument.DocumentElement.ChildNodes.Count; nindex++)
                        m_blockdeclarationlistdesigner.InsertComponentDesigner(m_blockdeclarationlistdesigner.UnnamedComponentDesignerCount, xmldocument.DocumentElement.ChildNodes[nindex]);
                }
                else
                    m_admlclassdesignerhelper.Paste(nIndex, stringClipboard);
            }
            catch
            {
            }
        }
        public override void OnPaste(string strClipboard, System.Drawing.Point point)
        {
            if (IsExpanded)
            {
                try
                {
                    System.Xml.XmlDocument xmldocument = new System.Xml.XmlDocument();
                    xmldocument.LoadXml(strClipboard);

                    if (xmldocument.DocumentElement.Name == typeof(DeclarationList).Name)
                        OnPaste(strClipboard);

                    else
                    {
                        if (xmldocument.DocumentElement.Name == typeof(Declaration).Name && m_blockdeclarationlistdesigner.Rectangle.Contains(point))
                            m_blockdeclarationlistdesigner.OnPaste(strClipboard, point);

                        else
                        {
                            int nIndex = 0;

                            for (nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                            {
                                if (((ClassRelatedDesigner)GetUnnamedComponentDesigner(nIndex)).Top > point.Y)
                                    break;
                            }

                            m_admlclassdesignerhelper.Paste(nIndex, strClipboard);
                        }
                    }
                }
                catch
                {
                }
            }
            else
                base.OnPaste(strClipboard, point);
        }

        public override bool OnMouseDown(object objSender, MouseEventArgs mouseeventargs)
        {
            return m_admlclassdesignerhelper.MouseDown(objSender, mouseeventargs, true, new System.Drawing.Point(Left, Top + m_sizeComment.Height));
            // if (m_admlclassdesignerhelper.MouseDown(objSender, mouseeventargs, true, new System.Drawing.Point(Left, Top + m_sizeComment.Height)))
            //     return true;

            // return base.OnMouseDown(objSender, mouseeventargs);
        }
        public override void OnKeyDown(object objSender, KeyEventArgs keyeventargs)
        {
            if (keyeventargs.KeyCode == Keys.Oemplus && IsSelected)
            {
                m_nInterval++;
                keyeventargs.Handled = true;
                ComponentDesignDocumentView.DesignControl.GetControl().Refresh();
            }
            else if (keyeventargs.KeyCode == Keys.OemMinus && IsSelected && Interval > 0)
            {
                m_nInterval--;
                keyeventargs.Handled = true;
                ComponentDesignDocumentView.DesignControl.GetControl().Refresh();
            }
            else if (keyeventargs.KeyCode == Keys.Insert && keyeventargs.Shift && !IsSelected)
            {
                for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                {
                    ClassRelatedDesigner classrelateddesigner = (ClassRelatedDesigner)GetUnnamedComponentDesigner(nIndex);

                    if (classrelateddesigner.GetSelectedComponentDesigner() != null)
                        m_admlclassdesignerhelper.ShowChildSelector(classrelateddesigner.Left, classrelateddesigner.Top, nIndex);
                }
            }
            else if (!m_admlclassdesignerhelper.KeyDown(objSender, keyeventargs, true))
                base.OnKeyDown(objSender, keyeventargs);
        }

        public override void OnCut(ComponentDesigner componentdesigner)
        {
            m_admlclassdesignerhelper.Cut(componentdesigner);
        }

        public override void Navigate(KeyEventArgs keyeventargs)
        {
            m_admlclassdesignerhelper.Navigate(keyeventargs, true, true);
        }

        private void ResetAdmlClassDesignerHelperChild()
        {
            if (m_admlclassdesignerhelper == null)
                m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);

            m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Clear();

            if (m_blockdeclarationlistdesigner.UnnamedComponentDesignerCount > 0)
                m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_blockdeclarationlistdesigner);

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
                ResetAdmlClassDesignerHelperChild();
        }
    }
}
