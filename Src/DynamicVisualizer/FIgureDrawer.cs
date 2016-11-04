using System;
using System.Windows;
using DynamicVisualizer.Logic.Storyboard;
using DynamicVisualizer.Logic.Storyboard.Steps;
using DynamicVisualizer.Logic.Storyboard.Steps.Draw;

namespace DynamicVisualizer
{
    internal class FigureDrawer
    {
        private DrawStep _nowDrawing;
        private Point _startPos;
        public DrawStep.DrawStepType DrawStepType = DrawStep.DrawStepType.DrawRect;

        public void Start(Point pos)
        {
            _startPos = pos;
            switch (DrawStepType)
            {
                case DrawStep.DrawStepType.DrawRect:
                    _nowDrawing = new DrawRectStep(_startPos.X, _startPos.Y, 0, 0);
                    break;
                case DrawStep.DrawStepType.DrawCircle:
                    _nowDrawing = new DrawCircleStep(_startPos.X, _startPos.Y, 0);
                    break;
            }
            Timeline.Insert(_nowDrawing, Timeline.CurrentStepIndex == -1 ? 0 : Timeline.CurrentStepIndex + 1);
        }

        public void Move(Point pos)
        {
            if (_nowDrawing != null)
                switch (DrawStepType)
                {
                    case DrawStep.DrawStepType.DrawRect:
                        ((DrawRectStep) _nowDrawing).ReInit(_startPos.X, _startPos.Y, pos.X - _startPos.X,
                            pos.Y - _startPos.Y);
                        break;

                    case DrawStep.DrawStepType.DrawCircle:
                        var dx = pos.X - _startPos.X;
                        var dy = pos.Y - _startPos.Y;
                        ((DrawCircleStep) _nowDrawing).ReInit(_startPos.X, _startPos.Y, Math.Sqrt(dx*dx + dy*dy));
                        break;
                }
        }

        public void Finish()
        {
            _nowDrawing = null;
        }
    }
}