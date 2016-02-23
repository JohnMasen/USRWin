using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UsrWin.UIElement;

namespace UsrWin.UI.Win10.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        UsrWin.UIElement.IoTDeviceManager manager;
        public MainViewModel()
        {
            manager = new UIElement.IoTDeviceManager();
            Devices = manager.Devices;
        }
        public RelayCommand Scan { get
            {
                return manager.ScanDevice;
            }
        }
        public ObservableCollection<IoTDevice> Devices { get; private set; }
    }
}