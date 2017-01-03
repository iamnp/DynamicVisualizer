namespace DynamicVisualizer.Steps
{
    public abstract class MoveStep : TransformStep
    {
        public enum MoveStepType
        {
            MoveRect,
            MoveEllipse,
            MoveLine,
            MoveText
        }

        public abstract MoveStepType StepType { get; }
    }
}