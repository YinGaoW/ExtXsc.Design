using Eap;
namespace ExtEap
{
    public class Evaluate : Factor
    {
        private String m_stringClassName;
        private Refer m_refer;
        private ComponentContainerDecorator m_componentcontainerdecoratorFactor;

        public Evaluate(Eap.Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }

        public Refer Refer
        {
            get { return m_refer; }
        }
        public Factor Factor
        {
            get { return (Factor)m_componentcontainerdecoratorFactor.Component; }
            set { m_componentcontainerdecoratorFactor.Component = value; }
        }

        private void OnConstruct()
        {
            SetElement("ClassName", m_stringClassName = new String());
            SetComponent("Refer", m_refer = new Refer(OwnerStatement));
            m_componentcontainerdecoratorFactor = new ComponentContainerDecorator(this, typeof(Factor), new object[] { OwnerStatement });
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_componentcontainerdecoratorFactor.Initialize();

            OnConstruct();
        }

        private decimal GetNumericObjectValue(Object objectNumeric)
        {
            if (objectNumeric is InterruptionObject)
                return ((IntegerObject)objectNumeric).Value;

            if (objectNumeric is DecimalObject)
                return ((DecimalObject)objectNumeric).Value;

            return (decimal)((FloatObject)objectNumeric).Value;
        }

        protected override Object OnExecute(AdmlObject admlobject, ExecutionStack executionstack)
        {
            Object objectFactor = Factor.Execute(admlobject, executionstack);

            if (m_stringClassName.Value == IntegerClass.NameStatic && objectFactor.ClassName != IntegerClass.NameStatic)
                objectFactor = new IntegerObject(ClassLibrary, (int)GetNumericObjectValue(objectFactor));

            else if (m_stringClassName.Value == FloatClass.NameStatic && objectFactor.ClassName != FloatClass.NameStatic)
                objectFactor = new FloatObject(ClassLibrary, (float)GetNumericObjectValue(objectFactor));

            else if (m_stringClassName.Value == DecimalClass .NameStatic && objectFactor.ClassName != DecimalClass.NameStatic)
                objectFactor = new DecimalObject(ClassLibrary, GetNumericObjectValue(objectFactor));

            return Refer.Execute(admlobject, executionstack);
        }

        protected override string OnParse(SyntaxErrorList syntaxerrorlist)
        {
            string strClassName = Refer.Parse(syntaxerrorlist);

            m_stringClassName.Value = strClassName;

            if (strClassName == null)
                return null;

            if (Factor == null)
                syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("源因子不能为空", this));

            else
            {
                Class classFactor = ClassLibrary.GetClass(AdmlClass, Factor.Parse(syntaxerrorlist));

                if (classFactor == null)
                    syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("源因子类型不确定", Factor));

                else if (strClassName != string.Empty && classFactor.Name != string.Empty)
                {
                    if (strClassName != IntegerClass.NameStatic || strClassName != FloatClass.NameStatic || strClassName != DecimalClass.NameStatic)
                    {
                        if (classFactor.Name != IntegerClass.NameStatic && classFactor.Name != FloatClass.NameStatic && classFactor.Name != DecimalClass.NameStatic)
                            syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("目标因子为数值类型，则源因子也必须为数值类型", Factor));
                    }
                    else if (!classFactor.IsDerivedClass(strClassName))
                        syntaxerrorlist.SetSyntaxError(new FactorSyntaxError("源因子和目标因子类型不一致", Factor));
                }
            }

            return strClassName;
        }
    }
}
