using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCSDiff
{
    public struct ChangeRecord
    {
        public ChangeRecord(ChangeType type, int length)
        {
            Type = type;
            Length = length;
        }
        public ChangeType Type { get; private set; }
        public int Length { get; private set; }
    }
}
