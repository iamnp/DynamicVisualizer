using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Draw
{
    public class DrawTextStep : DrawStep
    {
        private static int _count = 1;
        public readonly TextFigure TextFigure;
        public string Height;
        public string Width;
        public string X;
        public string Y;

        private DrawTextStep()
        {
            Figure = new TextFigure("text" + _count++);
            TextFigure = (TextFigure) Figure;
        }

        public DrawTextStep(string x, string y, string width, string height, string startDef = null) : this()
        {
            X = x;
            Y = y;
            SetStartDef(startDef);
            ReInit(width, height);
        }

        public DrawTextStep(double x, double y, double width, double height)
            : this(x.Str(), y.Str(), width.Str(), height.Str())
        {
        }

        public override DrawStepType StepType => DrawStepType.DrawText;

        public void SetStartDef(string point)
        {
            if (point == null)
            {
                StartDef = string.Format("Draw {0} from ({1}; {2})", TextFigure.Name, X, Y);
            }
            else
            {
                StartDef = string.Format("Draw {0} from {1}", TextFigure.Name, point);
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
                StepManager.Figures.Add(TextFigure);
            }

            Figure.FigureColor.SetIndex(0);
            TextFigure.FigureText.SetIndex(0);

            if ((TextFigure.X == null) || !Applied)
            {
                TextFigure.X = DataStorage.Add(new ScalarExpression(Figure.Name, "x", X, Figure.IsGuide));
            }
            else
            {
                TextFigure.X.SetRawExpression(X);
            }

            if ((TextFigure.Y == null) || !Applied)
            {
                TextFigure.Y = DataStorage.Add(new ScalarExpression(Figure.Name, "y", Y, Figure.IsGuide));
            }
            else
            {
                TextFigure.Y.SetRawExpression(Y);
            }

            if ((TextFigure.Width == null) || !Applied)
            {
                TextFigure.Width = DataStorage.Add(new ScalarExpression(Figure.Name, "width", Width, Figure.IsGuide));
            }
            else
            {
                TextFigure.Width.SetRawExpression(Width);
            }

            if ((TextFigure.Height == null) || !Applied)
            {
                TextFigure.Height =
                    DataStorage.Add(new ScalarExpression(Figure.Name, "height", Height, Figure.IsGuide));
            }
            else
            {
                TextFigure.Height.SetRawExpression(Height);
            }

            Applied = true;

            CopyStaticFigure();
        }

        public override void IterateNext()
        {
            TextFigure.X.IndexInArray = CompletedIterations;
            TextFigure.X.SetRawExpression(X);

            TextFigure.Y.IndexInArray = CompletedIterations;
            TextFigure.Y.SetRawExpression(Y);

            TextFigure.Width.IndexInArray = CompletedIterations;
            TextFigure.Width.SetRawExpression(Width);

            TextFigure.Height.IndexInArray = CompletedIterations;
            TextFigure.Height.SetRawExpression(Height);

            Figure.FigureColor.SetIndex(CompletedIterations);
            TextFigure.FigureText.SetIndex(CompletedIterations);
        }

        public override void CopyStaticFigure()
        {
            if ((Iterations == -1) || Figure.IsGuide)
            {
                return;
            }

            var lf = new TextFigure
            {
                X = new ScalarExpression("a", "a", TextFigure.X.CachedValue.Str),
                Y = new ScalarExpression("a", "a", TextFigure.Y.CachedValue.Str),
                Width = new ScalarExpression("a", "a", TextFigure.Width.CachedValue.Str),
                Height = new ScalarExpression("a", "a", TextFigure.Height.CachedValue.Str),
                FigureColor = TextFigure.FigureColor.Copy(),
                FigureText = TextFigure.FigureText.Copy()
            };
            Figure.StaticLoopFigures.Add(lf);
            StepManager.Figures.Add(lf);
        }
    }
}