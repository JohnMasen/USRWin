using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsrWin.Core;
namespace UsrWin.UIElement
{
    public class IoTDeviceManager
    {
        DeviceManager dm;
        public System.Collections.ObjectModel.ObservableCollection<IoTDevice> Devices { get; private set; }

        public IoTDeviceManager()
        {
            dm = new DeviceManager();
            dm.DeviceFound += Dm_DeviceFound;
            Devices = new System.Collections.ObjectModel.ObservableCollection<IoTDevice>();
        }

        private async void Dm_DeviceFound(object sender, IDevice e)
        {
            if (Devices.FirstOrDefault((x)=>x.MAC==e.MAC)==null)
            {
                IoTDevice tmp = new IoTDevice(e);
                try
                {
                    await tmp.RefreshResource();
                    Devices.Add(tmp);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
        }

        private RelayCommand _scanDeviceCmd;

        /// <summary>
        /// Gets the ScanDevice.
        /// </summary>
        public RelayCommand ScanDevice
        {
            get
            {
                return _scanDeviceCmd
                    ?? (_scanDeviceCmd = new  RelayCommand(
                    async () =>
                    {
                        await dm.Scan();
                    }));
            }
        }
    }
}
