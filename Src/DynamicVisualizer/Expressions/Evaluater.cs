using System;
using System.Collections.Generic;

namespace DynamicVisualizer.Expressions
{
    public static class Evaluater
    {
        private static int _currentIndexInArray;

        private static readonly Dictionary<char, Func<Value, Value, Value>> BinaryFunctions = new Dictionary
            <char, Func<Value, Value, Value>>
            {
                {
                    '+', (a, b) =>
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
                    '-', (a, b) =>
                    {
                        var op1 = a.IsDouble ? a.AsDouble : a.AsArray[_currentIndexInArray].AsDouble;
                        var op2 = b.IsDouble ? b.AsDouble : b.AsArray[_currentIndexInArray].AsDouble;
                        return new Value(op1 - op2);
                    }
                },
                {
                    '*', (a, b) =>
                    {
                        var op1 = a.IsDouble ? a.AsDouble : a.AsArray[_currentIndexInArray].AsDouble;
                        var op2 = b.IsDouble ? b.AsDouble : b.AsArray[_currentIndexInArray].AsDouble;
                        return new Value(op1 * op2);
                    }
                },
                {
                    '/', (a, b) =>
                    {
                        var op1 = a.IsDouble ? a.AsDouble : a.AsArray[_currentIndexInArray].AsDouble;
                        var op2 = b.IsDouble ? b.AsDouble : b.AsArray[_currentIndexInArray].AsDouble;
                        return new Value(op1 / op2);
                    }
                }
            };

        private static readonly Dictionary<char, int> Precedence = new Dictionary
            <char, int>
            {
                {
                    '+', 1
                },
                {
                    '-', 1
                },
                {
                    '*', 2
                },
                {
                    '/', 2
                }
            };

