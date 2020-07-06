using System;
using System.Collections;
using Eap;

namespace ExtEap
{
    public class Mathematics : Factor
    {
        public const string MethodNameAbs = "Abs";
        public const string MethodNameMax = "Max";
        public const string MethodNameMin = "Min";
        public const string MethodNameMod = "Mod";
        public const string MethodNameRandom = "Random";
        public const string MethodNameRound = "Round";
        public const string MethodNamePow = "Pow";
        public const string MethodNameCeil = "Ceil";
        public const string MethodNameSin = "Sin";
        public const string MethodNameArcSin = "ArcSin";
        public const string MethodNameCos = "Cos";
        public const string MethodNameArcCos = "ArcCos";
        public const string MethodNameTan = "Tan";
        public const string MethodNameArcTan = "ArcTan";
        public const string MethodNameExp = "Exp";
        public const string MethodNameLog = "Log";
        public const string MethodNameLn = "Ln";
        public const string MethodNameLg = "Lg";
        public const string MethodNameSqrt = "Sqrt";
        public const string MethodNameCbrt = "Cbrt";
        public const string MethodNameHypot = "Hypot";
        public const string MethodNameSum = "Sum";
        public const string MethodNameAverage = "Average";
        public const string MethodNameLarge = "Large";
        public const string MethodNameSmall = "Small";
        public const string MethodNameFloor = "Floor";
        public const string MethodNameSinh = "Sinh";
        public const string MethodNameCosh = "Cosh";
        public const string MethodNameTanh = "Tanh";
        public const string MethodNameToDegrees ="ToDegrees";
        public const string MethodNameToRadians = "ToRadians";
        public const string MethodNameContinuousProduct = "ContinuousProduct";

        public static string[] MethodNameValue = { MethodNameAbs, MethodNameMax, MethodNameMin, MethodNameMod, MethodNameRandom, MethodNameRound, MethodNamePow, MethodNameCeil,
                                                    MethodNameSin, MethodNameArcSin, MethodNameCos, MethodNameArcCos, MethodNameTan, MethodNameArcTan, MethodNameContinuousProduct,
                                                    MethodNameExp, MethodNameLog, MethodNameLn, MethodNameLg, MethodNameSqrt, MethodNameCbrt, MethodNameHypot,
                                                    MethodNameSum, MethodNameAverage, MethodNameLarge, MethodNameSmall, MethodNameFloor, MethodNameSinh, MethodNameCosh, MethodNameTanh, MethodNameToDegrees, MethodNameToRadians};

        public static string[] CommonMethodNameValue = { MethodNameAbs, MethodNameMax, MethodNameMin, MethodNameMod, MethodNameRandom, MethodNameRound, MethodNameCeil, MethodNameFloor};
        public static string[] MathematicsMethodNameValue = { MethodNamePow, MethodNameSin, MethodNameArcSin, MethodNameCos, MethodNameArcCos, MethodNameTan, MethodNameArcTan,
                                                                MethodNameExp, MethodNameLog, MethodNameLn, MethodNameLg, MethodNameSqrt, MethodNameCbrt, MethodNameHypot,
                                                                MethodNameSinh, MethodNameCosh, MethodNameTanh, MethodNameToDegrees, MethodNameToRadians};
        public static string[] StatisticalMethodNameVlaue = { MethodNameSum, MethodNameAverage, MethodNameLarge, MethodNameSmall, MethodNameContinuousProduct};

        public static Hashtable MethodTip = InitMethodTip();

        private static ValidateStringValue m_validatestringvalue = new ValidateStringValue(new StringEnumeration(MethodNameValue).ValidateStringValue);

        private Eap.String m_stringMethodName;
        private ComponentArrayListDecorator m_componentarraylistdecoratorParameterFactor;

        public Mathematics(Eap.Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }

        public string MethodName
        {
            get { return m_stringMethodName.Value; }
            set { m_stringMethodName.Value = value; }
        }
        public int ParameterFactorCount
        {
            get { return m_componentarraylistdecoratorParameterFactor.ComponentCount; }
        }

