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

        public DrawRectStep(string x, string y, string width, string height) : this()
        {
            X = x;
            Y = y;
            ReInit(width, height);
        }

        public DrawRectStep(double x, double y, double width, double height) : this()
        {
            X = x.Str();
            Y = y.Str();
            ReInit(width, height);
        }

        public override DrawStepType StepType => DrawStepType.DrawRect;

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
            if (!Applied) StepManager.Figures.Add(RectFigure);
            Applied = true;

            RectFigure.X = DataStorage.Add(new ScalarExpression(Figure.Name, "x", X, Figure.IsGuide));
            RectFigure.Y = DataStorage.Add(new ScalarExpression(Figure.Name, "y", Y, Figure.IsGuide));
            RectFigure.Width =
                DataStorage.Add(new ScalarExpression(Figure.Name, "width", Width, Figure.IsGuide));
            RectFigure.Height =
                DataStorage.Add(new ScalarExpression(Figure.Name, "height", Height, Figure.IsGuide));

            if ((Iterations != -1) && !Figure.IsGuide) CopyStaticFigure();
        }

        public override void IterateNext()
        {
            RectFigure.X =
                DataStorage.Add(new ScalarExpression(Figure.Name, "x", X, CompletedIterations,
                    Figure.IsGuide));
            RectFigure.Y =
                DataStorage.Add(new ScalarExpression(Figure.Name, "y", Y, CompletedIterations,
                    Figure.IsGuide));
            RectFigure.Width =
                DataStorage.Add(new ScalarExpression(Figure.Name, "width", Width, CompletedIterations,
                    Figure.IsGuide));
            RectFigure.Height =
                DataStorage.Add(new ScalarExpression(Figure.Name, "height", Height, CompletedIterations,
                    Figure.IsGuide));
        }

        public override void CopyStaticFigure()
        {
            var rf = new RectFigure("staticrect")
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