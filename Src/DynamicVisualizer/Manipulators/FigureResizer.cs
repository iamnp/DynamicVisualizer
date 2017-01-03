using System.Windows;
using DynamicVisualizer.Figures;
using DynamicVisualizer.Steps;
using DynamicVisualizer.Steps.Resize;

namespace DynamicVisualizer.Manipulators
{
    internal class FigureResizer
    {
        private Point _downPos;
        private bool _moved;
        private ResizeStep _nowResizing;

        public bool NowResizing => _nowResizing != null;

        public void SetDownPos(Point pos)
        {
            _moved = false;
            _downPos = pos;
        }

        public bool Reset()
        {
            _nowResizing = null;
            return _moved;
        }

        private void ResizeRect(RectFigure rf, Point pos)
        {
            if (_nowResizing == null)
            {
                var snappedTo = StepManager.SnapTo(pos, rf.GetMagnets(), rf.Center);
                if (snappedTo == rf.Left)
                {
                    _nowResizing = new ResizeRectStep(rf, ResizeRectStep.Side.Right, pos.X - _downPos.X);
                }
                else if (snappedTo == rf.Right)
                {
                    _nowResizing = new ResizeRectStep(rf, ResizeRectStep.Side.Left, pos.X - _downPos.X);
                }
                else if (snappedTo == rf.Top)
                {
                    _nowResizing = new ResizeRectStep(rf, ResizeRectStep.Side.Bottom, pos.Y - _downPos.Y);
                }
                else if (snappedTo == rf.Bottom)
                {
                    _nowResizing = new ResizeRectStep(rf, ResizeRectStep.Side.Top, pos.Y - _downPos.Y);
                }
                if (_nowResizing == null)
                {
                    return;
                }
                StepManager.InsertNext(_nowResizing);
            }
            else
            {
                var rrs = (ResizeRectStep) _nowResizing;
                var snapped = StepManager.Snap(pos, _nowResizing.Figure);
                if (snapped != null)
                {
                    if (rrs.ResizeAround == ResizeRectStep.Side.Right)
                    {
                        rrs.Resize("(" + snapped.X.ExprString + ") - " + rrs.Figure.Name + ".x", snapped.Def);
                    }
                    else if (rrs.ResizeAround == ResizeRectStep.Side.Left)
                    {
                        rrs.Resize("(" + snapped.X.ExprString + ") - (" + rrs.Figure.Name + ".x + (" +
                                   rrs.Figure.Name + ".width))", snapped.Def);
                    }
                    else if (rrs.ResizeAround == ResizeRectStep.Side.Top)
                    {
                        rrs.Resize("(" + snapped.Y.ExprString + ") - (" + rrs.Figure.Name + ".y + (" +
                                   rrs.Figure.Name + ".height))", snapped.Def);
                    }
                    else if (rrs.ResizeAround == ResizeRectStep.Side.Bottom)
                    {
                        rrs.Resize("(" + snapped.Y.ExprString + ") - " + rrs.Figure.Name + ".y", snapped.Def);
                    }
                }
                else
                {
                    if (rrs.ResizeAround == ResizeRectStep.Side.Right)
                    {
                        rrs.Resize(pos.X - _downPos.X);
                    }
                    else if (rrs.ResizeAround == ResizeRectStep.Side.Left)
                    {
                        rrs.Resize(pos.X - _downPos.X);
                    }
                    else if (rrs.ResizeAround == ResizeRectStep.Side.Top)
                    {
                        rrs.Resize(pos.Y - _downPos.Y);
                    }
                    else if (rrs.ResizeAround == ResizeRectStep.Side.Bottom)
                    {
                        rrs.Resize(pos.Y - _downPos.Y);
                    }
                }
            }
        }

