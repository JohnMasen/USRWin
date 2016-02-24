﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsrWin.Core.Command;

namespace UsrWin.UIElement
{
    public class IotDeviceOutputResource:IoTDeviceResourceBase
    {
        public IotDeviceOutputResource(IoTDevice parent,int id):base(parent,id)
        {

        }

        public override async Task RefreshStatus()
        {
            DCGetOutputStatus cmd = new DCGetOutputStatus();
            await Parent.OriginalDevice.ExecuteCommand(cmd);
            Status = cmd.Result[ID-1];
        }

        /// <summary>
        /// The <see cref="Status" /> property's name.
        /// </summary>
        public const string StatusPropertyName = "Status";

        private bool _status = false;

        /// <summary>
        /// Sets and gets the Status property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Status
        {
            get
            {
                return _status;
            }

            set
            {
                if (_status == value)
                {
                    return;
                }

                _status = value;
                RaisePropertyChanged(StatusPropertyName);
            }
        }

        private RelayCommand _reverseCmd;

        /// <summary>
        /// Gets the Revert.
        /// </summary>
        public RelayCommand Reverse
        {
            get
            {
                return _reverseCmd
                    ?? (_reverseCmd = new RelayCommand(ExecuteRevert));
            }
        }

        private async void ExecuteRevert()
        {
            
            DCSetOutputReverse cmd= new DCSetOutputReverse(Convert.ToByte(ID));

            await Parent.OriginalDevice.ExecuteCommand(cmd);
            Status = cmd.Result;
        }
    }
}
