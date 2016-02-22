using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.Command
{
    public class DeviceCommandHelper
    {
        private DeviceTCPHelper helper;
        public Windows.Networking.HostName Host { get; set; }
        public DeviceCommandHelper()
        {
            helper = new DeviceTCPHelper();
        }
        public struct SubDeviceList
        {
            public int OutputDevices;
            public int InputDevices;
            public int PWMDevices;
            public int InfraredDevices;
        }

        public async Task<SubDeviceList> GetSubDeviceList()
        {
            var tmp=await helper.SendCommand(Host, (byte)Commands.LoadResources, null);
            SubDeviceList result = new SubDeviceList()
            {
                OutputDevices = Convert.ToInt32(tmp[0]),
                InputDevices=Convert.ToInt32(tmp[1]),
                PWMDevices=Convert.ToInt32(tmp[2]),
                InfraredDevices=Convert.ToInt32(tmp[3])
            };
            return result;
        }
    }
}
