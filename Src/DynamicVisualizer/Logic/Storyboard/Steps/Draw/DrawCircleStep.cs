using DynamicVisualizer.Logic.Expressions;
using DynamicVisualizer.Logic.Storyboard.Figures;

namespace DynamicVisualizer.Logic.Storyboard.Steps.Draw
{
    public class DrawCircleStep : DrawStep
    {
        public readonly CircleFigure RectFigure;
        public string Radius;
        public string X;
        public string Y;

        private DrawCircleStep(bool isGuide)
        {
            Figure = new CircleFigure(isGuide);
            RectFigure = (CircleFigure) Figure;
        }

        public DrawCircleStep(string x, string y, string radius, bool isGuide = false) : this(isGuide)
        {
            ReInit(x, y, radius);
        }

        public DrawCircleStep(double x, double y, double radius, bool isGuide = false) : this(isGuide)
        {
            ReInit(x, y, radius);
        }

        public override DrawStepType StepType => DrawStepType.DrawCircle;

        public void ReInit(string x, string y, string radius)
        {
            X = x;
            Y = y;
            Radius = radius;
            Apply();
        }

        public void ReInit(double x, double y, double radius)
        {
            X = x.Str();
            Y = y.Str();
            Radius = radius.Str();
            Apply();
        }

        public override void Apply()
        {
            if (!Applied) Timeline.Figures.Add(RectFigure);
            Applied = true;

            RectFigure.X = DataStorage.Add(new ScalarExpression(Figure.Name, "x", X, Figure.IsGuide));
            RectFigure.Y = DataStorage.Add(new ScalarExpression(Figure.Name, "y", Y, Figure.IsGuide));
            RectFigure.Radius =
                DataStorage.Add(new ScalarExpression(Figure.Name, "radius", Radius, Figure.IsGuide));
        }

        public override void IterateNext()
        {
            RectFigure.X =
                DataStorage.Add(new ScalarExpression(Figure.Name, "x", X, CompletedIterations,
                    Figure.IsGuide));
            RectFigure.Y =
                DataStorage.Add(new ScalarExpression(Figure.Name, "y", Y, CompletedIterations,
                    Figure.IsGuide));
            RectFigure.Radius =
                DataStorage.Add(new ScalarExpression(Figure.Name, "radius", Radius, CompletedIterations,
                    Figure.IsGuide));
        }

        public override void CopyStaticFigure()
        {
            var rf = new CircleFigure
            {
                X = new ScalarExpression("a", "a", RectFigure.X.CachedValue.Str),
                Y = new ScalarExpression("a", "a", RectFigure.Y.CachedValue.Str),
                Radius = new ScalarExpression("a", "a", RectFigure.Radius.CachedValue.Str)
            };
            Figure.StaticLoopFigures.Add(rf);
            Timeline.Figures.Add(rf);
        }
    }
}