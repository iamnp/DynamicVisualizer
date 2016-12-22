using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Move
{
    public class MoveEllipseStep : MoveStep
    {
        public readonly EllipseFigure EllipseFigure;
        public double R1CachedDouble;
        public double R2CachedDouble;
        public string X;
        public double XCachedDouble;
        public string Y;
        public double YCachedDouble;

        private MoveEllipseStep(EllipseFigure figure)
        {
            Figure = figure;
            EllipseFigure = figure;
        }

        public MoveEllipseStep(EllipseFigure figure, string x, string y) : this(figure)
        {
            Move(x, y);
        }

        public MoveEllipseStep(EllipseFigure figure, double x, double y) : this(figure, x.Str(), y.Str())
        {
        }

        public override MoveStepType StepType => MoveStepType.MoveEllipse;

        public void SetDef(string what, string where)
        {
            if (what == null || where == null)
            {
                Def = string.Format("Move {0}, {1} horizontally, {2} vertically)", EllipseFigure.Name, X, Y);
            }
            else
            {
                Def = string.Format("Move {0} so {1} meets {2}", EllipseFigure.Name, what, where);
            }
        }

        private void CaptureBearings()
        {
            XCachedDouble = EllipseFigure.X.CachedValue.AsDouble;
            YCachedDouble = EllipseFigure.Y.CachedValue.AsDouble;
            R1CachedDouble = EllipseFigure.Radius1.CachedValue.AsDouble;
            R2CachedDouble = EllipseFigure.Radius2.CachedValue.AsDouble;
        }

        public override void Apply()
        {
            if (!Applied)
            {
                CaptureBearings();
            }
            Applied = true;

            EllipseFigure.Radius1.SetRawExpression(R1CachedDouble.Str());
            EllipseFigure.Radius2.SetRawExpression(R2CachedDouble.Str());
            EllipseFigure.X.SetRawExpression(XCachedDouble.Str());
            EllipseFigure.Y.SetRawExpression(YCachedDouble.Str());

            EllipseFigure.X.SetRawExpression(EllipseFigure.Name + ".x + (" + X + ")");
            EllipseFigure.Y.SetRawExpression(EllipseFigure.Name + ".y + (" + Y + ")");

            if (Iterations != -1 && !Figure.IsGuide)
            {
                CopyStaticFigure();
            }
        }

        public override void IterateNext()
        {
            EllipseFigure.X.IndexInArray = CompletedIterations;
            EllipseFigure.Y.IndexInArray = CompletedIterations;
            EllipseFigure.Radius1.IndexInArray = CompletedIterations;
            EllipseFigure.Radius2.IndexInArray = CompletedIterations;

            EllipseFigure.Radius1.SetRawExpression(EllipseFigure.Radius1.CachedValue.AsDouble.Str());
            EllipseFigure.Radius2.SetRawExpression(EllipseFigure.Radius2.CachedValue.AsDouble.Str());
            EllipseFigure.X.SetRawExpression(EllipseFigure.X.CachedValue.AsDouble.Str());
            EllipseFigure.Y.SetRawExpression(EllipseFigure.Y.CachedValue.AsDouble.Str());

            EllipseFigure.X.SetRawExpression(EllipseFigure.Name + ".x + (" + X + ")");
            EllipseFigure.Y.SetRawExpression(EllipseFigure.Name + ".y + (" + Y + ")");
        }

        public override void CopyStaticFigure()
        {
            var ef = (EllipseFigure) Figure.StaticLoopFigures[CompletedIterations];

            ef.X.SetRawExpression(EllipseFigure.X.CachedValue.Str);
            ef.Y.SetRawExpression(EllipseFigure.Y.CachedValue.Str);
            ef.Radius1.SetRawExpression(EllipseFigure.Radius1.CachedValue.Str);
            ef.Radius2.SetRawExpression(EllipseFigure.Radius2.CachedValue.Str);
        }

        public void Move(string x, string y, string what = null, string where = null)
        {
            X = x;
            Y = y;
            SetDef(what, where);
            Apply();
        }

        public void Move(double x, double y)
        {
            Move(x.Str(), y.Str());
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
    }
}