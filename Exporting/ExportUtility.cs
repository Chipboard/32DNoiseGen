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

                    int sliceCount = Math.Abs(atlasProperties.SliceEnd - atlasProperties.SliceStart);;
                    int columnCount = (int)Math.Floor(Math.Sqrt(atlasProperties.SliceCount));
                    int rowCount = (int)Math.Ceiling((double)atlasProperties.SliceCount / columnCount);

                    int imageWidth = columnCount * atlasProperties.Resolution;
                    int imageHeight = rowCount * atlasProperties.Resolution;

                    Bitmap atlasBitmap = new Bitmap(imageWidth, imageHeight);
                    Bitmap currentBitmap = new Bitmap(atlasProperties.Resolution, atlasProperties.Resolution);

                    using (Graphics g = Graphics.FromImage(atlasBitmap))
                    {
                        int column = 0;
                        int row = 0;
                        for (int i = 0; i < atlasProperties.SliceCount; i++)
                        {
                            if (column == columnCount)
                            {
                                column = 0;
                                row++;
                            }

                            int x = column * atlasProperties.Resolution;
                            int y = row * atlasProperties.Resolution;

                            float interp = (float)i / (sliceCount - 1);
                            int slice = (int)Math.Round(Lerp(atlasProperties.SliceStart, atlasProperties.SliceEnd, interp));

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

                    int sequenceSliceCount = Math.Abs(sequenceProperties.SliceEnd - sequenceProperties.SliceStart);
                    Bitmap sequenceBitmap = new Bitmap(sequenceProperties.Resolution, sequenceProperties.Resolution);
                    for (int i = 0; i < sequenceProperties.SliceCount; i++)
                    {
                        float interp = (float)i / sequenceSliceCount;
                        int slice = (int)Math.Ceiling(Lerp(sequenceProperties.SliceStart, sequenceProperties.SliceEnd, interp));

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

        static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        public enum ExportFormat
        {
            Atlas,
            Sequence
        }
    }
}
