using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Storage.Streams;

namespace UsrWin.Core.Command
{
    public class DeviceTCPHelper
    {
        Windows.Networking.Sockets.StreamSocket socket;
        DataWriter writer;
        DataReader reader;

        private byte[] COMMAND_HEADER = new byte[2] { 0x55, 0xaa };
        private byte[] RESULT_HEADER = new byte[2] { 0xaa, 0x55 };
        public TimeSpan CommandTimeout { get; set; }
        public DeviceTCPHelper()
        {
            CommandTimeout = TimeSpan.FromSeconds(3);
        }
        //private byte[] createCommand(byte command,byte[] parameter)
        //{
        //    int length;
        //    if (parameter==null)
        //    {
        //        length = COMMAND_HEADER.Length + 5;
        //    }
        //    else
        //    {
        //        length = COMMAND_HEADER.Length + 5 + parameter.Length ;
        //    }
        //    List<byte> buffer = new List<byte>();
        //    buffer.AddRange(COMMAND_HEADER);
            
        //    //set the length 
        //    byte[] lengthBytes = BitConverter.GetBytes(length);
        //    buffer.Add(lengthBytes[2]);
        //    buffer.Add(lengthBytes[3]);
        //    //add channel
        //    buffer.Add(0);
        //    //add command
        //    buffer.Add(command);
        //    //add parameter
        //    if (parameter != null)
        //    {
        //        buffer.AddRange(parameter);
        //    }
        //    //create checksum
        //    byte[] tmp = new byte[buffer.Count - 2];
        //    buffer.CopyTo(tmp, 2);
        //    buffer.Add(createCheckSum(tmp));

        //    return buffer.ToArray();
        //}
        //private byte createCheckSum(byte[] data)
        //{
        //    int tmpSum=0;
        //    foreach (var item in data)
        //    {
        //        tmpSum += Convert.ToInt32(data);
        //    }
        //    return BitConverter.GetBytes(tmpSum)[3];
        //}

        //public async Task<byte[]> SendCommand(HostName host, byte command, byte[] parameter)
        //{
        //    using (socket=new Windows.Networking.Sockets.StreamSocket())
        //    {
        //        await Connect(host, "admin");
        //        byte[] tmp = await sendReceive(createCommand(command, parameter), CommandTimeout);
        //        return parseResult(tmp, command);
        //    }
            
        //}

        //private byte[] parseResult(byte[] data, byte command)
        //{
        //    if (data.SequenceEqual(new byte[] { 0x7f, 0x7f }))
        //    {
        //        throw new InvalidOperationException("Server busy");
        //    }

        //    if (data.SequenceEqual(new byte[] { 0x00, 0x00 }))
        //    {
        //        throw new InvalidOperationException("Unknown error");
        //    }

        //    if (data.Length<7)
        //    {
        //        throw new InvalidOperationException("invalid result, payload should at least 7 bytes");
        //    }

        //    for (int i = 0; i < RESULT_HEADER.Length; i++)
        //    {
        //        if (RESULT_HEADER[i]!=data[i])
        //        {
        //            throw new InvalidOperationException("invalid result, incorrect header");
        //        }
        //    }

        //    byte[] tmp = new byte[4];
        //    Array.Copy(data, 2, tmp, 2, 2);
        //    int length = BitConverter.ToInt32(tmp, 0);
        //    if (length!=data.Length-3)
        //    {
        //        throw new InvalidOperationException("invalid result, data length does not match payload length");
        //    }

        //    byte resultCmd = data[5];
        //    if (resultCmd!=command+0x80)
        //    {
        //        throw new InvalidOperationException("invalid result, command not matching");
        //    }

        //    byte[] checkSumContent = new byte[data.Length - 3];
        //    Array.Copy(data, 2, checkSumContent, 0, data.Length - 3);
        //    byte checkSum = createCheckSum(checkSumContent);
        //    if (checkSum!=data.Last())
        //    {
        //        throw new InvalidOperationException("invalid result, checksum error");
        //    }
        //    if (data.Length==7)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        byte[] result = new byte[data.Length - 7];
        //        Array.Copy(data, 6, result, 0, data.Length - 7);
        //        return result;
        //    }
            

        //}

        private async Task Connect(HostName host, string pwd)
        {
            await socket.ConnectAsync(host, "8899");
            writer = new DataWriter(socket.OutputStream);
            reader = new DataReader(socket.InputStream);
            reader.InputStreamOptions = InputStreamOptions.Partial;
            byte[] data = Encoding.UTF8.GetBytes(pwd+Environment.NewLine);
            byte[] result=await sendReceive(data, TimeSpan.FromSeconds(3));
            if (!result.SequenceEqual(new byte[]{ 0x4f,0x4b}))
            {
                throw new InvalidOperationException("Invalid password");
            }
        }

        public async Task SendCommands(HostName host,List<IDeviceCommand> commands,string pwd)
        {
            using (socket=new Windows.Networking.Sockets.StreamSocket())
            {
                try
                {
                    await Connect(host, pwd);
                    if (commands == null)
                    {
                        return;
                    }
                    foreach (var item in commands)
                    {
                        var result = await sendReceive(item.CommandBuffer, CommandTimeout);
                        item.SetResult(result);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
        }

        private async Task<byte[]> sendReceive(byte[] buffer,TimeSpan timeout)
        {
            System.Diagnostics.Debug.WriteLine("send data {0}", BitConverter.ToString(buffer));
            await send(buffer);
            return await receive(timeout);
        }

        private async Task send(byte[] buffer)
        {
            writer.WriteBytes(buffer);
            await writer.StoreAsync();//send data
        }

        private async Task<byte[]> receive(TimeSpan timeout)
        {
            CancellationTokenSource cts = new CancellationTokenSource(timeout);
            var t = reader.LoadAsync(99).AsTask(cts.Token);
            
            uint length= await t; 
            byte[] output = new byte[length];
            reader.ReadBytes(output);
            System.Diagnostics.Debug.WriteLine("received data {0}", BitConverter.ToString(output));
            return output;
        }
        
    }
}
