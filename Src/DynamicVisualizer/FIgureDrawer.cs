﻿using System;
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

        public bool NowDrawing => _nowDrawing != null;

        public void Start(Point pos)
        {
            _startPos = pos;
            var snapped = Timeline.Snap(_startPos);
            if (snapped != null)
            {
                _startPos = new Point(snapped.X.CachedValue.AsDouble, snapped.Y.CachedValue.AsDouble);
                switch (DrawStepType)
                {
                    case DrawStep.DrawStepType.DrawRect:
                        _nowDrawing = new DrawRectStep(snapped.X.ExprString, snapped.Y.ExprString, "0", "0");
                        break;
                    case DrawStep.DrawStepType.DrawCircle:
                        _nowDrawing = new DrawCircleStep(snapped.X.ExprString, snapped.Y.ExprString, "0");
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
                    case DrawStep.DrawStepType.DrawCircle:
                        _nowDrawing = new DrawCircleStep(_startPos.X, _startPos.Y, 0);
                        break;
                }
            }
            Timeline.Insert(_nowDrawing, Timeline.CurrentStepIndex == -1 ? 0 : Timeline.CurrentStepIndex + 1);
        }

        public void Move(Point pos)
        {
            if (_nowDrawing != null)
                switch (DrawStepType)
                {
                    case DrawStep.DrawStepType.DrawRect:
                        ((DrawRectStep) _nowDrawing).ReInit(pos.X - _startPos.X, pos.Y - _startPos.Y);
                        break;

                    case DrawStep.DrawStepType.DrawCircle:
                        var dx = pos.X - _startPos.X;
                        var dy = pos.Y - _startPos.Y;
                        ((DrawCircleStep) _nowDrawing).ReInit(Math.Sqrt(dx*dx + dy*dy));
                        break;
                }
        }

        public void Finish()
        {
            _nowDrawing = null;
        }
    }
}