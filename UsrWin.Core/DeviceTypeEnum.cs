using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core
{
    public enum DeviceTypeEnum:byte
    {
        IOT = 0x01,
        WIFI_IO_Mini=0x02,
        GPRS_RTU=0x03,
        WIFI_IO_83=0x04,
        IOT2=0x05,
        USR_WP1q=0x0d
    }
}
