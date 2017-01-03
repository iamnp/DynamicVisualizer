using System.Windows;
using DynamicVisualizer.Figures;
using DynamicVisualizer.Steps;
using DynamicVisualizer.Steps.Move;

namespace DynamicVisualizer.Manipulators
{
    internal class FigureMover
    {
        private Point _downPos;
        private bool _moved;
        private MoveStep _nowMoving;

        public bool NowMoving => _nowMoving != null;

        public bool Reset()
        {
            _nowMoving = null;
            return _moved;
        }

        public void SetDownPos(Point pos)
        {
            _moved = false;
            _downPos = pos;
        }

        private void MoveRect(RectFigure rf, Point pos)
        {
            if (_nowMoving == null)
            {
                _nowMoving = new MoveRectStep(rf, pos.X - _downPos.X, pos.Y - _downPos.Y);
                StepManager.InsertNext(_nowMoving);
            }
            else
            {
                var snapped = StepManager.Snap(pos, _nowMoving.Figure);
                Magnet snappedBy;
                if ((snapped != null) &&
                    ((snappedBy = StepManager.SnapTo(pos, _nowMoving.Figure.GetMagnets())) != null))
                {
                    if (snappedBy.EqualExprStrings(rf.TopLeft))
                    {
                        ((MoveRectStep) _nowMoving).Move(
                            "(" + snapped.X.ExprString + ") - (" + _nowMoving.Figure.Name + ".x)",
                            "(" + snapped.Y.ExprString + ") - (" + _nowMoving.Figure.Name + ".y)", snappedBy.Def,
                            snapped.Def);
                    }
                    else if (snappedBy.EqualExprStrings(rf.TopRight))
                    {
                        ((MoveRectStep) _nowMoving).Move(
                            "((" + snapped.X.ExprString + ") - (" + rf.Name + ".width)) - (" + _nowMoving.Figure.Name +
                            ".x)",
                            "(" + snapped.Y.ExprString + ") - (" + _nowMoving.Figure.Name + ".y)", snappedBy.Def,
                            snapped.Def);
                    }
                    else if (snappedBy.EqualExprStrings(rf.BottomLeft))
                    {
                        ((MoveRectStep) _nowMoving).Move(
                            "(" + snapped.X.ExprString + ") - (" + _nowMoving.Figure.Name + ".x)",
                            "((" + snapped.Y.ExprString + ") - (" + rf.Name + ".height)) - (" + _nowMoving.Figure.Name +
                            ".y)", snappedBy.Def, snapped.Def);
                    }
                    else if (snappedBy.EqualExprStrings(rf.BottomRight))
                    {
                        ((MoveRectStep) _nowMoving).Move(
                            "((" + snapped.X.ExprString + ") - (" + rf.Name + ".width)) - (" + _nowMoving.Figure.Name +
                            ".x)",
                            "((" + snapped.Y.ExprString + ") - (" + rf.Name + ".height)) - (" + _nowMoving.Figure.Name +
                            ".y)", snappedBy.Def, snapped.Def);
                    }
                    else if (snappedBy.EqualExprStrings(rf.Center))
                    {
                        ((MoveRectStep) _nowMoving).Move(
                            "((" + snapped.X.ExprString + ") - (" + rf.Name + ".width/2)) - (" + _nowMoving.Figure.Name +
                            ".x)",
                            "((" + snapped.Y.ExprString + ") - (" + rf.Name + ".height/2)) - (" + _nowMoving.Figure.Name +
                            ".y)", snappedBy.Def, snapped.Def);
                    }
                }
                else
                {
                    ((MoveRectStep) _nowMoving).Move(pos.X - _downPos.X, pos.Y - _downPos.Y);
                }
            }
        }

