namespace DynamicVisualizer.Controls
{
    partial class StepItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clickThroughLabel1 = new DynamicVisualizer.Controls.ClickThroughLabel();
            this.SuspendLayout();
            // 
            // clickThroughLabel1
            // 
            this.clickThroughLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clickThroughLabel1.Location = new System.Drawing.Point(4, 4);
            this.clickThroughLabel1.Name = "clickThroughLabel1";
            this.clickThroughLabel1.Size = new System.Drawing.Size(123, 46);
            this.clickThroughLabel1.TabIndex = 0;
            this.clickThroughLabel1.Text = "clickThroughLabel1";
            // 
            // StepItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clickThroughLabel1);
            this.Name = "StepItem";
            this.Size = new System.Drawing.Size(130, 50);
            this.ResumeLayout(false);

        }

        #endregion

        private ClickThroughLabel clickThroughLabel1;
    }
}
