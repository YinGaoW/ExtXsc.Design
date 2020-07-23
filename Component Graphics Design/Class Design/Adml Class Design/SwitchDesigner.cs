using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Eap;

namespace ExtEap
{
    internal class FactorContainerDesigner : ExecutableComponentDesigner
    {
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;
        private Eap.FactorDesigner m_factordesigner;

        public FactorContainerDesigner(ComponentDesignDocumentView componentdesigndocumentview, Component component) : base(componentdesigndocumentview, component)
        {
            OnConstruct();
        }
        public FactorContainerDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, Component component) : base(componentdesigndocumentview, componentdesignspace, component)
        {
            OnConstruct();
        }

        public override SyntaxError SyntaxError
        {
            get
            {
                if (m_factordesigner != null && base.SyntaxError == null)
                    return m_factordesigner.SyntaxError;

                return base.SyntaxError;
            }
            set
            {
                base.SyntaxError = value;
            }
        }

        private void OnConstruct()
        {
            m_factordesigner = NamedDesignerCount < DesignerCount ? (Eap.FactorDesigner)GetDesigner(DesignerCount - 1) : null;
            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);
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

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            Left = 0;
            Top = 0;

            if (m_factordesigner == null)
                Size = MeasureString(graphics, "Factor[???]");

            else
            {
                m_factordesigner.Left = 0;
                m_factordesigner.Top = 0;
                m_factordesigner.Arrange(graphics);
                Size = m_factordesigner.Size;
            }
        }
        public override void Render(System.Drawing.Graphics graphics)
        {
            if (m_factordesigner == null)
                DrawString(graphics, "Factor[???]", KeywordBrush, Left, Top);

            else
                m_factordesigner.Render(graphics);

            if (SyntaxError != null || IsChildParsedSyntaxError)
                graphics.DrawLine(WarningPen, Left, Rectangle.Bottom - 1, Rectangle.Right - 1, Rectangle.Bottom - 1);
        }

        public override ComponentGraphicsDesigner FindComponentGraphicsDesigner(System.Drawing.Point point)
        {
            if (m_factordesigner == null)
                return base.FindComponentGraphicsDesigner(point);

            return m_factordesigner.FindComponentGraphicsDesigner(point);
        }

        public override bool OnPasteUpdate(System.String stringClipboard)
        {
            if (m_factordesigner == null)
                return m_admlclassdesignerhelper.PasteUpdate(stringClipboard);

            return m_factordesigner.OnPasteUpdate(stringClipboard);
        }
        public override void OnPaste(System.String stringClipboard, int nIndex)
        {
            if (m_factordesigner == null)
                m_admlclassdesignerhelper.Paste(nIndex, stringClipboard);

            else
                m_factordesigner.OnPaste(stringClipboard);
        }

        public override bool OnMouseDown(object objSender, MouseEventArgs mouseeventargs)
        {
            if (m_factordesigner == null)
                return base.OnMouseDown(objSender, mouseeventargs);

            else
                return ((Eap.FactorDesigner)GetDesigner(NamedDesignerCount)).OnMouseDown(objSender, mouseeventargs);
        }
        public override void OnKeyDown(object objSender, KeyEventArgs keyeventargs)
        {
            if (!IsSelected)
                return;

            if (!m_admlclassdesignerhelper.KeyDown(objSender, keyeventargs, false))
                base.OnKeyDown(objSender, keyeventargs);
        }

        public override void OnCut(ComponentDesigner componentdesigner)
        {
            m_admlclassdesignerhelper.Cut(componentdesigner);
        }

        public override void Navigate(KeyEventArgs keyeventargs)
        {
            if (m_factordesigner == null)
                base.Navigate(keyeventargs);

            else
            {
                IsSelected = false;

                ((Eap.FactorDesigner)GetDesigner(DesignerCount - 1)).Navigate(keyeventargs);
            }
        }

        protected override void OnUpdate(Component componentPath, Component componentUpdated)
        {
            base.OnUpdate(componentPath, componentUpdated);

            if (m_admlclassdesignerhelper != null)
                OnConstruct();
        }
    }

    public class SwitchDesigner : StatementDesigner, IBlockyDesigner
    {
        public static Bitmap UnexpandedSwitchBitmap = new Bitmap("..\\Image\\UnExpandedSwitch.bmp");

        private AdmlClassDesignerHelper m_admlclassdesignerhelper;
        private ContentDesignerProperty m_contentdesignerproperty;
        private FactorContainerDesigner m_factorcontainerdesignerValue;

        public SwitchDesigner(ComponentDesignDocumentView componentdesigndocumentview, Switch _switch) : base(componentdesigndocumentview, _switch)
        {
            OnConstruct();
        }
        public SwitchDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, Switch _switch) : base(componentdesigndocumentview, componentdesignspace, _switch)
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
        public Switch Switch
        {
            get { return (Switch)Statement; }
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
            SetDesigner("ValueFactorComponentContainer", m_factorcontainerdesignerValue = new FactorContainerDesigner(ComponentDesignDocumentView, ComponentDesignSpace, GetComponentDesigner("ValueFactorComponentContainer").Component));

            m_contentdesignerproperty = new ContentDesignerProperty();
            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(null, this);
            m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_factorcontainerdesignerValue);

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
                ((CaseBlockDesigner)GetUnnamedComponentDesigner(nIndex)).ProvideAccessoryDescription(arraylistDescription, nType);
        }

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            m_sizeComment = System.Drawing.Size.Empty;

            if (IsExpanded)
            {
                int nFontSize = AdmlClassDesignerHelper.CommentFont.Height;
                bool bValueSelected = m_factorcontainerdesignerValue.GetSelectedComponentDesigner() != null;

                m_factorcontainerdesignerValue.Left = 0;
                m_factorcontainerdesignerValue.Top = 0;
                m_factorcontainerdesignerValue.Arrange(graphics);

                System.Drawing.Size size = new System.Drawing.Size(0, 0);

                for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                {
                    CaseBlockDesigner caseblockdesigner = (CaseBlockDesigner)GetUnnamedComponentDesigner(nIndex);
                    caseblockdesigner.Left = 0;
                    caseblockdesigner.Top = 0;
                    caseblockdesigner.Arrange(graphics);

                    size.Width += caseblockdesigner.Width + 1;
                    size.Height = Math.Max(caseblockdesigner.Height, size.Height);
                }

                for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                {
                    CaseBlockDesigner caseblockdesigner = (CaseBlockDesigner)GetUnnamedComponentDesigner(nIndex);

                    if (caseblockdesigner.IsExpanded)
                        caseblockdesigner.Height = size.Height;
                }

                size.Width += 1;
                int nWidth = size.Width;

                size = new System.Drawing.Size(Math.Max(Math.Max(m_factorcontainerdesignerValue.Width, nFontSize * 3), size.Width), ComponentGraphicsDesignView.ExpandedBitmap.Height + 1 + m_factorcontainerdesignerValue.Height + nFontSize + nFontSize / 2 + 1 + size.Height + BottomInterval);
                size.Width = Math.Max(size.Width + ComponentGraphicsDesignView.ExpandedBitmap.Width, m_factorcontainerdesignerValue.Width + ComponentGraphicsDesignView.ExpandedBitmap.Width + 1);

                if (IsCommentVisible)
                {
                    ArrangeComment(graphics, size.Width);

                    size.Width = Math.Max(size.Width, m_sizeComment.Width);
                    size.Height += m_sizeComment.Height;
                }

                Size = size + new System.Drawing.Size(0, 10);// 高度多增加10个像素给CaseBlock移动

                m_factorcontainerdesignerValue.Move(Width / 2 - m_factorcontainerdesignerValue.Width / 2, ComponentGraphicsDesignView.ExpandedBitmap.Height + m_sizeComment.Height + 1);

                int nLeft = (Width - nWidth) / 2 + 1;

                for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                {
                    CaseBlockDesigner caseblockdesigner = (CaseBlockDesigner)GetUnnamedComponentDesigner(nIndex);
                    // 此处给CaseBlock多往下移动了10个像素,使得箭头不要被横线贯穿
                    caseblockdesigner.Move(nLeft, ComponentGraphicsDesignView.ExpandedBitmap.Height + m_sizeComment.Height + 1 + m_factorcontainerdesignerValue.Height + nFontSize + nFontSize / 2 + 1 + 10);
                    nLeft += caseblockdesigner.Width + 1;
                }
                /*
                if (UnnamedComponentDesignerCount % 2 == 0)
                {
                    for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                    {
                        CaseBlockDesigner caseblockdesigner = (CaseBlockDesigner)GetUnnamedComponentDesigner(nIndex);
                        caseblockdesigner.Move(nLeft, ComponentGraphicsDesignView.ExpandedBitmap.Height + m_sizeComment.Height + 1 + m_factorcontainerdesignerValue.Height + nFontSize + nFontSize / 2 + 1);
                        nLeft += caseblockdesigner.Width + 1;
                    }
                }
                else
                {
                    int nMidIndex = UnnamedComponentDesignerCount / 2;
                    CaseBlockDesigner caseBlockDesignerMid = (CaseBlockDesigner)GetUnnamedComponentDesigner(nMidIndex);
                    caseBlockDesignerMid.Move(Width / 2 - caseBlockDesignerMid.Width / 2, ComponentGraphicsDesignView.ExpandedBitmap.Height + m_sizeComment.Height + 1 + m_factorcontainerdesignerValue.Height + nFontSize + nFontSize / 2 + 1);
                    int nLeftSpace = caseBlockDesignerMid.Width / 2 + 1;
                    int nRightSpace = caseBlockDesignerMid.Width / 2 + 1 + 1;

                    for (int i = nMidIndex - 1; i >= 0; i--)
                    {
                        CaseBlockDesigner caseBlockDesigner = (CaseBlockDesigner)GetUnnamedComponentDesigner(i);
                        nLeftSpace += caseBlockDesigner.Width + 1;
                        caseBlockDesigner.Move(Width / 2 - nLeftSpace, ComponentGraphicsDesignView.ExpandedBitmap.Height + m_sizeComment.Height + 1 + m_factorcontainerdesignerValue.Height + nFontSize + nFontSize / 2 + 1);
                    }
                    for (int j = nMidIndex + 1; j < UnnamedComponentDesignerCount; j++)
                    {
                        CaseBlockDesigner caseBlockDesigner = (CaseBlockDesigner)GetUnnamedComponentDesigner(j);
                        caseBlockDesigner.Move(Width / 2 + nRightSpace, ComponentGraphicsDesignView.ExpandedBitmap.Height + m_sizeComment.Height + 1 + m_factorcontainerdesignerValue.Height + nFontSize + nFontSize / 2 + 1);
                        nRightSpace += caseBlockDesigner.Width + 1;
                    }
                }
                */
            }
            else if (!IsStatementVisible)
            {
                ArrangeComment(graphics, 0);
                Size = m_sizeComment;
            }
            else
            {
                System.Drawing.Size size = new System.Drawing.Size(Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Width, UnexpandedSwitchBitmap.Width), Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Height, UnexpandedSwitchBitmap.Height));

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
            ContentDesignerProperty.IsPenetrable = IsExpanded;

            if (IsExpanded)
            {
                graphics.DrawRectangle(StatementSyntaxError != null ? WarningPen : Pens.WhiteSmoke, Left, Top, Width - 1, Height - 1);
                graphics.DrawImage(ComponentGraphicsDesignView.ExpandedBitmap, Left, Top + m_sizeComment.Height);
                ComponentGraphicsDesignView.DrawLine(graphics, IsSelected || ContentDesignerProperty.IsOutDesignerArriving ? 2 : 0, true, Left + Width / 2, Top, Left + Width / 2, m_factorcontainerdesignerValue.Top);

                if (!AdmlClassDesignerHelper.IsOutRenderScope(m_factorcontainerdesignerValue) || ComponentDesignExtension.ThumbnailView.IsPainting)
                    m_factorcontainerdesignerValue.Render(graphics);

                int nTop = Top + ComponentGraphicsDesignView.ExpandedBitmap.Height + 1 + m_factorcontainerdesignerValue.Height + m_sizeComment.Height;
                int nFontSize = AdmlClassDesignerHelper.CommentFont.Height;

                AdmlClassDesignerHelper.DrawConditionSymbol(graphics, ComponentGraphicsDesignView.GetPreparedPen(0), new System.Drawing.Point(Left + Width / 2, nTop + nFontSize), nFontSize / 2);

                bool bValueSelected = m_factorcontainerdesignerValue.GetSelectedComponentDesigner() != null;
                nTop += nFontSize;
                ContentDesignerProperty.IsInnerDesignerOuting = false;

                for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                {
                    CaseBlockDesigner caseblockdesigner = (CaseBlockDesigner)GetUnnamedComponentDesigner(nIndex);
                    caseblockdesigner.ContentDesignerProperty.IsOutDesignerArriving = bValueSelected;
                    caseblockdesigner.Render(graphics);

                    System.Drawing.Point pointStart;

                    if (UnnamedComponentDesignerCount % 2 == 0)
                    {
                        if (nIndex < UnnamedComponentDesignerCount / 2)
                            pointStart = new System.Drawing.Point(Left + Width / 2 - nFontSize / 2, nTop);

                        else
                            pointStart = new System.Drawing.Point(Left + Width / 2 + nFontSize / 2, nTop);
                    }
                    else
                    {
                        if (nIndex < UnnamedComponentDesignerCount / 2)
                            pointStart = new System.Drawing.Point(Left + Width / 2 - nFontSize / 2, nTop);

                        else if (nIndex == UnnamedComponentDesignerCount / 2)
                            pointStart = new System.Drawing.Point(Left + Width / 2, nTop + nFontSize / 2);

                        else
                            pointStart = new System.Drawing.Point(Left + Width / 2 + nFontSize / 2, nTop);
                    }

                    ComponentGraphicsDesignView.DrawLine
                    (
                        graphics,
                        bValueSelected ? 2 : 0,
                        caseblockdesigner.ContentDesignerProperty.IsPenetrable ? false : true,
                        new System.Drawing.Point[]
                        {
                            //new System.Drawing.Point(Left + Width/2 - nFontSize /2, nTop),
                            pointStart,
                            new System.Drawing.Point(caseblockdesigner.Left + caseblockdesigner.Width / 2, nTop),
                            new System.Drawing.Point(caseblockdesigner.Left + caseblockdesigner.Width / 2, caseblockdesigner.Top)
                        }
                    );

                    ComponentGraphicsDesignView.DrawLine
                    (
                        graphics,
                        caseblockdesigner.IsSelected || caseblockdesigner.ContentDesignerProperty.IsInnerDesignerOuting ? 2 : 0,
                        false,
                        new System.Drawing.Point[]
                        {
                            new System.Drawing.Point(caseblockdesigner.Left + caseblockdesigner.Width / 2, caseblockdesigner.Rectangle.Bottom),
                            new System.Drawing.Point(caseblockdesigner.Left + caseblockdesigner.Width / 2, Rectangle.Bottom - BottomInterval / 2 - 1 ),
                            new System.Drawing.Point(Left + Width / 2, Rectangle.Bottom - BottomInterval / 2 - 1)
                        }
                    );

                    ContentDesignerProperty.IsInnerDesignerOuting |= caseblockdesigner.IsSelected || caseblockdesigner.ContentDesignerProperty.IsInnerDesignerOuting;
                }

                ComponentGraphicsDesignView.DrawLine(graphics, ContentDesignerProperty.IsInnerDesignerOuting ? 2 : 0, false, Left + Width / 2, Rectangle.Bottom - BottomInterval / 2 - 1, Left + Width / 2, Rectangle.Bottom);
            }
            else if (IsStatementVisible)
            {
                ContentDesignerProperty.IsOutDesignerArriving = false;

                graphics.DrawRectangle(IsChildParsedSyntaxError || StatementSyntaxError != null ? WarningPen : Pens.WhiteSmoke, Left, Top, Width - 1, Height - 1);
                graphics.DrawImage(UnexpandedSwitchBitmap, Left + (Width - Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Width, UnexpandedSwitchBitmap.Width)) / 2, Top + m_sizeComment.Height);
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

        public ContentDesignerProperty ContentDesignerProperty
        {
            get { return m_contentdesignerproperty; }
        }

        protected override void OnUpdate(Component componentPath, Component componentUpdated)
        {
            base.OnUpdate(componentPath, componentUpdated);

            if (m_admlclassdesignerhelper != null)
            {
                m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Clear();
                m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_factorcontainerdesignerValue);

                for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                    m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(GetUnnamedComponentDesigner(nIndex));
            }
        }

        private void RenderPlanTwo(System.Drawing.Graphics graphics)
        {
            ContentDesignerProperty.IsPenetrable = IsExpanded;

            if (IsExpanded)
            {
                graphics.DrawRectangle(StatementSyntaxError != null ? WarningPen : Pens.WhiteSmoke, Left, Top, Width - 1, Height - 1);
                graphics.DrawImage(ComponentGraphicsDesignView.ExpandedBitmap, Left, Top + m_sizeComment.Height);
                ComponentGraphicsDesignView.DrawLine(graphics, IsSelected || ContentDesignerProperty.IsOutDesignerArriving ? 2 : 0, true, Left + Width / 2, Top, Left + Width / 2, m_factorcontainerdesignerValue.Top);

                if (!AdmlClassDesignerHelper.IsOutRenderScope(m_factorcontainerdesignerValue) || ComponentDesignExtension.ThumbnailView.IsPainting)
                    m_factorcontainerdesignerValue.Render(graphics);

                int nTop = Top + ComponentGraphicsDesignView.ExpandedBitmap.Height + 1 + m_factorcontainerdesignerValue.Height + m_sizeComment.Height;
                int nFontSize = AdmlClassDesignerHelper.CommentFont.Height;

                AdmlClassDesignerHelper.DrawConditionSymbol(graphics, ComponentGraphicsDesignView.GetPreparedPen(0), new System.Drawing.Point(Left + Width / 2, nTop + nFontSize), nFontSize / 2);

                bool bValueSelected = m_factorcontainerdesignerValue.GetSelectedComponentDesigner() != null;
                nTop += nFontSize;
                ContentDesignerProperty.IsInnerDesignerOuting = false;

                for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
                {
                    CaseBlockDesigner caseblockdesigner = (CaseBlockDesigner)GetUnnamedComponentDesigner(nIndex);
                    caseblockdesigner.ContentDesignerProperty.IsOutDesignerArriving = bValueSelected;
                    caseblockdesigner.Render(graphics);

                    System.Drawing.Point pointStart;

                    if (UnnamedComponentDesignerCount % 2 == 0)
                    {
                        if (nIndex < UnnamedComponentDesignerCount / 2)
                            pointStart = new System.Drawing.Point(Left + Width / 2 - nFontSize / 2, nTop);

                        else
                            pointStart = new System.Drawing.Point(Left + Width / 2 + nFontSize / 2, nTop);
                    }
                    else
                    {
                        if (nIndex < UnnamedComponentDesignerCount / 2)
                            pointStart = new System.Drawing.Point(Left + Width / 2 - nFontSize / 2, nTop);

                        else if (nIndex == UnnamedComponentDesignerCount / 2)
                            pointStart = new System.Drawing.Point(Left + Width / 2, nTop + nFontSize / 2);

                        else
                            pointStart = new System.Drawing.Point(Left + Width / 2 + nFontSize / 2, nTop);
                    }

                    ComponentGraphicsDesignView.DrawLine
                    (
                        graphics,
                        bValueSelected ? 2 : 0,
                        caseblockdesigner.ContentDesignerProperty.IsPenetrable ? false : true,
                        new System.Drawing.Point[]
                        {
                            //new System.Drawing.Point(Left + Width/2 - nFontSize /2, nTop),
                            pointStart,
                            new System.Drawing.Point(caseblockdesigner.Left + caseblockdesigner.Width / 2, nTop),
                            new System.Drawing.Point(caseblockdesigner.Left + caseblockdesigner.Width / 2, caseblockdesigner.Top)
                        }
                    );

                    ComponentGraphicsDesignView.DrawLine
                    (
                        graphics,
                        caseblockdesigner.IsSelected || caseblockdesigner.ContentDesignerProperty.IsInnerDesignerOuting ? 2 : 0,
                        false,
                        new System.Drawing.Point[]
                        {
                            new System.Drawing.Point(caseblockdesigner.Left + caseblockdesigner.Width / 2, caseblockdesigner.Rectangle.Bottom),
                            new System.Drawing.Point(caseblockdesigner.Left + caseblockdesigner.Width / 2, Rectangle.Bottom - BottomInterval / 2 - 1 ),
                            new System.Drawing.Point(Left + Width / 2, Rectangle.Bottom - BottomInterval / 2 - 1)
                        }
                    );

                    ContentDesignerProperty.IsInnerDesignerOuting |= caseblockdesigner.IsSelected || caseblockdesigner.ContentDesignerProperty.IsInnerDesignerOuting;
                }

                ComponentGraphicsDesignView.DrawLine(graphics, ContentDesignerProperty.IsInnerDesignerOuting ? 2 : 0, false, Left + Width / 2, Rectangle.Bottom - BottomInterval / 2 - 1, Left + Width / 2, Rectangle.Bottom);
            }
            else if (IsStatementVisible)
            {
                ContentDesignerProperty.IsOutDesignerArriving = false;

                graphics.DrawRectangle(IsChildParsedSyntaxError || StatementSyntaxError != null ? WarningPen : Pens.WhiteSmoke, Left, Top, Width - 1, Height - 1);
                graphics.DrawImage(UnexpandedSwitchBitmap, Left + (Width - Math.Max(ComponentGraphicsDesignView.UnexpandedBitmap.Width, UnexpandedSwitchBitmap.Width)) / 2, Top + m_sizeComment.Height);
                graphics.DrawImage(ComponentGraphicsDesignView.UnexpandedBitmap, Left, Top + m_sizeComment.Height);
            }

            RenderComment(graphics);
        }
    }
}
