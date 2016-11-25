using System;
using System.Windows;
using DynamicVisualizer.Logic.Storyboard;
using DynamicVisualizer.Logic.Storyboard.Figures;
using DynamicVisualizer.Logic.Storyboard.Steps;
using DynamicVisualizer.Logic.Storyboard.Steps.Transform;

namespace DynamicVisualizer
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
                    var rf = (RectFigure)selected;
                    if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
                    {
                        _offsetX = _downPos.X - rf.X.CachedValue.AsDouble;
                        _offsetY = _downPos.Y - rf.Y.CachedValue.AsDouble;
                    }

                    if (_nowResizing == null)
                    {
                        var p = rf.PosInside(pos.X, pos.Y);
                        p = new Point(Math.Abs(p.X), Math.Abs(p.Y));
                        var smallW = Math.Abs(rf.Width.CachedValue.AsDouble / 6.0);
                        var smallH = Math.Abs(rf.Height.CachedValue.AsDouble / 6.0);
                        if (p.X < smallW)
                            _nowResizing = new ResizeRectStep(rf, ResizeRectStep.Side.Right, pos.X - _downPos.X);
                        else if (p.X > 5.0 * smallW)
                            _nowResizing = new ResizeRectStep(rf, ResizeRectStep.Side.Left, pos.X - _downPos.X);
                        else if (p.Y < smallH)
                            _nowResizing = new ResizeRectStep(rf, ResizeRectStep.Side.Bottom, pos.Y - _downPos.Y);
                        else if (p.Y > 5.0 * smallH)
                            _nowResizing = new ResizeRectStep(rf, ResizeRectStep.Side.Top, pos.Y - _downPos.Y);
                        if (_nowResizing == null) return;
                        Timeline.Insert(_nowResizing,
                            Timeline.CurrentStepIndex == -1 ? 0 : Timeline.CurrentStepIndex + 1);
                    }
                    else
                    {
                        var srs = (ResizeRectStep)_nowResizing;
                        var snapped = Timeline.Snap(pos, _nowResizing.Figure);
                        if (snapped != null)
                        {
                            if (srs.ResizeAround == ResizeRectStep.Side.Right)
                                srs.Resize("(" + snapped.X.ExprString + ") - (" + srs.XCachedDouble + ")");
                            else if (srs.ResizeAround == ResizeRectStep.Side.Left)
                                srs.Resize("(" + snapped.X.ExprString + ") - (" + (srs.XCachedDouble + srs.WidthOrig).Str() + ")");
                            else if (srs.ResizeAround == ResizeRectStep.Side.Top)
                                srs.Resize("(" + snapped.Y.ExprString + ") - (" + (srs.YCachedDouble + srs.HeightOrig).Str() + ")");
                            else if (srs.ResizeAround == ResizeRectStep.Side.Bottom)
                                srs.Resize("(" + snapped.Y.ExprString + ") - (" + srs.YCachedDouble + ")");
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
                    var ef = (EllipseFigure)selected;
                    if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
                    {
                        _offsetX = _downPos.X - ef.X.CachedValue.AsDouble;
                        _offsetY = _downPos.Y - ef.Y.CachedValue.AsDouble;
                    }

                    if (_nowResizing == null)
                    {
                        var p = ef.PosInside(pos.X, pos.Y);
                        p = new Point(Math.Abs(p.X), Math.Abs(p.Y));
                        var smallW = Math.Abs(ef.Radius1.CachedValue.AsDouble / 3.0);
                        var smallH = Math.Abs(ef.Radius2.CachedValue.AsDouble / 3.0);
                        if (p.X < smallW)
                            _nowResizing = new ResizeEllipseStep(ef, ResizeEllipseStep.Side.Right, pos.X - _downPos.X);
                        else if (p.X > 5.0 * smallW)
                            _nowResizing = new ResizeEllipseStep(ef, ResizeEllipseStep.Side.Left, pos.X - _downPos.X);
                        else if (p.Y < smallH)
                            _nowResizing = new ResizeEllipseStep(ef, ResizeEllipseStep.Side.Bottom, pos.Y - _downPos.Y);
                        else if (p.Y > 5.0 * smallH)
                            _nowResizing = new ResizeEllipseStep(ef, ResizeEllipseStep.Side.Top, pos.Y - _downPos.Y);
                        if (_nowResizing == null) return;
                        Timeline.Insert(_nowResizing,
                            Timeline.CurrentStepIndex == -1 ? 0 : Timeline.CurrentStepIndex + 1);
                    }
                    else
                    {
                        var srs = (ResizeEllipseStep)_nowResizing;

                        if (srs.ResizeAround == ResizeEllipseStep.Side.Right)
                            srs.Resize(pos.X - _downPos.X);
                        else if (srs.ResizeAround == ResizeEllipseStep.Side.Left)
                            srs.Resize(pos.X - _downPos.X);
                        else if (srs.ResizeAround == ResizeEllipseStep.Side.Top)
                            srs.Resize(pos.Y - _downPos.Y);
                        else if (srs.ResizeAround == ResizeEllipseStep.Side.Bottom)
                            srs.Resize(pos.Y - _downPos.Y);
                    }
                    break;
            }
        }
    }
}