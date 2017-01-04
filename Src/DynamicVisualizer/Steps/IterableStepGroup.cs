using System;
using DynamicVisualizer.Expressions;

namespace DynamicVisualizer.Steps
{
    public class IterableStepGroup
    {
        public readonly ScalarExpression IterationsExpr;
        private int _iterations;
        public int Length;
        public int StartIndex;

        public IterableStepGroup(string expr)
        {
            IterationsExpr = new ScalarExpression("a", "a", expr);
            IterationsExpr.ValueChanged += IterationsExprOnValueChanged;
            Iterations = (int) IterationsExpr.CachedValue.AsDouble;
        }

        public int EndIndex => StartIndex + Length - 1;

        public int Iterations
        {
            get { return _iterations; }
            private set
            {
                _iterations = value;
                for (var i = StartIndex; i < StartIndex + Length; ++i)
                {
                    StepManager.Steps[i].Iterations = _iterations - 1;
                }
            }
        }

        private void IterationsExprOnValueChanged(object sender, EventArgs eventArgs)
        {
            Iterations = (int) IterationsExpr.CachedValue.AsDouble;
        }
    }
}