using System;
using System.Windows;
using DynamicVisualizer.Figures;
using DynamicVisualizer.Steps;
using DynamicVisualizer.Steps.Draw;

namespace DynamicVisualizer.Manipulators
{
    internal class FigureDrawer
    {
        private DrawStep _nowDrawing;
        private Point _startPos;
        public DrawStep.DrawStepType DrawStepType = DrawStep.DrawStepType.DrawRect;

        public bool NowDrawing => _nowDrawing != null;

        private DrawRectStep StartDrawRect()
        {
            var snapped = StepManager.Snap(_startPos);
            if (snapped == null)
                return new DrawRectStep(_startPos.X, _startPos.Y, 0, 0);
            return new DrawRectStep(snapped.X.ExprString, snapped.Y.ExprString, "0", "0");
        }

        private DrawEllipseStep StartDrawEllipse()
        {
            var snapped = StepManager.Snap(_startPos);
            if (snapped == null)
                return new DrawEllipseStep(_startPos.X, _startPos.Y, 0);
            return new DrawEllipseStep(snapped.X.ExprString, snapped.Y.ExprString, "0");
        }

        public Figure Start(Point pos)
        {
            _startPos = pos;

            switch (DrawStepType)
            {
                case DrawStep.DrawStepType.DrawRect:
                    _nowDrawing = StartDrawRect();
                    break;
                case DrawStep.DrawStepType.DrawEllipse:
                    _nowDrawing = StartDrawEllipse();
                    break;
            }

            StepManager.Insert(_nowDrawing, StepManager.CurrentStepIndex == -1 ? 0 : StepManager.CurrentStepIndex + 1);
            return _nowDrawing.Figure;
        }

        private void MoveDrawnRect(Point pos)
        {
            var snapped = StepManager.Snap(pos, _nowDrawing.Figure);
            if (snapped == null)
                ((DrawRectStep) _nowDrawing).ReInit(pos.X - _startPos.X, pos.Y - _startPos.Y);
            else
                ((DrawRectStep) _nowDrawing).ReInit(
                    "(" + snapped.X.ExprString + ") - " + _nowDrawing.Figure.Name + ".x",
                    "(" + snapped.Y.ExprString + ") - " + _nowDrawing.Figure.Name + ".y");
        }

        private void MoveDrawnEllipse(Point pos)
        {
            var snapped = StepManager.Snap(pos, _nowDrawing.Figure);
            if (snapped == null)
            {
                var dx = pos.X - _startPos.X;
                var dy = pos.Y - _startPos.Y;
                ((DrawEllipseStep) _nowDrawing).ReInit(Math.Sqrt(dx*dx + dy*dy));
            }
            else
            {
                var dx = "((" + snapped.X.ExprString + ") - " + _nowDrawing.Figure.Name + ".x)";
                var dy = "((" + snapped.Y.ExprString + ") - " + _nowDrawing.Figure.Name + ".y)";
                ((DrawEllipseStep) _nowDrawing).ReInit("sqrt((" + dx + " * " + dx + ") + (" + dy + " * " +
                                                       dy +
                                                       "))");
            }
        }

        public void Move(Point pos)
        {
            if (_nowDrawing != null)
                switch (DrawStepType)
                {
                    case DrawStep.DrawStepType.DrawRect:
                        MoveDrawnRect(pos);
                        break;
                    case DrawStep.DrawStepType.DrawEllipse:
                        MoveDrawnEllipse(pos);
                        break;
                }
        }

        public void Finish()
        {
            _nowDrawing = null;
        }
    }
}