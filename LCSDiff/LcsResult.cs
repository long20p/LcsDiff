using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCSDiff
{
    public struct LcsResult
    {
        public int StartIndexInSource { get; set; }
        public int StartIndexInTarget { get; set; }
        public int Length { get; set; }
    }
}
