namespace DynamicVisualizer.Steps
{
    public abstract class ResizeStep : TransformStep
    {
        public enum ResizeStepType
        {
            ResizeRect,
            ResizeEllipse,
            ResizeLine,
            ResizeText
        }

        public abstract ResizeStepType StepType { get; }
    }
}