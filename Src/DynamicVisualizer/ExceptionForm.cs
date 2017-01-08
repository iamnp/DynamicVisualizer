using System;
using System.Windows.Forms;

namespace DynamicVisualizer
{
    public partial class ExceptionForm : Form
    {
        public static bool Showing;

        public ExceptionForm(Exception ex)
        {
            InitializeComponent();
            Showing = true;
            if (ex != null)
            {
                richTextBox1.Text = ex.Message + "\n" + ex.StackTrace;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Height == 400)
            {
                Height = 180;
            }
            else
            {
                Height = 400;
            }
        }
    }
}