using DynamicVisualizer.Logic.Expressions;
using DynamicVisualizer.Logic.Storyboard.Figures;

namespace DynamicVisualizer.Logic.Storyboard.Steps.Transform
{
    public class MoveCircleStep : TransformStep
    {
        public readonly CircleFigure CircleFigure;
        public string X;
        public string Y;

        private MoveCircleStep(CircleFigure figure)
        {
            Figure = figure;
            CircleFigure = figure;
        }

        public MoveCircleStep(CircleFigure figure, double x, double y) : this(figure)
        {
            Move(x, y);
        }

        public MoveCircleStep(CircleFigure figure, string x, string y) : this(figure)
        {
            Move(x, y);
        }

        public override TransformStepType StepType => TransformStepType.MoveCircle;

        public override void Apply()
        {
            Applied = true;
            CircleFigure.X.SetRawExpression(X);
            CircleFigure.Y.SetRawExpression(Y);

            if ((Iterations != -1) && !Figure.IsGuide) CopyStaticFigure();
        }

        public override void IterateNext()
        {
            CircleFigure.X.IndexInArray = CompletedIterations;
            CircleFigure.Y.IndexInArray = CompletedIterations;
            CircleFigure.X.SetRawExpression(X);
            CircleFigure.Y.SetRawExpression(Y);
        }

        public override void CopyStaticFigure()
        {
            var rf = (CircleFigure) Figure.StaticLoopFigures[CompletedIterations];
            rf.X = new ScalarExpression("a", "a", CircleFigure.X.CachedValue.Str);
            rf.Y = new ScalarExpression("a", "a", CircleFigure.Y.CachedValue.Str);
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