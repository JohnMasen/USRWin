using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrWin.Core.Command
{
    public abstract class DeviceCommandBase:IDeviceCommand
    {
        byte cmd;
        protected byte[] Parameter { get; set; }
        private byte[] COMMAND_HEADER = new byte[2] { 0x55, 0xaa };
        private byte[] RESULT_HEADER = new byte[2] { 0xaa, 0x55 };
        
        public DeviceCommandBase(byte command)
        {
            cmd = command;
        }
        public abstract void SetResult(byte[] result);


        public byte[] CommandBuffer
        {
            get
            {
                return createCommand(cmd, Parameter);
            }
        }
        private byte[] createCommand(byte command, byte[] parameter)
        {
            int length;
            if (parameter == null)
            {
                length = 2;
            }
            else
            {
                length = parameter.Length+2;
            }
            List<byte> buffer = new List<byte>();
            buffer.AddRange(COMMAND_HEADER);

            //set the length 
            byte[] lengthBytes = BitConverter.GetBytes(length);
            buffer.Add(lengthBytes[1]);
            buffer.Add(lengthBytes[0]);
            //add channel
            buffer.Add(0);
            //add command
            buffer.Add(command);
            //add parameter
            if (parameter != null)
            {
                buffer.AddRange(parameter);
            }
            //create checksum
            byte[] tmp = new byte[buffer.Count - 2];
            buffer.CopyTo(2,tmp,0,tmp.Length);
            buffer.Add(createCheckSum(tmp));

            return buffer.ToArray();
        }
        private byte createCheckSum(byte[] data)
        {
            int tmpSum = 0;
            foreach (var item in data)
            {
                tmpSum += Convert.ToInt32(item);
            }
            return BitConverter.GetBytes(tmpSum)[0];
        }

        private byte[] parseResult(byte[] data, byte command)
        {
            if (data.SequenceEqual(new byte[] { 0x7f, 0x7f }))
            {
                throw new InvalidOperationException("Server busy");
            }

            if (data.SequenceEqual(new byte[] { 0x00, 0x00 }))
            {
                throw new InvalidOperationException("Unknown error");
            }

            if (data.Length < 7)
            {
                throw new InvalidOperationException("invalid result, payload should at least 7 bytes");
            }

            for (int i = 0; i < RESULT_HEADER.Length; i++)
            {
                if (RESULT_HEADER[i] != data[i])
                {
                    throw new InvalidOperationException("invalid result, incorrect header");
                }
            }

            byte[] tmp = new byte[4];
            Array.Copy(data, 2, tmp, 2, 2);
            int length = BitConverter.ToInt32(tmp, 0);
            if (length != data.Length - 3)
            {
                throw new InvalidOperationException("invalid result, data length does not match payload length");
            }

            byte resultCmd = data[5];
            if (resultCmd != command + 0x80)
            {
                throw new InvalidOperationException("invalid result, command not matching");
            }

            byte[] checkSumContent = new byte[data.Length - 3];
            Array.Copy(data, 2, checkSumContent, 0, data.Length - 3);
            byte checkSum = createCheckSum(checkSumContent);
            if (checkSum != data.Last())
            {
                throw new InvalidOperationException("invalid result, checksum error");
            }
            if (data.Length == 7)
            {
                return null;
            }
            else
            {
                byte[] result = new byte[data.Length - 7];
                Array.Copy(data, 6, result, 0, data.Length - 7);
                return result;
            }


        }
    }
}
