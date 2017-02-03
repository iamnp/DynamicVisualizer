namespace DynamicVisualizer
{
    partial class SetCanvasSizeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(12, 9);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(35, 13);
            this.widthLabel.TabIndex = 0;
            this.widthLabel.Text = "Width";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(12, 37);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(38, 13);
            this.heightLabel.TabIndex = 1;
            this.heightLabel.Text = "Height";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 75);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(96, 75);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // widthNumericUpDown
            // 
            this.widthNumericUpDown.Location = new System.Drawing.Point(86, 7);
            this.widthNumericUpDown.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.widthNumericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.widthNumericUpDown.Name = "widthNumericUpDown";
            this.widthNumericUpDown.Size = new System.Drawing.Size(85, 20);
            this.widthNumericUpDown.TabIndex = 6;
            this.widthNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.widthNumericUpDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown_KeyPress);
            // 
            // heightNumericUpDown
            // 
            this.heightNumericUpDown.Location = new System.Drawing.Point(86, 35);
            this.heightNumericUpDown.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.heightNumericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.heightNumericUpDown.Name = "heightNumericUpDown";
            this.heightNumericUpDown.Size = new System.Drawing.Size(85, 20);
            this.heightNumericUpDown.TabIndex = 7;
            this.heightNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.heightNumericUpDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown_KeyPress);
            // 
            // SetCanvasSizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(183, 108);
            this.ControlBox = false;
            this.Controls.Add(this.heightNumericUpDown);
            this.Controls.Add(this.widthNumericUpDown);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SetCanvasSizeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter canvas size";
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.NumericUpDown widthNumericUpDown;
        private System.Windows.Forms.NumericUpDown heightNumericUpDown;
    }
}