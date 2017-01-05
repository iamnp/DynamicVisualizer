using System;
using System.Windows;
using DynamicVisualizer.Figures;
using DynamicVisualizer.Steps;
using DynamicVisualizer.Steps.Scale;

namespace DynamicVisualizer.Manipulators
{
    internal class FigureScaler
    {
        private Point _downPos;
        private bool _moved;
        private ScaleStep _nowScaling;
        private double _originalH;
        private double _originalW;
        private double _originalX;
        private double _originalY;

        public bool NowScailing => _nowScaling != null;

        public void SetDownPos(Point pos)
        {
            _moved = false;
            _downPos = pos;
        }

        public bool Reset()
        {
            _nowScaling = null;
            return _moved;
        }

        private void ScaleRect(RectFigure rf, Point pos)
        {
            if (_nowScaling == null)
            {
                _originalW = rf.Width.CachedValue.AsDouble;
                _originalH = rf.Height.CachedValue.AsDouble;
                var snappedTo = StepManager.SnapTo(pos, rf.GetMagnets(), rf.Center);
                if ((snappedTo == rf.Left) && (Math.Abs(_originalW) > Utils.Tolerance))
                {
                    _nowScaling = new ScaleRectStep(rf, ScaleRectStep.Side.Right,
                        1 - (pos.X - _downPos.X) / _originalW);
                }
                else if ((snappedTo == rf.Right) && (Math.Abs(_originalW) > Utils.Tolerance))
                {
                    _nowScaling = new ScaleRectStep(rf, ScaleRectStep.Side.Left,
                        1 + (pos.X - _downPos.X) / _originalW);
                }
                else if ((snappedTo == rf.Top) && (Math.Abs(_originalH) > Utils.Tolerance))
                {
                    _nowScaling = new ScaleRectStep(rf, ScaleRectStep.Side.Bottom,
                        1 - (pos.Y - _downPos.Y) / _originalH);
                }
                else if ((snappedTo == rf.Bottom) && (Math.Abs(_originalH) > Utils.Tolerance))
                {
                    _nowScaling = new ScaleRectStep(rf, ScaleRectStep.Side.Top,
                        1 + (pos.Y - _downPos.Y) / _originalH);
                }
                if (_nowScaling == null)
                {
                    return;
                }
                StepManager.InsertNext(_nowScaling);
            }
            else
            {
                var srs = (ScaleRectStep) _nowScaling;

                if (srs.ScaleAround == ScaleRectStep.Side.Right)
                {
                    srs.Scale(1 - (pos.X - _downPos.X) / _originalW);
                }
                else if (srs.ScaleAround == ScaleRectStep.Side.Left)
                {
                    srs.Scale(1 + (pos.X - _downPos.X) / _originalW);
                }
                else if (srs.ScaleAround == ScaleRectStep.Side.Top)
                {
                    srs.Scale(1 + (pos.Y - _downPos.Y) / _originalH);
                }
                else if (srs.ScaleAround == ScaleRectStep.Side.Bottom)
                {
                    srs.Scale(1 - (pos.Y - _downPos.Y) / _originalH);
                }
            }
        }

        private void ScaleEllipse(EllipseFigure ef, Point pos)
        {
            if (_nowScaling == null)
            {
                _originalW = ef.Radius1.CachedValue.AsDouble;
                _originalH = ef.Radius2.CachedValue.AsDouble;
                var snappedTo = StepManager.SnapTo(pos, ef.GetMagnets(), ef.Center);
                if ((snappedTo == ef.Left) && (Math.Abs(_originalW) > Utils.Tolerance))
                {
                    _nowScaling = new ScaleEllipseStep(ef, ScaleEllipseStep.Side.Right,
                        1 - (pos.X - _downPos.X) / _originalW);
                }
                else if ((snappedTo == ef.Right) && (Math.Abs(_originalW) > Utils.Tolerance))
                {
                    _nowScaling = new ScaleEllipseStep(ef, ScaleEllipseStep.Side.Left,
                        1 + (pos.X - _downPos.X) / _originalW);
                }
                else if ((snappedTo == ef.Top) && (Math.Abs(_originalH) > Utils.Tolerance))
                {
                    _nowScaling = new ScaleEllipseStep(ef, ScaleEllipseStep.Side.Bottom,
                        1 + (pos.Y - _downPos.Y) / _originalH);
                }
                else if ((snappedTo == ef.Bottom) && (Math.Abs(_originalH) > Utils.Tolerance))
                {
                    _nowScaling = new ScaleEllipseStep(ef, ScaleEllipseStep.Side.Top,
                        1 - (pos.Y - _downPos.Y) / _originalH);
                }
                if (_nowScaling == null)
                {
                    return;
                }
                StepManager.InsertNext(_nowScaling);
            }
            else
            {
                var srs = (ScaleEllipseStep) _nowScaling;

                if (srs.ScaleAround == ScaleEllipseStep.Side.Right)
                {
                    srs.Scale(1 - (pos.X - _downPos.X) / _originalW);
                }
                else if (srs.ScaleAround == ScaleEllipseStep.Side.Left)
                {
                    srs.Scale(1 + (pos.X - _downPos.X) / _originalW);
                }
                else if (srs.ScaleAround == ScaleEllipseStep.Side.Top)
                {
                    srs.Scale(1 - (pos.Y - _downPos.Y) / _originalH);
                }
                else if (srs.ScaleAround == ScaleEllipseStep.Side.Bottom)
                {
                    srs.Scale(1 + (pos.Y - _downPos.Y) / _originalH);
                }
            }
        }

