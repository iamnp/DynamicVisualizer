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
        private double _originalH;
        private double _originalW;
        private double _originalX;
        private double _originalY;

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

        private void RotateLine(LineFigure lf, Point pos)
        {
            if (_nowRotating == null)
            {
                var snappedTo = StepManager.SnapTo(pos, lf.GetMagnets(), lf.Center);
                if (snappedTo == lf.Start)
                {
                    var angle = Utils.AngleBetween(lf.X.CachedValue.AsDouble + lf.Width.CachedValue.AsDouble - pos.X,
                        lf.Y.CachedValue.AsDouble + lf.Height.CachedValue.AsDouble - pos.Y,
                        lf.Width.CachedValue.AsDouble,
                        lf.Height.CachedValue.AsDouble);

                    _nowRotating = new RotateLineStep(lf, RotateLineStep.Side.End,
                        angle / (2 * Math.PI));
                }
                else if (snappedTo == lf.End)
                {
                    var angle = Utils.AngleBetween(pos.X - lf.X.CachedValue.AsDouble, pos.Y - lf.Y.CachedValue.AsDouble,
                        lf.Width.CachedValue.AsDouble, lf.Height.CachedValue.AsDouble);

                    _nowRotating = new RotateLineStep(lf, RotateLineStep.Side.Start,
                        angle / (2 * Math.PI));
                }
                if (_nowRotating == null)
                {
                    return;
                }
                _originalX = lf.X.CachedValue.AsDouble;
                _originalY = lf.Y.CachedValue.AsDouble;
                _originalW = lf.Width.CachedValue.AsDouble;
                _originalH = lf.Height.CachedValue.AsDouble;
                StepManager.InsertNext(_nowRotating);
            }
            else
            {
                var rls = (RotateLineStep) _nowRotating;

                if (rls.RotateAround == RotateLineStep.Side.Start)
                {
                    var angle = Utils.AngleBetween(pos.X - _originalX, pos.Y - _originalY,
                        _originalW, _originalH);

                    rls.Rotate(angle / (2 * Math.PI));
                }
                else if (rls.RotateAround == RotateLineStep.Side.End)
                {
                    var angle = Utils.AngleBetween(_originalX + _originalW - pos.X,
                        _originalY + _originalH - pos.Y,
                        _originalW, _originalH);

                    rls.Rotate(angle / (2 * Math.PI));
                }
            }
        }

        private void RotateText(TextFigure tf, Point pos)
        {
            if (_nowRotating == null)
            {
                var snappedTo = StepManager.SnapTo(pos, tf.GetMagnets(), tf.Center);
                if (snappedTo == tf.Start)
                {
                    var angle = Utils.AngleBetween(tf.X.CachedValue.AsDouble + tf.Width.CachedValue.AsDouble - pos.X,
                        tf.Y.CachedValue.AsDouble + tf.Height.CachedValue.AsDouble - pos.Y,
                        tf.Width.CachedValue.AsDouble,
                        tf.Height.CachedValue.AsDouble);

                    _nowRotating = new RotateTextStep(tf, RotateTextStep.Side.End,
                        angle / (2 * Math.PI));
                }
                else if (snappedTo == tf.End)
                {
                    var angle = Utils.AngleBetween(pos.X - tf.X.CachedValue.AsDouble, pos.Y - tf.Y.CachedValue.AsDouble,
                        tf.Width.CachedValue.AsDouble, tf.Height.CachedValue.AsDouble);

                    _nowRotating = new RotateTextStep(tf, RotateTextStep.Side.Start,
                        angle / (2 * Math.PI));
                }
                if (_nowRotating == null)
                {
                    return;
                }
                _originalX = tf.X.CachedValue.AsDouble;
                _originalY = tf.Y.CachedValue.AsDouble;
                _originalW = tf.Width.CachedValue.AsDouble;
                _originalH = tf.Height.CachedValue.AsDouble;
                StepManager.InsertNext(_nowRotating);
            }
            else
            {
                var rls = (RotateTextStep) _nowRotating;

                if (rls.RotateAround == RotateTextStep.Side.Start)
                {
                    var angle = Utils.AngleBetween(pos.X - _originalX, pos.Y - _originalY,
                        _originalW, _originalH);

                    rls.Rotate(angle / (2 * Math.PI));
                }
                else if (rls.RotateAround == RotateTextStep.Side.End)
                {
                    var angle = Utils.AngleBetween(_originalX + _originalW - pos.X,
                        _originalY + _originalH - pos.Y,
                        _originalW, _originalH);

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
                case Figure.FigureType.Text:
                    _moved = true;
                    RotateText((TextFigure) selected, pos);
                    break;
            }

            if ((_nowRotating != null) && ((_nowRotating.Iterations != -1) || (StepManager.FinalStep != null)))
            {
                StepManager.RefreshToCurrentStep();
            }
        }
    }
}