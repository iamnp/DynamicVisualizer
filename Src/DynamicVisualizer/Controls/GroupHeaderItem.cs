﻿using System;
using System.Windows.Forms;
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
            textBox1.Text = _iterableStepGroup.IterationsExpr.ExprString;
            textBox1.TextChanged += textBox1_TextChanged;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _iterableStepGroup.IterationsExpr.SetRawExpression(textBox1.Text);
            StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex, true);
        }
    }
}