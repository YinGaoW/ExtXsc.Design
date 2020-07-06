using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using XtremePropertyGrid;
using Eap;

namespace ExtEap
{
    internal enum FontLevel
    {
        LevelOne = 1, LevelTwo = 2, LevelThree = 3, LevelFour = 4, LevelFive = 5, LevelSix = 6
       
    }
    internal class MathematicsDesignerHelper
    {
        private static readonly System.Drawing.Font FontLevelOne = CreateSpecifiedFont(FontLevel.LevelOne);
        private static readonly System.Drawing.Font FontLevelTwo = CreateSpecifiedFont(FontLevel.LevelTwo);
        private static readonly System.Drawing.Font FontLevelThree = CreateSpecifiedFont(FontLevel.LevelThree);
        private static readonly System.Drawing.Font FontLevelFour = CreateSpecifiedFont(FontLevel.LevelFour);
        private static readonly System.Drawing.Font FontLevelFive = CreateSpecifiedFont(FontLevel.LevelFive);
        private static readonly System.Drawing.Font FontLevelSix = CreateSpecifiedFont(FontLevel.LevelSix);
        private static Pen FillPen = CreateFillPen();

        private readonly FactorDesigner m_factordesigner;
        private System.Drawing.Size m_sizeComma;
        private System.Drawing.Size m_sizeBaseOfNaturalLogarithmSymbol;
        private System.Drawing.Size m_sizeSinSymbol;
        private System.Drawing.Size m_sizeArcSinSymbol;
        private System.Drawing.Size m_sizeSinhSymbol;
        private System.Drawing.Size m_sizeCosSymbol;
        private System.Drawing.Size m_sizeArcCosSymbol;
        private System.Drawing.Size m_sizeCoshSymbol;
        private System.Drawing.Size m_sizeTanSymbol;
        private System.Drawing.Size m_sizeArcTanSymbol;
        private System.Drawing.Size m_sizeTanhSymbol;
        private System.Drawing.Size m_sizeLogSymbol;
        private System.Drawing.Size m_sizeLnSymbol;
        private System.Drawing.Size m_sizeLgSymbol;
        private System.Drawing.Size m_sizeSigmaSymbol;
        private System.Drawing.Size m_sizeContinuousProductSymbol;
        private SizeF m_sizeFRootExponent;
        private int m_nPlaceHolderRectangleWidth;
        private int m_nPlaceHolderRectangleHeight;
        private int m_nLittlePlaceHolderRectangleWidth;
        private int m_nLittlePlaceHolderRectangleHeight;
        private int m_nSigmaLeftPartWidth;
        private int m_nContinuousProductLeftWidth;
        public MathematicsDesignerHelper(FactorDesigner factordesigner)
        {
            m_factordesigner = factordesigner;
        }

        public System.Drawing.Size Arrange(System.Drawing.Graphics graphics, string strMethodName)
        {
            System.Drawing.Size size = System.Drawing.Size.Empty;

            switch (strMethodName)
            {
                case Mathematics.MethodNameAbs:
                    {
                        size.Width += 4;
                        size.Height = 16;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameCeil:
                    {
                        size.Width += 6;
                        size.Height = 16;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameFloor:
                    {
                        size.Width += 6;
                        size.Height = 16;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameSin:
                    {
                        m_sizeSinSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "sin");
                        size.Width += m_sizeSinSymbol.Width;
                        size.Height = m_sizeSinSymbol.Height;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameArcSin:
                    {
                        m_sizeArcSinSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "arcsin");
                        size.Width += m_sizeArcSinSymbol.Width;
                        size.Height = m_sizeArcSinSymbol.Height;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameSinh:
                    {
                        m_sizeSinhSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "sinh");
                        size.Width += m_sizeSinhSymbol.Width;
                        size.Height = m_sizeSinhSymbol.Height;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameCos:
                    {
                        m_sizeCosSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "cos");
                        size.Width += m_sizeCosSymbol.Width;
                        size.Height = m_sizeCosSymbol.Height;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameArcCos:
                    {
                        m_sizeArcCosSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "arccos");
                        size.Width += m_sizeArcCosSymbol.Width;
                        size.Height = m_sizeArcCosSymbol.Height;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameCosh:
                    {
                        m_sizeCoshSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "cosh");
                        size.Width += m_sizeCoshSymbol.Width;
                        size.Height = m_sizeCoshSymbol.Height;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameTan:
                    {
                        m_sizeTanSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "tan");
                        size.Width += m_sizeTanSymbol.Width;
                        size.Height = m_sizeTanSymbol.Height;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameArcTan:
                    {
                        m_sizeArcTanSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "arctan");
                        size.Width += m_sizeArcTanSymbol.Width;
                        size.Height = m_sizeArcTanSymbol.Height;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameTanh:
                    {
                        m_sizeTanhSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "tanh");
                        size.Width += m_sizeTanhSymbol.Width;
                        size.Height = m_sizeTanhSymbol.Height;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameLn:
                    {
                        m_sizeLnSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "ln");
                        size.Width += m_sizeLnSymbol.Width;
                        size.Height = m_sizeLnSymbol.Height;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameLg:
                    {
                        m_sizeLgSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "lg");
                        size.Width += m_sizeLgSymbol.Width;
                        size.Height = m_sizeLgSymbol.Height;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameLog:
                    {
                        m_sizeLogSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "log");
                        size.Width += m_sizeLogSymbol.Width;
                        size.Height = m_sizeLogSymbol.Height;
                        
                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                        {
                            m_nPlaceHolderRectangleHeight = size.Height == 0 ? 16 - 4 : size.Height - 4;
                            m_nPlaceHolderRectangleWidth = m_nPlaceHolderRectangleHeight;
                            m_nLittlePlaceHolderRectangleHeight = m_nPlaceHolderRectangleHeight / 2;
                            m_nLittlePlaceHolderRectangleWidth = m_nLittlePlaceHolderRectangleHeight;
                            size.Width += m_nPlaceHolderRectangleWidth + m_nLittlePlaceHolderRectangleWidth + 3;
                            size.Height = Math.Max(size.Height, m_nPlaceHolderRectangleHeight);
                        }

                        else if (m_factordesigner.DesignerCount - m_factordesigner.NamedDesignerCount == 1)
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            factordesigner.Left = 0;
                            factordesigner.Top = 0;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                            factordesigner.Arrange(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;

                            size.Width += factordesigner.Width;
                            size.Height = Math.Max(size.Height, factordesigner.Height);

                            if ((factordesigner.Height - 2 * (size.Height / 3)) > 0)
                                size.Height += (factordesigner.Height - 2 * (size.Height / 3));

                            m_nPlaceHolderRectangleHeight = size.Height == 0 ? 16 - 4 : size.Height - 4;
                            m_nPlaceHolderRectangleWidth = m_nPlaceHolderRectangleHeight;
                            size.Width += m_nPlaceHolderRectangleWidth;
                            //size.Height = Math.Max(size.Height, m_nPlaceHolderRectangleWidth);
                        }

                        else
                        {
                            for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                            {
                                FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);

                                if (nIndex == m_factordesigner.NamedDesignerCount)
                                {
                                    System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                                    factordesigner.Left = 0;
                                    factordesigner.Top = 0;
                                    WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                                    factordesigner.Arrange(graphics);
                                    WorkbenchFramework.InstanceWorkbenchFramework.Font = font;

                                    size.Width += factordesigner.Width;
                                    size.Height = Math.Max(size.Height, factordesigner.Height);

                                    if ((factordesigner.Height - 2 * (size.Height / 3)) > 0)
                                        size.Height += (factordesigner.Height - 2 * (size.Height / 3));
                                }

                                else
                                {
                                    factordesigner.Left = 0;
                                    factordesigner.Top = 0;
                                    factordesigner.Arrange(graphics);

                                    size.Width += factordesigner.Width;
                                    size.Height = Math.Max(size.Height, factordesigner.Height);
                                }
                            }
                        }

                        int nLeft = m_sizeLogSymbol.Width;

                        for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);
                            
                            if (nIndex == m_factordesigner.NamedDesignerCount)
                                factordesigner.Move(nLeft, size.Height - factordesigner.Height);
                            
                            else
                                factordesigner.Move(nLeft, size.Height / 2 - factordesigner.Height / 2);
                            
                            nLeft += factordesigner.Width;
                        }
                    }
                    break;

                case Mathematics.MethodNameExp:
                    {
                        m_sizeBaseOfNaturalLogarithmSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "e");
                        size.Width += m_sizeBaseOfNaturalLogarithmSymbol.Width;
                        size.Height = m_sizeBaseOfNaturalLogarithmSymbol.Height;

                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                        {
                            m_nLittlePlaceHolderRectangleHeight = (size.Height - 4) / 2;
                            m_nLittlePlaceHolderRectangleWidth = m_nLittlePlaceHolderRectangleHeight;
                            size.Width += m_nLittlePlaceHolderRectangleWidth;
                            size.Height += m_nLittlePlaceHolderRectangleWidth / 2;
                        }

                        else
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            factordesigner.Left = 0;
                            factordesigner.Top = 0;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                            factordesigner.Arrange(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;

                            size.Width += factordesigner.Width;

                            if (factordesigner.Height / 2 <= size.Height)
                                size.Height += factordesigner.Height / 2;
                            
                            else
                                size.Height = factordesigner.Height;
                        }

                        int nLeft = m_sizeBaseOfNaturalLogarithmSymbol.Width;

                        for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);

