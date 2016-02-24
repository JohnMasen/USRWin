using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;
using UsrWin.UIElement;

namespace UsrWin.UI.WP81.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        UsrWin.UIElement.IoTDeviceManager manager;
        public MainViewModel()
        {
            manager = new UIElement.IoTDeviceManager();
            Devices = manager.Devices;
        }
        public RelayCommand Scan
        {
            get
            {
                return manager.ScanDevice;
            }
        }
        public ObservableCollection<IoTDevice> Devices { get; private set; }
    }
}