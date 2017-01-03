using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DynamicVisualizer.Figures
{
    public abstract class Figure
    {
        public enum FigureType
        {
            Rect,
            Ellipse,
            Line,
            Text
        }

        protected readonly Pen GuidePen = new Pen(Brushes.CornflowerBlue, 2);

        public readonly List<Figure> StaticLoopFigures = new List<Figure>();
        protected readonly Pen StrokePen = new Pen(Brushes.Black, 2);
        public FigureColor FigureColor;
        public int IndexInLoop = -1;
        public bool IsGuide;
        public bool IsSelected;
        public bool IsStatic;
        public string Name { get; protected set; }
        public abstract FigureType Type { get; }

        public abstract Magnet[] GetMagnets();

        public abstract void Draw(DrawingContext dc);
        public abstract bool IsMouseOver(double x, double y);
        public abstract Point PosInside(double x, double y);
    }
}