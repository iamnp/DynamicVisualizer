using System;
using System.Windows.Forms;

namespace DynamicVisualizer
{
    public partial class SetCanvasSizeForm : Form
    {
        public SetCanvasSizeForm()
        {
            InitializeComponent();
        }

        public int InputWidth => (int) widthNumericUpDown.Value;
        public int InputHeight => (int) heightNumericUpDown.Value;

        public void SetInitialVales(int width, int height)
        {
            widthNumericUpDown.Value = width;
            heightNumericUpDown.Value = height;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void numericUpDown_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int) Keys.Enter)
            {
                okButton.PerformClick();
            }
        }
    }
}