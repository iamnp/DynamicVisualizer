using DynamicVisualizer.Logic.Expressions;
using DynamicVisualizer.Logic.Storyboard.Figures;

namespace DynamicVisualizer.Logic.Storyboard.Steps.Draw
{
    public class DrawCircleStep : DrawStep
    {
        private static int _count = 1;
        public readonly EllipseFigure RectFigure;
        public string Radius;
        public string X;
        public string Y;

        private DrawCircleStep()
        {
            Figure = new EllipseFigure("circle" + _count++);
            RectFigure = (EllipseFigure) Figure;
        }

        public DrawCircleStep(string x, string y, string radius) : this()
        {
            X = x;
            Y = y;
            ReInit(radius);
        }

        public DrawCircleStep(double x, double y, double radius) : this()
        {
            X = x.Str();
            Y = y.Str();
            ReInit(radius);
        }

        public override DrawStepType StepType => DrawStepType.DrawCircle;

        public void ReInit(string radius)
        {
            Radius = radius;
            Apply();
        }

        public void ReInit(double radius)
        {
            Radius = radius.Str();
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

        public override void Apply()
        {
            if (!Applied) Timeline.Figures.Add(RectFigure);
            Applied = true;

            RectFigure.X = DataStorage.Add(new ScalarExpression(Figure.Name, "x", X, Figure.IsGuide));
            RectFigure.Y = DataStorage.Add(new ScalarExpression(Figure.Name, "y", Y, Figure.IsGuide));
            RectFigure.Radius1 =
                DataStorage.Add(new ScalarExpression(Figure.Name, "radius1", Radius, Figure.IsGuide));
            RectFigure.Radius2 =
                DataStorage.Add(new ScalarExpression(Figure.Name, "radius2", Radius, Figure.IsGuide));

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
            RectFigure.Radius1 =
                DataStorage.Add(new ScalarExpression(Figure.Name, "radius1", Radius, CompletedIterations,
                    Figure.IsGuide));
            RectFigure.Radius2 =
                DataStorage.Add(new ScalarExpression(Figure.Name, "radius2", Radius, CompletedIterations,
                    Figure.IsGuide));
        }

        public override void CopyStaticFigure()
        {
            var rf = new EllipseFigure("staticcircle")
            {
                X = new ScalarExpression("a", "a", RectFigure.X.CachedValue.Str),
                Y = new ScalarExpression("a", "a", RectFigure.Y.CachedValue.Str),
                Radius1 = new ScalarExpression("a", "a", RectFigure.Radius1.CachedValue.Str),
                Radius2 = new ScalarExpression("a", "a", RectFigure.Radius2.CachedValue.Str)
            };
            Figure.StaticLoopFigures.Add(rf);
            Timeline.Figures.Add(rf);
        }
    }
}