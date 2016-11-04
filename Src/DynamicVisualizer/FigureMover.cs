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
        private double _offsetX = double.NaN;
        private double _offsetY = double.NaN;
        public TransformStep NowMoving;

        public void Reset()
        {
            _offsetX = _offsetY = double.NaN;
        }

        public void SetDownPos(Point pos)
        {
            _downPos = pos;
        }

        public void Move(Figure selected, Point pos)
        {
            if ((selected == Timeline.CurrentStep.Figure) &&
                ((Timeline.CurrentStep is MoveRectStep && (selected.Type == Figure.FigureType.Rect))
                 || (Timeline.CurrentStep is MoveCircleStep && (selected.Type == Figure.FigureType.Circle))))
                NowMoving = (TransformStep) Timeline.CurrentStep;
            else
                NowMoving = null;

            switch (selected.Type)
            {
                case Figure.FigureType.Rect:
                    var rf = (RectFigure) selected;
                    if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
                    {
                        _offsetX = _downPos.X - rf.X.CachedValue.AsDouble;
                        _offsetY = _downPos.Y - rf.Y.CachedValue.AsDouble;
                    }

                    if (NowMoving == null)
                    {
                        NowMoving = new MoveRectStep(rf, pos.X - _offsetX, pos.Y - _offsetY);
                        Timeline.Insert(NowMoving,
                            Timeline.CurrentStepIndex == -1 ? 0 : Timeline.CurrentStepIndex + 1);
                    }
                    else
                    {
                        switch (NowMoving.StepType)
                        {
                            case TransformStep.TransformStepType.MoveRect:
                                ((MoveRectStep) NowMoving).Move(pos.X - _offsetX, pos.Y - _offsetY);
                                break;
                        }
                    }
                    break;
                case Figure.FigureType.Circle:
                    var cf = (CircleFigure) selected;
                    if (double.IsNaN(_offsetX) || double.IsNaN(_offsetY))
                    {
                        _offsetX = _downPos.X - cf.X.CachedValue.AsDouble;
                        _offsetY = _downPos.Y - cf.Y.CachedValue.AsDouble;
                    }

                    if (NowMoving == null)
                    {
                        NowMoving = new MoveCircleStep(cf, pos.X - _offsetX, pos.Y - _offsetY);
                        Timeline.Insert(NowMoving,
                            Timeline.CurrentStepIndex == -1 ? 0 : Timeline.CurrentStepIndex + 1);
                    }
                    else
                    {
                        switch (NowMoving.StepType)
                        {
                            case TransformStep.TransformStepType.MoveCircle:
                                ((MoveCircleStep) NowMoving).Move(pos.X - _offsetX, pos.Y - _offsetY);
                                break;
                        }
                    }
                    break;
            }
        }
    }
}