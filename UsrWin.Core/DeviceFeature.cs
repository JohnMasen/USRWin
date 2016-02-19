using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core
{
    public class DeviceFeature
    {
        private const byte WEB_CONFIG = 0x80;
        private const byte CONFIGABLE_RESOURCES = 0x40;
        private const byte WIFI = 0x20;
        private const byte LAN = 0x10;
        private const byte GPRS = 0x08;
        private const byte SMART_LINK = 0x04;
        private const byte SCHEDULER = 0x02;

        public DeviceFeature(byte value)
        {
            RawData = value;
        }
        private byte rawData;
        public byte RawData {
            get
            {
                return rawData;
            }
            private set
            {
                rawData = value;
                IsSupportWebConfig = getBoolFromByteWithMask(rawData, WEB_CONFIG);
                IsConfigableResources = getBoolFromByteWithMask(rawData, CONFIGABLE_RESOURCES);
                IsSupportWIFI = getBoolFromByteWithMask(rawData, WIFI);
                IsSupportLAN = getBoolFromByteWithMask(rawData, LAN);
                IsSupportGPRS = getBoolFromByteWithMask(rawData, GPRS);
                IsSupportSmartLink = getBoolFromByteWithMask(rawData, SMART_LINK);
                IsSupportScheduler = getBoolFromByteWithMask(rawData, SCHEDULER);
            }
        }
        public bool IsSupportWebConfig { get; private set; }
        public bool IsConfigableResources { get; private set; }
        public bool IsSupportWIFI { get; private set; }
        public bool IsSupportLAN { get; private set; }
        public bool IsSupportGPRS { get; private set; }
        public bool IsSupportSmartLink { get; private set; }
        public bool IsSupportScheduler { get; private set; }
        private bool getBoolFromByteWithMask(byte data, byte mask)
        {
            return (data & mask) != 0;
        }






    }
}
