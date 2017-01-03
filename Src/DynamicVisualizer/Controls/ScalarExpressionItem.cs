using System;
using System.Windows.Forms;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Steps;

namespace DynamicVisualizer.Controls
{
    public partial class ScalarExpressionItem : UserControl
    {
        public const int ItemHeight = 24;
        public ScalarExpression Expr;

        public ScalarExpressionItem(bool isDummy)
        {
            InitializeComponent();
            if (isDummy)
            {
                textBox2.Visible = false;
            }
        }

        public static event EventHandler ScalarExprEdited;

        public void MakeNotDummy()
        {
            textBox2.Visible = true;
            textBox1.ReadOnly = true;
            textBox2.KeyPress += ValueTextBoxKeyPress;
            textBox2.GotFocus += ValueTextBoxGotFocus;
            textBox2.LostFocus += ValueTextBoxLostFocus;
            textBox2.Focus();
        }

        private void ValueTextBoxLostFocus(object sender, EventArgs eventArgs)
        {
            if (Expr != null)
            {
                textBox2.Text = Expr.CachedValue.Str;
            }
        }

        private void ValueTextBoxGotFocus(object sender, EventArgs eventArgs)
        {
            if (Expr != null)
            {
                textBox2.Text = Expr.ExprString;
            }
        }

        private void ValueTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Return)
            {
                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    if (Expr == null)
                    {
                        Expr = DataStorage.Add(
                            new ScalarExpression("data", textBox1.Text, textBox2.Text));
                        Expr.ValueChanged += ValueTextBoxLostFocus;
                        textBox2.Text = Expr.CachedValue.Str;
                        textBox1.Focus();
                    }
                    else
                    {
                        Expr.SetRawExpression(textBox2.Text);
                        textBox2.Text = Expr.CachedValue.Str;
                        textBox1.Focus();
                        StepManager.RefreshToCurrentStep();
                    }
                    ScalarExprEdited?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}