using System;
using System.Collections.Generic;
using System.Text;

namespace Bebbs.Mdc.Command
{
    public static class Power
    {
        private const byte OffData = 0x00;
        private const byte OnData = 0x01;

        private class Command : IDeconstructor
        {
            public byte Word => Words.PowerControl;

            public byte Id { get; set; }

            public byte[] Data { get; set; }
        }

        public static ICommand On(byte id)
        {
            return new Command { Id = id, Data = new[] { OnData } };
        }

        public static ICommand Off(byte id)
        {
            return new Command { Id = id, Data = new[] { OffData } };
        }
    }
}
