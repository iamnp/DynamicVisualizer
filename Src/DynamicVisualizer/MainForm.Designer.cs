using DynamicVisualizer.Controls;

namespace DynamicVisualizer
{
    partial class MainForm
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
            this.textLabel = new System.Windows.Forms.Label();
            this.rLabel = new System.Windows.Forms.Label();
            this.cLabel = new System.Windows.Forms.Label();
            this.lLabel = new System.Windows.Forms.Label();
            this.tLabel = new System.Windows.Forms.Label();
            this.mLabel = new System.Windows.Forms.Label();
            this.sLabel = new System.Windows.Forms.Label();
            this.eLabel = new System.Windows.Forms.Label();
            this.oLabel = new System.Windows.Forms.Label();
            this.gLabel = new System.Windows.Forms.Label();
            this.pLabel = new System.Windows.Forms.Label();
            this.exportButton = new System.Windows.Forms.Button();
            this.arrayExpressionEditor1 = new DynamicVisualizer.Controls.ArrayExpressionEditor();
            this._scalarExpressionEditor1 = new DynamicVisualizer.Controls.ScalarExpressionEditor();
            this.stepEditor1 = new DynamicVisualizer.Controls.StepEditor();
            this._stepListControl1 = new DynamicVisualizer.Controls.StepListControl();
            this.markAsFinalLabel = new System.Windows.Forms.Label();
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
            this.drawLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.drawLabel.Location = new System.Drawing.Point(1255, 190);
            this.drawLabel.Name = "drawLabel";
            this.drawLabel.Size = new System.Drawing.Size(70, 20);
            this.drawLabel.TabIndex = 6;
            this.drawLabel.Text = "DRAW";
            this.drawLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rectLabel
            // 
            this.rectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rectLabel.Location = new System.Drawing.Point(1255, 210);
            this.rectLabel.Name = "rectLabel";
            this.rectLabel.Size = new System.Drawing.Size(70, 20);
            this.rectLabel.TabIndex = 7;
            this.rectLabel.Text = "Rect";
            this.rectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rectLabel.Click += new System.EventHandler(this.rectLabel_Click);
            // 
            // circleLabel
            // 
            this.circleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.circleLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.circleLabel.Location = new System.Drawing.Point(1255, 235);
            this.circleLabel.Name = "circleLabel";
            this.circleLabel.Size = new System.Drawing.Size(70, 20);
            this.circleLabel.TabIndex = 8;
            this.circleLabel.Text = "Circle";
            this.circleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.circleLabel.Click += new System.EventHandler(this.circleLabel_Click);
            // 
            // transformLabel
            // 
            this.transformLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transformLabel.Location = new System.Drawing.Point(1225, 325);
            this.transformLabel.Name = "transformLabel";
            this.transformLabel.Size = new System.Drawing.Size(100, 20);
            this.transformLabel.TabIndex = 9;
            this.transformLabel.Text = "TRANSFORM";
            this.transformLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // moveLabel
            // 
            this.moveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.moveLabel.Location = new System.Drawing.Point(1255, 350);
            this.moveLabel.Name = "moveLabel";
            this.moveLabel.Size = new System.Drawing.Size(70, 20);
            this.moveLabel.TabIndex = 10;
            this.moveLabel.Text = "Move";
            this.moveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.moveLabel.Click += new System.EventHandler(this.moveLabel_Click);
            // 
            // scaleLabel
            // 
            this.scaleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scaleLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.scaleLabel.Location = new System.Drawing.Point(1255, 375);
            this.scaleLabel.Name = "scaleLabel";
            this.scaleLabel.Size = new System.Drawing.Size(70, 20);
            this.scaleLabel.TabIndex = 11;
            this.scaleLabel.Text = "Scale";
            this.scaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.scaleLabel.Click += new System.EventHandler(this.scaleLabel_Click);
            // 
            // guideLabel
            // 
            this.guideLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.guideLabel.Location = new System.Drawing.Point(1255, 490);
            this.guideLabel.Name = "guideLabel";
            this.guideLabel.Size = new System.Drawing.Size(70, 20);
            this.guideLabel.TabIndex = 12;
            this.guideLabel.Text = "Guide";
            this.guideLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.guideLabel.Visible = false;
            this.guideLabel.Click += new System.EventHandler(this.guideLabel_Click);
            // 
            // loopLabel
            // 
            this.loopLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loopLabel.Location = new System.Drawing.Point(1255, 515);
            this.loopLabel.Name = "loopLabel";
            this.loopLabel.Size = new System.Drawing.Size(70, 20);
            this.loopLabel.TabIndex = 14;
            this.loopLabel.Text = "Loop";
            this.loopLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.loopLabel.Click += new System.EventHandler(this.loopLabel_Click);
            // 
            // modifiersLabel
            // 
            this.modifiersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.modifiersLabel.Location = new System.Drawing.Point(1225, 465);
            this.modifiersLabel.Name = "modifiersLabel";
            this.modifiersLabel.Size = new System.Drawing.Size(100, 20);
            this.modifiersLabel.TabIndex = 17;
            this.modifiersLabel.Text = "MODIFIERS";
            this.modifiersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // resizeLabel
            // 
            this.resizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resizeLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.resizeLabel.Location = new System.Drawing.Point(1255, 400);
            this.resizeLabel.Name = "resizeLabel";
            this.resizeLabel.Size = new System.Drawing.Size(70, 20);
            this.resizeLabel.TabIndex = 18;
            this.resizeLabel.Text = "Resize";
            this.resizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.resizeLabel.Click += new System.EventHandler(this.resizeLabel_Click);
            // 
            // lineLabel
            // 
            this.lineLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lineLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lineLabel.Location = new System.Drawing.Point(1255, 260);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(70, 20);
            this.lineLabel.TabIndex = 19;
            this.lineLabel.Text = "Line";
            this.lineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lineLabel.Click += new System.EventHandler(this.lineLabel_Click);
            // 
            // rotateLabel
            // 
            this.rotateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rotateLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.rotateLabel.Location = new System.Drawing.Point(1255, 425);
            this.rotateLabel.Name = "rotateLabel";
            this.rotateLabel.Size = new System.Drawing.Size(70, 20);
            this.rotateLabel.TabIndex = 20;
            this.rotateLabel.Text = "Rotate";
            this.rotateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rotateLabel.Click += new System.EventHandler(this.rotateLabel_Click);
            // 
            // addStepLabel
            // 
            this.addStepLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addStepLabel.Location = new System.Drawing.Point(1225, 555);
            this.addStepLabel.Name = "addStepLabel";
            this.addStepLabel.Size = new System.Drawing.Size(100, 20);
            this.addStepLabel.TabIndex = 21;
            this.addStepLabel.Text = "STEP";
            this.addStepLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // addStepAfterLabel
            // 
            this.addStepAfterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addStepAfterLabel.Location = new System.Drawing.Point(1225, 580);
            this.addStepAfterLabel.Name = "addStepAfterLabel";
            this.addStepAfterLabel.Size = new System.Drawing.Size(100, 20);
            this.addStepAfterLabel.TabIndex = 22;
            this.addStepAfterLabel.Text = "Add after";
            this.addStepAfterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addStepAfterLabel.Click += new System.EventHandler(this.addStepAfterLabel_Click);
            // 
            // addStepLoopedLabel
            // 
            this.addStepLoopedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addStepLoopedLabel.Location = new System.Drawing.Point(1225, 605);
            this.addStepLoopedLabel.Name = "addStepLoopedLabel";
            this.addStepLoopedLabel.Size = new System.Drawing.Size(100, 20);
            this.addStepLoopedLabel.TabIndex = 23;
            this.addStepLoopedLabel.Text = "Looped";
            this.addStepLoopedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addStepLoopedLabel.Click += new System.EventHandler(this.addStepLoopedLabel_Click);
            // 
            // textLabel
            // 
            this.textLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textLabel.Location = new System.Drawing.Point(1255, 285);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(70, 20);
            this.textLabel.TabIndex = 24;
            this.textLabel.Text = "Text";
            this.textLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.textLabel.Click += new System.EventHandler(this.textLabel_Click);
            // 
            // rLabel
            // 
            this.rLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rLabel.Location = new System.Drawing.Point(1325, 210);
            this.rLabel.Name = "rLabel";
            this.rLabel.Size = new System.Drawing.Size(20, 20);
            this.rLabel.TabIndex = 25;
            this.rLabel.Text = "r";
            this.rLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rLabel.Click += new System.EventHandler(this.rectLabel_Click);
            // 
            // cLabel
            // 
            this.cLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cLabel.Location = new System.Drawing.Point(1325, 235);
            this.cLabel.Name = "cLabel";
            this.cLabel.Size = new System.Drawing.Size(20, 20);
            this.cLabel.TabIndex = 26;
            this.cLabel.Text = "c";
            this.cLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cLabel.Click += new System.EventHandler(this.circleLabel_Click);
            // 
            // lLabel
            // 
            this.lLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lLabel.Location = new System.Drawing.Point(1325, 260);
            this.lLabel.Name = "lLabel";
            this.lLabel.Size = new System.Drawing.Size(20, 20);
            this.lLabel.TabIndex = 27;
            this.lLabel.Text = "l";
            this.lLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lLabel.Click += new System.EventHandler(this.lineLabel_Click);
            // 
            // tLabel
            // 
            this.tLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tLabel.Location = new System.Drawing.Point(1325, 285);
            this.tLabel.Name = "tLabel";
            this.tLabel.Size = new System.Drawing.Size(20, 20);
            this.tLabel.TabIndex = 28;
            this.tLabel.Text = "t";
            this.tLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tLabel.Click += new System.EventHandler(this.textLabel_Click);
            // 
            // mLabel
            // 
            this.mLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mLabel.Location = new System.Drawing.Point(1325, 350);
            this.mLabel.Name = "mLabel";
            this.mLabel.Size = new System.Drawing.Size(20, 20);
            this.mLabel.TabIndex = 29;
            this.mLabel.Text = "m";
            this.mLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mLabel.Click += new System.EventHandler(this.moveLabel_Click);
            // 
            // sLabel
            // 
            this.sLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sLabel.Location = new System.Drawing.Point(1325, 375);
            this.sLabel.Name = "sLabel";
            this.sLabel.Size = new System.Drawing.Size(20, 20);
            this.sLabel.TabIndex = 30;
            this.sLabel.Text = "s";
            this.sLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sLabel.Click += new System.EventHandler(this.scaleLabel_Click);
            // 
            // eLabel
            // 
            this.eLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.eLabel.Location = new System.Drawing.Point(1325, 400);
            this.eLabel.Name = "eLabel";
            this.eLabel.Size = new System.Drawing.Size(20, 20);
            this.eLabel.TabIndex = 31;
            this.eLabel.Text = "e";
            this.eLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.eLabel.Click += new System.EventHandler(this.resizeLabel_Click);
            // 
            // oLabel
            // 
            this.oLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.oLabel.Location = new System.Drawing.Point(1325, 425);
            this.oLabel.Name = "oLabel";
            this.oLabel.Size = new System.Drawing.Size(20, 20);
            this.oLabel.TabIndex = 32;
            this.oLabel.Text = "o";
            this.oLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.oLabel.Click += new System.EventHandler(this.rotateLabel_Click);
            // 
            // gLabel
            // 
            this.gLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gLabel.Location = new System.Drawing.Point(1325, 490);
            this.gLabel.Name = "gLabel";
            this.gLabel.Size = new System.Drawing.Size(20, 20);
            this.gLabel.TabIndex = 33;
            this.gLabel.Text = "g";
            this.gLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.gLabel.Visible = false;
            this.gLabel.Click += new System.EventHandler(this.guideLabel_Click);
            // 
            // pLabel
            // 
            this.pLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pLabel.Location = new System.Drawing.Point(1325, 515);
            this.pLabel.Name = "pLabel";
            this.pLabel.Size = new System.Drawing.Size(20, 20);
            this.pLabel.TabIndex = 34;
            this.pLabel.Text = "p";
            this.pLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pLabel.Click += new System.EventHandler(this.loopLabel_Click);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(1204, 677);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(150, 23);
            this.exportButton.TabIndex = 35;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
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
            this.stepEditor1.Size = new System.Drawing.Size(150, 160);
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
            // markAsFinalLabel
            // 
            this.markAsFinalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.markAsFinalLabel.Location = new System.Drawing.Point(1225, 630);
            this.markAsFinalLabel.Name = "markAsFinalLabel";
            this.markAsFinalLabel.Size = new System.Drawing.Size(100, 20);
            this.markAsFinalLabel.TabIndex = 36;
            this.markAsFinalLabel.Text = "Mark as final";
            this.markAsFinalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.markAsFinalLabel.Click += new System.EventHandler(this.markAsFinalLabel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 700);
            this.Controls.Add(this.markAsFinalLabel);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.pLabel);
            this.Controls.Add(this.gLabel);
            this.Controls.Add(this.oLabel);
            this.Controls.Add(this.eLabel);
            this.Controls.Add(this.sLabel);
            this.Controls.Add(this.mLabel);
            this.Controls.Add(this.tLabel);
            this.Controls.Add(this.lLabel);
            this.Controls.Add(this.cLabel);
            this.Controls.Add(this.rLabel);
            this.Controls.Add(this.textLabel);
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
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dynamic Visualizer";
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.Label rLabel;
        private System.Windows.Forms.Label cLabel;
        private System.Windows.Forms.Label lLabel;
        private System.Windows.Forms.Label tLabel;
        private System.Windows.Forms.Label mLabel;
        private System.Windows.Forms.Label sLabel;
        private System.Windows.Forms.Label eLabel;
        private System.Windows.Forms.Label oLabel;
        private System.Windows.Forms.Label gLabel;
        private System.Windows.Forms.Label pLabel;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Label markAsFinalLabel;
    }
}

