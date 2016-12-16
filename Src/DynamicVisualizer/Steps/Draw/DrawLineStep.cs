using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Draw
{
    public class DrawLineStep : DrawStep
    {
        private static int _count = 1;
        public readonly LineFigure LineFigure;
        public string Height;
        public string Width;
        public string X;
        public string Y;

        private DrawLineStep()
        {
            Figure = new LineFigure("line" + _count++);
            LineFigure = (LineFigure) Figure;
        }

        public DrawLineStep(string x, string y, string width, string height) : this()
        {
            X = x;
            Y = y;
            ReInit(width, height);
        }

        public DrawLineStep(double x, double y, double width, double height) : this()
        {
            X = x.Str();
            Y = y.Str();
            ReInit(width, height);
        }

        public override DrawStepType StepType => DrawStepType.DrawLine;

        public void ReInit(string width, string height)
        {
            Width = width;
            Height = height;
            Apply();
        }

        public void ReInitWidth(string width)
        {
            Width = width;
            Apply();
        }

        public void ReInitHeight(string height)
        {
            Height = height;
            Apply();
        }

        public void ReInitX(string x)
        {
            X = x;
            Apply();
        }

        public void ReInitY(string y)
        {
            Y = y;
            Apply();
        }

        public void ReInit(double width, double height)
        {
            Width = width.Str();
            Height = height.Str();
            Apply();
        }

        public override void Apply()
        {
            if (!Applied)
            {
                StepManager.Figures.Add(LineFigure);
            }

            if (LineFigure.X == null || !Applied)
            {
                LineFigure.X = DataStorage.Add(new ScalarExpression(Figure.Name, "x", X, Figure.IsGuide));
            }
            else
            {
                LineFigure.X.SetRawExpression(X);
            }

            if (LineFigure.Y == null || !Applied)
            {
                LineFigure.Y = DataStorage.Add(new ScalarExpression(Figure.Name, "y", Y, Figure.IsGuide));
            }
            else
            {
                LineFigure.Y.SetRawExpression(Y);
            }

            if (LineFigure.Width == null || !Applied)
            {
                LineFigure.Width = DataStorage.Add(new ScalarExpression(Figure.Name, "width", Width, Figure.IsGuide));
            }
            else
            {
                LineFigure.Width.SetRawExpression(Width);
            }

            if (LineFigure.Height == null || !Applied)
            {
                LineFigure.Height = DataStorage.Add(new ScalarExpression(Figure.Name, "height", Height, Figure.IsGuide));
            }
            else
            {
                LineFigure.Height.SetRawExpression(Height);
            }

            Applied = true;

            if (Iterations != -1 && !Figure.IsGuide)
            {
                CopyStaticFigure();
            }
        }

        public override void IterateNext()
        {
            LineFigure.X.IndexInArray = CompletedIterations;
            LineFigure.X.SetRawExpression(X);

            LineFigure.Y.IndexInArray = CompletedIterations;
            LineFigure.Y.SetRawExpression(Y);

            LineFigure.Width.IndexInArray = CompletedIterations;
            LineFigure.Width.SetRawExpression(Width);

            LineFigure.Height.IndexInArray = CompletedIterations;
            LineFigure.Height.SetRawExpression(Height);
        }

        public override void CopyStaticFigure()
        {
            var lf = new LineFigure
            {
                X = new ScalarExpression("a", "a", LineFigure.X.CachedValue.Str),
                Y = new ScalarExpression("a", "a", LineFigure.Y.CachedValue.Str),
                Width = new ScalarExpression("a", "a", LineFigure.Width.CachedValue.Str),
                Height = new ScalarExpression("a", "a", LineFigure.Height.CachedValue.Str)
            };
            Figure.StaticLoopFigures.Add(lf);
            StepManager.Figures.Add(lf);
        }
    }
}