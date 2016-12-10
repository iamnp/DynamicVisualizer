using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Resize
{
    public class ResizeEllipseStep : TransformStep
    {
        public enum Side
        {
            Left,
            Right,
            Top,
            Bottom
        }

        public readonly EllipseFigure EllipseFigure;
        public readonly Side ResizeAround;
        public string Delta;
        public double Radius1Orig;
        public double Radius2Orig;
        public double XCachedDouble;
        public double YCachedDouble;

        private ResizeEllipseStep(EllipseFigure figure, Side resizeAround)
        {
            Figure = figure;
            EllipseFigure = figure;
            ResizeAround = resizeAround;
        }

        public ResizeEllipseStep(EllipseFigure figure, Side resizeAround, double delta) : this(figure, resizeAround)
        {
            Resize(delta);
        }

        public ResizeEllipseStep(EllipseFigure figure, Side resizeAround, string delta) : this(figure, resizeAround)
        {
            Resize(delta);
        }

        public override TransformStepType StepType => TransformStepType.ResizeEllipse;

        private void CaptureBearings()
        {
            Radius1Orig = EllipseFigure.Radius1.CachedValue.AsDouble;
            Radius2Orig = EllipseFigure.Radius2.CachedValue.AsDouble;
            XCachedDouble = EllipseFigure.X.CachedValue.AsDouble;
            YCachedDouble = EllipseFigure.Y.CachedValue.AsDouble;
        }

        public override void Apply()
        {
            if (!Applied) CaptureBearings();
            Applied = true;

            if ((ResizeAround == Side.Left) || (ResizeAround == Side.Right))
            {
                EllipseFigure.X.SetRawExpression(XCachedDouble.Str());
                EllipseFigure.Radius1.SetRawExpression(Radius1Orig.Str());

                EllipseFigure.Radius1.SetRawExpression(EllipseFigure.Name + ".radius1 + (" + Delta + ")");
            }
            else if ((ResizeAround == Side.Top) || (ResizeAround == Side.Bottom))
            {
                EllipseFigure.Y.SetRawExpression(YCachedDouble.Str());
                EllipseFigure.Radius2.SetRawExpression(Radius2Orig.Str());

                EllipseFigure.Radius2.SetRawExpression(EllipseFigure.Name + ".radius2 - (" + Delta + ")");
            }
            if ((Iterations != -1) && !Figure.IsGuide) CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if ((ResizeAround == Side.Left) || (ResizeAround == Side.Right))
            {
                EllipseFigure.Radius1.IndexInArray = CompletedIterations;

                EllipseFigure.X.SetRawExpression(EllipseFigure.X.CachedValue.AsDouble.Str());
                EllipseFigure.Radius1.SetRawExpression(EllipseFigure.Radius1.CachedValue.AsDouble.Str());

                EllipseFigure.Radius1.SetRawExpression(EllipseFigure.Name + ".radius1 + (" + Delta + ")");
            }
            else if ((ResizeAround == Side.Top) || (ResizeAround == Side.Bottom))
            {
                EllipseFigure.Radius2.IndexInArray = CompletedIterations;

                EllipseFigure.Y.SetRawExpression(EllipseFigure.Y.CachedValue.AsDouble.Str());
                EllipseFigure.Radius2.SetRawExpression(EllipseFigure.Radius2.CachedValue.AsDouble.Str());

                EllipseFigure.Radius2.SetRawExpression(EllipseFigure.Name + ".radius2 + (" + Delta + ")");
            }
        }

        public override void CopyStaticFigure()
        {
            var rf = (EllipseFigure) Figure.StaticLoopFigures[CompletedIterations];
            if ((ResizeAround == Side.Left) || (ResizeAround == Side.Right))
                rf.Radius1.SetRawExpression(EllipseFigure.Radius1.CachedValue.Str);
            else if ((ResizeAround == Side.Top) || (ResizeAround == Side.Bottom))
                rf.Radius2.SetRawExpression(EllipseFigure.Radius2.CachedValue.Str);
        }

        public void Resize(string delta)
        {
            Delta = delta;
            Apply();
        }

        public void Resize(double delta)
        {
            Delta = delta.Str();
            Apply();
        }
    }
}