        private static readonly Dictionary<string, Func<Value, Value>> UnaryFunctions = new Dictionary
            <string, Func<Value, Value>>
            {
                {"sqrt", a => new Value(Math.Sqrt(a.AsDouble))},
                {"cos", a => new Value(Math.Cos(a.AsDouble))},
                {"sin", a => new Value(Math.Sin(a.AsDouble))},
                {"abs", a => new Value(Math.Abs(a.AsDouble))},
                {"len", a => new Value(a.AsArray.Length)},
                {"pos", a => new Value(_currentIndexInArray + 1)},
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
            //exprString = "(" + exprString + ")";
            _currentIndexInArray = indexInArray;

            // ReSharper disable once InconsistentNaming
            const int VALUE = 1;
            var values = new List<Value>();

            // ReSharper disable once InconsistentNaming
            const int FUNCTION = 2;
            var functions = new List<string>();

            // ReSharper disable once InconsistentNaming
            const int OPERATOR = 3;
            var operators = new List<char>();

            var outputQueue = new Queue<Tuple<int, int>>();
            var operatorStack = new Stack<Tuple<int, int>>();

            var firstInExpr = false;
            var afterOp = false;
            var afterLeftBrace = false;

            for (var i = 0; i < exprString.Length; ++i)
            {
                if (char.IsDigit(exprString[i])) // TOKEN: number
                {
                    firstInExpr = true;
                    afterOp = false;
                    afterLeftBrace = false;
                    var start1 = i;
                    while ((i < exprString.Length) &&
                           (char.IsDigit(exprString[i]) ||
                            (((exprString[i] == '.') || (exprString[i] == ',')) && (i + 1 < exprString.Length) &&
                             char.IsDigit(exprString[i + 1]))))
                    {
                        ++i;
                    }
                    i -= 1;
                    var d = double.Parse(exprString.Substring(start1, i - start1 + 1).Replace(".", ","));
                    values.Add(new Value(d));
                    outputQueue.Enqueue(new Tuple<int, int>(VALUE, values.Count - 1));
                }
                else if (char.IsLetter(exprString[i])) // TOKEN: function or variable
                {
                    firstInExpr = true;
                    afterOp = false;
                    afterLeftBrace = false;
                    var start = i;
                    while ((i < exprString.Length) &&
                           (char.IsLetter(exprString[i]) || char.IsDigit(exprString[i]) ||
                            ((exprString[i] == '.') && (i + 1 < exprString.Length) &&
                             (char.IsLetter(exprString[i + 1]) || char.IsDigit(exprString[i + 1])))))
                    {
                        ++i;
                    }
                    i -= 1;
                    var funcOrVar = exprString.Substring(start, i - start + 1);
                    if (UnaryFunctions.ContainsKey(funcOrVar))
                    {
                        // TOKEN: function
                        functions.Add(funcOrVar);
                        operatorStack.Push(new Tuple<int, int>(FUNCTION, functions.Count - 1));
                    }
                    else // TOKEN: variable
                    {
                        values.Add(variableEvaluater(funcOrVar));
                        outputQueue.Enqueue(new Tuple<int, int>(VALUE, values.Count - 1));
                    }
                }
                else if (exprString[i] == '"') // TOKEN: string
                {
                    firstInExpr = true;
                    afterOp = false;
                    afterLeftBrace = false;
                    var start = i;
                    i += 1;
                    while ((i < exprString.Length) &&
                           ((exprString[i] != '"') || ((i - 1 >= 0) && (exprString[i - 1] == '\\'))))
                    {
                        ++i;
                    }
                    values.Add(new Value(exprString.Substring(start + 1, i - start - 1).Replace("\\\"", "\"")));
                    outputQueue.Enqueue(new Tuple<int, int>(VALUE, values.Count - 1));
                }
                else if (BinaryFunctions.ContainsKey(exprString[i])) // TOKEN: one-char operator
                {
                    var o1 = exprString[i];
                    // if minus is unary then threat it as a function
                    if ((o1 == '-') && (!firstInExpr || afterOp || afterLeftBrace))
                    {
                        functions.Add("--");
                        operatorStack.Push(new Tuple<int, int>(FUNCTION, functions.Count - 1));
                    }
                    // minus is binary operator
                    else
                    {
                        while ((operatorStack.Count > 0) && (operatorStack.Peek().Item1 == OPERATOR) &&
                               (operators[operatorStack.Peek().Item2] != '(') &&
                               (Precedence[o1] <= Precedence[operators[operatorStack.Peek().Item2]]))
                        {
                            outputQueue.Enqueue(operatorStack.Pop());
                        }
                        operators.Add(o1);
                        operatorStack.Push(new Tuple<int, int>(OPERATOR, operators.Count - 1));
                    }
                    afterOp = true;
                }
                else if (exprString[i] == '(')
                {
                    afterLeftBrace = true;
                    operators.Add('(');
                    operatorStack.Push(new Tuple<int, int>(OPERATOR, operators.Count - 1));
                }
                else if (exprString[i] == ')')
                {
                    firstInExpr = true;
                    afterOp = false;
                    afterLeftBrace = false;
                    while ((operatorStack.Count > 0) &&
                           !((operatorStack.Peek().Item1 == OPERATOR) && (operators[operatorStack.Peek().Item2] == '(')))
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Pop();
                    if ((operatorStack.Count > 0) && (operatorStack.Peek().Item1 == FUNCTION))
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                }
            }

            while (operatorStack.Count > 0)
            {
                if ((operatorStack.Peek().Item1 == OPERATOR) && (operators[operatorStack.Peek().Item2] == '('))
                {
                    throw new Exception("Invalid expr!");
                }
                outputQueue.Enqueue(operatorStack.Pop());
            }

            var valStack = new Stack<Value>();

            while (outputQueue.Count > 0)
            {
                var token = outputQueue.Dequeue();
                if (token.Item1 == VALUE)
                {
                    valStack.Push(values[token.Item2]);
                }
                else if (token.Item1 == OPERATOR)
                {
                    if (valStack.Count < 2)
                    {
                        throw new Exception("Invalid expr!");
                    }
                    var v1 = valStack.Pop();
                    var v2 = valStack.Pop();
                    valStack.Push(BinaryFunctions[operators[token.Item2]](v2, v1));
                }
                // it is a function
                else
                {
                    if (valStack.Count < 1)
                    {
                        throw new Exception("Invalid expr!");
                    }
                    valStack.Push(UnaryFunctions[functions[token.Item2]](valStack.Pop()));
                }
            }
            if (valStack.Count > 1)
            {
                throw new Exception("Invalid expr!");
            }
            return valStack.Peek();
        }
    }
}