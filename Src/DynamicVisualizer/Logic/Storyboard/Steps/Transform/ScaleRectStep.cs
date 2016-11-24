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
        public double XCachedDouble;
        public double YCachedDouble;

        private ScaleRectStep(RectFigure figure, Side scaleAround)
        {
            ScaleAround = scaleAround;
            Figure = figure;
            RectFigure = figure;
            WidthExpr = RectFigure.Width.ExprString;
            HeightExpr = RectFigure.Height.ExprString;
            XCachedDouble = RectFigure.X.CachedValue.AsDouble;
            YCachedDouble = RectFigure.Y.CachedValue.AsDouble;
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
            {
                RectFigure.Width.SetRawExpression("(" + WidthExpr + ") * (" + Factor + ")");
                RectFigure.X.SetRawExpression(RectFigure.X.CachedValue.AsDouble.Str());
            }
            else if (ScaleAround == Side.Top)
            {
                RectFigure.Height.SetRawExpression("(" + HeightExpr + ") * (" + Factor + ")");
                RectFigure.Y.SetRawExpression(RectFigure.Y.CachedValue.AsDouble.Str());
            }
            else if (ScaleAround == Side.Right)
            {
                RectFigure.Width.SetRawExpression("(" + WidthExpr + ") * (" + Factor + ")");
                RectFigure.X.SetRawExpression("(" + XCachedDouble + ") + ((" + WidthExpr + ") * (1.0 - (" + Factor +
                                              ")))");
            }
            else if (ScaleAround == Side.Bottom)
            {
                RectFigure.Height.SetRawExpression("(" + HeightExpr + ") * (" + Factor + ")");
                RectFigure.Y.SetRawExpression("(" + YCachedDouble + ") + ((" + HeightExpr + ") * (1.0 - (" + Factor +
                                              ")))");
            }
            if ((Iterations != -1) && !Figure.IsGuide) CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if (ScaleAround == Side.Left)
            {
                RectFigure.Width.IndexInArray = CompletedIterations;
                RectFigure.X.IndexInArray = CompletedIterations;
                RectFigure.Width.SetRawExpression("(" + WidthExpr + ") * (" + Factor + ")");
                RectFigure.X.SetRawExpression(RectFigure.X.CachedValue.AsDouble.Str());
            }
            else if (ScaleAround == Side.Top)
            {
                RectFigure.Height.IndexInArray = CompletedIterations;
                RectFigure.Height.SetRawExpression("(" + HeightExpr + ") * (" + Factor + ")");
                RectFigure.Y.IndexInArray = CompletedIterations;
                RectFigure.Y.SetRawExpression(RectFigure.Y.CachedValue.AsDouble.Str());
            }
            else if (ScaleAround == Side.Right)
            {
                RectFigure.Width.IndexInArray = CompletedIterations;
                RectFigure.X.IndexInArray = CompletedIterations;

                // break the expr connection
                RectFigure.X.SetRawExpression(RectFigure.X.CachedValue.Str);
                RectFigure.Width.SetRawExpression("(" + WidthExpr + ") * (" + Factor + ")");
                RectFigure.X.SetRawExpression("(" + RectFigure.X.CachedValue.Str + ") + ((" + WidthExpr + ") * (1.0 - (" + Factor +
                                              ")))");
            }
            else if (ScaleAround == Side.Bottom)
            {
                RectFigure.Height.IndexInArray = CompletedIterations;
                RectFigure.Y.IndexInArray = CompletedIterations;

                // break the expr connection
                RectFigure.Y.SetRawExpression(RectFigure.Y.CachedValue.Str);
                RectFigure.Height.SetRawExpression("(" + HeightExpr + ") * (" + Factor + ")");
                RectFigure.Y.SetRawExpression("(" + RectFigure.Y.CachedValue.Str + ") + ((" + HeightExpr + ") * (1.0 - (" + Factor +
                                              ")))");
            }
        }

        public override void CopyStaticFigure()
        {
            var rf = (RectFigure) Figure.StaticLoopFigures[CompletedIterations];
            if (ScaleAround == Side.Left)
            {
                rf.Width.SetRawExpression(RectFigure.Width.CachedValue.Str);
                rf.X.SetRawExpression(RectFigure.X.CachedValue.Str);
            }
            else if (ScaleAround == Side.Top)
            {
                rf.Height.SetRawExpression(RectFigure.Height.CachedValue.Str);
                rf.Y.SetRawExpression(RectFigure.Y.CachedValue.Str);
            }
            else if (ScaleAround == Side.Right)
            {
                rf.Width.SetRawExpression(RectFigure.Width.CachedValue.Str);
                rf.X.SetRawExpression(RectFigure.X.CachedValue.Str);
            }
            else if (ScaleAround == Side.Bottom)
            {
                rf.Height.SetRawExpression(RectFigure.Height.CachedValue.Str);
                rf.Y.SetRawExpression(RectFigure.Y.CachedValue.Str);
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