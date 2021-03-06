﻿using GalaSoft.MvvmLight.Command;
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
        System.Threading.Tasks.TaskScheduler UIScheduler;
        public IoTDeviceManager()
        {
            dm = new DeviceManager();
            dm.DeviceFound += Dm_DeviceFound;
            Devices = new System.Collections.ObjectModel.ObservableCollection<IoTDevice>();
            UIScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        private async void Dm_DeviceFound(object sender, IDevice e)
        {
            IoTDevice tmp = Devices.FirstOrDefault((x) => x.MAC.SequenceEqual(e.MAC));
            if (tmp!=null)
            {
                await Task.Factory.StartNew(() =>
                {
                    Devices.Remove(tmp);//remove existing device to refresh
                }, Task.Factory.CancellationToken, TaskCreationOptions.None, UIScheduler);
                
            }
            tmp = new IoTDevice(e);
            try
            {
                await tmp.RefreshResource();
                await Task.Factory.StartNew(() =>
                    {
                        Devices.Add(tmp);
                    }, Task.Factory.CancellationToken, TaskCreationOptions.None, UIScheduler);


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
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
