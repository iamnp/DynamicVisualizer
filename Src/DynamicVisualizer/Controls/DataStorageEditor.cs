using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DynamicVisualizer.Controls
{
    internal class DataStorageEditor : Panel
    {
        private readonly List<DataStorageItem> _items = new List<DataStorageItem>();

        public DataStorageEditor()
        {
            AddDummyItem();
        }

        private void AddDummyItem()
        {
            var item = new DataStorageItem(true) {Location = new Point(0, _items.Count*DataStorageItem.ItemHeight)};
            item.textBox1.KeyPress += DummyItemNameKeyPress;
            _items.Add(item);
            Controls.Add(item);
        }

        private void MakeNotDummy(DataStorageItem di)
        {
            di.textBox1.KeyPress -= DummyItemNameKeyPress;
            di.MakeNotDummy();
            AddDummyItem();
        }

        private void DummyItemNameKeyPress(object sender, KeyPressEventArgs e)
        {
            var di = _items[_items.Count - 1];
            if ((e.KeyChar == (char) Keys.Return) && !string.IsNullOrWhiteSpace(di.textBox1.Text))
                MakeNotDummy(di);
        }
    }
}