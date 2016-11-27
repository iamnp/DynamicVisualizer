using DynamicVisualizer.Logic.Figures;

namespace DynamicVisualizer.Logic.Steps.Transform
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

            RectFigure.Width.SetRawExpression(RectFigure.Width.CachedValue.AsDouble.Str());
            RectFigure.Height.SetRawExpression(RectFigure.Height.CachedValue.AsDouble.Str());

            RectFigure.X.SetRawExpression(X);
            RectFigure.Y.SetRawExpression(Y);

            if ((Iterations != -1) && !Figure.IsGuide) CopyStaticFigure();
        }

        public override void IterateNext()
        {
            RectFigure.X.IndexInArray = CompletedIterations;
            RectFigure.Y.IndexInArray = CompletedIterations;
            RectFigure.Width.IndexInArray = CompletedIterations;
            RectFigure.Height.IndexInArray = CompletedIterations;

            RectFigure.X.SetRawExpression(X);
            RectFigure.Y.SetRawExpression(Y);
            RectFigure.Width.SetRawExpression(RectFigure.Width.CachedValue.AsDouble.Str());
            RectFigure.Height.SetRawExpression(RectFigure.Height.CachedValue.AsDouble.Str());
        }

        public override void CopyStaticFigure()
        {
            var rf = (RectFigure) Figure.StaticLoopFigures[CompletedIterations];

            rf.X.SetRawExpression(RectFigure.X.CachedValue.Str);
            rf.Y.SetRawExpression(RectFigure.Y.CachedValue.Str);
            rf.Width.SetRawExpression(RectFigure.Width.CachedValue.Str);
            rf.Height.SetRawExpression(RectFigure.Height.CachedValue.Str);
        }

        public void Move(string x, string y)
        {
            X = x;
            Y = y;
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

        public void Move(double x, double y)
        {
            X = x.Str();
            Y = y.Str();
            Apply();
        }
    }
}