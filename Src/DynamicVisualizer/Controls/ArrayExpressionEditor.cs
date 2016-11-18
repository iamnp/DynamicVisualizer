using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DynamicVisualizer.Controls
{
    internal class ArrayExpressionEditor : Panel
    {
        public static int Len = -1;
        private readonly List<ArrayExpressionItem> _items = new List<ArrayExpressionItem>();

        public ArrayExpressionEditor()
        {
            AddDummyItem();
            AllowDrop = true;
            DragEnter += OnDragEnter;
            DragDrop += OnDragDrop;
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && (Controls.Count > 1))
            {
                var files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                if ((files.Length == 1) && files[0].EndsWith(".csv"))
                    e.Effect = DragDropEffects.Copy;
            }
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            var file = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
            var line = File.ReadAllLines(file)[0];
            ((ArrayExpressionItem) Controls[Controls.Count - 2]).SetDataFromFile(line);
        }

        private void AddDummyItem()
        {
            var item = new ArrayExpressionItem(true)
            {
                Location = new Point(0, _items.Count*ArrayExpressionItem.ItemHeight)
            };
            item.textBox1.KeyPress += DummyItemNameKeyPress;
            _items.Add(item);
            Controls.Add(item);
        }

        private void MakeNotDummy(ArrayExpressionItem di)
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