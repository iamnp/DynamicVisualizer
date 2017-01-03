using System;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Resize
{
    public class ResizeTextStep : ResizeStep
    {
        public enum Side
        {
            Start,
            End
        }

        public readonly Side ResizeAround;

        public readonly TextFigure TextFigure;
        public string DeltaX;
        public string DeltaY;
        public double HeightOrig;
        public double WidthOrig;
        public double XCachedDouble;
        public double YCachedDouble;

        private ResizeTextStep(TextFigure figure, Side resizeAround)
        {
            Figure = figure;
            TextFigure = figure;
            ResizeAround = resizeAround;
        }

        public ResizeTextStep(TextFigure figure, Side resizeAround, string deltaX, string deltaY, string where = null)
            : this(figure, resizeAround)
        {
            Resize(deltaX, deltaY, where);
        }

        public ResizeTextStep(TextFigure figure, Side resizeAround, double deltaX, double deltaY)
            : this(figure, resizeAround, deltaX.Str(), deltaY.Str())
        {
        }

        public override ResizeStepType StepType => ResizeStepType.ResizeLine;

        public void SetDef(string where)
        {
            Magnet dragMagnet;
            switch (ResizeAround)
            {
                case Side.Start:
                    dragMagnet = TextFigure.End;
                    break;
                default:
                    dragMagnet = TextFigure.Start;
                    break;
            }

            if (where == null)
            {
                Def = string.Format("Move {0}, {1} horizontally, {2} vertically", dragMagnet.Def, DeltaX, DeltaY);
            }
            else
            {
                Def = string.Format("Move {0} to {1}", dragMagnet.Def, where);
            }
        }

        private void CaptureBearings()
        {
            XCachedDouble = TextFigure.X.CachedValue.AsDouble;
            YCachedDouble = TextFigure.Y.CachedValue.AsDouble;
            HeightOrig = TextFigure.Height.CachedValue.AsDouble;
            WidthOrig = TextFigure.Width.CachedValue.AsDouble;
        }

        public override void Apply()
        {
            if (!Applied)
            {
                CaptureBearings();
            }
            Applied = true;

            if (ResizeAround == Side.Start)
            {
                TextFigure.X.SetRawExpression(XCachedDouble.Str());
                TextFigure.Y.SetRawExpression(YCachedDouble.Str());
                TextFigure.Width.SetRawExpression(WidthOrig.Str());
                TextFigure.Height.SetRawExpression(HeightOrig.Str());

                TextFigure.Width.SetRawExpression(TextFigure.Name + ".width + (" + DeltaX + ")");
                TextFigure.Height.SetRawExpression(TextFigure.Name + ".height + (" + DeltaY + ")");
            }
            else if (ResizeAround == Side.End)
            {
                TextFigure.X.SetRawExpression(XCachedDouble.Str());
                TextFigure.Y.SetRawExpression(YCachedDouble.Str());
                TextFigure.Width.SetRawExpression(WidthOrig.Str());
                TextFigure.Height.SetRawExpression(HeightOrig.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(TextFigure.Width, TextFigure.Name + ".width - (" + DeltaX + ")"),
                    new Tuple<ScalarExpression, string>(TextFigure.X, TextFigure.Name + ".x + (" + DeltaX + ")"),
                    new Tuple<ScalarExpression, string>(TextFigure.Height,
                        TextFigure.Name + ".height - (" + DeltaY + ")"),
                    new Tuple<ScalarExpression, string>(TextFigure.Y, TextFigure.Name + ".y + (" + DeltaY + ")"));
            }

            CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if (ResizeAround == Side.Start)
            {
                TextFigure.Width.IndexInArray = CompletedIterations;
                TextFigure.X.IndexInArray = CompletedIterations;
                TextFigure.Height.IndexInArray = CompletedIterations;
                TextFigure.Y.IndexInArray = CompletedIterations;

                TextFigure.X.SetRawExpression(TextFigure.X.CachedValue.AsDouble.Str());
                TextFigure.Width.SetRawExpression(TextFigure.Width.CachedValue.AsDouble.Str());
                TextFigure.Y.SetRawExpression(TextFigure.Y.CachedValue.AsDouble.Str());
                TextFigure.Height.SetRawExpression(TextFigure.Height.CachedValue.AsDouble.Str());

                TextFigure.Width.SetRawExpression(TextFigure.Name + ".width + (" + DeltaX + ")");
                TextFigure.Height.SetRawExpression(TextFigure.Name + ".height + (" + DeltaY + ")");
            }
            else if (ResizeAround == Side.End)
            {
                TextFigure.Width.IndexInArray = CompletedIterations;
                TextFigure.X.IndexInArray = CompletedIterations;
                TextFigure.Height.IndexInArray = CompletedIterations;
                TextFigure.Y.IndexInArray = CompletedIterations;

                TextFigure.X.SetRawExpression(TextFigure.X.CachedValue.AsDouble.Str());
                TextFigure.Width.SetRawExpression(TextFigure.Width.CachedValue.AsDouble.Str());
                TextFigure.Y.SetRawExpression(TextFigure.Y.CachedValue.AsDouble.Str());
                TextFigure.Height.SetRawExpression(TextFigure.Height.CachedValue.AsDouble.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(TextFigure.Width, TextFigure.Name + ".width - (" + DeltaX + ")"),
                    new Tuple<ScalarExpression, string>(TextFigure.X, TextFigure.Name + ".x + (" + DeltaX + ")"),
                    new Tuple<ScalarExpression, string>(TextFigure.Height,
                        TextFigure.Name + ".height - (" + DeltaY + ")"),
                    new Tuple<ScalarExpression, string>(TextFigure.Y, TextFigure.Name + ".y + (" + DeltaY + ")"));
            }
        }

        public override void CopyStaticFigure()
        {
            if ((Iterations == -1) || Figure.IsGuide || (Figure.StaticLoopFigures.Count - 1 < CompletedIterations))
            {
                return;
            }

            var lf = (TextFigure) Figure.StaticLoopFigures[CompletedIterations];
            if (ResizeAround == Side.Start)
            {
                lf.Width.SetRawExpression(TextFigure.Width.CachedValue.Str);
                lf.X.SetRawExpression(TextFigure.X.CachedValue.Str);
                lf.Height.SetRawExpression(TextFigure.Height.CachedValue.Str);
                lf.Y.SetRawExpression(TextFigure.Y.CachedValue.Str);
            }
            else if (ResizeAround == Side.End)
            {
                lf.Width.SetRawExpression(TextFigure.Width.CachedValue.Str);
                lf.X.SetRawExpression(TextFigure.X.CachedValue.Str);
                lf.Height.SetRawExpression(TextFigure.Height.CachedValue.Str);
                lf.Y.SetRawExpression(TextFigure.Y.CachedValue.Str);
            }
        }

        public void Resize(string deltaX, string deltaY, string where = null)
        {
            DeltaX = deltaX;
            DeltaY = deltaY;
            SetDef(where);
            Apply();
        }

        public void Resize(double deltaX, double deltaY)
        {
            Resize(deltaX.Str(), deltaY.Str());
        }

        public void ResizeX(string deltaX)
        {
            DeltaX = deltaX;
            SetDef(null);
            Apply();
        }

        public void ResizeY(string deltaY)
        {
            DeltaY = deltaY;
            SetDef(null);
            Apply();
        }
    }
}