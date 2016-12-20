using System;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Scale
{
    public class ScaleLineStep : ScaleStep
    {
        public enum Side
        {
            Start,
            End
        }

        public readonly LineFigure LineFigure;
        public readonly Side ScaleAround;
        public string Factor;
        public double HeightOrig;
        public double WidthOrig;
        public double XCachedDouble;
        public double YCachedDouble;

        private ScaleLineStep(LineFigure figure, Side scaleAround)
        {
            Figure = figure;
            LineFigure = figure;
            ScaleAround = scaleAround;
        }

        public ScaleLineStep(LineFigure figure, Side scaleAround, double factor) : this(figure, scaleAround)
        {
            Scale(factor);
        }

        public ScaleLineStep(LineFigure figure, Side scaleAround, string factor) : this(figure, scaleAround)
        {
            Scale(factor);
        }

        public override ScaleStepType StepType => ScaleStepType.ScaleLine;

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

            if (ScaleAround == Side.Start)
            {
                LineFigure.X.SetRawExpression(XCachedDouble.Str());
                LineFigure.Y.SetRawExpression(YCachedDouble.Str());
                LineFigure.Width.SetRawExpression(WidthOrig.Str());
                LineFigure.Height.SetRawExpression(HeightOrig.Str());

                LineFigure.Width.SetRawExpression(LineFigure.Name + ".width * (" + Factor + ")");
                LineFigure.Height.SetRawExpression(LineFigure.Name + ".height * (" + Factor + ")");
            }
            else if (ScaleAround == Side.End)
            {
                LineFigure.X.SetRawExpression(XCachedDouble.Str());
                LineFigure.Y.SetRawExpression(YCachedDouble.Str());
                LineFigure.Width.SetRawExpression(WidthOrig.Str());
                LineFigure.Height.SetRawExpression(HeightOrig.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(LineFigure.Width, LineFigure.Name + ".width * (" + Factor + ")"),
                    new Tuple<ScalarExpression, string>(LineFigure.X,
                        LineFigure.Name + ".x + (" + LineFigure.Name + ".width * (1.0 - (" + Factor + ")))"),
                    new Tuple<ScalarExpression, string>(LineFigure.Height,
                        LineFigure.Name + ".height * (" + Factor + ")"),
                    new Tuple<ScalarExpression, string>(LineFigure.Y,
                        LineFigure.Name + ".y + (" + LineFigure.Name + ".height * (1.0 - (" + Factor + ")))"));
            }
            if (Iterations != -1 && !Figure.IsGuide)
            {
                CopyStaticFigure();
            }
        }

        public override void IterateNext()
        {
            if (ScaleAround == Side.Start)
            {
                LineFigure.Width.IndexInArray = CompletedIterations;
                LineFigure.X.IndexInArray = CompletedIterations;
                LineFigure.Height.IndexInArray = CompletedIterations;
                LineFigure.Y.IndexInArray = CompletedIterations;

                LineFigure.X.SetRawExpression(LineFigure.X.CachedValue.AsDouble.Str());
                LineFigure.Width.SetRawExpression(LineFigure.Width.CachedValue.AsDouble.Str());
                LineFigure.Y.SetRawExpression(LineFigure.Y.CachedValue.AsDouble.Str());
                LineFigure.Height.SetRawExpression(LineFigure.Height.CachedValue.AsDouble.Str());

                LineFigure.Width.SetRawExpression(LineFigure.Name + ".width * (" + Factor + ")");
                LineFigure.Height.SetRawExpression(LineFigure.Name + ".height * (" + Factor + ")");
            }
            else if (ScaleAround == Side.End)
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
                    new Tuple<ScalarExpression, string>(LineFigure.Width, LineFigure.Name + ".width * (" + Factor + ")"),
                    new Tuple<ScalarExpression, string>(LineFigure.X,
                        LineFigure.Name + ".x + (" + LineFigure.Name + ".width * (1.0 - (" + Factor + ")))"),
                    new Tuple<ScalarExpression, string>(LineFigure.Height,
                        LineFigure.Name + ".height * (" + Factor + ")"),
                    new Tuple<ScalarExpression, string>(LineFigure.Y,
                        LineFigure.Name + ".y + (" + LineFigure.Name + ".height * (1.0 - (" + Factor + ")))"));
            }
        }

        public override void CopyStaticFigure()
        {
            var rf = (LineFigure) Figure.StaticLoopFigures[CompletedIterations];
            if (ScaleAround == Side.Start)
            {
                rf.Width.SetRawExpression(LineFigure.Width.CachedValue.Str);
                rf.X.SetRawExpression(LineFigure.X.CachedValue.Str);
                rf.Height.SetRawExpression(LineFigure.Height.CachedValue.Str);
                rf.Y.SetRawExpression(LineFigure.Y.CachedValue.Str);
            }
            else if (ScaleAround == Side.End)
            {
                rf.Width.SetRawExpression(LineFigure.Width.CachedValue.Str);
                rf.X.SetRawExpression(LineFigure.X.CachedValue.Str);
                rf.Height.SetRawExpression(LineFigure.Height.CachedValue.Str);
                rf.Y.SetRawExpression(LineFigure.Y.CachedValue.Str);
            }
        }

        public void Scale(string factor)
        {
            Factor = factor;
            Apply();
        }

        public void Scale(double factor)
        {
            Factor = factor.Str();
            Apply();
        }
    }
}