using System;
using System.Collections.Generic;
using System.Windows;
using DynamicVisualizer.Controls;
using DynamicVisualizer.Expressions;
using DynamicVisualizer.Figures;

namespace DynamicVisualizer.Steps
{
    public static class StepManager
    {
        public const int ThresholdSquared = 8 * 8;
        private const double MaxDist = 1e9;

        public static readonly List<IterableStepGroup> IterableGroups = new List<IterableStepGroup>();

        public static readonly List<Step> Steps = new List<Step>();
        public static Step FinalStep;
        public static readonly List<Figure> Figures = new List<Figure>();
        public static Magnet[] CanvasMagnets;
        public static StepEditor StepEditor;
        public static StepListControl StepListControl;
        public static bool AddStepBeforeCurrent;
        public static bool AddStepLooped = true;

        static StepManager()
        {
            CurrentStepIndex = -1;
        }

        public static Step CurrentStep
        {
            get
            {
                if ((CurrentStepIndex >= 0) && (CurrentStepIndex <= Steps.Count - 1))
                {
                    return Steps[CurrentStepIndex];
                }
                return null;
            }
        }

        public static int CurrentStepIndex { get; private set; }

        public static Magnet Snap(Point p, Figure exclude = null)
        {
            var minDistSquared = MaxDist;
            Magnet closestMagnet = null;
            foreach (var figure in Figures)
            {
                if (figure == exclude)
                {
                    continue;
                }
                var magnets = figure.GetMagnets();
                if (magnets == null)
                {
                    continue;
                }
                foreach (var magnet in magnets)
                {
                    var dx = p.X - magnet.X.CachedValue.AsDouble;
                    var dy = p.Y - magnet.Y.CachedValue.AsDouble;
                    var distSquared = dx * dx + dy * dy;
                    if ((distSquared < minDistSquared) ||
                        ((Math.Abs(distSquared - minDistSquared) < double.Epsilon) && figure.IsSelected))
                    {
                        closestMagnet = magnet;
                        minDistSquared = distSquared;
                    }
                }
            }
            if (minDistSquared > ThresholdSquared)
            {
                foreach (var magnet in CanvasMagnets)
                {
                    var dx = p.X - magnet.X.CachedValue.AsDouble;
                    var dy = p.Y - magnet.Y.CachedValue.AsDouble;
                    var distSquared = dx * dx + dy * dy;
                    if (distSquared < minDistSquared)
                    {
                        closestMagnet = magnet;
                        minDistSquared = distSquared;
                    }
                }
            }
            return minDistSquared <= ThresholdSquared ? closestMagnet : null;
        }

        public static Magnet SnapTo(Point p, Magnet[] magnets, Magnet exclude = null)
        {
            var minDistSquared = MaxDist;
            Magnet closestMagnet = null;
            foreach (var magnet in magnets)
            {
                if (magnet == exclude)
                {
                    continue;
                }
                var dx = p.X - magnet.X.CachedValue.AsDouble;
                var dy = p.Y - magnet.Y.CachedValue.AsDouble;
                var distSquared = dx * dx + dy * dy;
                if (distSquared < minDistSquared)
                {
                    closestMagnet = magnet;
                    minDistSquared = distSquared;
                }
            }
            return minDistSquared <= ThresholdSquared ? closestMagnet : null;
        }

        public static IterableStepGroup GetGroupByIndex(int index)
        {
            for (var i = 0; i < IterableGroups.Count; ++i)
            {
                var g = IterableGroups[i];
                if ((g.StartIndex <= index) && (g.EndIndex >= index))
                {
                    return g;
                }
            }
            return null;
        }

