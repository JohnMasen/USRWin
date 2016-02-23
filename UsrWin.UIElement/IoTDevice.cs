using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsrWin.Core;
using UsrWin.Core.Command;
namespace UsrWin.UIElement
{
    public class IoTDevice:GalaSoft.MvvmLight.ObservableObject
    {
        internal IDevice OriginalDevice { get; private set; }

        /// <summary>
        /// The <see cref="IPAddress" /> property's name.
        /// </summary>
        public const string IPAddressPropertyName = "IPAddress";

        private string _IPAddress = string.Empty;

        /// <summary>
        /// Sets and gets the IPAddress property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string IPAddress
        {
            get
            {
                return _IPAddress;
            }

            set
            {
                if (_IPAddress == value)
                {
                    return;
                }

                _IPAddress = value;
                RaisePropertyChanged(IPAddressPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MAC" /> property's name.
        /// </summary>
        public const string MACPropertyName = "MAC";

        private byte[] _MAC = null;

        /// <summary>
        /// Sets and gets the MAC property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public byte[] MAC
        {
            get
            {
                return _MAC;
            }

            set
            {
                if (_MAC == value)
                {
                    return;
                }

                _MAC = value;
                RaisePropertyChanged(MACPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Title" /> property's name.
        /// </summary>
        public const string TitlePropertyName = "Title";

        private string _title = string.Empty;

        /// <summary>
        /// Sets and gets the Title property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (_title == value)
                {
                    return;
                }

                _title = value;
                RaisePropertyChanged(TitlePropertyName);
            }
        }

        public IoTDevice(IDevice source)
        {
            this.OriginalDevice = source;
            OutputResources = new List<IotDeviceOutputResource>();
            Title = source.Title;
            IPAddress = source.IPAddress;
            MAC = source.MAC;
        }

        public async Task RefreshResource()
        {
            DCGetResource cmd = new DCGetResource();
            await OriginalDevice.ExecuteCommand(cmd);
            for (int i = 0; i < cmd.Result.Output; i++)
            {
                IotDeviceOutputResource tmp = new IotDeviceOutputResource(this, i+1);
                await tmp.RefreshStatus();
                OutputResources.Add(tmp);
            }
        }
        public List<IotDeviceOutputResource> OutputResources { get; private set; }

    }
}