        private void MoveEllipse(EllipseFigure ef, Point pos)
        {
            if (_nowMoving == null)
            {
                _nowMoving = new MoveEllipseStep(ef, pos.X - _downPos.X, pos.Y - _downPos.Y);
                StepManager.InsertNext(_nowMoving);
            }
            else
            {
                var snapped = StepManager.Snap(pos, _nowMoving.Figure);
                Magnet snappedBy;
                if ((snapped != null) &&
                    ((snappedBy = StepManager.SnapTo(pos, _nowMoving.Figure.GetMagnets())) != null))
                {
                    if (snappedBy.EqualExprStrings(ef.Left))
                    {
                        ((MoveEllipseStep) _nowMoving).Move(
                            "((" + snapped.X.ExprString + ") + (" + ef.Name + ".radius1)) - (" + _nowMoving.Figure.Name +
                            ".x)",
                            "(" + snapped.Y.ExprString + ") - (" + _nowMoving.Figure.Name + ".y)",
                            snappedBy.Def, snapped.Def);
                    }
                    else if (snappedBy.EqualExprStrings(ef.Right))
                    {
                        ((MoveEllipseStep) _nowMoving).Move(
                            "((" + snapped.X.ExprString + ") - (" + ef.Name + ".radius1)) - (" + _nowMoving.Figure.Name +
                            ".x)",
                            "(" + snapped.Y.ExprString + ") - (" + _nowMoving.Figure.Name + ".y)",
                            snappedBy.Def, snapped.Def);
                    }
                    else if (snappedBy.EqualExprStrings(ef.Top))
                    {
                        ((MoveEllipseStep) _nowMoving).Move(
                            "(" + snapped.X.ExprString + ") - (" + _nowMoving.Figure.Name + ".x)",
                            "((" + snapped.Y.ExprString + ") + (" + ef.Name + ".radius2)) - (" + _nowMoving.Figure.Name +
                            ".y)", snappedBy.Def, snapped.Def);
                    }
                    else if (snappedBy.EqualExprStrings(ef.Bottom))
                    {
                        ((MoveEllipseStep) _nowMoving).Move(
                            "(" + snapped.X.ExprString + ") - (" + _nowMoving.Figure.Name + ".x)",
                            "((" + snapped.Y.ExprString + ") - (" + ef.Name + ".radius2)) - (" + _nowMoving.Figure.Name +
                            ".y)", snappedBy.Def, snapped.Def);
                    }
                    else if (snappedBy.EqualExprStrings(ef.Center))
                    {
                        ((MoveEllipseStep) _nowMoving).Move(
                            "(" + snapped.X.ExprString + ") - (" + _nowMoving.Figure.Name + ".x)",
                            "(" + snapped.Y.ExprString + ") - (" + _nowMoving.Figure.Name + ".y)", snappedBy.Def,
                            snapped.Def);
                    }
                }
                else
                {
                    ((MoveEllipseStep) _nowMoving).Move(pos.X - _downPos.X, pos.Y - _downPos.Y);
                }
            }
        }

        private void MoveLine(LineFigure lf, Point pos)
        {
            if (_nowMoving == null)
            {
                _nowMoving = new MoveLineStep(lf, pos.X - _downPos.X, pos.Y - _downPos.Y);
                StepManager.InsertNext(_nowMoving);
            }
            else
            {
                var snapped = StepManager.Snap(pos, _nowMoving.Figure);
                Magnet snappedBy;
                if ((snapped != null) &&
                    ((snappedBy = StepManager.SnapTo(pos, _nowMoving.Figure.GetMagnets())) != null))
                {
                    if (snappedBy.EqualExprStrings(lf.Start))
                    {
                        ((MoveLineStep) _nowMoving).Move(
                            "(" + snapped.X.ExprString + ") - (" + _nowMoving.Figure.Name + ".x)",
                            "(" + snapped.Y.ExprString + ") - (" + _nowMoving.Figure.Name + ".y)", snappedBy.Def,
                            snapped.Def);
                    }
                    else if (snappedBy.EqualExprStrings(lf.Center))
                    {
                        ((MoveLineStep) _nowMoving).Move(
                            "((" + snapped.X.ExprString + ") - (" + lf.Name + ".width/2)) - (" + _nowMoving.Figure.Name +
                            ".x)",
                            "((" + snapped.Y.ExprString + ") - (" + lf.Name + ".height/2)) - (" + _nowMoving.Figure.Name +
                            ".y)", snappedBy.Def, snapped.Def);
                    }
                    else if (snappedBy.EqualExprStrings(lf.End))
                    {
                        ((MoveLineStep) _nowMoving).Move(
                            "((" + snapped.X.ExprString + ") - (" + lf.Name + ".width)) - (" + _nowMoving.Figure.Name +
                            ".x)",
                            "((" + snapped.Y.ExprString + ") - (" + lf.Name + ".height)) - (" + _nowMoving.Figure.Name +
                            ".y)", snappedBy.Def, snapped.Def);
                    }
                }
                else
                {
                    ((MoveLineStep) _nowMoving).Move(pos.X - _downPos.X, pos.Y - _downPos.Y);
                }
            }
        }

