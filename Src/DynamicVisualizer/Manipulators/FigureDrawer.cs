using System;
using System.Windows;
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
            {
                return new DrawRectStep(_startPos.X, _startPos.Y, 0, 0);
            }
            return new DrawRectStep(snapped.X.ExprString, snapped.Y.ExprString, "0", "0", snapped.Def);
        }

        private DrawEllipseStep StartDrawEllipse()
        {
            var snapped = StepManager.Snap(_startPos);
            if (snapped == null)
            {
                return new DrawEllipseStep(_startPos.X, _startPos.Y, 0);
            }
            return new DrawEllipseStep(snapped.X.ExprString, snapped.Y.ExprString, "0", snapped.Def);
        }

        private DrawLineStep StartDrawLine()
        {
            var snapped = StepManager.Snap(_startPos);
            if (snapped == null)
            {
                return new DrawLineStep(_startPos.X, _startPos.Y, 0, 0);
            }
            return new DrawLineStep(snapped.X.ExprString, snapped.Y.ExprString, "0", "0", snapped.Def);
        }

        public void Start(Point pos)
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
                case DrawStep.DrawStepType.DrawLine:
                    _nowDrawing = StartDrawLine();
                    break;
            }

            StepManager.InsertNext(_nowDrawing);

            if (_nowDrawing.Iterations != -1)
            {
                StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex);
            }
        }

        private void ResizeDrawnRect(Point pos)
        {
            var snapped = StepManager.Snap(pos, _nowDrawing.Figure);
            if (snapped == null)
            {
                ((DrawRectStep) _nowDrawing).ReInit(pos.X - _startPos.X, pos.Y - _startPos.Y);
            }
            else
            {
                ((DrawRectStep) _nowDrawing).ReInit(
                    "(" + snapped.X.ExprString + ") - " + _nowDrawing.Figure.Name + ".x",
                    "(" + snapped.Y.ExprString + ") - " + _nowDrawing.Figure.Name + ".y", snapped.Def);
            }
        }

        private void ResizeDrawnEllipse(Point pos)
        {
            var snapped = StepManager.Snap(pos, _nowDrawing.Figure);
            if (snapped == null)
            {
                var dx = pos.X - _startPos.X;
                var dy = pos.Y - _startPos.Y;
                ((DrawEllipseStep) _nowDrawing).ReInit(Math.Sqrt(dx * dx + dy * dy));
            }
            else
            {
                var dx = "((" + snapped.X.ExprString + ") - " + _nowDrawing.Figure.Name + ".x)";
                var dy = "((" + snapped.Y.ExprString + ") - " + _nowDrawing.Figure.Name + ".y)";
                ((DrawEllipseStep) _nowDrawing).ReInit("sqrt((" + dx + " * " + dx + ") + (" + dy + " * " +
                                                       dy +
                                                       "))", snapped.Def);
            }
        }

        private void ResizeDrawnLine(Point pos)
        {
            var snapped = StepManager.Snap(pos, _nowDrawing.Figure);
            if (snapped == null)
            {
                ((DrawLineStep) _nowDrawing).ReInit(pos.X - _startPos.X, pos.Y - _startPos.Y);
            }
            else
            {
                ((DrawLineStep) _nowDrawing).ReInit(
                    "(" + snapped.X.ExprString + ") - " + _nowDrawing.Figure.Name + ".x",
                    "(" + snapped.Y.ExprString + ") - " + _nowDrawing.Figure.Name + ".y", snapped.Def);
            }
        }

        public void Move(Point pos)
        {
            if (_nowDrawing != null)
            {
                switch (DrawStepType)
                {
                    case DrawStep.DrawStepType.DrawRect:
                        ResizeDrawnRect(pos);
                        break;
                    case DrawStep.DrawStepType.DrawEllipse:
                        ResizeDrawnEllipse(pos);
                        break;
                    case DrawStep.DrawStepType.DrawLine:
                        ResizeDrawnLine(pos);
                        break;
                }
                if (_nowDrawing.Iterations != -1)
                {
                    StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex);
                }
            }
        }

        public void Finish()
        {
            _nowDrawing = null;
        }
    }
}