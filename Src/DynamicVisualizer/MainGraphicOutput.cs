using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace DynamicVisualizer
{
    internal class MainGraphicOutput : Control
    {
        public Action<DrawingContext, bool> DrawingFunc;

        protected override void OnRender(DrawingContext dc)
        {
            DrawingFunc?.Invoke(dc, false);
        }
    }
}