        public static void InsertNext(Step step)
        {
            var index = CurrentStepIndex == -1
                ? 0
                : (AddStepBeforeCurrent ? CurrentStepIndex : CurrentStepIndex + 1);
            if ((CurrentStepIndex > -1) && (CurrentStep.Iterations > -1))
            {
                var g = GetGroupByIndex(CurrentStepIndex);
                if (((index > g.StartIndex) && (index < g.EndIndex))
                    ||
                    (((index == g.StartIndex) || (index == g.StartIndex - 1) || (index == g.EndIndex) ||
                      (index == g.EndIndex + 1)) && AddStepLooped))
                {
                    step.MakeIterable(g.Iterations);
                    GetGroupByIndex(CurrentStepIndex).Length += 1;
                }
            }
            for (var i = 0; i < IterableGroups.Count; ++i)
            {
                var g = IterableGroups[i];
                if ((g.StartIndex > index) || ((g.StartIndex == index) && (step.Iterations == -1)))
                {
                    g.StartIndex += 1;
                }
            }
            Steps.Insert(index, step);
            StepListControl?.TimelineOnStepInserted(index);
            SetCurrentStepIndex(index);
        }

        public static void Remove(int pos)
        {
            if (Steps[pos].Iterations > -1)
            {
                var g = GetGroupByIndex(pos);
                g.Length -= 1;
                if (g.Length < 1)
                {
                    IterableGroups.Remove(g);
                }
            }
            for (var i = 0; i < IterableGroups.Count; ++i)
            {
                var g = IterableGroups[i];
                if (g.StartIndex > pos)
                {
                    g.StartIndex -= 1;
                }
            }
            Steps.RemoveAt(pos);
            StepListControl?.TimelineOnStepRemoved(pos);
            if (pos <= Steps.Count - 1)
            {
                SetCurrentStepIndex(pos);
            }
            else
            {
                SetCurrentStepIndex(pos - 1);
            }
        }

        public static void ResetIterations(int firstStep = 0)
        {
            for (var i = firstStep; i < Steps.Count; ++i)
            {
                if (Steps[i].Iterations != -1)
                {
                    Steps[i].CompletedIterations = 0;
                }
            }
        }

        public static void RefreshToCurrentStep()
        {
            SetCurrentStepIndex(CurrentStepIndex);
        }

        public static void SetCurrentStepIndex(int index)
        {
            CurrentStepIndex = index;
            int pos;
            if ((FinalStep != null) && ((pos = Steps.IndexOf(FinalStep)) != -1))
            {
                ApplySteps(pos);
                Drawer.SaveCurrentScene();
            }
            else
            {
                Drawer.DeleteCurrentScene();
            }
            ApplySteps(index);
            StepListControl?.MarkAsSelecgted(CurrentStepIndex);
        }

        private static void ApplySteps(int index)
        {
            Reset();
            for (var i = 0; i <= index; ++i)
            {
                if (Steps[i].Iterations != -1) // we got first iterative step in a group
                {
                    //finding the last one
                    int top, bot;
                    GetGroupBounds(i, out top, out bot);
                    GetGroupByIndex(i).IterationsExpr.Recalculate();
                    // now Steps[bot] is the last step in iterative group
                    var finalStepInGroup = index <= bot;
                    if (finalStepInGroup)
                    {
                        var totalSteps = (bot - i + 1) * Steps[i].CompletedIterations + (index - i) + 1;
                        for (var n = i; (n <= bot) && (totalSteps > 0); ++n)
                        {
                            Steps[n].CompletedIterations = 0;
                            if (!Steps[n].Applied)
                            {
                                Steps[n].Apply();
                            }
                            totalSteps -= 1;
                        }
                        for (var k = 0; (k < Steps[i].Iterations) && (totalSteps > 0); ++k)
                        {
                            for (var n = i; (n <= bot) && (totalSteps > 0); ++n)
                            {
                                Steps[n].ApplyNextIteration();
                                totalSteps -= 1;
                            }
                        }
                    }
                    else
                    {
                        for (var n = i; n <= bot; ++n)
                        {
                            Steps[n].CompletedIterations = 0;
                            if (!Steps[n].Applied)
                            {
                                Steps[n].Apply();
                            }
                        }
                        for (var k = 0; k < Steps[i].Iterations; ++k)
                        {
                            for (var n = i; n <= bot; ++n)
                            {
                                Steps[n].ApplyNextIteration();
                            }
                        }
                    }
                    i = bot;
                }
                else if (!Steps[i].Applied)
                {
                    Steps[i].Apply();
                }
            }
        }

