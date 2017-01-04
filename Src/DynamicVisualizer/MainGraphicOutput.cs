using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace DynamicVisualizer
{
    internal class MainGraphicOutput : Control
    {
        public Action<DrawingContext> DrawingFunc;

        //public MainGraphicOutput()
        //{
        //    SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
        //}

        protected override void OnRender(DrawingContext dc)
        {
            DrawingFunc?.Invoke(dc);
        }
    }
}