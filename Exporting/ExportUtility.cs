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

                    break;

                case ExportFormat.Sequence:

                    break;
            }
        }

        public static object GetPropertyInstance(this ExportFormat format)
        {
            switch (format)
            {
                case ExportFormat.Atlas:
                    return new ExportFormat_Atlas();

                case ExportFormat.Sequence:
                    return new ExportFormat_Sequence();
            }

            throw new NotImplementedException();
        }

        public enum ExportFormat
        {
            Atlas,
            Sequence
        }
    }
}
