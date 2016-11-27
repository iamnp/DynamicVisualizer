using System;
using System.Windows;
using DynamicVisualizer.Logic.Figures;
using DynamicVisualizer.Logic.Steps;
using DynamicVisualizer.Logic.Steps.Transform;

namespace DynamicVisualizer
{
    internal class FigureScaler
    {
        private Point _downPos;
        private TransformStep _nowScaling;
        private double _offsetX = double.NaN;
        private double _offsetY = double.NaN;

        public bool NowScailing => _nowScaling != null;

        public void SetDownPos(Point pos)
        {
            _downPos = pos;
        }

        public void Reset()
        {
            _offsetX = _offsetY = double.NaN;
            _nowScaling = null;
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

                    if (_nowScaling == null)
                    {
                        var p = rf.PosInside(pos.X, pos.Y);
                        p = new Point(Math.Abs(p.X), Math.Abs(p.Y));
                        var smallW = Math.Abs(rf.Width.CachedValue.AsDouble/6.0);
                        var smallH = Math.Abs(rf.Height.CachedValue.AsDouble/6.0);
                        if (p.X < smallW)
                            _nowScaling = new ScaleRectStep(rf, ScaleRectStep.Side.Right,
                                1 - (pos.X - _downPos.X)/rf.Width.CachedValue.AsDouble);
                        else if (p.X > 5.0*smallW)
                            _nowScaling = new ScaleRectStep(rf, ScaleRectStep.Side.Left,
                                1 + (pos.X - _downPos.X)/rf.Width.CachedValue.AsDouble);
                        else if (p.Y < smallH)
                            _nowScaling = new ScaleRectStep(rf, ScaleRectStep.Side.Bottom,
                                1 - (pos.Y - _downPos.Y)/rf.Height.CachedValue.AsDouble);
                        else if (p.Y > 5.0*smallH)
                            _nowScaling = new ScaleRectStep(rf, ScaleRectStep.Side.Top,
                                1 + (pos.Y - _downPos.Y)/rf.Height.CachedValue.AsDouble);
                        if (_nowScaling == null) return;
                        StepManager.Insert(_nowScaling,
                            StepManager.CurrentStepIndex == -1 ? 0 : StepManager.CurrentStepIndex + 1);
                    }
                    else
                    {
                        var srs = (ScaleRectStep) _nowScaling;

                        if (srs.ScaleAround == ScaleRectStep.Side.Right)
                            srs.Scale(1 - (pos.X - _downPos.X)/srs.WidthOrig);
                        else if (srs.ScaleAround == ScaleRectStep.Side.Left)
                            srs.Scale(1 + (pos.X - _downPos.X)/srs.WidthOrig);
                        else if (srs.ScaleAround == ScaleRectStep.Side.Top)
                            srs.Scale(1 + (pos.Y - _downPos.Y)/srs.HeightOrig);
                        else if (srs.ScaleAround == ScaleRectStep.Side.Bottom)
                            srs.Scale(1 - (pos.Y - _downPos.Y)/srs.HeightOrig);
                    }
                    break;
                case Figure.FigureType.Ellipse:
                    var ef = (EllipseFigure) selected;
                    if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
                    {
                        _offsetX = _downPos.X - ef.X.CachedValue.AsDouble;
                        _offsetY = _downPos.Y - ef.Y.CachedValue.AsDouble;
                    }

                    if (_nowScaling == null)
                    {
                        var p = ef.PosInside(pos.X, pos.Y);
                        p = new Point(Math.Abs(p.X), Math.Abs(p.Y));
                        var smallW = Math.Abs(ef.Radius1.CachedValue.AsDouble/3.0);
                        var smallH = Math.Abs(ef.Radius2.CachedValue.AsDouble/3.0);
                        if (p.X < smallW)
                            _nowScaling = new ScaleEllipseStep(ef, ScaleEllipseStep.Side.Right,
                                1 - (pos.X - _downPos.X)/ef.Radius1.CachedValue.AsDouble);
                        else if (p.X > 5.0*smallW)
                            _nowScaling = new ScaleEllipseStep(ef, ScaleEllipseStep.Side.Left,
                                1 + (pos.X - _downPos.X)/ef.Radius1.CachedValue.AsDouble);
                        else if (p.Y < smallH)
                            _nowScaling = new ScaleEllipseStep(ef, ScaleEllipseStep.Side.Bottom,
                                1 + (pos.Y - _downPos.Y)/ef.Radius2.CachedValue.AsDouble);
                        else if (p.Y > 5.0*smallH)
                            _nowScaling = new ScaleEllipseStep(ef, ScaleEllipseStep.Side.Top,
                                1 - (pos.Y - _downPos.Y)/ef.Radius2.CachedValue.AsDouble);
                        if (_nowScaling == null) return;
                        StepManager.Insert(_nowScaling,
                            StepManager.CurrentStepIndex == -1 ? 0 : StepManager.CurrentStepIndex + 1);
                    }
                    else
                    {
                        var srs = (ScaleEllipseStep) _nowScaling;

                        if (srs.ScaleAround == ScaleEllipseStep.Side.Right)
                            srs.Scale(1 - (pos.X - _downPos.X)/srs.Radius1Orig);
                        else if (srs.ScaleAround == ScaleEllipseStep.Side.Left)
                            srs.Scale(1 + (pos.X - _downPos.X)/srs.Radius1Orig);
                        else if (srs.ScaleAround == ScaleEllipseStep.Side.Top)
                            srs.Scale(1 - (pos.Y - _downPos.Y)/srs.Radius2Orig);
                        else if (srs.ScaleAround == ScaleEllipseStep.Side.Bottom)
                            srs.Scale(1 + (pos.Y - _downPos.Y)/srs.Radius2Orig);
                    }
                    break;
            }
        }
    }
}