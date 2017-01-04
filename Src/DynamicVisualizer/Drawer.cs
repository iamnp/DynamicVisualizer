using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DynamicVisualizer.Steps;

namespace DynamicVisualizer
{
    internal static class Drawer
    {
        public const int CanvasWidth = 800;
        public const int CanvasHeight = 600;
        public const int CanvasOffsetX = 100;
        public const int CanvasOffsetY = 50;
        public static readonly Rect HostRect = new Rect(0, 0, 1000, 700);
        public static readonly Rect CanvasRect = new Rect(0, 0, CanvasWidth, CanvasHeight);
        public static readonly Pen CanvasStroke = new Pen(Brushes.Gray, 1);
        public static readonly TranslateTransform CanvasTranslate = new TranslateTransform(CanvasOffsetX, CanvasOffsetY);
        public static bool DrawMagnets;

        private static DrawingVisual _savedScene;

        public static byte[] GetScenePngBytes()
        {
            var rtb = new RenderTargetBitmap(CanvasWidth, CanvasHeight, 96, 96, PixelFormats.Default);

            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                DrawSceneForExport(dc);
            }
            rtb.Render(dv);

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));
            var ms = new MemoryStream();
            pngEncoder.Save(ms);
            ms.Close();
            return ms.ToArray();
        }

        public static void DrawSceneForExport(DrawingContext dc)
        {
            dc.DrawRectangle(Brushes.White, null, CanvasRect);

            foreach (var figure in StepManager.Figures)
            {
                if (!figure.IsGuide)
                {
                    figure.Draw(dc);
                }
            }
        }

        private static void DrawFigures(DrawingContext dc, bool drawMagnets)
        {
            if (drawMagnets)
            {
                foreach (var magnet in StepManager.CanvasMagnets)
                {
                    magnet.Draw(dc, false);
                }
            }

            foreach (var figure in StepManager.Figures)
            {
                figure.Draw(dc);
            }

            if (drawMagnets)
            {
                foreach (var figure in StepManager.Figures)
                {
                    if (figure.IsSelected || DrawMagnets)
                    {
                        if (figure.IsSelected)
                        {
                            foreach (var magnet in figure.GetMagnets())
                            {
                                magnet.Draw(dc, true);
                            }
                        }
                        else
                        {
                            var magnets = figure.GetMagnets();
                            if (magnets == null)
                            {
                                continue;
                            }
                            foreach (var magnet in magnets)
                            {
                                magnet.Draw(dc, false);
                            }
                        }
                    }
                }
            }
        }

        public static void SaveCurrentScene()
        {
            _savedScene = new DrawingVisual();
            using (var dc = _savedScene.RenderOpen())
            {
                dc.DrawRectangle(Brushes.LightGray, null, HostRect);
                dc.PushTransform(CanvasTranslate);
                dc.DrawRectangle(null, CanvasStroke, CanvasRect);
                DrawSceneForExport(dc);
            }
        }

        public static void DeleteCurrentScene()
        {
            _savedScene = null;
        }

        public static void DrawScene(DrawingContext dc)
        {
            if (_savedScene == null)
            {
                dc.DrawRectangle(Brushes.LightGray, null, HostRect);
                dc.PushTransform(CanvasTranslate);
                dc.DrawRectangle(null, CanvasStroke, CanvasRect);
                dc.DrawRectangle(Brushes.White, null, CanvasRect);
            }
            else
            {
                dc.DrawDrawing(_savedScene.Drawing);
                dc.PushTransform(CanvasTranslate);
            }
            DrawFigures(dc, true);
        }
    }
}