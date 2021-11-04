using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapAssist.Types;

namespace MapAssist.Helpers
{
    //Creates an instance of Unit
    public class UnitFactory
    {
        private IntPtr _processHandle;

        private byte[] unitBuffer = new byte[0x158];

        private static readonly int _typeOffset = 0x00;
        private static readonly int _txtFileNoOffset = 0x04;
        private static readonly int _unitIdOffset = 0x08;
        private static readonly int _modeOffset = 0x0C;
        private static readonly int _dataPtrOffset = 0x10;
        private static readonly int _actOffset = 0x18;
        private static readonly int _actPtrOffset = 0x1B; //Act pointer?
        private static readonly int _xOffset = 0xD4;
        private static readonly int _yOffset = 0xD6;
        private static readonly int _pListNext = 0x150; //Point to next unit if same index in hash table



        public UnitFactory(IntPtr processHandle)
        {
            _processHandle = processHandle;
        }

        public List<Unit> GetUnits(IntPtr address)
        {
            var units = new List<Unit>();
            var u = GetUnit(address);
            units.Add(u);
            var nextPtr = u.pNext;
            while(nextPtr != IntPtr.Zero)
            {
                var next = GetUnit(nextPtr);
                units.Add(next);
                nextPtr = next.pNext;
            }

            return units;
        }

        private Unit GetUnit(IntPtr address)
        {
            WindowsExternal.ReadProcessMemory(_processHandle, address, unitBuffer, unitBuffer.Length, out _);
            var u = new Unit
            {
                pBaseAddress = address,
                type = BitConverter.ToUInt32(unitBuffer, _typeOffset),
                txtFileNo = BitConverter.ToUInt32(unitBuffer, _txtFileNoOffset),
                unitId = BitConverter.ToUInt32(unitBuffer, _unitIdOffset),
                mode = BitConverter.ToInt32(unitBuffer, _modeOffset),
                pData = (IntPtr)BitConverter.ToInt64(unitBuffer, _dataPtrOffset),
                act = BitConverter.ToInt32(unitBuffer, _actOffset),
                pAct = (IntPtr)BitConverter.ToInt64(unitBuffer, _actPtrOffset),
                xLoc = BitConverter.ToUInt16(unitBuffer, _xOffset),
                yLoc = BitConverter.ToUInt16(unitBuffer, _yOffset),
                pNext = (IntPtr)BitConverter.ToInt64(unitBuffer, _pListNext),
            };
            u.name = Enum.GetName(typeof(Npc), u.txtFileNo);
            return u;
        }

    }
}
