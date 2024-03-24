using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32DNoiseGen.Exporting
{
    public struct ExportFormat_Atlas
    {
        [Category("Settings"), Description("Resolution of each atlas square")]
        public int Resolution { get; set; }
    }

    public struct ExportFormat_Sequence
    {
        [Category("Settings"), Description("Resolution of each sequence image")]
        public int Resolution { get; set; }
    }
}
