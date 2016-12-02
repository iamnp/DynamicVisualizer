using System.Windows;
using DynamicVisualizer.Figures;
using DynamicVisualizer.Steps;
using DynamicVisualizer.Steps.Move;

namespace DynamicVisualizer.Manipulators
{
    internal class FigureMover
    {
        private Point _downPos;
        private TransformStep _nowMoving;
        private double _offsetX = double.NaN;
        private double _offsetY = double.NaN;

        public bool NowMoving => _nowMoving != null;

        public void Reset()
        {
            _offsetX = _offsetY = double.NaN;
            _nowMoving = null;
        }

        public void SetDownPos(Point pos)
        {
            _downPos = pos;
        }

        public void Move(Figure selected, Point pos)
        {
            if ((selected == StepManager.CurrentStep.Figure) &&
                ((StepManager.CurrentStep is MoveRectStep && (selected.Type == Figure.FigureType.Rect))
                 || (StepManager.CurrentStep is MoveEllipseStep && (selected.Type == Figure.FigureType.Ellipse))))
                _nowMoving = (TransformStep) StepManager.CurrentStep;
            else
                _nowMoving = null;

            switch (selected.Type)
            {
                case Figure.FigureType.Rect:
                    var rf = (RectFigure) selected;
                    if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
                    {
                        _offsetX = _downPos.X - rf.X.CachedValue.AsDouble;
                        _offsetY = _downPos.Y - rf.Y.CachedValue.AsDouble;
                    }

                    if (_nowMoving == null)
                    {
                        _nowMoving = new MoveRectStep(rf, pos.X - _offsetX, pos.Y - _offsetY);
                        StepManager.Insert(_nowMoving,
                            StepManager.CurrentStepIndex == -1 ? 0 : StepManager.CurrentStepIndex + 1);
                    }
                    else
                    {
                        var snapped = StepManager.Snap(pos, _nowMoving.Figure);
                        Magnet snappedBy = null;
                        if ((snapped != null) &&
                            ((snappedBy = StepManager.SnapTo(pos, _nowMoving.Figure.GetMagnets())) != null))
                        {
                            if (snappedBy.EqualExprStrings(rf.TopLeft))
                                ((MoveRectStep) _nowMoving).Move(snapped.X.ExprString, snapped.Y.ExprString);
                            else if (snappedBy.EqualExprStrings(rf.TopRight))
                                ((MoveRectStep) _nowMoving).Move(
                                    "(" + snapped.X.ExprString + ") - (" + rf.Name + ".width)", snapped.Y.ExprString);
                            else if (snappedBy.EqualExprStrings(rf.BottomLeft))
                                ((MoveRectStep) _nowMoving).Move(snapped.X.ExprString,
                                    "(" + snapped.Y.ExprString + ") - (" + rf.Name + ".height)");
                            else if (snappedBy.EqualExprStrings(rf.BottomRight))
                                ((MoveRectStep) _nowMoving).Move(
                                    "(" + snapped.X.ExprString + ") - (" + rf.Name + ".width)",
                                    "(" + snapped.Y.ExprString + ") - (" + rf.Name + ".height)");
                            else if (snappedBy.EqualExprStrings(rf.Center))
                                ((MoveRectStep) _nowMoving).Move(
                                    "(" + snapped.X.ExprString + ") - (" + rf.Name + ".width/2)",
                                    "(" + snapped.Y.ExprString + ") - (" + rf.Name + ".height/2)");
                        }
                        else
                        {
                            ((MoveRectStep) _nowMoving).Move(pos.X - _offsetX, pos.Y - _offsetY);
                        }
                    }
                    break;
                case Figure.FigureType.Ellipse:
                    var cf = (EllipseFigure) selected;
                    if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
                    {
                        _offsetX = _downPos.X - cf.X.CachedValue.AsDouble;
                        _offsetY = _downPos.Y - cf.Y.CachedValue.AsDouble;
                    }

                    if (_nowMoving == null)
                    {
                        _nowMoving = new MoveEllipseStep(cf, pos.X - _offsetX, pos.Y - _offsetY);
                        StepManager.Insert(_nowMoving,
                            StepManager.CurrentStepIndex == -1 ? 0 : StepManager.CurrentStepIndex + 1);
                    }
                    else
                    {
                        var snapped = StepManager.Snap(pos, _nowMoving.Figure);
                        Magnet snappedBy = null;
                        if ((snapped != null) &&
                            ((snappedBy = StepManager.SnapTo(pos, _nowMoving.Figure.GetMagnets())) != null))
                        {
                            if (snappedBy.EqualExprStrings(cf.Left))
                                ((MoveEllipseStep) _nowMoving).Move(
                                    "(" + snapped.X.ExprString + ") + (" + cf.Name + ".radius1)", snapped.Y.ExprString);
                            else if (snappedBy.EqualExprStrings(cf.Right))
                                ((MoveEllipseStep) _nowMoving).Move(
                                    "(" + snapped.X.ExprString + ") - (" + cf.Name + ".radius1)", snapped.Y.ExprString);
                            else if (snappedBy.EqualExprStrings(cf.Top))
                                ((MoveEllipseStep) _nowMoving).Move(snapped.X.ExprString,
                                    "(" + snapped.Y.ExprString + ") + (" + cf.Name + ".radius2)");
                            else if (snappedBy.EqualExprStrings(cf.Bottom))
                                ((MoveEllipseStep) _nowMoving).Move(snapped.X.ExprString,
                                    "(" + snapped.Y.ExprString + ") - (" + cf.Name + ".radius2)");
                            else if (snappedBy.EqualExprStrings(cf.Center))
                                ((MoveEllipseStep) _nowMoving).Move(snapped.X.ExprString, snapped.Y.ExprString);
                        }
                        else
                        {
                            ((MoveEllipseStep) _nowMoving).Move(pos.X - _offsetX, pos.Y - _offsetY);
                        }
                    }
                    break;
            }
        }
    }
}