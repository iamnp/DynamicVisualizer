using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Move
{
    public class MoveLineStep : MoveStep
    {
        public readonly LineFigure LineFigure;
        public string X;
        public string Y;

        private MoveLineStep(LineFigure figure)
        {
            Figure = figure;
            LineFigure = figure;
        }

        public MoveLineStep(LineFigure figure, string x, string y) : this(figure)
        {
            Move(x, y);
        }

        public MoveLineStep(LineFigure figure, double x, double y) : this(figure, x.Str(), y.Str())
        {
        }

        public override MoveStepType StepType => MoveStepType.MoveLine;

        public void SetDef(string what, string where)
        {
            if (what == null || where == null)
            {
                Def = string.Format("Move {0} to ({1}; {2})", LineFigure.Name, X, Y);
            }
            else
            {
                Def = string.Format("Move {0} so {1} meets {2}", LineFigure.Name, what, where);
            }
        }

        public override void Apply()
        {
            Applied = true;

            LineFigure.Width.SetRawExpression(LineFigure.Width.CachedValue.AsDouble.Str());
            LineFigure.Height.SetRawExpression(LineFigure.Height.CachedValue.AsDouble.Str());

            LineFigure.X.SetRawExpression(X);
            LineFigure.Y.SetRawExpression(Y);

            if (Iterations != -1 && !Figure.IsGuide)
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

        public void Move(string x, string y, string what = null, string where = null)
        {
            X = x;
            Y = y;
            SetDef(what, where);
            Apply();
        }

        public void MoveX(string x)
        {
            X = x;
            SetDef(null, null);
            Apply();
        }

        public void MoveY(string y)
        {
            Y = y;
            SetDef(null, null);
            Apply();
        }

        public void Move(double x, double y)
        {
            Move(x.Str(), y.Str());
        }
    }
}