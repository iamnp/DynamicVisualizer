using System;
using System.Windows;
using DynamicVisualizer.Figures;
using DynamicVisualizer.Steps;
using DynamicVisualizer.Steps.Resize;

namespace DynamicVisualizer.Manipulators
{
    internal class FigureResizer
    {
        private Point _downPos;
        private TransformStep _nowResizing;
        private double _offsetX = double.NaN;
        private double _offsetY = double.NaN;

        public bool NowResizing => _nowResizing != null;

        public void SetDownPos(Point pos)
        {
            _downPos = pos;
        }

        public void Reset()
        {
            _offsetX = _offsetY = double.NaN;
            _nowResizing = null;
        }

        private void ResizeRect(RectFigure rf, Point pos)
        {
            if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
            {
                _offsetX = _downPos.X - rf.X.CachedValue.AsDouble;
                _offsetY = _downPos.Y - rf.Y.CachedValue.AsDouble;
            }

            if (_nowResizing == null)
            {
                var p = rf.PosInside(pos.X, pos.Y);
                p = new Point(Math.Abs(p.X), Math.Abs(p.Y));
                var smallW = Math.Abs(rf.Width.CachedValue.AsDouble/6.0);
                var smallH = Math.Abs(rf.Height.CachedValue.AsDouble/6.0);
                if (p.X < smallW)
                    _nowResizing = new ResizeRectStep(rf, ResizeRectStep.Side.Right, pos.X - _downPos.X);
                else if (p.X > 5.0*smallW)
                    _nowResizing = new ResizeRectStep(rf, ResizeRectStep.Side.Left, pos.X - _downPos.X);
                else if (p.Y < smallH)
                    _nowResizing = new ResizeRectStep(rf, ResizeRectStep.Side.Bottom, pos.Y - _downPos.Y);
                else if (p.Y > 5.0*smallH)
                    _nowResizing = new ResizeRectStep(rf, ResizeRectStep.Side.Top, pos.Y - _downPos.Y);
                if (_nowResizing == null) return;
                StepManager.Insert(_nowResizing,
                    StepManager.CurrentStepIndex == -1 ? 0 : StepManager.CurrentStepIndex + 1);
            }
            else
            {
                var rrs = (ResizeRectStep) _nowResizing;
                var snapped = StepManager.Snap(pos, _nowResizing.Figure);
                if (snapped != null)
                {
                    if (rrs.ResizeAround == ResizeRectStep.Side.Right)
                        rrs.Resize("(" + snapped.X.ExprString + ") - " + rrs.Figure.Name + ".x");
                    else if (rrs.ResizeAround == ResizeRectStep.Side.Left)
                        rrs.Resize("(" + snapped.X.ExprString + ") - (" + rrs.Figure.Name + ".x + (" +
                                   rrs.Figure.Name + ".width))");
                    else if (rrs.ResizeAround == ResizeRectStep.Side.Top)
                        rrs.Resize("(" + snapped.Y.ExprString + ") - (" + rrs.Figure.Name + ".y + (" +
                                   rrs.Figure.Name + ".height))");
                    else if (rrs.ResizeAround == ResizeRectStep.Side.Bottom)
                        rrs.Resize("(" + snapped.Y.ExprString + ") - " + rrs.Figure.Name + ".y");
                }
                else
                {
                    if (rrs.ResizeAround == ResizeRectStep.Side.Right)
                        rrs.Resize(pos.X - _downPos.X);
                    else if (rrs.ResizeAround == ResizeRectStep.Side.Left)
                        rrs.Resize(pos.X - _downPos.X);
                    else if (rrs.ResizeAround == ResizeRectStep.Side.Top)
                        rrs.Resize(pos.Y - _downPos.Y);
                    else if (rrs.ResizeAround == ResizeRectStep.Side.Bottom)
                        rrs.Resize(pos.Y - _downPos.Y);
                }
            }
        }

        private void ResizeEllipse(EllipseFigure ef, Point pos)
        {
            if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
            {
                _offsetX = _downPos.X - ef.X.CachedValue.AsDouble;
                _offsetY = _downPos.Y - ef.Y.CachedValue.AsDouble;
            }

            if (_nowResizing == null)
            {
                var p = ef.PosInside(pos.X, pos.Y);
                p = new Point(Math.Abs(p.X), Math.Abs(p.Y));
                var smallW = Math.Abs(ef.Radius1.CachedValue.AsDouble/3.0);
                var smallH = Math.Abs(ef.Radius2.CachedValue.AsDouble/3.0);
                if (p.X < smallW)
                    _nowResizing = new ResizeEllipseStep(ef, ResizeEllipseStep.Side.Right, pos.X - _downPos.X);
                else if (p.X > 5.0*smallW)
                    _nowResizing = new ResizeEllipseStep(ef, ResizeEllipseStep.Side.Left, pos.X - _downPos.X);
                else if (p.Y < smallH)
                    _nowResizing = new ResizeEllipseStep(ef, ResizeEllipseStep.Side.Bottom, pos.Y - _downPos.Y);
                else if (p.Y > 5.0*smallH)
                    _nowResizing = new ResizeEllipseStep(ef, ResizeEllipseStep.Side.Top, pos.Y - _downPos.Y);
                if (_nowResizing == null) return;
                StepManager.Insert(_nowResizing,
                    StepManager.CurrentStepIndex == -1 ? 0 : StepManager.CurrentStepIndex + 1);
            }
            else
            {
                var res = (ResizeEllipseStep) _nowResizing;
                var snapped = StepManager.Snap(pos, _nowResizing.Figure);
                if (snapped != null)
                {
                    if (res.ResizeAround == ResizeEllipseStep.Side.Right)
                        res.Resize("(" + res.Figure.Name + ".x - " + res.Figure.Name + ".radius1 ) - (" +
                                   snapped.X.ExprString + ")");
                    else if (res.ResizeAround == ResizeEllipseStep.Side.Left)
                        res.Resize("(" + snapped.X.ExprString + ") - (" + res.Figure.Name + ".x + (" +
                                   res.Figure.Name + ".radius1))");
                    else if (res.ResizeAround == ResizeEllipseStep.Side.Top)
                        res.Resize("(" + res.Figure.Name + ".y + " + res.Figure.Name + ".radius2 ) - (" +
                                   snapped.Y.ExprString + ")");
                    else if (res.ResizeAround == ResizeEllipseStep.Side.Bottom)
                        res.Resize("(" + snapped.Y.ExprString + ") - (" + res.Figure.Name + ".y - (" +
                                   res.Figure.Name + ".radius2))");
                }
                else
                {
                    if (res.ResizeAround == ResizeEllipseStep.Side.Right)
                        res.Resize(_downPos.X - pos.X);
                    else if (res.ResizeAround == ResizeEllipseStep.Side.Left)
                        res.Resize(pos.X - _downPos.X);
                    else if (res.ResizeAround == ResizeEllipseStep.Side.Top)
                        res.Resize(_downPos.Y - pos.Y);
                    else if (res.ResizeAround == ResizeEllipseStep.Side.Bottom)
                        res.Resize(pos.Y - _downPos.Y);
                }
            }
        }

        public void Move(Figure selected, Point pos)
        {
            switch (selected.Type)
            {
                case Figure.FigureType.Rect:
                    ResizeRect((RectFigure) selected, pos);
                    break;
                case Figure.FigureType.Ellipse:
                    ResizeEllipse((EllipseFigure) selected, pos);
                    break;
            }
        }
    }
}