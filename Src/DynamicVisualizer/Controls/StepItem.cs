using System.Drawing;
using System.Windows.Forms;
using DynamicVisualizer.Steps;

namespace DynamicVisualizer.Controls
{
    public partial class StepItem : UserControl
    {
        public static int HeightValue = 50;

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
            clickThroughLabel1.Text = Step.Def;
        }
    }
}