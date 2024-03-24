using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32DNoiseGen.Exporting
{
    public struct FormatData_Atlas : IFormatData
    {
        [Category("Settings"), Description("Resolution (per-side) of each atlas square")]
        public int Resolution { get; set; }

        [Category("Settings"), Description("How many slices to cover on the z-axis")]
        public int Slices { get; set; }

        bool hasInitialized;
        public bool Validate()
        {
            if(!hasInitialized)
            {
                Resolution = 128;
                Slices = 128;

                hasInitialized = true;
            }

            return true;
        }
    }

    public struct FormatData_Sequence : IFormatData
    {
        [Category("Settings"), Description("Resolution (per-side) of each sequence image")]
        public int Resolution { get; set; }

        [Category("Settings"), Description("How many slices to cover on the z-axis")]
        public int Slices { get; set; }

        bool hasInitialized;
        public bool Validate()
        {
            if (!hasInitialized)
            {
                Resolution = 128;
                Slices = 128;

                hasInitialized = true;
            }

            return true;
        }
    }

    interface IFormatData
    {
        bool Validate();
    }
}
