using System.Collections.Generic;

namespace DynamicVisualizer.Logic.Expressions
{
    public static class DataStorage
    {
        private static readonly Dictionary<string, Expression> _data = new Dictionary<string, Expression>();

        public static ScalarExpression Add(ScalarExpression expr)
        {
            _data[expr.FullName] = expr;
            return expr;
        }

        public static void AddArrayExpression(string objectName, string varName, string[] generator)
        {
            var e = new ArrayExpression(objectName, varName, generator);
            _data[e.FullName] = e;
        }

        public static void AddArrayExpression(string objectName, string varName, string generator, int length)
        {
            var arr = new string[length];
            for (var i = 0; i < length; ++i)
                arr[i] = generator;
            var e = new ArrayExpression(objectName, varName, arr);
            _data[objectName + "." + varName] = e;
        }

        public static ScalarExpression GetScalarExpression(string fullName)
        {
            return _data[fullName] as ScalarExpression;
        }

        public static ArrayExpression GetArrayExpression(string fullName)
        {
            return _data[fullName] as ArrayExpression;
        }

        public static Expression GetExpression(string fullName)
        {
            return _data[fullName];
        }

        public static void Remove(string fullName)
        {
            if (_data[fullName].CanBeRemoved) _data.Remove(fullName);
        }

        public static void Remove(Expression expr)
        {
            if (expr.CanBeRemoved) _data.Remove(expr.FullName);
        }

        public static void WipeNonDataFields()
        {
            var toDel = new List<string>();
            foreach (var pair in _data)
                if (!pair.Key.StartsWith("data.") && !pair.Key.StartsWith("canvas."))
                    toDel.Add(pair.Key);
            foreach (var s in toDel)
                _data.Remove(s);
        }

        public static string Dump()
        {
            var s = "";
            foreach (var pair in _data)
                s += pair.Key + " = " + pair.Value.ExprString + ": " + pair.Value.CachedValue.Str +
                     "\n";
            return s;
        }
    }
}