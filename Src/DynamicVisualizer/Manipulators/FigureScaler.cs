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
                var snappedTo = StepManager.SnapTo(pos, rf.GetMagnets(), rf.Center);
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
                StepManager.InsertNext(_nowScaling);
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
            if (_nowScaling == null)
            {
                var snappedTo = StepManager.SnapTo(pos, ef.GetMagnets(), ef.Center);
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
                StepManager.InsertNext(_nowScaling);
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
            if (_nowScaling == null)
            {
                var snappedTo = StepManager.SnapTo(pos, lf.GetMagnets(), lf.Center);
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
                StepManager.InsertNext(_nowScaling);
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

        private void ScaleText(TextFigure tf, Point pos)
        {
            if (_nowScaling == null)
            {
                var snappedTo = StepManager.SnapTo(pos, tf.GetMagnets(), tf.Center);
                if (snappedTo == tf.Start)
                {
                    var bx = tf.Width.CachedValue.AsDouble;
                    var by = tf.Height.CachedValue.AsDouble;
                    var bLenSquared = bx * bx + by * by;

                    var ax = tf.X.CachedValue.AsDouble + tf.Width.CachedValue.AsDouble - pos.X;
                    var ay = tf.Y.CachedValue.AsDouble + tf.Height.CachedValue.AsDouble - pos.Y;

                    _nowScaling = new ScaleTextStep(tf, ScaleTextStep.Side.End,
                        (ax * bx + ay * by) / bLenSquared);
                }
                else if (snappedTo == tf.End)
                {
                    var bx = tf.Width.CachedValue.AsDouble;
                    var by = tf.Height.CachedValue.AsDouble;
                    var bLenSquared = bx * bx + by * by;

                    var ax = pos.X - tf.X.CachedValue.AsDouble;
                    var ay = pos.Y - tf.Y.CachedValue.AsDouble;

                    _nowScaling = new ScaleTextStep(tf, ScaleTextStep.Side.Start,
                        (ax * bx + ay * by) / bLenSquared);
                }
                if (_nowScaling == null)
                {
                    return;
                }
                StepManager.InsertNext(_nowScaling);
            }
            else
            {
                var sls = (ScaleTextStep) _nowScaling;

                if (sls.ScaleAround == ScaleTextStep.Side.Start)
                {
                    var bx = sls.WidthOrig;
                    var by = sls.HeightOrig;
                    var bLenSquared = bx * bx + by * by;

                    var ax = pos.X - sls.XCachedDouble;
                    var ay = pos.Y - sls.YCachedDouble;

                    sls.Scale((ax * bx + ay * by) / bLenSquared);
                }
                else if (sls.ScaleAround == ScaleTextStep.Side.End)
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

            if ((_nowScaling != null) && (_nowScaling.Iterations != -1))
            {
                StepManager.RefreshToCurrentStep();
            }
        }
    }
}