namespace DynamicVisualizer.Steps
{
    public abstract class RotateStep : TransformStep
    {
        public enum RotateStepType
        {
            RotateLine
        }

        public abstract RotateStepType StepType { get; }
    }
}