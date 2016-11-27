namespace DynamicVisualizer.Logic.Steps
{
    public abstract class DrawStep : Step
    {
        public enum DrawStepType
        {
            DrawRect,
            DrawEllipse
        }

        public abstract DrawStepType StepType { get; }
    }
}