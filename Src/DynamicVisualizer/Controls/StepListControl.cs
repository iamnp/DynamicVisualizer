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
            AutoScroll = true;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Resize += OnResize;
        }

        protected override void WndProc(ref Message m)
        {
            WinApi.ShowScrollBar(Handle, (int) WinApi.ScrollBarDirection.SB_VERT, false);
            base.WndProc(ref m);
        }

        private void OnResize(object sender, EventArgs eventArgs)
        {
            ConstructList();
        }

        public void CurrentSelectionToIterableGroup()
        {
            if (MarkedControls.Count == 0)
            {
                return;
            }
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
            if (max + 1 == StepManager.Steps.Count)
            {
                StepManager.Insert(new EmptyStep(), max + 1, false, null, false, true);
            }
            StepManager.IterableGroups.Add(new IterableStepGroup(ArrayExpressionEditor.Items.Count > 1
                ? string.Format("len({0})", ArrayExpressionEditor.Items[0].Expr.FullName)
                : "2")
            {
                StartIndex = min,
                Length = max - min + 1
            });
            ClearMarked();
            ConstructList();
            StepManager.RefreshToCurrentStep();
        }

        public void TimelineOnStepRemoved(int index, bool silent = false)
        {
            _stepControls.RemoveAt(index);
            if (!silent)
            {
                ConstructList();
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

        public void ConstructList()
        {
            SuspendLayout();
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
                            Width = Width,
                            Location = new Point(0, height - VerticalScroll.Value)
                        });
                        height += GroupHeaderItem.HeightValue;
                    }
                    prevGroup = currentGroup;
                }
                scc.Index = i;
                scc.Width = Width;
                scc.Location = new Point(0, height - VerticalScroll.Value);
                height += StepItem.HeightValue;
                Controls.Add(scc);
            }
            ResumeLayout(true);
        }

        public void TimelineOnStepInserted(int index, bool silent = false)
        {
            var sc = new StepItem(StepManager.Steps[index], index);
            sc.MouseEnter += OnMouseEnter;
            sc.MouseLeave += OnMouseLeave;
            sc.MouseClick += OnMouseClick;
            _stepControls.Insert(index, sc);
            if (!silent)
            {
                ConstructList();
            }
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