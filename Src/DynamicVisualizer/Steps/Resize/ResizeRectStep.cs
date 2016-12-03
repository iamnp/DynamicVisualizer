using System;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Resize
{
    public class ResizeRectStep : TransformStep
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
        public string HeightExpr;
        public double HeightOrig;
        public string WidthExpr;
        public double WidthOrig;
        public double XCachedDouble;
        public string XExpr;
        public double YCachedDouble;
        public string YExpr;

        private ResizeRectStep(RectFigure figure, Side resizeAround)
        {
            ResizeAround = resizeAround;
            Figure = figure;
            RectFigure = figure;
            WidthExpr = RectFigure.Width.ExprString;
            HeightExpr = RectFigure.Height.ExprString;
            XCachedDouble = RectFigure.X.CachedValue.AsDouble;
            YCachedDouble = RectFigure.Y.CachedValue.AsDouble;
            HeightOrig = RectFigure.Height.CachedValue.AsDouble;
            WidthOrig = RectFigure.Width.CachedValue.AsDouble;
            XExpr = RectFigure.X.ExprString;
            YExpr = RectFigure.Y.ExprString;
        }

        public ResizeRectStep(RectFigure figure, Side resizeAround, double value) : this(figure, resizeAround)
        {
            Resize(value);
        }

        public ResizeRectStep(RectFigure figure, Side resizeAround, string value) : this(figure, resizeAround)
        {
            Resize(value);
        }

        public override TransformStepType StepType => TransformStepType.ResizeRect;

        public override void Apply()
        {
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
            if ((Iterations != -1) && !Figure.IsGuide) CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if (ResizeAround == Side.Left)
            {
                RectFigure.Width.IndexInArray = CompletedIterations;
                RectFigure.X.IndexInArray = CompletedIterations;

                RectFigure.X.SetRawExpression(XCachedDouble.Str());
                RectFigure.Width.SetRawExpression(WidthOrig.Str());

                RectFigure.Width.SetRawExpression(RectFigure.Name + ".width + (" + Delta + ")");
            }
            else if (ResizeAround == Side.Top)
            {
                RectFigure.Height.IndexInArray = CompletedIterations;
                RectFigure.Y.IndexInArray = CompletedIterations;

                RectFigure.Y.SetRawExpression(YCachedDouble.Str());
                RectFigure.Height.SetRawExpression(HeightOrig.Str());

                RectFigure.Height.SetRawExpression(RectFigure.Name + ".height + (" + Delta + ")");
            }
            else if (ResizeAround == Side.Right)
            {
                RectFigure.Width.IndexInArray = CompletedIterations;
                RectFigure.X.IndexInArray = CompletedIterations;

                RectFigure.X.SetRawExpression(XCachedDouble.Str());
                RectFigure.Width.SetRawExpression(WidthOrig.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(RectFigure.X, RectFigure.Name + ".x + (" + Delta + ")"),
                    new Tuple<ScalarExpression, string>(RectFigure.Width, RectFigure.Name + ".width - (" + Delta + ")"));
            }
            else if (ResizeAround == Side.Bottom)
            {
                RectFigure.Height.IndexInArray = CompletedIterations;
                RectFigure.Y.IndexInArray = CompletedIterations;

                RectFigure.Y.SetRawExpression(YCachedDouble.Str());
                RectFigure.Height.SetRawExpression(HeightOrig.Str());

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

        public void Resize(string value)
        {
            Delta = value;
            Apply();
        }

        public void Resize(double value)
        {
            Delta = value.Str();
            Apply();
        }
    }
}