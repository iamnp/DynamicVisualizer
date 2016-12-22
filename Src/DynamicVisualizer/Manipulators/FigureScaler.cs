﻿using System.Windows;
using DynamicVisualizer.Figures;
using DynamicVisualizer.Steps;
using DynamicVisualizer.Steps.Scale;

namespace DynamicVisualizer.Manipulators
{
    internal class FigureScaler
    {
        private Point _downPos;
        private ScaleStep _nowScaling;
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

        private void ScaleRect(RectFigure rf, Point pos)
        {
            if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
            {
                _offsetX = _downPos.X - rf.X.CachedValue.AsDouble;
                _offsetY = _downPos.Y - rf.Y.CachedValue.AsDouble;
            }

            if (_nowScaling == null)
            {
                var snappedTo = StepManager.SnapTo(pos, rf.GetMagnets());
                if (snappedTo == rf.Left)
                {
                    _nowScaling = new ScaleRectStep(rf, ScaleRectStep.Side.Right,
                        1 - (pos.X - _downPos.X) / rf.Width.CachedValue.AsDouble);
                }
                else if (snappedTo == rf.Right)
                {
                    _nowScaling = new ScaleRectStep(rf, ScaleRectStep.Side.Left,
                        1 + (pos.X - _downPos.X) / rf.Width.CachedValue.AsDouble);
                }
                else if (snappedTo == rf.Top)
                {
                    _nowScaling = new ScaleRectStep(rf, ScaleRectStep.Side.Bottom,
                        1 - (pos.Y - _downPos.Y) / rf.Height.CachedValue.AsDouble);
                }
                else if (snappedTo == rf.Bottom)
                {
                    _nowScaling = new ScaleRectStep(rf, ScaleRectStep.Side.Top,
                        1 + (pos.Y - _downPos.Y) / rf.Height.CachedValue.AsDouble);
                }
                if (_nowScaling == null)
                {
                    return;
                }
                StepManager.Insert(_nowScaling,
                    StepManager.CurrentStepIndex == -1 ? 0 : StepManager.CurrentStepIndex + 1);
            }
            else
            {
                var srs = (ScaleRectStep) _nowScaling;

                if (srs.ScaleAround == ScaleRectStep.Side.Right)
                {
                    srs.Scale(1 - (pos.X - _downPos.X) / srs.WidthOrig);
                }
                else if (srs.ScaleAround == ScaleRectStep.Side.Left)
                {
                    srs.Scale(1 + (pos.X - _downPos.X) / srs.WidthOrig);
                }
                else if (srs.ScaleAround == ScaleRectStep.Side.Top)
                {
                    srs.Scale(1 + (pos.Y - _downPos.Y) / srs.HeightOrig);
                }
                else if (srs.ScaleAround == ScaleRectStep.Side.Bottom)
                {
                    srs.Scale(1 - (pos.Y - _downPos.Y) / srs.HeightOrig);
                }
            }
        }

        private void ScaleEllipse(EllipseFigure ef, Point pos)
        {
            if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
            {
                _offsetX = _downPos.X - ef.X.CachedValue.AsDouble;
                _offsetY = _downPos.Y - ef.Y.CachedValue.AsDouble;
            }

            if (_nowScaling == null)
            {
                var snappedTo = StepManager.SnapTo(pos, ef.GetMagnets());
                if (snappedTo == ef.Left)
                {
                    _nowScaling = new ScaleEllipseStep(ef, ScaleEllipseStep.Side.Right,
                        1 - (pos.X - _downPos.X) / ef.Radius1.CachedValue.AsDouble);
                }
                else if (snappedTo == ef.Right)
                {
                    _nowScaling = new ScaleEllipseStep(ef, ScaleEllipseStep.Side.Left,
                        1 + (pos.X - _downPos.X) / ef.Radius1.CachedValue.AsDouble);
                }
                else if (snappedTo == ef.Top)
                {
                    _nowScaling = new ScaleEllipseStep(ef, ScaleEllipseStep.Side.Bottom,
                        1 + (pos.Y - _downPos.Y) / ef.Radius2.CachedValue.AsDouble);
                }
                else if (snappedTo == ef.Bottom)
                {
                    _nowScaling = new ScaleEllipseStep(ef, ScaleEllipseStep.Side.Top,
                        1 - (pos.Y - _downPos.Y) / ef.Radius2.CachedValue.AsDouble);
                }
                if (_nowScaling == null)
                {
                    return;
                }
                StepManager.Insert(_nowScaling,
                    StepManager.CurrentStepIndex == -1 ? 0 : StepManager.CurrentStepIndex + 1);
            }
            else
            {
                var srs = (ScaleEllipseStep) _nowScaling;

                if (srs.ScaleAround == ScaleEllipseStep.Side.Right)
                {
                    srs.Scale(1 - (pos.X - _downPos.X) / srs.Radius1Orig);
                }
                else if (srs.ScaleAround == ScaleEllipseStep.Side.Left)
                {
                    srs.Scale(1 + (pos.X - _downPos.X) / srs.Radius1Orig);
                }
                else if (srs.ScaleAround == ScaleEllipseStep.Side.Top)
                {
                    srs.Scale(1 - (pos.Y - _downPos.Y) / srs.Radius2Orig);
                }
                else if (srs.ScaleAround == ScaleEllipseStep.Side.Bottom)
                {
                    srs.Scale(1 + (pos.Y - _downPos.Y) / srs.Radius2Orig);
                }
            }
        }

