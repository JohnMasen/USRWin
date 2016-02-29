using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.Command
{
    public class DCSetOutputOff:SimpleDeviceCommandBase
    {
        public DCSetOutputOff():base(0x01)
        {

        }
    }
}
