using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.Command
{

    public class DCGetOutputStatus:DeviceCommandWithResult<List<bool>>
    {
        public DCGetOutputStatus():base(0x0a)
        {

        }
        protected override List<bool> processResult(byte[] result)
        {
            List<bool> r = new List<bool>();
            byte mask = 0x01;
            foreach (var b in result)
            {
                for (int i = 0; i < 8; i++)
                {
                    r.Add(!((mask << i & b) == 0)) ;
                }
            }
            return r;
        }
    }
}
