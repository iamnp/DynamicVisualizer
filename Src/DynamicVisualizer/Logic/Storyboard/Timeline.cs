using System;
using System.Collections.Generic;
using DynamicVisualizer.Logic.Expressions;
using DynamicVisualizer.Logic.Storyboard.Figures;
using DynamicVisualizer.Logic.Storyboard.Steps;

namespace DynamicVisualizer.Logic.Storyboard
{
    public static class Timeline
    {
        public static readonly List<Step> Steps = new List<Step>();
        public static readonly List<Figure> Figures = new List<Figure>();

        static Timeline()
        {
            CurrentStepIndex = -1;
        }

        public static Step CurrentStep => Steps[CurrentStepIndex];

        public static int CurrentStepIndex { get; private set; }
        public static event EventHandler StepsChanged;

        public static void Insert(Step step, int index = -1)
        {
            if (index < 0) index = Steps.Count;
            Steps.Insert(index, step);
            SetCurrentStepIndex(0);
            SetCurrentStepIndex(index);
            StepsChanged?.Invoke(null, EventArgs.Empty);
        }

        public static void SetCurrentStepIndex(int index)
        {
            if (CurrentStepIndex == index) return;
            if (CurrentStepIndex < index)
            {
                for (var i = CurrentStepIndex + 1; i <= index; ++i)
                    if (!Steps[i].Applied) Steps[i].Apply();
                CurrentStepIndex = index;
                return;
            }
            CurrentStepIndex = index;
            Reset();
            for (var i = 0; i <= CurrentStepIndex; ++i)
                if (!Steps[i].Applied) Steps[i].Apply();
        }

        private static void Reset()
        {
            foreach (var step in Steps)
                step.Applied = false;
            Figures.Clear();
            DataStorage.WipeNonDataFields();
        }
    }
}