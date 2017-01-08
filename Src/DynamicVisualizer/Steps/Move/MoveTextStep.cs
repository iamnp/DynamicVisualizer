using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Move
{
    public class MoveTextStep : MoveStep
    {
        public readonly TextFigure TextFigure;
        public double HCachedDouble;
        public double WCachedDouble;
        public string X;
        public double XCachedDouble;
        public string Y;
        public double YCachedDouble;

        private MoveTextStep(TextFigure figure)
        {
            Figure = figure;
            TextFigure = figure;
        }

        public MoveTextStep(TextFigure figure, string x, string y) : this(figure)
        {
            Move(x, y);
        }

        public MoveTextStep(TextFigure figure, double x, double y) : this(figure, x.Str(), y.Str())
        {
        }

        public override MoveStepType StepType => MoveStepType.MoveText;

        public void SetDef(string what, string where)
        {
            if ((what == null) || (where == null))
            {
                Def = string.Format("Move {0}, {1} horizontally, {2} vertically", TextFigure.Name, X, Y);
            }
            else
            {
                Def = string.Format("Move {0} so {1} meets {2}", TextFigure.Name, what, where);
            }
        }

        private void CaptureBearings()
        {
            XCachedDouble = TextFigure.X.CachedValue.AsDouble;
            YCachedDouble = TextFigure.Y.CachedValue.AsDouble;
            WCachedDouble = TextFigure.Width.CachedValue.AsDouble;
            HCachedDouble = TextFigure.Height.CachedValue.AsDouble;
        }


        public override void Apply()
        {
            if (!Applied)
            {
                CaptureBearings();
            }
            Applied = true;

            TextFigure.Width.SetRawExpression(WCachedDouble.Str());
            TextFigure.Height.SetRawExpression(HCachedDouble.Str());
            TextFigure.X.SetRawExpression(XCachedDouble.Str());
            TextFigure.Y.SetRawExpression(YCachedDouble.Str());

            TextFigure.X.SetRawExpression(TextFigure.Name + ".x + (" + X + ")");
            TextFigure.Y.SetRawExpression(TextFigure.Name + ".y + (" + Y + ")");

            CopyStaticFigure();
        }

        public override void IterateNext()
        {
            TextFigure.X.IndexInArray = CompletedIterations;
            TextFigure.Y.IndexInArray = CompletedIterations;
            TextFigure.Width.IndexInArray = CompletedIterations;
            TextFigure.Height.IndexInArray = CompletedIterations;

            DataStorage.CachedSwapToAbs(TextFigure.X, TextFigure.Width, TextFigure.Y, TextFigure.Height);

            TextFigure.X.SetRawExpression(TextFigure.Name + ".x + (" + X + ")");
            TextFigure.Y.SetRawExpression(TextFigure.Name + ".y + (" + Y + ")");
        }

        public override void CopyStaticFigure()
        {
            if ((Iterations == -1) || Figure.IsGuide || (Figure.StaticLoopFigures.Count - 1 < CompletedIterations))
            {
                return;
            }

            var lf = (TextFigure) Figure.StaticLoopFigures[CompletedIterations];

            lf.X.SetRawExpression(TextFigure.X.CachedValue.Str);
            lf.Y.SetRawExpression(TextFigure.Y.CachedValue.Str);
            lf.Width.SetRawExpression(TextFigure.Width.CachedValue.Str);
            lf.Height.SetRawExpression(TextFigure.Height.CachedValue.Str);
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