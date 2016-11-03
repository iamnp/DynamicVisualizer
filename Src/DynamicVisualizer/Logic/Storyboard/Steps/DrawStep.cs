namespace DynamicVisualizer.Logic.Storyboard.Steps
{
    public abstract class DrawStep : Step
    {
        public enum DrawStepType
        {
            DrawRect,
            DrawCircle
        }

        public abstract DrawStepType StepType { get; }
    }
}