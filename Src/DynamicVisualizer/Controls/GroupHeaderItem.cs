using System;
using System.Windows.Forms;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Steps;

namespace DynamicVisualizer.Controls
{
    public partial class GroupHeaderItem : UserControl
    {
        public static int HeightValue = 20;
        private readonly IterableStepGroup _iterableStepGroup;

        public GroupHeaderItem(IterableStepGroup g)
        {
            InitializeComponent();
            _iterableStepGroup = g;
            textBox1.Text = _iterableStepGroup.IterationsExpr;
            textBox1.TextChanged += textBox1_TextChanged;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var expr = new ScalarExpression("a", "a", textBox1.Text);
            if (!expr.CachedValue.Empty)
            {
                _iterableStepGroup.Iterations = (int) expr.CachedValue.AsDouble;
                _iterableStepGroup.IterationsExpr = textBox1.Text;
                StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex);
            }
        }
    }
}