using System;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Scale
{
    public class ScaleTextStep : ScaleStep
    {
        public enum Side
        {
            Start,
            End
        }

        public readonly Side ScaleAround;

        public readonly TextFigure TextFigure;
        public string Factor;
        public double HeightOrig;
        public double WidthOrig;
        public double XCachedDouble;
        public double YCachedDouble;

        private ScaleTextStep(TextFigure figure, Side scaleAround)
        {
            Figure = figure;
            TextFigure = figure;
            ScaleAround = scaleAround;
        }

        public ScaleTextStep(TextFigure figure, Side scaleAround, string factor) : this(figure, scaleAround)
        {
            Scale(factor);
        }

        public ScaleTextStep(TextFigure figure, Side scaleAround, double factor)
            : this(figure, scaleAround, factor.Str())
        {
        }

        public override ScaleStepType StepType => ScaleStepType.ScaleText;

        public void SetDef()
        {
            Magnet aroundMagnet;
            switch (ScaleAround)
            {
                case Side.Start:
                    aroundMagnet = TextFigure.Start;
                    break;
                default:
                    aroundMagnet = TextFigure.End;
                    break;
            }

            Def = string.Format("Scale {0} around {1} by {2}", TextFigure.Name, aroundMagnet.Def, Factor);
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

            if (ScaleAround == Side.Start)
            {
                TextFigure.X.SetRawExpression(XCachedDouble.Str());
                TextFigure.Y.SetRawExpression(YCachedDouble.Str());
                TextFigure.Width.SetRawExpression(WidthOrig.Str());
                TextFigure.Height.SetRawExpression(HeightOrig.Str());

                TextFigure.Width.SetRawExpression(TextFigure.Name + ".width * (" + Factor + ")");
                TextFigure.Height.SetRawExpression(TextFigure.Name + ".height * (" + Factor + ")");
            }
            else if (ScaleAround == Side.End)
            {
                TextFigure.X.SetRawExpression(XCachedDouble.Str());
                TextFigure.Y.SetRawExpression(YCachedDouble.Str());
                TextFigure.Width.SetRawExpression(WidthOrig.Str());
                TextFigure.Height.SetRawExpression(HeightOrig.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(TextFigure.Width, TextFigure.Name + ".width * (" + Factor + ")"),
                    new Tuple<ScalarExpression, string>(TextFigure.X,
                        TextFigure.Name + ".x + (" + TextFigure.Name + ".width * (1.0 - (" + Factor + ")))"),
                    new Tuple<ScalarExpression, string>(TextFigure.Height,
                        TextFigure.Name + ".height * (" + Factor + ")"),
                    new Tuple<ScalarExpression, string>(TextFigure.Y,
                        TextFigure.Name + ".y + (" + TextFigure.Name + ".height * (1.0 - (" + Factor + ")))"));
            }

            CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if (ScaleAround == Side.Start)
            {
                TextFigure.Width.IndexInArray = CompletedIterations;
                TextFigure.X.IndexInArray = CompletedIterations;
                TextFigure.Height.IndexInArray = CompletedIterations;
                TextFigure.Y.IndexInArray = CompletedIterations;

                TextFigure.X.SetRawExpression(TextFigure.X.CachedValue.AsDouble.Str());
                TextFigure.Width.SetRawExpression(TextFigure.Width.CachedValue.AsDouble.Str());
                TextFigure.Y.SetRawExpression(TextFigure.Y.CachedValue.AsDouble.Str());
                TextFigure.Height.SetRawExpression(TextFigure.Height.CachedValue.AsDouble.Str());

                TextFigure.Width.SetRawExpression(TextFigure.Name + ".width * (" + Factor + ")");
                TextFigure.Height.SetRawExpression(TextFigure.Name + ".height * (" + Factor + ")");
            }
            else if (ScaleAround == Side.End)
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
                    new Tuple<ScalarExpression, string>(TextFigure.Width, TextFigure.Name + ".width * (" + Factor + ")"),
                    new Tuple<ScalarExpression, string>(TextFigure.X,
                        TextFigure.Name + ".x + (" + TextFigure.Name + ".width * (1.0 - (" + Factor + ")))"),
                    new Tuple<ScalarExpression, string>(TextFigure.Height,
                        TextFigure.Name + ".height * (" + Factor + ")"),
                    new Tuple<ScalarExpression, string>(TextFigure.Y,
                        TextFigure.Name + ".y + (" + TextFigure.Name + ".height * (1.0 - (" + Factor + ")))"));
            }
        }

        public override void CopyStaticFigure()
        {
            if ((Iterations == -1) || Figure.IsGuide || (Figure.StaticLoopFigures.Count - 1 < CompletedIterations))
            {
                return;
            }

            var lf = (TextFigure) Figure.StaticLoopFigures[CompletedIterations];
            if (ScaleAround == Side.Start)
            {
                lf.Width.SetRawExpression(TextFigure.Width.CachedValue.Str);
                lf.X.SetRawExpression(TextFigure.X.CachedValue.Str);
                lf.Height.SetRawExpression(TextFigure.Height.CachedValue.Str);
                lf.Y.SetRawExpression(TextFigure.Y.CachedValue.Str);
            }
            else if (ScaleAround == Side.End)
            {
                lf.Width.SetRawExpression(TextFigure.Width.CachedValue.Str);
                lf.X.SetRawExpression(TextFigure.X.CachedValue.Str);
                lf.Height.SetRawExpression(TextFigure.Height.CachedValue.Str);
                lf.Y.SetRawExpression(TextFigure.Y.CachedValue.Str);
            }
        }

        public void Scale(string factor)
        {
            Factor = factor;
            SetDef();
            Apply();
        }

        public void Scale(double factor)
        {
            Scale(factor.Str());
        }
    }
}