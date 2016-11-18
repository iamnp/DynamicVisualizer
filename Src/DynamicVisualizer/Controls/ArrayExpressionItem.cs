using System;
using System.Windows.Forms;
using DynamicVisualizer.Logic.Expressions;

namespace DynamicVisualizer.Controls
{
    public partial class ArrayExpressionItem : UserControl
    {
        public const int ItemHeight = 24;
        private bool _definedAsConstVector;
        public ArrayExpression Expr;

        public ArrayExpressionItem(bool isDummy)
        {
            InitializeComponent();
            if (isDummy) textBox2.Visible = false;
        }

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
                textBox2.Text = Expr.CachedValue.Str;
        }

        public void SetDataFromFile(string data)
        {
            _definedAsConstVector = true;
            var items = data.Split(';');
            for (var i = 0; i < items.Length; ++i)
                items[i] = items[i].Trim();
            ArrayExpressionEditor.Len = items.Length;
            Expr = DataStorage.Add(new ArrayExpression("data", textBox1.Text, items));

            textBox2.Text = Expr.CachedValue.Str;
            textBox1.Focus();
        }

        private void ValueTextBoxGotFocus(object sender, EventArgs eventArgs)
        {
            if (Expr != null)
                if (_definedAsConstVector)
                    textBox2.Text = Expr.ExprStrings();
                else
                    textBox2.Text = Expr.ExprString;
        }

        private void ValueTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Return)
                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                    if (Expr == null)
                    {
                        if ((ArrayExpressionEditor.Len == -1) || textBox2.Text.Contains(";"))
                        {
                            _definedAsConstVector = true;
                            var items = textBox2.Text.Split(';');
                            for (var i = 0; i < items.Length; ++i)
                                items[i] = items[i].Trim();
                            ArrayExpressionEditor.Len = items.Length;
                            Expr = DataStorage.Add(new ArrayExpression("data", textBox1.Text, items));
                        }
                        else
                        {
                            Expr =
                                DataStorage.Add(new ArrayExpression("data", textBox1.Text, textBox2.Text,
                                    ArrayExpressionEditor.Len));
                        }

                        textBox2.Text = Expr.CachedValue.Str;
                        textBox1.Focus();
                    }
                    else
                    {
                        if (_definedAsConstVector)
                        {
                            var items = textBox2.Text.Split(';');
                            for (var i = 0; i < items.Length; ++i)
                                items[i] = items[i].Trim();
                            Expr.SetRawExpressions(items);
                        }
                        else
                        {
                            Expr.SetRawExpression(textBox2.Text);
                        }
                        textBox2.Text = Expr.CachedValue.Str;
                        textBox1.Focus();
                    }
        }
    }
}