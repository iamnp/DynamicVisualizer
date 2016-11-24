using DynamicVisualizer.Logic.Storyboard.Figures;

namespace DynamicVisualizer.Logic.Storyboard.Steps.Transform
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
        public string Radius1Expr;
        public double Radius1Orig;
        public string Radius2Expr;
        public double Radius2Orig;

        private ResizeEllipseStep(EllipseFigure figure, Side resizeAround)
        {
            ResizeAround = resizeAround;
            Figure = figure;
            EllipseFigure = figure;
            Radius1Expr = EllipseFigure.Radius1.ExprString;
            Radius2Expr = EllipseFigure.Radius2.ExprString;
            Radius1Orig = EllipseFigure.Radius1.CachedValue.AsDouble;
            Radius2Orig = EllipseFigure.Radius2.CachedValue.AsDouble;
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

        public override void Apply()
        {
            Applied = true;

            if ((ResizeAround == Side.Left) || (ResizeAround == Side.Right))
                EllipseFigure.Radius1.SetRawExpression("(" + Radius1Expr + ") + (" + Delta + ")");
            else if ((ResizeAround == Side.Top) || (ResizeAround == Side.Bottom))
                EllipseFigure.Radius2.SetRawExpression("(" + Radius2Expr + ") + (" + Delta + ")");
            if ((Iterations != -1) && !Figure.IsGuide) CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if ((ResizeAround == Side.Left) || (ResizeAround == Side.Right))
            {
                EllipseFigure.Radius1.IndexInArray = CompletedIterations;
                EllipseFigure.Radius1.SetRawExpression("(" + Radius1Expr + ") + (" + Delta + ")");
            }
            else if ((ResizeAround == Side.Top) || (ResizeAround == Side.Bottom))
            {
                EllipseFigure.Radius2.IndexInArray = CompletedIterations;
                EllipseFigure.Radius2.SetRawExpression("(" + Radius2Expr + ") + (" + Delta + ")");
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