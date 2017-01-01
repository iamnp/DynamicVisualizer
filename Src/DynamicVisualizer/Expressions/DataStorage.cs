using System;
using System.Collections.Generic;

namespace DynamicVisualizer.Expressions
{
    public static class DataStorage
    {
        private static readonly Dictionary<string, Expression> Data = new Dictionary<string, Expression>();

        public static void SimultaneousSwap(params Tuple<ScalarExpression, string>[] swaps)
        {
            for (var i = 0; i < swaps.Length; ++i)
            {
                Add(new ScalarExpression(swaps[i].Item1.ObjectName, swaps[i].Item1.VarName,
                    swaps[i].Item1.CachedValue.AsDouble.Str(), true));
            }

            for (var i = 0; i < swaps.Length; ++i)
            {
                swaps[i].Item1.SetRawExpression(swaps[i].Item2);
            }

            for (var i = 0; i < swaps.Length; ++i)
            {
                Add(swaps[i].Item1);
            }
        }

        public static ScalarExpression Add(ScalarExpression expr)
        {
            Data[expr.FullName] = expr;
            return expr;
        }

        public static ArrayExpression Add(ArrayExpression expr)
        {
            Data[expr.FullName] = expr;
            return expr;
        }

        public static ScalarExpression GetScalarExpression(string fullName)
        {
            return Data[fullName] as ScalarExpression;
        }

        public static ArrayExpression GetArrayExpression(string fullName)
        {
            return Data[fullName] as ArrayExpression;
        }

        public static Expression GetExpression(string fullName)
        {
            return Data[fullName];
        }

        public static void Remove(string fullName)
        {
            if (Data[fullName].CanBeRemoved)
            {
                Data.Remove(fullName);
            }
        }

        public static void Remove(Expression expr)
        {
            if (expr.CanBeRemoved)
            {
                Data.Remove(expr.FullName);
            }
        }

        public static void WipeNonDataFields()
        {
            var toDel = new List<string>();
            foreach (var pair in Data)
            {
                if (!pair.Key.StartsWith("data.") && !pair.Key.StartsWith("canvas."))
                {
                    toDel.Add(pair.Key);
                }
            }
            foreach (var s in toDel)
            {
                Data.Remove(s);
            }
        }

        public static string Dump()
        {
            var s = "";
            foreach (var pair in Data)
            {
                s += pair.Key + " = " + pair.Value.ExprString + ": " + pair.Value.CachedValue.Str +
                     "\n";
            }
            return s;
        }
    }
}