using System.Collections.Generic;
using System.Windows.Media;

namespace DynamicVisualizer.Logic.Storyboard.Figures
{
    public abstract class Figure
    {
        public enum FigureType
        {
            Rect,
            Circle
        }

        public readonly List<Figure> StaticLoopFigures = new List<Figure>();
        public int IndexInLoop = -1;
        public bool IsGuide;
        public bool IsSelected;
        public string Name { get; protected set; }
        public abstract FigureType Type { get; }

        public abstract void Draw(DrawingContext dc);
        public abstract bool IsMouseOver(double x, double y);
    }
}