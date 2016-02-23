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
        public event EventHandler<IDevice> DeviceFound;
        public DeviceManager()
        {
            udpSocket = new Windows.Networking.Sockets.DatagramSocket();
            udpSocket.MessageReceived += udpReceived;
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

        private void udpReceived(Windows.Networking.Sockets.DatagramSocket sender, Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
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

            if (DeviceFound!=null)
            {
                DeviceFound(this, device);
            }
        }

        private IDevice createDeviceFromData(byte[] data)
        {
            Device d = new Device();
            d.BoardType = (DeviceTypeEnum)data[3];
            d.Feature = new DeviceFeature(data[4]);

            StringBuilder sb = new StringBuilder();
            for (int i = 5; i < 9; i++)
            {
                sb.AppendFormat("{0:d}", data[i]);
                sb.Append(".");
            }
            sb.Length--;
            d.IPAddress = sb.ToString();

            Array.Copy(data, 9, d.MAC, 0, 6);
            d.Title = Encoding.UTF8.GetString(data, 19, 16).Replace("\0", string.Empty);
            return d;
        }
    }
}
