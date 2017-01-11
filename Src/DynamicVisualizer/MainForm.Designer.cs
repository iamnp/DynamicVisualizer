﻿using DynamicVisualizer.Controls;

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
            this.markAsFinalLabel = new System.Windows.Forms.Label();
            this.straightLabel = new System.Windows.Forms.Label();
            this.hLabel = new System.Windows.Forms.Label();
            this.aLabel = new System.Windows.Forms.Label();
            this.dLabel = new System.Windows.Forms.Label();
            this.fLabel = new System.Windows.Forms.Label();
            this.addStepBeforeLabel = new System.Windows.Forms.Label();
            this.bLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._stepListControl1 = new DynamicVisualizer.Controls.StepListControl();
            this.arrayExpressionEditor1 = new DynamicVisualizer.Controls.ArrayExpressionEditor();
            this._scalarExpressionEditor1 = new DynamicVisualizer.Controls.ScalarExpressionEditor();
            this.stepEditor1 = new DynamicVisualizer.Controls.StepEditor();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(997, 700);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.TabStop = false;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // drawLabel
            // 
            this.drawLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.drawLabel.Location = new System.Drawing.Point(1255, 185);
            this.drawLabel.Name = "drawLabel";
            this.drawLabel.Size = new System.Drawing.Size(70, 20);
            this.drawLabel.TabIndex = 6;
            this.drawLabel.Text = "DRAW";
            this.drawLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rectLabel
            // 
            this.rectLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rectLabel.Location = new System.Drawing.Point(1255, 205);
            this.rectLabel.Name = "rectLabel";
            this.rectLabel.Size = new System.Drawing.Size(70, 20);
            this.rectLabel.TabIndex = 7;
            this.rectLabel.Text = "rect";
            this.rectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rectLabel.Click += new System.EventHandler(this.rectLabel_Click);
            // 
            // circleLabel
            // 
            this.circleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.circleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.circleLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.circleLabel.Location = new System.Drawing.Point(1255, 225);
            this.circleLabel.Name = "circleLabel";
            this.circleLabel.Size = new System.Drawing.Size(70, 20);
            this.circleLabel.TabIndex = 8;
            this.circleLabel.Text = "circle";
            this.circleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.circleLabel.Click += new System.EventHandler(this.circleLabel_Click);
            // 
            // transformLabel
            // 
            this.transformLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.transformLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transformLabel.Location = new System.Drawing.Point(1225, 305);
            this.transformLabel.Name = "transformLabel";
            this.transformLabel.Size = new System.Drawing.Size(100, 20);
            this.transformLabel.TabIndex = 9;
            this.transformLabel.Text = "TRANSFORM";
            this.transformLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // moveLabel
            // 
            this.moveLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.moveLabel.Location = new System.Drawing.Point(1255, 325);
            this.moveLabel.Name = "moveLabel";
            this.moveLabel.Size = new System.Drawing.Size(70, 20);
            this.moveLabel.TabIndex = 10;
            this.moveLabel.Text = "move";
            this.moveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.moveLabel.Click += new System.EventHandler(this.moveLabel_Click);
            // 
            // scaleLabel
            // 
            this.scaleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scaleLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.scaleLabel.Location = new System.Drawing.Point(1255, 345);
            this.scaleLabel.Name = "scaleLabel";
            this.scaleLabel.Size = new System.Drawing.Size(70, 20);
            this.scaleLabel.TabIndex = 11;
            this.scaleLabel.Text = "scale";
            this.scaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.scaleLabel.Click += new System.EventHandler(this.scaleLabel_Click);
            // 
            // guideLabel
            // 
            this.guideLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guideLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.guideLabel.Location = new System.Drawing.Point(1255, 465);
            this.guideLabel.Name = "guideLabel";
            this.guideLabel.Size = new System.Drawing.Size(70, 20);
            this.guideLabel.TabIndex = 12;
            this.guideLabel.Text = "guide";
            this.guideLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.guideLabel.Visible = false;
            this.guideLabel.Click += new System.EventHandler(this.guideLabel_Click);
            // 
            // loopLabel
            // 
            this.loopLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loopLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loopLabel.Location = new System.Drawing.Point(1255, 445);
            this.loopLabel.Name = "loopLabel";
            this.loopLabel.Size = new System.Drawing.Size(70, 20);
            this.loopLabel.TabIndex = 14;
            this.loopLabel.Text = "loop";
            this.loopLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.loopLabel.Click += new System.EventHandler(this.loopLabel_Click);
            // 
            // modifiersLabel
            // 
            this.modifiersLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modifiersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.modifiersLabel.Location = new System.Drawing.Point(1225, 425);
            this.modifiersLabel.Name = "modifiersLabel";
            this.modifiersLabel.Size = new System.Drawing.Size(100, 20);
            this.modifiersLabel.TabIndex = 17;
            this.modifiersLabel.Text = "MODIFIERS";
            this.modifiersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // resizeLabel
            // 
            this.resizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resizeLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.resizeLabel.Location = new System.Drawing.Point(1255, 365);
            this.resizeLabel.Name = "resizeLabel";
            this.resizeLabel.Size = new System.Drawing.Size(70, 20);
            this.resizeLabel.TabIndex = 18;
            this.resizeLabel.Text = "resize";
            this.resizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.resizeLabel.Click += new System.EventHandler(this.resizeLabel_Click);
            // 
            // lineLabel
            // 
            this.lineLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lineLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lineLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lineLabel.Location = new System.Drawing.Point(1255, 245);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(70, 20);
            this.lineLabel.TabIndex = 19;
            this.lineLabel.Text = "line";
            this.lineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lineLabel.Click += new System.EventHandler(this.lineLabel_Click);
            // 
            // rotateLabel
            // 
            this.rotateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rotateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rotateLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.rotateLabel.Location = new System.Drawing.Point(1255, 385);
            this.rotateLabel.Name = "rotateLabel";
            this.rotateLabel.Size = new System.Drawing.Size(70, 20);
            this.rotateLabel.TabIndex = 20;
            this.rotateLabel.Text = "rotate";
            this.rotateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rotateLabel.Click += new System.EventHandler(this.rotateLabel_Click);
            // 
            // addStepLabel
            // 
            this.addStepLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addStepLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addStepLabel.Location = new System.Drawing.Point(1225, 525);
            this.addStepLabel.Name = "addStepLabel";
            this.addStepLabel.Size = new System.Drawing.Size(100, 20);
            this.addStepLabel.TabIndex = 21;
            this.addStepLabel.Text = "STEP";
            this.addStepLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // addStepAfterLabel
            // 
            this.addStepAfterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addStepAfterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addStepAfterLabel.Location = new System.Drawing.Point(1225, 565);
            this.addStepAfterLabel.Name = "addStepAfterLabel";
            this.addStepAfterLabel.Size = new System.Drawing.Size(100, 20);
            this.addStepAfterLabel.TabIndex = 22;
            this.addStepAfterLabel.Text = "after";
            this.addStepAfterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addStepAfterLabel.Click += new System.EventHandler(this.addStepAfterLabel_Click);
            // 
            // addStepLoopedLabel
            // 
            this.addStepLoopedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addStepLoopedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addStepLoopedLabel.Location = new System.Drawing.Point(1225, 585);
            this.addStepLoopedLabel.Name = "addStepLoopedLabel";
            this.addStepLoopedLabel.Size = new System.Drawing.Size(100, 20);
            this.addStepLoopedLabel.TabIndex = 23;
            this.addStepLoopedLabel.Text = "looped";
            this.addStepLoopedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addStepLoopedLabel.Click += new System.EventHandler(this.addStepLoopedLabel_Click);
            // 
            // textLabel
            // 
            this.textLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textLabel.Location = new System.Drawing.Point(1255, 265);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(70, 20);
            this.textLabel.TabIndex = 24;
            this.textLabel.Text = "text";
            this.textLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.textLabel.Click += new System.EventHandler(this.textLabel_Click);
            // 
            // rLabel
            // 
            this.rLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rLabel.Location = new System.Drawing.Point(1325, 205);
            this.rLabel.Name = "rLabel";
            this.rLabel.Size = new System.Drawing.Size(20, 20);
            this.rLabel.TabIndex = 25;
            this.rLabel.Text = "r";
            this.rLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rLabel.Click += new System.EventHandler(this.rectLabel_Click);
            // 
            // cLabel
            // 
            this.cLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cLabel.Location = new System.Drawing.Point(1325, 225);
            this.cLabel.Name = "cLabel";
            this.cLabel.Size = new System.Drawing.Size(20, 20);
            this.cLabel.TabIndex = 26;
            this.cLabel.Text = "c";
            this.cLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cLabel.Click += new System.EventHandler(this.circleLabel_Click);
            // 
            // lLabel
            // 
            this.lLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lLabel.Location = new System.Drawing.Point(1325, 245);
            this.lLabel.Name = "lLabel";
            this.lLabel.Size = new System.Drawing.Size(20, 20);
            this.lLabel.TabIndex = 27;
            this.lLabel.Text = "l";
            this.lLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lLabel.Click += new System.EventHandler(this.lineLabel_Click);
            // 
            // tLabel
            // 
            this.tLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tLabel.Location = new System.Drawing.Point(1325, 265);
            this.tLabel.Name = "tLabel";
            this.tLabel.Size = new System.Drawing.Size(20, 20);
            this.tLabel.TabIndex = 28;
            this.tLabel.Text = "t";
            this.tLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tLabel.Click += new System.EventHandler(this.textLabel_Click);
            // 
            // mLabel
            // 
            this.mLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mLabel.Location = new System.Drawing.Point(1325, 325);
            this.mLabel.Name = "mLabel";
            this.mLabel.Size = new System.Drawing.Size(20, 20);
            this.mLabel.TabIndex = 29;
            this.mLabel.Text = "m";
            this.mLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mLabel.Click += new System.EventHandler(this.moveLabel_Click);
            // 
            // sLabel
            // 
            this.sLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sLabel.Location = new System.Drawing.Point(1325, 345);
            this.sLabel.Name = "sLabel";
            this.sLabel.Size = new System.Drawing.Size(20, 20);
            this.sLabel.TabIndex = 30;
            this.sLabel.Text = "s";
            this.sLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sLabel.Click += new System.EventHandler(this.scaleLabel_Click);
            // 
            // eLabel
            // 
            this.eLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.eLabel.Location = new System.Drawing.Point(1325, 365);
            this.eLabel.Name = "eLabel";
            this.eLabel.Size = new System.Drawing.Size(20, 20);
            this.eLabel.TabIndex = 31;
            this.eLabel.Text = "e";
            this.eLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.eLabel.Click += new System.EventHandler(this.resizeLabel_Click);
            // 
            // oLabel
            // 
            this.oLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.oLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.oLabel.Location = new System.Drawing.Point(1325, 385);
            this.oLabel.Name = "oLabel";
            this.oLabel.Size = new System.Drawing.Size(20, 20);
            this.oLabel.TabIndex = 32;
            this.oLabel.Text = "o";
            this.oLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.oLabel.Click += new System.EventHandler(this.rotateLabel_Click);
            // 
            // gLabel
            // 
            this.gLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gLabel.Location = new System.Drawing.Point(1325, 465);
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
            this.pLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pLabel.Location = new System.Drawing.Point(1325, 445);
            this.pLabel.Name = "pLabel";
            this.pLabel.Size = new System.Drawing.Size(20, 20);
            this.pLabel.TabIndex = 34;
            this.pLabel.Text = "p";
            this.pLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pLabel.Click += new System.EventHandler(this.loopLabel_Click);
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(1206, 677);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(148, 23);
            this.exportButton.TabIndex = 35;
            this.exportButton.TabStop = false;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // markAsFinalLabel
            // 
            this.markAsFinalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.markAsFinalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.markAsFinalLabel.Location = new System.Drawing.Point(1225, 605);
            this.markAsFinalLabel.Name = "markAsFinalLabel";
            this.markAsFinalLabel.Size = new System.Drawing.Size(100, 20);
            this.markAsFinalLabel.TabIndex = 36;
            this.markAsFinalLabel.Text = "mark as final";
            this.markAsFinalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.markAsFinalLabel.Click += new System.EventHandler(this.markAsFinalLabel_Click);
            // 
            // straightLabel
            // 
            this.straightLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.straightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.straightLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.straightLabel.Location = new System.Drawing.Point(1255, 485);
            this.straightLabel.Name = "straightLabel";
            this.straightLabel.Size = new System.Drawing.Size(70, 20);
            this.straightLabel.TabIndex = 37;
            this.straightLabel.Text = "straight";
            this.straightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.straightLabel.Visible = false;
            this.straightLabel.Click += new System.EventHandler(this.straightLabel_Click);
            // 
            // hLabel
            // 
            this.hLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hLabel.Location = new System.Drawing.Point(1325, 485);
            this.hLabel.Name = "hLabel";
            this.hLabel.Size = new System.Drawing.Size(20, 20);
            this.hLabel.TabIndex = 38;
            this.hLabel.Text = "h";
            this.hLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.hLabel.Visible = false;
            this.hLabel.Click += new System.EventHandler(this.straightLabel_Click);
            // 
            // aLabel
            // 
            this.aLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.aLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.aLabel.Location = new System.Drawing.Point(1325, 565);
            this.aLabel.Name = "aLabel";
            this.aLabel.Size = new System.Drawing.Size(20, 20);
            this.aLabel.TabIndex = 39;
            this.aLabel.Text = "a";
            this.aLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.aLabel.Click += new System.EventHandler(this.addStepAfterLabel_Click);
            // 
            // dLabel
            // 
            this.dLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dLabel.Location = new System.Drawing.Point(1325, 585);
            this.dLabel.Name = "dLabel";
            this.dLabel.Size = new System.Drawing.Size(20, 20);
            this.dLabel.TabIndex = 40;
            this.dLabel.Text = "d";
            this.dLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dLabel.Click += new System.EventHandler(this.addStepLoopedLabel_Click);
            // 
            // fLabel
            // 
            this.fLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fLabel.Location = new System.Drawing.Point(1325, 605);
            this.fLabel.Name = "fLabel";
            this.fLabel.Size = new System.Drawing.Size(20, 20);
            this.fLabel.TabIndex = 41;
            this.fLabel.Text = "f";
            this.fLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.fLabel.Click += new System.EventHandler(this.markAsFinalLabel_Click);
            // 
            // addStepBeforeLabel
            // 
            this.addStepBeforeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addStepBeforeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addStepBeforeLabel.Location = new System.Drawing.Point(1225, 545);
            this.addStepBeforeLabel.Name = "addStepBeforeLabel";
            this.addStepBeforeLabel.Size = new System.Drawing.Size(100, 20);
            this.addStepBeforeLabel.TabIndex = 43;
            this.addStepBeforeLabel.Text = "before";
            this.addStepBeforeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addStepBeforeLabel.Click += new System.EventHandler(this.addStepBeforeLabel_Click);
            // 
            // bLabel
            // 
            this.bLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bLabel.Location = new System.Drawing.Point(1325, 545);
            this.bLabel.Name = "bLabel";
            this.bLabel.Size = new System.Drawing.Size(20, 20);
            this.bLabel.TabIndex = 44;
            this.bLabel.Text = "b";
            this.bLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bLabel.Click += new System.EventHandler(this.addStepBeforeLabel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this._stepListControl1);
            this.splitContainer1.Panel1.Controls.Add(this.arrayExpressionEditor1);
            this.splitContainer1.Panel1.Controls.Add(this._scalarExpressionEditor1);
            this.splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.elementHost1);
            this.splitContainer1.Panel2MinSize = 820;
            this.splitContainer1.Size = new System.Drawing.Size(1200, 700);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 45;
            // 
            // _stepListControl1
            // 
            this._stepListControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._stepListControl1.AutoScroll = true;
            this._stepListControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._stepListControl1.Location = new System.Drawing.Point(-1, 304);
            this._stepListControl1.Name = "_stepListControl1";
            this._stepListControl1.Size = new System.Drawing.Size(202, 396);
            this._stepListControl1.TabIndex = 5;
            // 
            // arrayExpressionEditor1
            // 
            this.arrayExpressionEditor1.AllowDrop = true;
            this.arrayExpressionEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arrayExpressionEditor1.Location = new System.Drawing.Point(0, 152);
            this.arrayExpressionEditor1.Name = "arrayExpressionEditor1";
            this.arrayExpressionEditor1.Size = new System.Drawing.Size(200, 150);
            this.arrayExpressionEditor1.TabIndex = 16;
            // 
            // _scalarExpressionEditor1
            // 
            this._scalarExpressionEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._scalarExpressionEditor1.Location = new System.Drawing.Point(0, 0);
            this._scalarExpressionEditor1.Name = "_scalarExpressionEditor1";
            this._scalarExpressionEditor1.Size = new System.Drawing.Size(200, 150);
            this._scalarExpressionEditor1.TabIndex = 15;
            // 
            // stepEditor1
            // 
            this.stepEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stepEditor1.Location = new System.Drawing.Point(1204, 2);
            this.stepEditor1.Name = "stepEditor1";
            this.stepEditor1.Size = new System.Drawing.Size(150, 160);
            this.stepEditor1.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 701);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bLabel);
            this.Controls.Add(this.addStepBeforeLabel);
            this.Controls.Add(this.fLabel);
            this.Controls.Add(this.dLabel);
            this.Controls.Add(this.aLabel);
            this.Controls.Add(this.hLabel);
            this.Controls.Add(this.straightLabel);
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
            this.Controls.Add(this.loopLabel);
            this.Controls.Add(this.stepEditor1);
            this.Controls.Add(this.guideLabel);
            this.Controls.Add(this.scaleLabel);
            this.Controls.Add(this.moveLabel);
            this.Controls.Add(this.transformLabel);
            this.Controls.Add(this.circleLabel);
            this.Controls.Add(this.rectLabel);
            this.Controls.Add(this.drawLabel);
            this.MinimumSize = new System.Drawing.Size(1150, 690);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dynamic Visualizer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.Label straightLabel;
        private System.Windows.Forms.Label hLabel;
        private System.Windows.Forms.Label aLabel;
        private System.Windows.Forms.Label dLabel;
        private System.Windows.Forms.Label fLabel;
        private System.Windows.Forms.Label addStepBeforeLabel;
        private System.Windows.Forms.Label bLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

