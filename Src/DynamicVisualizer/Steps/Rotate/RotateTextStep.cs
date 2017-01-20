using System;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Rotate
{
    public class RotateTextStep : RotateStep
    {
        public enum Side
        {
            Start,
            End
        }

        public readonly Side RotateAround;

        public readonly TextFigure TextFigure;
        public string Factor;
        public double HeightOrig;
        public double WidthOrig;
        public double XCachedDouble;
        public double YCachedDouble;

        private RotateTextStep(TextFigure figure, Side rotateAround)
        {
            Figure = figure;
            TextFigure = figure;
            RotateAround = rotateAround;
        }

        public RotateTextStep(TextFigure figure, Side rotateAround, string factor) : this(figure, rotateAround)
        {
            Rotate(factor);
        }

        public RotateTextStep(TextFigure figure, Side rotateAround, double factor)
            : this(figure, rotateAround, factor.Str())
        {
        }

        public override RotateStepType StepType => RotateStepType.RotateText;

        public void SetDef()
        {
            Magnet aroundMagnet;
            switch (RotateAround)
            {
                case Side.Start:
                    aroundMagnet = TextFigure.Start;
                    break;
                default:
                    aroundMagnet = TextFigure.End;
                    break;
            }

            Def = string.Format("Rotate {0} around {1} by {2}", TextFigure.Name, aroundMagnet.Def, Factor);
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

            if (RotateAround == Side.Start)
            {
                TextFigure.X.SetRawExpression(XCachedDouble.Str());
                TextFigure.Y.SetRawExpression(YCachedDouble.Str());
                TextFigure.Width.SetRawExpression(WidthOrig.Str());
                TextFigure.Height.SetRawExpression(HeightOrig.Str());

                var se = new ScalarExpression("a", "a", Factor, true);
                var angle = se.CachedValue.Empty ? 0 : se.CachedValue.AsDouble * 2 * Math.PI;

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(TextFigure.Width,
                        "((cos(" + angle + ")) * " + TextFigure.Name + ".width) - ((sin(" + angle + ")) * " +
                        TextFigure.Name + ".height)"),
                    new Tuple<ScalarExpression, string>(TextFigure.Height,
                        "((sin(" + angle + ")) * " + TextFigure.Name + ".width) + ((cos(" + angle + ")) * " +
                        TextFigure.Name + ".height)"));
            }
            else if (RotateAround == Side.End)
            {
                TextFigure.X.SetRawExpression(XCachedDouble.Str());
                TextFigure.Y.SetRawExpression(YCachedDouble.Str());
                TextFigure.Width.SetRawExpression(WidthOrig.Str());
                TextFigure.Height.SetRawExpression(HeightOrig.Str());

                var se = new ScalarExpression("a", "a", Factor, true);
                var angle = se.CachedValue.Empty ? 0 : se.CachedValue.AsDouble * 2 * Math.PI;

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(TextFigure.Width,
                        "((cos(" + angle + ")) * " + TextFigure.Name + ".width) - ((sin(" + angle + ")) * " +
                        TextFigure.Name + ".height)"),
                    new Tuple<ScalarExpression, string>(TextFigure.Height,
                        "((sin(" + angle + ")) * " + TextFigure.Name + ".width) + ((cos(" + angle + ")) * " +
                        TextFigure.Name + ".height)"),
                    new Tuple<ScalarExpression, string>(TextFigure.X,
                        "(((sin(" + angle + ")) * " + TextFigure.Name + ".height) - ((cos(" + angle + ")) * " +
                        TextFigure.Name + ".width)) + (" + TextFigure.Name + ".x + " + TextFigure.Name + ".width)"),
                    new Tuple<ScalarExpression, string>(TextFigure.Y,
                        "(((sin(" + angle + ")) * (-" + TextFigure.Name + ".width)) - ((cos(" + angle + ")) * " +
                        TextFigure.Name + ".height)) + (" + TextFigure.Name + ".y + " + TextFigure.Name +
                        ".height)"));
            }

            CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if (RotateAround == Side.Start)
            {
                TextFigure.Width.IndexInArray = CompletedIterations;
                TextFigure.X.IndexInArray = CompletedIterations;
                TextFigure.Height.IndexInArray = CompletedIterations;
                TextFigure.Y.IndexInArray = CompletedIterations;

                DataStorage.CachedSwapToAbs(TextFigure.X, TextFigure.Width, TextFigure.Y, TextFigure.Height);

                var se = new ScalarExpression("a", "a", Factor, CompletedIterations, true);
                var angle = se.CachedValue.Empty ? 0 : se.CachedValue.AsDouble * 2 * Math.PI;

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(TextFigure.Width,
                        "((cos(" + angle + ")) * " + TextFigure.Name + ".width) - ((sin(" + angle + ")) * " +
                        TextFigure.Name + ".height)"),
                    new Tuple<ScalarExpression, string>(TextFigure.Height,
                        "((sin(" + angle + ")) * " + TextFigure.Name + ".width) + ((cos(" + angle + ")) * " +
                        TextFigure.Name + ".height)"));
            }
            else if (RotateAround == Side.End)
            {
                TextFigure.Width.IndexInArray = CompletedIterations;
                TextFigure.X.IndexInArray = CompletedIterations;
                TextFigure.Height.IndexInArray = CompletedIterations;
                TextFigure.Y.IndexInArray = CompletedIterations;

                DataStorage.CachedSwapToAbs(TextFigure.X, TextFigure.Width, TextFigure.Y, TextFigure.Height);

                var se = new ScalarExpression("a", "a", Factor, CompletedIterations, true);
                var angle = se.CachedValue.Empty ? 0 : se.CachedValue.AsDouble * 2 * Math.PI;

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(TextFigure.Width,
                        "((cos(" + angle + ")) * " + TextFigure.Name + ".width) - ((sin(" + angle + ")) * " +
                        TextFigure.Name + ".height)"),
                    new Tuple<ScalarExpression, string>(TextFigure.Height,
                        "((sin(" + angle + ")) * " + TextFigure.Name + ".width) + ((cos(" + angle + ")) * " +
                        TextFigure.Name + ".height)"),
                    new Tuple<ScalarExpression, string>(TextFigure.X,
                        "(((sin(" + angle + ")) * " + TextFigure.Name + ".height) - ((cos(" + angle + ")) * " +
                        TextFigure.Name + ".width)) + (" + TextFigure.Name + ".x + " + TextFigure.Name + ".width)"),
                    new Tuple<ScalarExpression, string>(TextFigure.Y,
                        "(((sin(" + angle + ")) * (-" + TextFigure.Name + ".width)) - ((cos(" + angle + ")) * " +
                        TextFigure.Name + ".height)) + (" + TextFigure.Name + ".y + " + TextFigure.Name +
                        ".height)"));
            }
        }

        public override void CopyStaticFigure()
        {
            if ((Iterations == -1) || Figure.IsGuide || (Figure.StaticLoopFigures.Count - 1 < CompletedIterations))
            {
                return;
            }

            var rf = (TextFigure) Figure.StaticLoopFigures[CompletedIterations];
            if (RotateAround == Side.Start)
            {
                rf.Width.SetRawExpression(TextFigure.Width.CachedValue.Str);
                rf.X.SetRawExpression(TextFigure.X.CachedValue.Str);
                rf.Height.SetRawExpression(TextFigure.Height.CachedValue.Str);
                rf.Y.SetRawExpression(TextFigure.Y.CachedValue.Str);
            }
            else if (RotateAround == Side.End)
            {
                rf.Width.SetRawExpression(TextFigure.Width.CachedValue.Str);
                rf.X.SetRawExpression(TextFigure.X.CachedValue.Str);
                rf.Height.SetRawExpression(TextFigure.Height.CachedValue.Str);
                rf.Y.SetRawExpression(TextFigure.Y.CachedValue.Str);
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