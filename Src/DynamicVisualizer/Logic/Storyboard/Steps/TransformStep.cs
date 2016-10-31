namespace DynamicVisualizer.Logic.Storyboard.Steps
{
    public abstract class TransformStep : Step
    {
        public enum TransformStepType
        {
            MoveRect
        }

        public abstract TransformStepType StepType { get; }
    }
}