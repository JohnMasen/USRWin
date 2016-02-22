using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.SubDevices
{
    public abstract class SubDeviceBase : ISubDevice
    {
        public SubDeviceBase(string type,int id,Device parent)
        {
            DeviceID = id;
            DeviceType = type;
            Parent = parent;
        }
        public int DeviceID { get; private set; }

        public string DeviceType { get; private set; }
        public Device Parent { get; set; }
    }
}
