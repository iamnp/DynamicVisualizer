using System;
using System.Windows.Forms;
using DynamicVisualizer.Logic.Expressions;

namespace DynamicVisualizer.Controls
{
    public partial class DataStorageItem : UserControl
    {
        public const int ItemHeight = 24;
        public ScalarExpression _expr;

        public DataStorageItem(bool isDummy)
        {
            InitializeComponent();
            if (isDummy) textBox2.Visible = false;
        }

        public void MakeNotDummy()
        {
            textBox2.Visible = true;
            textBox1.KeyPress += NameTextBoxKeyPress;
            textBox2.KeyPress += ValueTextBoxKeyPress;
            textBox2.GotFocus += ValueTextBoxGotFocus;
            textBox2.LostFocus += ValueTextBoxLostFocus;
            textBox2.Focus();
        }

        private void ValueTextBoxLostFocus(object sender, EventArgs eventArgs)
        {
            if (_expr != null)
                textBox2.Text = _expr.CachedValue.Str;
        }

        private void ValueTextBoxGotFocus(object sender, EventArgs eventArgs)
        {
            if (_expr != null)
                textBox2.Text = _expr.ExprString;
        }

        private void ValueTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Return)
                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                    if (_expr == null)
                    {
                        _expr = DataStorage.Add(
                            new ScalarExpression("data", textBox1.Text, textBox2.Text));
                        textBox2.Text = _expr.CachedValue.Str;
                        textBox1.Focus();
                    }
                    else
                    {
                        _expr.SetRawExpression(textBox2.Text);
                        textBox2.Text = _expr.CachedValue.Str;
                        textBox1.Focus();
                    }
        }

        private void NameTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Return)
                if (_expr != null)
                {
                    // TODO perform renaming
                    //_expr.name = textBox2.Text;
                }
        }
    }
}