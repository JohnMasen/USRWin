using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using UsrWin.Test.Model;
using System.Collections.Generic;
using UsrWin.Core;
using System.Collections.ObjectModel;

namespace UsrWin.Test.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UsrWin.Core.DeviceManager manager;
        public ObservableCollection<IDevice> Devices { get; private set; }
        public MainViewModel()
        {
            manager = new DeviceManager();
            manager.DeviceFound += Manager_DeviceFound;
            Devices = new ObservableCollection<IDevice>();
        }

        private void Manager_DeviceFound(object sender, Core.IDevice e)
        {
            if (!Devices.Contains(e))
            {
                GalaSoft.MvvmLight.Threading.DispatcherHelper.CheckBeginInvokeOnUI(() =>{ Devices.Add(e); });
                
            }
        }

        public RelayCommand StartScan => new RelayCommand(async () =>
        {
            await manager.Scan();
        }
        );
    }
}