                            if (factordesigner.Height == size.Height)
                                factordesigner.Move(nLeft, 0);

                            else
                                factordesigner.Move(nLeft, size.Height - m_sizeBaseOfNaturalLogarithmSymbol.Height - factordesigner.Height / 2);

                            nLeft += factordesigner.Width;
                        }
                    }
                    break;

                case Mathematics.MethodNamePow:
                    {
                        size.Width += 4;
                        size.Height = 16;
                        
                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                        {
                            m_nPlaceHolderRectangleHeight = size.Height == 0 ? 16 - 4 : size.Height - 4;
                            m_nPlaceHolderRectangleWidth = m_nPlaceHolderRectangleHeight;
                            m_nLittlePlaceHolderRectangleHeight = m_nPlaceHolderRectangleHeight / 2;
                            m_nLittlePlaceHolderRectangleWidth = m_nLittlePlaceHolderRectangleHeight;
                            size.Width += m_nPlaceHolderRectangleWidth + m_nLittlePlaceHolderRectangleWidth + 3;
                            size.Height = Math.Max(size.Height, m_nPlaceHolderRectangleHeight);
                            size.Height += m_nLittlePlaceHolderRectangleHeight / 2;
                            
                        }

                        else if (m_factordesigner.NamedDesignerCount + 1 == m_factordesigner.DesignerCount)
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            factordesigner.Left = 0;
                            factordesigner.Top = 0;
                            factordesigner.Arrange(graphics);

                            size.Width += factordesigner.Width;
                            size.Height = Math.Max(size.Height, factordesigner.Height);

                            m_nLittlePlaceHolderRectangleHeight = (size.Height - 4) / 2;
                            m_nLittlePlaceHolderRectangleWidth = m_nLittlePlaceHolderRectangleHeight;

                            size.Width += m_nLittlePlaceHolderRectangleWidth;
                            size.Height += m_nLittlePlaceHolderRectangleHeight / 2;
                        }

                        else
                        {
                            for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                            {
                                FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);

                                if (nIndex == m_factordesigner.NamedDesignerCount + 1)
                                {
                                    System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                                    factordesigner.Left = 0;
                                    factordesigner.Top = 0;
                                    WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                                    factordesigner.Arrange(graphics);
                                    WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
                                    
                                    size.Width += factordesigner.Width;
                                    //size.Height += factordesigner.Height / 2;

                                    if (factordesigner.Height / 2 <= size.Height)
                                        size.Height += factordesigner.Height / 2;

                                    else
                                        size.Height = factordesigner.Height;
                                }

                                else
                                {
                                    factordesigner.Left = 0;
                                    factordesigner.Top = 0;
                                    factordesigner.Arrange(graphics);

                                    size.Width += factordesigner.Width;
                                    size.Height = Math.Max(size.Height, factordesigner.Height);
                                }
                            }
                        }

                        int nLeft = 2;
                        int nBaseHeight = 0;

                        for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);

                            if (nIndex == m_factordesigner.NamedDesignerCount + 1)
                            {
                                if (factordesigner.Height == size.Height)
                                    factordesigner.Move(nLeft, 0);

                                else
                                    factordesigner.Move(nLeft, size.Height - nBaseHeight - factordesigner.Height / 2);
                            }

                            else
                            {
                                factordesigner.Move(nLeft, size.Height - factordesigner.Height);
                                nBaseHeight = factordesigner.Height;
                            }

                            nLeft += factordesigner.Width;
                        }
                    }
                    break;

                case Mathematics.MethodNameSqrt:
                    {
                        m_sizeFRootExponent = graphics.MeasureString("2", FontDecrementOneLevel(WorkbenchFramework.InstanceWorkbenchFramework.Font));
                        size.Width += (int)m_sizeFRootExponent.Width + (int)m_sizeFRootExponent.Width / 2;
                        size.Height = 18;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameCbrt:
                    {
                        m_sizeFRootExponent = graphics.MeasureString("3", FontDecrementOneLevel(WorkbenchFramework.InstanceWorkbenchFramework.Font));
                        size.Width += (int)m_sizeFRootExponent.Width + (int)m_sizeFRootExponent.Width / 2;
                        size.Height = 18;
                        ArrangeFactorDesigner(graphics, ref size, strMethodName);
                    }
                    break;

                case Mathematics.MethodNameAverage:
                    {
                        size.Width = 4;
                        size.Height = 16;

                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                        {
                            m_nPlaceHolderRectangleHeight = size.Height - 4;
                            m_nPlaceHolderRectangleWidth = m_nPlaceHolderRectangleHeight;
                            size.Width += m_nPlaceHolderRectangleWidth;
                            size.Height = Math.Max(size.Height, m_nPlaceHolderRectangleHeight);
                            size.Height += 4;
                        }

                        else
                        {
                            for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                            {
                                FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);
                                factordesigner.Left = 0;
                                factordesigner.Top = 0;
                                factordesigner.Arrange(graphics);
                                size.Width += factordesigner.Width;
                                size.Height = Math.Max(size.Height, factordesigner.Height);
                            }

                            size.Height += 2;
                        }

                        int nLeft = 2;

                        for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);
                            factordesigner.Move(nLeft, size.Height / 2 - factordesigner.Height / 2);
                            nLeft += factordesigner.Width;
                        }
                    }
                    break;

                case Mathematics.MethodNameSum:
                    {
                        m_sizeSigmaSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "∑");
                        size.Width += m_sizeSigmaSymbol.Width;
                        size.Height = m_sizeSigmaSymbol.Height;
                        m_nSigmaLeftPartWidth = size.Width;

                        if (m_factordesigner.DesignerCount == m_factordesigner.NamedDesignerCount)
                        {
                            m_nPlaceHolderRectangleHeight = m_sizeSigmaSymbol.Height - 4;
                            m_nPlaceHolderRectangleWidth = m_nPlaceHolderRectangleHeight;
                            m_nLittlePlaceHolderRectangleWidth = 6;
                            m_nLittlePlaceHolderRectangleHeight = 6;
                            size.Width += m_nPlaceHolderRectangleWidth;
                            size.Height += 12;
                        }

                        else if (m_factordesigner.NamedDesignerCount + 1 == m_factordesigner.DesignerCount)
                        {
                            FactorDesigner factordesignerLowerbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            factordesignerLowerbound.Left = 0;
                            factordesignerLowerbound.Top = 0;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                            factordesignerLowerbound.Arrange(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;

                            if (factordesignerLowerbound.Height > m_nLittlePlaceHolderRectangleHeight)
                                size.Height += factordesignerLowerbound.Height;

                            if (factordesignerLowerbound.Width > m_nSigmaLeftPartWidth)
                                m_nSigmaLeftPartWidth = factordesignerLowerbound.Width;

                            size.Width = m_nSigmaLeftPartWidth + m_nPlaceHolderRectangleWidth;
                            size.Height += 6;
                        }

                        else if (m_factordesigner.NamedDesignerCount + 2 == m_factordesigner.DesignerCount)
                        {
                            FactorDesigner factordesignerLowerbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            FactorDesigner factordesignerUpperbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount + 1);
                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            factordesignerLowerbound.Left = 0;
                            factordesignerLowerbound.Top = 0;
                            factordesignerUpperbound.Left = 0;
                            factordesignerUpperbound.Top = 0;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                            factordesignerLowerbound.Arrange(graphics);
                            factordesignerUpperbound.Arrange(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;

                            if (factordesignerLowerbound.Height > m_nLittlePlaceHolderRectangleHeight)
                                size.Height += factordesignerLowerbound.Height;

                            if (factordesignerUpperbound.Height > m_nLittlePlaceHolderRectangleHeight)
                                size.Height += factordesignerUpperbound.Height;

                            if (Math.Max(factordesignerLowerbound.Width, factordesignerUpperbound.Width) > m_nSigmaLeftPartWidth)
                                m_nSigmaLeftPartWidth = Math.Max(factordesignerLowerbound.Width, factordesignerUpperbound.Width);

                            size.Width = m_nSigmaLeftPartWidth + m_nPlaceHolderRectangleWidth;
                        }

                        else
                        {
                            FactorDesigner factordesignerLowerbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            FactorDesigner factordesignerUpperbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount + 1);
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount + 2);
                            factordesigner.Left = 0;
                            factordesigner.Top = 0;
                            factordesigner.Arrange(graphics);
                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            factordesignerLowerbound.Left = 0;
                            factordesignerLowerbound.Top = 0;
                            factordesignerUpperbound.Left = 0;
                            factordesignerUpperbound.Top = 0;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                            factordesignerLowerbound.Arrange(graphics);
                            factordesignerUpperbound.Arrange(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;

                            if (factordesignerLowerbound.Height > m_nLittlePlaceHolderRectangleHeight)
                                size.Height += factordesignerLowerbound.Height;

                            if (factordesignerUpperbound.Height > m_nLittlePlaceHolderRectangleHeight)
                                size.Height += factordesignerUpperbound.Height;
                            
                            if (factordesigner.Height / 2 - (size.Height - factordesignerLowerbound.Height - m_sizeSigmaSymbol.Height / 2) > 0)
                                size.Height += factordesigner.Height / 2 - (size.Height - factordesignerLowerbound.Height - m_sizeSigmaSymbol.Height / 2);

                            if (Math.Max(factordesignerLowerbound.Width, factordesignerUpperbound.Width) > m_nSigmaLeftPartWidth)
                                m_nSigmaLeftPartWidth = Math.Max(factordesignerLowerbound.Width, factordesignerUpperbound.Width);

                            size.Width = m_nSigmaLeftPartWidth + factordesigner.Width;
                        }

                        int nLowerboundHeight = 0;

                        for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);

                            if (nIndex == m_factordesigner.NamedDesignerCount)
                            {
                                nLowerboundHeight = factordesigner.Height;
                                factordesigner.Move(m_nSigmaLeftPartWidth / 2 - factordesigner.Width / 2, size.Height - factordesigner.Height);
                            }

                            else if (nIndex == m_factordesigner.NamedDesignerCount + 1)
                                factordesigner.Move(m_nSigmaLeftPartWidth / 2 - factordesigner.Width / 2, size.Height - nLowerboundHeight - m_sizeSigmaSymbol.Height - factordesigner.Height);

                            else
                            {
                                int nOffset = size.Height - nLowerboundHeight - m_sizeSigmaSymbol.Height / 2 - factordesigner.Height / 2;
                                factordesigner.Move(m_nSigmaLeftPartWidth, nOffset);
                            }
                        }
                    }
                    break;

                case Mathematics.MethodNameContinuousProduct:
                    {
                        m_sizeContinuousProductSymbol = ComponentGraphicsDesigner.MeasureString(graphics, "∏");
                        size.Width += m_sizeContinuousProductSymbol.Width;
                        size.Height = m_sizeContinuousProductSymbol.Height;
                        m_nContinuousProductLeftWidth = size.Width;

                        if (m_factordesigner.DesignerCount == m_factordesigner.NamedDesignerCount)
                        {
                            m_nPlaceHolderRectangleHeight = m_sizeContinuousProductSymbol.Height - 4;
                            m_nPlaceHolderRectangleWidth = m_nPlaceHolderRectangleHeight;
                            m_nLittlePlaceHolderRectangleWidth = 6;
                            m_nLittlePlaceHolderRectangleHeight = 6;
                            size.Width += m_nPlaceHolderRectangleWidth;
                            size.Height += 12;
                        }

                        else if (m_factordesigner.NamedDesignerCount + 1 == m_factordesigner.DesignerCount)
                        {
                            FactorDesigner factordesignerLowerbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            factordesignerLowerbound.Left = 0;
                            factordesignerLowerbound.Top = 0;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                            factordesignerLowerbound.Arrange(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;

                            if (factordesignerLowerbound.Height > m_nLittlePlaceHolderRectangleHeight)
                                size.Height += factordesignerLowerbound.Height;

                            if (factordesignerLowerbound.Width > m_nContinuousProductLeftWidth)
                                m_nContinuousProductLeftWidth = factordesignerLowerbound.Width;

                            size.Width = m_nContinuousProductLeftWidth + m_nPlaceHolderRectangleWidth;
                            size.Height += 6;
                        }

                        else if (m_factordesigner.NamedDesignerCount + 2 == m_factordesigner.DesignerCount)
                        {
                            FactorDesigner factordesignerLowerbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            FactorDesigner factordesignerUpperbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount + 1);
                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            factordesignerLowerbound.Left = 0;
                            factordesignerLowerbound.Top = 0;
                            factordesignerUpperbound.Left = 0;
                            factordesignerUpperbound.Top = 0;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                            factordesignerLowerbound.Arrange(graphics);
                            factordesignerUpperbound.Arrange(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;

                            if (factordesignerLowerbound.Height > m_nLittlePlaceHolderRectangleHeight)
                                size.Height += factordesignerLowerbound.Height;

                            if (factordesignerUpperbound.Height > m_nLittlePlaceHolderRectangleHeight)
                                size.Height += factordesignerUpperbound.Height;

                            if (Math.Max(factordesignerLowerbound.Width, factordesignerUpperbound.Width) > m_nContinuousProductLeftWidth)
                                m_nContinuousProductLeftWidth = Math.Max(factordesignerLowerbound.Width, factordesignerUpperbound.Width);

                            size.Width = m_nContinuousProductLeftWidth + m_nPlaceHolderRectangleWidth;
                        }

                        else
                        {
                            FactorDesigner factordesignerLowerbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            FactorDesigner factordesignerUpperbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount + 1);
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount + 2);
                            factordesigner.Left = 0;
                            factordesigner.Top = 0;
                            factordesigner.Arrange(graphics);
                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            factordesignerLowerbound.Left = 0;
                            factordesignerLowerbound.Top = 0;
                            factordesignerUpperbound.Left = 0;
                            factordesignerUpperbound.Top = 0;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                            factordesignerLowerbound.Arrange(graphics);
                            factordesignerUpperbound.Arrange(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;

                            if (factordesignerLowerbound.Height > m_nLittlePlaceHolderRectangleHeight)
                                size.Height += factordesignerLowerbound.Height;

                            if (factordesignerUpperbound.Height > m_nLittlePlaceHolderRectangleHeight)
                                size.Height += factordesignerUpperbound.Height;
                            
                            if (factordesigner.Height / 2 - (size.Height - factordesignerLowerbound.Height - m_sizeContinuousProductSymbol.Height / 2) > 0)
                                size.Height += factordesigner.Height / 2 - (size.Height - factordesignerLowerbound.Height - m_sizeContinuousProductSymbol.Height / 2);

                            if (Math.Max(factordesignerLowerbound.Width, factordesignerUpperbound.Width) > m_nContinuousProductLeftWidth)
                                m_nContinuousProductLeftWidth = Math.Max(factordesignerLowerbound.Width, factordesignerUpperbound.Width);

                            size.Width = m_nContinuousProductLeftWidth + factordesigner.Width;
                        }

                        int nLowerboundHeight = 0;

                        for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);

                            if (nIndex == m_factordesigner.NamedDesignerCount)
                            {
                                nLowerboundHeight = factordesigner.Height;
                                factordesigner.Move(m_nContinuousProductLeftWidth / 2 - factordesigner.Width / 2, size.Height - factordesigner.Height);
                            }

                            else if (nIndex == m_factordesigner.NamedDesignerCount + 1)
                                factordesigner.Move(m_nContinuousProductLeftWidth / 2 - factordesigner.Width / 2, size.Height - nLowerboundHeight - m_sizeContinuousProductSymbol.Height - factordesigner.Height);

                            else
                            {
                                int nOffset = size.Height - nLowerboundHeight - m_sizeContinuousProductSymbol.Height / 2 - factordesigner.Height / 2;
                                factordesigner.Move(m_nContinuousProductLeftWidth, nOffset);
                            }
                        }
                    }
                    break;
            }

            return size;
        }
        public void Render(System.Drawing.Graphics graphics, string strMethodName, FactorSyntaxError factorsyntaxerror)
        {
            switch (strMethodName)
            {
                case Mathematics.MethodNameAbs:
                    {
                        graphics.DrawLine(Pens.Black, m_factordesigner.Left - 2, m_factordesigner.Top, m_factordesigner.Left - 2, m_factordesigner.Rectangle.Bottom);

                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + 1, m_factordesigner.Top + m_factordesigner.Height / 2 - m_nPlaceHolderRectangleHeight / 2);
                        
                        else
                        {
                            for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                            {
                                FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);
                                factordesigner.Render(graphics);
                                
                                if (nIndex < m_factordesigner.DesignerCount - 1)
                                    ComponentGraphicsDesigner.DrawString(graphics, ",", Brushes.Black, factordesigner.Rectangle.Right, m_factordesigner.Top + (m_factordesigner.Height - m_sizeComma.Height) / 2);
                            }
                        }

                        graphics.DrawLine(Pens.Black, m_factordesigner.Rectangle.Right + 1, m_factordesigner.Top, m_factordesigner.Rectangle.Right + 1, m_factordesigner.Rectangle.Bottom);
                        DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    }
                    break;

                case Mathematics.MethodNameCeil:
                    {
                        graphics.DrawLine(Pens.Black, m_factordesigner.Left, m_factordesigner.Top, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom);
                        graphics.DrawLine(Pens.Black, m_factordesigner.Left, m_factordesigner.Top, m_factordesigner.Left + 3, m_factordesigner.Top);

                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + 3, m_factordesigner.Top + 2);
                         
                        else
                        {
                            for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                            {
                                FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);
                                factordesigner.Render(graphics);

                                if (nIndex < m_factordesigner.DesignerCount - 1)
                                    ComponentGraphicsDesigner.DrawString(graphics, ",", Brushes.Black, factordesigner.Rectangle.Right, m_factordesigner.Top + (m_factordesigner.Height - m_sizeComma.Height) / 2);
                            }
                        }

                        graphics.DrawLine(Pens.Black, m_factordesigner.Rectangle.Right - 1, m_factordesigner.Top, m_factordesigner.Rectangle.Right -1, m_factordesigner.Rectangle.Bottom); ;
                        graphics.DrawLine(Pens.Black, m_factordesigner.Rectangle.Right - 1, m_factordesigner.Top, m_factordesigner.Rectangle.Right - 1 -3, m_factordesigner.Top);
                        DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    }
                    break;

                case Mathematics.MethodNameFloor:
                    {
                        graphics.DrawLine(Pens.Black, m_factordesigner.Left, m_factordesigner.Top, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom);
                        graphics.DrawLine(Pens.Black, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Left + 3, m_factordesigner.Rectangle.Bottom);

                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + 3, m_factordesigner.Top + 2);

                        else
                        {
                            for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                            {
                                FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);
                                factordesigner.Render(graphics);

                                if (nIndex < m_factordesigner.DesignerCount - 1)
                                    ComponentGraphicsDesigner.DrawString(graphics, ",", Brushes.Black, factordesigner.Rectangle.Right, m_factordesigner.Top + (m_factordesigner.Height - m_sizeComma.Height) / 2);
                            }
                        }

                        graphics.DrawLine(Pens.Black, m_factordesigner.Rectangle.Right - 1, m_factordesigner.Top, m_factordesigner.Rectangle.Right - 1, m_factordesigner.Rectangle.Bottom);
                        graphics.DrawLine(Pens.Black, m_factordesigner.Rectangle.Right - 1, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right - 1 - 3, m_factordesigner.Rectangle.Bottom);
                        DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom + 2, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom + 2);
                    }
                    break;

                case Mathematics.MethodNameSin:
                    RenderSpecificMathematicsSymbol(graphics, "sin", m_sizeSinSymbol);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameArcSin:
                    RenderSpecificMathematicsSymbol(graphics, "arcsin", m_sizeArcSinSymbol);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameSinh:
                    RenderSpecificMathematicsSymbol(graphics, "sinh", m_sizeSinhSymbol);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameCos:
                    RenderSpecificMathematicsSymbol(graphics, "cos", m_sizeCosSymbol);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameArcCos:
                    RenderSpecificMathematicsSymbol(graphics, "arccos", m_sizeArcCosSymbol);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameCosh:
                    RenderSpecificMathematicsSymbol(graphics, "cosh", m_sizeCoshSymbol);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameTan:
                    RenderSpecificMathematicsSymbol(graphics, "tan", m_sizeTanSymbol);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameArcTan:
                    RenderSpecificMathematicsSymbol(graphics, "arctan", m_sizeArcTanSymbol);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameTanh:
                    RenderSpecificMathematicsSymbol(graphics, "tanh", m_sizeTanhSymbol);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameLn:
                    RenderSpecificMathematicsSymbol(graphics, "ln", m_sizeLnSymbol);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameLg:
                    RenderSpecificMathematicsSymbol(graphics, "lg", m_sizeLgSymbol);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameLog:
                    {
                        ComponentGraphicsDesigner.DrawString(graphics, "log", Brushes.Black, m_factordesigner.Left, m_factordesigner.Top + (m_factordesigner.Height - m_sizeLogSymbol.Height) / 2);

                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                        {
                            DrawLittlePlaceHolderRectangle(graphics, m_factordesigner.Left + m_sizeLogSymbol.Width + 1, m_factordesigner.Rectangle.Bottom - 1 - m_nLittlePlaceHolderRectangleHeight);
                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + m_sizeLogSymbol.Width + 1 + m_nLittlePlaceHolderRectangleWidth + 2, m_factordesigner.Top + 2);
                        }

                        else if (m_factordesigner.NamedDesignerCount + 1 == m_factordesigner.DesignerCount)
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontLevelTwo;
                            factordesigner.Render(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;

                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + m_sizeLogSymbol.Width + 1 + factordesigner.Width, m_factordesigner.Top + 2);
                        }

                        else
                        {
                            for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                            {
                                FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);

                                if (nIndex == m_factordesigner.NamedDesignerCount)
                                {
                                    System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                                    WorkbenchFramework.InstanceWorkbenchFramework.Font = FontLevelTwo;
                                    factordesigner.Render(graphics);
                                    WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
                                }

                                else
                                {
                                    factordesigner.Render(graphics);
                                }
                            }
                        }

                        DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    }
                    break;

                case Mathematics.MethodNameExp:
                    {
                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                        {
                            ComponentGraphicsDesigner.DrawString(graphics, "e", Brushes.Black, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom - m_sizeBaseOfNaturalLogarithmSymbol.Height);
                            DrawLittlePlaceHolderRectangle(graphics, m_factordesigner.Left + m_sizeBaseOfNaturalLogarithmSymbol.Width, m_factordesigner.Rectangle.Bottom - m_sizeBaseOfNaturalLogarithmSymbol.Height - m_nLittlePlaceHolderRectangleHeight / 2);
                        }

                        else
                        {
                            ComponentGraphicsDesigner.DrawString(graphics, "e", Brushes.Black, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom - m_sizeBaseOfNaturalLogarithmSymbol.Height);
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            factordesigner.Render(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
                        }

                        DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    }
                    break;

                case Mathematics.MethodNamePow:
                    {
                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                        {
                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + 2, m_factordesigner.Rectangle.Bottom - m_nPlaceHolderRectangleHeight - 1);
                            DrawLittlePlaceHolderRectangle(graphics, m_factordesigner.Left + 2 + m_nPlaceHolderRectangleWidth + 2, m_factordesigner.Rectangle.Bottom - m_nPlaceHolderRectangleHeight - 1 - m_nLittlePlaceHolderRectangleHeight / 2);
                        }

                        else if (m_factordesigner.NamedDesignerCount + 1 == m_factordesigner.DesignerCount)
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            factordesigner.Render(graphics);

                            DrawLittlePlaceHolderRectangle(graphics, m_factordesigner.Left + factordesigner.Width + 2, m_factordesigner.Rectangle.Bottom - factordesigner.Height - 1 - m_nLittlePlaceHolderRectangleHeight / 2);
                        }

                        else
                        {
                            for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                            {
                                FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);

                                if (nIndex == m_factordesigner.NamedDesignerCount + 1)
                                {
                                    System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                                    WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                                    factordesigner.Render(graphics);
                                    WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
                                }

                                else
                                {
                                    factordesigner.Render(graphics);
                                }
                            }
                        }

                        DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    }
                    break;

                case Mathematics.MethodNameSqrt:
                    RenderRootMathematicsSymbol(graphics, 2);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameCbrt:
                    RenderRootMathematicsSymbol(graphics, 3);
                    DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    break;

                case Mathematics.MethodNameAverage:
                    {
                        graphics.DrawLine(Pens.Black, m_factordesigner.Left + 1, m_factordesigner.Top, m_factordesigner.Rectangle.Right - 1, m_factordesigner.Top);

                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + m_factordesigner.Width / 2 - m_nPlaceHolderRectangleWidth / 2, m_factordesigner.Top + m_factordesigner.Height / 2 - m_nPlaceHolderRectangleHeight / 2);

                        else
                        {
                            for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                            {
                                FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);
                                factordesigner.Render(graphics);
                            }
                        }

                        DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom);
                    }
                    break;

                case Mathematics.MethodNameSum:
                    {
                        //ComponentGraphicsDesigner.DrawString(graphics, "∑", Brushes.Black, m_factordesigner.Left + m_nSigmaLeftPartWidth / 2 - m_sizeSigmaSymbol.Width / 2, m_factordesigner.Top + m_factordesigner.Height / 2 - m_sizeSigmaSymbol.Height / 2);

                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                        {
                            ComponentGraphicsDesigner.DrawString(graphics, "∑", Brushes.Black, m_factordesigner.Left + m_nSigmaLeftPartWidth / 2 - m_sizeSigmaSymbol.Width / 2, m_factordesigner.Top + m_factordesigner.Height / 2 - m_sizeSigmaSymbol.Height / 2);
                            DrawLittlePlaceHolderRectangle(graphics, m_factordesigner.Left + Math.Max(m_nSigmaLeftPartWidth, m_nLittlePlaceHolderRectangleWidth) / 2 - Math.Min(m_sizeSigmaSymbol.Width, m_nLittlePlaceHolderRectangleWidth) / 2, m_factordesigner.Top);
                            DrawLittlePlaceHolderRectangle(graphics, m_factordesigner.Left + Math.Max(m_nSigmaLeftPartWidth, m_nLittlePlaceHolderRectangleWidth) / 2 - Math.Min(m_sizeSigmaSymbol.Width, m_nLittlePlaceHolderRectangleWidth) / 2, m_factordesigner.Rectangle.Bottom - m_nLittlePlaceHolderRectangleHeight);
                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + m_nSigmaLeftPartWidth, m_factordesigner.Top + m_factordesigner.Height / 2 - m_nPlaceHolderRectangleHeight / 2);
                        }

                        else if (m_factordesigner.DesignerCount - m_factordesigner.NamedDesignerCount == 1)
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                            factordesigner.Render(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
                            ComponentGraphicsDesigner.DrawString(graphics, "∑", Brushes.Black, m_factordesigner.Left + m_nSigmaLeftPartWidth / 2 - m_sizeSigmaSymbol.Width / 2, m_factordesigner.Rectangle.Bottom - factordesigner.Height - m_sizeSigmaSymbol.Height);
                            DrawLittlePlaceHolderRectangle(graphics, m_factordesigner.Left + Math.Max(m_nSigmaLeftPartWidth, m_nLittlePlaceHolderRectangleWidth) / 2 - Math.Min(m_sizeSigmaSymbol.Width, m_nLittlePlaceHolderRectangleWidth) / 2, m_factordesigner.Top + (m_factordesigner.Height - factordesigner.Height - m_sizeSigmaSymbol.Height) / 2 - m_nLittlePlaceHolderRectangleHeight / 2);
                            int nOffset = (m_factordesigner.Height - factordesigner.Height - m_sizeSigmaSymbol.Height / 2) - m_nPlaceHolderRectangleHeight / 2;
                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + m_nSigmaLeftPartWidth, m_factordesigner.Top + nOffset);
                        }

                        else if (m_factordesigner.DesignerCount - m_factordesigner.NamedDesignerCount == 2)
                        {
                            FactorDesigner factordesignerLowerbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            FactorDesigner factordesignerUpperbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount + 1);

                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                            factordesignerLowerbound.Render(graphics);
                            factordesignerUpperbound.Render(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
                            ComponentGraphicsDesigner.DrawString(graphics, "∑", Brushes.Black, m_factordesigner.Left + m_nSigmaLeftPartWidth / 2 - m_sizeSigmaSymbol.Width / 2, m_factordesigner.Rectangle.Bottom - factordesignerLowerbound.Height - m_sizeSigmaSymbol.Height);
                            int nOffset = (m_factordesigner.Height - factordesignerLowerbound.Height - m_sizeSigmaSymbol.Height / 2) - m_nPlaceHolderRectangleHeight / 2;
                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + m_nSigmaLeftPartWidth, m_factordesigner.Top + nOffset);
                        }

                        else
                        {
                            int nLowerboundHeight = 0;

                            for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                            {
                                FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);

                                if (nIndex <= m_factordesigner.NamedDesignerCount + 1)
                                {
                                    if (nIndex == m_factordesigner.NamedDesignerCount)
                                        nLowerboundHeight = factordesigner.Height;

                                    System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                                    WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                                    factordesigner.Render(graphics);
                                    WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
                                }

                                else
                                    factordesigner.Render(graphics);
                            }

                            ComponentGraphicsDesigner.DrawString(graphics, "∑", Brushes.Black, m_factordesigner.Left + m_nSigmaLeftPartWidth / 2 - m_sizeSigmaSymbol.Width / 2, m_factordesigner.Rectangle.Bottom - nLowerboundHeight - m_sizeSigmaSymbol.Height);
                        }

                        DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom + 1, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom + 1);
                    }
                    break;

                case Mathematics.MethodNameContinuousProduct:
                    {
                        if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                        {
                            ComponentGraphicsDesigner.DrawString(graphics, "∏", Brushes.Black, m_factordesigner.Left + m_nContinuousProductLeftWidth / 2 - m_sizeContinuousProductSymbol.Width / 2, m_factordesigner.Top + m_factordesigner.Height / 2 - m_sizeContinuousProductSymbol.Height / 2);
                            DrawLittlePlaceHolderRectangle(graphics, m_factordesigner.Left + Math.Max(m_nContinuousProductLeftWidth, m_nLittlePlaceHolderRectangleWidth) / 2 - Math.Min(m_sizeContinuousProductSymbol.Width, m_nLittlePlaceHolderRectangleWidth) / 2, m_factordesigner.Top);
                            DrawLittlePlaceHolderRectangle(graphics, m_factordesigner.Left + Math.Max(m_nContinuousProductLeftWidth, m_nLittlePlaceHolderRectangleWidth) / 2 - Math.Min(m_sizeContinuousProductSymbol.Width, m_nLittlePlaceHolderRectangleWidth) / 2, m_factordesigner.Rectangle.Bottom - m_nLittlePlaceHolderRectangleHeight);
                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + m_nContinuousProductLeftWidth, m_factordesigner.Top + m_factordesigner.Height / 2 - m_nPlaceHolderRectangleHeight / 2);
                        }

                        else if (m_factordesigner.DesignerCount - m_factordesigner.NamedDesignerCount == 1)
                        {
                            FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                            factordesigner.Render(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
                            ComponentGraphicsDesigner.DrawString(graphics, "∏", Brushes.Black, m_factordesigner.Left + m_nContinuousProductLeftWidth / 2 - m_sizeContinuousProductSymbol.Width / 2, m_factordesigner.Rectangle.Bottom - factordesigner.Height - m_sizeContinuousProductSymbol.Height);
                            DrawLittlePlaceHolderRectangle(graphics, m_factordesigner.Left + Math.Max(m_nContinuousProductLeftWidth, m_nLittlePlaceHolderRectangleWidth) / 2 - Math.Min(m_sizeContinuousProductSymbol.Width, m_nLittlePlaceHolderRectangleWidth) / 2, m_factordesigner.Top + (m_factordesigner.Height - factordesigner.Height - m_sizeContinuousProductSymbol.Height) / 2 - m_nLittlePlaceHolderRectangleHeight / 2);
                            int nOffset = (m_factordesigner.Height - factordesigner.Height - m_sizeContinuousProductSymbol.Height / 2) - m_nPlaceHolderRectangleHeight / 2;
                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + m_nContinuousProductLeftWidth, m_factordesigner.Top + nOffset);
                        }

                        else if (m_factordesigner.DesignerCount - m_factordesigner.NamedDesignerCount == 2)
                        {
                            FactorDesigner factordesignerLowerbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount);
                            FactorDesigner factordesignerUpperbound = (FactorDesigner)m_factordesigner.GetDesigner(m_factordesigner.NamedDesignerCount + 1);

                            System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                            factordesignerLowerbound.Render(graphics);
                            factordesignerUpperbound.Render(graphics);
                            WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
                            ComponentGraphicsDesigner.DrawString(graphics, "∏", Brushes.Black, m_factordesigner.Left + m_nContinuousProductLeftWidth / 2 - m_sizeContinuousProductSymbol.Width / 2, m_factordesigner.Rectangle.Bottom - factordesignerLowerbound.Height - m_sizeContinuousProductSymbol.Height);
                            int nOffset = (m_factordesigner.Height - factordesignerLowerbound.Height - m_sizeContinuousProductSymbol.Height / 2) - m_nPlaceHolderRectangleHeight / 2;
                            DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + m_nContinuousProductLeftWidth, m_factordesigner.Top + nOffset);
                        }

                        else
                        {
                            int nLowerboundHeight = 0;

                            for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                            {
                                FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);

                                if (nIndex <= m_factordesigner.NamedDesignerCount + 1)
                                {
                                    if (nIndex == m_factordesigner.NamedDesignerCount)
                                        nLowerboundHeight = factordesigner.Height;

                                    System.Drawing.Font font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                                    WorkbenchFramework.InstanceWorkbenchFramework.Font = FontDecrementOneLevel(font);
                                    factordesigner.Render(graphics);
                                    WorkbenchFramework.InstanceWorkbenchFramework.Font = font;
                                }

                                else
                                    factordesigner.Render(graphics);
                            }

                            ComponentGraphicsDesigner.DrawString(graphics, "∏", Brushes.Black, m_factordesigner.Left + m_nContinuousProductLeftWidth / 2 - m_sizeContinuousProductSymbol.Width / 2, m_factordesigner.Rectangle.Bottom - nLowerboundHeight - m_sizeContinuousProductSymbol.Height);
                        }

                        DrawErrorLine(graphics, factorsyntaxerror, m_factordesigner.Left, m_factordesigner.Rectangle.Bottom + 1, m_factordesigner.Rectangle.Right, m_factordesigner.Rectangle.Bottom + 1);
                    }
                    break;
            }
        }

        private void ArrangeFactorDesigner(System.Drawing.Graphics graphics, ref System.Drawing.Size size, string strMethodName)
        {
            if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
            {
                m_nPlaceHolderRectangleHeight = size.Height == 0 ? 16 - 4 : size.Height - 4;
                m_nPlaceHolderRectangleWidth = m_nPlaceHolderRectangleHeight;
                //size.Width += 4;
                size.Width += m_nPlaceHolderRectangleWidth;
                size.Height = Math.Max(size.Height, m_nPlaceHolderRectangleHeight);               
            }

            else
            {
                if (m_factordesigner.NamedDesignerCount < m_factordesigner.DesignerCount)
                    m_sizeComma = ComponentGraphicsDesigner.MeasureString(graphics, ",");

                for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                {
                    FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);
                    factordesigner.Left = 0;
                    factordesigner.Top = 0;
                    factordesigner.Arrange(graphics);

                    size.Width += factordesigner.Width;
                    size.Height = Math.Max(size.Height, factordesigner.Height);

                    if (nIndex < m_factordesigner.DesignerCount - 1)
                        size.Width += m_sizeComma.Width;
                }

                switch (strMethodName)
                {
                    case Mathematics.MethodNameAbs:
                        FactorDesignerMove(2, size);
                        break;

                    case Mathematics.MethodNameCeil:
                        FactorDesignerMove(3, size);
                        break;

                    case Mathematics.MethodNameFloor:
                        FactorDesignerMove(3, size);
                        break;

                    case Mathematics.MethodNameSin:
                        FactorDesignerMove(m_sizeSinSymbol.Width, size);
                        break;

                    case Mathematics.MethodNameArcSin:
                        FactorDesignerMove(m_sizeArcSinSymbol.Width, size);
                        break;

                    case Mathematics.MethodNameSinh:
                        FactorDesignerMove(m_sizeSinhSymbol.Width, size);
                        break;

                    case Mathematics.MethodNameCos:
                        FactorDesignerMove(m_sizeCosSymbol.Width, size);
                        break;

                    case Mathematics.MethodNameArcCos:
                        FactorDesignerMove(m_sizeArcCosSymbol.Width, size);
                        break;

                    case Mathematics.MethodNameCosh:
                        FactorDesignerMove(m_sizeCoshSymbol.Width, size);
                        break;

                    case Mathematics.MethodNameTan:
                        FactorDesignerMove(m_sizeTanSymbol.Width, size);
                        break;

                    case Mathematics.MethodNameArcTan:
                        FactorDesignerMove(m_sizeArcTanSymbol.Width, size);
                        break;

                    case Mathematics.MethodNameTanh:
                        FactorDesignerMove(m_sizeTanhSymbol.Width, size);
                        break;

                    case Mathematics.MethodNameLn:
                        FactorDesignerMove(m_sizeLnSymbol.Width, size);
                        break;

                    case Mathematics.MethodNameLg:
                        FactorDesignerMove(m_sizeLgSymbol.Width, size);
                        break;

                    case Mathematics.MethodNameSqrt:
                        FactorDesignerMove((int)m_sizeFRootExponent.Width + (int)m_sizeFRootExponent.Width / 2, size);
                        break;

                    case Mathematics.MethodNameCbrt:
                        FactorDesignerMove((int)m_sizeFRootExponent.Width + (int)m_sizeFRootExponent.Width / 2, size);
                        break;
                }
                //size.Width += 4;
            }
        }
        private void FactorDesignerMove(int nLeft, System.Drawing.Size size)
        {
            int nLeft2 = nLeft;

            for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
            {
                FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);
                factordesigner.Move(nLeft2, size.Height / 2 - factordesigner.Height / 2);

                nLeft2 += factordesigner.Width + m_sizeComma.Width;
            }
        }
        private void DrawErrorLine(System.Drawing.Graphics graphics, FactorSyntaxError factorsyntaxerror, int nX1, int nY1, int nX2, int nY2)
        {
            if (factorsyntaxerror != null)
                graphics.DrawLine(Pens.Red, nX1, nY1, nX2, nY2);               
        }
        private void RenderSpecificMathematicsSymbol(System.Drawing.Graphics graphics, string strSymbol, System.Drawing.Size sizeSymbol)
        {
            ComponentGraphicsDesigner.DrawString(graphics, strSymbol, Brushes.Black, m_factordesigner.Left, m_factordesigner.Top + (m_factordesigner.Height - sizeSymbol.Height) / 2);

            if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + sizeSymbol.Width, m_factordesigner.Top + 2);

            else
            {
                for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                {
                    FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);
                    factordesigner.Render(graphics);

                    if (nIndex < m_factordesigner.DesignerCount - 1)
                        ComponentGraphicsDesigner.DrawString(graphics, ",", Brushes.Black, factordesigner.Rectangle.Right, m_factordesigner.Top + (m_factordesigner.Height - m_sizeComma.Height) / 2);
                }
            }
        }
        private void RenderRootMathematicsSymbol(System.Drawing.Graphics graphics, int nRootExponent)
        {
            if (m_factordesigner.NamedDesignerCount == m_factordesigner.DesignerCount)
                DrawPlaceHolderRectangle(graphics, m_factordesigner.Left + (int)m_sizeFRootExponent.Width + (int)m_sizeFRootExponent.Width / 2 + 1, m_factordesigner.Top + 2);

            else
            {
                for (int nIndex = m_factordesigner.NamedDesignerCount; nIndex < m_factordesigner.DesignerCount; nIndex++)
                {
                    FactorDesigner factordesigner = (FactorDesigner)m_factordesigner.GetDesigner(nIndex);
                    factordesigner.Render(graphics);

                    if (nIndex < m_factordesigner.DesignerCount - 1)
                        ComponentGraphicsDesigner.DrawString(graphics, ",", Brushes.Black, factordesigner.Rectangle.Right, m_factordesigner.Top + (m_factordesigner.Height - m_sizeComma.Height) / 2);
                }
            }

            DrawRootSymbol(graphics, nRootExponent, m_factordesigner.Left, m_factordesigner.Top, m_factordesigner.Width == 0 ? 28 : m_factordesigner.Width, m_factordesigner.Height == 0 ? 18 : m_factordesigner.Height);
        }
        private void DrawPlaceHolderRectangle(System.Drawing.Graphics graphics, int nLeft, int nTop)
        {
            Pen pen = new Pen(Brushes.Green);
            pen.DashStyle = DashStyle.Dash;
            graphics.DrawRectangle(pen, nLeft, nTop, m_nPlaceHolderRectangleWidth, m_nPlaceHolderRectangleHeight);
        }
        private void DrawLittlePlaceHolderRectangle(System.Drawing.Graphics graphics, int nLeft, int nTop)
        {
            Pen pen = new Pen(Brushes.Green);
            pen.DashStyle = DashStyle.Dash;
            graphics.DrawRectangle(pen, nLeft, nTop, m_nLittlePlaceHolderRectangleWidth, m_nLittlePlaceHolderRectangleHeight);
        }
        private void DrawPlaceHolderRectangle(System.Drawing.Graphics graphics, Pen pen, int nLeft, int nTop)
        {
            graphics.DrawRectangle(pen, nLeft, nTop, m_nPlaceHolderRectangleWidth, m_nPlaceHolderRectangleHeight);
        }
        private void DrawRootSymbol(System.Drawing.Graphics graphics, int nRootExponent,int nLeft, int nTop, int nWidth, int nHeight)
        {
            int nRootExponentWidth = (int)m_sizeFRootExponent.Width;
            
            graphics.DrawLine(Pens.Black, nLeft, nTop + 2 * (nHeight / 3) + 2, nLeft + nRootExponentWidth / 3, nTop + 2 * (nHeight / 3));
            graphics.DrawLine(Pens.Black, nLeft + nRootExponentWidth / 3, nTop + 2 * (nHeight / 3), nLeft + nRootExponentWidth, nTop + nHeight);
            graphics.DrawLine(Pens.Black, nLeft + nRootExponentWidth, nTop + nHeight, nLeft + nRootExponentWidth + nRootExponentWidth / 2, nTop);
            graphics.DrawLine(Pens.Black, nLeft + nRootExponentWidth + nRootExponentWidth / 2, nTop, nLeft + nWidth, nTop);

            if (nRootExponent != 2)
                graphics.DrawString(nRootExponent.ToString(), FontLevelTwo, Brushes.Black, nLeft + 2, nTop - 2);
        }
        private static Pen CreateFillPen()
        {
            Pen pen = new Pen(Brushes.Green);
            pen.DashStyle = DashStyle.Dot;
            return pen;
        }
        private static System.Drawing.Font CreateSpecifiedFont(FontLevel fontLevel)
        {
            System.Drawing.Font font = null;
            System.Drawing.Font workbenchframeworkFont = WorkbenchFramework.InstanceWorkbenchFramework.Font;
            float nSize = workbenchframeworkFont.Size;
            float nInterval = (float)(workbenchframeworkFont.Size - workbenchframeworkFont.Size * 0.3) / 5;

            switch (fontLevel)
            {
                case FontLevel.LevelOne:
                    font = workbenchframeworkFont;
                    break;

                case FontLevel.LevelTwo:
                    font = new System.Drawing.Font(workbenchframeworkFont.FontFamily, nSize - nInterval);
                    break;

                case FontLevel.LevelThree:
                    font = new System.Drawing.Font(workbenchframeworkFont.FontFamily, nSize - 2 * nInterval);
                    break;

                case FontLevel.LevelFour:
                    font = new System.Drawing.Font(workbenchframeworkFont.FontFamily, nSize - 3 * nInterval);
                    break;

                case FontLevel.LevelFive:
                    font = new System.Drawing.Font(workbenchframeworkFont.FontFamily, nSize - 4 * nInterval);
                    break;

                case FontLevel.LevelSix:
                    font = new System.Drawing.Font(workbenchframeworkFont.FontFamily, nSize - 5 * nInterval);
                    break;

                default:
                    break;
            }

            return font;
        }
        private static System.Drawing.Font FontDecrementOneLevel(System.Drawing.Font fontCurrent)
        {
            if (fontCurrent != null)
            {
                if (fontCurrent.Equals(FontLevelOne))
                    return FontLevelTwo;

                else if (fontCurrent.Equals(FontLevelTwo))
                    return FontLevelThree;

                else if (fontCurrent.Equals(FontLevelThree))
                    return FontLevelFour;

                else if (fontCurrent.Equals(FontLevelFour))
                    return FontLevelFive;

                else if (fontCurrent.Equals(FontLevelFive))
                    return FontLevelSix;
            }

            return null;
        }
    }

    public class MathematicsDesigner : Eap.FactorDesigner
    {
        private System.Drawing.Size m_sizeComma;
        private System.Drawing.Size m_sizeLeftBracket;
        private System.Drawing.Size m_sizeRightBracket;
        private System.Drawing.Size m_sizeMethodName;
        private AdmlClassDesignerHelper m_admlclassdesignerhelper;
        private MathematicsDesignerHelper m_mathematicsdesignerhelper;
        public MathematicsDesigner(ComponentDesignDocumentView componentdesigndocumentview, Mathematics mathematics) : base(componentdesigndocumentview, mathematics)
        {
            OnConstruct();
        }
        public MathematicsDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace, Mathematics mathematics) : base(componentdesigndocumentview, componentdesignspace, mathematics)
        {
            OnConstruct();
        }

        public Mathematics Mathematics
        {
            get { return (Mathematics)Component; }
        }

        private void OnConstruct()
        {
            m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);
            m_mathematicsdesignerhelper = new MathematicsDesignerHelper(this);
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();
            OnConstruct();
        }

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            if (Mathematics.MethodName == Mathematics.MethodNameAbs)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameAbs);

            else if (Mathematics.MethodName == Mathematics.MethodNameCeil)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameCeil);

            else if (Mathematics.MethodName == Mathematics.MethodNameFloor)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameFloor);

            else if (Mathematics.MethodName == Mathematics.MethodNameSin)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameSin);

            else if (Mathematics.MethodName == Mathematics.MethodNameArcSin)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameArcSin);

            else if (Mathematics.MethodName == Mathematics.MethodNameCos)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameCos);

            else if (Mathematics.MethodName == Mathematics.MethodNameArcCos)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameArcCos);

            else if (Mathematics.MethodName == Mathematics.MethodNameTan)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameTan);

            else if (Mathematics.MethodName == Mathematics.MethodNameArcTan)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameArcTan);

            else if (Mathematics.MethodName == Mathematics.MethodNameLn)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameLn);

            else if (Mathematics.MethodName == Mathematics.MethodNameLg)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameLg);

            else if (Mathematics.MethodName == Mathematics.MethodNameSinh)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameSinh);

            else if (Mathematics.MethodName == Mathematics.MethodNameCosh)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameCosh);

            else if (Mathematics.MethodName == Mathematics.MethodNameTanh)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameTanh);

            else if (Mathematics.MethodName == Mathematics.MethodNameCbrt)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameCbrt);

            else if (Mathematics.MethodName == Mathematics.MethodNameSqrt)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameSqrt);

            else if (Mathematics.MethodName == Mathematics.MethodNameLog)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameLog);

            else if (Mathematics.MethodName == Mathematics.MethodNameExp)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameExp);

            else if (Mathematics.MethodName == Mathematics.MethodNamePow)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNamePow);

            else if (Mathematics.MethodName == Mathematics.MethodNameAverage)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameAverage);

            else if (Mathematics.MethodName == Mathematics.MethodNameSum)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameSum);

            else if (Mathematics.MethodName == Mathematics.MethodNameContinuousProduct)
                Size = m_mathematicsdesignerhelper.Arrange(graphics, Mathematics.MethodNameContinuousProduct);

            else
            {
                m_sizeMethodName = MeasureString(graphics, Mathematics.MethodName);
                m_sizeLeftBracket = MeasureString(graphics, "(");
                m_sizeRightBracket = MeasureString(graphics, ")");

                System.Drawing.Size size = new System.Drawing.Size(m_sizeMethodName.Width + m_sizeLeftBracket.Width + m_sizeRightBracket.Width, Math.Max(m_sizeMethodName.Height, Math.Max(m_sizeLeftBracket.Height, m_sizeRightBracket.Height)));

                if (NamedDesignerCount < DesignerCount)
                    m_sizeComma = MeasureString(graphics, ",");

                for (int nIndex = NamedDesignerCount; nIndex < DesignerCount; nIndex++)
                {
                    FactorDesigner factordesigner = (FactorDesigner)GetDesigner(nIndex);
                    factordesigner.Left = 0;
                    factordesigner.Top = 0;
                    factordesigner.Arrange(graphics);

                    size.Width += factordesigner.Width;
                    size.Height = Math.Max(size.Height, factordesigner.Height);

                    if (nIndex < DesignerCount - 1)
                        size.Width += m_sizeComma.Width;
                }

                int nLeft = m_sizeMethodName.Width + m_sizeLeftBracket.Width;

                for (int nIndex = NamedDesignerCount; nIndex < DesignerCount; nIndex++)
                {
                    FactorDesigner factordesigner = (FactorDesigner)GetDesigner(nIndex);
                    factordesigner.Move(nLeft, size.Height / 2 - factordesigner.Height / 2);

                    nLeft += factordesigner.Width + m_sizeComma.Width;
                }

                Size = size;
            }
        }
        public override void Render(System.Drawing.Graphics graphics)
        {
            if (Mathematics.MethodName == Mathematics.MethodNameAbs)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameAbs, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameCeil)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameCeil, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameFloor)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameFloor, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameSin)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameSin, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameArcSin)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameArcSin, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameCos)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameCos, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameArcCos)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameArcCos, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameTan)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameTan, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameArcTan)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameArcTan, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameLn)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameLn, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameLg)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameLg, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameSinh)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameSinh, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameCosh)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameCosh, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameTanh)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameTanh, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameCbrt)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameCbrt, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameSqrt)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameSqrt, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameLog)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameLog, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameExp)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameExp, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNamePow)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNamePow, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameAverage)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameAverage, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameSum)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameSum, FactorSyntaxError);

            else if (Mathematics.MethodName == Mathematics.MethodNameContinuousProduct)
                m_mathematicsdesignerhelper.Render(graphics, Mathematics.MethodNameContinuousProduct, FactorSyntaxError);

            else
            {
                DrawString(graphics, Mathematics.MethodName, KeywordBrush, Left, Top + (Height - m_sizeMethodName.Height) / 2);
                DrawString(graphics, "(", Brushes.Black, Left + m_sizeMethodName.Width, Top + (Height - m_sizeLeftBracket.Height) / 2);

                for (int nIndex = NamedDesignerCount; nIndex < DesignerCount; nIndex++)
                {
                    FactorDesigner factordesigner = (FactorDesigner)GetDesigner(nIndex);
                    factordesigner.Render(graphics);

                    if (nIndex < DesignerCount - 1)
                        DrawString(graphics, ",", Brushes.Black, factordesigner.Rectangle.Right, Top + (Height - m_sizeComma.Height) / 2);
                }

                DrawString(graphics, ")", Brushes.Black, Rectangle.Right - m_sizeRightBracket.Width, Top + (Height - m_sizeRightBracket.Height) / 2);

                if (FactorSyntaxError != null)
                    graphics.DrawLine(WarningPen, Left, Rectangle.Bottom - 1, Left + m_sizeMethodName.Width - 1, Rectangle.Bottom - 1);
            }
        }

        public override ComponentGraphicsDesigner FindComponentGraphicsDesigner(System.Drawing.Point point)
        {
            ComponentGraphicsDesigner componentgraphicsdesigner = m_admlclassdesignerhelper.FindComponentGraphicsDesigner(point, false);

            if (componentgraphicsdesigner != null)
                return componentgraphicsdesigner;

            return base.FindComponentGraphicsDesigner(point);
        }

        public override bool OnPasteUpdate(string stringClipboard)
        {
            return m_admlclassdesignerhelper.PasteUpdate(stringClipboard);
        }
        public override void OnPaste(string stringClipboard, System.Drawing.Point point)
        {
            if (CheckFactorNumberIsCorrect())
            {
                int nInsertingIndex = 0;

                for (int nIndex = NamedDesignerCount; nIndex < DesignerCount; nIndex++)
                {
                    if (((FactorDesigner)GetDesigner(nIndex)).Rectangle.Left > point.X)
                        break;

                    nInsertingIndex++;
                }

                m_admlclassdesignerhelper.Paste(nInsertingIndex, stringClipboard);
            }
        }
        public override void OnPaste(string stringClipboard, int nIndex)
        {
            m_admlclassdesignerhelper.Paste(nIndex, stringClipboard);
        }

        public override void OnKeyDown(object objSender, KeyEventArgs keyeventargs)
        {
            if (keyeventargs.KeyCode == Keys.Enter && IsSelected && AllowEdit)
            {
                keyeventargs.Handled = true;

                ArrayList arraylistSelectorItem = new ArrayList();

                arraylistSelectorItem.Add(new SelectorItem(null, "Common", "常用", null, "N/A"));
                arraylistSelectorItem.Add(new SelectorItem(null, "Math", "数学", null, "N/A"));
                arraylistSelectorItem.Add(new SelectorItem(null, "Statistical", "统计", null, "N/A"));
                
                for (int nIndex = 0; nIndex < Mathematics.CommonMethodNameValue.Length; nIndex++)
                    arraylistSelectorItem.Add(new SelectorItem(null, Mathematics.CommonMethodNameValue[nIndex], null, "Common", (string)Mathematics.MethodTip[Mathematics.CommonMethodNameValue[nIndex]]));

                for (int nIndex = 0; nIndex < Mathematics.MathematicsMethodNameValue.Length; nIndex++)
                    arraylistSelectorItem.Add(new SelectorItem(null, Mathematics.MathematicsMethodNameValue[nIndex], null, "Math", (string)Mathematics.MethodTip[Mathematics.MathematicsMethodNameValue[nIndex]]));

                for (int nIndex = 0; nIndex < Mathematics.StatisticalMethodNameVlaue.Length; nIndex++)
                    arraylistSelectorItem.Add(new SelectorItem(null, Mathematics.StatisticalMethodNameVlaue[nIndex], null, "Statistical", (string)Mathematics.MethodTip[Mathematics.StatisticalMethodNameVlaue[nIndex]]));
                
                Selector selector = new Selector(arraylistSelectorItem);

                selector.Font = WorkbenchFramework.InstanceWorkbenchFramework.Font;
                selector.Owner = WorkbenchFramework.InstanceWorkbenchFramework;
                selector.Selected += new HandleSelected(OnUpdateMethodNameSelectorSelected);
                selector.Show(AdmlClassDesignerHelper.GetPoint(this, Left, Top));
                selector.SelectedText = Mathematics.MethodName;
            }

            //else if (!m_admlclassdesignerhelper.KeyDown(objSender, keyeventargs, false))
            //    base.OnKeyDown(objSender, keyeventargs);
            else if (keyeventargs.KeyCode == Keys.Insert && IsSelected && AllowEdit)
            {
                if (CheckFactorNumberIsCorrect())
                {
                    bool bResult = m_admlclassdesignerhelper.KeyDown(objSender, keyeventargs, false);

                    if (!bResult)
                        base.OnKeyDown(objSender, keyeventargs);
                }   
            }
        }
        public override void Navigate(KeyEventArgs keyeventargs)
        {
            m_admlclassdesignerhelper.Navigate(keyeventargs, false, false);
        }

        private void OnUpdateMethodNameSelectorSelected(Selector selector, string strText)
        {
            if (Mathematics.MethodName != strText)
            {
                ComponentDesignDocument.BeforeUpdate(Mathematics, "*");
                Mathematics.MethodName = strText;

                PropertyGrid.Categories.Clear();
                PropertyGridItem propertygriditem = ComponentDesigner.PropertyGrid.AddCategory(Component.GetType().Name + "属性");
                propertygriditem.Expanded = true;
                ProvidePropertyGridControl(propertygriditem);

                ComponentDesignDocument.AfterUpdate(Mathematics, "*");
            }
        }

        protected override void OnUpdate(Component componentPath, Component componentUpdated)
        {
            base.OnUpdate(componentPath, componentUpdated);

            if (m_admlclassdesignerhelper != null)
                m_admlclassdesignerhelper = new AdmlClassDesignerHelper(this);
        }

        private bool CheckFactorNumberIsCorrect()
        {
            if (Mathematics.MethodName == Mathematics.MethodNameAbs || Mathematics.MethodName == Mathematics.MethodNameCeil || Mathematics.MethodName == Mathematics.MethodNameFloor || Mathematics.MethodName == Mathematics.MethodNameSin || Mathematics.MethodName == Mathematics.MethodNameArcSin || Mathematics.MethodName == Mathematics.MethodNameSinh ||
                Mathematics.MethodName == Mathematics.MethodNameCos || Mathematics.MethodName == Mathematics.MethodNameArcCos || Mathematics.MethodName == Mathematics.MethodNameCosh || Mathematics.MethodName == Mathematics.MethodNameTan || Mathematics.MethodName == Mathematics.MethodNameArcTan || Mathematics.MethodName == Mathematics.MethodNameTanh ||
                Mathematics.MethodName == Mathematics.MethodNameLn || Mathematics.MethodName == Mathematics.MethodNameLg || Mathematics.MethodName == Mathematics.MethodNameSqrt || Mathematics.MethodName == Mathematics.MethodNameCbrt || Mathematics.MethodName == Mathematics.MethodNameExp)
            {
                if ((DesignerCount - NamedDesignerCount) >= 1)
                {
                    MessageBox.Show(Mathematics.MethodName + "函数因子个数过多,不可再插入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            else if (Mathematics.MethodName == Mathematics.MethodNamePow || Mathematics.MethodName == Mathematics.MethodNameLog)
            {
                if ((DesignerCount - NamedDesignerCount) >= 2)
                {
                    MessageBox.Show(Mathematics.MethodName + "函数因子个数过多,不可再插入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            else if (Mathematics.MethodName == Mathematics.MethodNameSum || Mathematics.MethodName == Mathematics.MethodNameContinuousProduct)
            {
                if ((DesignerCount - NamedDesignerCount) >= 3)
                {
                    MessageBox.Show(Mathematics.MethodName + "函数因子个数过多,不可再插入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            return true;
        }
    }
}
