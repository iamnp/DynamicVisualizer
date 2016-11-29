using System.Collections.Generic;
using System.Windows;
using DynamicVisualizer.Controls;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps
{
    public static class StepManager
    {
        public delegate void StepInsertedEventHandler(int index);

        public delegate void StepRemovedEventHandler(int index);

        private const int ThresholdSquared = 10*10;

        public static readonly List<IterableStepGroup> IterableGroups = new List<IterableStepGroup>();

        public static readonly List<Step> Steps = new List<Step>();
        public static readonly List<Figure> Figures = new List<Figure>();
        public static Magnet[] CanvasMagnets;
        public static StepEditor StepEditor;

        static StepManager()
        {
            CurrentStepIndex = -1;
        }

        public static Step CurrentStep => Steps[CurrentStepIndex];

        public static int CurrentStepIndex { get; private set; }

        public static Magnet Snap(Point p, Figure exclude = null)
        {
            var minDistSquared = 1e9;
            Magnet closestMagnet = null;
            foreach (var figure in Figures)
            {
                if (figure == exclude) continue;
                foreach (var magnet in figure.GetMagnets())
                {
                    var dx = p.X - magnet.X.CachedValue.AsDouble;
                    var dy = p.Y - magnet.Y.CachedValue.AsDouble;
                    var distSquared = dx*dx + dy*dy;
                    if (distSquared < minDistSquared)
                    {
                        closestMagnet = magnet;
                        minDistSquared = distSquared;
                    }
                }
            }
            if (minDistSquared > ThresholdSquared)
                foreach (var magnet in CanvasMagnets)
                {
                    var dx = p.X - magnet.X.CachedValue.AsDouble;
                    var dy = p.Y - magnet.Y.CachedValue.AsDouble;
                    var distSquared = dx*dx + dy*dy;
                    if (distSquared < minDistSquared)
                    {
                        closestMagnet = magnet;
                        minDistSquared = distSquared;
                    }
                }
            return minDistSquared <= ThresholdSquared ? closestMagnet : null;
        }

        public static Magnet SnapTo(Point p, Magnet[] magnets)
        {
            var minDistSquared = 1e9;
            Magnet closestMagnet = null;
            foreach (var magnet in magnets)
            {
                var dx = p.X - magnet.X.CachedValue.AsDouble;
                var dy = p.Y - magnet.Y.CachedValue.AsDouble;
                var distSquared = dx*dx + dy*dy;
                if (distSquared < minDistSquared)
                {
                    closestMagnet = magnet;
                    minDistSquared = distSquared;
                }
            }
            return minDistSquared <= ThresholdSquared ? closestMagnet : null;
        }

        public static event StepInsertedEventHandler StepInserted;
        public static event StepRemovedEventHandler StepRemoved;

        private static void BackwardsAndAgain(int index)
        {
            if (index < 0)
            {
                Reset();
                CurrentStepIndex = -1;
            }
            else
            {
                SetCurrentStepIndex(0);
                SetCurrentStepIndex(index);
            }
        }

        public static IterableStepGroup GetGroupByIndex(int index)
        {
            for (var i = 0; i < IterableGroups.Count; ++i)
                if ((IterableGroups[i].StartIndex <= index) && (IterableGroups[i].EndIndex >= index))
                    return IterableGroups[i];
            return null;
        }

        public static void Insert(Step step, int index = -1)
        {
            if (index < 0) index = Steps.Count;
            if ((CurrentStepIndex > -1) && (CurrentStep.Iterations > 0))
            {
                step.MakeIterable(ArrayExpressionEditor.Len);
                GetGroupByIndex(CurrentStepIndex).EndIndex += 1;
            }
            Steps.Insert(index, step);
            BackwardsAndAgain(index);
            StepInserted?.Invoke(index);
        }

        public static void Remove(int pos)
        {
            if (Steps[pos].Iterations > 0)
                GetGroupByIndex(pos).EndIndex -= 1;
            Steps.RemoveAt(pos);
            if (pos <= Steps.Count - 1)
                BackwardsAndAgain(pos);
            else
                BackwardsAndAgain(pos - 1);
            StepRemoved?.Invoke(pos);
        }

        public static void ResetIterations()
        {
            for (var i = 0; i < Steps.Count; ++i)
                if (Steps[i].Iterations != -1)
                    Steps[i].CompletedIterations = 0;
        }

        public static void SetCurrentStepIndex(int index, bool force = false)
        {
            if ((CurrentStepIndex == index) && !force) return;
            if (CurrentStepIndex < index)
            {
                ApplySteps(CurrentStepIndex + 1, index);
                CurrentStepIndex = index;
                StepEditor?.ShowStep(CurrentStep);
                return;
            }
            CurrentStepIndex = index;
            Reset();
            ApplySteps(0, CurrentStepIndex);
            StepEditor?.ShowStep(CurrentStep);
        }

        private static void GetGroupBounds(int index, out int top, out int bot)
        {
            top = -1;
            bot = -1;
            for (var i = 0; i < IterableGroups.Count; ++i)
                if ((IterableGroups[i].StartIndex <= index) && (IterableGroups[i].EndIndex >= index))
                {
                    top = IterableGroups[i].StartIndex;
                    bot = IterableGroups[i].EndIndex;
                }
        }

        public static void NextIterationFromCurrentPos()
        {
            int top, bot;
            GetGroupBounds(CurrentStepIndex, out top, out bot);

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
            GetGroupBounds(CurrentStepIndex, out top, out bot);

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
                    int top, bot;
                    GetGroupBounds(i, out top, out bot);
                    // now Steps[bot] is the last step in iterative group
                    var finalStepInGroup = b <= bot;
                    if (finalStepInGroup)
                    {
                        var totalSteps = (bot - i + 1)*Steps[i].CompletedIterations + (b - i) + 1;
                        for (var n = i; (n <= bot) && (totalSteps > 0); ++n)
                        {
                            Steps[n].CompletedIterations = 0;
                            if (!Steps[n].Applied) Steps[n].Apply();
                            totalSteps -= 1;
                        }
                        for (var k = 0; (k < Steps[i].Iterations) && (totalSteps > 0); ++k)
                            for (var n = i; (n <= bot) && (totalSteps > 0); ++n)
                            {
                                Steps[n].ApplyNextIteration();
                                totalSteps -= 1;
                            }
                    }
                    else
                    {
                        for (var n = i; n <= bot; ++n)
                        {
                            Steps[n].CompletedIterations = 0;
                            if (!Steps[n].Applied) Steps[n].Apply();
                        }
                        for (var k = 0; k < Steps[i].Iterations; ++k)
                            for (var n = i; n <= bot; ++n)
                                Steps[n].ApplyNextIteration();
                    }
                    i = bot;
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