        private void ScaleLine(LineFigure lf, Point pos)
        {
            if (_nowScaling == null)
            {
                _originalX = lf.X.CachedValue.AsDouble;
                _originalY = lf.Y.CachedValue.AsDouble;
                _originalW = lf.Width.CachedValue.AsDouble;
                _originalH = lf.Height.CachedValue.AsDouble;
                var snappedTo = StepManager.SnapTo(pos, lf.GetMagnets(), lf.Center);
                var bLenSquared = _originalW * _originalW + _originalH * _originalH;
                if ((snappedTo == lf.Start) && (Math.Abs(bLenSquared) > Utils.Tolerance))
                {
                    var ax = _originalX + _originalW - pos.X;
                    var ay = _originalY + _originalH - pos.Y;

                    _nowScaling = new ScaleLineStep(lf, ScaleLineStep.Side.End,
                        (ax * _originalW + ay * _originalH) / bLenSquared);
                }
                else if ((snappedTo == lf.End) && (Math.Abs(bLenSquared) > Utils.Tolerance))
                {
                    var ax = pos.X - _originalX;
                    var ay = pos.Y - _originalY;

                    _nowScaling = new ScaleLineStep(lf, ScaleLineStep.Side.Start,
                        (ax * _originalW + ay * _originalH) / bLenSquared);
                }
                if (_nowScaling == null)
                {
                    return;
                }
                StepManager.InsertNext(_nowScaling);
            }
            else
            {
                var sls = (ScaleLineStep) _nowScaling;

                if (sls.ScaleAround == ScaleLineStep.Side.Start)
                {
                    var bLenSquared = _originalW * _originalW + _originalH * _originalH;

                    var ax = pos.X - _originalX;
                    var ay = pos.Y - _originalY;

                    sls.Scale((ax * _originalW + ay * _originalH) / bLenSquared);
                }
                else if (sls.ScaleAround == ScaleLineStep.Side.End)
                {
                    var bLenSquared = _originalW * _originalW + _originalH * _originalH;

                    var ax = _originalX + _originalW - pos.X;
                    var ay = _originalY + _originalH - pos.Y;

                    sls.Scale((ax * _originalW + ay * _originalH) / bLenSquared);
                }
            }
        }

        private void ScaleText(TextFigure tf, Point pos)
        {
            if (_nowScaling == null)
            {
                _originalX = tf.X.CachedValue.AsDouble;
                _originalY = tf.Y.CachedValue.AsDouble;
                _originalW = tf.Width.CachedValue.AsDouble;
                _originalH = tf.Height.CachedValue.AsDouble;
                var snappedTo = StepManager.SnapTo(pos, tf.GetMagnets(), tf.Center);
                var bLenSquared = _originalW * _originalW + _originalH * _originalH;
                if ((snappedTo == tf.Start) && (Math.Abs(bLenSquared) > Utils.Tolerance))
                {
                    var ax = _originalX + _originalW - pos.X;
                    var ay = _originalY + _originalH - pos.Y;

                    _nowScaling = new ScaleTextStep(tf, ScaleTextStep.Side.End,
                        (ax * _originalW + ay * _originalH) / bLenSquared);
                }
                else if ((snappedTo == tf.End) && (Math.Abs(bLenSquared) > Utils.Tolerance))
                {
                    var ax = pos.X - _originalX;
                    var ay = pos.Y - _originalY;

                    _nowScaling = new ScaleTextStep(tf, ScaleTextStep.Side.Start,
                        (ax * _originalW + ay * _originalH) / bLenSquared);
                }
                if (_nowScaling == null)
                {
                    return;
                }
                StepManager.InsertNext(_nowScaling);
            }
            else
            {
                var sts = (ScaleTextStep) _nowScaling;

                if (sts.ScaleAround == ScaleTextStep.Side.Start)
                {
                    var bLenSquared = _originalW * _originalW + _originalH * _originalH;

                    var ax = pos.X - _originalX;
                    var ay = pos.Y - _originalY;

                    sts.Scale((ax * _originalW + ay * _originalH) / bLenSquared);
                }
                else if (sts.ScaleAround == ScaleTextStep.Side.End)
                {
                    var bLenSquared = _originalW * _originalW + _originalH * _originalH;

                    var ax = _originalX + _originalW - pos.X;
                    var ay = _originalY + _originalH - pos.Y;

                    sts.Scale((ax * _originalW + ay * _originalH) / bLenSquared);
                }
            }
        }

        public void Move(Figure selected, Point pos)
        {
            switch (selected.Type)
            {
                case Figure.FigureType.Rect:
                    _moved = true;
                    ScaleRect((RectFigure) selected, pos);
                    break;
                case Figure.FigureType.Ellipse:
                    _moved = true;
                    ScaleEllipse((EllipseFigure) selected, pos);
                    break;
                case Figure.FigureType.Line:
                    _moved = true;
                    ScaleLine((LineFigure) selected, pos);
                    break;
                case Figure.FigureType.Text:
                    _moved = true;
                    ScaleText((TextFigure) selected, pos);
                    break;
            }

            if ((_nowScaling != null) && ((_nowScaling.Iterations != -1) || (StepManager.FinalStep != null)))
            {
                StepManager.RefreshToCurrentStep();
            }
        }
    }
}