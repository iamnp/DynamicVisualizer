using System;
using System.Collections.Generic;

namespace DynamicVisualizer.Expressions
{
    public class ArrayExpression : Expression
    {
        private bool _ignoreNotifyElementChanged;
        private Value[] _values;
        public ScalarExpression[] Exprs;
        public string[] ExprsStrings;

        public ArrayExpression(string objectName, string varName, string[] rawExprs) : base(objectName, varName)
        {
            SetRawExpressions(rawExprs);
        }

        public ArrayExpression(string objectName, string varName, string rawExpr, int n) : base(objectName, varName)
        {
            var rawExprs = new string[n];
            for (var i = 0; i < n; ++i)
            {
                rawExprs[i] = rawExpr;
            }

            SetRawExpressions(rawExprs);
        }

        public override bool CanBeRemoved
        {
            get
            {
                for (var i = 0; i < Exprs.Length; ++i)
                {
                    if (Exprs[i].UsedBy.Count != 0)
                    {
                        return false;
                    }
                }
                return UsedBy.Count == 0;
            }
        }

        public event EventHandler ValueChanged;

        public void SetRawExpressions(string[] rawExprs)
        {
            ExprsStrings = new string[rawExprs.Length];
            if (Exprs != null)
            {
                for (var i = 0; i < Exprs.Length; ++i)
                {
                    Exprs[i]?.Dealloc();
                }
            }
            Exprs = new ScalarExpression[rawExprs.Length];
            _values = new Value[rawExprs.Length];
            for (var i = 0; i < rawExprs.Length; ++i)
            {
                ExprsStrings[i] = rawExprs[i];
            }
            ExprString = ExprsStrings[0];
            CachedValue.SwitchTo(new Value(_values));
            Recalculate();
        }

        public void SetRawExpression(string rawExpr, int len)
        {
            ExprsStrings = new string[len];
            if (Exprs != null)
            {
                for (var i = 0; i < Exprs.Length; ++i)
                {
                    Exprs[i]?.Dealloc();
                }
            }
            Exprs = new ScalarExpression[len];
            _values = new Value[len];
            for (var i = 0; i < ExprsStrings.Length; ++i)
            {
                ExprsStrings[i] = rawExpr;
            }
            ExprString = ExprsStrings[0];
            CachedValue.SwitchTo(new Value(_values));
            Recalculate();
        }

        public string ExprStrings()
        {
            var s = "";
            for (var i = 0; i < ExprsStrings.Length; ++i)
            {
                if (i != ExprsStrings.Length - 1)
                {
                    s += ExprsStrings[i] + "; ";
                }
                else
                {
                    s += ExprsStrings[i];
                }
            }
            return s;
        }

        public void ChildChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public override void Recalculate(int depth = 1)
        {
            foreach (var e in DependentOn)
            {
                e.UsedBy.Remove(this);
            }
            DependentOn.Clear();

            _ignoreNotifyElementChanged = true;
            for (var i = 0; i < Exprs.Length; ++i)
            {
                if (Exprs[i] == null)
                {
                    var e = new ScalarExpression(ObjectName, VarName + (i + 1), ExprsStrings[i], i, i == 0, this);
                    Exprs[i] = e;
                    _values[i] = e.CachedValue;
                }
                else
                {
                    Exprs[i].AllowedToAddToParent = i == 0;
                    Exprs[i].SetRawExpression(ExprsStrings[i]);
                }
                Exprs[i].AllowedToAddToParent = false;
            }
            _ignoreNotifyElementChanged = false;

            NotifyElementChanged();
        }

        public void NotifyElementChanged()
        {
            if (_ignoreNotifyElementChanged)
            {
                return;
            }
            if (UsedBy.Count > 0)
            {
                var copyOfUsedBy = new List<Expression>(UsedBy);
                UsedBy.Clear();
                foreach (var e in copyOfUsedBy)
                {
                    var ae = e as ArrayExpression;
                    ae?.SetRawExpression(ae.ExprString, Exprs.Length);
                    e.Recalculate();
                }
            }

            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}