        private void OnConstruct()
        {
            SetElement("MethodName", m_stringMethodName = new Eap.String(m_validatestringvalue, MethodNameAbs));
            m_componentarraylistdecoratorParameterFactor = new ComponentArrayListDecorator(this, ComponentFactory, typeof(Factor), new object[] { OwnerStatement });
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_componentarraylistdecoratorParameterFactor.Initialize();

            OnConstruct();
        }
        private static Hashtable InitMethodTip()
        {
            Hashtable hashtable = new Hashtable();
            hashtable[MethodNameAbs] = "Abs(integer x), 返回x的绝对值(+2 多个重载)";
            hashtable[MethodNameMax] = "Max(integer x,integer y)或Max(integer[] param), 返回最大值(+2 多个重载)";
            hashtable[MethodNameMin] = "Min(integer x,integer y)或Min(integer[] param), 返回最小值(+2 多个重载)";
            hashtable[MethodNameMod] = "Mod(integer x,integer y), 返回x%y的值(+1 多个重载)";
            hashtable[MethodNameRandom] = "Random(), 返回随机数";
            hashtable[MethodNameRound] = "Round(float x,integer y), 返回将x按y指定的小数位数舍入值(+1 多个重载)";
            hashtable[MethodNamePow] = "Pow(float x,float y), 返回x的y次幂";
            hashtable[MethodNameCeil] = "Ceil(float x), 返回大于或等于x的最小的整数值(+1 多个重载)";
            hashtable[MethodNameSin] = "Sin(float x), 返回x(弧度值)对应的正弦值";
            hashtable[MethodNameArcSin] = "ArcSin(float x), 返回值为x对应的角度(弧度值)";
            hashtable[MethodNameCos] = "Cos(float x), 返回x(弧度值)对应的余弦值";
            hashtable[MethodNameArcCos] = "ArcCos(float x), 返回值为x对应的角度(弧度值)";
            hashtable[MethodNameTan] = "Tan(float x), 返回x(弧度值)对应的正切值";
            hashtable[MethodNameArcTan] = "ArcTan(float x), 返回值为x对应的角度(弧度值)";
            hashtable[MethodNameExp] = "Exp(float x), 返回自然对数的底e的x次幂";
            hashtable[MethodNameLog] = "Log(float x,float y), 返回x以y为底的对数值";
            hashtable[MethodNameLn] = "Ln(float x), 返回x以e为底的对数值";
            hashtable[MethodNameLg] = "Lg(float x), 返回x以10为底的对数值";
            hashtable[MethodNameSqrt] = "Sqrt(float x), 返回x的算术平方根";
            hashtable[MethodNameCbrt] = "Cbrt(float x), 返回x的立方根";
            hashtable[MethodNameHypot] = "Hypot(float x,float y), 返回Sqrt(x²+y²)的值";
            hashtable[MethodNameSum] = "Sum(integer[] param int nStart, int nEnd), 返回一组数据指定起始点和终止点的和(+2 多个重载)";
            hashtable[MethodNameAverage] = "Average(float[] param), 返回一组数据的平均数(+1 多个重载)";
            hashtable[MethodNameLarge] = "Large(float[] param, int nExpect), 返回nExpect指定的第几大的值(+1 多个重载)";
            hashtable[MethodNameSmall] = "Small(float[] param, int nExpect), 返回nExpect指定的第几小的值(+1 多个重载)";
            hashtable[MethodNameFloor] = "Floor(float x), 返回小于或等于x的最大整数值(+1 多个重载)";
            hashtable[MethodNameSinh] = "Sinh(float x), 返回x(弧度值)的双曲正弦值";
            hashtable[MethodNameCosh] = "Cosh(float x), 返回x(弧度值)的双曲余弦值";
            hashtable[MethodNameTanh] = "Tanh(float x), 返回x(弧度值)的双曲正切值";
            hashtable[MethodNameToDegrees] = "ToDegrees(float x),将x(弧度值)转为对应的角的角度值";
            hashtable[MethodNameToRadians] = "ToRadians(float x), 将x(角度值)转换为对应的角的弧度值";
            hashtable[MethodNameContinuousProduct] = "ContinuousProduct(integer[] param, int nStart, int nEnd)，对数组指定起始部点和终止点连续求积(+2 多个重载)";

            return hashtable;

        }
        public Factor GetParameterFactor(int nIndex)
        {
            return (Factor)m_componentarraylistdecoratorParameterFactor.GetComponent(nIndex);
        }
        public void SetParameterFactor(int nIndex, Factor factor, bool bIsInserting)
        {
            m_componentarraylistdecoratorParameterFactor.SetComponent(nIndex, factor, bIsInserting);
        }
        public void SetParameterFactor(int nIndex, Factor factor)
        {
            m_componentarraylistdecoratorParameterFactor.SetComponent(nIndex, factor, false);
        }

        protected override Eap.Object OnExecute(AdmlObject admlobject, ExecutionStack executionstack)
        {
            int nExecutionIndex = ParameterFactorCount - 1;
            string strExecutionName = executionstack.GetExecutionName();

            if (strExecutionName != null)
            {
                nExecutionIndex = int.Parse(strExecutionName.Substring(16));

                Eap.Object objectParameter = GetParameterFactor(nExecutionIndex).Execute(admlobject, executionstack);

                executionstack.PopExecutionName();
                executionstack.PushObject(objectParameter);

                nExecutionIndex--;
            }

            for (int nIndex = nExecutionIndex; nIndex >= 0; nIndex--)
            {
                executionstack.PushExecutionName("ParameterFactor" + nIndex);
                Eap.Object objectParameter = GetParameterFactor(nIndex).Execute(admlobject, executionstack);

                executionstack.PopExecutionName();
                executionstack.PushObject(objectParameter);
            }

            Eap.Object objectReturn = ReferableObject.NullReferableObject;

            if (MethodName == MethodNameAbs)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is IntegerObject)
                    objectReturn = new IntegerObject(ClassLibrary, Math.Abs(((IntegerObject)objectParameter).Value));

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Abs(((FloatObject)objectParameter).Value));

