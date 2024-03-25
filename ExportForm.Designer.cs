namespace _32DNoiseGen
{
    partial class ExportForm
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
            this.label15 = new System.Windows.Forms.Label();
            this.exportType = new System.Windows.Forms.ComboBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.exportProperties = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 16);
            this.label15.TabIndex = 38;
            this.label15.Text = "Export Type";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // exportType
            // 
            this.exportType.FormattingEnabled = true;
            this.exportType.Location = new System.Drawing.Point(12, 28);
            this.exportType.Name = "exportType";
            this.exportType.Size = new System.Drawing.Size(258, 24);
            this.exportType.TabIndex = 37;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(12, 326);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(258, 24);
            this.exportButton.TabIndex = 39;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.Export_Click);
            // 
            // exportProperties
            // 
            this.exportProperties.Location = new System.Drawing.Point(12, 58);
            this.exportProperties.Name = "exportProperties";
            this.exportProperties.Size = new System.Drawing.Size(258, 262);
            this.exportProperties.TabIndex = 40;
            // 
            // Export_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(282, 353);
            this.Controls.Add(this.exportProperties);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.exportType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Export_Form";
            this.Text = "Export";
            this.Load += new System.EventHandler(this.Export_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.ComboBox exportType;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.PropertyGrid exportProperties;
    }
}