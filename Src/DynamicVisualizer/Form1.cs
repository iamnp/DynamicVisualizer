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
        private readonly MainGraphicOutput _mainGraphics;
        private Point _downPos;

        private TransformStep _nowMoving;
        private double _offsetX = double.NaN;
        private double _offsetY = double.NaN;
        private Figure _selected;

        public Form1()
        {
            InitializeComponent();

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
        }

        private void DrawScene(DrawingContext dc)
        {
            dc.PushTransform(_canvasTranslate);
            dc.DrawRectangle(null, _canvasStroke, _canvasRect);
            dc.DrawRectangle(Brushes.White, null, _canvasRect);

            foreach (var figure in Timeline.Figures)
                figure.Draw(dc);
        }

        private void MainGraphicsOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                _figureDrawer.Finish();
            if (e.ChangedButton == MouseButton.Right)
                _offsetX = _offsetY = double.NaN;
            RedrawNeeded();
        }

        private void MainGraphicsOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            _figureDrawer.Finish();
            _offsetX = _offsetY = double.NaN;
            RedrawNeeded();
        }

        private void MainGraphicsOnMouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(_mainGraphics).Move(-CanvasOffsetX, -CanvasOffsetY);
            if (e.LeftButton == MouseButtonState.Pressed)
                _figureDrawer.Move(pos);
            if ((e.RightButton == MouseButtonState.Pressed) && (_selected != null))
                switch (_selected.Type)
                {
                    case Figure.FigureType.Rect:
                        var rf = (RectFigure) _selected;
                        if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
                        {
                            _offsetX = _downPos.X - rf.X.CachedValue.AsDouble;
                            _offsetY = _downPos.Y - rf.Y.CachedValue.AsDouble;
                        }

                        if (_nowMoving == null)
                        {
                            _nowMoving = new MoveRectStep(rf, pos.X - _offsetX, pos.Y - _offsetY);
                            Timeline.Insert(_nowMoving,
                                Timeline.CurrentStepIndex == -1 ? 0 : Timeline.CurrentStepIndex + 1);
                        }
                        else
                        {
                            switch (_nowMoving.StepType)
                            {
                                case TransformStep.TransformStepType.MoveRect:
                                    ((MoveRectStep) _nowMoving).Move(pos.X - _offsetX, pos.Y - _offsetY);
                                    break;
                            }
                        }
                        break;
                    case Figure.FigureType.Circle:
                        var cf = (CircleFigure) _selected;
                        if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
                        {
                            _offsetX = _downPos.X - cf.X.CachedValue.AsDouble;
                            _offsetY = _downPos.Y - cf.Y.CachedValue.AsDouble;
                        }

                        if (_nowMoving == null)
                        {
                            _nowMoving = new MoveCircleStep(cf, pos.X - _offsetX, pos.Y - _offsetY);
                            Timeline.Insert(_nowMoving,
                                Timeline.CurrentStepIndex == -1 ? 0 : Timeline.CurrentStepIndex + 1);
                        }
                        else
                        {
                            switch (_nowMoving.StepType)
                            {
                                case TransformStep.TransformStepType.MoveCircle:
                                    ((MoveCircleStep) _nowMoving).Move(pos.X - _offsetX, pos.Y - _offsetY);
                                    break;
                            }
                        }
                        break;
                }
            RedrawNeeded();
        }

        private void MainGraphicsOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _downPos = e.GetPosition(_mainGraphics).Move(-CanvasOffsetX, -CanvasOffsetY);
            if (e.ChangedButton == MouseButton.Left)
                _figureDrawer.Start(_downPos);
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
                    if (f.IsMouseOver(_downPos.X, _downPos.Y))
                    {
                        _selected = f;
                        if ((_selected == Timeline.CurrentStep.Figure) &&
                            ((Timeline.CurrentStep is MoveRectStep && (_selected.Type == Figure.FigureType.Rect))
                             || (Timeline.CurrentStep is MoveCircleStep && (_selected.Type == Figure.FigureType.Circle))))
                            _nowMoving = (TransformStep) Timeline.CurrentStep;
                        else
                            _nowMoving = null;
                        _selected.IsSelected = true;
                        break;
                    }
                }
            }
            RedrawNeeded();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataStorage.Add(new ScalarExpression("data", "width", ((int) numericUpDown1.Value).ToString()));
            var s = new DrawRectStep("10", "10", "50", "50");
            Timeline.Insert(s);
            Timeline.Insert(new ScaleRectStep(s.RectFigure, ScaleRectStep.ScalingSide.Left, 0.5));
            Timeline.Insert(new ScaleRectStep(s.RectFigure, ScaleRectStep.ScalingSide.Top, 0.5));


            RedrawNeeded();
        }

        private IEnumerable<string> GetData()
        {
            for (var i = 1; i <= 10; ++i)
                yield return i + "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataStorage.Add(new ScalarExpression("data", "canvasHeight", CanvasHeight.ToString()));
            DataStorage.Add(new ScalarExpression("data", "canvasWidth", CanvasWidth.ToString()));
            DataStorage.AddArrayExpression("data", "height", GetData().ToArray());
            DataStorage.Add(new ScalarExpression("data", "count", "len(height)"));
            var len = (int) DataStorage.GetScalarExpression("data.count").CachedValue.AsDouble;

            var drawGuide = new DrawRectStep("0", "data.canvasHeight", "data.canvasWidth/data.count",
                "-data.canvasHeight", true);
            Timeline.Insert(drawGuide);

            var drawBar = new DrawRectStep("rect1.x", "rect1.y", "rect1.width", "rect1.height");
            drawBar.MakeIterable(len);
            Timeline.Insert(drawBar);

            var scaleBarHeight = new ScaleRectStep(drawBar.RectFigure, ScaleRectStep.ScalingSide.Top,
                "data.height / max(data.height)");
            scaleBarHeight.MakeIterable(len);
            Timeline.Insert(scaleBarHeight);

            var moveGuide = new MoveRectStep(drawGuide.RectFigure, "rect2.x + rect2.width", "rect2.y");
            moveGuide.MakeIterable(len);
            Timeline.Insert(moveGuide);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DataStorage.GetScalarExpression("data.width").SetRawExpression(((int) numericUpDown1.Value).ToString());
            RedrawNeeded();
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
    }
}