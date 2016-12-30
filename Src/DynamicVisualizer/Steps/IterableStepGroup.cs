namespace DynamicVisualizer.Steps
{
    public class IterableStepGroup
    {
        private int _iterations;

        public string IterationsExpr;
        public int Length;
        public int StartIndex;
        public int EndIndex => StartIndex + Length - 1;

        public int Iterations
        {
            get { return _iterations; }
            set
            {
                _iterations = value;
                for (var i = StartIndex; i < StartIndex + Length; ++i)
                {
                    StepManager.Steps[i].Iterations = _iterations - 1;
                }
            }
        }
    }
}