namespace DynamicVisualizer.Steps
{
    public abstract class ScaleStep : TransformStep
    {
        public enum ScaleStepType
        {
            ScaleRect,
            ScaleEllipse,
            ScaleLine
        }

        public abstract ScaleStepType StepType { get; }
    }
}