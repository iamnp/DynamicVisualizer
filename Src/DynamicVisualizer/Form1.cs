﻿using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;
using DynamicVisualizer.Manipulators;
using DynamicVisualizer.Steps;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using SystemColors = System.Drawing.SystemColors;

// TODO fix Snap() and SnapTo() methods
// TODO make steps in list with proper text definition
// TODO rotate line step
// TODO maybe fix order of iterative drawing
// TODO fix removing steps from group
// TODO fix adding steps before group (shift groups)
// TODO fix adding steps into group
// TODO fix exception when moving line-like rect

namespace DynamicVisualizer
{
    public partial class Form1 : Form
    {
        private const int CanvasWidth = 800;
        private const int CanvasHeight = 600;
        private const int CanvasOffsetX = 100;
        private const int CanvasOffsetY = 50;

        public static Action RedrawNeeded;
        private readonly Rect _canvasRect = new Rect(0, 0, CanvasWidth, CanvasHeight);
        private readonly Pen _canvasStroke = new Pen(Brushes.Gray, 1);
        private readonly TranslateTransform _canvasTranslate = new TranslateTransform(CanvasOffsetX, CanvasOffsetY);
        private readonly FigureDrawer _figureDrawer = new FigureDrawer();
        private readonly FigureMover _figureMover = new FigureMover();
        private readonly FigureResizer _figureResizer = new FigureResizer();
        private readonly FigureScaler _figureScaler = new FigureScaler();
        private readonly Rect _hostRect = new Rect(0, 0, 1000, 700);
        private readonly MainGraphicOutput _mainGraphics;
        private Figure _selected;
        private TransformStep.TransformType _transformType = TransformStep.TransformType.Move;

        public Form1()
        {
            InitializeComponent();

            RedrawNeeded = Redraw;

            StepManager.StepEditor = stepEditor1;
            StepManager.StepListControl = _stepListControl1;
            _stepListControl1.Form1 = this;

            DataStorage.Add(new ScalarExpression("canvas", "height", CanvasHeight.ToString()));
            DataStorage.Add(new ScalarExpression("canvas", "width", CanvasWidth.ToString()));
            DataStorage.Add(new ScalarExpression("canvas", "x", "0"));
            DataStorage.Add(new ScalarExpression("canvas", "y", "0"));

            var x1 = new ScalarExpression("a", "a", "canvas.x", true);
            var y1 = new ScalarExpression("a", "a", "canvas.y", true);
            var w = new ScalarExpression("a", "a", "canvas.width", true);
            var h = new ScalarExpression("a", "a", "canvas.height", true);

            StepManager.CanvasMagnets = new[]
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
        }

        private void Redraw()
        {
            _mainGraphics.InvalidateVisual();
            _stepListControl1.ReSetText();
            stepEditor1.Redraw();
        }

        private void DrawScene(DrawingContext dc)
        {
            dc.DrawRectangle(Brushes.LightGray, null, _hostRect);
            dc.PushTransform(_canvasTranslate);
            dc.DrawRectangle(null, _canvasStroke, _canvasRect);
            dc.DrawRectangle(Brushes.White, null, _canvasRect);

            foreach (var magnet in StepManager.CanvasMagnets)
            {
                dc.DrawEllipse(Brushes.Yellow, new Pen(Brushes.Black, 1),
                    new Point(magnet.X.CachedValue.AsDouble, magnet.Y.CachedValue.AsDouble),
                    4, 4);
            }

            foreach (var figure in StepManager.Figures)
            {
                figure.Draw(dc);
            }

            foreach (var figure in StepManager.Figures)
            {
                if (figure.IsSelected || _figureMover.NowMoving || _figureDrawer.NowDrawing || _figureScaler.NowScailing ||
                    _figureResizer.NowResizing)
                {
                    if (figure.IsSelected)
                    {
                        foreach (var magnet in figure.GetMagnets())
                        {
                            dc.DrawEllipse(Brushes.DeepSkyBlue, new Pen(Brushes.Black, 1),
                                new Point(magnet.X.CachedValue.AsDouble, magnet.Y.CachedValue.AsDouble),
                                5, 5);
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
                            dc.DrawEllipse(Brushes.Yellow, new Pen(Brushes.Black, 1),
                                new Point(magnet.X.CachedValue.AsDouble, magnet.Y.CachedValue.AsDouble),
                                4, 4);
                        }
                    }
                }
            }
        }

