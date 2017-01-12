using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Steps;

namespace DynamicVisualizer.Controls
{
    public partial class ArrayExpressionItem : UserControl
    {
        public const int ItemHeight = 20;
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

        private void OnTextBoxDragDrop(object sender, DragEventArgs e)
        {
            var file = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
            var data = File.ReadAllLines(file)[0];
            SetDataFromFile(data);
        }

        public bool SetDataFromFile(string data, string filename = null)
        {
            if (filename != null)
            {
                textBox1.Text = filename;
            }
            if (!TryChangeData(data))
            {
                textBox1.Text = null;
                return false;
            }
            textBox2.Text = Expr.CachedValue.Str;
            return true;
        }

        private bool TryChangeData(string data)
        {
            _ignoreTextChange = true;
            if (Expr == null)
            {
                if ((ArrayExpressionEditor.Len == -1) || data.Contains(";"))
                {
                    var items = data.Split(';');
                    for (var i = 0; i < items.Length; ++i)
                    {
                        items[i] = items[i].Trim();
                        if (items[i].Length == 0)
                        {
                            _ignoreTextChange = false;
                            return false;
                        }
                    }
                    _definedAsConstVector = true;
                    Expr = DataStorage.Add(new ArrayExpression("data", textBox1.Text, items));
                    if ((ArrayExpressionEditor.Len != -1) &&
                        (items.Length != ArrayExpressionEditor.Len))
                    {
                        ArrayExpressionEditor.Len = items.Length;
                        ArrayExpressionEditor.LenChanged(this);
                    }
                }
                else
                {
                    _definedAsConstVector = false;
                    Expr =
                        DataStorage.Add(new ArrayExpression("data", textBox1.Text, data,
                            ArrayExpressionEditor.Len));
                }
                Expr.ValueChanged += ExprValueChanged;
            }
            else
            {
                if (data.Contains(";"))
                {
                    var items = data.Split(';');
                    for (var i = 0; i < items.Length; ++i)
                    {
                        items[i] = items[i].Trim();
                        if (items[i].Length == 0)
                        {
                            _ignoreTextChange = false;
                            return false;
                        }
                    }
                    _definedAsConstVector = true;
                    Expr.SetRawExpressions(items);
                    if ((ArrayExpressionEditor.Len != -1)
                        && (items.Length != ArrayExpressionEditor.Len))
                    {
                        ArrayExpressionEditor.Len = items.Length;
                        ArrayExpressionEditor.LenChanged(this);
                    }
                }
                else
                {
                    _definedAsConstVector = false;
                    Expr.SetRawExpression(data, ArrayExpressionEditor.Len);
                }
            }
            ArrayExpressionEditor.Len = Expr.Exprs.Length;
            StepManager.RefreshToCurrentStep();
            _ignoreTextChange = false;
            return true;
        }

        public void ApplyNewLen()
        {
            if (_definedAsConstVector)
            {
                var curLen = Expr.Exprs.Length;
                var list = new List<string>();
                if (curLen >= ArrayExpressionEditor.Len)
                {
                    for (var i = 0; i < ArrayExpressionEditor.Len; ++i)
                    {
                        list.Add(Expr.ExprsStrings[i]);
                    }
                }
                else
                {
                    for (var i = 0; i < curLen; ++i)
                    {
                        list.Add(Expr.ExprsStrings[i]);
                    }
                    for (var i = 0; i < ArrayExpressionEditor.Len - curLen; ++i)
                    {
                        list.Add("0");
                    }
                }
                Expr.SetRawExpressions(list.ToArray());
            }
            else
            {
                Expr.SetRawExpression(Expr.ExprString, ArrayExpressionEditor.Len);
            }
        }

        private void ValueTextBoxTextChanged(object sender, EventArgs eventArgs)
        {
            if (!_ignoreTextChange && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                TryChangeData(textBox2.Text);
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

        public void MakeNotDummy()
        {
            textBox2.Visible = true;
            textBox2.TextChanged += ValueTextBoxTextChanged;
            textBox2.GotFocus += ValueTextBoxGotFocus;
            textBox2.LostFocus += ValueTextBoxLostFocus;
            textBox2.MouseEnter += ValueTextBoxMouseEnter;
            textBox2.MouseLeave += ValueTextBoxMouseLeave;
            textBox2.Focus();
            textBox2.AllowDrop = true;
            textBox2.DragEnter += OnTextBoxDragEnter;
            textBox2.DragDrop += OnTextBoxDragDrop;
            textBox1.TextChanged += NameTextBoxTextChanged;
        }

        private void NameTextBoxTextChanged(object sender, EventArgs eventArgs)
        {
            if ((Expr != null) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                DataStorage.Rename(Expr, "data", textBox1.Text);
            }
        }

        private void ArrayExpressionItem_MouseEnter(object sender, EventArgs e)
        {
            BackColor = SystemColors.ControlLight;
            textBox1.BackColor = SystemColors.ControlLight;
            textBox2.BackColor = SystemColors.ControlLight;
        }

        private void ArrayExpressionItem_MouseLeave(object sender, EventArgs e)
        {
            BackColor = SystemColors.Control;
            textBox1.BackColor = SystemColors.Control;
            textBox2.BackColor = SystemColors.Control;
        }
    }
}