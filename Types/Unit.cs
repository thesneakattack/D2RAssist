using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapAssist.Types
{
    
    public class Unit
    {
        public IntPtr pBaseAddress; //Base Address of UnitAny
        public uint type;
        public uint txtFileNo;
        public uint unitId;
        public int mode;
        public IntPtr pData;
        public int act;
        public IntPtr pAct;
        public IntPtr pPath;
        public ushort xLoc;
        public ushort yLoc;
        public string name;
        public IntPtr pNext;

        public Point GetPoint()
        {
            return new Point(xLoc, yLoc);
        }

        public override string ToString()
        {
            return string.Format("{0} with id {1} at {2},{3} at idx {4}", name, unitId, xLoc, yLoc, unitId & 0x7F);
        }

    }
}
