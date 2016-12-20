namespace DynamicVisualizer.Steps
{
    public abstract class MoveStep : TransformStep
    {
        public enum MoveStepType
        {
            MoveRect,
            MoveEllipse,
            MoveLine
        }

        public abstract MoveStepType StepType { get; }
    }
}