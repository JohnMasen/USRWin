using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UsrWin.Core.Command;

namespace UsrWin.Core
{
    [DataContract]
    public class Device : IDevice,IEquatable<Device>
    {
        [DataMember]
        public DeviceTypeEnum BoardType { get; set; }
        [DataMember]
        public DeviceFeature Feature { get; set; }
        private string ip;
        [DataMember]
        public string IPAddress
        {
            get { return ip; }
            set
            {
                ip = value;
            }
        }
        [DataMember]
        public byte[] MAC { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Password { get; set; }
        public DeviceTCPHelper helper { get; set; }
        public Windows.Networking.HostName Host { get
            {
                
                return new Windows.Networking.HostName(IPAddress);
            }
        }

        public Device()
        {
            IPAddress = string.Empty;
            MAC = new byte[6];
            helper = new DeviceTCPHelper();
            Password = "admin";
        }



        public async Task ExecuteCommand(IDeviceCommand command)
        {
             await helper.SendCommands(this.Host, new List<IDeviceCommand>(1) { command },Password);
        }
        public async Task ExecuteCommand(List<IDeviceCommand> command)
        {
            await helper.SendCommands(this.Host, command, Password);
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

        public override int GetHashCode()
        {
            return MAC.GetHashCode();
        }

    }
}
