using DynamicVisualizer.Logic.Figures;

namespace DynamicVisualizer.Logic.Steps.Transform
{
    public class MoveEllipseStep : TransformStep
    {
        public readonly EllipseFigure EllipseFigure;
        public string X;
        public string Y;

        private MoveEllipseStep(EllipseFigure figure)
        {
            Figure = figure;
            EllipseFigure = figure;
        }

        public MoveEllipseStep(EllipseFigure figure, double x, double y) : this(figure)
        {
            Move(x, y);
        }

        public MoveEllipseStep(EllipseFigure figure, string x, string y) : this(figure)
        {
            Move(x, y);
        }

        public override TransformStepType StepType => TransformStepType.MoveEllipse;

        public override void Apply()
        {
            Applied = true;

            EllipseFigure.Radius1.SetRawExpression(EllipseFigure.Radius1.CachedValue.AsDouble.Str());
            EllipseFigure.Radius2.SetRawExpression(EllipseFigure.Radius2.CachedValue.AsDouble.Str());

            EllipseFigure.X.SetRawExpression(X);
            EllipseFigure.Y.SetRawExpression(Y);

            if ((Iterations != -1) && !Figure.IsGuide) CopyStaticFigure();
        }

        public override void IterateNext()
        {
            EllipseFigure.X.IndexInArray = CompletedIterations;
            EllipseFigure.Y.IndexInArray = CompletedIterations;
            EllipseFigure.Radius1.IndexInArray = CompletedIterations;
            EllipseFigure.Radius2.IndexInArray = CompletedIterations;

            EllipseFigure.X.SetRawExpression(X);
            EllipseFigure.Y.SetRawExpression(Y);
            EllipseFigure.Radius1.SetRawExpression(EllipseFigure.Radius1.CachedValue.AsDouble.Str());
            EllipseFigure.Radius2.SetRawExpression(EllipseFigure.Radius2.CachedValue.AsDouble.Str());
        }

        public override void CopyStaticFigure()
        {
            var ef = (EllipseFigure) Figure.StaticLoopFigures[CompletedIterations];

            ef.X.SetRawExpression(EllipseFigure.X.CachedValue.Str);
            ef.Y.SetRawExpression(EllipseFigure.Y.CachedValue.Str);
            ef.Radius1.SetRawExpression(EllipseFigure.Radius1.CachedValue.Str);
            ef.Radius2.SetRawExpression(EllipseFigure.Radius2.CachedValue.Str);
        }

        public void Move(string x, string y)
        {
            X = x;
            Y = y;
            Apply();
        }

        public void Move(double x, double y)
        {
            X = x.Str();
            Y = y.Str();
            Apply();
        }

        public void MoveX(string x)
        {
            X = x;
            Apply();
        }

        public void MoveY(string y)
        {
            Y = y;
            Apply();
        }
    }
}