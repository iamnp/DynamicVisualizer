using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;
using DynamicVisualizer.Manipulators;
using DynamicVisualizer.Steps;
using MessageBox = System.Windows.Forms.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using SystemColors = System.Drawing.SystemColors;

// TODO GUI improvements (scalar and array(!) editors, step list, step editor)
// TODO? refactoring (get rid of static members)
// TODO? deal with expr dependants system

namespace DynamicVisualizer
{
    public partial class MainForm : Form
    {
        public static Action RedrawNeeded;
        private readonly FigureDrawer _figureDrawer = new FigureDrawer();
        private readonly FigureMover _figureMover = new FigureMover();
        private readonly FigureResizer _figureResizer = new FigureResizer();
        private readonly FigureRotater _figureRotater = new FigureRotater();
        private readonly FigureScaler _figureScaler = new FigureScaler();

        private readonly Dictionary<Keys, Action> _hotkeys;
        private readonly MainGraphicOutput _mainGraphics;
        private Figure _selected;
        private TransformStep.TransformType _transformType = TransformStep.TransformType.Move;

        public MainForm()
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
                {Keys.P, () => loopLabel_Click(loopLabel, EventArgs.Empty)},
                {Keys.H, () => straightLabel_Click(straightLabel, EventArgs.Empty)},
                {Keys.A, () => addStepAfterLabel_Click(addStepAfterLabel, EventArgs.Empty)},
                {Keys.B, () => addStepBeforeLabel_Click(addStepBeforeLabel, EventArgs.Empty)},
                {Keys.D, () => addStepLoopedLabel_Click(addStepLoopedLabel, EventArgs.Empty)},
                {Keys.F, () => markAsFinalLabel_Click(markAsFinalLabel, EventArgs.Empty)}
            };

            RedrawNeeded = Redraw;

            StepManager.StepEditor = stepEditor1;
            StepManager.StepListControl = _stepListControl1;
            _stepListControl1.MainForm = this;

            DataStorage.Add(new ScalarExpression("canvas", "height", Drawer.CanvasHeight.ToString()));
            DataStorage.Add(new ScalarExpression("canvas", "width", Drawer.CanvasWidth.ToString()));
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

            _mainGraphics = new MainGraphicOutput {DrawingFunc = Drawer.DrawScene};
            elementHost1.Child = _mainGraphics;
            _mainGraphics.MouseDown += MainGraphicsOnMouseDown;
            _mainGraphics.MouseMove += MainGraphicsOnMouseMove;
            _mainGraphics.MouseUp += MainGraphicsOnMouseUp;
            _mainGraphics.MouseLeave += MainGraphicsOnMouseLeave;
            _mainGraphics.MouseEnter += MainGraphicsOnMouseEnter;
            _mainGraphics.SizeChanged += MainGraphicsOnSizeChanged;

