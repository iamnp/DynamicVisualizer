﻿using DynamicVisualizer.Expressions;

namespace DynamicVisualizer.Figures
{
    public class Magnet
    {
        public ScalarExpression X;
        public ScalarExpression Y;

        public Magnet(ScalarExpression x, ScalarExpression y)
        {
            X = x;
            Y = y;
        }

        public bool EqualExprStrings(Magnet a)
        {
            return (X.ExprString == a.X.ExprString) && (Y.ExprString == a.Y.ExprString);
        }
    }
}