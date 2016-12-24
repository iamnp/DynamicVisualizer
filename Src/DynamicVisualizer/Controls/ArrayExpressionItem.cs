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
            textBox2.KeyPress += ValueTextBoxKeyPress;
            textBox2.GotFocus += ValueTextBoxGotFocus;
            textBox2.LostFocus += ValueTextBoxLostFocus;
            textBox2.Focus();
            textBox2.AllowDrop = true;
            textBox2.DragEnter += OnTextBoxDragEnter;
            textBox2.DragDrop += OnTextBoxDragDrop;
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
                if (_definedAsConstVector)
                {
                    textBox2.Text = Expr.ExprStrings();
                }
                else
                {
                    textBox2.Text = Expr.ExprString;
                }
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
                        if ((ArrayExpressionEditor.Len == -1) || textBox2.Text.Contains(";"))
                        {
                            _definedAsConstVector = true;
                            var items = textBox2.Text.Split(';');
                            for (var i = 0; i < items.Length; ++i)
                            {
                                items[i] = items[i].Trim();
                            }
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
                            {
                                items[i] = items[i].Trim();
                            }
                            Expr.SetRawExpressions(items);
                        }
                        else
                        {
                            Expr.SetRawExpression(textBox2.Text);
                        }
                        textBox2.Text = Expr.CachedValue.Str;
                        textBox1.Focus();
                        StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex);
                        Form1.RedrawNeeded?.Invoke();
                    }
                }
            }
        }
    }
}