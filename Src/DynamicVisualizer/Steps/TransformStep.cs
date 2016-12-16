namespace DynamicVisualizer.Steps
{
    public abstract class TransformStep : Step
    {
        public enum TransformStepType
        {
            MoveRect,
            MoveEllipse,
            MoveLine,
            ScaleRect,
            ScaleEllipse,
            ScaleLine,
            ResizeRect,
            ResizeEllipse
        }

        public enum TransformType
        {
            Move,
            Scale,
            Resize
        }

        public abstract TransformStepType StepType { get; }
    }
}