﻿using DynamicVisualizer.Logic.Storyboard.Figures;

namespace DynamicVisualizer.Logic.Storyboard.Steps
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
        public abstract void ApplyNextIteration();
    }
}