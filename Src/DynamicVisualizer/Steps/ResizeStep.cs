namespace DynamicVisualizer.Steps
{
    public abstract class ResizeStep : TransformStep
    {
        public enum ResizeStepType
        {
            ResizeRect,
            ResizeEllipse,
            ResizeLine
        }

        public abstract ResizeStepType StepType { get; }
    }
}