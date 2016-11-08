using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using DynamicVisualizer.Logic.Expressions;
using DynamicVisualizer.Logic.Storyboard;
using DynamicVisualizer.Logic.Storyboard.Figures;
using DynamicVisualizer.Logic.Storyboard.Steps;
using DynamicVisualizer.Logic.Storyboard.Steps.Draw;
using DynamicVisualizer.Logic.Storyboard.Steps.Transform;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using SystemColors = System.Drawing.SystemColors;

namespace DynamicVisualizer
{
    public partial class Form1 : Form
    {
        private const int CanvasWidth = 800;
        private const int CanvasHeight = 600;
        private const int CanvasOffsetX = 100;
        private const int CanvasOffsetY = 50;
        private readonly Rect _canvasRect = new Rect(0, 0, CanvasWidth, CanvasHeight);
        private readonly Pen _canvasStroke = new Pen(Brushes.Gray, 1);
        private readonly TranslateTransform _canvasTranslate = new TranslateTransform(CanvasOffsetX, CanvasOffsetY);
        private readonly FigureDrawer _figureDrawer = new FigureDrawer();
        private readonly FigureMover _figureMover = new FigureMover();
        private readonly FigureScaler _figureScaler = new FigureScaler();
        private readonly Rect _hostRect = new Rect(0, 0, 1000, 700);
        private readonly MainGraphicOutput _mainGraphics;
        private Figure _selected;
        private TransformType _transformType = TransformType.Move;

        public Form1()
        {
            InitializeComponent();

            DataStorage.Add(new ScalarExpression("canvas", "height", CanvasHeight.ToString()));
            DataStorage.Add(new ScalarExpression("canvas", "width", CanvasWidth.ToString()));
            DataStorage.Add(new ScalarExpression("canvas", "x", "0"));
            DataStorage.Add(new ScalarExpression("canvas", "y", "0"));

            var x1 = new ScalarExpression("a", "a", "canvas.x", true);
            var y1 = new ScalarExpression("a", "a", "canvas.y", true);
            var w = new ScalarExpression("a", "a", "canvas.width", true);
            var h = new ScalarExpression("a", "a", "canvas.height", true);

            Timeline.CanvasMagnets = new[]
            {
                new Magnet(x1, y1),
                new Magnet(x1, h),
                new Magnet(w, y1),
                new Magnet(w, h),
                new Magnet(new ScalarExpression("a", "a", "canvas.width/2", true),
                    new ScalarExpression("a", "a", "canvas.height/2", true))
            };

            _mainGraphics = new MainGraphicOutput {DrawingFunc = DrawScene};
            elementHost1.Child = _mainGraphics;
            _mainGraphics.MouseDown += MainGraphicsOnMouseDown;
            _mainGraphics.MouseMove += MainGraphicsOnMouseMove;
            _mainGraphics.MouseUp += MainGraphicsOnMouseUp;
            _mainGraphics.MouseLeave += MainGraphicsOnMouseLeave;

            stepsListControl1.RedrawNeeded += RedrawNeeded;
        }

        private void RedrawNeeded()
        {
            _mainGraphics.InvalidateVisual();
            stepsListControl1.ReSetText();
        }

        private void DrawScene(DrawingContext dc)
        {
            dc.DrawRectangle(Brushes.LightGray, null, _hostRect);
            dc.PushTransform(_canvasTranslate);
            dc.DrawRectangle(null, _canvasStroke, _canvasRect);
            dc.DrawRectangle(Brushes.White, null, _canvasRect);

            foreach (var magnet in Timeline.CanvasMagnets)
                dc.DrawEllipse(Brushes.Yellow, new Pen(Brushes.Black, 1),
                    new Point(magnet.X.CachedValue.AsDouble, magnet.Y.CachedValue.AsDouble),
                    4, 4);

            foreach (var figure in Timeline.Figures)
            {
                figure.Draw(dc);
                if (figure.IsSelected || _figureMover.NowMoving || _figureDrawer.NowDrawing || _figureScaler.NowScailing)
                    foreach (var magnet in figure.GetMagnets())
                        dc.DrawEllipse(Brushes.Yellow, new Pen(Brushes.Black, 1),
                            new Point(magnet.X.CachedValue.AsDouble, magnet.Y.CachedValue.AsDouble),
                            4, 4);
            }
        }

