namespace _32DNoiseGen
{
    partial class NoiseForm
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
            this.noiseList = new System.Windows.Forms.CheckedListBox();
            this.addLayerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.removeLayerButton = new System.Windows.Forms.Button();
            this.previewImage = new System.Windows.Forms.PictureBox();
            this.previewDepthBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.noiseType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.combineType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.amplitude = new System.Windows.Forms.NumericUpDown();
            this.frequency = new System.Windows.Forms.NumericUpDown();
            this.inverted = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.oneMinus = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.absolute = new System.Windows.Forms.CheckBox();
            this.FBMLacunarity = new System.Windows.Forms.NumericUpDown();
            this.FBMGain = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.FBM = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.FBMOctaves = new System.Windows.Forms.NumericUpDown();
            this.seed = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.previewImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewDepthBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amplitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FBMLacunarity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FBMGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FBMOctaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seed)).BeginInit();
            this.SuspendLayout();
            // 
            // noiseList
            // 
            this.noiseList.FormattingEnabled = true;
            this.noiseList.Location = new System.Drawing.Point(13, 47);
            this.noiseList.Name = "noiseList";
            this.noiseList.Size = new System.Drawing.Size(137, 327);
            this.noiseList.TabIndex = 0;
            // 
            // addLayerButton
            // 
            this.addLayerButton.Location = new System.Drawing.Point(13, 380);
            this.addLayerButton.Name = "addLayerButton";
            this.addLayerButton.Size = new System.Drawing.Size(138, 27);
            this.addLayerButton.TabIndex = 1;
            this.addLayerButton.Text = "Add Layer";
            this.addLayerButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Noise Layers";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // removeLayerButton
            // 
            this.removeLayerButton.Location = new System.Drawing.Point(12, 413);
            this.removeLayerButton.Name = "removeLayerButton";
            this.removeLayerButton.Size = new System.Drawing.Size(139, 27);
            this.removeLayerButton.TabIndex = 3;
            this.removeLayerButton.Text = "Remove Layer";
            this.removeLayerButton.UseVisualStyleBackColor = true;
            // 
            // previewImage
            // 
            this.previewImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.previewImage.Image = global::_32DNoiseGen.Properties.Resources.PreviewGrid;
            this.previewImage.Location = new System.Drawing.Point(157, 12);
            this.previewImage.Name = "previewImage";
            this.previewImage.Size = new System.Drawing.Size(428, 428);
            this.previewImage.TabIndex = 4;
            this.previewImage.TabStop = false;
            // 
            // previewDepthBar
            // 
            this.previewDepthBar.Location = new System.Drawing.Point(591, 413);
            this.previewDepthBar.Name = "previewDepthBar";
            this.previewDepthBar.Size = new System.Drawing.Size(196, 56);
            this.previewDepthBar.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(647, 394);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Preview Depth";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // noiseType
            // 
            this.noiseType.FormattingEnabled = true;
            this.noiseType.Location = new System.Drawing.Point(591, 28);
            this.noiseType.Name = "noiseType";
            this.noiseType.Size = new System.Drawing.Size(197, 24);
            this.noiseType.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(647, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Noise Type";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(647, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Combine Type";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // combineType
            // 
            this.combineType.FormattingEnabled = true;
            this.combineType.Location = new System.Drawing.Point(591, 74);
            this.combineType.Name = "combineType";
            this.combineType.Size = new System.Drawing.Size(197, 24);
            this.combineType.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(588, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Amplitude:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(588, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Frequency:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // amplitude
            // 
            this.amplitude.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.amplitude.Location = new System.Drawing.Point(668, 104);
            this.amplitude.Name = "amplitude";
            this.amplitude.Size = new System.Drawing.Size(120, 22);
            this.amplitude.TabIndex = 16;
            // 
            // frequency
            // 
            this.frequency.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.frequency.Location = new System.Drawing.Point(668, 134);
            this.frequency.Name = "frequency";
            this.frequency.Size = new System.Drawing.Size(120, 22);
            this.frequency.TabIndex = 17;
            // 
            // inverted
            // 
            this.inverted.AutoSize = true;
            this.inverted.Location = new System.Drawing.Point(769, 190);
            this.inverted.Name = "inverted";
            this.inverted.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.inverted.Size = new System.Drawing.Size(18, 17);
            this.inverted.TabIndex = 18;
            this.inverted.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(588, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = "Negate:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(588, 214);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "One Minus:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // oneMinus
            // 
            this.oneMinus.AutoSize = true;
            this.oneMinus.Location = new System.Drawing.Point(769, 215);
            this.oneMinus.Name = "oneMinus";
            this.oneMinus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.oneMinus.Size = new System.Drawing.Size(18, 17);
            this.oneMinus.TabIndex = 20;
            this.oneMinus.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(588, 237);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 16);
            this.label9.TabIndex = 23;
            this.label9.Text = "Absolute:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // absolute
            // 
            this.absolute.AutoSize = true;
            this.absolute.Location = new System.Drawing.Point(769, 238);
            this.absolute.Name = "absolute";
            this.absolute.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.absolute.Size = new System.Drawing.Size(18, 17);
            this.absolute.TabIndex = 22;
            this.absolute.UseVisualStyleBackColor = true;
            // 
            // FBMLacunarity
            // 
            this.FBMLacunarity.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.FBMLacunarity.Location = new System.Drawing.Point(668, 314);
            this.FBMLacunarity.Name = "FBMLacunarity";
            this.FBMLacunarity.Size = new System.Drawing.Size(120, 22);
            this.FBMLacunarity.TabIndex = 27;
            // 
            // FBMGain
            // 
            this.FBMGain.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.FBMGain.Location = new System.Drawing.Point(668, 284);
            this.FBMGain.Name = "FBMGain";
            this.FBMGain.Size = new System.Drawing.Size(120, 22);
            this.FBMGain.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(589, 316);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 16);
            this.label10.TabIndex = 25;
            this.label10.Text = "Lacunarity:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(589, 286);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 16);
            this.label11.TabIndex = 24;
            this.label11.Text = "Gain:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(589, 260);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 16);
            this.label12.TabIndex = 29;
            this.label12.Text = "FBM:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FBM
            // 
            this.FBM.AutoSize = true;
            this.FBM.Location = new System.Drawing.Point(769, 261);
            this.FBM.Name = "FBM";
            this.FBM.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FBM.Size = new System.Drawing.Size(18, 17);
            this.FBM.TabIndex = 28;
            this.FBM.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(589, 344);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 16);
            this.label13.TabIndex = 30;
            this.label13.Text = "Octaves:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FBMOctaves
            // 
            this.FBMOctaves.Location = new System.Drawing.Point(668, 342);
            this.FBMOctaves.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FBMOctaves.Name = "FBMOctaves";
            this.FBMOctaves.Size = new System.Drawing.Size(120, 22);
            this.FBMOctaves.TabIndex = 31;
            this.FBMOctaves.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // seed
            // 
            this.seed.Location = new System.Drawing.Point(668, 162);
            this.seed.Maximum = new decimal(new int[] {
            -1304428545,
            434162106,
            542,
            0});
            this.seed.Minimum = new decimal(new int[] {
            268435455,
            1042612833,
            542101086,
            -2147483648});
            this.seed.Name = "seed";
            this.seed.Size = new System.Drawing.Size(120, 22);
            this.seed.TabIndex = 33;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(588, 164);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 16);
            this.label14.TabIndex = 32;
            this.label14.Text = "Seed:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 34;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // NoiseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.seed);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.FBMOctaves);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.FBM);
            this.Controls.Add(this.FBMLacunarity);
            this.Controls.Add(this.FBMGain);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.absolute);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.oneMinus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.inverted);
            this.Controls.Add(this.frequency);
            this.Controls.Add(this.amplitude);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.combineType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.noiseType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.previewDepthBar);
            this.Controls.Add(this.previewImage);
            this.Controls.Add(this.removeLayerButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addLayerButton);
            this.Controls.Add(this.noiseList);
            this.Name = "NoiseForm";
            this.Text = "32D Noise Creator";
            ((System.ComponentModel.ISupportInitialize)(this.previewImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewDepthBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amplitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FBMLacunarity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FBMGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FBMOctaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckedListBox noiseList;
        public System.Windows.Forms.Button addLayerButton;
        public System.Windows.Forms.Button removeLayerButton;
        public System.Windows.Forms.PictureBox previewImage;
        public System.Windows.Forms.TrackBar previewDepthBar;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox noiseType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox combineType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown amplitude;
        public System.Windows.Forms.NumericUpDown frequency;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.CheckBox inverted;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.CheckBox oneMinus;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.CheckBox absolute;
        public System.Windows.Forms.NumericUpDown FBMLacunarity;
        public System.Windows.Forms.NumericUpDown FBMGain;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.CheckBox FBM;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.NumericUpDown FBMOctaves;
        public System.Windows.Forms.NumericUpDown seed;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}

