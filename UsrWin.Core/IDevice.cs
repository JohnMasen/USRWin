using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsrWin.Core.Command;

namespace UsrWin.Core
{
    
    public interface IDevice
    {
        DeviceTypeEnum BoardType { get; set; }
        DeviceFeature Feature { get; set; }
        string IPAddress { get; set; }
        string Password { get; set; }
        byte[] MAC { get; set; }

        string Title { get; set; }
        Task ExecuteCommand(List<IDeviceCommand> command);
        Task ExecuteCommand(IDeviceCommand command);
    }
}
