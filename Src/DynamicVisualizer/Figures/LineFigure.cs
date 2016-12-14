using System.Windows;
using System.Windows.Media;
using DynamicVisualizer.Expressions;

namespace DynamicVisualizer.Figures
{
    public class LineFigure : Figure
    {
        public Magnet Center;
        public Magnet End;
        public ScalarExpression Height;
        public Magnet Start;
        public ScalarExpression Width;
        public ScalarExpression X;
        public ScalarExpression Y;

        public LineFigure(string name)
        {
            Name = name;
        }

        public LineFigure()
        {
            IsStatic = true;
        }

        public override FigureType Type => FigureType.Line;

        public override Magnet[] GetMagnets()
        {
            if (IsStatic)
                return null;
            var w = new ScalarExpression(Name, "a", Name + ".x + " + Name + ".width", true);
            var h = new ScalarExpression(Name, "a", Name + ".y + " + Name + ".height", true);

            var x = new ScalarExpression(Name, "a", Name + ".x", true);
            var y = new ScalarExpression(Name, "a", Name + ".y", true);

            Start = new Magnet(x, y);
            End = new Magnet(w, h);
            Center = new Magnet(new ScalarExpression(Name, "a", Name + ".x + (" + Name + ".width/2)", true),
                new ScalarExpression(Name, "a", Name + ".y + (" + Name + ".height/2)", true));

            return new[]
            {
                Start,
                End,
                Center
            };
        }

        public override void Draw(DrawingContext dc)
        {
            var x = X.CachedValue.AsDouble;
            var width = Width.CachedValue.AsDouble;

            var y = Y.CachedValue.AsDouble;
            var height = Height.CachedValue.AsDouble;

            if (IsGuide)
                dc.DrawLine(GuidePen, new Point(x, y), new Point(x + width, y + height));
            else
                dc.DrawLine(IsSelected ? SelectionPen : StrokePen,
                    new Point(x, y), new Point(x + width, y + height));
        }

        public override bool IsMouseOver(double x, double y)
        {
            // TODO implement
            //var x1 = X.CachedValue.AsDouble;
            //var y1 = Y.CachedValue.AsDouble;
            //var x2 = x1 + Width.CachedValue.AsDouble;
            //var y2 = y1 + Height.CachedValue.AsDouble;

            //if (x1 > x2)
            //{
            //    var t = x1;
            //    x1 = x2;
            //    x2 = t;
            //}
            //if (y1 > y2)
            //{
            //    var t = y1;
            //    y1 = y2;
            //    y2 = t;
            //}

            //return (x >= x1) && (x <= x2) && (y >= y1) && (y <= y2);
            return false;
        }

        public override Point PosInside(double x, double y)
        {
            return new Point(x - X.CachedValue.AsDouble, y - Y.CachedValue.AsDouble);
        }
    }
}