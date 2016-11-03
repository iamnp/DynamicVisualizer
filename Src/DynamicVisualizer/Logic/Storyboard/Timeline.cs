using System.Collections.Generic;
using DynamicVisualizer.Logic.Expressions;
using DynamicVisualizer.Logic.Storyboard.Figures;
using DynamicVisualizer.Logic.Storyboard.Steps;

namespace DynamicVisualizer.Logic.Storyboard
{
    public static class Timeline
    {
        public delegate void StepInsertedEventHandler(int index);

        public static readonly List<Step> Steps = new List<Step>();
        public static readonly List<Figure> Figures = new List<Figure>();

        static Timeline()
        {
            CurrentStepIndex = -1;
        }

        public static Step CurrentStep => Steps[CurrentStepIndex];

        public static int CurrentStepIndex { get; private set; }
        public static event StepInsertedEventHandler StepInserted;

        private static void BackwardsAndAgain(int index)
        {
            SetCurrentStepIndex(0);
            SetCurrentStepIndex(index);
        }

        public static void Insert(Step step, int index = -1)
        {
            if (index < 0) index = Steps.Count;
            Steps.Insert(index, step);
            BackwardsAndAgain(index);
            StepInserted?.Invoke(index);
        }

        public static void ResetIterations()
        {
            for (var i = 0; i < Steps.Count; ++i)
                if (Steps[i].Iterations != -1)
                    Steps[i].CompletedIterations = 0;
        }

        public static void SetCurrentStepIndex(int index)
        {
            if (CurrentStepIndex == index) return;
            if (CurrentStepIndex < index)
            {
                ApplySteps(CurrentStepIndex + 1, index);
                CurrentStepIndex = index;
                return;
            }
            CurrentStepIndex = index;
            Reset();
            ApplySteps(0, CurrentStepIndex);
        }

        private static void GroupBounds(int index, out int top, out int bot)
        {
            top = index;
            while ((top >= 0) && (Steps[top].Iterations == Steps[index].Iterations)) top -= 1;
            top += 1;

            bot = index;
            while ((bot < Steps.Count) && (Steps[bot].Iterations == Steps[index].Iterations)) bot += 1;
            bot -= 1;
        }

        public static void NextIterationFromCurrentPos()
        {
            int top, bot;
            GroupBounds(CurrentStepIndex, out top, out bot);

            if (CurrentStepIndex == bot)
            {
                if (Steps[CurrentStepIndex].CompletedIterations == Steps[CurrentStepIndex].Iterations)
                {
                    if (CurrentStepIndex < Steps.Count - 1)
                    {
                        ResetIterations();
                        SetCurrentStepIndex(CurrentStepIndex + 1);
                    }
                }
                else
                {
                    Steps[top].ApplyNextIteration();
                    CurrentStepIndex = top;
                }
            }
            else
            {
                if (!Steps[CurrentStepIndex + 1].Applied) Steps[CurrentStepIndex + 1].Apply();
                else Steps[CurrentStepIndex + 1].ApplyNextIteration();
                CurrentStepIndex += 1;
            }
        }

        public static void PrevIterationFromCurrentPos()
        {
            int top, bot;
            GroupBounds(CurrentStepIndex, out top, out bot);

            if (CurrentStepIndex == top)
            {
                if (Steps[CurrentStepIndex].CompletedIterations == 0)
                {
                    if (CurrentStepIndex > 0)
                    {
                        ResetIterations();
                        BackwardsAndAgain(CurrentStepIndex - 1);
                    }
                }
                else
                {
                    Steps[CurrentStepIndex].CompletedIterations -= 1;
                    BackwardsAndAgain(bot);
                }
            }
            else
            {
                Steps[CurrentStepIndex].CompletedIterations -= 1;
                BackwardsAndAgain(CurrentStepIndex - 1);
            }
        }

        private static void ApplySteps(int a, int b)
        {
            for (var i = a; i <= b; ++i)
                if (Steps[i].Iterations != -1) // we got first iterative step in a group
                {
                    //finding the last one
                    var j = i + 1;
                    while ((j < Steps.Count) && (Steps[j].Iterations == Steps[i].Iterations)) j += 1;
                    j -= 1;
                    // now Steps[j] is the last step in iterative group
                    var finalStepInGroup = b <= j;
                    if (finalStepInGroup)
                    {
                        var totalSteps = (j - i + 1)*Steps[i].CompletedIterations + (b - i) + 1;
                        for (var n = i; (n <= j) && (totalSteps > 0); ++n)
                        {
                            if (!Steps[n].Applied) Steps[n].Apply();
                            Steps[n].CompletedIterations = 0;
                            totalSteps -= 1;
                        }
                        for (var k = 0; (k < Steps[i].Iterations) && (totalSteps > 0); ++k)
                            for (var n = i; (n <= j) && (totalSteps > 0); ++n)
                            {
                                Steps[n].ApplyNextIteration();
                                totalSteps -= 1;
                            }
                    }
                    else
                    {
                        for (var n = i; n <= j; ++n)
                        {
                            if (!Steps[n].Applied) Steps[n].Apply();
                            Steps[n].CompletedIterations = 0;
                        }
                        for (var k = 0; k < Steps[i].Iterations; ++k)
                            for (var n = i; n <= j; ++n)
                                Steps[n].ApplyNextIteration();
                    }
                    i = j;
                }
                else if (!Steps[i].Applied) Steps[i].Apply();
        }

        private static void Reset()
        {
            foreach (var step in Steps)
            {
                step.Applied = false;
                step.Figure.StaticLoopFigures.Clear();
            }
            Figures.Clear();
            DataStorage.WipeNonDataFields();
        }
    }
}