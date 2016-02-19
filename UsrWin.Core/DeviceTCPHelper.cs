using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace UsrWin.Core
{
    public class DeviceTCPHelper
    {
        Windows.Networking.Sockets.StreamSocket socket;
        DataWriter writer;
        DataReader reader;

        private byte[] COMMAND_HEADER = new byte[2] { 0x55, 0xaa };
        private byte[] createCommand(byte[] command,byte[] parameter)
        {
            int length =COMMAND_HEADER.Length + command.Length + parameter.Length + 4;
            List<byte> buffer = new List<byte>();
            buffer.AddRange(COMMAND_HEADER);
            
            //set the length 
            byte[] lengthBytes = BitConverter.GetBytes(length);
            buffer.Add(lengthBytes[2]);
            buffer.Add(lengthBytes[3]);
            //add command
            buffer.AddRange(command);
            //add parameter
            if (parameter != null)
            {
                buffer.AddRange(parameter);
            }
            //create checksum
            byte[] tmp = new byte[buffer.Count - 2];
            buffer.CopyTo(tmp, 2);
            buffer.Add(createCheckSum(tmp));

            return buffer.ToArray();
        }
        private byte createCheckSum(byte[] data)
        {
            int tmpSum=0;
            foreach (var item in data)
            {
                tmpSum += Convert.ToInt32(data);
            }
            return BitConverter.GetBytes(tmpSum)[3];
        }

        public void SendCommand( byte[] command, byte[] parameter)
        {
            writer.WriteBytes(createCommand(command, parameter));
        }

        public async Task Connect(Windows.Networking.HostName host, string pwd)
        {
            socket = new Windows.Networking.Sockets.StreamSocket();
            await socket.ConnectAsync(host, "8899");
            writer = new DataWriter(socket.OutputStream);
            reader = new DataReader(socket.InputStream);
            writer.WriteString(pwd);
            writer.WriteBytes(new byte[] { 0x0d, 0x0a });
            await writer.StoreAsync();
            string result=reader.ReadString(2);
            if (result!="OK")
            {
                throw new InvalidOperationException("Incorrect password");
            }
        }
        public void Disconnect()
        {
            reader.DetachStream();
            writer.DetachStream();
            socket.Dispose();
        }
        
    }
}
