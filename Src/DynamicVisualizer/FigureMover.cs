using System.Windows;
using DynamicVisualizer.Logic.Storyboard;
using DynamicVisualizer.Logic.Storyboard.Figures;
using DynamicVisualizer.Logic.Storyboard.Steps;
using DynamicVisualizer.Logic.Storyboard.Steps.Transform;

namespace DynamicVisualizer
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
            if ((selected == Timeline.CurrentStep.Figure) &&
                ((Timeline.CurrentStep is MoveRectStep && (selected.Type == Figure.FigureType.Rect))
                 || (Timeline.CurrentStep is MoveEllipseStep && (selected.Type == Figure.FigureType.Ellipse))))
                _nowMoving = (TransformStep) Timeline.CurrentStep;
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
                        Timeline.Insert(_nowMoving,
                            Timeline.CurrentStepIndex == -1 ? 0 : Timeline.CurrentStepIndex + 1);
                    }
                    else
                    {
                        var snapped = Timeline.Snap(pos, _nowMoving.Figure);
                        Magnet snappedBy = null;
                        if ((snapped != null) &&
                            ((snappedBy = Timeline.SnapTo(pos, _nowMoving.Figure.GetMagnets())) != null))
                        {
                            if ((snappedBy.X.ExprString == rf.TopLeft.X.ExprString)
                                && (snappedBy.Y.ExprString == rf.TopLeft.Y.ExprString))
                                ((MoveRectStep) _nowMoving).Move(snapped.X.ExprString, snapped.Y.ExprString);
                            else if ((snappedBy.X.ExprString == rf.TopRight.X.ExprString)
                                     && (snappedBy.Y.ExprString == rf.TopRight.Y.ExprString))
                                ((MoveRectStep) _nowMoving).Move(
                                    "(" + snapped.X.ExprString + ") - (" + rf.Name + ".width)", snapped.Y.ExprString);
                            else if ((snappedBy.X.ExprString == rf.BottomLeft.X.ExprString)
                                     && (snappedBy.Y.ExprString == rf.BottomLeft.Y.ExprString))
                                ((MoveRectStep) _nowMoving).Move(snapped.X.ExprString,
                                    "(" + snapped.Y.ExprString + ") - (" + rf.Name + ".height)");
                            else if ((snappedBy.X.ExprString == rf.BottomRight.X.ExprString)
                                     && (snappedBy.Y.ExprString == rf.BottomRight.Y.ExprString))
                                ((MoveRectStep) _nowMoving).Move(
                                    "(" + snapped.X.ExprString + ") - (" + rf.Name + ".width)",
                                    "(" + snapped.Y.ExprString + ") - (" + rf.Name + ".height)");
                            else if ((snappedBy.X.ExprString == rf.Center.X.ExprString)
                                     && (snappedBy.Y.ExprString == rf.Center.Y.ExprString))
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
                        Timeline.Insert(_nowMoving,
                            Timeline.CurrentStepIndex == -1 ? 0 : Timeline.CurrentStepIndex + 1);
                    }
                    else
                    {
                        var snapped = Timeline.Snap(pos, _nowMoving.Figure);
                        Magnet snappedBy = null;
                        if ((snapped != null) &&
                            ((snappedBy = Timeline.SnapTo(pos, _nowMoving.Figure.GetMagnets())) != null))
                        {
                            if ((snappedBy.X.ExprString == cf.Left.X.ExprString)
                                && (snappedBy.Y.ExprString == cf.Left.Y.ExprString))
                                ((MoveEllipseStep) _nowMoving).Move(
                                    "(" + snapped.X.ExprString + ") + (" + cf.Name + ".radius1)", snapped.Y.ExprString);
                            else if ((snappedBy.X.ExprString == cf.Right.X.ExprString)
                                     && (snappedBy.Y.ExprString == cf.Right.Y.ExprString))
                                ((MoveEllipseStep) _nowMoving).Move(
                                    "(" + snapped.X.ExprString + ") - (" + cf.Name + ".radius1)", snapped.Y.ExprString);
                            else if ((snappedBy.X.ExprString == cf.Top.X.ExprString)
                                     && (snappedBy.Y.ExprString == cf.Top.Y.ExprString))
                                ((MoveEllipseStep) _nowMoving).Move(snapped.X.ExprString,
                                    "(" + snapped.Y.ExprString + ") + (" + cf.Name + ".radius2)");
                            else if ((snappedBy.X.ExprString == cf.Bottom.X.ExprString)
                                     && (snappedBy.Y.ExprString == cf.Bottom.Y.ExprString))
                                ((MoveEllipseStep) _nowMoving).Move(snapped.X.ExprString,
                                    "(" + snapped.Y.ExprString + ") - (" + cf.Name + ".radius2)");
                            else if ((snappedBy.X.ExprString == cf.Center.X.ExprString)
                                     && (snappedBy.Y.ExprString == cf.Center.Y.ExprString))
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