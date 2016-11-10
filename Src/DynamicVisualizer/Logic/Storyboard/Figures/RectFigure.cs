using System.Windows;
using System.Windows.Media;
using DynamicVisualizer.Logic.Expressions;

namespace DynamicVisualizer.Logic.Storyboard.Figures
{
    public class RectFigure : Figure
    {
        public Magnet BottomLeft;
        public Magnet BottomRight;
        public Magnet Center;
        public ScalarExpression Height;
        public Magnet TopLeft;
        public Magnet TopRight;
        public ScalarExpression Width;
        public ScalarExpression X;
        public ScalarExpression Y;

        public RectFigure(string name)
        {
            Name = name;
        }

        public override FigureType Type => FigureType.Rect;

        public override Magnet[] GetMagnets()
        {
            if (Name == "staticrect")
            {
                var w = new ScalarExpression(Name, "a", (X.CachedValue.AsDouble + Width.CachedValue.AsDouble).Str());
                var h = new ScalarExpression(Name, "a", (Y.CachedValue.AsDouble + Height.CachedValue.AsDouble).Str());

                var x = new ScalarExpression(Name, "a", X.CachedValue.AsDouble.Str());
                var y = new ScalarExpression(Name, "a", Y.CachedValue.AsDouble.Str());

                TopLeft = new Magnet(x, y);
                BottomLeft = new Magnet(x, h);
                TopRight = new Magnet(w, y);
                BottomRight = new Magnet(w, h);
                Center = new Magnet(
                    new ScalarExpression(Name, "a", (X.CachedValue.AsDouble + Width.CachedValue.AsDouble/2.0).Str()),
                    new ScalarExpression(Name, "a", (Y.CachedValue.AsDouble + Height.CachedValue.AsDouble/2.0).Str()));

                return new[]
                {
                    TopLeft,
                    BottomLeft,
                    TopRight,
                    BottomRight,
                    Center
                };
            }
            else
            {
                var w = new ScalarExpression(Name, "a", Name + ".x + " + Name + ".width", true);
                var h = new ScalarExpression(Name, "a", Name + ".y + " + Name + ".height", true);

                var x = new ScalarExpression(Name, "a", Name + ".x", true);
                var y = new ScalarExpression(Name, "a", Name + ".y", true);

                TopLeft = new Magnet(x, y);
                BottomLeft = new Magnet(x, h);
                TopRight = new Magnet(w, y);
                BottomRight = new Magnet(w, h);
                Center = new Magnet(new ScalarExpression(Name, "a", Name + ".x + (" + Name + ".width/2)", true),
                    new ScalarExpression(Name, "a", Name + ".y + (" + Name + ".height/2)", true));

                return new[]
                {
                    TopLeft,
                    BottomLeft,
                    TopRight,
                    BottomRight,
                    Center
                };
            }
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
                dc.DrawRectangle(null, GuidePen, new Rect(x, y, width, height));
            else
                dc.DrawRectangle(Brushes.Green, IsSelected ? SelectionPen : StrokePen,
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

        public override Point PosInside(double x, double y)
        {
            return new Point(x - X.CachedValue.AsDouble, y - Y.CachedValue.AsDouble);
        }
    }
}