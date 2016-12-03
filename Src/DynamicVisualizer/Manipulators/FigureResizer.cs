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

        public void Move(Figure selected, Point pos)
        {
            switch (selected.Type)
            {
                case Figure.FigureType.Rect:
                    var rf = (RectFigure) selected;
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
                        var srs = (ResizeRectStep) _nowResizing;
                        var snapped = StepManager.Snap(pos, _nowResizing.Figure);
                        if (snapped != null)
                        {
                            if (srs.ResizeAround == ResizeRectStep.Side.Right)
                                srs.Resize("(" + snapped.X.ExprString + ") - " + srs.Figure.Name + ".x");
                            else if (srs.ResizeAround == ResizeRectStep.Side.Left)
                                srs.Resize("(" + snapped.X.ExprString + ") - (" + srs.Figure.Name + ".x + (" +
                                           srs.Figure.Name + ".width))");
                            else if (srs.ResizeAround == ResizeRectStep.Side.Top)
                                srs.Resize("(" + snapped.Y.ExprString + ") - (" + srs.Figure.Name + ".y + (" +
                                           srs.Figure.Name + ".height))");
                            else if (srs.ResizeAround == ResizeRectStep.Side.Bottom)
                                srs.Resize("(" + snapped.Y.ExprString + ") - " + srs.Figure.Name + ".y");
                        }
                        else
                        {
                            if (srs.ResizeAround == ResizeRectStep.Side.Right)
                                srs.Resize(pos.X - _downPos.X);
                            else if (srs.ResizeAround == ResizeRectStep.Side.Left)
                                srs.Resize(pos.X - _downPos.X);
                            else if (srs.ResizeAround == ResizeRectStep.Side.Top)
                                srs.Resize(pos.Y - _downPos.Y);
                            else if (srs.ResizeAround == ResizeRectStep.Side.Bottom)
                                srs.Resize(pos.Y - _downPos.Y);
                        }
                    }
                    break;
                case Figure.FigureType.Ellipse:
                    var ef = (EllipseFigure) selected;
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
                        var srs = (ResizeEllipseStep) _nowResizing;
                        var snapped = StepManager.Snap(pos, _nowResizing.Figure);
                        if (snapped != null)
                        {
                            if (srs.ResizeAround == ResizeEllipseStep.Side.Right)
                                srs.Resize("(" + srs.Figure.Name + ".x - " + srs.Figure.Name + ".radius1 ) - (" +
                                           snapped.X.ExprString + ")");
                            else if (srs.ResizeAround == ResizeEllipseStep.Side.Left)
                                srs.Resize("(" + snapped.X.ExprString + ") - (" + srs.Figure.Name + ".x + (" +
                                           srs.Figure.Name + ".radius1))");
                            else if (srs.ResizeAround == ResizeEllipseStep.Side.Top)
                                srs.Resize("(" + srs.Figure.Name + ".y + " + srs.Figure.Name + ".radius2 ) - (" +
                                           snapped.Y.ExprString + ")");
                            else if (srs.ResizeAround == ResizeEllipseStep.Side.Bottom)
                                srs.Resize("(" + snapped.Y.ExprString + ") - (" + srs.Figure.Name + ".y - (" +
                                           srs.Figure.Name + ".radius2))");
                        }
                        else
                        {
                            if (srs.ResizeAround == ResizeEllipseStep.Side.Right)
                                srs.Resize(_downPos.X - pos.X);
                            else if (srs.ResizeAround == ResizeEllipseStep.Side.Left)
                                srs.Resize(pos.X - _downPos.X);
                            else if (srs.ResizeAround == ResizeEllipseStep.Side.Top)
                                srs.Resize(_downPos.Y - pos.Y);
                            else if (srs.ResizeAround == ResizeEllipseStep.Side.Bottom)
                                srs.Resize(pos.Y - _downPos.Y);
                        }
                    }
                    break;
            }
        }
    }
}