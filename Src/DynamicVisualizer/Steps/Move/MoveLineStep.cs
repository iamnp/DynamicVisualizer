using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Move
{
    public class MoveLineStep : TransformStep
    {
        public readonly LineFigure LineFigure;
        public string X;
        public string Y;

        private MoveLineStep(LineFigure figure)
        {
            Figure = figure;
            LineFigure = figure;
        }

        public MoveLineStep(LineFigure figure, double x, double y) : this(figure)
        {
            Move(x, y);
        }

        public MoveLineStep(LineFigure figure, string x, string y) : this(figure)
        {
            Move(x, y);
        }

        public override TransformStepType StepType => TransformStepType.MoveLine;

        public override void Apply()
        {
            Applied = true;

            LineFigure.Width.SetRawExpression(LineFigure.Width.CachedValue.AsDouble.Str());
            LineFigure.Height.SetRawExpression(LineFigure.Height.CachedValue.AsDouble.Str());

            LineFigure.X.SetRawExpression(X);
            LineFigure.Y.SetRawExpression(Y);

            if ((Iterations != -1) && !Figure.IsGuide)
            {
                CopyStaticFigure();
            }
        }

        public override void IterateNext()
        {
            LineFigure.X.IndexInArray = CompletedIterations;
            LineFigure.Y.IndexInArray = CompletedIterations;
            LineFigure.Width.IndexInArray = CompletedIterations;
            LineFigure.Height.IndexInArray = CompletedIterations;

            LineFigure.Width.SetRawExpression(LineFigure.Width.CachedValue.AsDouble.Str());
            LineFigure.Height.SetRawExpression(LineFigure.Height.CachedValue.AsDouble.Str());
            LineFigure.X.SetRawExpression(X);
            LineFigure.Y.SetRawExpression(Y);
        }

        public override void CopyStaticFigure()
        {
            var lf = (LineFigure) Figure.StaticLoopFigures[CompletedIterations];

            lf.X.SetRawExpression(LineFigure.X.CachedValue.Str);
            lf.Y.SetRawExpression(LineFigure.Y.CachedValue.Str);
            lf.Width.SetRawExpression(LineFigure.Width.CachedValue.Str);
            lf.Height.SetRawExpression(LineFigure.Height.CachedValue.Str);
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