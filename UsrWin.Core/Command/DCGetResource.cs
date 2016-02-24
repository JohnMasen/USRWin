using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.Command
{
    public class DeviceResourceList
    {
        public int Output { get; set; }
        public int Input { get; set; }
        public int PWM { get; set; }
        public int Infrared { get; set; }
    }
    public class DCGetResource:DeviceCommandWithResult<DeviceResourceList>
    {
        public DCGetResource():base(0x7e)
        {

        }

        protected override DeviceResourceList processResult(byte[] result)
        {
            DeviceResourceList r = new DeviceResourceList();
            r.Output = result[0];
            r.Input = result[1];
            r.PWM = result[2];
            r.Input = result[3];
            return r;
        }
    }
}