        private static void GetGroupBounds(int index, out int top, out int bot)
        {
            top = -1;
            bot = -1;
            for (var i = 0; i < IterableGroups.Count; ++i)
            {
                var g = IterableGroups[i];
                if ((g.StartIndex <= index) && (g.EndIndex >= index))
                {
                    top = g.StartIndex;
                    bot = g.EndIndex;
                    break;
                }
            }
        }

        public static void NextLoopFromCurrentPos()
        {
            StepListControl.IgnoreMarkAsSelected = true;
            int top, bot;
            GetGroupBounds(CurrentStepIndex, out top, out bot);
            var len = bot - top + 1;
            for (var i = 0; i < len; ++i)
            {
                NextIterationFromCurrentPos();
            }
            StepListControl.IgnoreMarkAsSelected = false;
            StepListControl.MarkAsSelecgted(CurrentStepIndex);
        }

        public static void PrevLoopFromCurrentPos()
        {
            StepListControl.IgnoreMarkAsSelected = true;
            int top, bot;
            GetGroupBounds(CurrentStepIndex, out top, out bot);
            var len = bot - top + 1;
            for (var i = 0; i < len; ++i)
            {
                PrevIterationFromCurrentPos(true);
            }
            SetCurrentStepIndex(CurrentStepIndex);
            StepListControl.IgnoreMarkAsSelected = false;
            StepListControl.MarkAsSelecgted(CurrentStepIndex);
        }

        /// <summary>
        ///     Does not start from the first step, apllies only not applied steps.
        /// </summary>
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
                        SetCurrentStepIndex(CurrentStepIndex + 1);
                    }
                }
                else
                {
                    Steps[top].ApplyNextIteration();
                    CurrentStepIndex = top;
                    StepListControl?.MarkAsSelecgted(CurrentStepIndex);
                }
            }
            else
            {
                if (!Steps[CurrentStepIndex + 1].Applied)
                {
                    Steps[CurrentStepIndex + 1].Apply();
                }
                else
                {
                    Steps[CurrentStepIndex + 1].ApplyNextIteration();
                }
                CurrentStepIndex += 1;
                StepListControl?.MarkAsSelecgted(CurrentStepIndex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="doNotSetCurrentStep">
        ///     True if method shoud not apply all steps from the first one to cuurrent new - only
        ///     change current index.
        /// </param>
        public static void PrevIterationFromCurrentPos(bool doNotSetCurrentStep = false)
        {
            int top, bot;
            GetGroupBounds(CurrentStepIndex, out top, out bot);

            if (CurrentStepIndex == top)
            {
                if (Steps[CurrentStepIndex].CompletedIterations == 0)
                {
                    if (CurrentStepIndex > 0)
                    {
                        if (doNotSetCurrentStep)
                        {
                            CurrentStepIndex -= 1;
                        }
                        else
                        {
                            SetCurrentStepIndex(CurrentStepIndex - 1);
                        }
                    }
                }
                else
                {
                    Steps[CurrentStepIndex].CompletedIterations -= 1;
                    if (doNotSetCurrentStep)
                    {
                        CurrentStepIndex = bot;
                    }
                    else
                    {
                        SetCurrentStepIndex(bot);
                    }
                }
            }
            else
            {
                if (Steps[CurrentStepIndex].CompletedIterations > 0)
                {
                    Steps[CurrentStepIndex].CompletedIterations -= 1;
                }
                if (doNotSetCurrentStep)
                {
                    CurrentStepIndex -= 1;
                }
                else
                {
                    SetCurrentStepIndex(CurrentStepIndex - 1);
                }
            }
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