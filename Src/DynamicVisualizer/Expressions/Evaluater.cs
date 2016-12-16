using System;
using System.Collections.Generic;

namespace DynamicVisualizer.Expressions
{
    public static class Evaluater
    {
        private static int _currentIndexInArray;

        private static readonly Dictionary<string, Func<Value, Value, Value>> BinaryFunctions = new Dictionary
            <string, Func<Value, Value, Value>>
            {
                {
                    "+", (a, b) =>
                    {
                        if (a.IsString && b.IsString)
                        {
                            return new Value(a.AsString + b.AsString);
                        }
                        if (a.IsString)
                        {
                            var op2 = b.IsDouble ? b : b.AsArray[_currentIndexInArray];
                            if (op2.IsString)
                            {
                                return new Value(a.AsString + op2.AsString);
                            }
                            return new Value(a.AsString + op2.AsDouble);
                        }
                        if (b.IsString)
                        {
                            var op1 = a.IsDouble ? a : a.AsArray[_currentIndexInArray];
                            if (op1.IsString)
                            {
                                return new Value(op1.AsString + b.AsString);
                            }
                            return new Value(op1.AsDouble + b.AsString);
                        }
                        var op11 = a.IsDouble ? a.AsDouble : a.AsArray[_currentIndexInArray].AsDouble;
                        var op22 = b.IsDouble ? b.AsDouble : b.AsArray[_currentIndexInArray].AsDouble;
                        return new Value(op11 + op22);
                    }
                },
                {
                    "-", (a, b) =>
                    {
                        var op1 = a.IsDouble ? a.AsDouble : a.AsArray[_currentIndexInArray].AsDouble;
                        var op2 = b.IsDouble ? b.AsDouble : b.AsArray[_currentIndexInArray].AsDouble;
                        return new Value(op1 - op2);
                    }
                },
                {
                    "*", (a, b) =>
                    {
                        var op1 = a.IsDouble ? a.AsDouble : a.AsArray[_currentIndexInArray].AsDouble;
                        var op2 = b.IsDouble ? b.AsDouble : b.AsArray[_currentIndexInArray].AsDouble;
                        return new Value(op1 * op2);
                    }
                },
                {
                    "/", (a, b) =>
                    {
                        var op1 = a.IsDouble ? a.AsDouble : a.AsArray[_currentIndexInArray].AsDouble;
                        var op2 = b.IsDouble ? b.AsDouble : b.AsArray[_currentIndexInArray].AsDouble;
                        return new Value(op1 / op2);
                    }
                }
            };

        private static readonly Dictionary<string, Func<Value, Value>> UnaryFunctions = new Dictionary
            <string, Func<Value, Value>>
            {
                {"sqrt", a => new Value(Math.Sqrt(a.AsDouble))},
                {"len", a => new Value(a.AsArray.Length)},
                {
                    "mean", a =>
                    {
                        double sum = 0;
                        var arr = a.AsArray;
                        for (var j = 0; j < arr.Length; ++j)
                        {
                            sum += arr[j].AsDouble;
                        }
                        return new Value(sum / arr.Length);
                    }
                },
                {
                    "max", a =>
                    {
                        var arr = a.AsArray;
                        var max = arr[0].AsDouble;
                        for (var j = 1; j < arr.Length; ++j)
                        {
                            if (arr[j].AsDouble > max)
                            {
                                max = arr[j].AsDouble;
                            }
                        }
                        return new Value(max);
                    }
                },
                {"--", a => new Value(-a.AsDouble)}
            };

        public static Value Evaluate(string exprString, Func<string, Value> variableEvaluater, int indexInArray)
        {
            exprString = "(" + exprString + ")";
            _currentIndexInArray = indexInArray;

            var operands = new Stack<Value>();
            var operators = new Stack<string>();
            var needToEval = new Stack<bool>();

            var firstInExpr = false;
            var afterOp = false;
            var afterLeftBrace = false;

            for (var i = 0; i < exprString.Length; ++i)
            {
                if (exprString[i] == '(')
                {
                    afterLeftBrace = true;
                    needToEval.Push(false);
                }
                if (exprString[i] == ' ' || exprString[i] == '(')
                {
                    continue;
                }
                if (char.IsDigit(exprString[i])) // encountered a number
                {
                    firstInExpr = true;
                    afterOp = false;
                    afterLeftBrace = false;
                    var start1 = i;
                    while (i < exprString.Length &&
                           (char.IsDigit(exprString[i]) || exprString[i] == '.' || exprString[i] == ','))
                    {
                        ++i;
                    }
                    i -= 1;
                    operands.Push(
                        new Value(double.Parse(exprString.Substring(start1, i - start1 + 1).Replace(".", ","))));
                    continue;
                }
                if (exprString[i] == ')') // evaluate part
                {
                    firstInExpr = true;
                    afterOp = false;
                    afterLeftBrace = false;
                    if (!needToEval.Pop())
                    {
                        continue;
                    }
                    if (operators.Count == 0)
                    {
                        continue;
                    }
                    var function = operators.Pop();
                    if (BinaryFunctions.ContainsKey(function))
                    {
                        var operand2 = operands.Pop();
                        var operand1 = operands.Pop();
                        operands.Push(BinaryFunctions[function](operand1, operand2));
                    }
                    else if (UnaryFunctions.ContainsKey(function))
                    {
                        var operand = operands.Pop();
                        operands.Push(UnaryFunctions[function](operand));
                    }
                    continue;
                }

                if (char.IsLetter(exprString[i])) // encountered variable or func
                {
                    firstInExpr = true;
                    afterOp = false;
                    afterLeftBrace = false;
                    var start = i;
                    while (i < exprString.Length &&
                           (char.IsLetter(exprString[i]) || char.IsDigit(exprString[i]) || exprString[i] == '.'))
                    {
                        ++i;
                    }
                    i -= 1;
                    var funcOrVar = exprString.Substring(start, i - start + 1);
                    if (UnaryFunctions.ContainsKey(funcOrVar))
                    {
                        // func
                        operators.Push(funcOrVar);
                        needToEval.Pop();
                        needToEval.Push(true);
                    }
                    else // variable
                    {
                        operands.Push(variableEvaluater(funcOrVar));
                    }
                    continue;
                }

                if (exprString[i] == '"') // encountered string literal
                {
                    firstInExpr = true;
                    afterOp = false;
                    afterLeftBrace = false;
                    var start = i;
                    i += 1;
                    while (i < exprString.Length && exprString[i] != '"')
                    {
                        ++i;
                    }
                    var funcOrVar = exprString.Substring(start, i - start + 1);
                    operands.Push(new Value(funcOrVar.Replace("\"", "")));
                    continue;
                }

                // ecnountered one-char operator
                if (BinaryFunctions.ContainsKey(exprString[i].ToString()))
                {
                    needToEval.Pop();
                    needToEval.Push(true);
                    if (exprString[i] == '-') // unary or binary
                    {
                        if (!firstInExpr || afterOp || afterLeftBrace)
                        {
                            operators.Push("--");
                            continue;
                        }
                    }
                    operators.Push(exprString[i].ToString());
                    afterOp = true;
                }
            }
            return operands.Pop();
        }
    }
}