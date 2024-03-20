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
        public NoiseForm()
        {
            InitializeComponent();
        }

        private void NoiseForm_Load(object sender, EventArgs e)
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private void previewImage_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(previewImage.Image);
        }
    }
}
