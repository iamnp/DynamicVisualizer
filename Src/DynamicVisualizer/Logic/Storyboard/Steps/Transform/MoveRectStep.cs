using DynamicVisualizer.Logic.Expressions;
using DynamicVisualizer.Logic.Storyboard.Figures;

namespace DynamicVisualizer.Logic.Storyboard.Steps.Transform
{
    public class MoveRectStep : TransformStep
    {
        public readonly RectFigure RectFigure;
        public string X;
        public string Y;

        private MoveRectStep(RectFigure figure)
        {
            Figure = figure;
            RectFigure = figure;
        }

        public MoveRectStep(RectFigure figure, double x, double y) : this(figure)
        {
            Move(x, y);
        }

        public MoveRectStep(RectFigure figure, string x, string y) : this(figure)
        {
            Move(x, y);
        }

        public override TransformStepType StepType => TransformStepType.MoveRect;

        public override void Apply()
        {
            Applied = true;
            RectFigure.X.SetRawExpression(X);
            RectFigure.Y.SetRawExpression(Y);
        }

        public override void ApplyNextIteration()
        {
            if (CurrentIteration >= Iterations) return;

            if (!RectFigure.IsGuide)
            {
                var rf = (RectFigure) Figure.StaticLoopFigures[CurrentIteration];
                rf.X = new ScalarExpression("a", "a", RectFigure.X.CachedValue.Str);
                rf.Y = new ScalarExpression("a", "a", RectFigure.Y.CachedValue.Str);
            }

            if (++CurrentIteration >= Iterations) return;

            RectFigure.X.IndexInArray = CurrentIteration;
            RectFigure.Y.IndexInArray = CurrentIteration;
            RectFigure.X.SetRawExpression(X);
            RectFigure.Y.SetRawExpression(Y);
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