using System;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Resize
{
    public class ResizeRectStep : ResizeStep
    {
        public enum Side
        {
            Left,
            Right,
            Top,
            Bottom
        }

        public readonly RectFigure RectFigure;
        public readonly Side ResizeAround;
        public string Delta;
        public double HeightOrig;
        public double WidthOrig;
        public double XCachedDouble;
        public double YCachedDouble;

        private ResizeRectStep(RectFigure figure, Side resizeAround)
        {
            Figure = figure;
            RectFigure = figure;
            ResizeAround = resizeAround;
        }

        public ResizeRectStep(RectFigure figure, Side resizeAround, string value, string where = null)
            : this(figure, resizeAround)
        {
            Resize(value, where);
        }

        public ResizeRectStep(RectFigure figure, Side resizeAround, double value)
            : this(figure, resizeAround, value.Str())
        {
        }

        public override ResizeStepType StepType => ResizeStepType.ResizeRect;

        public void SetDef(string where)
        {
            var dimension = ResizeAround == Side.Left || ResizeAround == Side.Right
                ? "horizontally"
                : "vertically";
            Magnet dragMagnet;
            switch (ResizeAround)
            {
                case Side.Left:
                    dragMagnet = RectFigure.Right;
                    break;
                case Side.Right:
                    dragMagnet = RectFigure.Left;
                    break;
                case Side.Top:
                    dragMagnet = RectFigure.Bottom;
                    break;
                default:
                    dragMagnet = RectFigure.Top;
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
            XCachedDouble = RectFigure.X.CachedValue.AsDouble;
            YCachedDouble = RectFigure.Y.CachedValue.AsDouble;
            HeightOrig = RectFigure.Height.CachedValue.AsDouble;
            WidthOrig = RectFigure.Width.CachedValue.AsDouble;
        }

        public override void Apply()
        {
            if (!Applied)
            {
                CaptureBearings();
            }
            Applied = true;

            if (ResizeAround == Side.Left)
            {
                RectFigure.X.SetRawExpression(XCachedDouble.Str());
                RectFigure.Width.SetRawExpression(WidthOrig.Str());

                RectFigure.Width.SetRawExpression(RectFigure.Name + ".width + (" + Delta + ")");
            }
            else if (ResizeAround == Side.Top)
            {
                RectFigure.Y.SetRawExpression(YCachedDouble.Str());
                RectFigure.Height.SetRawExpression(HeightOrig.Str());

                RectFigure.Height.SetRawExpression(RectFigure.Name + ".height + (" + Delta + ")");
            }
            else if (ResizeAround == Side.Right)
            {
                RectFigure.X.SetRawExpression(XCachedDouble.Str());
                RectFigure.Width.SetRawExpression(WidthOrig.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(RectFigure.X, RectFigure.Name + ".x + (" + Delta + ")"),
                    new Tuple<ScalarExpression, string>(RectFigure.Width, RectFigure.Name + ".width - (" + Delta + ")"));
            }
            else if (ResizeAround == Side.Bottom)
            {
                RectFigure.Y.SetRawExpression(YCachedDouble.Str());
                RectFigure.Height.SetRawExpression(HeightOrig.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(RectFigure.Y, RectFigure.Name + ".y + (" + Delta + ")"),
                    new Tuple<ScalarExpression, string>(RectFigure.Height, RectFigure.Name + ".height - (" + Delta + ")"));
            }
            if (Iterations != -1 && !Figure.IsGuide)
            {
                CopyStaticFigure();
            }
        }

        public override void IterateNext()
        {
            if (ResizeAround == Side.Left)
            {
                RectFigure.Width.IndexInArray = CompletedIterations;
                RectFigure.X.IndexInArray = CompletedIterations;

                RectFigure.X.SetRawExpression(RectFigure.X.CachedValue.AsDouble.Str());
                RectFigure.Width.SetRawExpression(RectFigure.Width.CachedValue.AsDouble.Str());

                RectFigure.Width.SetRawExpression(RectFigure.Name + ".width + (" + Delta + ")");
            }
            else if (ResizeAround == Side.Top)
            {
                RectFigure.Height.IndexInArray = CompletedIterations;
                RectFigure.Y.IndexInArray = CompletedIterations;

                RectFigure.Y.SetRawExpression(RectFigure.Y.CachedValue.AsDouble.Str());
                RectFigure.Height.SetRawExpression(RectFigure.Height.CachedValue.AsDouble.Str());

                RectFigure.Height.SetRawExpression(RectFigure.Name + ".height + (" + Delta + ")");
            }
            else if (ResizeAround == Side.Right)
            {
                RectFigure.Width.IndexInArray = CompletedIterations;
                RectFigure.X.IndexInArray = CompletedIterations;

                RectFigure.X.SetRawExpression(RectFigure.X.CachedValue.AsDouble.Str());
                RectFigure.Width.SetRawExpression(RectFigure.Width.CachedValue.AsDouble.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(RectFigure.X, RectFigure.Name + ".x + (" + Delta + ")"),
                    new Tuple<ScalarExpression, string>(RectFigure.Width, RectFigure.Name + ".width - (" + Delta + ")"));
            }
            else if (ResizeAround == Side.Bottom)
            {
                RectFigure.Height.IndexInArray = CompletedIterations;
                RectFigure.Y.IndexInArray = CompletedIterations;

                RectFigure.Y.SetRawExpression(RectFigure.Y.CachedValue.AsDouble.Str());
                RectFigure.Height.SetRawExpression(RectFigure.Height.CachedValue.AsDouble.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(RectFigure.Y, RectFigure.Name + ".y + (" + Delta + ")"),
                    new Tuple<ScalarExpression, string>(RectFigure.Height, RectFigure.Name + ".height - (" + Delta + ")"));
            }
        }

        public override void CopyStaticFigure()
        {
            var rf = (RectFigure) Figure.StaticLoopFigures[CompletedIterations];
            if (ResizeAround == Side.Left)
            {
                rf.Width.SetRawExpression(RectFigure.Width.CachedValue.Str);
                rf.X.SetRawExpression(RectFigure.X.CachedValue.Str);
            }
            else if (ResizeAround == Side.Top)
            {
                rf.Height.SetRawExpression(RectFigure.Height.CachedValue.Str);
                rf.Y.SetRawExpression(RectFigure.Y.CachedValue.Str);
            }
            else if (ResizeAround == Side.Right)
            {
                rf.Width.SetRawExpression(RectFigure.Width.CachedValue.Str);
                rf.X.SetRawExpression(RectFigure.X.CachedValue.Str);
            }
            else if (ResizeAround == Side.Bottom)
            {
                rf.Height.SetRawExpression(RectFigure.Height.CachedValue.Str);
                rf.Y.SetRawExpression(RectFigure.Y.CachedValue.Str);
            }
        }

        public void Resize(string value, string where = null)
        {
            Delta = value;
            SetDef(where);
            Apply();
        }

        public void Resize(double value)
        {
            Resize(value.Str());
        }
    }
}