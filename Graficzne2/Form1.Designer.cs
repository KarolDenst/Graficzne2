namespace Graficzne2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.canvas = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.zBar = new System.Windows.Forms.TrackBar();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.mBar = new System.Windows.Forms.TrackBar();
            this.ksBar = new System.Windows.Forms.TrackBar();
            this.kdBar = new System.Windows.Forms.TrackBar();
            this.chooseColorButton = new System.Windows.Forms.Button();
            this.drawButton = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.interpolateCornersButton = new System.Windows.Forms.RadioButton();
            this.interpolateEachButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ksBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdBar)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(700, 700);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.interpolateEachButton);
            this.groupBox1.Controls.Add(this.interpolateCornersButton);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.zBar);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.mBar);
            this.groupBox1.Controls.Add(this.ksBar);
            this.groupBox1.Controls.Add(this.kdBar);
            this.groupBox1.Controls.Add(this.chooseColorButton);
            this.groupBox1.Controls.Add(this.drawButton);
            this.groupBox1.Location = new System.Drawing.Point(706, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 688);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(3, 294);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(36, 27);
            this.textBox4.TabIndex = 9;
            this.textBox4.Text = "Z";
            // 
            // zBar
            // 
            this.zBar.Location = new System.Drawing.Point(38, 294);
            this.zBar.Name = "zBar";
            this.zBar.Size = new System.Drawing.Size(133, 56);
            this.zBar.TabIndex = 8;
            this.zBar.Scroll += new System.EventHandler(this.zBar_Scroll);
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(3, 243);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(36, 27);
            this.textBox3.TabIndex = 7;
            this.textBox3.Text = "M";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(3, 177);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(36, 27);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "KS";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(3, 114);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(36, 27);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "KD";
            // 
            // mBar
            // 
            this.mBar.Location = new System.Drawing.Point(38, 238);
            this.mBar.Name = "mBar";
            this.mBar.Size = new System.Drawing.Size(139, 56);
            this.mBar.TabIndex = 4;
            this.mBar.Scroll += new System.EventHandler(this.mBar_Scroll);
            // 
            // ksBar
            // 
            this.ksBar.Location = new System.Drawing.Point(38, 176);
            this.ksBar.Name = "ksBar";
            this.ksBar.Size = new System.Drawing.Size(136, 56);
            this.ksBar.TabIndex = 3;
            this.ksBar.Value = 5;
            this.ksBar.Scroll += new System.EventHandler(this.ksBar_Scroll);
            // 
            // kdBar
            // 
            this.kdBar.Location = new System.Drawing.Point(38, 114);
            this.kdBar.Name = "kdBar";
            this.kdBar.Size = new System.Drawing.Size(130, 56);
            this.kdBar.TabIndex = 2;
            this.kdBar.Value = 5;
            this.kdBar.Scroll += new System.EventHandler(this.kdBar_Scroll);
            // 
            // chooseColorButton
            // 
            this.chooseColorButton.Location = new System.Drawing.Point(3, 67);
            this.chooseColorButton.Name = "chooseColorButton";
            this.chooseColorButton.Size = new System.Drawing.Size(168, 29);
            this.chooseColorButton.TabIndex = 1;
            this.chooseColorButton.Text = "Choose Color";
            this.chooseColorButton.UseVisualStyleBackColor = true;
            this.chooseColorButton.Click += new System.EventHandler(this.chooseColorButton_Click);
            // 
            // drawButton
            // 
            this.drawButton.Location = new System.Drawing.Point(3, 23);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(168, 29);
            this.drawButton.TabIndex = 0;
            this.drawButton.Text = "Draw";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.Click += new System.EventHandler(this.drawButton_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.Red;
            // 
            // interpolateCornersButton
            // 
            this.interpolateCornersButton.AutoSize = true;
            this.interpolateCornersButton.Checked = true;
            this.interpolateCornersButton.Location = new System.Drawing.Point(6, 345);
            this.interpolateCornersButton.Name = "interpolateCornersButton";
            this.interpolateCornersButton.Size = new System.Drawing.Size(157, 24);
            this.interpolateCornersButton.TabIndex = 10;
            this.interpolateCornersButton.TabStop = true;
            this.interpolateCornersButton.Text = "Interpolate Corners";
            this.interpolateCornersButton.UseVisualStyleBackColor = true;
            // 
            // interpolateEachButton
            // 
            this.interpolateEachButton.AutoSize = true;
            this.interpolateEachButton.Location = new System.Drawing.Point(6, 375);
            this.interpolateEachButton.Name = "interpolateEachButton";
            this.interpolateEachButton.Size = new System.Drawing.Size(138, 24);
            this.interpolateEachButton.TabIndex = 11;
            this.interpolateEachButton.TabStop = true;
            this.interpolateEachButton.Text = "Interpolate Each";
            this.interpolateEachButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 703);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.canvas);
            this.MaximumSize = new System.Drawing.Size(900, 750);
            this.MinimumSize = new System.Drawing.Size(900, 750);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ksBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox canvas;
        private GroupBox groupBox1;
        private Button drawButton;
        private Button chooseColorButton;
        private ColorDialog colorDialog;
        private TrackBar mBar;
        private TrackBar ksBar;
        private TrackBar kdBar;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private TextBox textBox4;
        private TrackBar zBar;
        private RadioButton interpolateEachButton;
        private RadioButton interpolateCornersButton;
    }
}