using System.Windows;
using System.Windows.Media;
using DynamicVisualizer.Logic.Expressions;

namespace DynamicVisualizer.Logic.Storyboard.Figures
{
    public class RectFigure : Figure
    {
        private static int _count = 1;
        public ScalarExpression Height;
        public ScalarExpression Width;
        public ScalarExpression X;
        public ScalarExpression Y;

        public RectFigure()
        {
            Name = "rect" + _count++;
        }

        public RectFigure(bool isGuide) : this()
        {
            IsGuide = isGuide;
        }

        public override FigureType Type => FigureType.Rect;

        public override void Draw(DrawingContext dc)
        {
            var x = X.CachedValue.AsDouble;
            var width = Width.CachedValue.AsDouble;
            if (width < 0)
            {
                width = -width;
                x = x - width;
            }

            var y = Y.CachedValue.AsDouble;
            var height = Height.CachedValue.AsDouble;
            if (height < 0)
            {
                height = -height;
                y = y - height;
            }

            if (IsGuide)
                dc.DrawRectangle(null, new Pen(Brushes.CornflowerBlue, 3), new Rect(x, y, width, height));
            else
                dc.DrawRectangle(Brushes.Green, IsSelected ? new Pen(Brushes.Yellow, 3) : new Pen(Brushes.Black, 1),
                    new Rect(x, y, width, height));
        }

        public override bool IsMouseOver(double x, double y)
        {
            var x1 = X.CachedValue.AsDouble;
            var y1 = Y.CachedValue.AsDouble;
            var x2 = x1 + Width.CachedValue.AsDouble;
            var y2 = y1 + Height.CachedValue.AsDouble;

            if (x1 > x2)
            {
                var t = x1;
                x1 = x2;
                x2 = t;
            }
            if (y1 > y2)
            {
                var t = y1;
                y1 = y2;
                y2 = t;
            }

            return (x >= x1) && (x <= x2) && (y >= y1) && (y <= y2);
        }
    }
}