using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _32DNoiseGen.IO
{
    internal class IOUtility
    {
        public static string SaveDialog(string format)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                InitialDirectory = Application.ExecutablePath,
                Title = "Save Layers",

                CheckFileExists = false,
                CheckPathExists = true,

                DefaultExt = format,
                Filter = $"{format} files (*.{format})|*.{format}",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            DialogResult result = saveDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return saveDialog.FileName;
            } else
            {
                if(result != DialogResult.Cancel)
                    MessageBox.Show($"SaveDialog failure! {result}");

                return null;
            }
        }
    }
}
