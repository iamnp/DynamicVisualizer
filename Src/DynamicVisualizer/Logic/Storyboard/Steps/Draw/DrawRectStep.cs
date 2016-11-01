using DynamicVisualizer.Logic.Expressions;
using DynamicVisualizer.Logic.Storyboard.Figures;

namespace DynamicVisualizer.Logic.Storyboard.Steps.Draw
{
    public class DrawRectStep : DrawStep
    {
        public readonly RectFigure RectFigure;
        public string Height;
        public string Width;
        public string X;
        public string Y;

        private DrawRectStep(bool isGuide)
        {
            Figure = new RectFigure(isGuide);
            RectFigure = (RectFigure) Figure;
        }

        public DrawRectStep(string x, string y, string width, string height, bool isGuide = false) : this(isGuide)
        {
            ReInit(x, y, width, height);
        }

        public DrawRectStep(double x, double y, double width, double height, bool isGuide = false) : this(isGuide)
        {
            ReInit(x, y, width, height);
        }

        public override DrawStepType StepType => DrawStepType.DrawRect;

        public void ReInit(string x, string y, string width, string height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Apply();
        }

        public void ReInit(double x, double y, double width, double height)
        {
            X = x.Str();
            Y = y.Str();
            Width = width.Str();
            Height = height.Str();
            Apply();
        }

        public override void Apply()
        {
            if (!Applied) Timeline.Figures.Add(RectFigure);
            Applied = true;

            RectFigure.X = DataStorage.Add(new ScalarExpression(Figure.Name, "x", X, Figure.IsGuide));
            RectFigure.Y = DataStorage.Add(new ScalarExpression(Figure.Name, "y", Y, Figure.IsGuide));
            RectFigure.Width =
                DataStorage.Add(new ScalarExpression(Figure.Name, "width", Width, Figure.IsGuide));
            RectFigure.Height =
                DataStorage.Add(new ScalarExpression(Figure.Name, "height", Height, Figure.IsGuide));
        }

        public override void ApplyNextIteration()
        {
            if (CompletedIterations >= Iterations) return;

            if (!RectFigure.IsGuide)
            {
                var rf = new RectFigure
                {
                    X = new ScalarExpression("a", "a", RectFigure.X.CachedValue.Str),
                    Y = new ScalarExpression("a", "a", RectFigure.Y.CachedValue.Str),
                    Width = new ScalarExpression("a", "a", RectFigure.Width.CachedValue.Str),
                    Height = new ScalarExpression("a", "a", RectFigure.Height.CachedValue.Str)
                };
                Figure.StaticLoopFigures.Add(rf);
                Timeline.Figures.Add(rf);
            }

            if (CompletedIterations++ >= Iterations) return;

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
    }
}