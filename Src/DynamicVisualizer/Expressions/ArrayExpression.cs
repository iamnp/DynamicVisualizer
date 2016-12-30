namespace DynamicVisualizer.Expressions
{
    public class ArrayExpression : Expression
    {
        private string[] _exprsStrings;
        private Value[] _values;
        public ScalarExpression[] Exprs;

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

        public void SetRawExpressions(string[] rawExprs)
        {
            _exprsStrings = new string[rawExprs.Length];
            Exprs = new ScalarExpression[rawExprs.Length];
            _values = new Value[rawExprs.Length];
            for (var i = 0; i < rawExprs.Length; ++i)
            {
                _exprsStrings[i] = rawExprs[i];
            }
            ExprString = _exprsStrings[0];
            CachedValue.SwitchTo(new Value(_values));
            Recalculate();
        }

        public void SetRawExpression(string rawExpr)
        {
            for (var i = 0; i < _exprsStrings.Length; ++i)
            {
                _exprsStrings[i] = rawExpr;
            }
            ExprString = _exprsStrings[0];
            Recalculate();
        }

        public string ExprStrings()
        {
            var s = "";
            for (var i = 0; i < _exprsStrings.Length; ++i)
            {
                if (i != _exprsStrings.Length - 1)
                {
                    s += _exprsStrings[i] + "; ";
                }
                else
                {
                    s += _exprsStrings[i];
                }
            }
            return s;
        }

        public override void Recalculate()
        {
            for (var i = 0; i < Exprs.Length; ++i)
            {
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
}