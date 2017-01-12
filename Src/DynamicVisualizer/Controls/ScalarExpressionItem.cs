using System;
using System.Drawing;
using System.Windows.Forms;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Steps;

namespace DynamicVisualizer.Controls
{
    public partial class ScalarExpressionItem : UserControl
    {
        public const int ItemHeight = 20;
        private bool _ignoreTextChange;
        private bool _mouseOver;
        public ScalarExpression Expr;

        public ScalarExpressionItem(bool isDummy)
        {
            InitializeComponent();
            if (isDummy)
            {
                textBox2.Visible = false;
            }
        }

        public void MakeNotDummy()
        {
            textBox2.Visible = true;
            textBox2.TextChanged += ValueTextBoxTextChanged;
            textBox2.GotFocus += ValueTextBoxGotFocus;
            textBox2.LostFocus += ValueTextBoxLostFocus;
            textBox2.MouseEnter += ValueTextBoxMouseEnter;
            textBox2.MouseLeave += ValueTextBoxMouseLeave;
            textBox2.Focus();
            textBox1.TextChanged += NameTextBoxTextChanged;
        }

        private void NameTextBoxTextChanged(object sender, EventArgs eventArgs)
        {
            if ((Expr != null) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                DataStorage.Rename(Expr, "data", textBox1.Text);
            }
        }

        private void ValueTextBoxTextChanged(object sender, EventArgs eventArgs)
        {
            if (!_ignoreTextChange && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                if (Expr == null)
                {
                    Expr = DataStorage.Add(new ScalarExpression("data", textBox1.Text, textBox2.Text));
                    Expr.ValueChanged += ExprValueChanged;
                }
                else
                {
                    Expr.SetRawExpression(textBox2.Text);
                }
                StepManager.RefreshToCurrentStep();
            }
        }

        private void ExprValueChanged(object sender, EventArgs eventArgs)
        {
            if (!_mouseOver && !textBox2.Focused)
            {
                _ignoreTextChange = true;
                textBox2.Text = Expr.CachedValue.Str;
                _ignoreTextChange = false;
            }
        }

        private void ValueTextBoxLostFocus(object sender, EventArgs eventArgs)
        {
            if (!_mouseOver && (Expr != null))
            {
                _ignoreTextChange = true;
                textBox2.Text = Expr.CachedValue.Str;
                _ignoreTextChange = false;
            }
        }

        private void ValueTextBoxGotFocus(object sender, EventArgs eventArgs)
        {
            if (!_mouseOver && (Expr != null))
            {
                _ignoreTextChange = true;
                textBox2.Text = Expr.ExprString;
                _ignoreTextChange = false;
            }
        }

        private void ValueTextBoxMouseLeave(object sender, EventArgs eventArgs)
        {
            _mouseOver = false;
            if (!textBox2.Focused && (Expr != null))
            {
                _ignoreTextChange = true;
                textBox2.Text = Expr.CachedValue.Str;
                _ignoreTextChange = false;
            }
        }

        private void ValueTextBoxMouseEnter(object sender, EventArgs eventArgs)
        {
            _mouseOver = true;
            if (!textBox2.Focused && (Expr != null))
            {
                _ignoreTextChange = true;
                textBox2.Text = Expr.ExprString;
                _ignoreTextChange = false;
            }
        }

        private void ScalarExpressionItem_MouseEnter(object sender, EventArgs e)
        {
            BackColor = SystemColors.ControlLight;
            textBox1.BackColor = SystemColors.ControlLight;
            textBox2.BackColor = SystemColors.ControlLight;
        }

        private void ScalarExpressionItem_MouseLeave(object sender, EventArgs e)
        {
            BackColor = SystemColors.Control;
            textBox1.BackColor = SystemColors.Control;
            textBox2.BackColor = SystemColors.Control;
        }
    }
}