                if (objectParameter is DecimalObject)
                    objectReturn = new DecimalObject(ClassLibrary, Math.Abs(((DecimalObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameMax)
            {
                Eap.Object objectParameter0 = executionstack.GetTopObject(0);

                if (objectParameter0 is ArrayObject)
                {
                    ArrayObject arrayobjectParameter = (ArrayObject)objectParameter0;
                    int nLength = arrayobjectParameter.Count;

                    if (arrayobjectParameter.ClassName == IntegerClass.NameStatic + "[]")
                    {
                        int[] nParam = new int[nLength];

                        for (int nIndex = 0; nIndex < nParam.Length; nIndex++)
                            nParam[nIndex] = ((IntegerObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new IntegerObject(ClassLibrary, MathUtil.Max(nParam));
                    }

                    if (arrayobjectParameter.ClassName == FloatClass.NameStatic + "[]")
                    {
                        double[] dParam = new double[nLength];

                        for (int nIndex = 0; nIndex < dParam.Length; nIndex++)
                            dParam[nIndex] = ((FloatObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new FloatObject(ClassLibrary, MathUtil.Max(dParam));
                    }

                    if (arrayobjectParameter.ClassName == DecimalClass.NameStatic + "[]")
                    {
                        decimal[] decParam = new decimal[nLength];

                        for (int nIndex = 0; nIndex < decParam.Length; nIndex++)
                            decParam[nIndex] = ((DecimalObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new DecimalObject(ClassLibrary, MathUtil.Max(decParam));
                    }
                }

                else
                {
                    Eap.Object objectParameter1 = executionstack.GetTopObject(0, 1);

                    if (objectParameter0 is IntegerObject && objectParameter1 is IntegerObject)
                        objectReturn = new IntegerObject(ClassLibrary, Math.Max(((IntegerObject)objectParameter0).Value, ((IntegerObject)objectParameter1).Value));

                    if (objectParameter0 is FloatObject && objectParameter1 is FloatObject)
                        objectReturn = new FloatObject(ClassLibrary, Math.Max(((FloatObject)objectParameter0).Value, ((FloatObject)objectParameter1).Value));

                    if (objectParameter0 is DecimalObject && objectParameter1 is DecimalObject)
                        objectReturn = new DecimalObject(ClassLibrary, Math.Max(((DecimalObject)objectParameter0).Value, ((DecimalObject)objectParameter1).Value));
                }
            }

            else if (MethodName == MethodNameMin)
            {
                Eap.Object objectParameter0 = executionstack.GetTopObject(0);
                
                if (objectParameter0 is ArrayObject)
                {
                    ArrayObject arrayobjectParameter = (ArrayObject)objectParameter0;
                    int nLength = arrayobjectParameter.Count;

                    if (arrayobjectParameter.ClassName == IntegerClass.NameStatic + "[]")
                    {
                        int[] nParam = new int[nLength];

                        for (int nIndex = 0; nIndex < nParam.Length; nIndex++)
                            nParam[nIndex] = ((IntegerObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new IntegerObject(ClassLibrary, MathUtil.Min(nParam));
                    }

                    if (arrayobjectParameter.ClassName == FloatClass.NameStatic + "[]")
                    {
                        double[] dParam = new double[nLength];

                        for (int nIndex = 0; nIndex < dParam.Length; nIndex++)
                            dParam[nIndex] = ((FloatObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new FloatObject(ClassLibrary, MathUtil.Min(dParam));
                    }

                    if (arrayobjectParameter.ClassName == DecimalClass.NameStatic + "[]")
                    {
                        decimal[] decParam = new decimal[nLength];

                        for (int nIndex = 0; nIndex < decParam.Length; nIndex++)
                            decParam[nIndex] = ((DecimalObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new DecimalObject(ClassLibrary, MathUtil.Min(decParam));
                    }
                }

                else
                {
                    Eap.Object objectParameter1 = executionstack.GetTopObject(0, 1);

                    if (objectParameter0 is IntegerObject && objectParameter1 is IntegerObject)
                        objectReturn = new IntegerObject(ClassLibrary, Math.Min(((IntegerObject)objectParameter0).Value, ((IntegerObject)objectParameter1).Value));

                    if (objectParameter0 is FloatObject && objectParameter1 is FloatObject)
                        objectReturn = new FloatObject(ClassLibrary, Math.Min(((FloatObject)objectParameter0).Value, ((FloatObject)objectParameter1).Value));

                    if (objectParameter0 is DecimalObject && objectParameter1 is DecimalObject)
                        objectReturn = new DecimalObject(ClassLibrary, Math.Min(((DecimalObject)objectParameter0).Value, ((DecimalObject)objectParameter1).Value));
                }
            }

            else if (MethodName == MethodNameMod)
            {
                Eap.Object objectParameter0 = executionstack.GetTopObject(0);
                Eap.Object objectParameter1 = executionstack.GetTopObject(0, 1);

                if (objectParameter0 is IntegerObject && objectParameter1 is IntegerObject)
                    objectReturn = new IntegerObject(ClassLibrary, ((IntegerObject)objectParameter0).Value % ((IntegerObject)objectParameter1).Value);

                if (objectParameter0 is FloatObject && objectParameter1 is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, ((FloatObject)objectParameter0).Value % ((FloatObject)objectParameter1).Value);
            }

            else if (MethodName == MethodNameRandom)
                objectReturn = new IntegerObject(ClassLibrary, new Random().Next());

            else if (MethodName == MethodNamePow)
            {
                Eap.Object objectParameter0 = executionstack.GetTopObject(0);
                Eap.Object objectParameter1 = executionstack.GetTopObject(0, 1);

                objectReturn = new FloatObject(ClassLibrary, Math.Pow(((FloatObject)objectParameter0).Value, ((FloatObject)objectParameter1).Value));
            }

            else if (MethodName == MethodNameRound)
            {
                Eap.Object objectParameter0 = executionstack.GetTopObject(0);
                Eap.Object objectParameter1 = executionstack.GetTopObject(0, 1);

                objectReturn = objectParameter0;

                if (objectParameter0 is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Round(((FloatObject)objectParameter0).Value, ((IntegerObject)objectParameter1).Value, MidpointRounding.AwayFromZero));

                if (objectParameter0 is DecimalObject)
                    objectReturn = new DecimalObject(ClassLibrary, Math.Round(((DecimalObject)objectParameter0).Value, ((IntegerObject)objectParameter1).Value, MidpointRounding.AwayFromZero));
            }

            else if (MethodName == MethodNameCeil)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Ceiling(((FloatObject)objectParameter).Value));

                if (objectParameter is DecimalObject)
                    objectReturn = new DecimalObject(ClassLibrary, Math.Ceiling(((DecimalObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameSin)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Sin(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameArcSin)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject && objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Asin(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameCos)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Cos(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameArcCos)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject && objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Acos(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameTan)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Tan(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameArcTan)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject && objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Atan(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameExp)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Exp(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameLog)
            {
                Eap.Object objectParameter0 = executionstack.GetTopObject(0);
                Eap.Object objectParameter1 = executionstack.GetTopObject(0, 1);

                if (objectParameter0 is FloatObject && objectParameter1 is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Log(((FloatObject)objectParameter1).Value, ((FloatObject)objectParameter0).Value));
            }

            else if (MethodName == MethodNameLn)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Log(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameLg)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Log10(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameSqrt)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Sqrt(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameCbrt)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Pow(((FloatObject)objectParameter).Value, 1.0 / 3));
            }

            else if (MethodName == MethodNameHypot)
            {
                Eap.Object objectParameter0 = executionstack.GetTopObject(0);
                Eap.Object objectParameter1 = executionstack.GetTopObject(0, 1);

                if (objectParameter0 is FloatObject && objectParameter1 is FloatObject)
                {
                    double dParame0 = ((FloatObject)objectParameter0).Value * ((FloatObject)objectParameter0).Value;
                    double dParame1 = ((FloatObject)objectParameter1).Value * ((FloatObject)objectParameter1).Value;
                    objectReturn = new FloatObject(ClassLibrary, Math.Sqrt(dParame0 + dParame1));
                }
            }

            else if (MethodName == MethodNameContinuousProduct)
            {
                Eap.Object objectLownerBound = executionstack.GetTopObject(0);
                Eap.Object objectUpperBound = executionstack.GetTopObject(0, 1);
                Eap.Object objectParameter = executionstack.GetTopObject(0, 2);

                if (objectLownerBound is IntegerObject && objectUpperBound is IntegerObject && objectParameter is ArrayObject)
                {
                    ArrayObject arrayobjectParameter = (ArrayObject)objectParameter;
                    int nLength = arrayobjectParameter.Count;
                    int nStart = ((IntegerObject)objectLownerBound).Value;
                    int nEnd = ((IntegerObject)objectUpperBound).Value;

                    if (arrayobjectParameter.ClassName == IntegerClass.NameStatic + "[]")
                    {
                        int[] nParam = new int[nLength];

                        for (int nIndex = 0; nIndex < nParam.Length; nIndex++)
                            nParam[nIndex] = ((IntegerObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new IntegerObject(ClassLibrary, MathUtil.ContinuousProduct(nParam, nStart, nEnd));
                    }

                    if (arrayobjectParameter.ClassName == FloatClass.NameStatic + "[]")
                    {
                        double[] dParam = new double[nLength];

                        for (int nIndex = 0; nIndex < dParam.Length; nIndex++)
                            dParam[nIndex] = ((FloatObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new FloatObject(ClassLibrary, MathUtil.ContinuousProduct(dParam, nStart, nEnd));
                    }

                    if (arrayobjectParameter.ClassName == DecimalClass.NameStatic + "[]")
                    {
                        decimal[] decParam = new decimal[nLength];

                        for (int nIndex = 0; nIndex < decParam.Length; nIndex++)
                            decParam[nIndex] = ((DecimalObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new DecimalObject(ClassLibrary, MathUtil.ContinuousProduct(decParam, nStart, nEnd));
                    }
                }
            }

            else if (MethodName == MethodNameSum)
            {
                Eap.Object objectLowerBound = executionstack.GetTopObject(0);
                Eap.Object objectUpperBound = executionstack.GetTopObject(0, 1);
                Eap.Object objectParameter = executionstack.GetTopObject(0, 2);

                if (objectLowerBound is IntegerObject && objectUpperBound is IntegerObject && objectParameter is ArrayObject)
                {
                    ArrayObject arrayobjectParameter = (ArrayObject)objectParameter;
                    int nLength = arrayobjectParameter.Count;
                    int nStart = ((IntegerObject)objectLowerBound).Value;
                    int nEnd = ((IntegerObject)objectUpperBound).Value;

                    if (arrayobjectParameter.ClassName == IntegerClass.NameStatic + "[]")
                    {
                        int[] nParam = new int[nLength];

                        for (int nIndex = 0; nIndex < nParam.Length; nIndex++)
                            nParam[nIndex] = ((IntegerObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new IntegerObject(ClassLibrary, MathUtil.Sum(nParam, nStart, nEnd));
                    }

                    if (arrayobjectParameter.ClassName == FloatClass.NameStatic + "[]")
                    {
                        double[] dParam = new double[nLength];

                        for (int nIndex = 0; nIndex < dParam.Length; nIndex++)
                            dParam[nIndex] = ((FloatObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new FloatObject(ClassLibrary, MathUtil.Sum(dParam, nStart, nEnd));
                    }

                    if (arrayobjectParameter.ClassName == DecimalClass.NameStatic + "[]")
                    {
                        decimal[] decParam = new decimal[nLength];

                        for (int nIndex = 0; nIndex < decParam.Length; nIndex++)
                            decParam[nIndex] = ((DecimalObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new DecimalObject(ClassLibrary, MathUtil.Sum(decParam, nStart, nEnd));
                    }
                }
            }

            else if (MethodName == MethodNameAverage)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is ArrayObject)
                {
                    ArrayObject arrayobjectParameter = (ArrayObject)objectParameter;
                    int nLength = arrayobjectParameter.Count;

                    if (arrayobjectParameter.ClassName == FloatClass.NameStatic + "[]")
                    {
                        double[] dParam = new double[nLength];

                        for (int nIndex = 0; nIndex < dParam.Length; nIndex++)
                            dParam[nIndex] = ((FloatObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new FloatObject(ClassLibrary, MathUtil.Average(dParam));
                    }

                    if (arrayobjectParameter.ClassName == DecimalClass.NameStatic + "[]")
                    {
                        decimal[] decParam = new decimal[nLength];

                        for (int nIndex = 0; nIndex < decParam.Length; nIndex++)
                            decParam[nIndex] = ((DecimalObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new DecimalObject(ClassLibrary, MathUtil.Average(decParam));
                    }
                }
            }

            else if (MethodName == MethodNameLarge)
            {
                Eap.Object objectParameter0 = executionstack.GetTopObject(0);
                Eap.Object objectParameter1 = executionstack.GetTopObject(0, 1);

                if (objectParameter0 is ArrayObject && objectParameter1 is IntegerObject)
                {
                    ArrayObject arrayobjectParameter = (ArrayObject)objectParameter0;
                    int nExpect = ((IntegerObject)objectParameter1).Value;
                    int nLength = arrayobjectParameter.Count;

                    if (arrayobjectParameter.ClassName == FloatClass.NameStatic + "[]")
                    {
                        double[] dParam = new double[nLength];

                        for (int nIndex = 0; nIndex < dParam.Length; nIndex++)
                            dParam[nIndex] = ((FloatObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new FloatObject(ClassLibrary, MathUtil.Large(dParam, nExpect));
                    }

                    if (arrayobjectParameter.ClassName == DecimalClass.NameStatic + "[]")
                    {
                        decimal[] decParam = new decimal[nLength];

                        for (int nIndex = 0; nIndex < decParam.Length; nIndex++)
                            decParam[nIndex] = ((DecimalObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new DecimalObject(ClassLibrary, MathUtil.Large(decParam, nExpect));
                    }
                }
            }

            else if (MethodName == MethodNameSmall)
            {
                Eap.Object objectParameter0 = executionstack.GetTopObject(0);
                Eap.Object objectParameter1 = executionstack.GetTopObject(0, 1);

                if (objectParameter0 is ArrayObject && objectParameter1 is IntegerObject)
                {
                    ArrayObject arrayobjectParameter = (ArrayObject)objectParameter0;
                    int nExpect = ((IntegerObject)objectParameter1).Value;
                    int nLength = arrayobjectParameter.Count;

                    if (arrayobjectParameter.ClassName == FloatClass.NameStatic + "[]")
                    {
                        double[] dParam = new double[nLength];

                        for (int nIndex = 0; nIndex < dParam.Length; nIndex++)
                            dParam[nIndex] = ((FloatObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new FloatObject(ClassLibrary, MathUtil.Small(dParam, nExpect));
                    }

                    if (arrayobjectParameter.ClassName == DecimalClass.NameStatic + "[]")
                    {
                        decimal[] decParam = new decimal[nLength];

                        for (int nIndex = 0; nIndex < decParam.Length; nIndex++)
                            decParam[nIndex] = ((DecimalObject)arrayobjectParameter.GetItemObject(nIndex)).Value;

                        objectReturn = new DecimalObject(ClassLibrary, MathUtil.Small(decParam, nExpect));
                    }
                }
            }

            else if (MethodName == MethodNameFloor)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Floor(((FloatObject)objectParameter).Value));

                if (objectParameter is DecimalObject)
                    objectReturn = new DecimalObject(ClassLibrary, Math.Floor(((DecimalObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameSinh)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Sinh(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameCosh)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Cosh(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameTanh)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                    objectReturn = new FloatObject(ClassLibrary, Math.Tanh(((FloatObject)objectParameter).Value));
            }

            else if (MethodName == MethodNameToDegrees)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                {
                    double dParam = ((FloatObject)objectParameter).Value;
                    objectReturn = new FloatObject(ClassLibrary, dParam * 180.0 / Math.PI);
                }
            }

            else if (MethodName == MethodNameToRadians)
            {
                Eap.Object objectParameter = executionstack.GetTopObject(0);

                if (objectParameter is FloatObject)
                {
                    double dParam = ((FloatObject)objectParameter).Value;
                    objectReturn = new FloatObject(ClassLibrary, dParam / 180.0 * Math.PI);
                }
            }

            executionstack.PopObject(ParameterFactorCount);
            return objectReturn;
        }
        protected override string OnParse(SyntaxErrorList syntaxerrorlist)
        {
            if (MethodName == MethodNameRandom)
            {
                if (ParameterFactorCount > 0)
                    syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("有多余的参数", this));

                return IntegerClass.NameStatic;
            }

            if (MethodName == MethodNameAbs || MethodName == MethodNameCeil || MethodName == MethodNameSin || MethodName == MethodNameArcSin || MethodName == MethodNameCos || MethodName == MethodNameArcCos || MethodName == MethodNameTan || MethodName == MethodNameArcTan ||
                MethodName == MethodNameExp || MethodName == MethodNameLg || MethodName == MethodNameLn || MethodName == MethodNameSqrt || MethodName == MethodNameCbrt || MethodName == MethodNameAverage ||
                MethodName == MethodNameFloor || MethodName == MethodNameSinh || MethodName == MethodNameCosh || MethodName == MethodNameTanh || MethodName == MethodNameToDegrees || MethodName == MethodNameToRadians)
            {
                if (ParameterFactorCount < 1)
                    syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("缺少参数", this));

                if (ParameterFactorCount > 1)
                    syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("有多余的参数", this));
            }

            if (MethodName == MethodNameMod || MethodName == MethodNameRound || MethodName == MethodNamePow || MethodName == MethodNameLog || MethodName == MethodNameHypot || MethodName == MethodNameLarge || MethodName == MethodNameSmall)
            {
                if (ParameterFactorCount < 2)
                    syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("缺少参数", this));

                if (ParameterFactorCount > 2)
                    syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("有多余的参数", this));
            }

            if (MethodName == MethodNameMax || MethodName == MethodNameMin)
            {
                if (ParameterFactorCount < 1)
                    syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("缺少参数", this));

                if (ParameterFactorCount > 2)
                    syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("有多余的参数", this));
            }

            if (MethodName == MethodNameSum || MethodName == MethodNameContinuousProduct)
            {
                if (ParameterFactorCount < 1)
                    syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("缺少参数", this));

                if (ParameterFactorCount > 3)
                    syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("有多余的参数", this));
            }

            return ParseParameter(syntaxerrorlist);
        }
        private string ParseParameter(SyntaxErrorList syntaxerrorlist)
        {
            string strParseName = null;

            for (int nIndex = 0; nIndex < ParameterFactorCount; nIndex++)
            {
                Factor factor = GetParameterFactor(nIndex);
                string strName = factor.Parse(syntaxerrorlist);

                if (nIndex == 0)
                    strParseName = strName;

                if (MethodName == MethodNameAbs)
                {
                    if (strName != IntegerClass.NameStatic && strName != FloatClass.NameStatic && strName != DecimalClass.NameStatic)
                        syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("参数类型必须是" + IntegerClass.NameStatic + "、" + FloatClass.NameStatic + "或" + DecimalClass.NameStatic, factor));
                }

                if (MethodName == MethodNameMax || MethodName == MethodNameMin)
                {
                    if (ParameterFactorCount == 2)
                    {
                        if (strName != IntegerClass.NameStatic && strName != FloatClass.NameStatic && strName != DecimalClass.NameStatic)
                            syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("参数类型必须是" + IntegerClass.NameStatic + "、" + FloatClass.NameStatic + "或" + DecimalClass.NameStatic, factor));
                    }

                    else
                    {
                        if (strName != IntegerClass.NameStatic + "[]" && strName != FloatClass.NameStatic + "[]" && strName != DecimalClass.NameStatic + "[]")
                            syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("参数类型必须是" + IntegerClass.NameStatic+ "[]" + "、" + FloatClass.NameStatic + "[]" + "或" + DecimalClass.NameStatic + "[]", factor));
                    }
                }

                if (MethodName == MethodNameMod)
                {
                    if (strParseName != strName && strName != IntegerClass.NameStatic && strName != FloatClass.NameStatic)
                        syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("参数类型必须都是" + IntegerClass.NameStatic + "或" + FloatClass.NameStatic, this));
                }

                if (MethodName == MethodNameCeil || MethodName == MethodNameFloor)
                {
                    if (strName != DecimalClass.NameStatic && strName != FloatClass.NameStatic)
                        syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("参数类型必须是" + DecimalClass.NameStatic + "或" + FloatClass.NameStatic, this));
                }

                if (MethodName == MethodNamePow || MethodName == MethodNameSin || MethodName == MethodNameArcSin || MethodName == MethodNameCos || MethodName == MethodNameArcCos || MethodName == MethodNameTan ||
                    MethodName == MethodNameArcTan || MethodName == MethodNameExp || MethodName == MethodNameLog || MethodName == MethodNameLn || MethodName == MethodNameLg || MethodName == MethodNameSqrt || MethodName == MethodNameCbrt || MethodName == MethodNameHypot ||
                    MethodName == MethodNameSinh || MethodName == MethodNameCosh || MethodName == MethodNameTanh || MethodName == MethodNameToDegrees || MethodName == MethodNameToRadians)
                {
                    if (strName != FloatClass.NameStatic)
                        syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("参数类型必须是" + FloatClass.NameStatic, factor));
                }

                if (MethodName == MethodNameRound)
                {
                    if (nIndex == 0 && strName != FloatClass.NameStatic && strName != DecimalClass.NameStatic)
                        syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("第一个参数类型必须是" + FloatClass.NameStatic + "或" + DecimalClass.NameStatic, this));

                    if (nIndex == 1 && strName != IntegerClass.NameStatic)
                        syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("第二个参数类型必须是" + IntegerClass.NameStatic, this));
                }

                if (MethodName == MethodNameContinuousProduct || MethodName == MethodNameSum)
                {
                    if ((nIndex == 0 || nIndex == 1) && strName != IntegerClass.NameStatic)
                    {
                        if (nIndex == 0)
                            syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("第一个参数类型必须是" + IntegerClass.NameStatic, this));

                        else
                            syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("第二个参数类型必须是" + IntegerClass.NameStatic, this));
                    }

                    if (nIndex == 2 && strName != IntegerClass.NameStatic + "[]" && strName != FloatClass.NameStatic + "[]" && strName != DecimalClass.NameStatic + "[]")
                        syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("第三个参数类型必须是" + IntegerClass.NameStatic + "[]" + "、" + FloatClass.NameStatic + "[]" + "或" + DecimalClass.NameStatic + "[]", factor));
                }
                //if (MethodName == MethodNameSum)
                //{
                //    if (strName != IntegerClass.NameStatic + "[]" && strName != FloatClass.NameStatic + "[]" && strName != DecimalClass.NameStatic + "[]")
                //        syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("参数类型必须是" + IntegerClass.NameStatic + "[]" + "、" + FloatClass.NameStatic + "[]" + "或" + DecimalClass.NameStatic + "[]", factor));
                //}

                if (MethodName == MethodNameAverage)
                {
                    if (strName != FloatClass.NameStatic + "[]" && strName != DecimalClass.NameStatic + "[]")
                        syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("参数类型必须是" + FloatClass.NameStatic + "[]" + "或" + DecimalClass.NameStatic + "[]", factor));
                }

                if (MethodName == MethodNameLarge || MethodName == MethodNameSmall)
                {
                    if (nIndex == 0 && strName != FloatClass.NameStatic + "[]" && strName != DecimalClass.NameStatic + "[]")
                        syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("第一个参数类型必须是" + FloatClass.NameStatic + "[]" + "或" + DecimalClass.NameStatic + "[]", factor));

                    if (nIndex == 1 && strName != IntegerClass.NameStatic)
                        syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("第二个参数类型必须是" + IntegerClass.NameStatic, factor));
                }
            }

            return strParseName == null ? null : strParseName.IndexOf('[') != -1 ? strParseName.Substring(0, strParseName.Length - 2) : strParseName;
        }

        private class MathUtil
        {
            private MathUtil() { }

            public static int Sum(int[] nParam)
            {
                int nSum = 0;

                foreach (int nValue in nParam)
                    nSum += nValue;

                return nSum;
            }
            public static int Sum(int[] nParam, int nStart, int nEnd)
            {
                int nSum = 0;

                for (int nIndex = nStart; nIndex < nEnd; nIndex++)
                    nSum += nParam[nIndex];

                return nSum;
            }
            public static double Average(double[] dParam)
            {
                return Sum(dParam) / dParam.Length;
            }
            public static decimal Average(decimal[] decParam)
            {
                return Sum(decParam) / decParam.Length;
            }
            public static double Sum(double[] dParam)
            {
                double dSum = 0.0;

                foreach (double dValue in dParam)
                    dSum += dValue;

                return dSum;
            }
            public static double Sum(double[] dParam, int nStart, int nEnd)
            {
                double dSum = 0.0;

                for (int nIndex = nStart; nIndex < nEnd; nIndex++)
                    dSum += dParam[nIndex];

                return dSum;
            }
            public static decimal Sum(decimal[] decParam)
            {
                decimal decSum = 0;

                foreach (decimal decValue in decParam)
                    decSum += decValue;

                return decSum;
            }
            public static decimal Sum(decimal[] decParam, int nStart, int nEnd)
            {
                decimal decSum = 0;

                for (int nIndex = nStart; nIndex < nEnd; nIndex++)
                    decSum += decParam[nIndex];

                return decSum;
            }
            public static int ContinuousProduct(int[] nParam, int nStart, int nEnd)
            {
                int nProduct = 1;

                for (int nIndex = nStart; nIndex < nEnd; nIndex++)
                    nProduct *= nParam[nIndex];

                return nProduct;
            }
            public static double ContinuousProduct(double[] dParam, int nStart, int nEnd)
            {
                double dProduct = 0;

                for (int nIndex = nStart; nIndex < nEnd; nIndex++)
                    dProduct *= dParam[nIndex];

                return dProduct;
            }
            public static decimal ContinuousProduct(decimal[] decParam, int nStart, int nEnd)
            {
                decimal decProduct = 1M;

                for (int nIndex = nStart; nIndex < nEnd; nIndex++)
                    decProduct *= decParam[nIndex];

                return decProduct;
            }
            public static int Max(int[] nParam)
            {
                int nMax = nParam[0];

                foreach (int nValue in nParam)
                {
                    if (nMax < nValue)
                        nMax = nValue;
                }

                return nMax;
            }
            public static double Max(double[] dParam)
            {
                double dMax = dParam[0];

                foreach (double dValue in dParam)
                {
                    if (dMax < dValue)
                        dMax = dValue;
                }

                return dMax;
            }
            public static decimal Max(decimal[] decParam)
            {
                decimal decMax = decParam[0];

                foreach (decimal decValue in decParam)
                {
                    if (decMax < decValue)
                        decMax = decValue;
                }

                return decMax;
            }
            public static int Min(int[] nParam)
            {
                int nMin = nParam[0];

                foreach (int nValue in nParam)
                {
                    if (nMin > nValue)
                        nMin = nValue;
                }

                return nMin;
            }
            public static double Min(double[] dParam)
            {
                double dMin = dParam[0];

                foreach (double dValue in dParam)
                {
                    if (dMin > dValue)
                        dMin = dValue;
                }

                return dMin;
            }
            public static decimal Min(decimal[] decParam)
            {
                decimal decMin = decParam[0];

                foreach (decimal decValue in decParam)
                {
                    if (decMin > decValue)
                        decMin = decValue;
                }

                return decMin;
            }
            public static double Large(double[] dParam, int nExpect)
            {
                double[] dResult = RemoveDuplicateAndSort(dParam);
                return dResult[dResult.Length - nExpect];
            }
            public static decimal Large(decimal[] decParam, int nExpect)
            {
                decimal[] dResult = RemoveDuplicateAndSort(decParam);
                return dResult[dResult.Length - nExpect];
            }
            public static double Small(double[] dParam, int nExpect)
            {
                double[] dResult = RemoveDuplicateAndSort(dParam);
                return dResult[nExpect - 1];
            }
            public static decimal Small(decimal[] decParam, int nExpect)
            {
                decimal[] dResult = RemoveDuplicateAndSort(decParam);
                return dResult[nExpect - 1];
            }
            private static double[] RemoveDuplicateAndSort(double[] objParam)
            {
                if (objParam != null && objParam.Length > 0)
                {
                    SortedList sortlist = new SortedList();

                    foreach (double obj in objParam)
                        sortlist.Add(obj, obj);

                    IDictionaryEnumerator idictionaryenumerator = sortlist.GetEnumerator();
                    double[] objResult = new double[sortlist.Count];
                    int nIndex = 0;
                    while (idictionaryenumerator.MoveNext())
                        objResult[nIndex++] = (double)idictionaryenumerator.Key;

                    return objResult;
                }

                return null;
            }
            private static decimal[] RemoveDuplicateAndSort(decimal[] objParam)
            {
                if (objParam != null && objParam.Length > 0)
                {
                    SortedList sortlist = new SortedList();

                    foreach (decimal obj in objParam)
                        sortlist.Add(obj, obj);

                    IDictionaryEnumerator idictionaryenumerator = sortlist.GetEnumerator();
                    decimal[] objResult = new decimal[sortlist.Count];
                    int nIndex = 0;
                    while (idictionaryenumerator.MoveNext())
                        objResult[nIndex++] = (decimal)idictionaryenumerator.Key;

                    return objResult;
                }

                return null;
            }
        }
    }
}
