using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.Command
{
    public class DCSetOutputOn:SimpleDeviceCommandBase
    {
        public DCSetOutputOn():base(0x02)
        {

        }
    }
}
