using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.SubDevices
{
    public interface ISubDevice
    {
        string DeviceType { get; }
        int DeviceID { get;  }
        Device Parent { get; set; }
    }
}
