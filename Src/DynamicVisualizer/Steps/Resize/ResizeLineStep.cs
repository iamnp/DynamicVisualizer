using System;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Resize
{
    public class ResizeLineStep : ResizeStep
    {
        public enum Side
        {
            Start,
            End
        }

        public readonly LineFigure LineFigure;
        public readonly Side ResizeAround;
        public string DeltaX;
        public string DeltaY;
        public double HeightOrig;
        public double WidthOrig;
        public double XCachedDouble;
        public double YCachedDouble;

        private ResizeLineStep(LineFigure figure, Side resizeAround)
        {
            Figure = figure;
            LineFigure = figure;
            ResizeAround = resizeAround;
        }

        public ResizeLineStep(LineFigure figure, Side resizeAround, string deltaX, string deltaY, string where = null)
            : this(figure, resizeAround)
        {
            Resize(deltaX, deltaY, where);
        }

        public ResizeLineStep(LineFigure figure, Side resizeAround, double deltaX, double deltaY)
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
                    dragMagnet = LineFigure.End;
                    break;
                default:
                    dragMagnet = LineFigure.Start;
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
            XCachedDouble = LineFigure.X.CachedValue.AsDouble;
            YCachedDouble = LineFigure.Y.CachedValue.AsDouble;
            HeightOrig = LineFigure.Height.CachedValue.AsDouble;
            WidthOrig = LineFigure.Width.CachedValue.AsDouble;
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
                LineFigure.X.SetRawExpression(XCachedDouble.Str());
                LineFigure.Y.SetRawExpression(YCachedDouble.Str());
                LineFigure.Width.SetRawExpression(WidthOrig.Str());
                LineFigure.Height.SetRawExpression(HeightOrig.Str());

                LineFigure.Width.SetRawExpression(LineFigure.Name + ".width + (" + DeltaX + ")");
                LineFigure.Height.SetRawExpression(LineFigure.Name + ".height + (" + DeltaY + ")");
            }
            else if (ResizeAround == Side.End)
            {
                LineFigure.X.SetRawExpression(XCachedDouble.Str());
                LineFigure.Y.SetRawExpression(YCachedDouble.Str());
                LineFigure.Width.SetRawExpression(WidthOrig.Str());
                LineFigure.Height.SetRawExpression(HeightOrig.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(LineFigure.Width, LineFigure.Name + ".width - (" + DeltaX + ")"),
                    new Tuple<ScalarExpression, string>(LineFigure.X, LineFigure.Name + ".x + (" + DeltaX + ")"),
                    new Tuple<ScalarExpression, string>(LineFigure.Height,
                        LineFigure.Name + ".height - (" + DeltaY + ")"),
                    new Tuple<ScalarExpression, string>(LineFigure.Y, LineFigure.Name + ".y + (" + DeltaY + ")"));
            }

            CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if (ResizeAround == Side.Start)
            {
                LineFigure.Width.IndexInArray = CompletedIterations;
                LineFigure.X.IndexInArray = CompletedIterations;
                LineFigure.Height.IndexInArray = CompletedIterations;
                LineFigure.Y.IndexInArray = CompletedIterations;

                LineFigure.X.SetRawExpression(LineFigure.X.CachedValue.AsDouble.Str());
                LineFigure.Width.SetRawExpression(LineFigure.Width.CachedValue.AsDouble.Str());
                LineFigure.Y.SetRawExpression(LineFigure.Y.CachedValue.AsDouble.Str());
                LineFigure.Height.SetRawExpression(LineFigure.Height.CachedValue.AsDouble.Str());

                LineFigure.Width.SetRawExpression(LineFigure.Name + ".width + (" + DeltaX + ")");
                LineFigure.Height.SetRawExpression(LineFigure.Name + ".height + (" + DeltaY + ")");
            }
            else if (ResizeAround == Side.End)
            {
                LineFigure.Width.IndexInArray = CompletedIterations;
                LineFigure.X.IndexInArray = CompletedIterations;
                LineFigure.Height.IndexInArray = CompletedIterations;
                LineFigure.Y.IndexInArray = CompletedIterations;

                LineFigure.X.SetRawExpression(LineFigure.X.CachedValue.AsDouble.Str());
                LineFigure.Width.SetRawExpression(LineFigure.Width.CachedValue.AsDouble.Str());
                LineFigure.Y.SetRawExpression(LineFigure.Y.CachedValue.AsDouble.Str());
                LineFigure.Height.SetRawExpression(LineFigure.Height.CachedValue.AsDouble.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(LineFigure.Width, LineFigure.Name + ".width - (" + DeltaX + ")"),
                    new Tuple<ScalarExpression, string>(LineFigure.X, LineFigure.Name + ".x + (" + DeltaX + ")"),
                    new Tuple<ScalarExpression, string>(LineFigure.Height,
                        LineFigure.Name + ".height - (" + DeltaY + ")"),
                    new Tuple<ScalarExpression, string>(LineFigure.Y, LineFigure.Name + ".y + (" + DeltaY + ")"));
            }
        }

        public override void CopyStaticFigure()
        {
            if ((Iterations == -1) || Figure.IsGuide || (Figure.StaticLoopFigures.Count - 1 < CompletedIterations))
            {
                return;
            }

            var lf = (LineFigure) Figure.StaticLoopFigures[CompletedIterations];
            if (ResizeAround == Side.Start)
            {
                lf.Width.SetRawExpression(LineFigure.Width.CachedValue.Str);
                lf.X.SetRawExpression(LineFigure.X.CachedValue.Str);
                lf.Height.SetRawExpression(LineFigure.Height.CachedValue.Str);
                lf.Y.SetRawExpression(LineFigure.Y.CachedValue.Str);
            }
            else if (ResizeAround == Side.End)
            {
                lf.Width.SetRawExpression(LineFigure.Width.CachedValue.Str);
                lf.X.SetRawExpression(LineFigure.X.CachedValue.Str);
                lf.Height.SetRawExpression(LineFigure.Height.CachedValue.Str);
                lf.Y.SetRawExpression(LineFigure.Y.CachedValue.Str);
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