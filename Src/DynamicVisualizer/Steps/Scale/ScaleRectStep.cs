﻿using System;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Scale
{
    public class ScaleRectStep : ScaleStep
    {
        public enum Side
        {
            Left,
            Right,
            Top,
            Bottom
        }

        public readonly RectFigure RectFigure;
        public readonly Side ScaleAround;
        public string Factor;
        public double HeightOrig;
        public double WidthOrig;
        public double XCachedDouble;
        public double YCachedDouble;

        private ScaleRectStep(RectFigure figure, Side scaleAround)
        {
            Figure = figure;
            RectFigure = figure;
            ScaleAround = scaleAround;
        }

        public ScaleRectStep(RectFigure figure, Side scaleAround, string factor) : this(figure, scaleAround)
        {
            Scale(factor);
        }

        public ScaleRectStep(RectFigure figure, Side scaleAround, double factor)
            : this(figure, scaleAround, factor.Str())
        {
        }

        public override ScaleStepType StepType => ScaleStepType.ScaleRect;

        public void SetDef()
        {
            var dimension = (ScaleAround == Side.Left) || (ScaleAround == Side.Right) ? "width" : "height";
            Magnet aroundMagnet;
            switch (ScaleAround)
            {
                case Side.Left:
                    aroundMagnet = RectFigure.Left;
                    break;
                case Side.Right:
                    aroundMagnet = RectFigure.Right;
                    break;
                case Side.Top:
                    aroundMagnet = RectFigure.Top;
                    break;
                default:
                    aroundMagnet = RectFigure.Bottom;
                    break;
            }

            Def = string.Format("Scale {0}'s {1} around {2} by {3}", RectFigure.Name, dimension, aroundMagnet.Def,
                Factor);
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

            if (ScaleAround == Side.Left)
            {
                RectFigure.X.SetRawExpression(XCachedDouble.Str());
                RectFigure.Width.SetRawExpression(WidthOrig.Str());

                RectFigure.Width.SetRawExpression(RectFigure.Name + ".width * (" + Factor + ")");
            }
            else if (ScaleAround == Side.Top)
            {
                RectFigure.Y.SetRawExpression(YCachedDouble.Str());
                RectFigure.Height.SetRawExpression(HeightOrig.Str());

                RectFigure.Height.SetRawExpression(RectFigure.Name + ".height * (" + Factor + ")");
            }
            else if (ScaleAround == Side.Right)
            {
                RectFigure.X.SetRawExpression(XCachedDouble.Str());
                RectFigure.Width.SetRawExpression(WidthOrig.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(RectFigure.Width,
                        RectFigure.Name + ".width * (" + Factor + ")"),
                    new Tuple<ScalarExpression, string>(RectFigure.X,
                        RectFigure.Name + ".x + (" + RectFigure.Name + ".width * (1.0 - (" + Factor + ")))"));
            }
            else if (ScaleAround == Side.Bottom)
            {
                RectFigure.Y.SetRawExpression(YCachedDouble.Str());
                RectFigure.Height.SetRawExpression(HeightOrig.Str());

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(RectFigure.Height,
                        RectFigure.Name + ".height * (" + Factor + ")"),
                    new Tuple<ScalarExpression, string>(RectFigure.Y,
                        RectFigure.Name + ".y + (" + RectFigure.Name + ".height * (1.0 - (" + Factor + ")))"));
            }

            CopyStaticFigure();
        }

        public override void IterateNext()
        {
            if (ScaleAround == Side.Left)
            {
                RectFigure.Width.IndexInArray = CompletedIterations;
                RectFigure.X.IndexInArray = CompletedIterations;

                DataStorage.CachedSwapToAbs(RectFigure.X, RectFigure.Width);

                RectFigure.Width.SetRawExpression(RectFigure.Name + ".width * (" + Factor + ")");
            }
            else if (ScaleAround == Side.Top)
            {
                RectFigure.Height.IndexInArray = CompletedIterations;
                RectFigure.Y.IndexInArray = CompletedIterations;

                DataStorage.CachedSwapToAbs(RectFigure.Y, RectFigure.Height);

                RectFigure.Height.SetRawExpression(RectFigure.Name + ".height * (" + Factor + ")");
            }
            else if (ScaleAround == Side.Right)
            {
                RectFigure.Width.IndexInArray = CompletedIterations;
                RectFigure.X.IndexInArray = CompletedIterations;

                DataStorage.CachedSwapToAbs(RectFigure.X, RectFigure.Width);

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(RectFigure.Width,
                        RectFigure.Name + ".width * (" + Factor + ")"),
                    new Tuple<ScalarExpression, string>(RectFigure.X,
                        RectFigure.Name + ".x + (" + RectFigure.Name + ".width * (1.0 - (" + Factor + ")))"));
            }
            else if (ScaleAround == Side.Bottom)
            {
                RectFigure.Height.IndexInArray = CompletedIterations;
                RectFigure.Y.IndexInArray = CompletedIterations;

                DataStorage.CachedSwapToAbs(RectFigure.Y, RectFigure.Height);

                DataStorage.SimultaneousSwap(
                    new Tuple<ScalarExpression, string>(RectFigure.Height,
                        RectFigure.Name + ".height * (" + Factor + ")"),
                    new Tuple<ScalarExpression, string>(RectFigure.Y,
                        RectFigure.Name + ".y + (" + RectFigure.Name + ".height * (1.0 - (" + Factor + ")))"));
            }
        }

        public override void CopyStaticFigure()
        {
            if ((Iterations == -1) || Figure.IsGuide || (Figure.StaticLoopFigures.Count - 1 < CompletedIterations))
            {
                return;
            }

            var rf = (RectFigure) Figure.StaticLoopFigures[CompletedIterations];
            if (ScaleAround == Side.Left)
            {
                rf.Width.SetRawExpression(RectFigure.Width.CachedValue.Str);
                rf.X.SetRawExpression(RectFigure.X.CachedValue.Str);
            }
            else if (ScaleAround == Side.Top)
            {
                rf.Height.SetRawExpression(RectFigure.Height.CachedValue.Str);
                rf.Y.SetRawExpression(RectFigure.Y.CachedValue.Str);
            }
            else if (ScaleAround == Side.Right)
            {
                rf.Width.SetRawExpression(RectFigure.Width.CachedValue.Str);
                rf.X.SetRawExpression(RectFigure.X.CachedValue.Str);
            }
            else if (ScaleAround == Side.Bottom)
            {
                rf.Height.SetRawExpression(RectFigure.Height.CachedValue.Str);
                rf.Y.SetRawExpression(RectFigure.Y.CachedValue.Str);
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