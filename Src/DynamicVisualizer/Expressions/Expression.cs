using System.Collections.Generic;

namespace DynamicVisualizer.Expressions
{
    public abstract class Expression
    {
        protected const int MaxRecursionDepth = 1000;
        public readonly Value CachedValue = new Value();
        public readonly List<Expression> DependentOn = new List<Expression>();
        public readonly string ObjectName;
        public readonly List<Expression> UsedBy = new List<Expression>();
        public readonly string VarName;
        public string ExprString;

        protected Expression(string objectName, string varName)
        {
            ObjectName = objectName;
            VarName = varName;
        }

        public bool Independent => DependentOn.Count == 0;

        public string FullName => ObjectName + "." + VarName;
        public abstract bool CanBeRemoved { get; }

        public abstract void Recalculate(int depth = 1);
    }
}