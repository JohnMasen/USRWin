using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsrWin.Core.Command;

namespace UsrWin.Core
{
    public class Device : IDevice,IEquatable<Device>
    {
        public DeviceTypeEnum BoardType { get; set; }
        public DeviceFeature Feature { get; set; }
        private byte[] ip;

        public byte[] IPAddress
        {
            get { return ip; }
            set
            {
                ip = value;
                Host = new Windows.Networking.HostName(IPAddress.ToString().Replace("-", "."));
            }
        }

        public byte[] MAC { get; set; }

        public string Title { get; set; }

        public DeviceTCPHelper helper { get; set; }
        public Windows.Networking.HostName Host { get; private set; }

        public Device()
        {
            IPAddress = new byte[4];
            MAC = new byte[6];
            helper = new DeviceTCPHelper();
        }



        public async Task ExecuteCommand(Command.Commands cmd, byte[] parameter,Action<byte[]> resultAction)
        {
            var result = await helper.SendCommand(this.Host, (byte)cmd, parameter);
            if (resultAction!=null)
            {
                resultAction(result);
            }
        }

        public bool Equals(Device other)
        {
            return other.MAC.SequenceEqual(MAC);
        }

        public override bool Equals(object obj)
        {
            if (obj is Device)
            {
                return Equals((obj as Device));
            }
            else
            {
                return base.Equals(obj);
            }
            
        }

    }
}
