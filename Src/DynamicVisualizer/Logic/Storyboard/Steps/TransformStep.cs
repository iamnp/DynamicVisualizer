namespace DynamicVisualizer.Logic.Storyboard.Steps
{
    public abstract class TransformStep : Step
    {
        public enum TransformStepType
        {
            MoveRect,
            MoveCircle
        }

        public abstract TransformStepType StepType { get; }
    }
}