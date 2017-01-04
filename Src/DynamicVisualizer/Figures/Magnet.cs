using System.Windows;
using System.Windows.Media;
using DynamicVisualizer.Expressions;

namespace DynamicVisualizer.Figures
{
    public class Magnet
    {
        public readonly string Def;
        public readonly ScalarExpression X;
        public readonly ScalarExpression Y;

        public Magnet(ScalarExpression x, ScalarExpression y, string def)
        {
            X = x;
            Y = y;
            Def = def;
        }

        public bool EqualExprStrings(Magnet a)
        {
            return (X.ExprString == a.X.ExprString) && (Y.ExprString == a.Y.ExprString);
        }

        public void Draw(DrawingContext dc, bool selected)
        {
            if (selected)
            {
                dc.DrawEllipse(Drawer.DeepSkyBlueBrush, Drawer.BlackPen,
                    new Point(X.CachedValue.AsDouble, Y.CachedValue.AsDouble),
                    4, 4);
            }
            else
            {
                dc.DrawEllipse(Drawer.YellowBrush, Drawer.BlackPen,
                    new Point(X.CachedValue.AsDouble, Y.CachedValue.AsDouble),
                    3, 3);
            }
        }
    }
}