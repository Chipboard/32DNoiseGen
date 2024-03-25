using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _32DNoiseGen.Exporting;
using static _32DNoiseGen.Exporting.ExportUtility;

namespace _32DNoiseGen
{
    public partial class ExportForm : Form
    {
        public ExportForm()
        {
            InitializeComponent();
        }

        private void Export_Form_Load(object sender, EventArgs e)
        {
            exportType.Items.AddRange(new object[]
            {
                ExportFormat.Atlas,
                ExportFormat.Sequence
            });

            exportType.SelectedIndex = 0;

            exportType.SelectedIndexChanged += UpdateProperties;
            UpdateProperties(null, null);
        }

        private void UpdateProperties(object sender, EventArgs e)
        {
            exportProperties.SelectedObject = ((ExportFormat)exportType.SelectedItem).GetPropertyInstance();
        }

        private void Export_Click(object sender, EventArgs e)
        {
            Export((ExportFormat)exportType.SelectedItem, exportProperties.SelectedObject);
        }
    }
}