        private void MainGraphicsOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                _figureDrawer.Finish();
            if (e.ChangedButton == MouseButton.Right)
            {
                _figureMover.Reset();
                _figureScaler.Reset();
            }
            RedrawNeeded();
        }

        private void MainGraphicsOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            _figureDrawer.Finish();
            _figureMover.Reset();
            _figureScaler.Reset();
            RedrawNeeded();
        }

        private void MainGraphicsOnMouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(_mainGraphics).Move(-CanvasOffsetX, -CanvasOffsetY);
            if (e.LeftButton == MouseButtonState.Pressed)
                _figureDrawer.Move(pos);
            if ((e.RightButton == MouseButtonState.Pressed) && (_selected != null))
                switch (_transformType)
                {
                    case TransformType.Move:
                        _figureMover.Move(_selected, pos);
                        break;
                    case TransformType.Scale:
                        _figureScaler.Move(_selected, pos);
                        break;
                }

            RedrawNeeded();
        }

        private void MainGraphicsOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var downPos = e.GetPosition(_mainGraphics).Move(-CanvasOffsetX, -CanvasOffsetY);
            switch (_transformType)
            {
                case TransformType.Move:
                    _figureMover.SetDownPos(downPos);
                    break;
                case TransformType.Scale:
                    _figureScaler.SetDownPos(downPos);
                    break;
            }
            if (e.ChangedButton == MouseButton.Left)
                _figureDrawer.Start(downPos);
            if (e.ChangedButton == MouseButton.Right)
            {
                if (_selected != null)
                {
                    _selected.IsSelected = false;
                    _selected = null;
                }
                for (var i = Timeline.Figures.Count - 1; i >= 0; --i)
                {
                    var f = Timeline.Figures[i];
                    if (f.IsMouseOver(downPos.X, downPos.Y))
                    {
                        _selected = f;
                        _selected.IsSelected = true;
                        break;
                    }
                }
                if (_selected == null) label7.Visible = false;
                else
                {
                    label7.Visible = true;
                    label7.ForeColor = _selected.IsGuide ? SystemColors.ControlText : SystemColors.ControlDark;
                }
            }
            RedrawNeeded();
        }

        private IEnumerable<string> GetData()
        {
            for (var i = 1; i <= 10; ++i)
                yield return i + "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataStorage.AddArrayExpression("data", "height", GetData().ToArray());
            DataStorage.Add(new ScalarExpression("data", "count", "len(height)"));
            var len = (int) DataStorage.GetScalarExpression("data.count").CachedValue.AsDouble;

            var drawGuide = new DrawRectStep("0", "canvas.height", "canvas.width/data.count",
                "-canvas.height");
            drawGuide.Figure.IsGuide = true;
            Timeline.Insert(drawGuide);

            var drawBar = new DrawRectStep("rect1.x", "rect1.y", "rect1.width", "rect1.height");
            drawBar.MakeIterable(len);
            Timeline.Insert(drawBar);

            var scaleBarHeight = new ScaleRectStep(drawBar.RectFigure, ScaleRectStep.Side.Top,
                "data.height / (max(data.height))");
            scaleBarHeight.MakeIterable(len);
            Timeline.Insert(scaleBarHeight);

            var moveGuide = new MoveRectStep(drawGuide.RectFigure, "rect2.x + rect2.width", "rect2.y");
            moveGuide.MakeIterable(len);
            Timeline.Insert(moveGuide);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            _figureDrawer.DrawStepType = DrawStep.DrawStepType.DrawRect;
            label2.ForeColor = SystemColors.ControlText;
            label3.ForeColor = SystemColors.ControlDark;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            _figureDrawer.DrawStepType = DrawStep.DrawStepType.DrawCircle;
            label3.ForeColor = SystemColors.ControlText;
            label2.ForeColor = SystemColors.ControlDark;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            _transformType = TransformType.Move;
            label5.ForeColor = SystemColors.ControlText;
            label6.ForeColor = SystemColors.ControlDark;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            _transformType = TransformType.Scale;
            label6.ForeColor = SystemColors.ControlText;
            label5.ForeColor = SystemColors.ControlDark;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            _selected.IsGuide = !_selected.IsGuide;
            label7.ForeColor = _selected.IsGuide ? SystemColors.ControlText : SystemColors.ControlDark;
            RedrawNeeded();
        }
    }
}