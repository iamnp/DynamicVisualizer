using System.Windows;
using System.Windows.Media;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Steps;

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
            {
                return null;
            }
            var w = new ScalarExpression(Name, "a", Name + ".x + " + Name + ".width", true);
            var h = new ScalarExpression(Name, "a", Name + ".y + " + Name + ".height", true);

            var x = new ScalarExpression(Name, "a", Name + ".x", true);
            var y = new ScalarExpression(Name, "a", Name + ".y", true);
            var name = Name + "'s ";
            Start = new Magnet(x, y, name + "start");
            End = new Magnet(w, h, name + "end");
            Center = new Magnet(new ScalarExpression(Name, "a", Name + ".x + (" + Name + ".width/2)", true),
                new ScalarExpression(Name, "a", Name + ".y + (" + Name + ".height/2)", true), name + "center");

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
            {
                dc.DrawLine(GuidePen, new Point(x, y), new Point(x + width, y + height));
            }
            else
            {
                dc.DrawLine(IsSelected ? SelectionPen : StrokePen,
                    new Point(x, y), new Point(x + width, y + height));
            }
        }

        public override bool IsMouseOver(double x, double y)
        {
            // start point
            var point = new Point(X.CachedValue.AsDouble, Y.CachedValue.AsDouble);
            var dx = point.X - x;
            var dy = point.Y - y;
            if (dx * dx + dy * dy <= StepManager.ThresholdSquared)
            {
                return true;
            }

            // center point
            point = new Point(X.CachedValue.AsDouble + Width.CachedValue.AsDouble / 2.0,
                Y.CachedValue.AsDouble + Height.CachedValue.AsDouble / 2.0);
            dx = point.X - x;
            dy = point.Y - y;
            if (dx * dx + dy * dy <= StepManager.ThresholdSquared)
            {
                return true;
            }

            // утв point
            point = new Point(X.CachedValue.AsDouble + Width.CachedValue.AsDouble,
                Y.CachedValue.AsDouble + Height.CachedValue.AsDouble);
            dx = point.X - x;
            dy = point.Y - y;
            if (dx * dx + dy * dy <= StepManager.ThresholdSquared)
            {
                return true;
            }

            return false;
        }

        public override Point PosInside(double x, double y)
        {
            return new Point(x - X.CachedValue.AsDouble, y - Y.CachedValue.AsDouble);
        }
    }
}