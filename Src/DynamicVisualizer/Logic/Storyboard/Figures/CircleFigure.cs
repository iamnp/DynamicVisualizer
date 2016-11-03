using System.Windows;
using System.Windows.Media;
using DynamicVisualizer.Logic.Expressions;

namespace DynamicVisualizer.Logic.Storyboard.Figures
{
    public class CircleFigure : Figure
    {
        public ScalarExpression Radius;
        public ScalarExpression X;
        public ScalarExpression Y;

        public CircleFigure(string name, bool isGuide = false)
        {
            Name = name;
            IsGuide = isGuide;
        }

        public override FigureType Type => FigureType.Circle;

        public override void Draw(DrawingContext dc)
        {
            var r = Radius.CachedValue.AsDouble;
            if (IsGuide)
                dc.DrawEllipse(null, GuidePen,
                    new Point(X.CachedValue.AsDouble, Y.CachedValue.AsDouble), r, r);
            else
                dc.DrawEllipse(Brushes.Green, IsSelected ? SelectionPen : StrokePen,
                    new Point(X.CachedValue.AsDouble, Y.CachedValue.AsDouble), r, r);
        }

        public override bool IsMouseOver(double x, double y)
        {
            var dx = x - X.CachedValue.AsDouble;
            var dy = y - Y.CachedValue.AsDouble;
            return dx*dx + dy*dy <= Radius.CachedValue.AsDouble*Radius.CachedValue.AsDouble;
        }
    }
}