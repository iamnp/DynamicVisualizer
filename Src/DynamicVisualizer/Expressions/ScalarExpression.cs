using System;
using System.Collections.Generic;

namespace DynamicVisualizer.Expressions
{
    public class ScalarExpression : Expression
    {
        private readonly ArrayExpression _parentArray;
        private readonly Func<string, Value> _varEvaluater;
        public int IndexInArray;
        public bool IsWeak;

        private ScalarExpression(string objectName, string varName, string rawExpr, int indexInArray,
            ArrayExpression parentArray, bool isWeak) : base(objectName, varName)
        {
            _varEvaluater = s =>
            {
                if (!s.Contains("."))
                {
                    s = ObjectName + "." + s;
                }
                var usedExpr = DataStorage.GetExpression(s);
                if (!IsWeak && (usedExpr != this))
                {
                    usedExpr.UsedBy.Add(this);
                    DependentOn.Add(usedExpr);
                }
                return usedExpr.CachedValue;
            };
            IndexInArray = indexInArray;
            IsWeak = isWeak;
            _parentArray = parentArray;
            SetRawExpression(rawExpr);
        }

        public ScalarExpression(string objectName, string varName, string rawExpr)
            : this(objectName, varName, rawExpr, 0, null, false)
        {
        }

        public ScalarExpression(string objectName, string varName, string rawExpr, int indexInArray,
            ArrayExpression parentArray = null)
            : this(objectName, varName, rawExpr, indexInArray, parentArray, false)
        {
        }

        public ScalarExpression(string objectName, string varName, string rawExpr, bool isWeak)
            : this(objectName, varName, rawExpr, 0, null, isWeak)
        {
        }

        public ScalarExpression(string objectName, string varName, string rawExpr, int indexInArray, bool isWeak)
            : this(objectName, varName, rawExpr, indexInArray, null, isWeak)
        {
        }

        public override bool CanBeRemoved => (UsedBy.Count == 0) && (_parentArray == null);
        public event EventHandler ValueChanged;

        public void SetRawExpression(string rawExpr)
        {
            ExprString = rawExpr;
            Recalculate();
        }

        public void NotifyDependantArrays()
        {
            if (!IsWeak)
            {
                if (_parentArray != null)
                {
                    if (_parentArray.UsedBy.Count > 0)
                    {
                        _parentArray.UsedBy[0].NotifyDependantArrays();
                    }
                    _parentArray.ChildChanged();
                }
            }
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
                var val = Evaluater.Evaluate(ExprString, _varEvaluater, IndexInArray);
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
                if (_parentArray != null)
                {
                    var copyOfParentUsedBy = new List<Expression>(_parentArray.UsedBy);
                    foreach (var e in copyOfParentUsedBy)
                    {
                        e.Recalculate();
                    }
                }
            }
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}