using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DynamicVisualizer.Controls
{
    internal class ScalarExpressionEditor : Panel
    {
        private readonly List<ScalarExpressionItem> _items = new List<ScalarExpressionItem>();

        public ScalarExpressionEditor()
        {
            AddDummyItem();
        }

        private void AddDummyItem()
        {
            var item = new ScalarExpressionItem(true)
            {
                Location = new Point(0, _items.Count * ArrayExpressionItem.ItemHeight),
                Width = Width
            };
            item.textBox1.KeyPress += DummyItemNameKeyPress;
            _items.Add(item);
            item.Anchor |= AnchorStyles.Right;
            Controls.Add(item);
        }

        private void MakeNotDummy(ScalarExpressionItem di)
        {
            di.textBox1.KeyPress -= DummyItemNameKeyPress;
            di.MakeNotDummy();
            AddDummyItem();
        }

        private void DummyItemNameKeyPress(object sender, KeyPressEventArgs e)
        {
            var di = _items[_items.Count - 1];
            if ((e.KeyChar == (char) Keys.Return) && !string.IsNullOrWhiteSpace(di.textBox1.Text))
            {
                MakeNotDummy(di);
            }
        }
    }
}