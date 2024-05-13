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
        [Category("Settings"), Description("File name")]
        public string FileName { get; set; }

        [Category("Settings"), Description("Resolution (per-side) of each atlas square")]
        public int Resolution { get; set; }

        [Category("Settings"), Description("The number of z slices to cover")]
        public int SliceCount { get; set; }

        [Category("Settings"), Description("The beginning range of z slices to cover")]
        public int SliceStart { get; set; }

        [Category("Settings"), Description("The ending range of z slices to cover")]
        public int SliceEnd { get; set; }

        bool hasInitialized;
        public bool Validate()
        {
            if(!hasInitialized)
            {
                FileName = "Export";
                Resolution = 400;
                SliceCount = 400;
                SliceStart = 0;
                SliceEnd = 400;

                hasInitialized = true;
            }

            return true;
        }
    }

    public struct FormatData_Sequence : IFormatData
    {
        [Category("Settings"), Description("File name")]
        public string FileName { get; set; }

        [Category("Settings"), Description("Resolution (per-side) of each sequence image")]
        public int Resolution { get; set; }

        [Category("Settings"), Description("The number of z slices to cover")]
        public int SliceCount { get; set; }

        [Category("Settings"), Description("The beginning range of z slices to cover")]
        public int SliceStart { get; set; }

        [Category("Settings"), Description("The ending range of z slices to cover")]
        public int SliceEnd { get; set; }

        bool hasInitialized;
        public bool Validate()
        {
            if (!hasInitialized)
            {
                FileName = "Export";
                Resolution = 400;
                SliceCount = 400;
                SliceStart = 0;
                SliceEnd = 400;

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
