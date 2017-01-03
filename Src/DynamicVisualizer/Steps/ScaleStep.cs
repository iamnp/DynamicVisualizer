namespace DynamicVisualizer.Steps
{
    public abstract class ScaleStep : TransformStep
    {
        public enum ScaleStepType
        {
            ScaleRect,
            ScaleEllipse,
            ScaleLine,
            ScaleText
        }

        public abstract ScaleStepType StepType { get; }
    }
}