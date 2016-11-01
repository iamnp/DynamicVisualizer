using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
using Application = System.Windows.Forms.Application;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace DynamicVisualizer
{
    public partial class Form1 : Form
    {
        private const int CanvasWidth = 800;
        private const int CanvasHeight = 600;
        private const int CanvasOffsetX = 100;
        private const int CanvasOffsetY = 50;
        private readonly MainGraphicOutput _mainGraphics;
        private Point _downPos;
        private DrawStep _nowDrawing;
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
            dc.PushTransform(new TranslateTransform(CanvasOffsetX, CanvasOffsetY));
            dc.DrawRectangle(null, new Pen(Brushes.Gray, 1), new Rect(0, 0, CanvasWidth, CanvasHeight));
            dc.DrawRectangle(Brushes.White, null, new Rect(0, 0, CanvasWidth, CanvasHeight));

            foreach (var figure in Timeline.Figures)
                figure.Draw(dc);
        }

        private void MainGraphicsOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                _nowDrawing = null;
            if (e.ChangedButton == MouseButton.Right)
                _offsetX = _offsetY = double.NaN;
            RedrawNeeded();
        }

        private void MainGraphicsOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            _nowDrawing = null;
            _offsetX = _offsetY = double.NaN;
            RedrawNeeded();
        }

        private void MainGraphicsOnMouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(_mainGraphics).Move(-CanvasOffsetX, -CanvasOffsetY);
            if (e.LeftButton == MouseButtonState.Pressed)
                if (_nowDrawing != null)
                    switch (_nowDrawing.StepType)
                    {
                        case DrawStep.DrawStepType.DrawRect:
                            ((DrawRectStep) _nowDrawing).ReInit(_downPos.X, _downPos.Y, pos.X - _downPos.X,
                                pos.Y - _downPos.Y);
                            break;
                    }
            if ((e.RightButton == MouseButtonState.Pressed) && (_selected != null))
            {
                var rf = (RectFigure) _selected;
                if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
                {
                    _offsetX = _downPos.X - rf.X.CachedValue.AsDouble;
                    _offsetY = _downPos.Y - rf.Y.CachedValue.AsDouble;
                }

                if (_nowMoving == null)
                {
                    _nowMoving = new MoveRectStep(rf, pos.X - _offsetX, pos.Y - _offsetY);
                    Timeline.Insert(_nowMoving, Timeline.CurrentStepIndex == -1 ? 0 : Timeline.CurrentStepIndex + 1);
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
            }
            RedrawNeeded();
        }

        private void MainGraphicsOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _downPos = e.GetPosition(_mainGraphics).Move(-CanvasOffsetX, -CanvasOffsetY);
            if (e.ChangedButton == MouseButton.Left)
            {
                _nowDrawing = new DrawRectStep(_downPos.X, _downPos.Y, 0, 0);
                Timeline.Insert(_nowDrawing, Timeline.CurrentStepIndex == -1 ? 0 : Timeline.CurrentStepIndex + 1);
            }
            if (e.ChangedButton == MouseButton.Right)
            {
                if (_selected != null)
                {
                    _selected.IsSelected = false;
                    _selected = null;
                }
                foreach (var f in Timeline.Figures)
                    if (f.IsMouseOver(_downPos.X, _downPos.Y))
                    {
                        _selected = f;
                        if ((_selected == Timeline.CurrentStep.Figure) && Timeline.CurrentStep is MoveRectStep)
                            _nowMoving = (TransformStep) Timeline.Steps[Timeline.CurrentStepIndex];
                        else
                            _nowMoving = null;
                        _selected.IsSelected = true;
                        break;
                    }
            }
            RedrawNeeded();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataStorage.Add(new ScalarExpression("data", "width", ((int) numericUpDown1.Value).ToString()));
            Timeline.Insert(new DrawRectStep("10", "10", "data.width", "50"));
            Timeline.Insert(new DrawRectStep("rect1.x+rect1.width", "rect1.y + rect1.height", "50", "50"));
            RedrawNeeded();
        }

        private IEnumerable<string> GetData()
        {
            var r = new Random();
            for (var i = 0; i < 10; ++i)
                yield return r.Next(0, 100) + r.NextDouble() + "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataStorage.Add(new ScalarExpression("data", "canvasHeight", CanvasHeight.ToString()));
            DataStorage.Add(new ScalarExpression("data", "canvasWidth", CanvasWidth.ToString()));
            DataStorage.AddArrayExpression("data", "height", GetData().ToArray());
            DataStorage.Add(new ScalarExpression("data", "count", "len(height)"));
            var len = (int) DataStorage.GetScalarExpression("data.count").CachedValue.AsDouble;

            var drawGuide = new DrawRectStep("0", "0", "data.canvasWidth/data.count", "data.canvasHeight", true);
            Timeline.Insert(drawGuide);

            var drawBar = new DrawRectStep("rect1.x", "rect1.y", "rect1.width",
                "(data.height / max(data.height)) * data.canvasHeight");
            drawBar.MakeIterable(len);
            Timeline.Insert(drawBar);

            var moveGuide = new MoveRectStep((RectFigure) drawGuide.Figure, "rect2.x + rect2.width", "rect2.y");
            moveGuide.MakeIterable(len);
            Timeline.Insert(moveGuide);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DataStorage.GetScalarExpression("data.width").SetRawExpression(((int) numericUpDown1.Value).ToString());
            RedrawNeeded();
        }
    }
}