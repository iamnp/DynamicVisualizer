using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Draw
{
    public class DrawRectStep : DrawStep
    {
        private static int _count = 1;
        public readonly RectFigure RectFigure;
        public string Height;
        public string Width;
        public string X;
        public string Y;

        private DrawRectStep()
        {
            Figure = new RectFigure("rect" + _count++);
            RectFigure = (RectFigure) Figure;
        }

        public DrawRectStep(string x, string y, string width, string height, string startDef = null) : this()
        {
            X = x;
            Y = y;
            SetStartDef(startDef);
            ReInit(width, height);
        }

        public DrawRectStep(double x, double y, double width, double height)
            : this(x.Str(), y.Str(), width.Str(), height.Str())
        {
        }

        public override DrawStepType StepType => DrawStepType.DrawRect;

        public void SetStartDef(string point)
        {
            if (point == null)
            {
                StartDef = string.Format("Draw {0} from ({1}; {2})", RectFigure.Name, X, Y);
            }
            else
            {
                StartDef = string.Format("Draw {0} from {1}", RectFigure.Name, point);
            }
            Def = StartDef + EndDef;
        }

        public void SetEndDef(string point)
        {
            if (point == null)
            {
                EndDef = string.Format(", {0} width, {1} height", Width, Height);
            }
            else
            {
                EndDef = string.Format(" to {0}", point);
            }
            Def = StartDef + EndDef;
        }

        public void ReInit(string width, string height, string endDef = null)
        {
            Width = width;
            Height = height;
            SetEndDef(endDef);
            Apply();
        }

        public void ReInitWidth(string width)
        {
            Width = width;
            SetEndDef(null);
            Apply();
        }

        public void ReInitHeight(string height)
        {
            Height = height;
            SetEndDef(null);
            Apply();
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

        public void ReInit(double width, double height)
        {
            ReInit(width.Str(), height.Str());
        }

        public override void Apply()
        {
            if (!Applied)
            {
                StepManager.Figures.Add(RectFigure);
            }

            if (RectFigure.X == null || !Applied)
            {
                RectFigure.X = DataStorage.Add(new ScalarExpression(Figure.Name, "x", X, Figure.IsGuide));
            }
            else
            {
                RectFigure.X.SetRawExpression(X);
            }

            if (RectFigure.Y == null || !Applied)
            {
                RectFigure.Y = DataStorage.Add(new ScalarExpression(Figure.Name, "y", Y, Figure.IsGuide));
            }
            else
            {
                RectFigure.Y.SetRawExpression(Y);
            }

            if (RectFigure.Width == null || !Applied)
            {
                RectFigure.Width = DataStorage.Add(new ScalarExpression(Figure.Name, "width", Width, Figure.IsGuide));
            }
            else
            {
                RectFigure.Width.SetRawExpression(Width);
            }

            if (RectFigure.Height == null || !Applied)
            {
                RectFigure.Height = DataStorage.Add(new ScalarExpression(Figure.Name, "height", Height, Figure.IsGuide));
            }
            else
            {
                RectFigure.Height.SetRawExpression(Height);
            }

            Applied = true;

            if (Iterations != -1 && !Figure.IsGuide)
            {
                CopyStaticFigure();
            }
        }

        public override void IterateNext()
        {
            RectFigure.X.IndexInArray = CompletedIterations;
            RectFigure.X.SetRawExpression(X);

            RectFigure.Y.IndexInArray = CompletedIterations;
            RectFigure.Y.SetRawExpression(Y);

            RectFigure.Width.IndexInArray = CompletedIterations;
            RectFigure.Width.SetRawExpression(Width);

            RectFigure.Height.IndexInArray = CompletedIterations;
            RectFigure.Height.SetRawExpression(Height);
        }

        public override void CopyStaticFigure()
        {
            var rf = new RectFigure
            {
                X = new ScalarExpression("a", "a", RectFigure.X.CachedValue.Str),
                Y = new ScalarExpression("a", "a", RectFigure.Y.CachedValue.Str),
                Width = new ScalarExpression("a", "a", RectFigure.Width.CachedValue.Str),
                Height = new ScalarExpression("a", "a", RectFigure.Height.CachedValue.Str)
            };
            Figure.StaticLoopFigures.Add(rf);
            StepManager.Figures.Add(rf);
        }
    }
}