using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.Command
{
    public abstract class DeviceCommandWithResult<T>:DeviceCommandBase
    {
        public T Result { get; private set; }
        public DeviceCommandWithResult(byte command):base(command)
        {

        }
        public sealed override void SetResult(byte[] result)
        {
            Result = processResult(parseResult(result));
        }
        protected abstract T processResult(byte[] result);
        
    }
}
