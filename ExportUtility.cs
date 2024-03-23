using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32DNoiseGen
{
    internal class ExportUtility
    {
        public static void Export(NoiseLayer[] layers, ExportOptions options)
        {
            switch(options.format)
            {
                case ExportFormat.Atlas:

                    break;

                case ExportFormat.Sequence:

                    break;
            }
        }

        public struct ExportOptions
        {
            public ExportFormat format; // Export format
            public int resolution; // Width of image in pixels
            public object[] properties; // Properties (settings) of current export format
        }

        public enum ExportFormat
        {
            Atlas,
            Sequence
        }
    }
}
