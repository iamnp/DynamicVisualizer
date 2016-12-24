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
        public Form1 Form1;
        public bool IgnoreMarkAsSelected;

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
                MarkedControls[i].Step.MakeIterable(ArrayExpressionEditor.Len);
                MarkedControls[i].RespectIterable();
            }
            StepManager.IterableGroups.Add(new IterableStepGroup
            {
                StartIndex = min,
                Length = max - min + 1
            });
            StepManager.IterableGroups.Sort((a, b) => a.StartIndex.CompareTo(b.StartIndex));
            ClearMarked();
            StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex);
        }

        public void TimelineOnStepRemoved(int index)
        {
            Controls.RemoveAt(index);
            _stepControls.RemoveAt(index);
            if (_stepControls.Count != 0)
            {
                for (var i = index; i < _stepControls.Count; ++i)
                {
                    var scc = _stepControls[i];
                    scc.Index = i;
                    scc.Location = new Point(0, scc.Location.Y - scc.Height);
                }
            }
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

        public void TimelineOnStepInserted(int index)
        {
            for (var i = index; i < _stepControls.Count; ++i)
            {
                var scc = _stepControls[i];
                scc.Index = i + 1;
                scc.Location = new Point(0, scc.Location.Y + scc.Height);
            }

            var sc = new StepItem(StepManager.Steps[index], index)
            {
                Width = Width - 2 - 17
            };
            sc.Location = new Point(0, index * sc.Height + AutoScrollPosition.Y);
            sc.MouseEnter += OnMouseEnter;
            sc.MouseLeave += OnMouseLeave;
            sc.MouseClick += OnMouseClick;
            Controls.Add(sc);
            Controls.SetChildIndex(sc, index);
            _stepControls.Insert(index, sc);
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
            Form1.RedrawNeeded?.Invoke();
            Form1.PerformFigureSelection(_currentSelection?.Step.Figure);
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