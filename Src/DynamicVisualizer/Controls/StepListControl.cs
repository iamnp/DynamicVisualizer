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
        private bool _ignoreSelectionChanged;

        public StepListControl()
        {
            BorderStyle = BorderStyle.FixedSingle;
            AutoScroll = true;

            StepManager.StepInserted += TimelineOnStepInserted;
            StepManager.StepRemoved += TimelineOnStepRemoved;
        }

        public StepItem CurrentSelection
        {
            get { return _currentSelection; }
            set
            {
                _currentSelection = value;
                if ((_currentSelection != null) && !_ignoreSelectionChanged)
                    StepManager.SetCurrentStepIndex(_currentSelection.Index);
                RedrawNeeded?.Invoke();
            }
        }

        public void CurrentSelectionToIterableGroup()
        {
            var min = _stepControls.Count;
            var max = -1;
            for (var i = 0; i < MarkedControls.Count; ++i)
            {
                if (MarkedControls[i].Index < min) min = MarkedControls[i].Index;
                if (MarkedControls[i].Index > max) max = MarkedControls[i].Index;
                MarkedControls[i].Step.MakeIterable(ArrayExpressionEditor.Len);
                MarkedControls[i].RespectIterable();
            }
            StepManager.IterableGroups.Add(new IterableStepGroup
            {
                StartIndex = min,
                EndIndex = max
            });
            StepManager.IterableGroups.Sort((a, b) => a.StartIndex.CompareTo(b.StartIndex));
            ClearMarked();
            MarkAsSelecgted(CurrentSelection);
            StepManager.SetCurrentStepIndex(StepManager.CurrentStepIndex, true);
        }

        private void TimelineOnStepRemoved(int index)
        {
            Controls.RemoveAt(index);
            _stepControls.RemoveAt(index);
            if (_stepControls.Count != 0)
            {
                for (var i = index; i < _stepControls.Count; ++i)
                {
                    var scc = _stepControls[i];
                    scc.Index = index;
                    scc.Location = new Point(0, scc.Location.Y - scc.Height);
                }

                _ignoreSelectionChanged = true;
                if (index <= _stepControls.Count - 1)
                    MarkAsSelecgted(_stepControls[index]);
                else MarkAsSelecgted(_stepControls[index - 1]);
                _ignoreSelectionChanged = false;
            }
            else
            {
                CurrentSelection = null;
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
                _stepControls[i].SetText();
        }

        private void TimelineOnStepInserted(int index)
        {
            _ignoreSelectionChanged = true;

            for (var i = index; i < _stepControls.Count; ++i)
            {
                var scc = _stepControls[i];
                scc.Index = index + 1;
                scc.Location = new Point(0, scc.Location.Y + scc.Height);
            }

            var sc = new StepItem(StepManager.Steps[index], index)
            {
                Width = Width - 2
            };
            sc.Location = new Point(0, index*sc.Height);
            sc.MouseEnter += OnMouseEnter;
            sc.MouseLeave += OnMouseLeave;
            sc.MouseClick += OnMouseClick;
            Controls.Add(sc);
            _stepControls.Insert(index, sc);
            if (_currentSelection == null) MarkAsSelecgted(_stepControls[0]);
            else MarkAsSelecgted(_stepControls[_currentSelection.Index + 1]);
            for (var i = 0; i < _stepControls.Count; ++i)
                _stepControls[i].Index = i;
            _ignoreSelectionChanged = false;
        }

        public event Action RedrawNeeded;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                if (_currentSelection.Step.Iterations != -1)
                {
                    StepManager.PrevIterationFromCurrentPos();
                    _ignoreSelectionChanged = true;
                    MarkAsSelecgted(_stepControls[StepManager.CurrentStepIndex]);
                    _ignoreSelectionChanged = false;
                }
                else if (_currentSelection.Index > 0)
                    MarkAsSelecgted(_stepControls[_currentSelection.Index - 1]);
                return true;
            }
            if (keyData == Keys.Down)
            {
                if (_currentSelection.Step.Iterations != -1)
                {
                    StepManager.NextIterationFromCurrentPos();
                    _ignoreSelectionChanged = true;
                    MarkAsSelecgted(_stepControls[StepManager.CurrentStepIndex]);
                    _ignoreSelectionChanged = false;
                }
                else if (_currentSelection.Index < _stepControls.Count - 1)
                    MarkAsSelecgted(_stepControls[_currentSelection.Index + 1]);
                return true;
            }
            if (keyData == Keys.Delete)
            {
                if (_currentSelection != null) StepManager.Remove(_currentSelection.Index);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void MarkAsSelecgted(StepItem sc)
        {
            if ((CurrentSelection != null) && !CurrentSelection.Marked)
                CurrentSelection.BackColor = BackColor;
            CurrentSelection = sc;
            CurrentSelection.BackColor = Color.Aqua;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            StepManager.ResetIterations();
            MarkAsSelecgted((StepItem) sender);
            if (e.Button == MouseButtons.Left)
            {
                ClearMarked();
            }
            else if (e.Button == MouseButtons.Right)
            {
                CurrentSelection.Marked = true;
                CurrentSelection.BackColor = Color.DarkGray;
                MarkedControls.Add(CurrentSelection);
            }
        }

        private void OnMouseLeave(object sender, EventArgs eventArgs)
        {
            if (sender != CurrentSelection)
            {
                var c = (StepItem) sender;
                if (!c.Marked) c.BackColor = BackColor;
            }
        }

        private void OnMouseEnter(object sender, EventArgs eventArgs)
        {
            if (sender != CurrentSelection)
            {
                var c = (StepItem) sender;
                if (!c.Marked) c.BackColor = Color.Aqua;
            }
        }
    }
}