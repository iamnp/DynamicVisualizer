using System;
using System.Windows.Forms;
using DynamicVisualizer.Steps;
using DynamicVisualizer.Steps.Draw;
using DynamicVisualizer.Steps.Move;
using DynamicVisualizer.Steps.Resize;
using DynamicVisualizer.Steps.Rotate;
using DynamicVisualizer.Steps.Scale;

namespace DynamicVisualizer.Controls
{
    public partial class StepEditor : UserControl
    {
        private bool _ignoreTextChanged;

        public StepEditor()
        {
            InitializeComponent();
            ShowFirst(0);
        }

        public void Redraw()
        {
            _ignoreTextChanged = true;
            if (StepManager.CurrentStep is DrawStep)
            {
                var ds = (DrawStep) StepManager.CurrentStep;
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
            else if (StepManager.CurrentStep is MoveStep)
            {
                var ms = (MoveStep) StepManager.CurrentStep;
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
            else if (StepManager.CurrentStep is ScaleStep)
            {
                var ss = (ScaleStep) StepManager.CurrentStep;
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
            else if (StepManager.CurrentStep is ResizeStep)
            {
                var rs = (ResizeStep) StepManager.CurrentStep;
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
                if (rs.StepType == ResizeStep.ResizeStepType.ResizeLine)
                {
                    var rls = (ResizeLineStep) rs;
                    label1.Text = "DeltaX";
                    label2.Text = "DeltaY";

                    textBox1.Text = rls.DeltaX;
                    textBox1.Text = rls.DeltaY;

                    ShowFirst(2);
                }
            }
            else if (StepManager.CurrentStep is RotateStep)
            {
                var rs = (RotateStep) StepManager.CurrentStep;
                if (rs.StepType == RotateStep.RotateStepType.RotateLine)
                {
                    var rls = (RotateLineStep) rs;
                    label1.Text = "Factor";

                    textBox1.Text = rls.Factor;

                    ShowFirst(1);
                }
            }
            else
            {
                ShowFirst(0);
            }
            _ignoreTextChanged = false;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreTextChanged)
            {
                return;
            }
            if (StepManager.CurrentStep is DrawStep)
            {
                var ds = (DrawStep) StepManager.CurrentStep;
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
            else if (StepManager.CurrentStep is MoveStep)
            {
                var ms = (MoveStep) StepManager.CurrentStep;
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
            else if (StepManager.CurrentStep is ScaleStep)
            {
                var ss = (ScaleStep) StepManager.CurrentStep;
                if (ss.StepType == ScaleStep.ScaleStepType.ScaleRect)
                {
                    ((ScaleRectStep) ss).Scale(textBox1.Text);
                }
                else if (ss.StepType == ScaleStep.ScaleStepType.ScaleEllipse)
                {
                    ((ScaleEllipseStep) ss).Scale(textBox1.Text);
                }
                else if (ss.StepType == ScaleStep.ScaleStepType.ScaleLine)
                {
                    ((ScaleLineStep) ss).Scale(textBox1.Text);
                }
            }
            else if (StepManager.CurrentStep is ResizeStep)
            {
                var rs = (ResizeStep) StepManager.CurrentStep;
                if (rs.StepType == ResizeStep.ResizeStepType.ResizeRect)
                {
                    ((ResizeRectStep) rs).Resize(textBox1.Text);
                }
                else if (rs.StepType == ResizeStep.ResizeStepType.ResizeEllipse)
                {
                    ((ResizeEllipseStep) rs).Resize(textBox1.Text);
                }
                else if (rs.StepType == ResizeStep.ResizeStepType.ResizeLine)
                {
                    ((ResizeLineStep) rs).ResizeX(textBox1.Text);
                }
            }
            else if (StepManager.CurrentStep is RotateStep)
            {
                var rs = (RotateStep) StepManager.CurrentStep;
                if (rs.StepType == RotateStep.RotateStepType.RotateLine)
                {
                    ((RotateLineStep) rs).Rotate(textBox1.Text);
                }
            }
            if (StepManager.CurrentStep.Iterations != -1)
            {
                StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex);
            }
            else
            {
                Form1.RedrawNeeded?.Invoke();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreTextChanged)
            {
                return;
            }
            if (StepManager.CurrentStep is DrawStep)
            {
                var ds = (DrawStep) StepManager.CurrentStep;
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
            else if (StepManager.CurrentStep is MoveStep)
            {
                var ms = (MoveStep) StepManager.CurrentStep;
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
            else if (StepManager.CurrentStep is ResizeStep)
            {
                var rs = (ResizeStep) StepManager.CurrentStep;
                if (rs.StepType == ResizeStep.ResizeStepType.ResizeLine)
                {
                    ((ResizeLineStep) rs).ResizeY(textBox2.Text);
                }
            }
            if (StepManager.CurrentStep.Iterations != -1)
            {
                StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex);
            }
            else
            {
                Form1.RedrawNeeded?.Invoke();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreTextChanged)
            {
                return;
            }
            if (StepManager.CurrentStep is DrawStep)
            {
                var ds = (DrawStep) StepManager.CurrentStep;
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
            if (StepManager.CurrentStep.Iterations != -1)
            {
                StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex);
            }
            else
            {
                Form1.RedrawNeeded?.Invoke();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (_ignoreTextChanged)
            {
                return;
            }
            if (StepManager.CurrentStep is DrawStep)
            {
                var ds = (DrawStep) StepManager.CurrentStep;
                if (ds.StepType == DrawStep.DrawStepType.DrawRect)
                {
                    ((DrawRectStep) ds).ReInitHeight(textBox4.Text);
                }
                else if (ds.StepType == DrawStep.DrawStepType.DrawLine)
                {
                    ((DrawLineStep) ds).ReInitHeight(textBox4.Text);
                }
            }
            if (StepManager.CurrentStep.Iterations != -1)
            {
                StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex);
            }
            else
            {
                Form1.RedrawNeeded?.Invoke();
            }
        }
    }
}