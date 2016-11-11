using System;
using System.Windows.Forms;
using DynamicVisualizer.Logic.Storyboard.Steps;
using DynamicVisualizer.Logic.Storyboard.Steps.Draw;

namespace DynamicVisualizer
{
    public partial class StepEditor : UserControl
    {
        private bool _ignoreTextChanged;
        private Step _step;

        public StepEditor()
        {
            InitializeComponent();
        }

        public event Action RedrawNeeded;

        public void Redraw()
        {
            ShowStep(_step);
        }

        public void ShowStep(Step step)
        {
            _ignoreTextChanged = true;
            _step = step;
            if (step is DrawStep)
            {
                var ds = (DrawStep) step;
                if (ds.StepType == DrawStep.DrawStepType.DrawRect)
                {
                    var drs = (DrawRectStep) ds;
                    label1.Text = "X";
                    label2.Text = "Y";
                    label3.Text = "Width";
                    label4.Text = "Height";

                    textBox1.Text = drs.X;
                    textBox2.Text = drs.Y;
                    textBox3.Text = drs.Width;
                    textBox4.Text = drs.Height;

                    label4.Visible = true;
                    textBox4.Visible = true;
                }
                else if (ds.StepType == DrawStep.DrawStepType.DrawCircle)
                {
                    var drs = (DrawCircleStep) ds;
                    label1.Text = "X";
                    label2.Text = "Y";
                    label3.Text = "Radius";
                    label4.Visible = false;

                    textBox1.Text = drs.X;
                    textBox2.Text = drs.Y;
                    textBox3.Text = drs.Radius;
                    textBox4.Visible = false;
                }
            }
            _ignoreTextChanged = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreTextChanged) return;
            if (_step is DrawStep)
            {
                var ds = (DrawStep) _step;
                if (ds.StepType == DrawStep.DrawStepType.DrawRect)
                    ((DrawRectStep) ds).ReInitX(textBox1.Text);
                else if (ds.StepType == DrawStep.DrawStepType.DrawCircle)
                    ((DrawCircleStep) ds).ReInitX(textBox1.Text);
            }
            RedrawNeeded?.Invoke();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreTextChanged) return;
            if (_step is DrawStep)
            {
                var ds = (DrawStep) _step;
                if (ds.StepType == DrawStep.DrawStepType.DrawRect)
                    ((DrawRectStep) ds).ReInitY(textBox2.Text);
                else if (ds.StepType == DrawStep.DrawStepType.DrawCircle)
                    ((DrawCircleStep) ds).ReInitY(textBox2.Text);
            }
            RedrawNeeded?.Invoke();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreTextChanged) return;
            if (_step is DrawStep)
            {
                var ds = (DrawStep) _step;
                if (ds.StepType == DrawStep.DrawStepType.DrawRect)
                    ((DrawRectStep) ds).ReInitWidth(textBox3.Text);
                else if (ds.StepType == DrawStep.DrawStepType.DrawCircle)
                    ((DrawCircleStep) ds).ReInit(textBox3.Text);
            }
            RedrawNeeded?.Invoke();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreTextChanged) return;
            if (_step is DrawStep)
            {
                var ds = (DrawStep) _step;
                if (ds.StepType == DrawStep.DrawStepType.DrawRect)
                    ((DrawRectStep) ds).ReInitHeight(textBox4.Text);
            }
            RedrawNeeded?.Invoke();
        }
    }
}