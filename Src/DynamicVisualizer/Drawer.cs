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
        public static int CanvasOffsetX = 100;
        public static int CanvasOffsetY = 50;
        public static Rect HostRect = new Rect(0, 0, 1000, 700);
        private static readonly Rect CanvasRect = new Rect(0, 0, CanvasWidth, CanvasHeight);
        public static bool DrawMagnets;

        public static TranslateTransform CanvasTranslate = new TranslateTransform(CanvasOffsetX, CanvasOffsetY);
        private static readonly Pen CanvasStroke = new Pen(Brushes.Gray, 1);
        private static readonly Brush WhiteBrush = Brushes.White;
        public static readonly Brush BlackBrush = Brushes.Black;
        public static readonly Brush DeepSkyBlueBrush = Brushes.DeepSkyBlue;
        public static readonly Brush YellowBrush = Brushes.Yellow;
        private static readonly Brush LightGrayBrush = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
        public static readonly Pen GuidePen = new Pen(Brushes.CornflowerBlue, 2);
        public static readonly Pen StrokePen = new Pen(Brushes.Black, 2);
        public static readonly Pen BlackPen = new Pen(Brushes.Black, 1);

        private static DrawingVisual _savedScene;

        static Drawer()
        {
            CanvasTranslate.Freeze();
            CanvasStroke.Freeze();
            WhiteBrush.Freeze();
            BlackBrush.Freeze();
            DeepSkyBlueBrush.Freeze();
            YellowBrush.Freeze();
            LightGrayBrush.Freeze();
            GuidePen.Freeze();
            StrokePen.Freeze();
            BlackPen.Freeze();
        }

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
            dc.DrawRectangle(WhiteBrush, null, CanvasRect);

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
                dc.DrawRectangle(LightGrayBrush, null, HostRect);
                dc.PushTransform(CanvasTranslate);
                dc.DrawRectangle(null, CanvasStroke, CanvasRect);
                DrawSceneForExport(dc);
            }
            _savedScene.Drawing?.Freeze();
        }

        public static void DeleteCurrentScene()
        {
            _savedScene = null;
        }

        public static void DrawScene(DrawingContext dc)
        {
            if (_savedScene == null)
            {
                dc.DrawRectangle(LightGrayBrush, null, HostRect);
                dc.PushTransform(CanvasTranslate);
                dc.DrawRectangle(null, CanvasStroke, CanvasRect);
                dc.DrawRectangle(WhiteBrush, null, CanvasRect);
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