using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.Command
{
    public interface IDeviceCommand
    {
        byte[] CommandBuffer { get; }
        void SetResult(byte[] result);
    }
}
