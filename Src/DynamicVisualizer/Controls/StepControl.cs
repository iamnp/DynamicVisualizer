using System.Drawing;
using System.Windows.Forms;
using DynamicVisualizer.Logic.Storyboard.Steps;
using DynamicVisualizer.Logic.Storyboard.Steps.Draw;
using DynamicVisualizer.Logic.Storyboard.Steps.Transform;

namespace DynamicVisualizer.Controls
{
    public partial class StepControl : UserControl
    {
        public StepControl(Step step, int index)
        {
            InitializeComponent();
            Index = index;
            Step = step;
            if (step is DrawRectStep)
                clickThroughLabel1.Text = "draw " + step.Figure.Name;
            if (step is MoveRectStep)
                clickThroughLabel1.Text = "move " + step.Figure.Name;
            if (step is DrawCircleStep)
                clickThroughLabel1.Text = "draw " + step.Figure.Name;
            if (step is MoveCircleStep)
                clickThroughLabel1.Text = "move " + step.Figure.Name;
            if (step is ScaleRectStep)
                clickThroughLabel1.Text = "scale " + step.Figure.Name;
            if (step.Iterations > 0)
                clickThroughLabel1.Location = new Point(clickThroughLabel1.Location.X + 10,
                    clickThroughLabel1.Location.Y);
        }

        public int Index { get; set; }
        public Step Step { get; private set; }
    }
}