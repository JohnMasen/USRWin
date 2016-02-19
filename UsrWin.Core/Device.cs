using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core
{
    public class Device : IDevice,IEquatable<Device>
    {
        public DeviceTypeEnum BoardType { get; set; }
        public DeviceFeature Feature { get; set; }
        public byte[] IPAddress { get; set; }
        public byte[] MAC { get; set; }

        public string Title { get; set; }
        public Device()
        {
            IPAddress = new byte[4];
            MAC = new byte[6];
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
