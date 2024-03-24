using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32DNoiseGen.Exporting
{
    internal static class ExportUtility
    {
        public static void Export(ExportFormat format, object properties)
        {
            switch(format)
            {
                case ExportFormat.Atlas:
                    FormatData_Atlas atlasProperties = (FormatData_Atlas)properties;
                    break;

                case ExportFormat.Sequence:
                    FormatData_Sequence sequenceProperties = (FormatData_Sequence)properties;
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

            return data;
        }

        public enum ExportFormat
        {
            Atlas,
            Sequence
        }
    }
}
