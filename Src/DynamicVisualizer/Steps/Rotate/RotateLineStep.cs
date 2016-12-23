using System;
using System.Diagnostics;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Rotate
{
    public class RotateLineStep : RotateStep
    {
        public enum Side
        {
            Start,
            End
        }

        public readonly LineFigure LineFigure;
        public readonly Side RotateAround;
        public string Factor;
        public double HeightOrig;
        public double WidthOrig;
        public double XCachedDouble;
        public double YCachedDouble;

        private RotateLineStep(LineFigure figure, Side rotateAround)
        {
            Figure = figure;
            LineFigure = figure;
            RotateAround = rotateAround;
        }

        public RotateLineStep(LineFigure figure, Side rotateAround, string factor) : this(figure, rotateAround)
        {
            Rotate(factor);
        }

        public RotateLineStep(LineFigure figure, Side rotateAround, double factor)
            : this(figure, rotateAround, factor.Str())
        {
        }

        public override RotateStepType StepType => RotateStepType.RotateLine;

        public void SetDef()
        {
            Magnet aroundMagnet;
            switch (RotateAround)
            {
                case Side.Start:
                    aroundMagnet = LineFigure.Start;
                    break;
                default:
                    aroundMagnet = LineFigure.End;
                    break;
            }

            Def = string.Format("Rotate {0} around {1} by {2}", LineFigure.Name, aroundMagnet.Def, Factor);
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

            if (RotateAround == Side.Start)
            {
                LineFigure.X.SetRawExpression(XCachedDouble.Str());
                LineFigure.Y.SetRawExpression(YCachedDouble.Str());
                LineFigure.Width.SetRawExpression(WidthOrig.Str());
                LineFigure.Height.SetRawExpression(HeightOrig.Str());

                var se = new ScalarExpression("a", "a", Factor, true);
                var angle = se.CachedValue.Empty ? 0 : se.CachedValue.AsDouble * 2 * Math.PI;

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(LineFigure.Width,
                        "((cos(" + angle + ")) * " + LineFigure.Name + ".width) - ((sin(" + angle + ")) * " +
                        LineFigure.Name + ".height)"),
                    new Tuple<ScalarExpression, string>(LineFigure.Height,
                        "((sin(" + angle + ")) * " + LineFigure.Name + ".width) + ((cos(" + angle + ")) * " +
                        LineFigure.Name + ".height)"));
            }
            else if (RotateAround == Side.End)
            {
                Debug.WriteLine(
                    "X = {0}, Y = {1}, W = {2}, H = {3}", XCachedDouble.Str(), YCachedDouble.Str(), WidthOrig.Str(),
                    HeightOrig.Str());

                LineFigure.X.SetRawExpression(XCachedDouble.Str());
                LineFigure.Y.SetRawExpression(YCachedDouble.Str());
                LineFigure.Width.SetRawExpression(WidthOrig.Str());
                LineFigure.Height.SetRawExpression(HeightOrig.Str());

                var se = new ScalarExpression("a", "a", Factor, true);
                var angle = se.CachedValue.Empty ? 0 : se.CachedValue.AsDouble * 2 * Math.PI;

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(LineFigure.Width,
                        "((cos(" + angle + ")) * " + LineFigure.Name + ".width) - ((sin(" + angle + ")) * " +
                        LineFigure.Name + ".height)"),
                    new Tuple<ScalarExpression, string>(LineFigure.Height,
                        "((sin(" + angle + ")) * " + LineFigure.Name + ".width) + ((cos(" + angle + ")) * " +
                        LineFigure.Name + ".height)"),
                    new Tuple<ScalarExpression, string>(LineFigure.X,
                        "(((sin(" + angle + ")) * " + LineFigure.Name + ".height) - ((cos(" + angle + ")) * " +
                        LineFigure.Name + ".width)) + (" + LineFigure.Name + ".x + " + LineFigure.Name + ".width)"),
                    new Tuple<ScalarExpression, string>(LineFigure.Y,
                        "(((sin(" + angle + ")) * (-" + LineFigure.Name + ".width)) - ((cos(" + angle + ")) * " +
                        LineFigure.Name + ".height)) + (" + LineFigure.Name + ".y + " + LineFigure.Name +
                        ".height)"));
            }

            CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if (RotateAround == Side.Start)
            {
                LineFigure.Width.IndexInArray = CompletedIterations;
                LineFigure.X.IndexInArray = CompletedIterations;
                LineFigure.Height.IndexInArray = CompletedIterations;
                LineFigure.Y.IndexInArray = CompletedIterations;

                LineFigure.X.SetRawExpression(LineFigure.X.CachedValue.AsDouble.Str());
                LineFigure.Width.SetRawExpression(LineFigure.Width.CachedValue.AsDouble.Str());
                LineFigure.Y.SetRawExpression(LineFigure.Y.CachedValue.AsDouble.Str());
                LineFigure.Height.SetRawExpression(LineFigure.Height.CachedValue.AsDouble.Str());

                var se = new ScalarExpression("a", "a", Factor, true);
                var angle = se.CachedValue.Empty ? 0 : se.CachedValue.AsDouble * 2 * Math.PI;

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(LineFigure.Width,
                        "((cos(" + angle + ")) * " + LineFigure.Name + ".width) - ((sin(" + angle + ")) * " +
                        LineFigure.Name + ".height)"),
                    new Tuple<ScalarExpression, string>(LineFigure.Height,
                        "((sin(" + angle + ")) * " + LineFigure.Name + ".width) + ((cos(" + angle + ")) * " +
                        LineFigure.Name + ".height)"));
            }
            else if (RotateAround == Side.End)
            {
                LineFigure.Width.IndexInArray = CompletedIterations;
                LineFigure.X.IndexInArray = CompletedIterations;
                LineFigure.Height.IndexInArray = CompletedIterations;
                LineFigure.Y.IndexInArray = CompletedIterations;

                LineFigure.X.SetRawExpression(LineFigure.X.CachedValue.AsDouble.Str());
                LineFigure.Width.SetRawExpression(LineFigure.Width.CachedValue.AsDouble.Str());
                LineFigure.Y.SetRawExpression(LineFigure.Y.CachedValue.AsDouble.Str());
                LineFigure.Height.SetRawExpression(LineFigure.Height.CachedValue.AsDouble.Str());

                var se = new ScalarExpression("a", "a", Factor, true);
                var angle = se.CachedValue.Empty ? 0 : se.CachedValue.AsDouble * 2 * Math.PI;

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(LineFigure.Width,
                        "((cos(" + angle + ")) * " + LineFigure.Name + ".width) - ((sin(" + angle + ")) * " +
                        LineFigure.Name + ".height)"),
                    new Tuple<ScalarExpression, string>(LineFigure.Height,
                        "((sin(" + angle + ")) * " + LineFigure.Name + ".width) + ((cos(" + angle + ")) * " +
                        LineFigure.Name + ".height)"),
                    new Tuple<ScalarExpression, string>(LineFigure.X,
                        "(((sin(" + angle + ")) * " + LineFigure.Name + ".height) - ((cos(" + angle + ")) * " +
                        LineFigure.Name + ".width)) + (" + LineFigure.Name + ".x + " + LineFigure.Name + ".width)"),
                    new Tuple<ScalarExpression, string>(LineFigure.Y,
                        "(((sin(" + angle + ")) * (-" + LineFigure.Name + ".width)) - ((cos(" + angle + ")) * " +
                        LineFigure.Name + ".height)) + (" + LineFigure.Name + ".y + " + LineFigure.Name +
                        ".height)"));
            }
        }

        public override void CopyStaticFigure()
        {
            if (Iterations == -1 || Figure.IsGuide || Figure.StaticLoopFigures.Count - 1 < CompletedIterations)
            {
                return;
            }

            var rf = (LineFigure) Figure.StaticLoopFigures[CompletedIterations];
            if (RotateAround == Side.Start)
            {
                rf.Width.SetRawExpression(LineFigure.Width.CachedValue.Str);
                rf.X.SetRawExpression(LineFigure.X.CachedValue.Str);
                rf.Height.SetRawExpression(LineFigure.Height.CachedValue.Str);
                rf.Y.SetRawExpression(LineFigure.Y.CachedValue.Str);
            }
            else if (RotateAround == Side.End)
            {
                rf.Width.SetRawExpression(LineFigure.Width.CachedValue.Str);
                rf.X.SetRawExpression(LineFigure.X.CachedValue.Str);
                rf.Height.SetRawExpression(LineFigure.Height.CachedValue.Str);
                rf.Y.SetRawExpression(LineFigure.Y.CachedValue.Str);
            }
        }

        public void Rotate(string factor)
        {
            Factor = factor;
            SetDef();
            Apply();
        }

        public void Rotate(double factor)
        {
            Rotate(factor.Str());
        }
    }
}