            ActiveControl = _stepListControl1;
        }

        private bool IsTextBoxActive()
        {
            var c = ActiveControl;
            var p = c;
            while ((c = (c as ContainerControl)?.ActiveControl) != null)
            {
                p = c;
            }
            return p.GetType() == typeof(TextBox);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!IsTextBoxActive())
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
                            if (StepManager.TryToRemove(StepManager.CurrentStepIndex))
                            {
                                StepManager.FinalStep = null;
                                markAsFinalLabel.Text = "mark as final";
                            }
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
            Drawer.DrawMagnets = _figureMover.NowMoving || _figureDrawer.NowDrawing || _figureResizer.NowResizing;
            _mainGraphics.InvalidateVisual();
            _stepListControl1.ReSetText();
            stepEditor1.Redraw();
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
                gLabel.Visible = false;
            }
            else
            {
                guideLabel.Visible = true;
                gLabel.Visible = true;
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
                gLabel.Visible = true;
                guideLabel.ForeColor = _selected.IsGuide ? SystemColors.ControlText : SystemColors.ControlDark;
            }
            else
            {
                guideLabel.Visible = false;
                gLabel.Visible = false;
            }
        }

        private void MainGraphicsOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var upPos = e.GetPosition(_mainGraphics).Move(-Drawer.CanvasOffsetX, -Drawer.CanvasOffsetY);
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

        private void MainGraphicsOnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            ActiveControl = _stepListControl1;
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
            var pos = e.GetPosition(_mainGraphics).Move(-Drawer.CanvasOffsetX, -Drawer.CanvasOffsetY);
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
            var downPos = e.GetPosition(_mainGraphics).Move(-Drawer.CanvasOffsetX, -Drawer.CanvasOffsetY);
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
            straightLabel.Visible = false;
            hLabel.Visible = false;
        }

        private void circleLabel_Click(object sender, EventArgs e)
        {
            _figureDrawer.DrawStepType = DrawStep.DrawStepType.DrawEllipse;
            circleLabel.ForeColor = SystemColors.ControlText;
            rectLabel.ForeColor = SystemColors.ControlDark;
            lineLabel.ForeColor = SystemColors.ControlDark;
            textLabel.ForeColor = SystemColors.ControlDark;
            straightLabel.Visible = false;
            hLabel.Visible = false;
        }

        private void lineLabel_Click(object sender, EventArgs e)
        {
            _figureDrawer.DrawStepType = DrawStep.DrawStepType.DrawLine;
            circleLabel.ForeColor = SystemColors.ControlDark;
            rectLabel.ForeColor = SystemColors.ControlDark;
            lineLabel.ForeColor = SystemColors.ControlText;
            textLabel.ForeColor = SystemColors.ControlDark;
            straightLabel.Visible = true;
            hLabel.Visible = true;
        }

        private void textLabel_Click(object sender, EventArgs e)
        {
            _figureDrawer.DrawStepType = DrawStep.DrawStepType.DrawText;
            circleLabel.ForeColor = SystemColors.ControlDark;
            rectLabel.ForeColor = SystemColors.ControlDark;
            lineLabel.ForeColor = SystemColors.ControlDark;
            textLabel.ForeColor = SystemColors.ControlText;
            straightLabel.Visible = true;
            hLabel.Visible = true;
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
            StepManager.RefreshToCurrentStep();
        }

        private void loopLabel_Click(object sender, EventArgs e)
        {
            if (loopLabel.Visible)
            {
                _stepListControl1.CurrentSelectionToIterableGroup();
            }
        }

        private void straightLabel_Click(object sender, EventArgs e)
        {
            if (!straightLabel.Visible)
            {
                return;
            }
            if (_figureDrawer.Straight)
            {
                straightLabel.ForeColor = SystemColors.ControlDark;
            }
            else
            {
                straightLabel.ForeColor = SystemColors.ControlText;
            }
            _figureDrawer.Straight = !_figureDrawer.Straight;
        }

        private void addStepAfterLabel_Click(object sender, EventArgs e)
        {
            StepManager.InsertNext(new EmptyStep());
        }

        private void addStepBeforeLabel_Click(object sender, EventArgs e)
        {
            StepManager.InsertNext(new EmptyStep(), true);
        }

        private void addStepLoopedLabel_Click(object sender, EventArgs e)
        {
            if (StepManager.AddStepLooped)
            {
                addStepLoopedLabel.Text = "not looped";
                StepManager.AddStepLooped = false;
            }
            else
            {
                addStepLoopedLabel.Text = "looped";
                StepManager.AddStepLooped = true;
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog {Filter = @"PNG files (*.png)|*.png"})
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        var pngBytes = Drawer.GetScenePngBytes();
                        File.WriteAllBytes(dialog.FileName, pngBytes);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка при сохранении файла!\n" + ex.Message);
                    }
                }
            }
        }

        private void markAsFinalLabel_Click(object sender, EventArgs e)
        {
            if (StepManager.CurrentStep == null)
            {
                return;
            }
            if (StepManager.FinalStep == null)
            {
                if (StepManager.CurrentStep.Iterations == -1)
                {
                    StepManager.FinalStep = StepManager.CurrentStep;
                    markAsFinalLabel.Text = "reset final";
                }
            }
            else
            {
                StepManager.FinalStep = null;
                markAsFinalLabel.Text = "mark as final";
            }
            StepManager.RefreshToCurrentStep();
        }

        private void MainGraphicsOnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            Drawer.HostRect = new Rect(0, 0, _mainGraphics.ActualWidth, _mainGraphics.ActualHeight);
            Drawer.CanvasOffsetX = (int) ((Drawer.HostRect.Width - Drawer.CanvasWidth) / 2);
            Drawer.CanvasOffsetY = (int) ((Drawer.HostRect.Height - Drawer.CanvasHeight) / 2);
            Drawer.CanvasTranslate = new TranslateTransform(Drawer.CanvasOffsetX, Drawer.CanvasOffsetY);
            Drawer.CanvasTranslate.Freeze();
            if (StepManager.CurrentStep == null)
            {
                _mainGraphics.InvalidateVisual();
            }
            else
            {
                StepManager.RefreshToCurrentStep();
            }
        }
    }
}