using System;

namespace DynamicVisualizer.Logic.Expressions
{
    public class ArrayExpression : Expression
    {
        private readonly string[] _exprsStrings;
        private readonly Value[] _values;
        public readonly ScalarExpression[] Exprs;

        public ArrayExpression(string objectName, string varName, string[] rawExprs) : base(objectName, varName)
        {
            _exprsStrings = new string[rawExprs.Length];
            Exprs = new ScalarExpression[rawExprs.Length];
            _values = new Value[rawExprs.Length];
            SetRawExpressions(rawExprs);
            CachedValue.SwitchTo(new Value(_values));
        }

        public override bool CanBeRemoved
        {
            get
            {
                for (var i = 0; i < Exprs.Length; ++i)
                    if (Exprs[i].UsedBy.Count != 0) return false;
                return UsedBy.Count == 0;
            }
        }

        public void SetRawExpressions(string[] rawExprs)
        {
            if (rawExprs.Length != _exprsStrings.Length) throw new ArgumentException("Arrays lengths must be equual!");
            for (var i = 0; i < rawExprs.Length; ++i)
                _exprsStrings[i] = rawExprs[i];
            ExprString = _exprsStrings[0];
            Recalculate();
        }

        public override void Recalculate()
        {
            for (var i = 0; i < Exprs.Length; ++i)
                if (Exprs[i] == null)
                {
                    var e = new ScalarExpression(ObjectName, VarName + (i + 1), _exprsStrings[i], i, this);
                    Exprs[i] = e;
                    _values[i] = e.CachedValue;
                }
                else
                {
                    Exprs[i].SetRawExpression(_exprsStrings[i]);
                }
        }
    }
}