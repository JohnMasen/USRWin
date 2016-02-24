using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.Command
{
    public class DCSetOutputReverse:SimpleDeviceCommandBase
    {
        public DCSetOutputReverse(byte channel):base(0x03)
        {
            this.Parameter = new byte[1] { channel };
        }

        
    }
}
