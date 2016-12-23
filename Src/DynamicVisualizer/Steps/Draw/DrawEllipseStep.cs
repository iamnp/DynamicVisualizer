using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Draw
{
    public class DrawEllipseStep : DrawStep
    {
        private static int _count = 1;
        public readonly EllipseFigure EllipseFigure;
        public string Radius;
        public string X;
        public string Y;

        private DrawEllipseStep()
        {
            Figure = new EllipseFigure("circle" + _count++);
            EllipseFigure = (EllipseFigure) Figure;
        }

        public DrawEllipseStep(string x, string y, string radius, string startDef = null) : this()
        {
            X = x;
            Y = y;
            SetStartDef(startDef);
            ReInit(radius);
        }

        public DrawEllipseStep(double x, double y, double radius) : this(x.Str(), y.Str(), radius.Str())
        {
        }

        public override DrawStepType StepType => DrawStepType.DrawEllipse;

        public void SetStartDef(string point)
        {
            if (point == null)
            {
                StartDef = string.Format("Draw {0} around ({1}; {2})", EllipseFigure.Name, X, Y);
            }
            else
            {
                StartDef = string.Format("Draw {0} around {1}", EllipseFigure.Name, point);
            }
            Def = StartDef + EndDef;
        }

        public void SetEndDef(string point)
        {
            if (point == null)
            {
                EndDef = string.Format(", {0} radius", Radius);
            }
            else
            {
                EndDef = string.Format(" to {0}", point);
            }
            Def = StartDef + EndDef;
        }

        public void ReInit(string radius, string endDef = null)
        {
            Radius = radius;
            SetEndDef(endDef);
            Apply();
        }

        public void ReInit(double radius)
        {
            ReInit(radius.Str());
        }

        public void ReInitX(string x)
        {
            X = x;
            SetStartDef(null);
            Apply();
        }

        public void ReInitY(string y)
        {
            Y = y;
            SetStartDef(null);
            Apply();
        }

        public override void Apply()
        {
            if (!Applied)
            {
                StepManager.Figures.Add(EllipseFigure);
            }

            if (EllipseFigure.X == null || !Applied)
            {
                EllipseFigure.X = DataStorage.Add(new ScalarExpression(Figure.Name, "x", X, Figure.IsGuide));
            }
            else
            {
                EllipseFigure.X.SetRawExpression(X);
            }

            if (EllipseFigure.Y == null || !Applied)
            {
                EllipseFigure.Y = DataStorage.Add(new ScalarExpression(Figure.Name, "y", Y, Figure.IsGuide));
            }
            else
            {
                EllipseFigure.Y.SetRawExpression(Y);
            }

            if (EllipseFigure.Radius1 == null || !Applied)
            {
                EllipseFigure.Radius1 =
                    DataStorage.Add(new ScalarExpression(Figure.Name, "radius1", Radius, Figure.IsGuide));
            }
            else
            {
                EllipseFigure.Radius1.SetRawExpression(Radius);
            }

            if (EllipseFigure.Radius2 == null || !Applied)
            {
                EllipseFigure.Radius2 =
                    DataStorage.Add(new ScalarExpression(Figure.Name, "radius2", Radius, Figure.IsGuide));
            }
            else
            {
                EllipseFigure.Radius2.SetRawExpression(Radius);
            }

            Applied = true;

            CopyStaticFigure();
        }

        public override void IterateNext()
        {
            EllipseFigure.X.IndexInArray = CompletedIterations;
            EllipseFigure.X.SetRawExpression(X);

            EllipseFigure.Y.IndexInArray = CompletedIterations;
            EllipseFigure.Y.SetRawExpression(Y);

            EllipseFigure.Radius1.IndexInArray = CompletedIterations;
            EllipseFigure.Radius1.SetRawExpression(Radius);

            EllipseFigure.Radius2.IndexInArray = CompletedIterations;
            EllipseFigure.Radius2.SetRawExpression(Radius);
        }

        public override void CopyStaticFigure()
        {
            if (Iterations == -1 || Figure.IsGuide)
            {
                return;
            }

            var rf = new EllipseFigure
            {
                X = new ScalarExpression("a", "a", EllipseFigure.X.CachedValue.Str),
                Y = new ScalarExpression("a", "a", EllipseFigure.Y.CachedValue.Str),
                Radius1 = new ScalarExpression("a", "a", EllipseFigure.Radius1.CachedValue.Str),
                Radius2 = new ScalarExpression("a", "a", EllipseFigure.Radius2.CachedValue.Str)
            };
            Figure.StaticLoopFigures.Add(rf);
            StepManager.Figures.Add(rf);
        }
    }
}