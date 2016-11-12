using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DynamicVisualizer.Logic.Storyboard;

namespace DynamicVisualizer.Controls
{
    public class StepsListControl : Panel
    {
        private readonly List<StepControl> _stepControls = new List<StepControl>();
        public readonly List<StepControl> MarkedControls = new List<StepControl>();
        private StepControl _currentSelection;
        private bool _ignoreSelectionChanged;

        public StepsListControl()
        {
            BorderStyle = BorderStyle.FixedSingle;
            AutoScroll = true;

            Timeline.StepInserted += TimelineOnStepInserted;
        }

        public StepControl CurrentSelection
        {
            get { return _currentSelection; }
            set
            {
                _currentSelection = value;
                if ((_currentSelection != null) && !_ignoreSelectionChanged)
                    Timeline.SetCurrentStepIndex(_currentSelection.Index);
                RedrawNeeded?.Invoke();
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
                scc.Location = new Point(0, scc.Location.Y + scc.Height);
            }

            var sc = new StepControl(Timeline.Steps[index], index)
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
                    Timeline.PrevIterationFromCurrentPos();
                    _ignoreSelectionChanged = true;
                    MarkAsSelecgted(_stepControls[Timeline.CurrentStepIndex]);
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
                    Timeline.NextIterationFromCurrentPos();
                    _ignoreSelectionChanged = true;
                    MarkAsSelecgted(_stepControls[Timeline.CurrentStepIndex]);
                    _ignoreSelectionChanged = false;
                }
                else if (_currentSelection.Index < _stepControls.Count - 1)
                    MarkAsSelecgted(_stepControls[_currentSelection.Index + 1]);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void MarkAsSelecgted(StepControl sc)
        {
            if ((CurrentSelection != null) && !CurrentSelection.Marked)
                CurrentSelection.BackColor = BackColor;
            CurrentSelection = sc;
            CurrentSelection.BackColor = Color.Aqua;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            Timeline.ResetIterations();
            MarkAsSelecgted((StepControl) sender);
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
                var c = (StepControl) sender;
                if (!c.Marked) c.BackColor = BackColor;
            }
        }

        private void OnMouseEnter(object sender, EventArgs eventArgs)
        {
            if (sender != CurrentSelection)
            {
                var c = (StepControl) sender;
                if (!c.Marked) c.BackColor = Color.Aqua;
            }
        }
    }
}