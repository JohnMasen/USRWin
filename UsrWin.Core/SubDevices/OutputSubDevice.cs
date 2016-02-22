using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.SubDevices
{
    public class OutputSubDevice :SubDeviceBase
    {
        public OutputSubDevice(int id,Device parent):base("Output",id,parent)
        {

        }
        public void Reverse()
        {
        }
    }
}
