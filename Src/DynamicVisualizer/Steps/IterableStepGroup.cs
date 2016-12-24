namespace DynamicVisualizer.Steps
{
    public class IterableStepGroup
    {
        public int Length;
        public int StartIndex;
        public int EndIndex => StartIndex + Length - 1;
    }
}