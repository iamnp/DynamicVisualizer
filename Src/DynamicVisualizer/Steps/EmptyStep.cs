namespace DynamicVisualizer.Steps
{
    public class EmptyStep : Step
    {
        public EmptyStep()
        {
            Def = "Empty step";
        }

        public override void Apply()
        {
        }

        public override void IterateNext()
        {
        }

        public override void CopyStaticFigure()
        {
        }
    }
}