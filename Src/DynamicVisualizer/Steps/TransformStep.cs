namespace DynamicVisualizer.Steps
{
    public abstract class TransformStep : Step
    {
        public enum TransformType
        {
            Move,
            Scale,
            Resize,
            Rotate
        }
    }
}