using DynamicVisualizer.Logic.Expressions;
using DynamicVisualizer.Logic.Storyboard.Figures;

namespace DynamicVisualizer.Logic.Storyboard.Steps.Transform
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
            EllipseFigure.X.SetRawExpression(X);
            EllipseFigure.Y.SetRawExpression(Y);

            if ((Iterations != -1) && !Figure.IsGuide) CopyStaticFigure();
        }

        public override void IterateNext()
        {
            EllipseFigure.X.IndexInArray = CompletedIterations;
            EllipseFigure.Y.IndexInArray = CompletedIterations;
            EllipseFigure.X.SetRawExpression(X);
            EllipseFigure.Y.SetRawExpression(Y);
        }

        public override void CopyStaticFigure()
        {
            var rf = (EllipseFigure) Figure.StaticLoopFigures[CompletedIterations];
            rf.X = new ScalarExpression("a", "a", EllipseFigure.X.CachedValue.Str);
            rf.Y = new ScalarExpression("a", "a", EllipseFigure.Y.CachedValue.Str);
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
    }
}