        private void PerformFigureSelection(Point pos)
        {
            if (_selected != null)
            {
                _selected.IsSelected = false;
                _selected = null;
            }
            for (var i = StepManager.Figures.Count - 1; i >= 0; --i)
            {
                var f = StepManager.Figures[i];
                if (!f.IsStatic && f.IsMouseOver(pos.X, pos.Y))
                {
                    _selected = f;
                    _selected.IsSelected = true;
                    break;
                }
            }
            if (_selected == null)
            {
                label7.Visible = false;
            }
            else
            {
                label7.Visible = true;
                label7.ForeColor = _selected.IsGuide ? SystemColors.ControlText : SystemColors.ControlDark;
            }
        }

        public void PerformFigureSelection(Figure figure)
        {
            if (_selected != null)
            {
                _selected.IsSelected = false;
                _selected = null;
            }
            if (figure != null)
            {
                _selected = figure;
                _selected.IsSelected = true;
                label7.Visible = true;
                label7.ForeColor = _selected.IsGuide ? SystemColors.ControlText : SystemColors.ControlDark;
            }
            else
            {
                label7.Visible = false;
            }
        }

        private void MainGraphicsOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var upPos = e.GetPosition(_mainGraphics).Move(-CanvasOffsetX, -CanvasOffsetY);
            if (e.ChangedButton == MouseButton.Right)
            {
                _figureMover.Reset();
                _figureScaler.Reset();
                _figureResizer.Reset();
                PerformFigureSelection(upPos);
            }
            if (e.ChangedButton == MouseButton.Left)
            {
                _figureDrawer.Finish();
            }
            Redraw();
        }

        private void MainGraphicsOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            _figureDrawer.Finish();
            _figureMover.Reset();
            _figureScaler.Reset();
            _figureResizer.Reset();
            Redraw();
        }

        private void MainGraphicsOnMouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(_mainGraphics).Move(-CanvasOffsetX, -CanvasOffsetY);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _figureDrawer.Move(pos);
            }
            if (e.RightButton == MouseButtonState.Pressed && _selected != null)
            {
                switch (_transformType)
                {
                    case TransformStep.TransformType.Move:
                        _figureMover.Move(_selected, pos);
                        break;
                    case TransformStep.TransformType.Scale:
                        _figureScaler.Move(_selected, pos);
                        break;
                    case TransformStep.TransformType.Resize:
                        _figureResizer.Move(_selected, pos);
                        break;
                }
            }

            Redraw();
        }

        private void MainGraphicsOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var downPos = e.GetPosition(_mainGraphics).Move(-CanvasOffsetX, -CanvasOffsetY);
            switch (_transformType)
            {
                case TransformStep.TransformType.Move:
                    _figureMover.SetDownPos(downPos);
                    break;
                case TransformStep.TransformType.Scale:
                    _figureScaler.SetDownPos(downPos);
                    break;
                case TransformStep.TransformType.Resize:
                    _figureResizer.SetDownPos(downPos);
                    break;
            }
            if (e.ChangedButton == MouseButton.Left)
            {
                _figureDrawer.Start(downPos);
            }
            if (e.ChangedButton == MouseButton.Right)
            {
                if (_selected == null || !_selected.IsMouseOver(downPos.X, downPos.Y))
                {
                    PerformFigureSelection(downPos);
                }
            }
            Redraw();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            _figureDrawer.DrawStepType = DrawStep.DrawStepType.DrawRect;
            label2.ForeColor = SystemColors.ControlText;
            label3.ForeColor = SystemColors.ControlDark;
            label11.ForeColor = SystemColors.ControlDark;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            _figureDrawer.DrawStepType = DrawStep.DrawStepType.DrawEllipse;
            label3.ForeColor = SystemColors.ControlText;
            label2.ForeColor = SystemColors.ControlDark;
            label11.ForeColor = SystemColors.ControlDark;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            _transformType = TransformStep.TransformType.Move;
            label5.ForeColor = SystemColors.ControlText;
            label6.ForeColor = SystemColors.ControlDark;
            label10.ForeColor = SystemColors.ControlDark;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            _transformType = TransformStep.TransformType.Scale;
            label6.ForeColor = SystemColors.ControlText;
            label5.ForeColor = SystemColors.ControlDark;
            label10.ForeColor = SystemColors.ControlDark;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            _transformType = TransformStep.TransformType.Resize;
            label6.ForeColor = SystemColors.ControlDark;
            label5.ForeColor = SystemColors.ControlDark;
            label10.ForeColor = SystemColors.ControlText;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            _selected.IsGuide = !_selected.IsGuide;
            label7.ForeColor = _selected.IsGuide ? SystemColors.ControlText : SystemColors.ControlDark;
            Redraw();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            _stepListControl1.CurrentSelectionToIterableGroup();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            _figureDrawer.DrawStepType = DrawStep.DrawStepType.DrawLine;
            label3.ForeColor = SystemColors.ControlDark;
            label2.ForeColor = SystemColors.ControlDark;
            label11.ForeColor = SystemColors.ControlText;
        }
    }
}