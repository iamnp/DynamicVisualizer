using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DynamicVisualizer.Controls
{
    partial class ScalarExpressionItem
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(5, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(60, 13);
            this.textBox1.TabIndex = 0;
            this.textBox1.MouseEnter += new System.EventHandler(this.ScalarExpressionItem_MouseEnter);
            this.textBox1.MouseLeave += new System.EventHandler(this.ScalarExpressionItem_MouseLeave);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(77, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(120, 13);
            this.textBox2.TabIndex = 1;
            this.textBox2.MouseEnter += new System.EventHandler(this.ScalarExpressionItem_MouseEnter);
            this.textBox2.MouseLeave += new System.EventHandler(this.ScalarExpressionItem_MouseLeave);
            // 
            // ScalarExpressionItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "ScalarExpressionItem";
            this.Size = new System.Drawing.Size(200, 20);
            this.MouseEnter += new System.EventHandler(this.ScalarExpressionItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ScalarExpressionItem_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public TextBox textBox1;
        public TextBox textBox2;
    }
}
