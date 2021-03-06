﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using DynamicVisualizer.Expressions;

namespace DynamicVisualizer.Figures
{
    public class FigureText
    {
        private readonly ScalarExpression _size;
        private readonly ScalarExpression _text;
        public FormattedText FormattedText;
        public string StringExpr;

        public FigureText(string text, string size, int index = 0)
        {
            _text = new ScalarExpression("a", "a", text, index, true);
            _text.ValueChanged += ExprValueChanged;
            _size = new ScalarExpression("a", "a", size, index, true);
            _size.ValueChanged += ExprValueChanged;
            FormattedText = GetText();
            StringExpr = text + ";" + size;
        }

        public FigureText(double text, double size, int index = 0)
            : this(text.Str(), size.Str(), index)
        {
        }

        private void ExprValueChanged(object sender, EventArgs eventArgs)
        {
            FormattedText = GetText();
            StringExpr = _text.ExprString + ";" + _size.ExprString;
        }

        private FormattedText GetText()
        {
            if (_text.CachedValue.Empty || _size.CachedValue.Empty || !_size.CachedValue.IsDouble ||
                (_size.CachedValue.AsDouble < 0.0))
            {
                return new FormattedText(
                    "",
                    CultureInfo.CurrentUICulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Verdana"),
                    14,
                    Drawer.BlackBrush)
                {
                    TextAlignment = TextAlignment.Center,
                    Trimming = TextTrimming.None
                };
            }

            return new FormattedText(
                _text.CachedValue.Str,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                _size.CachedValue.AsDouble,
                Drawer.BlackBrush)
            {
                TextAlignment = TextAlignment.Center,
                Trimming = TextTrimming.None
            };
        }

        public void Parse(string s)
        {
            StringExpr = s;
            var p = s.Split(';');
            if (p.Length != 2)
            {
                return;
            }

            _text.SetRawExpression(p[0]);
            _size.SetRawExpression(p[1]);
            FormattedText = GetText();
        }

        public void SetIndex(int index)
        {
            _text.IndexInArray = index;
            _size.IndexInArray = index;

            _text.Recalculate();
            _size.Recalculate();
        }

        public FigureText Copy()
        {
            return new FigureText(_text.ExprString, _size.ExprString, _text.IndexInArray);
        }
    }
}