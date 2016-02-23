using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.UIElement
{
    public abstract class IoTDeviceResourceBase:GalaSoft.MvvmLight.ObservableObject
    {
        public IoTDevice Parent { get; private set; }
        public IoTDeviceResourceBase(IoTDevice parent,int id)
        {
            this.Parent = parent;
            ID = id;
        }
        public int ID { get; private set; }
        public abstract Task RefreshStatus();

    }
}
