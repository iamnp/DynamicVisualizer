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
        public string Radius1Expr;
        public double Radius1Orig;
        public string Radius2Expr;
        public double Radius2Orig;

        private ScaleEllipseStep(EllipseFigure figure, Side scaleAround)
        {
            ScaleAround = scaleAround;
            Figure = figure;
            EllipseFigure = figure;
            Radius1Expr = EllipseFigure.Radius1.ExprString;
            Radius2Expr = EllipseFigure.Radius2.ExprString;
            Radius1Orig = EllipseFigure.Radius1.CachedValue.AsDouble;
            Radius2Orig = EllipseFigure.Radius2.CachedValue.AsDouble;
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

        public override void Apply()
        {
            Applied = true;

            if ((ScaleAround == Side.Left) || (ScaleAround == Side.Right))
                EllipseFigure.Radius1.SetRawExpression("(" + Radius1Expr + ") * (" + Factor + ")");
            else if ((ScaleAround == Side.Top) || (ScaleAround == Side.Bottom))
                EllipseFigure.Radius2.SetRawExpression("(" + Radius2Expr + ") * (" + Factor + ")");
            if ((Iterations != -1) && !Figure.IsGuide) CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if ((ScaleAround == Side.Left) || (ScaleAround == Side.Right))
            {
                EllipseFigure.Radius1.IndexInArray = CompletedIterations;
                EllipseFigure.Radius1.SetRawExpression("(" + Radius1Expr + ") * (" + Factor + ")");
            }
            else if ((ScaleAround == Side.Top) || (ScaleAround == Side.Bottom))
            {
                EllipseFigure.Radius2.IndexInArray = CompletedIterations;
                EllipseFigure.Radius2.SetRawExpression("(" + Radius2Expr + ") * (" + Factor + ")");
            }
        }

        public override void CopyStaticFigure()
        {
            var rf = (EllipseFigure) Figure.StaticLoopFigures[CompletedIterations];
            if ((ScaleAround == Side.Left) || (ScaleAround == Side.Right))
                rf.Radius1.SetRawExpression(EllipseFigure.Radius1.CachedValue.Str);
            else if ((ScaleAround == Side.Top) || (ScaleAround == Side.Bottom))
                rf.Radius2.SetRawExpression(EllipseFigure.Radius2.CachedValue.Str);
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