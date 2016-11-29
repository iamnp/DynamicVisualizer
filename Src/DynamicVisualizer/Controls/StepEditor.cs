using System;
using System.Windows.Forms;
using DynamicVisualizer.Steps;
using DynamicVisualizer.Steps.Draw;
using DynamicVisualizer.Steps.Move;
using DynamicVisualizer.Steps.Scale;

namespace DynamicVisualizer.Controls
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

        private void ShowFirst(int n)
        {
            if (n == 1)
            {
                label1.Visible = true;
                textBox1.Visible = true;

                label2.Visible = false;
                textBox2.Visible = false;
                label3.Visible = false;
                textBox3.Visible = false;
                label4.Visible = false;
                textBox4.Visible = false;
            }
            else if (n == 2)
            {
                label1.Visible = true;
                textBox1.Visible = true;
                label2.Visible = true;
                textBox2.Visible = true;

                label3.Visible = false;
                textBox3.Visible = false;
                label4.Visible = false;
                textBox4.Visible = false;
            }
            else if (n == 3)
            {
                label1.Visible = true;
                textBox1.Visible = true;
                label2.Visible = true;
                textBox2.Visible = true;
                label3.Visible = true;
                textBox3.Visible = true;

                label4.Visible = false;
                textBox4.Visible = false;
            }
            else if (n == 4)
            {
                label1.Visible = true;
                textBox1.Visible = true;
                label2.Visible = true;
                textBox2.Visible = true;
                label3.Visible = true;
                textBox3.Visible = true;
                label4.Visible = true;
                textBox4.Visible = true;
            }
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

                    ShowFirst(4);
                }
                else if (ds.StepType == DrawStep.DrawStepType.DrawEllipse)
                {
                    var drs = (DrawEllipseStep) ds;
                    label1.Text = "X";
                    label2.Text = "Y";
                    label3.Text = "Radius";

                    textBox1.Text = drs.X;
                    textBox2.Text = drs.Y;
                    textBox3.Text = drs.Radius;

                    ShowFirst(3);
                }
            }
            else if (step is TransformStep)
            {
                var ts = (TransformStep) step;
                if (ts.StepType == TransformStep.TransformStepType.MoveRect)
                {
                    var mrs = (MoveRectStep) ts;
                    label1.Text = "X";
                    label2.Text = "Y";

                    textBox1.Text = mrs.X;
                    textBox2.Text = mrs.Y;

                    ShowFirst(2);
                }
                else if (ts.StepType == TransformStep.TransformStepType.MoveEllipse)
                {
                    var mes = (MoveEllipseStep) ts;
                    label1.Text = "X";
                    label2.Text = "Y";

                    textBox1.Text = mes.X;
                    textBox2.Text = mes.Y;

                    ShowFirst(2);
                }
                else if (ts.StepType == TransformStep.TransformStepType.ScaleRect)
                {
                    var srs = (ScaleRectStep) ts;
                    label1.Text = "Factor";

                    textBox1.Text = srs.Factor;

                    ShowFirst(1);
                }
                else if (ts.StepType == TransformStep.TransformStepType.ScaleEllipse)
                {
                    var ses = (ScaleEllipseStep) ts;
                    label1.Text = "Factor";

                    textBox1.Text = ses.Factor;

                    ShowFirst(1);
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
                else if (ds.StepType == DrawStep.DrawStepType.DrawEllipse)
                    ((DrawEllipseStep) ds).ReInitX(textBox1.Text);
            }
            else if (_step is TransformStep)
            {
                var ts = (TransformStep) _step;
                if (ts.StepType == TransformStep.TransformStepType.MoveRect)
                    ((MoveRectStep) ts).MoveX(textBox1.Text);
                else if (ts.StepType == TransformStep.TransformStepType.MoveEllipse)
                    ((MoveEllipseStep) ts).MoveX(textBox1.Text);
                else if (ts.StepType == TransformStep.TransformStepType.ScaleRect)
                    ((ScaleRectStep) ts).Scale(textBox1.Text);
                else if (ts.StepType == TransformStep.TransformStepType.ScaleEllipse)
                    ((ScaleEllipseStep) ts).Scale(textBox1.Text);
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
                else if (ds.StepType == DrawStep.DrawStepType.DrawEllipse)
                    ((DrawEllipseStep) ds).ReInitY(textBox2.Text);
            }
            else if (_step is TransformStep)
            {
                var ts = (TransformStep) _step;
                if (ts.StepType == TransformStep.TransformStepType.MoveRect)
                    ((MoveRectStep) ts).MoveY(textBox2.Text);
                else if (ts.StepType == TransformStep.TransformStepType.MoveEllipse)
                    ((MoveEllipseStep) ts).MoveY(textBox2.Text);
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
                else if (ds.StepType == DrawStep.DrawStepType.DrawEllipse)
                    ((DrawEllipseStep) ds).ReInit(textBox3.Text);
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