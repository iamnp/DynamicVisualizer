using DynamicVisualizer.Logic.Expressions;
using DynamicVisualizer.Logic.Storyboard.Figures;

namespace DynamicVisualizer.Logic.Storyboard.Steps.Transform
{
    public class ScaleRectStep : TransformStep
    {
        public enum Side
        {
            Left,
            Right,
            Top,
            Bottom
        }

        public readonly RectFigure RectFigure;
        public readonly Side ScaleAround;
        public string Factor;
        public string HeightExpr;
        public double HeightOrig;
        public string WidthExpr;
        public double WidthOrig;
        public string XExpr;
        public string YExpr;

        private ScaleRectStep(RectFigure figure, Side scaleAround)
        {
            ScaleAround = scaleAround;
            Figure = figure;
            RectFigure = figure;
            WidthExpr = RectFigure.Width.ExprString;
            HeightExpr = RectFigure.Height.ExprString;
            XExpr = RectFigure.X.ExprString;
            YExpr = RectFigure.Y.ExprString;
            HeightOrig = RectFigure.Height.CachedValue.AsDouble;
            WidthOrig = RectFigure.Width.CachedValue.AsDouble;
        }

        public ScaleRectStep(RectFigure figure, Side scaleAround, double factor) : this(figure, scaleAround)
        {
            Scale(factor);
        }

        public ScaleRectStep(RectFigure figure, Side scaleAround, string factor) : this(figure, scaleAround)
        {
            Scale(factor);
        }

        public override TransformStepType StepType => TransformStepType.ScaleRect;

        public override void Apply()
        {
            Applied = true;

            if (ScaleAround == Side.Left)
                RectFigure.Width.SetRawExpression("(" + WidthExpr + ") * (" + Factor + ")");
            else if (ScaleAround == Side.Top)
                RectFigure.Height.SetRawExpression("(" + HeightExpr + ") * (" + Factor + ")");
            else if (ScaleAround == Side.Right)
            {
                RectFigure.Width.SetRawExpression("(" + WidthExpr + ") * (" + Factor + ")");
                RectFigure.X.SetRawExpression("(" + XExpr + ") + ((" + WidthExpr + ") * (1.0 - (" + Factor + ")))");
            }
            else if (ScaleAround == Side.Bottom)
            {
                RectFigure.Height.SetRawExpression("(" + HeightExpr + ") * (" + Factor + ")");
                RectFigure.Y.SetRawExpression("(" + YExpr + ") + ((" + HeightExpr + ") * (1.0 - (" + Factor + ")))");
            }
            if ((Iterations != -1) && !Figure.IsGuide) CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if (ScaleAround == Side.Left)
            {
                RectFigure.Width.IndexInArray = CompletedIterations;
                RectFigure.Width.SetRawExpression("(" + WidthExpr + ") * (" + Factor + ")");
            }
            else if (ScaleAround == Side.Top)
            {
                RectFigure.Height.IndexInArray = CompletedIterations;
                RectFigure.Height.SetRawExpression("(" + HeightExpr + ") * (" + Factor + ")");
            }
            else if (ScaleAround == Side.Right)
            {
                RectFigure.Width.IndexInArray = CompletedIterations;
                RectFigure.X.IndexInArray = CompletedIterations;
                RectFigure.Width.SetRawExpression("(" + WidthExpr + ") * (" + Factor + ")");
                RectFigure.X.SetRawExpression("(" + XExpr + ") + ((" + WidthExpr + ") * (1.0 - (" + Factor + ")))");
            }
            else if (ScaleAround == Side.Bottom)
            {
                RectFigure.Height.IndexInArray = CompletedIterations;
                RectFigure.Y.IndexInArray = CompletedIterations;
                RectFigure.Height.SetRawExpression("(" + HeightExpr + ") * (" + Factor + ")");
                RectFigure.Y.SetRawExpression("(" + YExpr + ") + ((" + HeightExpr + ") * (1.0 - (" + Factor + ")))");
            }
        }

        public override void CopyStaticFigure()
        {
            var rf = (RectFigure) Figure.StaticLoopFigures[CompletedIterations];
            if (ScaleAround == Side.Left)
                rf.Width = new ScalarExpression("a", "a", RectFigure.Width.CachedValue.Str);
            else if (ScaleAround == Side.Top)
                rf.Height = new ScalarExpression("a", "a", RectFigure.Height.CachedValue.Str);
            else if (ScaleAround == Side.Right)
            {
                rf.Width = new ScalarExpression("a", "a", RectFigure.Width.CachedValue.Str);
                rf.X = new ScalarExpression("a", "a", RectFigure.X.CachedValue.Str);
            }
            else if (ScaleAround == Side.Bottom)
            {
                rf.Height = new ScalarExpression("a", "a", RectFigure.Height.CachedValue.Str);
                rf.Y = new ScalarExpression("a", "a", RectFigure.Y.CachedValue.Str);
            }
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