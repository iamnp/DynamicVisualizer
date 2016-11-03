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
        private StepControl _currentSelection;
        private bool _ignoreSelectionChanged;

        public StepsListControl()
        {
            BorderStyle = BorderStyle.FixedSingle;
            AutoScroll = true;

            Timeline.StepInserted += TimelineOnStepInserted;
            KeyDown += OnKeyDown;
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

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (_currentSelection.Index > 0)
                    MarkAsSelecgted(_stepControls[_currentSelection.Index - 1]);
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (_currentSelection.Index < _stepControls.Count - 1)
                    MarkAsSelecgted(_stepControls[_currentSelection.Index + 1]);
            }
        }

        private void MarkAsSelecgted(StepControl sc)
        {
            if (CurrentSelection != null)
                CurrentSelection.BackColor = BackColor;
            CurrentSelection = sc;
            CurrentSelection.BackColor = Color.BlueViolet;
        }

        private void OnMouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            Timeline.ResetIterations();
            MarkAsSelecgted((StepControl) sender);
        }

        private void OnMouseLeave(object sender, EventArgs eventArgs)
        {
            if (sender != CurrentSelection)
                ((Control) sender).BackColor = BackColor;
        }

        private void OnMouseEnter(object sender, EventArgs eventArgs)
        {
            if (sender != CurrentSelection)
                ((Control) sender).BackColor = Color.Aqua;
        }
    }
}