        private void ResizeEllipse(EllipseFigure ef, Point pos)
        {
            if (_nowResizing == null)
            {
                var snappedTo = StepManager.SnapTo(pos, ef.GetMagnets(), ef.Center);
                if (snappedTo == ef.Left)
                {
                    _nowResizing = new ResizeEllipseStep(ef, ResizeEllipseStep.Side.Right, pos.X - _downPos.X);
                }
                else if (snappedTo == ef.Right)
                {
                    _nowResizing = new ResizeEllipseStep(ef, ResizeEllipseStep.Side.Left, pos.X - _downPos.X);
                }
                else if (snappedTo == ef.Top)
                {
                    _nowResizing = new ResizeEllipseStep(ef, ResizeEllipseStep.Side.Bottom, pos.Y - _downPos.Y);
                }
                else if (snappedTo == ef.Bottom)
                {
                    _nowResizing = new ResizeEllipseStep(ef, ResizeEllipseStep.Side.Top, pos.Y - _downPos.Y);
                }
                if (_nowResizing == null)
                {
                    return;
                }
                StepManager.InsertNext(_nowResizing);
            }
            else
            {
                var res = (ResizeEllipseStep) _nowResizing;
                var snapped = StepManager.Snap(pos, _nowResizing.Figure);
                if (snapped != null)
                {
                    if (res.ResizeAround == ResizeEllipseStep.Side.Right)
                    {
                        res.Resize("(" + res.Figure.Name + ".x - " + res.Figure.Name + ".radius1 ) - (" +
                                   snapped.X.ExprString + ")", snapped.Def);
                    }
                    else if (res.ResizeAround == ResizeEllipseStep.Side.Left)
                    {
                        res.Resize("(" + snapped.X.ExprString + ") - (" + res.Figure.Name + ".x + (" +
                                   res.Figure.Name + ".radius1))", snapped.Def);
                    }
                    else if (res.ResizeAround == ResizeEllipseStep.Side.Top)
                    {
                        res.Resize("(" + res.Figure.Name + ".y + " + res.Figure.Name + ".radius2 ) - (" +
                                   snapped.Y.ExprString + ")", snapped.Def);
                    }
                    else if (res.ResizeAround == ResizeEllipseStep.Side.Bottom)
                    {
                        res.Resize("(" + snapped.Y.ExprString + ") - (" + res.Figure.Name + ".y - (" +
                                   res.Figure.Name + ".radius2))", snapped.Def);
                    }
                }
                else
                {
                    if (res.ResizeAround == ResizeEllipseStep.Side.Right)
                    {
                        res.Resize(_downPos.X - pos.X);
                    }
                    else if (res.ResizeAround == ResizeEllipseStep.Side.Left)
                    {
                        res.Resize(pos.X - _downPos.X);
                    }
                    else if (res.ResizeAround == ResizeEllipseStep.Side.Top)
                    {
                        res.Resize(_downPos.Y - pos.Y);
                    }
                    else if (res.ResizeAround == ResizeEllipseStep.Side.Bottom)
                    {
                        res.Resize(pos.Y - _downPos.Y);
                    }
                }
            }
        }

        private void ResizeLine(LineFigure lf, Point pos)
        {
            if (_nowResizing == null)
            {
                var snappedTo = StepManager.SnapTo(pos, lf.GetMagnets(), lf.Center);
                if (snappedTo == lf.Start)
                {
                    _nowResizing = new ResizeLineStep(lf, ResizeLineStep.Side.End, pos.X - _downPos.X,
                        pos.Y - _downPos.Y);
                }
                else if (snappedTo == lf.End)
                {
                    _nowResizing = new ResizeLineStep(lf, ResizeLineStep.Side.Start, pos.X - _downPos.X,
                        pos.Y - _downPos.Y);
                }
                if (_nowResizing == null)
                {
                    return;
                }
                StepManager.InsertNext(_nowResizing);
            }
            else
            {
                var rls = (ResizeLineStep) _nowResizing;
                var snapped = StepManager.Snap(pos, _nowResizing.Figure);
                if (snapped != null)
                {
                    if (rls.ResizeAround == ResizeLineStep.Side.End)
                    {
                        rls.Resize("(" + snapped.X.ExprString + ") - " + rls.Figure.Name + ".x",
                            "(" + snapped.Y.ExprString + ") - " + rls.Figure.Name + ".y", snapped.Def);
                    }
                    else if (rls.ResizeAround == ResizeLineStep.Side.Start)
                    {
                        rls.Resize("(" + snapped.X.ExprString + ") - (" + rls.Figure.Name + ".x + (" +
                                   rls.Figure.Name + ".width))",
                            "(" + snapped.Y.ExprString + ") - (" + rls.Figure.Name + ".y + (" +
                            rls.Figure.Name + ".height))", snapped.Def);
                    }
                }
                else
                {
                    if (rls.ResizeAround == ResizeLineStep.Side.End)
                    {
                        rls.Resize(pos.X - _downPos.X, pos.Y - _downPos.Y);
                    }
                    else if (rls.ResizeAround == ResizeLineStep.Side.Start)
                    {
                        rls.Resize(pos.X - _downPos.X, pos.Y - _downPos.Y);
                    }
                }
            }
        }

