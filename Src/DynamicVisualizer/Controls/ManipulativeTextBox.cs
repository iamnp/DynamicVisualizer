using System;
using System.Windows.Forms;
using System.Windows.Input;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace DynamicVisualizer.Controls
{
    public class ManipulativeTextBox : TextBox
    {
        private int _decimalPlacesAfterPoint;
        private int _initialMouseXPos;
        private double _initialValue;
        private double _lastNum = double.NaN;
        private int _manipulatingSelectionStart;
        private int _manipulatingSelectionStop;
        public bool Manipulating { get; private set; }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Manipulating = false;
            if ((Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) !=
                System.Windows.Input.ModifierKeys.Control)
            {
                return;
            }
            var cursorPos = SelectionStart;
            if (cursorPos == Text.Length)
            {
                cursorPos -= 1;
            }
            if (!IsNumberSymbol(Text[cursorPos]))
            {
                if ((cursorPos > 0) && IsNumberSymbol(Text[cursorPos - 1]))
                {
                    cursorPos -= 1;
                }
                else
                {
                    return;
                }
            }

            _manipulatingSelectionStart = cursorPos;
            while ((_manipulatingSelectionStart > 0) && IsNumberSymbol(Text[_manipulatingSelectionStart - 1]))
            {
                _manipulatingSelectionStart -= 1;
            }

            _manipulatingSelectionStop = cursorPos;
            while ((_manipulatingSelectionStop < Text.Length - 1) &&
                   IsNumberSymbol(Text[_manipulatingSelectionStop + 1]))
            {
                _manipulatingSelectionStop += 1;
            }

            if (_manipulatingSelectionStop - _manipulatingSelectionStart < 0)
            {
                return;
            }

            var number = Text.Substring(_manipulatingSelectionStart,
                _manipulatingSelectionStop - _manipulatingSelectionStart + 1).Replace(",", ".");

            var pointPos = number.IndexOf('.');
            _decimalPlacesAfterPoint = pointPos == -1 ? 0 : number.Length - pointPos - 1;

            if (!double.TryParse(number.Replace(".", ","), out _initialValue))
            {
                return;
            }

            Select(_manipulatingSelectionStart, _manipulatingSelectionStop - _manipulatingSelectionStart + 1);

            _initialMouseXPos = e.X;
            Manipulating = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Manipulating)
            {
                ManipulateSelectedNumber(e.X);
            }
            else
            {
                base.OnMouseMove(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Manipulating = false;
        }

        private void ManipulateSelectedNumber(int x)
        {
            var delta = (x - _initialMouseXPos) / Math.Pow(10, _decimalPlacesAfterPoint);
            var num = _initialValue + delta;
            var numStr = string.Format($"{{0:F{_decimalPlacesAfterPoint}}}", num).Replace(",", ".");
            if (Math.Abs(num - _lastNum) > double.Epsilon)
            {
                Select(_manipulatingSelectionStart, _manipulatingSelectionStop - _manipulatingSelectionStart + 1);
                SelectedText = numStr;
            }
            _lastNum = num;

            _manipulatingSelectionStop = _manipulatingSelectionStart + numStr.Length - 1;

            Select(_manipulatingSelectionStart, _manipulatingSelectionStop - _manipulatingSelectionStart + 1);
        }

        private static bool IsNumberSymbol(char c)
        {
            return char.IsDigit(c) || (c == '-') || (c == '.') || (c == ',');
        }
    }
}