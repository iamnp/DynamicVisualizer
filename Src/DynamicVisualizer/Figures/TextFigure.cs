using System;
using System.Windows;
using System.Windows.Media;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Steps;

namespace DynamicVisualizer.Figures
{
    public class TextFigure : Figure
    {
        public Magnet Center;
        public Magnet End;
        public FigureText FigureText;
        public ScalarExpression Height;
        public Magnet Start;
        public ScalarExpression Width;
        public ScalarExpression X;
        public ScalarExpression Y;

        public TextFigure(string name)
        {
            Name = name;
            FigureColor = new FigureColor(0, 0.0, 0, 1);
            FigureText = new FigureText("\"Hi!\"", "20");
        }

        public TextFigure() : this(null)
        {
            IsStatic = true;
        }

        public override FigureType Type => FigureType.Text;

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
            if (StaticLoopFigures.Count > 0)
            {
                return;
            }

            var width = Width.CachedValue.AsDouble;
            var height = Height.CachedValue.AsDouble;
            var len = Math.Sqrt(width * width + height * height);
            var angle = Math.Atan2(height, width);

            FigureText.FormattedText.MaxTextWidth = len;
            FigureText.FormattedText.SetForegroundBrush(FigureColor.Brush);

            var x = X.CachedValue.AsDouble;
            var y = Y.CachedValue.AsDouble;
            var t = new RotateTransform(angle * 180.0 / Math.PI, x, y);
            t.Freeze();
            dc.PushTransform(t);
            dc.DrawText(FigureText.FormattedText, new Point(x, y - FigureText.FormattedText.Height));
            dc.Pop();
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

            // end point
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