﻿using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Resize
{
    public class ResizeEllipseStep : ResizeStep
    {
        public enum Side
        {
            Left,
            Right,
            Top,
            Bottom
        }

        public readonly EllipseFigure EllipseFigure;
        public readonly Side ResizeAround;
        public string Delta;
        public double Radius1Orig;
        public double Radius2Orig;
        public double XCachedDouble;
        public double YCachedDouble;

        private ResizeEllipseStep(EllipseFigure figure, Side resizeAround)
        {
            Figure = figure;
            EllipseFigure = figure;
            ResizeAround = resizeAround;
        }

        public ResizeEllipseStep(EllipseFigure figure, Side resizeAround, string delta, string where = null)
            : this(figure, resizeAround)
        {
            Resize(delta, where);
        }

        public ResizeEllipseStep(EllipseFigure figure, Side resizeAround, double delta)
            : this(figure, resizeAround, delta.Str())
        {
        }

        public override ResizeStepType StepType => ResizeStepType.ResizeEllipse;

        public void SetDef(string where)
        {
            var dimension = (ResizeAround == Side.Left) || (ResizeAround == Side.Right)
                ? "horizontally"
                : "vertically";
            Magnet dragMagnet;
            switch (ResizeAround)
            {
                case Side.Left:
                    dragMagnet = EllipseFigure.Right;
                    break;
                case Side.Right:
                    dragMagnet = EllipseFigure.Left;
                    break;
                case Side.Top:
                    dragMagnet = EllipseFigure.Bottom;
                    break;
                default:
                    dragMagnet = EllipseFigure.Top;
                    break;
            }

            if (where == null)
            {
                Def = string.Format("Move {0}, {1} {2}", dragMagnet.Def, Delta, dimension);
            }
            else
            {
                Def = string.Format("Move {0} to {1}", dragMagnet.Def, where);
            }
        }

        private void CaptureBearings()
        {
            Radius1Orig = EllipseFigure.Radius1.CachedValue.AsDouble;
            Radius2Orig = EllipseFigure.Radius2.CachedValue.AsDouble;
            XCachedDouble = EllipseFigure.X.CachedValue.AsDouble;
            YCachedDouble = EllipseFigure.Y.CachedValue.AsDouble;
        }

        public override void Apply()
        {
            if (!Applied)
            {
                CaptureBearings();
            }
            Applied = true;

            if ((ResizeAround == Side.Left) || (ResizeAround == Side.Right))
            {
                EllipseFigure.X.SetRawExpression(XCachedDouble.Str());
                EllipseFigure.Radius1.SetRawExpression(Radius1Orig.Str());

                EllipseFigure.Radius1.SetRawExpression(EllipseFigure.Name + ".radius1 + (" + Delta + ")");
            }
            else if ((ResizeAround == Side.Top) || (ResizeAround == Side.Bottom))
            {
                EllipseFigure.Y.SetRawExpression(YCachedDouble.Str());
                EllipseFigure.Radius2.SetRawExpression(Radius2Orig.Str());

                EllipseFigure.Radius2.SetRawExpression(EllipseFigure.Name + ".radius2 - (" + Delta + ")");
            }

            CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if ((ResizeAround == Side.Left) || (ResizeAround == Side.Right))
            {
                EllipseFigure.Radius1.IndexInArray = CompletedIterations;

                DataStorage.CachedSwapToAbs(EllipseFigure.X, EllipseFigure.Radius1);

                EllipseFigure.Radius1.SetRawExpression(EllipseFigure.Name + ".radius1 + (" + Delta + ")");
            }
            else if ((ResizeAround == Side.Top) || (ResizeAround == Side.Bottom))
            {
                EllipseFigure.Radius2.IndexInArray = CompletedIterations;

                DataStorage.CachedSwapToAbs(EllipseFigure.Y, EllipseFigure.Radius2);

                EllipseFigure.Radius2.SetRawExpression(EllipseFigure.Name + ".radius2 + (" + Delta + ")");
            }
        }

        public override void CopyStaticFigure()
        {
            if ((Iterations == -1) || Figure.IsGuide || (Figure.StaticLoopFigures.Count - 1 < CompletedIterations))
            {
                return;
            }

            var rf = (EllipseFigure) Figure.StaticLoopFigures[CompletedIterations];
            if ((ResizeAround == Side.Left) || (ResizeAround == Side.Right))
            {
                rf.Radius1.SetRawExpression(EllipseFigure.Radius1.CachedValue.Str);
            }
            else if ((ResizeAround == Side.Top) || (ResizeAround == Side.Bottom))
            {
                rf.Radius2.SetRawExpression(EllipseFigure.Radius2.CachedValue.Str);
            }
        }

        public void Resize(string delta, string where = null)
        {
            Delta = delta;
            SetDef(where);
            Apply();
        }

        public void Resize(double delta)
        {
            Resize(delta.Str());
        }
    }
}