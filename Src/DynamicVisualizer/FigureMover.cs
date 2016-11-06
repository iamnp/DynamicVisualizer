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
                 || (Timeline.CurrentStep is MoveEllipseStep && (selected.Type == Figure.FigureType.Circle))))
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
                        switch (_nowMoving.StepType)
                        {
                            case TransformStep.TransformStepType.MoveRect:
                                ((MoveRectStep) _nowMoving).Move(pos.X - _offsetX, pos.Y - _offsetY);
                                break;
                        }
                    }
                    break;
                case Figure.FigureType.Circle:
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
                        switch (_nowMoving.StepType)
                        {
                            case TransformStep.TransformStepType.MoveEllipse:
                                ((MoveEllipseStep) _nowMoving).Move(pos.X - _offsetX, pos.Y - _offsetY);
                                break;
                        }
                    }
                    break;
            }
        }
    }
}