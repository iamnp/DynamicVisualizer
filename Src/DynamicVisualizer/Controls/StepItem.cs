﻿using System.Drawing;
using System.Windows.Forms;
using DynamicVisualizer.Steps;
using DynamicVisualizer.Steps.Resize;
using DynamicVisualizer.Steps.Scale;

namespace DynamicVisualizer.Controls
{
    public partial class StepItem : UserControl
    {
        public StepItem(Step step, int index)
        {
            InitializeComponent();
            Index = index;
            Step = step;
            RespectIterable();
            SetText();
        }

        public int Index { get; set; }
        public Step Step { get; }
        public bool Marked { get; set; }

        public void RespectIterable()
        {
            if (Step.Iterations > 0)
            {
                clickThroughLabel1.Location = new Point(clickThroughLabel1.Location.X + 10,
                    clickThroughLabel1.Location.Y);
            }
        }

        public void SetText()
        {
            if (Step is DrawStep || Step is MoveStep)
            {
                clickThroughLabel1.Text = Step.Def;
                return;
            }

            var scaleRectStep = Step as ScaleRectStep;
            if (scaleRectStep != null)
            {
                clickThroughLabel1.Text =
                    string.Format("scale {0} by factor {1} around {2} side",
                        scaleRectStep.Figure.Name, scaleRectStep.Factor, scaleRectStep.ScaleAround);
                return;
            }

            var scaleEllipseStep = Step as ScaleEllipseStep;
            if (scaleEllipseStep != null)
            {
                clickThroughLabel1.Text =
                    string.Format("scale {0} by factor {1} around {2} side",
                        scaleEllipseStep.Figure.Name, scaleEllipseStep.Factor, scaleEllipseStep.ScaleAround);
            }

            var scaleLineStep = Step as ScaleLineStep;
            if (scaleLineStep != null)
            {
                clickThroughLabel1.Text =
                    string.Format("scale {0} by factor {1} around {2} side",
                        scaleLineStep.Figure.Name, scaleLineStep.Factor, scaleLineStep.ScaleAround);
            }

            var resizeRectStep = Step as ResizeRectStep;
            if (resizeRectStep != null)
            {
                clickThroughLabel1.Text =
                    string.Format("resize {0} by {1} around {2} side",
                        resizeRectStep.Figure.Name, resizeRectStep.Delta, resizeRectStep.ResizeAround);
            }

            var resizeEllipseStep = Step as ResizeEllipseStep;
            if (resizeEllipseStep != null)
            {
                clickThroughLabel1.Text =
                    string.Format("resize {0} by {1} around {2} side",
                        resizeEllipseStep.Figure.Name, resizeEllipseStep.Delta, resizeEllipseStep.ResizeAround);
            }
        }
    }
}