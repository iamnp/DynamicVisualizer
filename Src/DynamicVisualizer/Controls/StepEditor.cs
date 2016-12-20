using System;
using System.Windows.Forms;
using DynamicVisualizer.Steps;
using DynamicVisualizer.Steps.Draw;
using DynamicVisualizer.Steps.Move;
using DynamicVisualizer.Steps.Resize;
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
            ShowFirst(0);
        }

        public void Redraw()
        {
            ShowStep(_step);
        }

        private void ShowFirst(int n)
        {
            if (n == 0)
            {
                label1.Visible = false;
                textBox1.Visible = false;
                label2.Visible = false;
                textBox2.Visible = false;
                label3.Visible = false;
                textBox3.Visible = false;
                label4.Visible = false;
                textBox4.Visible = false;
            }
            else if (n == 1)
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
                else if (ds.StepType == DrawStep.DrawStepType.DrawLine)
                {
                    var dls = (DrawLineStep) ds;
                    label1.Text = "X";
                    label2.Text = "Y";
                    label3.Text = "Width";
                    label4.Text = "Height";

                    textBox1.Text = dls.X;
                    textBox2.Text = dls.Y;
                    textBox3.Text = dls.Width;
                    textBox4.Text = dls.Height;

                    ShowFirst(4);
                }
            }
            else if (step is MoveStep)
            {
                var ms = (MoveStep) step;
                if (ms.StepType == MoveStep.MoveStepType.MoveRect)
                {
                    var mrs = (MoveRectStep) ms;
                    label1.Text = "X";
                    label2.Text = "Y";

                    textBox1.Text = mrs.X;
                    textBox2.Text = mrs.Y;

                    ShowFirst(2);
                }
                if (ms.StepType == MoveStep.MoveStepType.MoveEllipse)
                {
                    var mes = (MoveEllipseStep) ms;
                    label1.Text = "X";
                    label2.Text = "Y";

                    textBox1.Text = mes.X;
                    textBox2.Text = mes.Y;

                    ShowFirst(2);
                }
                if (ms.StepType == MoveStep.MoveStepType.MoveLine)
                {
                    var mls = (MoveLineStep) ms;
                    label1.Text = "X";
                    label2.Text = "Y";

                    textBox1.Text = mls.X;
                    textBox2.Text = mls.Y;

                    ShowFirst(2);
                }
            }
            else if (step is ScaleStep)
            {
                var ss = (ScaleStep) step;
                if (ss.StepType == ScaleStep.ScaleStepType.ScaleRect)
                {
                    var srs = (ScaleRectStep) ss;
                    label1.Text = "Factor";

                    textBox1.Text = srs.Factor;

                    ShowFirst(1);
                }
                if (ss.StepType == ScaleStep.ScaleStepType.ScaleEllipse)
                {
                    var ses = (ScaleEllipseStep) ss;
                    label1.Text = "Factor";

                    textBox1.Text = ses.Factor;

                    ShowFirst(1);
                }
                if (ss.StepType == ScaleStep.ScaleStepType.ScaleLine)
                {
                    var sls = (ScaleLineStep) ss;
                    label1.Text = "Factor";

                    textBox1.Text = sls.Factor;

                    ShowFirst(1);
                }
            }
            else if (step is ResizeStep)
            {
                var rs = (ResizeStep) step;
                if (rs.StepType == ResizeStep.ResizeStepType.ResizeRect)
                {
                    var rrs = (ResizeRectStep) rs;
                    label1.Text = "Delta";

                    textBox1.Text = rrs.Delta;

                    ShowFirst(1);
                }
                if (rs.StepType == ResizeStep.ResizeStepType.ResizeEllipse)
                {
                    var res = (ResizeEllipseStep) rs;
                    label1.Text = "Delta";

                    textBox1.Text = res.Delta;

                    ShowFirst(1);
                }
            }
            else
            {
                ShowFirst(0);
            }
            _ignoreTextChanged = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreTextChanged)
            {
                return;
            }
            if (_step is DrawStep)
            {
                var ds = (DrawStep) _step;
                if (ds.StepType == DrawStep.DrawStepType.DrawRect)
                {
                    ((DrawRectStep) ds).ReInitX(textBox1.Text);
                }
                else if (ds.StepType == DrawStep.DrawStepType.DrawEllipse)
                {
                    ((DrawEllipseStep) ds).ReInitX(textBox1.Text);
                }
                else if (ds.StepType == DrawStep.DrawStepType.DrawLine)
                {
                    ((DrawLineStep) ds).ReInitX(textBox1.Text);
                }
            }
            else if (_step is MoveStep)
            {
                var ms = (MoveStep) _step;
                if (ms.StepType == MoveStep.MoveStepType.MoveRect)
                {
                    ((MoveRectStep) ms).MoveX(textBox1.Text);
                }
                else if (ms.StepType == MoveStep.MoveStepType.MoveEllipse)
                {
                    ((MoveEllipseStep) ms).MoveX(textBox1.Text);
                }
                else if (ms.StepType == MoveStep.MoveStepType.MoveLine)
                {
                    ((MoveLineStep) ms).MoveX(textBox1.Text);
                }
            }
            else if (_step is ScaleStep)
            {
                var ss = (ScaleStep) _step;
                if (ss.StepType == ScaleStep.ScaleStepType.ScaleRect)
                {
                    ((ScaleRectStep) ss).Scale(textBox1.Text);
                }
                else if (ss.StepType == ScaleStep.ScaleStepType.ScaleEllipse)
                {
                    ((ScaleLineStep) ss).Scale(textBox1.Text);
                }
                else if (ss.StepType == ScaleStep.ScaleStepType.ScaleLine)
                {
                    ((ScaleEllipseStep) ss).Scale(textBox1.Text);
                }
            }
            else if (_step is ResizeStep)
            {
                var rs = (ResizeStep) _step;
                if (rs.StepType == ResizeStep.ResizeStepType.ResizeRect)
                {
                    ((ResizeRectStep) rs).Resize(textBox1.Text);
                }
                else if (rs.StepType == ResizeStep.ResizeStepType.ResizeEllipse)
                {
                    ((ResizeEllipseStep) rs).Resize(textBox1.Text);
                }
            }
            Form1.RedrawNeeded?.Invoke();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreTextChanged)
            {
                return;
            }
            if (_step is DrawStep)
            {
                var ds = (DrawStep) _step;
                if (ds.StepType == DrawStep.DrawStepType.DrawRect)
                {
                    ((DrawRectStep) ds).ReInitY(textBox2.Text);
                }
                else if (ds.StepType == DrawStep.DrawStepType.DrawEllipse)
                {
                    ((DrawEllipseStep) ds).ReInitY(textBox2.Text);
                }
                else if (ds.StepType == DrawStep.DrawStepType.DrawLine)
                {
                    ((DrawLineStep) ds).ReInitY(textBox2.Text);
                }
            }
            else if (_step is MoveStep)
            {
                var ms = (MoveStep) _step;
                if (ms.StepType == MoveStep.MoveStepType.MoveRect)
                {
                    ((MoveRectStep) ms).MoveY(textBox2.Text);
                }
                else if (ms.StepType == MoveStep.MoveStepType.MoveEllipse)
                {
                    ((MoveEllipseStep) ms).MoveY(textBox2.Text);
                }
                else if (ms.StepType == MoveStep.MoveStepType.MoveLine)
                {
                    ((MoveLineStep) ms).MoveY(textBox2.Text);
                }
            }
            Form1.RedrawNeeded?.Invoke();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreTextChanged)
            {
                return;
            }
            if (_step is DrawStep)
            {
                var ds = (DrawStep) _step;
                if (ds.StepType == DrawStep.DrawStepType.DrawRect)
                {
                    ((DrawRectStep) ds).ReInitWidth(textBox3.Text);
                }
                else if (ds.StepType == DrawStep.DrawStepType.DrawEllipse)
                {
                    ((DrawEllipseStep) ds).ReInit(textBox3.Text);
                }
                else if (ds.StepType == DrawStep.DrawStepType.DrawLine)
                {
                    ((DrawLineStep) ds).ReInitWidth(textBox3.Text);
                }
            }
            Form1.RedrawNeeded?.Invoke();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreTextChanged)
            {
                return;
            }
            if (_step is DrawStep)
            {
                var ds = (DrawStep) _step;
                if (ds.StepType == DrawStep.DrawStepType.DrawRect)
                {
                    ((DrawRectStep) ds).ReInitHeight(textBox4.Text);
                }
                else if (ds.StepType == DrawStep.DrawStepType.DrawLine)
                {
                    ((DrawLineStep) ds).ReInitHeight(textBox4.Text);
                }
            }
            Form1.RedrawNeeded?.Invoke();
        }
    }
}