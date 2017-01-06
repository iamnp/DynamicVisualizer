using System;
using System.Collections.Generic;

namespace DynamicVisualizer.Expressions
{
    public class ScalarExpression : Expression
    {
        public readonly ArrayExpression ParentArray;
        public bool AllowedToAddToParent;
        public int IndexInArray;
        public bool IsWeak;

        private ScalarExpression(string objectName, string varName, string rawExpr, int indexInArray,
            bool allowedToAddToParent, ArrayExpression parentArray, bool isWeak) : base(objectName, varName)
        {
            IndexInArray = indexInArray;
            IsWeak = isWeak;
            ParentArray = parentArray;
            AllowedToAddToParent = allowedToAddToParent;
            SetRawExpression(rawExpr);
        }

        public ScalarExpression(string objectName, string varName, string rawExpr)
            : this(objectName, varName, rawExpr, 0, false, null, false)
        {
        }

        public ScalarExpression(string objectName, string varName, string rawExpr, int indexInArray,
            bool allowedToAddToParent, ArrayExpression parentArray = null)
            : this(objectName, varName, rawExpr, indexInArray, allowedToAddToParent, parentArray, false)
        {
        }

        public ScalarExpression(string objectName, string varName, string rawExpr, bool isWeak)
            : this(objectName, varName, rawExpr, 0, false, null, isWeak)
        {
        }

        public ScalarExpression(string objectName, string varName, string rawExpr, int indexInArray, bool isWeak)
            : this(objectName, varName, rawExpr, indexInArray, false, null, isWeak)
        {
        }

        public override bool CanBeRemoved => (UsedBy.Count == 0) && (ParentArray == null);

        private Value VarEval(string s)
        {
            if (!s.Contains("."))
            {
                s = "data." + s;
            }
            var usedExpr = DataStorage.GetExpression(s);
            if (!IsWeak)
            {
                // used scalar expr
                if (usedExpr is ScalarExpression && (usedExpr != this))
                {
                    usedExpr.UsedBy.Add(this);
                    DependentOn.Add(usedExpr);
                }
                // used i-th element of array expr
                else if ((usedExpr != ParentArray) && AllowedToAddToParent)
                {
                    usedExpr.UsedBy.Add(ParentArray);
                    ParentArray.DependentOn.Add(usedExpr);
                }
                // used whole array expr
                else if (usedExpr != this)
                {
                    usedExpr.UsedBy.Add(this);
                    DependentOn.Add(usedExpr);
                }
            }
            return usedExpr.CachedValue;
        }

        public event EventHandler ValueChanged;

        public void SetRawExpression(string rawExpr)
        {
            ExprString = rawExpr;
            Recalculate();
        }

        public override void Recalculate()
        {
            // remove all the DependentOn values
            // e.g. remove the cross-references
            foreach (var e in DependentOn)
            {
                e.UsedBy.Remove(this);
            }
            DependentOn.Clear();

            try
            {
                var val = Evaluater.Evaluate(ExprString, VarEval, IndexInArray);
                CachedValue.SwitchTo(val.IsArray ? val.AsArray[IndexInArray] : val);
            }
            catch
            {
            }

            if (!IsWeak)
            {
                if (UsedBy.Count > 0)
                {
                    var copyOfUsedBy = new List<Expression>(UsedBy);
                    UsedBy.Clear();
                    foreach (var e in copyOfUsedBy)
                    {
                        e.Recalculate();
                    }
                }
                ParentArray?.NotifyElementChanged();
            }
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}