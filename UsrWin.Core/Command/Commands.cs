using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.Command
{
    public enum Commands:byte
    {
        LoadResources=0x7e,
        OutputOff=0x01,
        OutputOn=0x02,
        OutputRevert=0x03,
        OutputGetStatus=0x09
    }
}
