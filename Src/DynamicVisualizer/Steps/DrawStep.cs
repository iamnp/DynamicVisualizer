namespace DynamicVisualizer.Steps
{
    public abstract class DrawStep : Step
    {
        public enum DrawStepType
        {
            DrawRect,
            DrawEllipse,
            DrawLine
        }

        public abstract DrawStepType StepType { get; }
    }
}