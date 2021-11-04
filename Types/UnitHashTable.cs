using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapAssist.Types
{
    public class UnitHashTable
    {
        private IntPtr _baseAddress;
        private IntPtr[] _array;

        public UnitHashTable(IntPtr address)
        {
            _baseAddress = address;
            _array = new IntPtr[128];
        }

        public void update()
        {
            
        }

    }
}
