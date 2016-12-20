using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Move
{
    public class MoveRectStep : MoveStep
    {
        public readonly RectFigure RectFigure;
        public string X;
        public string Y;

        private MoveRectStep(RectFigure figure)
        {
            Figure = figure;
            RectFigure = figure;
        }

        public MoveRectStep(RectFigure figure, string x, string y) : this(figure)
        {
            Move(x, y);
        }

        public MoveRectStep(RectFigure figure, double x, double y) : this(figure, x.Str(), y.Str())
        {
        }

        public override MoveStepType StepType => MoveStepType.MoveRect;

        public void SetDef(string what, string where)
        {
            if (what == null || where == null)
            {
                Def = string.Format("Move {0} to ({1}; {2})", RectFigure.Name, X, Y);
            }
            else
            {
                Def = string.Format("Move {0} so {1} meets {2}", RectFigure.Name, what, where);
            }
        }

        public override void Apply()
        {
            Applied = true;

            RectFigure.Width.SetRawExpression(RectFigure.Width.CachedValue.AsDouble.Str());
            RectFigure.Height.SetRawExpression(RectFigure.Height.CachedValue.AsDouble.Str());

            RectFigure.X.SetRawExpression(X);
            RectFigure.Y.SetRawExpression(Y);

            if (Iterations != -1 && !Figure.IsGuide)
            {
                CopyStaticFigure();
            }
        }

        public override void IterateNext()
        {
            RectFigure.X.IndexInArray = CompletedIterations;
            RectFigure.Y.IndexInArray = CompletedIterations;
            RectFigure.Width.IndexInArray = CompletedIterations;
            RectFigure.Height.IndexInArray = CompletedIterations;

            RectFigure.Width.SetRawExpression(RectFigure.Width.CachedValue.AsDouble.Str());
            RectFigure.Height.SetRawExpression(RectFigure.Height.CachedValue.AsDouble.Str());
            RectFigure.X.SetRawExpression(X);
            RectFigure.Y.SetRawExpression(Y);
        }

        public override void CopyStaticFigure()
        {
            var rf = (RectFigure) Figure.StaticLoopFigures[CompletedIterations];

            rf.X.SetRawExpression(RectFigure.X.CachedValue.Str);
            rf.Y.SetRawExpression(RectFigure.Y.CachedValue.Str);
            rf.Width.SetRawExpression(RectFigure.Width.CachedValue.Str);
            rf.Height.SetRawExpression(RectFigure.Height.CachedValue.Str);
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