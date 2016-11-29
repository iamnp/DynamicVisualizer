using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps
{
    public abstract class Step
    {
        public bool Applied;
        public int CompletedIterations = -1;
        public Figure Figure;
        public int Iterations = -1;

        public void MakeIterable(int iterations)
        {
            Iterations = iterations - 1;
            CompletedIterations = 0;
        }

        public abstract void Apply();

        public void ApplyNextIteration()
        {
            if (CompletedIterations >= Iterations) return;
            CompletedIterations++;
            IterateNext();
            if (!Figure.IsGuide) CopyStaticFigure();
        }

        public abstract void IterateNext();
        public abstract void CopyStaticFigure();
    }
}