        private void MoveText(TextFigure tf, Point pos)
        {
            if (_nowMoving == null)
            {
                _nowMoving = new MoveTextStep(tf, pos.X - _downPos.X, pos.Y - _downPos.Y);
                StepManager.InsertNext(_nowMoving);
            }
            else
            {
                var snapped = StepManager.Snap(pos, _nowMoving.Figure);
                Magnet snappedBy;
                if ((snapped != null) &&
                    ((snappedBy = StepManager.SnapTo(pos, _nowMoving.Figure.GetMagnets())) != null))
                {
                    if (snappedBy.EqualExprStrings(tf.Start))
                    {
                        ((MoveTextStep) _nowMoving).Move(
                            "(" + snapped.X.ExprString + ") - (" + _nowMoving.Figure.Name + ".x)",
                            "(" + snapped.Y.ExprString + ") - (" + _nowMoving.Figure.Name + ".y)", snappedBy.Def,
                            snapped.Def);
                    }
                    else if (snappedBy.EqualExprStrings(tf.Center))
                    {
                        ((MoveTextStep) _nowMoving).Move(
                            "((" + snapped.X.ExprString + ") - (" + tf.Name + ".width/2)) - (" + _nowMoving.Figure.Name +
                            ".x)",
                            "((" + snapped.Y.ExprString + ") - (" + tf.Name + ".height/2)) - (" + _nowMoving.Figure.Name +
                            ".y)", snappedBy.Def, snapped.Def);
                    }
                    else if (snappedBy.EqualExprStrings(tf.End))
                    {
                        ((MoveTextStep) _nowMoving).Move(
                            "((" + snapped.X.ExprString + ") - (" + tf.Name + ".width)) - (" + _nowMoving.Figure.Name +
                            ".x)",
                            "((" + snapped.Y.ExprString + ") - (" + tf.Name + ".height)) - (" + _nowMoving.Figure.Name +
                            ".y)", snappedBy.Def, snapped.Def);
                    }
                }
                else
                {
                    ((MoveTextStep) _nowMoving).Move(pos.X - _downPos.X, pos.Y - _downPos.Y);
                }
            }
        }

        public void Move(Figure selected, Point pos)
        {
            switch (selected.Type)
            {
                case Figure.FigureType.Rect:
                    _moved = true;
                    MoveRect((RectFigure) selected, pos);
                    break;
                case Figure.FigureType.Ellipse:
                    _moved = true;
                    MoveEllipse((EllipseFigure) selected, pos);
                    break;
                case Figure.FigureType.Line:
                    _moved = true;
                    MoveLine((LineFigure) selected, pos);
                    break;
                case Figure.FigureType.Text:
                    _moved = true;
                    MoveText((TextFigure) selected, pos);
                    break;
            }

            if ((_nowMoving != null) && (_nowMoving.Iterations != -1))
            {
                StepManager.RefreshToCurrentStep();
            }
        }
    }
}