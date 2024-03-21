using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _32DNoiseGen
{
    public partial class NoiseForm : Form
    {
        ToolTip imageToolTip;

        public NoiseForm()
        {
            InitializeComponent();
        }

        private void NoiseForm_Load(object sender, EventArgs e)
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            imageToolTip = new ToolTip
            {
                AutoPopDelay = 3000,
                AutomaticDelay = 0,
                InitialDelay = 0,
                ReshowDelay = 0,
                ShowAlways = true
            };

            imageToolTip.SetToolTip(previewImage, "Click to copy");
        }

        private void previewImage_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(previewImage.Image);
            imageToolTip.Show("Image Copied!", Application.OpenForms[Application.OpenForms.Count - 1], 3000);
        }
    }
}
