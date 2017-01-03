namespace DynamicVisualizer.Steps
{
    public abstract class RotateStep : TransformStep
    {
        public enum RotateStepType
        {
            RotateLine,
            RotateText
        }

        public abstract RotateStepType StepType { get; }
    }
}