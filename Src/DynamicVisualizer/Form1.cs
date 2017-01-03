using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using DynamicVisualizer.Controls;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;
using DynamicVisualizer.Manipulators;
using DynamicVisualizer.Steps;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using SystemColors = System.Drawing.SystemColors;

// TODO fix exception when drawing on disapperaning magnets (before step insertion)
// TODO fix iterated line rotating (cached center point)
// TODO deal with expr dependants, identify infinite resucrsion
// TODO add global exception handler (for stack overflow too)
// TODO deal with too many static members
// TODO add 'eval-to-step' marker
// TODO deal with removing steps with dependants
// TODO scaling 0-width rect gets scale by NaN
// TODO? fix order of iterative drawing

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
        private readonly FigureRotater _figureRotater = new FigureRotater();
        private readonly FigureScaler _figureScaler = new FigureScaler();
        private readonly Rect _hostRect = new Rect(0, 0, 1000, 700);

        private readonly Dictionary<Keys, Action> _hotkeys;
        private readonly MainGraphicOutput _mainGraphics;
        private Figure _selected;
        private TransformStep.TransformType _transformType = TransformStep.TransformType.Move;

        public Form1()
        {
            InitializeComponent();

            _hotkeys = new Dictionary<Keys, Action>
            {
                {Keys.R, () => rectLabel_Click(rectLabel, EventArgs.Empty)},
                {Keys.C, () => circleLabel_Click(circleLabel, EventArgs.Empty)},
                {Keys.L, () => lineLabel_Click(lineLabel, EventArgs.Empty)},
                {Keys.T, () => textLabel_Click(textLabel, EventArgs.Empty)},
                {Keys.M, () => moveLabel_Click(moveLabel, EventArgs.Empty)},
                {Keys.S, () => scaleLabel_Click(scaleLabel, EventArgs.Empty)},
                {Keys.E, () => resizeLabel_Click(resizeLabel, EventArgs.Empty)},
                {Keys.O, () => rotateLabel_Click(rotateLabel, EventArgs.Empty)},
                {Keys.G, () => guideLabel_Click(guideLabel, EventArgs.Empty)},
                {Keys.P, () => loopLabel_Click(loopLabel, EventArgs.Empty)}
            };

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
            var wover2 = new ScalarExpression("a", "a", "canvas.width/2", true);
            var hover2 = new ScalarExpression("a", "a", "canvas.height/2", true);

            StepManager.CanvasMagnets = new[]
            {
                new Magnet(x1, y1, "canvas's top-left"),
                new Magnet(x1, h, "canvas's bottom-left"),
                new Magnet(w, y1, "canvas's top-right"),
                new Magnet(w, h, "canvas's bottom-right"),
                new Magnet(wover2, hover2, "canvas's center"),
                new Magnet(wover2, y1, "canvas's top"),
                new Magnet(wover2, h, "canvas's bottom"),
                new Magnet(x1, hover2, "canvas's left"),
                new Magnet(w, hover2, "canvas's right")
            };

            _mainGraphics = new MainGraphicOutput {DrawingFunc = DrawScene};
            elementHost1.Child = _mainGraphics;
            _mainGraphics.MouseDown += MainGraphicsOnMouseDown;
            _mainGraphics.MouseMove += MainGraphicsOnMouseMove;
            _mainGraphics.MouseUp += MainGraphicsOnMouseUp;
            _mainGraphics.MouseLeave += MainGraphicsOnMouseLeave;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((ActiveControl.GetType() != typeof(ArrayExpressionItem))
                && (ActiveControl.GetType() != typeof(ScalarExpressionItem)) &&
                (ActiveControl.GetType() != typeof(StepEditor)) && (ActiveControl.GetType() != typeof(GroupHeaderItem)))
            {
                if (StepManager.CurrentStep != null)
                {
                    if (keyData == Keys.Up)
                    {
                        if (StepManager.CurrentStep.Iterations != -1)
                        {
                            StepManager.PrevIterationFromCurrentPos();
                        }
                        else if (StepManager.CurrentStepIndex > 0)
                        {
                            StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex - 1);
                        }
                        return true;
                    }
                    if (keyData == Keys.Down)
                    {
                        if (StepManager.CurrentStep.Iterations != -1)
                        {
                            StepManager.NextIterationFromCurrentPos();
                        }
                        else if (StepManager.CurrentStepIndex < StepManager.Steps.Count - 1)
                        {
                            StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex + 1);
                        }
                        return true;
                    }

                    if (keyData == Keys.Right)
                    {
                        if ((StepManager.CurrentStep.Iterations != -1)
                            && (StepManager.CurrentStep.CompletedIterations != StepManager.CurrentStep.Iterations))
                        {
                            StepManager.NextLoopFromCurrentPos();
                        }
                        return true;
                    }
                    if (keyData == Keys.Left)
                    {
                        if ((StepManager.CurrentStep.Iterations != -1) &&
                            (StepManager.CurrentStep.CompletedIterations != 0))
                        {
                            StepManager.PrevLoopFromCurrentPos();
                        }
                        return true;
                    }

                    if (keyData == Keys.Delete)
                    {
                        if (StepManager.CurrentStepIndex != -1)
                        {
                            StepManager.Remove(StepManager.CurrentStepIndex);
                        }
                        return true;
                    }
                }
                if (_hotkeys.ContainsKey(keyData))
                {
                    _hotkeys[keyData]();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
                magnet.Draw(dc, false);
            }

            foreach (var figure in StepManager.Figures)
            {
                figure.Draw(dc);
            }

            foreach (var figure in StepManager.Figures)
            {
                if (figure.IsSelected || _figureMover.NowMoving || _figureDrawer.NowDrawing ||
                    _figureResizer.NowResizing)
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
                guideLabel.Visible = false;
            }
            else
            {
                guideLabel.Visible = true;
                guideLabel.ForeColor = _selected.IsGuide ? SystemColors.ControlText : SystemColors.ControlDark;
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
                guideLabel.Visible = true;
                guideLabel.ForeColor = _selected.IsGuide ? SystemColors.ControlText : SystemColors.ControlDark;
            }
            else
            {
                guideLabel.Visible = false;
            }
        }

        private void MainGraphicsOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var upPos = e.GetPosition(_mainGraphics).Move(-CanvasOffsetX, -CanvasOffsetY);
            if (e.ChangedButton == MouseButton.Right)
            {
                var moved = false;
                switch (_transformType)
                {
                    case TransformStep.TransformType.Move:
                        moved = _figureMover.Reset();
                        break;
                    case TransformStep.TransformType.Scale:
                        moved = _figureScaler.Reset();
                        break;
                    case TransformStep.TransformType.Resize:
                        moved = _figureResizer.Reset();
                        break;
                    case TransformStep.TransformType.Rotate:
                        moved = _figureRotater.Reset();
                        break;
                }
                if (!moved)
                {
                    PerformFigureSelection(upPos);
                }
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
            _figureRotater.Reset();
            Redraw();
        }

        private void MainGraphicsOnMouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(_mainGraphics).Move(-CanvasOffsetX, -CanvasOffsetY);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _figureDrawer.Move(pos);
            }
            if ((e.RightButton == MouseButtonState.Pressed) && (_selected != null))
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
                    case TransformStep.TransformType.Rotate:
                        _figureRotater.Move(_selected, pos);
                        break;
                }
            }

            Redraw();
        }

        private void MainGraphicsOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ActiveControl = _stepListControl1;
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
                case TransformStep.TransformType.Rotate:
                    _figureRotater.SetDownPos(downPos);
                    break;
            }
            if (e.ChangedButton == MouseButton.Left)
            {
                _figureDrawer.Start(downPos);
            }
            Redraw();
        }

        private void rectLabel_Click(object sender, EventArgs e)
        {
            _figureDrawer.DrawStepType = DrawStep.DrawStepType.DrawRect;
            rectLabel.ForeColor = SystemColors.ControlText;
            circleLabel.ForeColor = SystemColors.ControlDark;
            lineLabel.ForeColor = SystemColors.ControlDark;
            textLabel.ForeColor = SystemColors.ControlDark;
        }

        private void circleLabel_Click(object sender, EventArgs e)
        {
            _figureDrawer.DrawStepType = DrawStep.DrawStepType.DrawEllipse;
            circleLabel.ForeColor = SystemColors.ControlText;
            rectLabel.ForeColor = SystemColors.ControlDark;
            lineLabel.ForeColor = SystemColors.ControlDark;
            textLabel.ForeColor = SystemColors.ControlDark;
        }

        private void lineLabel_Click(object sender, EventArgs e)
        {
            _figureDrawer.DrawStepType = DrawStep.DrawStepType.DrawLine;
            circleLabel.ForeColor = SystemColors.ControlDark;
            rectLabel.ForeColor = SystemColors.ControlDark;
            lineLabel.ForeColor = SystemColors.ControlText;
            textLabel.ForeColor = SystemColors.ControlDark;
        }

        private void textLabel_Click(object sender, EventArgs e)
        {
            _figureDrawer.DrawStepType = DrawStep.DrawStepType.DrawText;
            circleLabel.ForeColor = SystemColors.ControlDark;
            rectLabel.ForeColor = SystemColors.ControlDark;
            lineLabel.ForeColor = SystemColors.ControlDark;
            textLabel.ForeColor = SystemColors.ControlText;
        }

        private void moveLabel_Click(object sender, EventArgs e)
        {
            _transformType = TransformStep.TransformType.Move;
            moveLabel.ForeColor = SystemColors.ControlText;
            scaleLabel.ForeColor = SystemColors.ControlDark;
            resizeLabel.ForeColor = SystemColors.ControlDark;
            rotateLabel.ForeColor = SystemColors.ControlDark;
        }

        private void scaleLabel_Click(object sender, EventArgs e)
        {
            _transformType = TransformStep.TransformType.Scale;
            scaleLabel.ForeColor = SystemColors.ControlText;
            moveLabel.ForeColor = SystemColors.ControlDark;
            resizeLabel.ForeColor = SystemColors.ControlDark;
            rotateLabel.ForeColor = SystemColors.ControlDark;
        }

        private void resizeLabel_Click(object sender, EventArgs e)
        {
            _transformType = TransformStep.TransformType.Resize;
            scaleLabel.ForeColor = SystemColors.ControlDark;
            moveLabel.ForeColor = SystemColors.ControlDark;
            resizeLabel.ForeColor = SystemColors.ControlText;
            rotateLabel.ForeColor = SystemColors.ControlDark;
        }

        private void rotateLabel_Click(object sender, EventArgs e)
        {
            _transformType = TransformStep.TransformType.Rotate;
            scaleLabel.ForeColor = SystemColors.ControlDark;
            moveLabel.ForeColor = SystemColors.ControlDark;
            resizeLabel.ForeColor = SystemColors.ControlDark;
            rotateLabel.ForeColor = SystemColors.ControlText;
        }

        private void guideLabel_Click(object sender, EventArgs e)
        {
            _selected.IsGuide = !_selected.IsGuide;
            guideLabel.ForeColor = _selected.IsGuide ? SystemColors.ControlText : SystemColors.ControlDark;
            Redraw();
        }

        private void loopLabel_Click(object sender, EventArgs e)
        {
            if (loopLabel.Visible)
            {
                _stepListControl1.CurrentSelectionToIterableGroup();
            }
        }

        private void addStepAfterLabel_Click(object sender, EventArgs e)
        {
            if (StepManager.AddStepBeforeCurrent)
            {
                addStepAfterLabel.Text = "After current";
                StepManager.AddStepBeforeCurrent = false;
            }
            else
            {
                addStepAfterLabel.Text = "Before current";
                StepManager.AddStepBeforeCurrent = true;
            }
        }

        private void addStepLoopedLabel_Click(object sender, EventArgs e)
        {
            if (StepManager.AddStepLooped)
            {
                addStepLoopedLabel.Text = "Not looped";
                StepManager.AddStepLooped = false;
            }
            else
            {
                addStepLoopedLabel.Text = "Looped";
                StepManager.AddStepLooped = true;
            }
        }
    }
}