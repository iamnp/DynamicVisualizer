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

        public string EndDef;
        public string StartDef;

        public abstract DrawStepType StepType { get; }
    }
}