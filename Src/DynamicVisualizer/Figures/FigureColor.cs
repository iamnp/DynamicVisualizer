using System.Windows.Media;
using DynamicVisualizer.Expressions;

namespace DynamicVisualizer.Figures
{
    public class FigureColor
    {
        private readonly ScalarExpression _a;
        private readonly ScalarExpression _b;
        private readonly ScalarExpression _g;
        private readonly ScalarExpression _r;
        public Brush Brush;
        public Pen Pen;
        public string StringExpr;

        public FigureColor(string r, string g, string b, string a, int index = 0)
        {
            _r = new ScalarExpression("a", "a", r, index, true);
            _g = new ScalarExpression("a", "a", g, index, true);
            _b = new ScalarExpression("a", "a", b, index, true);
            _a = new ScalarExpression("a", "a", a, index, true);
            Brush = new SolidColorBrush(Color);
            Pen = new Pen(Brush, 2);
            StringExpr = r + ";" + g + ";" + b + ";" + a;
        }


        public FigureColor(double r, double g, double b, double a, int index = 0)
            : this(r.Str(), g.Str(), b.Str(), a.Str(), index)
        {
        }

        private Color Color
        {
            get
            {
                if (_a.CachedValue.Empty || !_a.CachedValue.IsDouble || (_a.CachedValue.AsDouble < 0.0) ||
                    (_a.CachedValue.AsDouble > 1.0)
                    || _r.CachedValue.Empty || !_r.CachedValue.IsDouble || (_r.CachedValue.AsDouble < 0.0) ||
                    (_r.CachedValue.AsDouble > 1.0)
                    || _g.CachedValue.Empty || !_g.CachedValue.IsDouble || (_g.CachedValue.AsDouble < 0.0) ||
                    (_g.CachedValue.AsDouble > 1.0)
                    || _b.CachedValue.Empty || !_b.CachedValue.IsDouble || (_b.CachedValue.AsDouble < 0.0) ||
                    (_b.CachedValue.AsDouble > 1.0))
                {
                    return Color.FromArgb(0, 0, 0, 0);
                }

                return Color.FromArgb((byte) (_a.CachedValue.AsDouble * 255), (byte) (_r.CachedValue.AsDouble * 255),
                    (byte) (_g.CachedValue.AsDouble * 255), (byte) (_b.CachedValue.AsDouble * 255));
            }
        }

        public void Parse(string s)
        {
            StringExpr = s;
            var p = s.Split(';');
            if (p.Length != 4)
            {
                return;
            }

            _r.SetRawExpression(p[0]);
            _g.SetRawExpression(p[1]);
            _b.SetRawExpression(p[2]);
            _a.SetRawExpression(p[3]);
            Brush = new SolidColorBrush(Color);
            Pen = new Pen(Brush, 2);
        }

        public void SetIndex(int index)
        {
            _r.IndexInArray = index;
            _g.IndexInArray = index;
            _b.IndexInArray = index;
            _a.IndexInArray = index;

            _r.Recalculate();
            _g.Recalculate();
            _b.Recalculate();
            _a.Recalculate();
        }

        public FigureColor Copy()
        {
            return new FigureColor(_r.ExprString, _g.ExprString, _b.ExprString, _a.ExprString, _r.IndexInArray);
        }
    }
}