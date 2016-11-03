using System.Windows;
using System.Windows.Media;
using DynamicVisualizer.Logic.Expressions;

namespace DynamicVisualizer.Logic.Storyboard.Figures
{
    public class CircleFigure : Figure
    {
        private static int _count = 1;
        public ScalarExpression Radius;
        public ScalarExpression X;
        public ScalarExpression Y;

        public CircleFigure()
        {
            Name = "circle" + _count++;
        }

        public CircleFigure(bool isGuide) : this()
        {
            IsGuide = isGuide;
        }

        public override FigureType Type => FigureType.Circle;

        public override void Draw(DrawingContext dc)
        {
            var r = Radius.CachedValue.AsDouble;
            if (IsGuide)
                dc.DrawEllipse(null, new Pen(Brushes.CornflowerBlue, 3),
                    new Point(X.CachedValue.AsDouble, Y.CachedValue.AsDouble), r, r);
            else
                dc.DrawEllipse(Brushes.Green, IsSelected ? new Pen(Brushes.Yellow, 3) : new Pen(Brushes.Black, 1),
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