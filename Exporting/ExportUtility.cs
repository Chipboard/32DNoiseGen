using _32DNoiseGen.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _32DNoiseGen.Exporting
{
    internal static class ExportUtility
    {
        public static void Export(ExportFormat format, object properties)
        {
            string folderPath = IOUtility.FolderDialog();

            if (folderPath == null)
                return;

            switch (format)
            {
                case ExportFormat.Atlas:
                    FormatData_Atlas atlasProperties = (FormatData_Atlas)properties;

                    break;

                case ExportFormat.Sequence:
                    FormatData_Sequence sequenceProperties = (FormatData_Sequence)properties;

                    Bitmap bitmap = new Bitmap(sequenceProperties.Resolution, sequenceProperties.Resolution);
                    for (int i = 0; i < sequenceProperties.Slices; i++)
                    {
                        float interp = (float)i / sequenceProperties.Slices;
                        int slice = (int)Math.Ceiling(interp * sequenceProperties.Resolution);
                        bitmap.SetGrayscaleBitmap(Program.GetTotalNoise(slice, sequenceProperties.Resolution), 0, 0, sequenceProperties.Resolution, sequenceProperties.Resolution);
                        bitmap.Save($"{folderPath}/{sequenceProperties.FileName}{i:D4}.png", ImageFormat.Png);
                    }
                    break;
            }
        }

        public static object GetPropertyInstance(this ExportFormat format)
        {
            IFormatData data = null;

            switch (format)
            {
                case ExportFormat.Atlas:
                    data = new FormatData_Atlas();
                    break;

                case ExportFormat.Sequence:
                    data = new FormatData_Sequence();
                    break;
            }

            data.Validate();
            return data;
        }

        public enum ExportFormat
        {
            Atlas,
            Sequence
        }
    }
}
