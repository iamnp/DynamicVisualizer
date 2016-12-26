using System;
using System.Windows;
using DynamicVisualizer.Figures;
using DynamicVisualizer.Steps;
using DynamicVisualizer.Steps.Rotate;

namespace DynamicVisualizer.Manipulators
{
    internal class FigureRotater
    {
        private bool _moved;
        private RotateStep _nowRotating;

        public bool NowRotating => _nowRotating != null;

        public void SetDownPos(Point pos)
        {
            _moved = false;
        }

        public bool Reset()
        {
            _nowRotating = null;
            return _moved;
        }

        private double AngleBetween(double ax, double ay, double bx, double by)
        {
            var angle = Math.Acos((ax * bx + ay * by)
                                  / (Math.Sqrt(ax * ax + ay * ay) * Math.Sqrt(bx * bx + by * by)));
            if (ax * by - ay * bx < 0)
            {
                return angle;
            }
            return -angle;
        }

        private void RotateLine(LineFigure lf, Point pos)
        {
            if (_nowRotating == null)
            {
                var snappedTo = StepManager.SnapTo(pos, lf.GetMagnets(), lf.Center);
                if (snappedTo == lf.Start)
                {
                    var angle = AngleBetween(lf.X.CachedValue.AsDouble + lf.Width.CachedValue.AsDouble - pos.X,
                        lf.Y.CachedValue.AsDouble + lf.Height.CachedValue.AsDouble - pos.Y,
                        lf.Width.CachedValue.AsDouble,
                        lf.Height.CachedValue.AsDouble);

                    _nowRotating = new RotateLineStep(lf, RotateLineStep.Side.End,
                        angle / (2 * Math.PI));
                }
                else if (snappedTo == lf.End)
                {
                    var angle = AngleBetween(pos.X - lf.X.CachedValue.AsDouble, pos.Y - lf.Y.CachedValue.AsDouble,
                        lf.Width.CachedValue.AsDouble, lf.Height.CachedValue.AsDouble);

                    _nowRotating = new RotateLineStep(lf, RotateLineStep.Side.Start,
                        angle / (2 * Math.PI));
                }
                if (_nowRotating == null)
                {
                    return;
                }
                StepManager.InsertNext(_nowRotating);
            }
            else
            {
                var rls = (RotateLineStep) _nowRotating;

                if (rls.RotateAround == RotateLineStep.Side.Start)
                {
                    var angle = AngleBetween(pos.X - rls.XCachedDouble, pos.Y - rls.YCachedDouble,
                        rls.WidthOrig, rls.HeightOrig);

                    rls.Rotate(angle / (2 * Math.PI));
                }
                else if (rls.RotateAround == RotateLineStep.Side.End)
                {
                    var angle = AngleBetween(rls.XCachedDouble + rls.WidthOrig - pos.X,
                        rls.YCachedDouble + rls.HeightOrig - pos.Y,
                        rls.WidthOrig, rls.HeightOrig);

                    rls.Rotate(angle / (2 * Math.PI));
                }
            }
        }

        public void Move(Figure selected, Point pos)
        {
            switch (selected.Type)
            {
                case Figure.FigureType.Line:
                    _moved = true;
                    RotateLine((LineFigure) selected, pos);
                    break;
            }

            if ((_nowRotating != null) && (_nowRotating.Iterations != -1))
            {
                StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex);
            }
        }
    }
}