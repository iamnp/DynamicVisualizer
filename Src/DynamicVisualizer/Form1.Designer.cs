using DynamicVisualizer.Controls;

namespace DynamicVisualizer
{
    partial class Form1
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
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.drawLabel = new System.Windows.Forms.Label();
            this.rectLabel = new System.Windows.Forms.Label();
            this.circleLabel = new System.Windows.Forms.Label();
            this.transformLabel = new System.Windows.Forms.Label();
            this.moveLabel = new System.Windows.Forms.Label();
            this.scaleLabel = new System.Windows.Forms.Label();
            this.guideLabel = new System.Windows.Forms.Label();
            this.loopLabel = new System.Windows.Forms.Label();
            this.modifiersLabel = new System.Windows.Forms.Label();
            this.resizeLabel = new System.Windows.Forms.Label();
            this.lineLabel = new System.Windows.Forms.Label();
            this.rotateLabel = new System.Windows.Forms.Label();
            this.addStepLabel = new System.Windows.Forms.Label();
            this.addStepAfterLabel = new System.Windows.Forms.Label();
            this.addStepLoopedLabel = new System.Windows.Forms.Label();
            this.arrayExpressionEditor1 = new DynamicVisualizer.Controls.ArrayExpressionEditor();
            this._scalarExpressionEditor1 = new DynamicVisualizer.Controls.ScalarExpressionEditor();
            this.stepEditor1 = new DynamicVisualizer.Controls.StepEditor();
            this._stepListControl1 = new DynamicVisualizer.Controls.StepListControl();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(202, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(1000, 700);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // drawLabel
            // 
            this.drawLabel.AutoSize = true;
            this.drawLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.drawLabel.Location = new System.Drawing.Point(1208, 190);
            this.drawLabel.Name = "drawLabel";
            this.drawLabel.Size = new System.Drawing.Size(50, 16);
            this.drawLabel.TabIndex = 6;
            this.drawLabel.Text = "DRAW";
            // 
            // rectLabel
            // 
            this.rectLabel.AutoSize = true;
            this.rectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rectLabel.Location = new System.Drawing.Point(1219, 215);
            this.rectLabel.Name = "rectLabel";
            this.rectLabel.Size = new System.Drawing.Size(36, 16);
            this.rectLabel.TabIndex = 7;
            this.rectLabel.Text = "Rect";
            this.rectLabel.Click += new System.EventHandler(this.rectLabel_Click);
            // 
            // circleLabel
            // 
            this.circleLabel.AutoSize = true;
            this.circleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.circleLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.circleLabel.Location = new System.Drawing.Point(1219, 240);
            this.circleLabel.Name = "circleLabel";
            this.circleLabel.Size = new System.Drawing.Size(42, 16);
            this.circleLabel.TabIndex = 8;
            this.circleLabel.Text = "Circle";
            this.circleLabel.Click += new System.EventHandler(this.circleLabel_Click);
            // 
            // transformLabel
            // 
            this.transformLabel.AutoSize = true;
            this.transformLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transformLabel.Location = new System.Drawing.Point(1208, 305);
            this.transformLabel.Name = "transformLabel";
            this.transformLabel.Size = new System.Drawing.Size(94, 16);
            this.transformLabel.TabIndex = 9;
            this.transformLabel.Text = "TRANSFORM";
            // 
            // moveLabel
            // 
            this.moveLabel.AutoSize = true;
            this.moveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.moveLabel.Location = new System.Drawing.Point(1219, 330);
            this.moveLabel.Name = "moveLabel";
            this.moveLabel.Size = new System.Drawing.Size(42, 16);
            this.moveLabel.TabIndex = 10;
            this.moveLabel.Text = "Move";
            this.moveLabel.Click += new System.EventHandler(this.moveLabel_Click);
            // 
            // scaleLabel
            // 
            this.scaleLabel.AutoSize = true;
            this.scaleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scaleLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.scaleLabel.Location = new System.Drawing.Point(1219, 355);
            this.scaleLabel.Name = "scaleLabel";
            this.scaleLabel.Size = new System.Drawing.Size(43, 16);
            this.scaleLabel.TabIndex = 11;
            this.scaleLabel.Text = "Scale";
            this.scaleLabel.Click += new System.EventHandler(this.scaleLabel_Click);
            // 
            // guideLabel
            // 
            this.guideLabel.AutoSize = true;
            this.guideLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.guideLabel.Location = new System.Drawing.Point(1219, 470);
            this.guideLabel.Name = "guideLabel";
            this.guideLabel.Size = new System.Drawing.Size(44, 16);
            this.guideLabel.TabIndex = 12;
            this.guideLabel.Text = "Guide";
            this.guideLabel.Visible = false;
            this.guideLabel.Click += new System.EventHandler(this.guideLabel_Click);
            // 
            // loopLabel
            // 
            this.loopLabel.AutoSize = true;
            this.loopLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loopLabel.Location = new System.Drawing.Point(1219, 495);
            this.loopLabel.Name = "loopLabel";
            this.loopLabel.Size = new System.Drawing.Size(39, 16);
            this.loopLabel.TabIndex = 14;
            this.loopLabel.Text = "Loop";
            this.loopLabel.Click += new System.EventHandler(this.loopLabel_Click);
            // 
            // modifiersLabel
            // 
            this.modifiersLabel.AutoSize = true;
            this.modifiersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.modifiersLabel.Location = new System.Drawing.Point(1208, 445);
            this.modifiersLabel.Name = "modifiersLabel";
            this.modifiersLabel.Size = new System.Drawing.Size(81, 16);
            this.modifiersLabel.TabIndex = 17;
            this.modifiersLabel.Text = "MODIFIERS";
            // 
            // resizeLabel
            // 
            this.resizeLabel.AutoSize = true;
            this.resizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resizeLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.resizeLabel.Location = new System.Drawing.Point(1219, 380);
            this.resizeLabel.Name = "resizeLabel";
            this.resizeLabel.Size = new System.Drawing.Size(50, 16);
            this.resizeLabel.TabIndex = 18;
            this.resizeLabel.Text = "Resize";
            this.resizeLabel.Click += new System.EventHandler(this.resizeLabel_Click);
            // 
            // lineLabel
            // 
            this.lineLabel.AutoSize = true;
            this.lineLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lineLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lineLabel.Location = new System.Drawing.Point(1219, 265);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(33, 16);
            this.lineLabel.TabIndex = 19;
            this.lineLabel.Text = "Line";
            this.lineLabel.Click += new System.EventHandler(this.lineLabel_Click);
            // 
            // rotateLabel
            // 
            this.rotateLabel.AutoSize = true;
            this.rotateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rotateLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.rotateLabel.Location = new System.Drawing.Point(1219, 405);
            this.rotateLabel.Name = "rotateLabel";
            this.rotateLabel.Size = new System.Drawing.Size(48, 16);
            this.rotateLabel.TabIndex = 20;
            this.rotateLabel.Text = "Rotate";
            this.rotateLabel.Click += new System.EventHandler(this.rotateLabel_Click);
            // 
            // addStepLabel
            // 
            this.addStepLabel.AutoSize = true;
            this.addStepLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addStepLabel.Location = new System.Drawing.Point(1208, 535);
            this.addStepLabel.Name = "addStepLabel";
            this.addStepLabel.Size = new System.Drawing.Size(76, 16);
            this.addStepLabel.TabIndex = 21;
            this.addStepLabel.Text = "ADD STEP";
            // 
            // addStepAfterLabel
            // 
            this.addStepAfterLabel.AutoSize = true;
            this.addStepAfterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addStepAfterLabel.Location = new System.Drawing.Point(1219, 560);
            this.addStepAfterLabel.Name = "addStepAfterLabel";
            this.addStepAfterLabel.Size = new System.Drawing.Size(78, 16);
            this.addStepAfterLabel.TabIndex = 22;
            this.addStepAfterLabel.Text = "After current";
            this.addStepAfterLabel.Click += new System.EventHandler(this.addStepAfterLabel_Click);
            // 
            // addStepLoopedLabel
            // 
            this.addStepLoopedLabel.AutoSize = true;
            this.addStepLoopedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addStepLoopedLabel.Location = new System.Drawing.Point(1219, 585);
            this.addStepLoopedLabel.Name = "addStepLoopedLabel";
            this.addStepLoopedLabel.Size = new System.Drawing.Size(55, 16);
            this.addStepLoopedLabel.TabIndex = 23;
            this.addStepLoopedLabel.Text = "Looped";
            this.addStepLoopedLabel.Click += new System.EventHandler(this.addStepLoopedLabel_Click);
            // 
            // arrayExpressionEditor1
            // 
            this.arrayExpressionEditor1.AllowDrop = true;
            this.arrayExpressionEditor1.Location = new System.Drawing.Point(2, 190);
            this.arrayExpressionEditor1.Name = "arrayExpressionEditor1";
            this.arrayExpressionEditor1.Size = new System.Drawing.Size(200, 164);
            this.arrayExpressionEditor1.TabIndex = 16;
            // 
            // _scalarExpressionEditor1
            // 
            this._scalarExpressionEditor1.Location = new System.Drawing.Point(2, 2);
            this._scalarExpressionEditor1.Name = "_scalarExpressionEditor1";
            this._scalarExpressionEditor1.Size = new System.Drawing.Size(200, 181);
            this._scalarExpressionEditor1.TabIndex = 15;
            // 
            // stepEditor1
            // 
            this.stepEditor1.Location = new System.Drawing.Point(1204, 2);
            this.stepEditor1.Name = "stepEditor1";
            this.stepEditor1.Size = new System.Drawing.Size(150, 156);
            this.stepEditor1.TabIndex = 13;
            // 
            // _stepListControl1
            // 
            this._stepListControl1.AutoScroll = true;
            this._stepListControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._stepListControl1.Location = new System.Drawing.Point(2, 360);
            this._stepListControl1.Name = "_stepListControl1";
            this._stepListControl1.Size = new System.Drawing.Size(200, 342);
            this._stepListControl1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 700);
            this.Controls.Add(this.addStepLoopedLabel);
            this.Controls.Add(this.addStepAfterLabel);
            this.Controls.Add(this.addStepLabel);
            this.Controls.Add(this.rotateLabel);
            this.Controls.Add(this.lineLabel);
            this.Controls.Add(this.resizeLabel);
            this.Controls.Add(this.modifiersLabel);
            this.Controls.Add(this.arrayExpressionEditor1);
            this.Controls.Add(this._scalarExpressionEditor1);
            this.Controls.Add(this.loopLabel);
            this.Controls.Add(this.stepEditor1);
            this.Controls.Add(this.guideLabel);
            this.Controls.Add(this.scaleLabel);
            this.Controls.Add(this.moveLabel);
            this.Controls.Add(this.transformLabel);
            this.Controls.Add(this.circleLabel);
            this.Controls.Add(this.rectLabel);
            this.Controls.Add(this.drawLabel);
            this.Controls.Add(this._stepListControl1);
            this.Controls.Add(this.elementHost1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dynamic Visualizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private StepListControl _stepListControl1;
        private System.Windows.Forms.Label drawLabel;
        private System.Windows.Forms.Label rectLabel;
        private System.Windows.Forms.Label circleLabel;
        private System.Windows.Forms.Label transformLabel;
        private System.Windows.Forms.Label moveLabel;
        private System.Windows.Forms.Label scaleLabel;
        private System.Windows.Forms.Label guideLabel;
        private StepEditor stepEditor1;
        private System.Windows.Forms.Label loopLabel;
        private ScalarExpressionEditor _scalarExpressionEditor1;
        private ArrayExpressionEditor arrayExpressionEditor1;
        private System.Windows.Forms.Label modifiersLabel;
        private System.Windows.Forms.Label resizeLabel;
        private System.Windows.Forms.Label lineLabel;
        private System.Windows.Forms.Label rotateLabel;
        private System.Windows.Forms.Label addStepLabel;
        private System.Windows.Forms.Label addStepAfterLabel;
        private System.Windows.Forms.Label addStepLoopedLabel;
    }
}

