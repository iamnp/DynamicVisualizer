using System.Collections.Generic;

namespace DynamicVisualizer.Expressions
{
    public abstract class Expression
    {
        public readonly Value CachedValue = new Value();
        public readonly List<Expression> DependentOn = new List<Expression>();
        public readonly string ObjectName;
        public readonly List<ScalarExpression> UsedBy = new List<ScalarExpression>();
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

        public abstract void Recalculate();
    }
}