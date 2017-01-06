using System;
using System.IO;
using System.Windows.Forms;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Steps;

namespace DynamicVisualizer.Controls
{
    public partial class ArrayExpressionItem : UserControl
    {
        public const int ItemHeight = 24;
        private bool _definedAsConstVector;
        private bool _ignoreTextChange;
        private bool _mouseOver;
        public ArrayExpression Expr;

        public ArrayExpressionItem()
        {
            InitializeComponent();
            textBox2.Visible = false;
        }

        private void OnTextBoxDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                if ((files.Length == 1) && files[0].EndsWith(".csv"))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        public void SetDataFromFile(string data, string filename = null)
        {
            if (filename != null)
            {
                textBox1.Text = filename;
            }

            _definedAsConstVector = true;
            var items = data.Split(';');
            for (var i = 0; i < items.Length; ++i)
            {
                items[i] = items[i].Trim();
            }
            ArrayExpressionEditor.Len = items.Length;
            Expr = DataStorage.Add(new ArrayExpression("data", textBox1.Text, items));
            Expr.ValueChanged += ExprValueChanged;

            textBox2.Text = Expr.CachedValue.Str;
            textBox1.Focus();
        }

        private void OnTextBoxDragDrop(object sender, DragEventArgs e)
        {
            var file = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
            var data = File.ReadAllLines(file)[0];
            SetDataFromFile(data);
        }

        public void MakeNotDummy()
        {
            textBox2.Visible = true;
            textBox1.ReadOnly = true;
            textBox2.TextChanged += ValueTextBoxTextChanged;
            textBox2.GotFocus += ValueTextBoxGotFocus;
            textBox2.LostFocus += ValueTextBoxLostFocus;
            textBox2.MouseEnter += ValueTextBoxMouseEnter;
            textBox2.MouseLeave += ValueTextBoxMouseLeave;
            textBox2.Focus();
            textBox2.AllowDrop = true;
            textBox2.DragEnter += OnTextBoxDragEnter;
            textBox2.DragDrop += OnTextBoxDragDrop;
        }

        private void ValueTextBoxTextChanged(object sender, EventArgs eventArgs)
        {
            if (!_ignoreTextChange && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                if (Expr == null)
                {
                    if ((ArrayExpressionEditor.Len == -1) || textBox2.Text.Contains(";"))
                    {
                        _definedAsConstVector = true;
                        var items = textBox2.Text.Split(';');
                        for (var i = 0; i < items.Length; ++i)
                        {
                            items[i] = items[i].Trim();
                        }
                        Expr = DataStorage.Add(new ArrayExpression("data", textBox1.Text, items));
                    }
                    else
                    {
                        Expr =
                            DataStorage.Add(new ArrayExpression("data", textBox1.Text, textBox2.Text,
                                ArrayExpressionEditor.Len));
                    }
                    Expr.ValueChanged += ExprValueChanged;
                }
                else
                {
                    if (_definedAsConstVector)
                    {
                        var items = textBox2.Text.Split(';');
                        for (var i = 0; i < items.Length; ++i)
                        {
                            items[i] = items[i].Trim();
                        }
                        Expr.SetRawExpressions(items);
                    }
                    else
                    {
                        Expr.SetRawExpression(textBox2.Text);
                    }
                }
                ArrayExpressionEditor.Len = Expr.Exprs.Length;
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
                if (_definedAsConstVector)
                {
                    textBox2.Text = Expr.ExprStrings();
                }
                else
                {
                    textBox2.Text = Expr.ExprString;
                }
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
                if (_definedAsConstVector)
                {
                    textBox2.Text = Expr.ExprStrings();
                }
                else
                {
                    textBox2.Text = Expr.ExprString;
                }
                _ignoreTextChange = false;
            }
        }
    }
}