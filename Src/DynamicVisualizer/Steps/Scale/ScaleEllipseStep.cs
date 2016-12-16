using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Scale
{
    public class ScaleEllipseStep : TransformStep
    {
        public enum Side
        {
            Left,
            Right,
            Top,
            Bottom
        }

        public readonly EllipseFigure EllipseFigure;
        public readonly Side ScaleAround;
        public string Factor;
        public double Radius1Orig;
        public double Radius2Orig;
        public double XCachedDouble;
        public double YCachedDouble;

        private ScaleEllipseStep(EllipseFigure figure, Side scaleAround)
        {
            Figure = figure;
            EllipseFigure = figure;
            ScaleAround = scaleAround;
        }

        public ScaleEllipseStep(EllipseFigure figure, Side scaleAround, double factor) : this(figure, scaleAround)
        {
            Scale(factor);
        }

        public ScaleEllipseStep(EllipseFigure figure, Side scaleAround, string factor) : this(figure, scaleAround)
        {
            Scale(factor);
        }

        public override TransformStepType StepType => TransformStepType.ScaleEllipse;

        private void CaptureBearings()
        {
            Radius1Orig = EllipseFigure.Radius1.CachedValue.AsDouble;
            Radius2Orig = EllipseFigure.Radius2.CachedValue.AsDouble;
            XCachedDouble = EllipseFigure.X.CachedValue.AsDouble;
            YCachedDouble = EllipseFigure.Y.CachedValue.AsDouble;
        }

        public override void Apply()
        {
            if (!Applied)
            {
                CaptureBearings();
            }
            Applied = true;

            if (ScaleAround == Side.Left || ScaleAround == Side.Right)
            {
                EllipseFigure.X.SetRawExpression(XCachedDouble.Str());
                EllipseFigure.Radius1.SetRawExpression(Radius1Orig.Str());

                EllipseFigure.Radius1.SetRawExpression(EllipseFigure.Name + ".radius1 * (" + Factor + ")");
            }
            else if (ScaleAround == Side.Top || ScaleAround == Side.Bottom)
            {
                EllipseFigure.Y.SetRawExpression(YCachedDouble.Str());
                EllipseFigure.Radius2.SetRawExpression(Radius2Orig.Str());

                EllipseFigure.Radius2.SetRawExpression(EllipseFigure.Name + ".radius2 * (" + Factor + ")");
            }
            if (Iterations != -1 && !Figure.IsGuide)
            {
                CopyStaticFigure();
            }
        }

        public override void IterateNext()
        {
            if (ScaleAround == Side.Left || ScaleAround == Side.Right)
            {
                EllipseFigure.Radius1.IndexInArray = CompletedIterations;

                EllipseFigure.X.SetRawExpression(EllipseFigure.X.CachedValue.AsDouble.Str());
                EllipseFigure.Radius1.SetRawExpression(Radius1Orig.Str());

                EllipseFigure.Radius1.SetRawExpression(EllipseFigure.Name + ".radius1 * (" + Factor + ")");
            }
            else if (ScaleAround == Side.Top || ScaleAround == Side.Bottom)
            {
                EllipseFigure.Radius2.IndexInArray = CompletedIterations;

                EllipseFigure.Y.SetRawExpression(EllipseFigure.Y.CachedValue.AsDouble.Str());
                EllipseFigure.Radius2.SetRawExpression(Radius2Orig.Str());

                EllipseFigure.Radius2.SetRawExpression(EllipseFigure.Name + ".radius2 * (" + Factor + ")");
            }
        }

        public override void CopyStaticFigure()
        {
            var rf = (EllipseFigure) Figure.StaticLoopFigures[CompletedIterations];
            if (ScaleAround == Side.Left || ScaleAround == Side.Right)
            {
                rf.Radius1.SetRawExpression(EllipseFigure.Radius1.CachedValue.Str);
            }
            else if (ScaleAround == Side.Top || ScaleAround == Side.Bottom)
            {
                rf.Radius2.SetRawExpression(EllipseFigure.Radius2.CachedValue.Str);
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