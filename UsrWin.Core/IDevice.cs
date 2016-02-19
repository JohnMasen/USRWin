using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core
{
    public interface IDevice
    {
        DeviceTypeEnum BoardType { get; set; }
        DeviceFeature Feature { get; set; }
        byte[] IPAddress { get; set; }
        byte[] MAC { get; set; }

        string Title { get; set; }

    }
}
