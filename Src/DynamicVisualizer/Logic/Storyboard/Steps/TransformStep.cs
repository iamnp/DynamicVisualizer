namespace DynamicVisualizer.Logic.Storyboard.Steps
{
    public abstract class TransformStep : Step
    {
        public enum TransformStepType
        {
            MoveRect,
            MoveEllipse,
            ScaleRect,
            ScaleEllipse
        }

        public abstract TransformStepType StepType { get; }
    }
}