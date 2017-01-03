using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DynamicVisualizer.Steps;

namespace DynamicVisualizer.Controls
{
    public class StepListControl : Panel
    {
        private readonly List<StepItem> _stepControls = new List<StepItem>();
        public readonly List<StepItem> MarkedControls = new List<StepItem>();
        private StepItem _currentSelection;
        public bool IgnoreMarkAsSelected;
        public MainForm MainForm;

        public StepListControl()
        {
            BorderStyle = BorderStyle.FixedSingle;
            AutoScroll = true;
        }

        public void CurrentSelectionToIterableGroup()
        {
            var min = _stepControls.Count;
            var max = -1;
            for (var i = 0; i < MarkedControls.Count; ++i)
            {
                if (MarkedControls[i].Index < min)
                {
                    min = MarkedControls[i].Index;
                }
                if (MarkedControls[i].Index > max)
                {
                    max = MarkedControls[i].Index;
                }
                MarkedControls[i].Step.MakeIterable(ArrayExpressionEditor.Items.Count > 1
                    ? ArrayExpressionEditor.Len
                    : 2);
                MarkedControls[i].RespectIterable();
            }
            StepManager.IterableGroups.Add(new IterableStepGroup
            {
                StartIndex = min,
                Length = max - min + 1,
                Iterations = ArrayExpressionEditor.Items.Count > 1 ? ArrayExpressionEditor.Len : 2,
                IterationsExpr =
                    ArrayExpressionEditor.Items.Count > 1
                        ? string.Format("len({0})", ArrayExpressionEditor.Items[0].Expr.FullName)
                        : "2"
            });
            ClearMarked();
            ConstructList();
            StepManager.RefreshToCurrentStep();
        }

        public void TimelineOnStepRemoved(int index)
        {
            _stepControls.RemoveAt(index);
            ConstructList();
        }

        public void ClearMarked()
        {
            for (var i = 0; i < MarkedControls.Count; ++i)
            {
                MarkedControls[i].Marked = false;
                MarkedControls[i].BackColor = BackColor;
            }
            MarkedControls.Clear();
        }

        public void ReSetText()
        {
            for (var i = 0; i < _stepControls.Count; i++)
            {
                _stepControls[i].SetText();
            }
        }

        private void ConstructList()
        {
            for (var i = Controls.Count - 1; i >= 0; --i)
            {
                if (Controls[i] is GroupHeaderItem)
                {
                    Controls[i].Dispose();
                }
            }
            Controls.Clear();

            var height = 0;
            IterableStepGroup prevGroup = null;
            for (var i = 0; i < _stepControls.Count; ++i)
            {
                var scc = _stepControls[i];
                if (scc.Step.Iterations != -1)
                {
                    var currentGroup = StepManager.GetGroupByIndex(i);
                    if (currentGroup != prevGroup)
                    {
                        Controls.Add(new GroupHeaderItem(currentGroup)
                        {
                            Width = Width - 2 - 17,
                            Location = new Point(0, height)
                        });
                        height += GroupHeaderItem.HeightValue;
                    }
                    prevGroup = currentGroup;
                }
                scc.Index = i;
                scc.Location = new Point(0, height);
                height += StepItem.HeightValue;
                Controls.Add(scc);
            }
        }

        public void TimelineOnStepInserted(int index)
        {
            var sc = new StepItem(StepManager.Steps[index], index)
            {
                Width = Width - 2 - 17
            };
            sc.MouseEnter += OnMouseEnter;
            sc.MouseLeave += OnMouseLeave;
            sc.MouseClick += OnMouseClick;
            _stepControls.Insert(index, sc);
            ConstructList();
        }

        public void MarkAsSelecgted(int index)
        {
            if (IgnoreMarkAsSelected)
            {
                return;
            }
            if ((_currentSelection != null) && !_currentSelection.Marked)
            {
                _currentSelection.BackColor = BackColor;
            }
            if (index < 0)
            {
                _currentSelection = null;
            }
            else
            {
                _currentSelection = _stepControls[index];
                _currentSelection.BackColor = Color.Aqua;
                ScrollControlIntoView(_currentSelection);
            }
            MainForm.RedrawNeeded?.Invoke();
            MainForm.PerformFigureSelection(_currentSelection?.Step.Figure);
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ClearMarked();
            }
            StepManager.SetCurrentStepIndex(((StepItem) sender).Index);
            if (StepManager.CurrentStep.Iterations == -1)
            {
                StepManager.ResetIterations(StepManager.CurrentStepIndex);
            }
            if (e.Button == MouseButtons.Right)
            {
                if (!_currentSelection.Marked)
                {
                    MarkedControls.Add(_currentSelection);
                }
                _currentSelection.Marked = true;
                _currentSelection.BackColor = Color.DarkGray;
            }
        }

        private void OnMouseLeave(object sender, EventArgs eventArgs)
        {
            if (sender != _currentSelection)
            {
                var c = (StepItem) sender;
                if (!c.Marked)
                {
                    c.BackColor = BackColor;
                }
            }
        }

        private void OnMouseEnter(object sender, EventArgs eventArgs)
        {
            if (sender != _currentSelection)
            {
                var c = (StepItem) sender;
                if (!c.Marked)
                {
                    c.BackColor = Color.Aqua;
                }
            }
        }
    }
}