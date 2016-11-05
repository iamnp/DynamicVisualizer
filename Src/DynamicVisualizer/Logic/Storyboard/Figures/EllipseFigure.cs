using System.Windows;
using System.Windows.Media;
using DynamicVisualizer.Logic.Expressions;

namespace DynamicVisualizer.Logic.Storyboard.Figures
{
    public class EllipseFigure : Figure
    {
        public ScalarExpression Radius1;
        public ScalarExpression Radius2;
        public ScalarExpression X;
        public ScalarExpression Y;

        public EllipseFigure(string name, bool isGuide = false)
        {
            Name = name;
            IsGuide = isGuide;
        }

        public override FigureType Type => FigureType.Circle;

        public override void Draw(DrawingContext dc)
        {
            var r1 = Radius1.CachedValue.AsDouble;
            var r2 = Radius2.CachedValue.AsDouble;
            if (IsGuide)
                dc.DrawEllipse(null, GuidePen,
                    new Point(X.CachedValue.AsDouble, Y.CachedValue.AsDouble), r1, r2);
            else
                dc.DrawEllipse(Brushes.Green, IsSelected ? SelectionPen : StrokePen,
                    new Point(X.CachedValue.AsDouble, Y.CachedValue.AsDouble), r1, r2);
        }

        public override bool IsMouseOver(double x, double y)
        {
            var dx = x - X.CachedValue.AsDouble;
            var dy = y - Y.CachedValue.AsDouble;
            var r1 = Radius1.CachedValue.AsDouble;
            var r2 = Radius2.CachedValue.AsDouble;

            return dx*dx/(r1*r1) + dy*dy/(r2*r2) <= 1.0;
        }

        public override Point PosInside(double x, double y)
        {
            return new Point(x - (X.CachedValue.AsDouble - Radius1.CachedValue.AsDouble),
                y - (Y.CachedValue.AsDouble - -Radius2.CachedValue.AsDouble));
        }
    }
}