        private void ScaleLine(LineFigure lf, Point pos)
        {
            if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
            {
                _offsetX = _downPos.X - lf.X.CachedValue.AsDouble;
                _offsetY = _downPos.Y - lf.Y.CachedValue.AsDouble;
            }

            if (_nowScaling == null)
            {
                var snappedTo = StepManager.SnapTo(pos, lf.GetMagnets());
                if (snappedTo == lf.Start)
                {
                    var bx = lf.Width.CachedValue.AsDouble;
                    var by = lf.Height.CachedValue.AsDouble;
                    var bLenSquared = bx * bx + by * by;

                    var ax = lf.X.CachedValue.AsDouble + lf.Width.CachedValue.AsDouble - pos.X;
                    var ay = lf.Y.CachedValue.AsDouble + lf.Height.CachedValue.AsDouble - pos.Y;

                    _nowScaling = new ScaleLineStep(lf, ScaleLineStep.Side.End,
                        (ax * bx + ay * by) / bLenSquared);
                }
                else if (snappedTo == lf.End)
                {
                    var bx = lf.Width.CachedValue.AsDouble;
                    var by = lf.Height.CachedValue.AsDouble;
                    var bLenSquared = bx * bx + by * by;

                    var ax = pos.X - lf.X.CachedValue.AsDouble;
                    var ay = pos.Y - lf.Y.CachedValue.AsDouble;

                    _nowScaling = new ScaleLineStep(lf, ScaleLineStep.Side.Start,
                        (ax * bx + ay * by) / bLenSquared);
                }
                if (_nowScaling == null)
                {
                    return;
                }
                StepManager.Insert(_nowScaling,
                    StepManager.CurrentStepIndex == -1 ? 0 : StepManager.CurrentStepIndex + 1);
            }
            else
            {
                var sls = (ScaleLineStep) _nowScaling;

                if (sls.ScaleAround == ScaleLineStep.Side.Start)
                {
                    var bx = sls.WidthOrig;
                    var by = sls.HeightOrig;
                    var bLenSquared = bx * bx + by * by;

                    var ax = pos.X - sls.XCachedDouble;
                    var ay = pos.Y - sls.YCachedDouble;

                    sls.Scale((ax * bx + ay * by) / bLenSquared);
                }
                else if (sls.ScaleAround == ScaleLineStep.Side.End)
                {
                    var bx = sls.WidthOrig;
                    var by = sls.HeightOrig;
                    var bLenSquared = bx * bx + by * by;

                    var ax = sls.XCachedDouble + sls.WidthOrig - pos.X;
                    var ay = sls.YCachedDouble + sls.HeightOrig - pos.Y;

                    sls.Scale((ax * bx + ay * by) / bLenSquared);
                }
            }
        }

        public void Move(Figure selected, Point pos)
        {
            switch (selected.Type)
            {
                case Figure.FigureType.Rect:
                    ScaleRect((RectFigure) selected, pos);
                    break;
                case Figure.FigureType.Ellipse:
                    ScaleEllipse((EllipseFigure) selected, pos);
                    break;
                case Figure.FigureType.Line:
                    ScaleLine((LineFigure) selected, pos);
                    break;
            }
        }
    }
}