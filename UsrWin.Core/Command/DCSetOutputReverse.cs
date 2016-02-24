using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.Command
{
    public class DCSetOutputReverse:DeviceCommandWithResult<bool>
    {
        public DCSetOutputReverse(byte channel):base(0x03)
        {
            this.Parameter = new byte[1] { channel };
        }
        protected override bool processResult(byte[] result)
        {
            return result[1] == 1;
        }

    }
}
