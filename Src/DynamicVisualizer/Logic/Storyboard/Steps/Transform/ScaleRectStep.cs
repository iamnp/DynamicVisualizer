using DynamicVisualizer.Logic.Expressions;
using DynamicVisualizer.Logic.Storyboard.Figures;

namespace DynamicVisualizer.Logic.Storyboard.Steps.Transform
{
    public class ScaleRectStep : TransformStep
    {
        public enum ScalingSide
        {
            Left,
            Top
        }

        public readonly RectFigure RectFigure;
        public readonly ScalingSide ScaleAround;
        public string Factor;
        public string HeightExpr;
        public string WidthExpr;

        private ScaleRectStep(RectFigure figure, ScalingSide scaleAround)
        {
            ScaleAround = scaleAround;
            Figure = figure;
            RectFigure = figure;
            WidthExpr = RectFigure.Width.ExprString;
            HeightExpr = RectFigure.Height.ExprString;
        }

        public ScaleRectStep(RectFigure figure, ScalingSide scaleAround, double factor) : this(figure, scaleAround)
        {
            Scale(factor);
        }

        public ScaleRectStep(RectFigure figure, ScalingSide scaleAround, string factor) : this(figure, scaleAround)
        {
            Scale(factor);
        }

        public override TransformStepType StepType => TransformStepType.ScaleRect;

        public override void Apply()
        {
            Applied = true;

            if (ScaleAround == ScalingSide.Left)
                RectFigure.Width.SetRawExpression("(" + WidthExpr + ") * (" + Factor + ")");
            else
                RectFigure.Height.SetRawExpression("(" + HeightExpr + ") * (" + Factor + ")");

            if ((Iterations != -1) && !Figure.IsGuide) CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if (ScaleAround == ScalingSide.Left)
            {
                RectFigure.Width.IndexInArray = CompletedIterations;
                RectFigure.Width.SetRawExpression("(" + WidthExpr + ") * (" + Factor + ")");
            }
            else
            {
                RectFigure.Height.IndexInArray = CompletedIterations;
                RectFigure.Height.SetRawExpression("(" + HeightExpr + ") * (" + Factor + ")");
            }
        }

        public override void CopyStaticFigure()
        {
            var rf = (RectFigure) Figure.StaticLoopFigures[CompletedIterations];
            if (ScaleAround == ScalingSide.Left)
                rf.Width = new ScalarExpression("a", "a", RectFigure.Width.CachedValue.Str);
            else
                rf.Height = new ScalarExpression("a", "a", RectFigure.Height.CachedValue.Str);
        }

        public void Scale(string factor)
        {
            Factor = factor;
            Apply();
        }

        public void Scale(double factor)
        {
            Factor = factor.Str();
            Apply();
        }
    }
}