        private void ResizeText(TextFigure tf, Point pos)
        {
            if (_nowResizing == null)
            {
                var snappedTo = StepManager.SnapTo(pos, tf.GetMagnets(), tf.Center);
                if (snappedTo == tf.Start)
                {
                    _nowResizing = new ResizeTextStep(tf, ResizeTextStep.Side.End, pos.X - _downPos.X,
                        pos.Y - _downPos.Y);
                }
                else if (snappedTo == tf.End)
                {
                    _nowResizing = new ResizeTextStep(tf, ResizeTextStep.Side.Start, pos.X - _downPos.X,
                        pos.Y - _downPos.Y);
                }
                if (_nowResizing == null)
                {
                    return;
                }
                StepManager.InsertNext(_nowResizing);
            }
            else
            {
                var rts = (ResizeTextStep) _nowResizing;
                var snapped = StepManager.Snap(pos, _nowResizing.Figure);
                if (snapped != null)
                {
                    if (rts.ResizeAround == ResizeTextStep.Side.End)
                    {
                        rts.Resize("(" + snapped.X.ExprString + ") - " + rts.Figure.Name + ".x",
                            "(" + snapped.Y.ExprString + ") - " + rts.Figure.Name + ".y", snapped.Def);
                    }
                    else if (rts.ResizeAround == ResizeTextStep.Side.Start)
                    {
                        rts.Resize("(" + snapped.X.ExprString + ") - (" + rts.Figure.Name + ".x + (" +
                                   rts.Figure.Name + ".width))",
                            "(" + snapped.Y.ExprString + ") - (" + rts.Figure.Name + ".y + (" +
                            rts.Figure.Name + ".height))", snapped.Def);
                    }
                }
                else
                {
                    if (rts.ResizeAround == ResizeTextStep.Side.End)
                    {
                        rts.Resize(pos.X - _downPos.X, pos.Y - _downPos.Y);
                    }
                    else if (rts.ResizeAround == ResizeTextStep.Side.Start)
                    {
                        rts.Resize(pos.X - _downPos.X, pos.Y - _downPos.Y);
                    }
                }
            }
        }

        public void Move(Figure selected, Point pos)
        {
            switch (selected.Type)
            {
                case Figure.FigureType.Rect:
                    _moved = true;
                    ResizeRect((RectFigure) selected, pos);
                    break;
                case Figure.FigureType.Ellipse:
                    _moved = true;
                    ResizeEllipse((EllipseFigure) selected, pos);
                    break;
                case Figure.FigureType.Line:
                    _moved = true;
                    ResizeLine((LineFigure) selected, pos);
                    break;
                case Figure.FigureType.Text:
                    _moved = true;
                    ResizeText((TextFigure) selected, pos);
                    break;
            }

            if ((_nowResizing != null) && (_nowResizing.Iterations != -1))
            {
                StepManager.RefreshToCurrentStep();
            }
        }
    }
}