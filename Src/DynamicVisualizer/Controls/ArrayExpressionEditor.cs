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
        }

        private void AddDummyItem()
        {
            var item = new ArrayExpressionItem
            {
                Location = new Point(0, _items.Count * ArrayExpressionItem.ItemHeight),
                AllowDrop = true
            };
            item.DragEnter += OnDummyItemDragEnter;
            item.DragDrop += OnDummyItemDragDrop;
            item.textBox1.KeyPress += DummyItemNameKeyPress;
            _items.Add(item);
            Controls.Add(item);
        }

        private void OnDummyItemDragDrop(object sender, DragEventArgs e)
        {
            var file = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
            var data = File.ReadAllLines(file)[0];
            var item = (ArrayExpressionItem) sender;
            item.AllowDrop = false;
            item.DragEnter -= OnDummyItemDragEnter;
            item.DragDrop -= OnDummyItemDragDrop;
            MakeNotDummy(item);
            var name = new FileInfo(file).Name;
            item.SetDataFromFile(data, name.Substring(0, name.Length - 4));
        }

        private void OnDummyItemDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                if ((files.Length == 1) && files[0].EndsWith(".csv"))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
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
            {
                MakeNotDummy(di);
            }
        }
    }
}