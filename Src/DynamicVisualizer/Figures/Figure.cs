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
            Line
        }

        protected readonly Pen GuidePen = new Pen(Brushes.CornflowerBlue, 3);
        protected readonly Pen SelectionPen = new Pen(Brushes.Yellow, 3);

        public readonly List<Figure> StaticLoopFigures = new List<Figure>();
        protected readonly Pen StrokePen = new Pen(Brushes.Black, 1);
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