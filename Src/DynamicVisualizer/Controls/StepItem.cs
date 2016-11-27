using System.Drawing;
using System.Windows.Forms;
using DynamicVisualizer.Logic.Steps;
using DynamicVisualizer.Logic.Steps.Draw;
using DynamicVisualizer.Logic.Steps.Transform;

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
                clickThroughLabel1.Location = new Point(clickThroughLabel1.Location.X + 10,
                    clickThroughLabel1.Location.Y);
        }

        public void SetText()
        {
            var drawRectStep = Step as DrawRectStep;
            if (drawRectStep != null)
            {
                clickThroughLabel1.Text =
                    string.Format("draw {0} at ({1}; {2}) {3} width, {4} height",
                        drawRectStep.Figure.Name, drawRectStep.X, drawRectStep.Y, drawRectStep.Width,
                        drawRectStep.Height);
                return;
            }

            var moveRectStep = Step as MoveRectStep;
            if (moveRectStep != null)
            {
                clickThroughLabel1.Text =
                    string.Format("move {0} to ({1}; {2})",
                        moveRectStep.Figure.Name, moveRectStep.X, moveRectStep.Y);
                return;
            }

            var drawEllipseStep = Step as DrawEllipseStep;
            if (drawEllipseStep != null)
            {
                clickThroughLabel1.Text =
                    string.Format("draw {0} at ({1}; {2}) {3} radius",
                        drawEllipseStep.Figure.Name, drawEllipseStep.X, drawEllipseStep.Y, drawEllipseStep.Radius);
                return;
            }


            var moveEllipseStep = Step as MoveEllipseStep;
            if (moveEllipseStep != null)
            {
                clickThroughLabel1.Text =
                    string.Format("move {0} to ({1}; {2})",
                        moveEllipseStep.Figure.Name, moveEllipseStep.X, moveEllipseStep.Y);
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
                clickThroughLabel1.Text =
                    string.Format("scale {0} by factor {1} around {2} side",
                        scaleEllipseStep.Figure.Name, scaleEllipseStep.Factor, scaleEllipseStep.ScaleAround);

            var resizeRectStep = Step as ResizeRectStep;
            if (resizeRectStep != null)
                clickThroughLabel1.Text =
                    string.Format("resize {0} by {1} around {2} side",
                        resizeRectStep.Figure.Name, resizeRectStep.Delta, resizeRectStep.ResizeAround);

            var resizeEllipseStep = Step as ResizeEllipseStep;
            if (resizeEllipseStep != null)
                clickThroughLabel1.Text =
                    string.Format("resize {0} by {1} around {2} side",
                        resizeEllipseStep.Figure.Name, resizeEllipseStep.Delta, resizeEllipseStep.ResizeAround);
        }
    }
}