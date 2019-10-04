using System;
using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Mdc.Message
{
    public static class Factory
    {
        private static byte CalculateChecksum(byte[] message)
        {
            IEnumerable<byte> source = (message[0] == 0xAA) ? message.Skip(1) : message;

            var value = source.Sum(v => v).ToString("X2");

            var sum = Convert.ToByte(value.Substring(value.Length - 2, 2), 16);

            return sum;
        }

        public static byte[] Create(byte id, byte word, params byte[] data)
        {
            byte[] message = new byte[5 + data.Length];
            message[0] = 0xAA;
            message[1] = (byte)word;
            message[2] = id;
            message[3] = (byte)data.Length;

            if (data.Length > 0)
            {
                Array.Copy(data, 0, message, 4, data.Length);
            }

            message[4 + data.Length] = CalculateChecksum(message);

            return message;
        }
    }
}
