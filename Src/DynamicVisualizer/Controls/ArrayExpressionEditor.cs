using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DynamicVisualizer.Controls
{
    internal class ArrayExpressionEditor : Panel
    {
        public static int Len = -1;
        public static readonly List<ArrayExpressionItem> Items = new List<ArrayExpressionItem>();

        public ArrayExpressionEditor()
        {
            AddDummyItem();
            Len = 5;
            Items[0].textBox1.Text = "item";
            MakeNotDummy(Items[0]);
            Items[0].textBox2.Text = "1; 2; 3; 4; 5";
        }

        public static void LenChanged(ArrayExpressionItem by)
        {
            for (var i = 0; i < Items.Count - 1; ++i)
            {
                if (Items[i] != by)
                {
                    Items[i].ApplyNewLen();
                }
            }
        }

        private void AddDummyItem()
        {
            var item = new ArrayExpressionItem
            {
                Location = new Point(0, Items.Count * ArrayExpressionItem.ItemHeight),
                AllowDrop = true,
                Width = Width
            };
            item.DragEnter += OnDummyItemDragEnter;
            item.DragDrop += OnDummyItemDragDrop;
            item.textBox1.KeyPress += DummyItemNameKeyPress;
            Items.Add(item);
            item.Anchor |= AnchorStyles.Right;
            Controls.Add(item);
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

        private void OnDummyItemDragDrop(object sender, DragEventArgs e)
        {
            var file = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
            var data = File.ReadAllLines(file)[0];
            var item = (ArrayExpressionItem) sender;
            var name = new FileInfo(file).Name;
            if (!item.SetDataFromFile(data, name.Substring(0, name.Length - 4)))
            {
                return;
            }
            item.AllowDrop = false;
            item.DragEnter -= OnDummyItemDragEnter;
            item.DragDrop -= OnDummyItemDragDrop;
            MakeNotDummy(item);
        }

        private void MakeNotDummy(ArrayExpressionItem di)
        {
            di.textBox1.KeyPress -= DummyItemNameKeyPress;
            di.MakeNotDummy();
            AddDummyItem();
        }

        private void DummyItemNameKeyPress(object sender, KeyPressEventArgs e)
        {
            var di = Items[Items.Count - 1];
            if ((e.KeyChar == (char) Keys.Return) && !string.IsNullOrWhiteSpace(di.textBox1.Text))
            {
                MakeNotDummy(di);
                var s = "";
                for (var i = 0; i < Len; ++i)
                {
                    s += "0";
                    if (i != Len - 1)
                    {
                        s += "; ";
                    }
                }
                di.textBox2.Text = s;
            }
        }
    }
}