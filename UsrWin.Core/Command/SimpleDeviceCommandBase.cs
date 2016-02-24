using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.Command
{
    public abstract class SimpleDeviceCommandBase:DeviceCommandBase
    {
        public SimpleDeviceCommandBase(byte command):base(command)
        {

        }
        public override void SetResult(byte[] result)
        {
        }
    }
}
