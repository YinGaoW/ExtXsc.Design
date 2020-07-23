using System;
using System.Collections;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using XtremePropertyGrid;
using Eap;

namespace ExtEap
{
    internal class ParameterListDesigner : ExecutableComponentDesigner
    {
        private ArrayList m_arraylistHint;
        private ArrayList m_arraylistHintSize;
        private ArrayList m_arraylistHintMethodDeclaration;
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;

        public ParameterListDesigner(ComponentDesignDocumentView componentdesigndocumentview, Component component) : base(componentdesigndocumentview, component)
        {
            OnConstruct();
        }
        public ParameterListDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, Component component) : base(componentdesigndocumentview, componentdesignspace, component)
        {
            OnConstruct();
        }

        public InvokeMethod InvokeMethod
        {
            get { return (InvokeMethod)Component.OwnerComponent; }
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
            m_arraylistHint = new ArrayList();
            m_arraylistHintSize = new ArrayList();
            m_arraylistHintMethodDeclaration = new ArrayList();

            try
            {
                Class classFactor = null;

                if (InvokeMethod.Factor == null)
                    classFactor = InvokeMethod.AdmlClass;

                else
                {
                    try
                    {
                        string strClassName = InvokeMethod.Factor.Parse(new SyntaxErrorList());

                        if (strClassName != null)
                            classFactor = InvokeMethod.ClassLibrary.GetClass(InvokeMethod.AdmlClass, strClassName);
                    }
                    catch
                    {
                    }
                }

                if (classFactor != null)
                {
                    GlobalMemberDeclarationList globalmemberdeclarationlist = classFactor.CreateGlobalMemberDeclarationList(InvokeMethod.Method.ExecutionEnvironment);

                    for (int nIndex = 0; nIndex < globalmemberdeclarationlist.MemberDeclarationCount; nIndex++)
                    {
                        MemberDeclaration memberdeclaration = globalmemberdeclarationlist.GetMemberDeclaration(nIndex);

                        if (memberdeclaration is MethodDeclaration && memberdeclaration.Name == InvokeMethod.MethodName)
                        {
                            m_arraylistHintMethodDeclaration.Add(memberdeclaration);

                            if (((MethodDeclaration)memberdeclaration).ParameterDeclarationList.DeclarationCount == UnnamedComponentDesignerCount)
                            {
                                m_arraylistHintMethodDeclaration.Clear();
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            Left = 0;
            Top = 0;
            System.Drawing.Size size = MeasureString(graphics, "(");

            for (int nIndex = 0; nIndex < UnnamedComponentDesignerCount; nIndex++)
            {
                Eap.FactorDesigner factordesigner = (Eap.FactorDesigner)GetUnnamedComponentDesigner(nIndex);
                factordesigner.Left = 0;
                factordesigner.Top = 0;
                factordesigner.Arrange(graphics);
                factordesigner.Move(size.Width, 0);

                size.Width += factordesigner.Width;
                size.Height = Math.Max(size.Height, factordesigner.Height);

                if (nIndex < DesignerCount - 1)
                    size.Width += MeasureString(graphics, ",").Width;

                try
                {
                    string strClassName = factordesigner.Factor.Parse(new SyntaxErrorList());

                    for (int n = 0; n < m_arraylistHintMethodDeclaration.Count; n++)
                    {
                        if (m_arraylistHintMethodDeclaration.Count == 1)
                            continue;

                        MethodDeclaration methoddeclaration = (MethodDeclaration)m_arraylistHintMethodDeclaration[n];

                        if (UnnamedComponentDesignerCount > methoddeclaration.ParameterDeclarationList.DeclarationCount || methoddeclaration.ParameterDeclarationList.GetDeclaration(nIndex).ClassName != strClassName)
                        {
                            m_arraylistHintMethodDeclaration.RemoveAt(n);
                            n--;
                        }
                    }
                }
                catch
                {
                }
            }

            if (m_arraylistHintMethodDeclaration.Count > 0)
            {
                System.Drawing.Size sizeHintMethodDeclaration = System.Drawing.Size.Empty;

                for (int nIndex = 0; nIndex < m_arraylistHintMethodDeclaration.Count; nIndex++)
                {
                    MethodDeclaration methoddeclaration = (MethodDeclaration)m_arraylistHintMethodDeclaration[nIndex];
                    string strHint = string.Empty;

                    for (int n = UnnamedComponentDesignerCount; n < methoddeclaration.ParameterDeclarationList.DeclarationCount; n++)
                    {
                        Declaration declaration = methoddeclaration.ParameterDeclarationList.GetDeclaration(n);
                        strHint += (declaration.ClassName + " " + declaration.Name);

                        if (n < methoddeclaration.ParameterDeclarationList.DeclarationCount - 1)
                            strHint += ",";
                    }

                    System.Drawing.Size sizeHint = MeasureString(graphics, strHint);
                    sizeHintMethodDeclaration.Width = Math.Max(sizeHintMethodDeclaration.Width, sizeHint.Width);
                    sizeHintMethodDeclaration.Height += sizeHint.Height;

                    m_arraylistHint.Add(strHint);
                    m_arraylistHintSize.Add(sizeHint);
                }

                if (UnnamedComponentDesignerCount > 0)
                    size.Width += MeasureString(graphics, ",").Width;

                size.Width += sizeHintMethodDeclaration.Width;
                size.Height = Math.Max(size.Height, sizeHintMethodDeclaration.Height);
            }

            size.Width += MeasureString(graphics, ")").Width;
            Size = size;

            for (int nIndex = NamedDesignerCount; nIndex < DesignerCount; nIndex++)
            {
                Eap.FactorDesigner factordesigner = (Eap.FactorDesigner)GetDesigner(nIndex);
                factordesigner.Move(0, (Height - factordesigner.Height) / 2);
            }
        }
        public override void Render(System.Drawing.Graphics graphics)
        {
            System.Drawing.Size size = MeasureString(graphics, "(");
            DrawString(graphics, "(", Brushes.Black, Left, Top + (Height - size.Height) / 2);
            int nLeft = Left + size.Width;

            for (int nIndex = NamedDesignerCount; nIndex < DesignerCount; nIndex++)
            {
                Eap.FactorDesigner factordesigner = (Eap.FactorDesigner)GetDesigner(nIndex);

                factordesigner.Top = Top;

                factordesigner.Render(graphics);

                size = MeasureString(graphics, ",");

                if (nIndex < DesignerCount - 1)
                    DrawString(graphics, ",", Brushes.Black, factordesigner.Rectangle.Right, Top + (Height - size.Height) / 2);

                nLeft = factordesigner.Rectangle.Right + size.Width;
            }

            if (m_arraylistHint.Count > 0)
            {
                int nTop = Top;

                for (int nIndex = 0; nIndex < m_arraylistHint.Count; nIndex++)
                {
                    DrawString(graphics, (string)m_arraylistHint[nIndex], Brushes.LightGray, nLeft, nTop);
                    nTop += ((System.Drawing.Size)m_arraylistHintSize[nIndex]).Height;
                }
            }

            size = MeasureString(graphics, ")");
            DrawString(graphics, ")", Brushes.Black, Rectangle.Right - size.Width, Top + (Height - size.Height) / 2);
        }

        public override ComponentGraphicsDesigner FindComponentGraphicsDesigner(System.Drawing.Point point)
        {
            ComponentGraphicsDesigner componentgraphicsdesigner = m_admlclassdesignerhelper.FindComponentGraphicsDesigner(point, false);

            if (componentgraphicsdesigner != null)
                return componentgraphicsdesigner;

            return base.FindComponentGraphicsDesigner(point);
        }

        public override bool OnPasteUpdate(System.String strClipboard)
        {
            return m_admlclassdesignerhelper.PasteUpdate(strClipboard);
        }
        public override void OnPaste(string strClipboard, System.Drawing.Point point)
        {
            int nInsertingIndex = 0;

            for (int nIndex = NamedDesignerCount; nIndex < DesignerCount; nIndex++)
            {
                if (((Eap.FactorDesigner)GetDesigner(nIndex)).Rectangle.Left > point.X)
                    break;

                nInsertingIndex++;
            }

            m_admlclassdesignerhelper.Paste(nInsertingIndex, strClipboard);
        }
        public override void OnPaste(System.String strClipboard)
        {
            m_admlclassdesignerhelper.Paste(UnnamedComponentDesignerCount, strClipboard);
        }

        public override void OnKeyDown(object objSender, KeyEventArgs keyeventargs)
        {
            if (!m_admlclassdesignerhelper.KeyDown(objSender, keyeventargs, false))
                base.OnKeyDown(objSender, keyeventargs);
        }

        public override void Navigate(KeyEventArgs keyeventargs)
        {
            if (keyeventargs.KeyCode == Keys.Up && NamedDesignerCount < DesignerCount)
                ((Eap.FactorDesigner)GetDesigner(DesignerCount - 1)).Navigate(keyeventargs);

            else
                IsSelected = true;
        }

        protected override void OnUpdate(Component componentPath, Component componentUpdated)
        {
            base.OnUpdate(componentPath, componentUpdated);

            if (m_admlclassdesignerhelper != null)
                m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);
        }
    }

    internal class DomainTableDesigner : ExecutableComponentDesigner
    {
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;
        private System.Drawing.Rectangle m_rectangleDomainTableTitle;
        private System.Drawing.Size m_sizeDomainTableTitle;
        private ArrayList m_arraylistCellRectangle;
        private ArrayList m_arraylistCellSize;
        private ArrayList m_arraylistDomainName;
        private bool m_bIsFactorSupportShowDomainTable = false;
        private bool m_bIsMethodSupportShowDomainTable = false;
        private string m_strSelectedDomainName;
        private string[] m_header;
        private int[] m_nColumnWidth;
        private int[] m_nRowHeight;
        private int m_nCommand;
        private int m_nColumnCount;
        public DomainTableDesigner(ComponentDesignDocumentView componentdesigndocumentview, Component component) : base(componentdesigndocumentview, component)
        {
            OnConstruct();
        }
        public DomainTableDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, Component component) : base(componentdesigndocumentview, componentdesignspace, component)
        {
            OnConstruct();
        }

        public InvokeMethod InvokeMethod
        {
            get { return (InvokeMethod)Component.OwnerComponent; }
        }
        private DomainTable DomainTable
        {
            get { return InvokeMethod.DomainTable; }
        }
        public bool IsMethodSupportShowDomainTable
        {
            get { return m_bIsMethodSupportShowDomainTable; }
            set { m_bIsMethodSupportShowDomainTable = value; }
        }
        private void OnConstruct()
        {
            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);
        }
        public override void Arrange(System.Drawing.Graphics graphics)
        {
            Class classFactor = null;

            try
            {
                if (InvokeMethod.Factor == null)
                    classFactor = InvokeMethod.AdmlClass;

                else
                {
                    try
                    {
                        string strClassName = InvokeMethod.Factor.Parse(new SyntaxErrorList());

                        if (strClassName != null)
                            classFactor = InvokeMethod.ClassLibrary.GetClass(InvokeMethod.AdmlClass, strClassName);
                    }
                    catch
                    {
                    }
                }

                if (classFactor != null)
                {
                    if (classFactor is CoreEntityDataAdmlClass || classFactor is EntityDataAdmlClass)
                        m_bIsFactorSupportShowDomainTable = true;
                }
            }
            catch
            {
            }

            if (!m_bIsFactorSupportShowDomainTable)
                return;

            if (InvokeMethod.MethodName != null && InvokeMethod.MethodName.IndexOf("Update") != -1)
            {
                m_bIsMethodSupportShowDomainTable = true;
                m_nCommand = InvokeMethod.CommandForUpdateMethod;
            }

            if (InvokeMethod.MethodName != null && InvokeMethod.MethodName.IndexOf("Select") != -1)
            {
                m_bIsMethodSupportShowDomainTable = true;
                m_nCommand = InvokeMethod.CommandForSelectMethod;
            }

            if (m_bIsMethodSupportShowDomainTable)
            {
                m_arraylistCellRectangle = new ArrayList();
                m_arraylistCellSize = new ArrayList();
                InitDomainName(classFactor);
                DomainRow[] domainrow = new DomainRow[m_arraylistDomainName.Count];

                for (int nIndex = 0; nIndex < m_arraylistDomainName.Count; nIndex++)
                {
                    DomainRow row = new DomainRow((string)m_arraylistDomainName[nIndex]);
                    domainrow[nIndex] = row;
                }

                if (m_nCommand == InvokeMethod.CommandForUpdateMethod)
                    InvokeMethod.InitDomainTable(m_nCommand, domainrow);

                if (m_nCommand == InvokeMethod.CommandForSelectMethod)
                    InvokeMethod.InitDomainTable(m_nCommand, domainrow);

                int nLeft = Left;
                int nTop = Top;

                if (m_nCommand == InvokeMethod.CommandForSelectMethod)
                {
                    if (IsExpanded)
                    {
                        System.Drawing.Size size = System.Drawing.Size.Empty;
                        m_sizeDomainTableTitle = MeasureString(graphics, DomainTable.DomainTableTitle);

                        m_sizeDomainTableTitle.Width += ComponentGraphicsDesignView.ExpandedBitmap.Width + 1;
                        m_sizeDomainTableTitle.Height = Math.Max(m_sizeDomainTableTitle.Height, ComponentGraphicsDesignView.ExpandedBitmap.Height);

                        m_header = DomainTable.DomainTableHeader.Split(',');
                        m_nColumnCount = m_header.Length;
                        m_nColumnWidth = new int[m_header.Length];
                        m_nRowHeight = new int[DomainTable.RowCount + 1];

                        System.Drawing.Size sizeHeaderDomainName = MeasureString(graphics, m_header[0]);
                        System.Drawing.Size sizeHeaderIndexDomainIsSelected = MeasureString(graphics, m_header[1]);
                        System.Drawing.Size sizeHeaderOperator = MeasureString(graphics, m_header[2]);

                        m_arraylistCellSize.Add(sizeHeaderDomainName);
                        m_arraylistCellSize.Add(sizeHeaderIndexDomainIsSelected);
                        m_arraylistCellSize.Add(sizeHeaderOperator);
                        m_nRowHeight[0] = Math.Max(Math.Max(sizeHeaderDomainName.Height, sizeHeaderIndexDomainIsSelected.Height), sizeHeaderOperator.Height);

                        for (int nIndex = 0; nIndex < DomainTable.RowCount; nIndex++)
                        {
                            DomainRow row = (DomainRow)DomainTable.TableData.GetComponent(nIndex);
                            System.Drawing.Size sizeDomainName = MeasureString(graphics, row.DomainName);
                            System.Drawing.Size sizeIndexDomainIsSelected;

                            if (row.IndexDomainIsSelected)
                                sizeIndexDomainIsSelected = MeasureString(graphics, "√");

                            else
                                sizeIndexDomainIsSelected = MeasureString(graphics, "×");

                            System.Drawing.Size sizeOperator = MeasureString(graphics, row.Operator);
                            m_arraylistCellSize.Add(sizeDomainName);
                            m_arraylistCellSize.Add(sizeIndexDomainIsSelected);
                            m_arraylistCellSize.Add(sizeOperator);

                            int i = nIndex;
                            m_nRowHeight[++i] = Math.Max(Math.Max(sizeDomainName.Height, sizeIndexDomainIsSelected.Height), sizeOperator.Height);
                        }

                        ArrayList arraylistColumnWidth = new ArrayList();

                        for (int nIndex = 0; nIndex < m_nColumnCount; nIndex++)
                            arraylistColumnWidth.Add(new ArrayList());

                        for (int nIndex = 0; nIndex < m_arraylistCellSize.Count; nIndex++)
                        {
                            if (nIndex % m_nColumnCount == 0)
                                ((ArrayList)arraylistColumnWidth[0]).Add(((System.Drawing.Size)m_arraylistCellSize[nIndex]).Width);

                            if (nIndex % m_nColumnCount == 1)
                                ((ArrayList)arraylistColumnWidth[1]).Add(((System.Drawing.Size)m_arraylistCellSize[nIndex]).Width);

                            if (nIndex % m_nColumnCount == 2)
                                ((ArrayList)arraylistColumnWidth[2]).Add(((System.Drawing.Size)m_arraylistCellSize[nIndex]).Width);
                        }

                        int nTotalWidth = 0;

                        for (int nIndex = 0; nIndex < m_nColumnWidth.Length; nIndex++)
                        {
                            m_nColumnWidth[nIndex] = Max((ArrayList)arraylistColumnWidth[nIndex]);
                            nTotalWidth += m_nColumnWidth[nIndex];
                        }

                        m_sizeDomainTableTitle.Width = nTotalWidth;
                        m_rectangleDomainTableTitle = new System.Drawing.Rectangle(nLeft, nTop, m_sizeDomainTableTitle.Width, m_sizeDomainTableTitle.Height);

                        for (int nIndex = 0; nIndex < m_nColumnWidth.Length; nIndex++)
                            size.Width += m_nColumnWidth[nIndex];

                        for (int nIndex = 0; nIndex < m_nRowHeight.Length; nIndex++)
                            size.Height += m_nRowHeight[nIndex];

                        size.Height += m_sizeDomainTableTitle.Height;
                        size.Width = Math.Max(size.Width, m_sizeDomainTableTitle.Width);
                        Size = size;
                    }

                    else
                    {
                        System.Drawing.Size size = MeasureString(graphics, DomainTable.DomainTableTitle);

                        size.Width += ComponentGraphicsDesignView.UnexpandedBitmap.Width + 2;
                        size.Height = Math.Max(size.Height, ComponentGraphicsDesignView.UnexpandedBitmap.Height);

                        m_rectangleDomainTableTitle = new System.Drawing.Rectangle(nLeft, nTop, size.Width, size.Height);
                        Size = size;
                    }
                }

                else
                {
                    if (IsExpanded)
                    {
                        System.Drawing.Size size = System.Drawing.Size.Empty;
                        m_sizeDomainTableTitle = MeasureString(graphics, DomainTable.DomainTableTitle);

                        m_sizeDomainTableTitle.Width += ComponentGraphicsDesignView.ExpandedBitmap.Width + 1;
                        m_sizeDomainTableTitle.Height = Math.Max(m_sizeDomainTableTitle.Height, ComponentGraphicsDesignView.ExpandedBitmap.Height);

                        m_header = DomainTable.DomainTableHeader.Split(',');
                        m_nColumnCount = m_header.Length;
                        m_nColumnWidth = new int[m_header.Length];
                        m_nRowHeight = new int[DomainTable.RowCount + 1];

                        System.Drawing.Size sizeHeaderDomainName = MeasureString(graphics, m_header[0]);
                        System.Drawing.Size sizeHeaderUpdateDomainIsSelected = MeasureString(graphics, m_header[1]);
                        System.Drawing.Size sizeHeaderIndexDomainIsSelected = MeasureString(graphics, m_header[2]);
                        System.Drawing.Size sizeHeaderOperator = MeasureString(graphics, m_header[3]);

                        m_arraylistCellSize.Add(sizeHeaderDomainName);
                        m_arraylistCellSize.Add(sizeHeaderUpdateDomainIsSelected);
                        m_arraylistCellSize.Add(sizeHeaderIndexDomainIsSelected);
                        m_arraylistCellSize.Add(sizeHeaderOperator);
                        m_nRowHeight[0] = Math.Max(Math.Max(sizeHeaderDomainName.Height, sizeHeaderUpdateDomainIsSelected.Height), Math.Max(sizeHeaderIndexDomainIsSelected.Height, sizeHeaderOperator.Height));

                        for (int nIndex = 0; nIndex < DomainTable.RowCount; nIndex++)
                        {
                            DomainRow row = (DomainRow)DomainTable.TableData.GetComponent(nIndex);
                            System.Drawing.Size sizeDomainName = MeasureString(graphics, row.DomainName);
                            System.Drawing.Size sizeUpdateDomainIsSelected;

                            if (row.UpdateDomainIsSelected)
                                sizeUpdateDomainIsSelected = MeasureString(graphics, "√");

                            else
                                sizeUpdateDomainIsSelected = MeasureString(graphics, "×");

                            System.Drawing.Size sizeIndexDomainIsSelected;

                            if (row.IndexDomainIsSelected)
                                sizeIndexDomainIsSelected = MeasureString(graphics, "√");

                            else
                                sizeIndexDomainIsSelected = MeasureString(graphics, "×");

                            System.Drawing.Size sizeOperator = MeasureString(graphics, row.Operator);

                            m_arraylistCellSize.Add(sizeDomainName);
                            m_arraylistCellSize.Add(sizeUpdateDomainIsSelected);
                            m_arraylistCellSize.Add(sizeIndexDomainIsSelected);
                            m_arraylistCellSize.Add(sizeOperator);

                            int i = nIndex;
                            m_nRowHeight[++i] = Math.Max(Math.Max(sizeDomainName.Height, sizeUpdateDomainIsSelected.Height), Math.Max(sizeIndexDomainIsSelected.Height, sizeOperator.Height));
                        }

                        ArrayList arraylistColumnWidth = new ArrayList();

                        for (int nIndex = 0; nIndex < m_nColumnCount; nIndex++)
                            arraylistColumnWidth.Add(new ArrayList());

                        for (int nIndex = 0; nIndex < m_arraylistCellSize.Count; nIndex++)
                        {
                            if (nIndex % m_nColumnCount == 0)
                                ((ArrayList)arraylistColumnWidth[0]).Add(((System.Drawing.Size)m_arraylistCellSize[nIndex]).Width);

                            if (nIndex % m_nColumnCount == 1)
                                ((ArrayList)arraylistColumnWidth[1]).Add(((System.Drawing.Size)m_arraylistCellSize[nIndex]).Width);

                            if (nIndex % m_nColumnCount == 2)
                                ((ArrayList)arraylistColumnWidth[2]).Add(((System.Drawing.Size)m_arraylistCellSize[nIndex]).Width);

                            if (nIndex % m_nColumnCount == 3)
                                ((ArrayList)arraylistColumnWidth[3]).Add(((System.Drawing.Size)m_arraylistCellSize[nIndex]).Width);
                        }

                        int nTotalWidth = 0;

                        for (int nIndex = 0; nIndex < m_nColumnWidth.Length; nIndex++)
                        {
                            m_nColumnWidth[nIndex] = Max((ArrayList)arraylistColumnWidth[nIndex]);
                            nTotalWidth += m_nColumnWidth[nIndex];
                        }


                        m_sizeDomainTableTitle.Width = nTotalWidth;
                        m_rectangleDomainTableTitle = new System.Drawing.Rectangle(nLeft, nTop, m_sizeDomainTableTitle.Width, m_sizeDomainTableTitle.Height);

                        for (int nIndex = 0; nIndex < m_nColumnWidth.Length; nIndex++)
                            size.Width += m_nColumnWidth[nIndex];

                        for (int nIndex = 0; nIndex < m_nRowHeight.Length; nIndex++)
                            size.Height += m_nRowHeight[nIndex];

                        size.Width = Math.Max(size.Width, m_sizeDomainTableTitle.Width);
                        size.Height += m_sizeDomainTableTitle.Height;

                        Size = size;
                    }

                    else
                    {
                        System.Drawing.Size size = MeasureString(graphics, DomainTable.DomainTableTitle);

                        size.Width += ComponentGraphicsDesignView.UnexpandedBitmap.Width + 2;
                        size.Height = Math.Max(size.Height, ComponentGraphicsDesignView.UnexpandedBitmap.Height);

                        m_rectangleDomainTableTitle = new System.Drawing.Rectangle(nLeft, nTop, size.Width, size.Height);

                        Size = size;
                    }
                }
            }
        }
        public override void Render(System.Drawing.Graphics graphics)
        {
            int nLeft = Left;
            int nTop = Top;

            if (m_bIsFactorSupportShowDomainTable && m_bIsMethodSupportShowDomainTable)
            {
                m_arraylistCellRectangle.Clear();

                if (m_nCommand == InvokeMethod.CommandForSelectMethod)
                {
                    nTop += m_rectangleDomainTableTitle.Height;

                    if (IsExpanded)
                    {
                        for (int nIndex = 0; nIndex < m_nRowHeight.Length; nIndex++)
                        {
                            if (nIndex != 0)
                            {
                                int i = nIndex;
                                nTop += m_nRowHeight[--i];
                            }

                            System.Drawing.Rectangle rectangleColumnDomainName = new System.Drawing.Rectangle(nLeft, nTop, m_nColumnWidth[0], m_nRowHeight[nIndex]);
                            System.Drawing.Rectangle rectangleColumnIndexDomainIsSelected = new System.Drawing.Rectangle(nLeft + m_nColumnWidth[0], nTop, m_nColumnWidth[1], m_nRowHeight[nIndex]);
                            System.Drawing.Rectangle rectangleColumnOperator = new System.Drawing.Rectangle(nLeft + m_nColumnWidth[0] + m_nColumnWidth[1], nTop, m_nColumnWidth[2], m_nRowHeight[nIndex]);

                            m_arraylistCellRectangle.Add(rectangleColumnDomainName);
                            m_arraylistCellRectangle.Add(rectangleColumnIndexDomainIsSelected);
                            m_arraylistCellRectangle.Add(rectangleColumnOperator);
                        }

                        m_rectangleDomainTableTitle = new System.Drawing.Rectangle(Left, Top, m_rectangleDomainTableTitle.Width, m_rectangleDomainTableTitle.Height);
                        graphics.FillRectangle(Brushes.LightGray, m_rectangleDomainTableTitle);
                        graphics.DrawRectangle(Pens.Black, m_rectangleDomainTableTitle);
                        graphics.DrawImage(ComponentGraphicsDesignView.ExpandedBitmap, Left, Top);
                        DrawString(graphics, DomainTable.DomainTableTitle, Brushes.Black, Left + ComponentGraphicsDesignView.ExpandedBitmap.Width + 1, Top);

                        DomainRow row = null;

                        for (int nIndex = 0; nIndex < m_arraylistCellRectangle.Count; nIndex++)
                        {
                            System.Drawing.Rectangle rectangle = (System.Drawing.Rectangle)m_arraylistCellRectangle[nIndex];

                            if (nIndex < m_header.Length)
                            {
                                graphics.FillRectangle(Brushes.LightGray, rectangle);
                                graphics.DrawRectangle(Pens.Black, rectangle);
                                DrawString(graphics, m_header[nIndex], Brushes.Black, rectangle.Left, rectangle.Top);
                            }

                            else
                            {

                                if (nIndex % m_nColumnCount == 0)
                                {
                                    row = (DomainRow)DomainTable.TableData.GetComponent(nIndex / m_nColumnCount - 1);
                                    graphics.FillRectangle(Brushes.LightGray, rectangle);
                                    graphics.DrawRectangle(Pens.Black, rectangle);
                                    DrawString(graphics, row.DomainName, Brushes.Black, rectangle.Left, rectangle.Top);
                                }

                                if (nIndex % m_nColumnCount == 1)
                                {
                                    graphics.DrawRectangle(Pens.Black, rectangle);

                                    if (row.IndexDomainIsSelected)
                                        DrawString(graphics, "√", Brushes.Black, rectangle.Left, rectangle.Top);

                                    else
                                        DrawString(graphics, "×", Brushes.Black, rectangle.Left, rectangle.Top);
                                }

                                if (nIndex % m_nColumnCount == 2)
                                {
                                    graphics.DrawRectangle(Pens.Black, rectangle);
                                    DrawString(graphics, row.Operator, Brushes.Black, rectangle.Left, rectangle.Top);
                                }
                            }
                        }
                    }

                    else
                    {
                        m_rectangleDomainTableTitle = new System.Drawing.Rectangle(Left, Top, m_rectangleDomainTableTitle.Width, m_rectangleDomainTableTitle.Height);
                        graphics.FillRectangle(Brushes.LightGray, m_rectangleDomainTableTitle);
                        graphics.DrawRectangle(Pens.Black, m_rectangleDomainTableTitle);
                        graphics.DrawImage(ComponentGraphicsDesignView.UnexpandedBitmap, Left, Top);
                        DrawString(graphics, DomainTable.DomainTableTitle, Brushes.Black, Left + ComponentGraphicsDesignView.UnexpandedBitmap.Width + 1, Top);
                    }
                }

                else
                {
                    if (IsExpanded)
                    {
                        nTop += m_rectangleDomainTableTitle.Height;

                        for (int nIndex = 0; nIndex < m_nRowHeight.Length; nIndex++)
                        {
                            if (nIndex != 0)
                            {
                                int i = nIndex;
                                nTop += m_nRowHeight[--i];
                            }

                            System.Drawing.Rectangle rectangleColumnDomainName = new System.Drawing.Rectangle(nLeft, nTop, m_nColumnWidth[0], m_nRowHeight[nIndex]);
                            System.Drawing.Rectangle rectangleColumnUpdateDomainIsSelected = new System.Drawing.Rectangle(nLeft + m_nColumnWidth[0], nTop, m_nColumnWidth[1], m_nRowHeight[nIndex]);
                            System.Drawing.Rectangle rectangleColumnIndexDomainIsSelected = new System.Drawing.Rectangle(nLeft + m_nColumnWidth[0] + m_nColumnWidth[1], nTop, m_nColumnWidth[2], m_nRowHeight[nIndex]);
                            System.Drawing.Rectangle rectangleColumnOperator = new System.Drawing.Rectangle(nLeft + m_nColumnWidth[0] + m_nColumnWidth[1] + m_nColumnWidth[2], nTop, m_nColumnWidth[3], m_nRowHeight[nIndex]);

                            m_arraylistCellRectangle.Add(rectangleColumnDomainName);
                            m_arraylistCellRectangle.Add(rectangleColumnUpdateDomainIsSelected);
                            m_arraylistCellRectangle.Add(rectangleColumnIndexDomainIsSelected);
                            m_arraylistCellRectangle.Add(rectangleColumnOperator);
                        }

                        m_rectangleDomainTableTitle = new System.Drawing.Rectangle(Left, Top, m_rectangleDomainTableTitle.Width, m_rectangleDomainTableTitle.Height);
                        graphics.FillRectangle(Brushes.LightGray, m_rectangleDomainTableTitle);
                        graphics.DrawRectangle(Pens.Black, m_rectangleDomainTableTitle);
                        graphics.DrawImage(ComponentGraphicsDesignView.ExpandedBitmap, Left, Top);
                        DrawString(graphics, DomainTable.DomainTableTitle, Brushes.Black, Left + ComponentGraphicsDesignView.ExpandedBitmap.Width + 1, Top);

                        DomainRow row = null;

                        for (int nIndex = 0; nIndex < m_arraylistCellRectangle.Count; nIndex++)
                        {
                            System.Drawing.Rectangle rectangle = (System.Drawing.Rectangle)m_arraylistCellRectangle[nIndex];

                            if (nIndex < m_header.Length)
                            {
                                graphics.FillRectangle(Brushes.LightGray, rectangle);
                                graphics.DrawRectangle(Pens.Black, rectangle);
                                DrawString(graphics, m_header[nIndex], Brushes.Black, rectangle.Left, rectangle.Top);
                            }

                            else
                            {

                                if (nIndex % m_nColumnCount == 0)
                                {
                                    row = (DomainRow)DomainTable.TableData.GetComponent(nIndex / m_nColumnCount - 1);
                                    graphics.FillRectangle(Brushes.LightGray, rectangle);
                                    graphics.DrawRectangle(Pens.Black, rectangle);
                                    DrawString(graphics, row.DomainName, Brushes.Black, rectangle.Left, rectangle.Top);
                                }

                                if (nIndex % m_nColumnCount == 1)
                                {
                                    graphics.DrawRectangle(Pens.Black, rectangle);

                                    if (row.UpdateDomainIsSelected)
                                        DrawString(graphics, "√", Brushes.Black, rectangle.Left, rectangle.Top);

                                    else
                                        DrawString(graphics, "×", Brushes.Black, rectangle.Left, rectangle.Top);
                                }

                                if (nIndex % m_nColumnCount == 2)
                                {
                                    graphics.DrawRectangle(Pens.Black, rectangle);

                                    if (row.IndexDomainIsSelected)
                                        DrawString(graphics, "√", Brushes.Black, rectangle.Left, rectangle.Top);

                                    else
                                        DrawString(graphics, "×", Brushes.Black, rectangle.Left, rectangle.Top);
                                }

                                if (nIndex % m_nColumnCount == 3)
                                {
                                    graphics.DrawRectangle(Pens.Black, rectangle);
                                    DrawString(graphics, row.Operator, Brushes.Black, rectangle.Left, rectangle.Top);
                                }
                            }
                        }
                    }

                    else
                    {
                        m_rectangleDomainTableTitle = new System.Drawing.Rectangle(Left, Top, m_rectangleDomainTableTitle.Width, m_rectangleDomainTableTitle.Height);
                        graphics.FillRectangle(Brushes.LightGray, m_rectangleDomainTableTitle);
                        graphics.DrawRectangle(Pens.Black, m_rectangleDomainTableTitle);
                        graphics.DrawImage(ComponentGraphicsDesignView.UnexpandedBitmap, Left, Top);
                        DrawString(graphics, DomainTable.DomainTableTitle, Brushes.Black, Left + ComponentGraphicsDesignView.UnexpandedBitmap.Width + 1, Top);
                    }
                }
            }
        }
        public override bool OnMouseDown(object objSender, MouseEventArgs mouseeventargs)
        {
            if (m_admlclassdesignerhelper.MouseDown(objSender, mouseeventargs, true))
                return true;

            bool result = base.OnMouseDown(objSender, mouseeventargs);

            if (m_bIsFactorSupportShowDomainTable && m_bIsFactorSupportShowDomainTable)
            {
                if (m_nCommand == InvokeMethod.CommandForSelectMethod)
                {
                    for (int nIndex = m_header.Length; nIndex < m_arraylistCellRectangle.Count; nIndex++)
                    {
                        System.Drawing.Rectangle rectangle = (System.Drawing.Rectangle)m_arraylistCellRectangle[nIndex];

                        if (rectangle.Contains(mouseeventargs.Location))
                        {
                            if (nIndex % m_nColumnCount == 1)
                            {
                                m_strSelectedDomainName = (string)m_arraylistDomainName[(nIndex - 1) / m_nColumnCount - 1];
                                ComponentDesignDocument.BeforeUpdate(DomainTable, "*");
                                DomainTable.Toggle(m_strSelectedDomainName, true);
                                ComponentDesignDocument.AfterUpdate(DomainTable, "*");
                                break;
                            }

                            if (nIndex % m_nColumnCount == 2)
                            {
                                int nRowIndex = (nIndex - 2) / m_nColumnCount - 1;
                                m_strSelectedDomainName = (string)m_arraylistDomainName[nRowIndex];

                                ArrayList arraylistSelectorItem = GetOperatorSelectorItemArrayList();

                                if (arraylistSelectorItem.Count > 0)
                                {
                                    Selector selector = new Selector(arraylistSelectorItem);
                                    selector.Font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                                    selector.Owner = WorkbenchFramework.InstanceWorkbenchFramework;
                                    selector.Selected += new HandleSelected(OnUpdateOperatorSelectorSelected);
                                    selector.Show(AdmlClassDesignerHelper.GetPoint(this, rectangle.Left + rectangle.Width, rectangle.Top));

                                    for (int i = 0; i < arraylistSelectorItem.Count; i++)
                                    {
                                        if (((SelectorItem)arraylistSelectorItem[i]).Text == ((DomainRow)DomainTable.TableData.GetComponent(nRowIndex)).Operator)
                                        {
                                            selector.SelectedIndex = nIndex;
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }

                    }

                }

                else
                {
                    for (int nIndex = m_header.Length; nIndex < m_arraylistCellRectangle.Count; nIndex++)
                    {
                        System.Drawing.Rectangle rectangle = (System.Drawing.Rectangle)m_arraylistCellRectangle[nIndex];

                        if (rectangle.Contains(mouseeventargs.Location))
                        {
                            if (nIndex % m_nColumnCount == 1)
                            {
                                m_strSelectedDomainName = (string)m_arraylistDomainName[(nIndex - 1) / m_nColumnCount - 1];
                                ComponentDesignDocument.BeforeUpdate(DomainTable, "*");
                                DomainTable.Toggle(m_strSelectedDomainName, false);
                                ComponentDesignDocument.AfterUpdate(DomainTable, "*");
                                break;
                            }

                            if (nIndex % m_nColumnCount == 2)
                            {
                                m_strSelectedDomainName = (string)m_arraylistDomainName[(nIndex - 2) / m_nColumnCount - 1];
                                ComponentDesignDocument.BeforeUpdate(DomainTable, "*");
                                DomainTable.Toggle(m_strSelectedDomainName, true);
                                ComponentDesignDocument.AfterUpdate(DomainTable, "*");
                                break;
                            }

                            if (nIndex % m_nColumnCount == 3)
                            {
                                int nRowIndex = (nIndex - 3) / m_nColumnCount - 1;
                                m_strSelectedDomainName = (string)m_arraylistDomainName[nRowIndex];

                                ArrayList arraylistSelectorItem = GetOperatorSelectorItemArrayList();

                                if (arraylistSelectorItem.Count > 0)
                                {
                                    Selector selector = new Selector(arraylistSelectorItem);
                                    selector.Font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                                    selector.Owner = WorkbenchFramework.InstanceWorkbenchFramework;
                                    selector.Selected += new HandleSelected(OnUpdateOperatorSelectorSelected);
                                    selector.Show(AdmlClassDesignerHelper.GetPoint(this, rectangle.Left + rectangle.Width, rectangle.Top));

                                    for (int i = 0; i < arraylistSelectorItem.Count; i++)
                                    {
                                        if (((SelectorItem)arraylistSelectorItem[i]).Text == ((DomainRow)DomainTable.TableData.GetComponent(nRowIndex)).Operator)
                                        {
                                            selector.SelectedIndex = nIndex;
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }
        public override ComponentGraphicsDesigner FindComponentGraphicsDesigner(System.Drawing.Point point)
        {
            ComponentGraphicsDesigner componentgraphicsdesigner = m_admlclassdesignerhelper.FindComponentGraphicsDesigner(point, false);

            if (componentgraphicsdesigner != null)
                return componentgraphicsdesigner;

            return base.FindComponentGraphicsDesigner(point);
        }
        public override bool OnPasteUpdate(System.String strClipboard)
        {
            return m_admlclassdesignerhelper.PasteUpdate(strClipboard);
        }
        public override void OnPaste(string strClipboard, System.Drawing.Point point)
        {
            int nInsertingIndex = 0;

            for (int nIndex = NamedDesignerCount; nIndex < DesignerCount; nIndex++)
            {
                if (((Eap.FactorDesigner)GetDesigner(nIndex)).Rectangle.Left > point.X)
                    break;

                nInsertingIndex++;
            }

            m_admlclassdesignerhelper.Paste(nInsertingIndex, strClipboard);
        }
        public override void OnPaste(System.String strClipboard)
        {
            m_admlclassdesignerhelper.Paste(UnnamedComponentDesignerCount, strClipboard);
        }

        public override void OnKeyDown(object objSender, KeyEventArgs keyeventargs)
        {
            if (!m_admlclassdesignerhelper.KeyDown(objSender, keyeventargs, false))
                base.OnKeyDown(objSender, keyeventargs);
        }

        public override void Navigate(KeyEventArgs keyeventargs)
        {
            if (keyeventargs.KeyCode == Keys.Up && NamedDesignerCount < DesignerCount)
                ((Eap.FactorDesigner)GetDesigner(DesignerCount - 1)).Navigate(keyeventargs);

            else
                IsSelected = true;
        }

        protected override void OnUpdate(Component componentPath, Component componentUpdated)
        {
            base.OnUpdate(componentPath, componentUpdated);

            if (m_admlclassdesignerhelper != null)
                m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);
        }
        private void InitDomainName(Class classFactor)
        {
            m_arraylistDomainName = new ArrayList();

            if (classFactor != null)
            {
                string[] dataFieldName = classFactor.DataFieldName;

                if (dataFieldName != null && dataFieldName.Length > 0)
                {
                    for (int nIndex = 0; nIndex < dataFieldName.Length; nIndex++)
                    {
                        if (dataFieldName[nIndex] == "TypeName" || dataFieldName[nIndex] == "ReferenceCount" || dataFieldName[nIndex] == "IsAbstract" || dataFieldName[nIndex].IndexOf('_') != -1)
                            continue;

                        m_arraylistDomainName.Add(dataFieldName[nIndex]);
                    }
                }
            }
        }
        private int Max(ArrayList arraylistColumnWidth)
        {
            if (arraylistColumnWidth == null || arraylistColumnWidth.Count == 0)
                return -1;

            int nMax = (int)arraylistColumnWidth[0];

            for (int nIndex = 1; nIndex < arraylistColumnWidth.Count; nIndex++)
            {
                if (nMax < (int)arraylistColumnWidth[nIndex])
                    nMax = (int)arraylistColumnWidth[nIndex];
            }

            return nMax;
        }
        private ArrayList GetOperatorSelectorItemArrayList()
        {
            ArrayList arraylist = new ArrayList();

            for (int nIndex = 0; nIndex < InvokeMethod.OperatorValue.Length; nIndex++)
            {
                SelectorItem selectorItem = new SelectorItem(InvokeMethod.OperatorValue[nIndex]);
                arraylist.Add(selectorItem);
            }

            return arraylist;
        }
        private void OnUpdateOperatorSelectorSelected(Selector selector, string strText)
        {
            SelectorItem selectoritem = selector.GetSelectedSelectorItem();

            DomainRow row = DomainTable.GetDomainRowByDomainName(m_strSelectedDomainName);

            if (row == null)
                return;

            if (selectoritem.Text == row.Operator)
                return;

            ComponentDesignDocument.BeforeUpdate(InvokeMethod, "*");
            DomainTable.ChangeOperator(m_strSelectedDomainName, selectoritem.Text);
            ComponentDesignDocument.AfterUpdate(InvokeMethod, "*");

        }
    }

    public class InvokeMethodDesigner : Eap.FactorDesigner, IOnPropertyGridValueChanged
    {
        private System.Drawing.Size m_sizeMethodName;
        private System.Drawing.Size m_sizeOperator;
        private Eap.FactorDesigner m_factordesigner;
        private ParameterListDesigner m_parameterlistdesigner;
        private DomainTableDesigner m_domaintabledesigner;
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;

        public InvokeMethodDesigner(ComponentDesignDocumentView componentdesigndocumentview, InvokeMethod invokemethod) : base(componentdesigndocumentview, invokemethod)
        {
            OnConstruct();
        }
        public InvokeMethodDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, InvokeMethod invokemethod) : base(componentdesigndocumentview, componentdesignspace, invokemethod)
        {
            OnConstruct();
        }

        public string MethodName
        {
            get { return string.IsNullOrEmpty(InvokeMethod.MethodName) ? "MethodName[???]" : InvokeMethod.MethodName; }
        }
        public InvokeMethod InvokeMethod
        {
            get { return (InvokeMethod)Component; }
        }

        private void OnConstruct()
        {
            SetDesigner("ParameterFactorComponentArrayList", m_parameterlistdesigner = new ParameterListDesigner(ComponentDesignDocumentView, ComponentDesignSpace, GetComponentDesigner("ParameterFactorComponentArrayList").Component));
            SetDesigner("DomainTable", m_domaintabledesigner = new DomainTableDesigner(ComponentDesignDocumentView, ComponentDesignSpace, GetComponentDesigner("DomainTable").Component));

            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);
            RefreshAdmlClassDesignerHelperChild();
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnConstruct();
        }

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            System.Drawing.Size size;
            m_sizeMethodName = MeasureString(graphics, MethodName);

            if (m_factordesigner != null)
            {
                m_factordesigner.Left = 0;
                m_factordesigner.Top = 0;
                m_factordesigner.Arrange(graphics);

                m_sizeOperator = MeasureString(graphics, ".");

                size = new System.Drawing.Size(m_factordesigner.Width + m_sizeOperator.Width + m_sizeMethodName.Width, Math.Max(m_factordesigner.Height, m_sizeMethodName.Height));
            }
            else
                size = m_sizeMethodName;

            m_parameterlistdesigner.Left = 0;
            m_parameterlistdesigner.Top = 0;
            m_parameterlistdesigner.Arrange(graphics);
            m_parameterlistdesigner.Move(size.Width, 0);

            size.Width += m_parameterlistdesigner.Width;
            size.Height = Math.Max(size.Height, m_parameterlistdesigner.Height);

            m_domaintabledesigner.Left = 0;
            m_domaintabledesigner.Top = 0;
            m_domaintabledesigner.Arrange(graphics);
            m_domaintabledesigner.Move(0, size.Height);

            size.Width = Math.Max(size.Width, m_domaintabledesigner.Width);
            size.Height += m_domaintabledesigner.Height;

            Size = size;

            if (m_factordesigner != null)
                m_factordesigner.Move(0, (Height - m_factordesigner.Height) / 2);

            m_parameterlistdesigner.Move(0, (Height - m_parameterlistdesigner.Height) / 2);
        }
        public override void Render(System.Drawing.Graphics graphics)
        {
            int nLeft = Left;
            int nTop = Top;
            if (m_factordesigner != null)
            {
                m_factordesigner.Left = nLeft;
                m_factordesigner.Top = nTop;
                m_factordesigner.Render(graphics);

                DrawString(graphics, ".", Brushes.Black, Left + m_factordesigner.Width, nTop);

                nLeft += m_factordesigner.Width + m_sizeOperator.Width;
            }

            DrawString(graphics, MethodName, Brushes.Black, nLeft, nTop);
            m_parameterlistdesigner.Left = nLeft + m_sizeMethodName.Width;
            m_parameterlistdesigner.Top = nTop;
            m_parameterlistdesigner.Render(graphics);

            m_domaintabledesigner.Left = Left;
            m_domaintabledesigner.Top = nTop + m_sizeMethodName.Height;
            m_domaintabledesigner.Render(graphics);

            if (FactorSyntaxError != null)
                graphics.DrawLine(WarningPen, Left, Rectangle.Bottom - m_domaintabledesigner.Height - 1, Rectangle.Right - 1, Rectangle.Bottom - m_domaintabledesigner.Height - 1);
        }

        public override ComponentGraphicsDesigner FindComponentGraphicsDesigner(System.Drawing.Point point)
        {
            return m_admlclassdesignerhelper.FindComponentGraphicsDesigner(point, false);
        }

        protected override void ProvidePropertyGridControl(PropertyGridItem propertygriditem)
        {
            for (int nIndex = 0; nIndex < ElementDesignerCount; nIndex++)
            {
                string strName;
                ElementDesigner elementdesigner = (ElementDesigner)GetDesigner(nIndex, out strName);

                if (strName == "MethodName" && AllowEdit)
                {
                    PropertyGridItem propertygriditemChild = propertygriditem.AddChildItem(XtremePropertyGrid.PropertyItemType.PropertyItemString, strName, InvokeMethod.MethodName);
                    propertygriditemChild.Tag = this;
                    propertygriditemChild.Flags = XtremePropertyGrid.PropertyItemFlags.ItemHasComboButton;

                    ArrayList arraylist = GetUpdateableDeclarationNameSelectorItemArrayList();

                    for (int n = 0; n < arraylist.Count; n++)
                    {
                        SelectorItem selectoritem = (SelectorItem)arraylist[n];

                        if (selectoritem.Tag is MethodDeclaration)
                            propertygriditemChild.Constraints.Add(((MemberDeclaration)selectoritem.Tag).Name, null);
                    }
                }
                else
                    elementdesigner.ProvidePropertyGridControl(propertygriditem, strName);
            }

            AdmlClassDesignerHelper.UpdatePropertyGridControl(this, propertygriditem);
        }
        public void OnPropertyGridValueChanged(PropertyGridItem propertygriditem)
        {
            ComponentDesignDocument.BeforeUpdate(InvokeMethod, "*");
            InvokeMethod.MethodName = (string)propertygriditem.Value;
            JudgeClearDomainTable(InvokeMethod.MethodName);
            ComponentDesignDocument.AfterUpdate(InvokeMethod, "*");

            m_admlclassdesignerhelper.CheckMethodDeclarationUpdate(InvokeMethod.MethodName, new UpdateDeclaration(UpdateDeclaration));
        }

        public override void OnKeyDown(object objSender, KeyEventArgs keyeventargs)
        {
            if (keyeventargs.KeyCode == Keys.Enter && IsSelected && AllowEdit)
            {
                keyeventargs.Handled = true;

                ArrayList arraylistSelectorItem = GetUpdateableDeclarationNameSelectorItemArrayList();

                if (arraylistSelectorItem.Count > 0)
                {
                    Selector selector = new Selector(arraylistSelectorItem);
                    selector.Font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                    selector.Owner = WorkbenchFramework.InstanceWorkbenchFramework;
                    selector.Selected += new HandleSelected(OnUpdateMethodNameSelectorSelected);
                    selector.Show(AdmlClassDesignerHelper.GetPoint(this, InvokeMethod.Factor == null ? Left : ((Eap.FactorDesigner)GetUnnamedComponentDesigner(0)).Rectangle.Right + 4, base.Rectangle.Top));

                    for (int nIndex = 0; nIndex < arraylistSelectorItem.Count; nIndex++)
                    {
                        if (((SelectorItem)arraylistSelectorItem[nIndex]).Text == InvokeMethod.MethodName + "()")
                        {
                            selector.SelectedIndex = nIndex;
                            break;
                        }
                    }
                }
            }
            else if (keyeventargs.KeyCode == Keys.Back && IsSelected && OwnerComponentDesigner != null && AllowEdit)
            {
                keyeventargs.Handled = true;

                ComponentDesigner componentdesignerOwner = OwnerComponentDesigner;
                int nIndex = componentdesignerOwner.GetUnnamedComponentDesignerIndex(this);

                if (nIndex >= 0)
                {
                    if (InvokeMethod.Factor != null)
                    {
                        System.Xml.XmlDocument xmldocument = new System.Xml.XmlDocument();
                        xmldocument.LoadXml("<?xml version=\"1.0\" encoding=\"gb2312\"?><" + InvokeMethod.Factor.GetType().Name + "/>");
                        InvokeMethod.Factor.Serialize(false, xmldocument.DocumentElement);

                        componentdesignerOwner.DeleteComponentDesigner(nIndex);

                        if (TryInsert(componentdesignerOwner.Component, InvokeMethod.Factor.GetType().Name) != null)
                        {
                            componentdesignerOwner.InsertComponentDesigner(nIndex, xmldocument.DocumentElement);
                            componentdesignerOwner.GetUnnamedComponentDesigner(nIndex).IsSelected = true;
                        }
                    }
                }
            }
            else if (!m_admlclassdesignerhelper.KeyDown(objSender, keyeventargs, false))
                base.OnKeyDown(objSender, keyeventargs);
        }

        public override void Navigate(KeyEventArgs keyeventargs)
        {
            m_admlclassdesignerhelper.Navigate(keyeventargs, false, false);
        }

        private void OnUpdateMethodNameSelectorSelected(Selector selector, string strText)
        {
            SelectorItem selectoritem = selector.GetSelectedSelectorItem();

            if (selectoritem.Tag is MethodDeclaration)
            {
                if (InvokeMethod.MethodName == ((MethodDeclaration)selectoritem.Tag).Name)
                    return;

                ComponentDesignDocument.BeforeUpdate(InvokeMethod, "*");
                InvokeMethod.MethodName = ((MethodDeclaration)selectoritem.Tag).Name;
                JudgeClearDomainTable(InvokeMethod.MethodName);
                ComponentDesignDocument.AfterUpdate(InvokeMethod, "*");

                m_admlclassdesignerhelper.CheckMethodDeclarationUpdate(InvokeMethod.MethodName, new UpdateDeclaration(UpdateDeclaration));
            }
            else if ((selectoritem.Tag is Declaration || selectoritem.Tag is PropertyDeclaration) && OwnerComponentDesigner != null)
            {
                System.Xml.XmlDocument xmldocumentFactor = new System.Xml.XmlDocument();

                if (InvokeMethod.Factor != null)
                {
                    xmldocumentFactor = new System.Xml.XmlDocument();
                    xmldocumentFactor.LoadXml("<?xml version=\"1.0\" encoding=\"gb2312\"?><" + InvokeMethod.Factor.GetType().Name + "/>");
                    GetUnnamedComponentDesigner(0).Component.Serialize(false, xmldocumentFactor.DocumentElement);
                }

                ComponentDesigner componentdesignerNew = null;
                System.Xml.XmlDocument xmldocumentInsert = new System.Xml.XmlDocument();
                int nIndex = OwnerComponentDesigner.GetUnnamedComponentDesignerIndex(this);

                if (nIndex < 0)
                {
                    componentdesignerNew = this;
                    componentdesignerNew.IsSelected = true;
                    DeleteComponentDesigner(0);
                    InvokeMethod.MethodName = string.Empty;
                    nIndex = 0;
                }
                else
                {
                    componentdesignerNew = OwnerComponentDesigner;
                    componentdesignerNew.DeleteComponentDesigner(nIndex);

                    if (TryInsert(componentdesignerNew.Component, typeof(Refer).Name) == null)
                        xmldocumentInsert.LoadXml("<?xml version=\"1.0\" encoding=\"gb2312\"?><" + typeof(InvokeMethod).Name + "/>");
                }

                XmlAttribute xmlattribute = xmldocumentInsert.CreateAttribute("DeclarationName");
                xmlattribute.Value = strText;
                XmlNode xmlnodeFactor = null;

                if (InvokeMethod.Factor != null)
                {
                    xmlnodeFactor = xmldocumentInsert.CreateElement(xmldocumentFactor.DocumentElement.Name + "-" + Guid.NewGuid().ToString());
                    Eap.XmlDocument.CloneXmlNode(xmlnodeFactor, xmldocumentFactor.DocumentElement);
                }

                if (xmldocumentInsert.ChildNodes.Count == 0)
                {
                    xmldocumentInsert.LoadXml("<?xml version=\"1.0\" encoding=\"gb2312\"?><" + typeof(Refer).Name + "/>");
                    xmldocumentInsert.DocumentElement.Attributes.Append(xmlattribute);

                    if (InvokeMethod.Factor != null)
                        xmldocumentInsert.DocumentElement.AppendChild(xmlnodeFactor);
                }
                else
                {
                    XmlNode xmlnode = xmldocumentInsert.CreateElement(typeof(Refer).Name + "-" + Guid.NewGuid().ToString());
                    xmlnode.Attributes.Append(xmlattribute);

                    if (InvokeMethod.Factor != null)
                        xmlnode.AppendChild(xmlnodeFactor);

                    xmldocumentInsert.DocumentElement.AppendChild(xmlnode);
                }

                componentdesignerNew.InsertComponentDesigner(nIndex, xmldocumentInsert.DocumentElement);

                if (!componentdesignerNew.IsSelected)
                    componentdesignerNew.GetUnnamedComponentDesigner(nIndex).IsSelected = true;

                PropertyGrid.Categories.Clear();
                PropertyGridItem propertygriditem = ComponentDesigner.PropertyGrid.AddCategory("All attributes");
                propertygriditem.Expanded = true;
                ProvidePropertyGridControl(propertygriditem);

                ComponentDesignDocumentView.DesignControl.GetControl().Refresh();
            }
        }

        private ArrayList GetUpdateableDeclarationNameSelectorItemArrayList()
        {
            ArrayList arraylist = new ArrayList();

            if (InvokeMethod.Factor == null)
                arraylist = m_admlclassdesignerhelper.GetInsertableDeclarationNameSelectorItem();

            else
            {
                try
                {
                    arraylist = AdmlClassDesignerHelper.GetMemberNameSelectorItem(InvokeMethod.ClassLibrary.GetClass(InvokeMethod.AdmlClass, InvokeMethod.Factor.Parse(new SyntaxErrorList())), InvokeMethod.Method == null ? null : InvokeMethod.Method.ExecutionEnvironment);
                }
                catch
                {
                }
            }

            return arraylist;
        }
        private void UpdateDeclaration(Component component)
        {
            ComponentDesignDocument.BeforeUpdate(InvokeMethod, "*");
            InvokeMethod.MethodName = ((Method)component).Name;
            ComponentDesignDocument.AfterUpdate(InvokeMethod, "*");
        }

        private void RefreshAdmlClassDesignerHelperChild()
        {
            m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Clear();

            if (NamedDesignerCount == DesignerCount)
            {
                m_factordesigner = null;
                m_admlclassdesignerhelper.CheckMethodDeclarationUpdate(InvokeMethod.MethodName, new UpdateDeclaration(UpdateDeclaration));
            }
            else
            {
                m_factordesigner = (Eap.FactorDesigner)GetDesigner(NamedDesignerCount);
                m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_factordesigner);
            }

            m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_parameterlistdesigner);
            m_admlclassdesignerhelper.VisibleChildClassRelatedDesigner.Add(m_domaintabledesigner);
        }
        protected override void OnUpdate(Component componentPath, Component componentUpdated)
        {
            base.OnUpdate(componentPath, componentUpdated);

            if (m_admlclassdesignerhelper != null)
                RefreshAdmlClassDesignerHelperChild();
        }
        private void JudgeClearDomainTable(string strMethodName)
        {
            if (strMethodName.IndexOf("Update") == -1 && strMethodName.IndexOf("Select") == -1)
            {
                m_domaintabledesigner.IsMethodSupportShowDomainTable = false;
                m_domaintabledesigner.Size = System.Drawing.Size.Empty;
                ((InvokeMethod)InvokeMethod).DomainTable.ClearDomainTable();
            }
        }
    }
}
