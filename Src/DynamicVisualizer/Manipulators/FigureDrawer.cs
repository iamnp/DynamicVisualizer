﻿using System;
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

        public Figure Start(Point pos)
        {
            _startPos = pos;
            var snapped = StepManager.Snap(_startPos);
            if (snapped != null)
            {
                _startPos = new Point(snapped.X.CachedValue.AsDouble, snapped.Y.CachedValue.AsDouble);
                switch (DrawStepType)
                {
                    case DrawStep.DrawStepType.DrawRect:
                        _nowDrawing = new DrawRectStep(snapped.X.ExprString, snapped.Y.ExprString, "0", "0");
                        break;
                    case DrawStep.DrawStepType.DrawEllipse:
                        _nowDrawing = new DrawEllipseStep(snapped.X.ExprString, snapped.Y.ExprString, "0");
                        break;
                }
            }
            else
            {
                switch (DrawStepType)
                {
                    case DrawStep.DrawStepType.DrawRect:
                        _nowDrawing = new DrawRectStep(_startPos.X, _startPos.Y, 0, 0);
                        break;
                    case DrawStep.DrawStepType.DrawEllipse:
                        _nowDrawing = new DrawEllipseStep(_startPos.X, _startPos.Y, 0);
                        break;
                }
            }
            StepManager.Insert(_nowDrawing, StepManager.CurrentStepIndex == -1 ? 0 : StepManager.CurrentStepIndex + 1);
            return _nowDrawing.Figure;
        }

        public void Move(Point pos)
        {
            if (_nowDrawing != null)
            {
                var snapped = StepManager.Snap(pos, _nowDrawing.Figure);
                if (snapped != null)
                    switch (DrawStepType)
                    {
                        case DrawStep.DrawStepType.DrawRect:
                            ((DrawRectStep) _nowDrawing).ReInit(
                                "(" + snapped.X.ExprString + ") - " + _nowDrawing.Figure.Name + ".x",
                                "(" + snapped.Y.ExprString + ") - " + _nowDrawing.Figure.Name + ".y");
                            break;

                        case DrawStep.DrawStepType.DrawEllipse:
                            var dx = "((" + snapped.X.ExprString + ") - " + _nowDrawing.Figure.Name + ".x)";
                            var dy = "((" + snapped.Y.ExprString + ") - " + _nowDrawing.Figure.Name + ".y)";
                            ((DrawEllipseStep) _nowDrawing).ReInit("sqrt((" + dx + " * " + dx + ") + (" + dy + " * " +
                                                                   dy +
                                                                   "))");
                            break;
                    }
                else
                    switch (DrawStepType)
                    {
                        case DrawStep.DrawStepType.DrawRect:
                            ((DrawRectStep) _nowDrawing).ReInit(pos.X - _startPos.X, pos.Y - _startPos.Y);
                            break;

                        case DrawStep.DrawStepType.DrawEllipse:
                            var dx = pos.X - _startPos.X;
                            var dy = pos.Y - _startPos.Y;
                            ((DrawEllipseStep) _nowDrawing).ReInit(Math.Sqrt(dx*dx + dy*dy));
                            break;
                    }
            }
        }

        public void Finish()
        {
            _nowDrawing = null;
        }
    }
}