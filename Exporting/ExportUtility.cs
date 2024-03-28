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

                    int columnCount = (int)Math.Floor(Math.Sqrt(atlasProperties.Slices));
                    int rowCount = (int)Math.Ceiling((double)atlasProperties.Slices / columnCount);

                    int imageWidth = columnCount * atlasProperties.Resolution;
                    int imageHeight = rowCount * atlasProperties.Resolution;

                    Bitmap atlasBitmap = new Bitmap(imageWidth, imageHeight);
                    Bitmap currentBitmap = new Bitmap(atlasProperties.Resolution, atlasProperties.Resolution);

                    using (Graphics g = Graphics.FromImage(atlasBitmap))
                    {
                        int column = 0;
                        int row = 0;
                        for (int i = 0; i < atlasProperties.Slices; i++)
                        {
                            if (column == columnCount)
                            {
                                column = 0;
                                row++;
                            }

                            int x = column * atlasProperties.Resolution;
                            int y = row * atlasProperties.Resolution;

                            float interp = (float)i / (atlasProperties.Slices - 1);
                            int slice = (int)Math.Round(interp * (atlasProperties.Resolution - 1));

                            currentBitmap.SetGrayscaleBitmap(
                                Program.GetTotalNoise(slice, atlasProperties.Resolution),
                                0,
                                0,
                                atlasProperties.Resolution, atlasProperties.Resolution);

                            g.DrawImage(currentBitmap, new Rectangle(x, y, atlasProperties.Resolution, atlasProperties.Resolution));
                            column++;
                        }
                    }

                    atlasBitmap.Save($"{folderPath}/{atlasProperties.FileName}.png", ImageFormat.Png);
                    break;

                case ExportFormat.Sequence:
                    FormatData_Sequence sequenceProperties = (FormatData_Sequence)properties;

                    Bitmap sequenceBitmap = new Bitmap(sequenceProperties.Resolution, sequenceProperties.Resolution);
                    for (int i = 0; i < sequenceProperties.Slices; i++)
                    {
                        float interp = (float)i / sequenceProperties.Slices;
                        int slice = (int)Math.Ceiling(interp * sequenceProperties.Resolution);
                        sequenceBitmap.SetGrayscaleBitmap(Program.GetTotalNoise(slice, sequenceProperties.Resolution), 0, 0, sequenceProperties.Resolution, sequenceProperties.Resolution);
                        sequenceBitmap.Save($"{folderPath}/{sequenceProperties.FileName}{i:D4}.png", ImageFormat.Png);
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
