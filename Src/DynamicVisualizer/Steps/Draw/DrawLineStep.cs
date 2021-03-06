﻿using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps.Draw
{
    public class DrawLineStep : DrawStep
    {
        private static int _count = 1;
        public readonly LineFigure LineFigure;
        public string Height;
        public string Width;
        public string X;
        public string Y;

        private DrawLineStep()
        {
            Figure = new LineFigure("line" + _count++);
            LineFigure = (LineFigure) Figure;
        }

        public DrawLineStep(string x, string y, string width, string height, string startDef = null) : this()
        {
            X = x;
            Y = y;
            SetStartDef(startDef);
            ReInit(width, height);
        }

        public DrawLineStep(double x, double y, double width, double height)
            : this(x.Str(), y.Str(), width.Str(), height.Str())
        {
        }

        public override DrawStepType StepType => DrawStepType.DrawLine;

        public void SetStartDef(string point)
        {
            if (point == null)
            {
                StartDef = string.Format("Draw {0} from ({1}; {2})", LineFigure.Name, X, Y);
            }
            else
            {
                StartDef = string.Format("Draw {0} from {1}", LineFigure.Name, point);
            }
            Def = StartDef + EndDef;
        }

        public void SetEndDef(string point)
        {
            if (point == null)
            {
                EndDef = string.Format(", {0} width, {1} height", Width, Height);
            }
            else
            {
                EndDef = string.Format(" to {0}", point);
            }
            Def = StartDef + EndDef;
        }

        public void ReInit(string width, string height, string endDef = null)
        {
            Width = width;
            Height = height;
            SetEndDef(endDef);
            Apply();
        }

        public void ReInitWidth(string width)
        {
            Width = width;
            SetEndDef(null);
            Apply();
        }

        public void ReInitHeight(string height)
        {
            Height = height;
            SetEndDef(null);
            Apply();
        }

        public void ReInitX(string x)
        {
            X = x;
            SetStartDef(null);
            Apply();
        }

        public void ReInitY(string y)
        {
            Y = y;
            SetStartDef(null);
            Apply();
        }

        public void ReInit(double width, double height)
        {
            ReInit(width.Str(), height.Str());
        }

        public override void Apply()
        {
            if (!Applied)
            {
                StepManager.Figures.Add(LineFigure);
            }

            Figure.FigureColor.SetIndex(0);

            if ((LineFigure.X == null) || !Applied)
            {
                LineFigure.X = DataStorage.Add(new ScalarExpression(Figure.Name, "x", X, Figure.IsGuide));
            }
            else
            {
                LineFigure.X.SetRawExpression(X);
            }

            if ((LineFigure.Y == null) || !Applied)
            {
                LineFigure.Y = DataStorage.Add(new ScalarExpression(Figure.Name, "y", Y, Figure.IsGuide));
            }
            else
            {
                LineFigure.Y.SetRawExpression(Y);
            }

            if ((LineFigure.Width == null) || !Applied)
            {
                LineFigure.Width = DataStorage.Add(new ScalarExpression(Figure.Name, "width", Width, Figure.IsGuide));
            }
            else
            {
                LineFigure.Width.SetRawExpression(Width);
            }

            if ((LineFigure.Height == null) || !Applied)
            {
                LineFigure.Height =
                    DataStorage.Add(new ScalarExpression(Figure.Name, "height", Height, Figure.IsGuide));
            }
            else
            {
                LineFigure.Height.SetRawExpression(Height);
            }

            Applied = true;

            CopyStaticFigure();
        }

        public override void IterateNext()
        {
            LineFigure.X.IndexInArray = CompletedIterations;
            LineFigure.X.SetRawExpression(X);

            LineFigure.Y.IndexInArray = CompletedIterations;
            LineFigure.Y.SetRawExpression(Y);

            LineFigure.Width.IndexInArray = CompletedIterations;
            LineFigure.Width.SetRawExpression(Width);

            LineFigure.Height.IndexInArray = CompletedIterations;
            LineFigure.Height.SetRawExpression(Height);

            Figure.FigureColor.SetIndex(CompletedIterations);
        }

        public override void CopyStaticFigure()
        {
            if ((Iterations == -1) || Figure.IsGuide)
            {
                return;
            }

            var lf = new LineFigure
            {
                X = new ScalarExpression("a", "a", LineFigure.X.CachedValue.Str),
                Y = new ScalarExpression("a", "a", LineFigure.Y.CachedValue.Str),
                Width = new ScalarExpression("a", "a", LineFigure.Width.CachedValue.Str),
                Height = new ScalarExpression("a", "a", LineFigure.Height.CachedValue.Str),
                FigureColor = LineFigure.FigureColor.Copy()
            };
            Figure.StaticLoopFigures.Add(lf);
            StepManager.Figures.Add(lf);
        }
    }
}