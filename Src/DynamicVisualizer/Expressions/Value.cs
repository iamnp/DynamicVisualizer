namespace DynamicVisualizer.Expressions
{
    public class Value
    {
        private double? _double;

        public Value()
        {
        }

        public Value(double d)
        {
            _double = d;
        }

        public Value(string s)
        {
            AsString = s;
        }

        public Value(Value[] a)
        {
            AsArray = a;
        }

        public bool IsDouble => _double != null;
        public bool IsString => AsString != null;
        public bool IsArray => AsArray != null;
        public bool Empty => (_double == null) && (AsString == null) && (AsArray == null);

        public double AsDouble => _double.Value;
        public string AsString { get; private set; }

        public Value[] AsArray { get; private set; }

        public string Str
        {
            get
            {
                if (IsDouble)
                {
                    return AsDouble.Str();
                }
                if (IsString)
                {
                    return AsString;
                }
                var s = "";
                for (var i = 0; i < AsArray.Length; ++i)
                {
                    if (i != AsArray.Length - 1)
                    {
                        s += AsArray[i].Str + "; ";
                    }
                    else
                    {
                        s += AsArray[i].Str;
                    }
                }
                return s;
            }
        }

        public void SwitchTo(Value v)
        {
            _double = null;
            AsString = null;
            AsArray = null;
            if (v.IsDouble)
            {
                _double = v.AsDouble;
            }
            if (v.IsString)
            {
                AsString = v.AsString;
            }
            if (v.IsArray)
            {
                AsArray = v.AsArray;
            }
        }
    }
}