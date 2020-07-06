using System;
using System.Drawing;
using System.Windows.Forms;
using Eap;

namespace ExtEap
{
    public class FloatInitializationDesigner : InitializationDesigner
    {
        private System.Drawing.Font m_fontCustomized;
        public FloatInitializationDesigner(ComponentDesignDocumentView componentdesigndocumentview, FloatInitialization floatinitialization) : base(componentdesigndocumentview, floatinitialization)
        {
        }
        public FloatInitializationDesigner(ComponentDesignDocumentView componentdesigndocumentview, ComponentDesignSpace componentdesignspace,FloatInitialization floatinitialization) : base(componentdesigndocumentview, componentdesignspace,floatinitialization)
        {
        }

        public FloatInitialization FloatInitialization
        {
            get { return (FloatInitialization)Component; }
        }
        public System.Drawing.Font FontCustomized
        {
            get { return m_fontCustomized; }
            set { m_fontCustomized = value; }
        }

        public override void Arrange(System.Drawing.Graphics graphics)
        {
            FloatInitialization.IsValid = true;

            System.Drawing.Size size = FontCustomized == null ? MeasureString(graphics, FloatInitialization.Value.ToString()) : Eap.Graphics.MeasureString(graphics, FloatInitialization.Value.ToString(), FontCustomized);
            size.Width += 2;

            Size = size;
        }
        public override void Render(System.Drawing.Graphics graphics)
        {
            if (FontCustomized == null)
                DrawString(graphics, FloatInitialization.Value.ToString(), Brushes.DarkRed, Left + 1, Top);

            else
                Eap.Graphics.DrawString(graphics, FloatInitialization.Value.ToString(), FontCustomized, Brushes.DarkRed, Left + 1, Top);
        }

        private void OnEditorTextChanged(object objSender, string strText)
        {
            ComponentDesignDocument.BeforeUpdate(FloatInitialization, "*");
            FloatInitialization.Value = double.Parse(strText);
            ComponentDesignDocument.AfterUpdate(FloatInitialization, "*");
        }
        public override void OnKeyDown(object objSender, KeyEventArgs keyeventargs)
        {
            if (keyeventargs.KeyData == Keys.Enter && AllowEdit)
            {
                Editor editor = new Editor();
                editor.Font = FontCustomized == null ? WorkbenchFramework.InstanceWorkbenchFramework.Font : FontCustomized;
                editor.Owner = WorkbenchFramework.InstanceWorkbenchFramework;
                editor.Width = Width + 2;
                editor.InputType = Editor.InputTypeFloat;
                editor.Text = FloatInitialization.Value.ToString();
                editor.TextChanged += new HandleTextChanged(OnEditorTextChanged);
                editor.Show(AdmlClassDesignerHelper.GetPoint(this, Left, Top));

                keyeventargs.Handled = true;
            }

            else
                base.OnKeyDown(objSender, keyeventargs);
        }
    }
}
