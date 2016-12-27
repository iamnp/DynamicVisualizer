using System.Windows;
using System.Windows.Media;
using DynamicVisualizer.Expressions;

namespace DynamicVisualizer.Figures
{
    public class RectFigure : Figure
    {
        public Magnet Bottom;
        public Magnet BottomLeft;
        public Magnet BottomRight;
        public Magnet Center;
        public ScalarExpression Height;
        public Magnet Left;
        public Magnet Right;
        public Magnet Top;
        public Magnet TopLeft;
        public Magnet TopRight;
        public ScalarExpression Width;
        public ScalarExpression X;
        public ScalarExpression Y;

        public RectFigure(string name)
        {
            Name = name;
        }

        public RectFigure()
        {
            IsStatic = true;
        }

        public override FigureType Type => FigureType.Rect;

        public override Magnet[] GetMagnets()
        {
            if (IsStatic)
            {
                return null;
            }
            var w = new ScalarExpression(Name, "a", Name + ".x + " + Name + ".width", true);
            var h = new ScalarExpression(Name, "a", Name + ".y + " + Name + ".height", true);
            var wover2 = new ScalarExpression(Name, "a", Name + ".x + (" + Name + ".width/2)", true);
            var hover2 = new ScalarExpression(Name, "a", Name + ".y + (" + Name + ".height/2)", true);

            var x = new ScalarExpression(Name, "a", Name + ".x", true);
            var y = new ScalarExpression(Name, "a", Name + ".y", true);
            var name = Name + "'s ";
            TopLeft = new Magnet(x, y, name + "top-left");
            BottomLeft = new Magnet(x, h, name + "bottom-left");
            TopRight = new Magnet(w, y, name + "top-right");
            BottomRight = new Magnet(w, h, name + "bottom-right");
            Center = new Magnet(wover2, hover2, name + "center");
            Left = new Magnet(x, hover2, name + "left");
            Right = new Magnet(w, hover2, name + "right");
            Top = new Magnet(wover2, y, name + "top");
            Bottom = new Magnet(wover2, h, name + "bottom");

            return new[]
            {
                TopLeft,
                BottomLeft,
                TopRight,
                BottomRight,
                Center,
                Left,
                Right,
                Top,
                Bottom
            };
        }

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
            {
                dc.DrawRectangle(null, GuidePen, new Rect(x, y, width, height));
            }
            else
            {
                dc.DrawRectangle(Brushes.Green, StrokePen,
                    new Rect(x, y, width, height));
            }
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

        public override Point PosInside(double x, double y)
        {
            return new Point(x - X.CachedValue.AsDouble, y - Y.CachedValue.AsDouble);
        }
    }
}