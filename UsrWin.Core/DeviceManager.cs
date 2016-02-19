using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Storage.Streams;

namespace UsrWin.Core
{
    public class DeviceManager
    {
        
        Windows.Networking.Sockets.DatagramSocket udpSocket ;
        private byte[] dicoverDeviceCommand = new byte[] { 0xFF, 0x01, 0x01, 0x02 };
        TaskScheduler uiScheduler;
        public ObservableCollection<IDevice> Devices { get; private set; }
        public DeviceManager()
        {
            Devices = new ObservableCollection<IDevice>();
            udpSocket = new Windows.Networking.Sockets.DatagramSocket();
            udpSocket.MessageReceived += udpReceived;
            uiScheduler = System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext();
        }
        public async Task Init()
        {
            //await udpSocket.BindServiceNameAsync("1919");
        }
        public async Task Scan()
        {
            
            
            try
            {
                var stream = udpSocket.GetOutputStreamAsync(new HostName("255.255.255.255"), "1901");
                //udpSocket.JoinMulticastGroup(new HostName("255.255.255.255"));
                DataWriter writer = new DataWriter(await stream.AsTask());
                writer.WriteBytes(dicoverDeviceCommand);
                await writer.StoreAsync();
                await writer.FlushAsync();
                writer.DetachStream();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private async void udpReceived(Windows.Networking.Sockets.DatagramSocket sender, Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
        {
            
            byte[] buffer = new byte[36];
            using (var reader= args.GetDataReader())
            {
                if (reader.UnconsumedBufferLength != 36)
                {
                    System.Diagnostics.Debug.WriteLine("Invalid data,data length={0}", reader.UnconsumedBufferLength);

                    return;

                }
                args.GetDataReader().ReadBytes(buffer);
            }
            
            var device = createDeviceFromData(buffer);
            
            if (!Devices.Contains(device))
            {
                await Task.Factory.StartNew
                    (
                    () =>
                    {
                        Devices.Add(device);
                    }
                    , Task.Factory.CancellationToken, TaskCreationOptions.None, uiScheduler);

                
            }
        }

        private IDevice createDeviceFromData(byte[] data)
        {
            Device d = new Device();
            d.BoardType = (DeviceTypeEnum)data[3];
            d.Feature = new DeviceFeature(data[4]);
            Array.Copy(data, 5, d.IPAddress, 0, 4);
            Array.Copy(data, 9, d.MAC, 0, 6);
            d.Title = Encoding.UTF8.GetString(data, 19, 16).Replace("\0", string.Empty);
            return d;

        }
    }
}
