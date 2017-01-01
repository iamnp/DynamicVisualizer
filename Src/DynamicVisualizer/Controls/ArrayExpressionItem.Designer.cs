using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DynamicVisualizer.Controls
{
    partial class ArrayExpressionItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new Point(3, 1);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(60, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new Point(77, 1);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(120, 20);
            this.textBox2.TabIndex = 1;
            // 
            // ArrayExpressionItem
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "ArrayExpressionItem";
            this.Size = new Size(200, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public TextBox textBox1;
        public TextBox textBox2;
    }
}
