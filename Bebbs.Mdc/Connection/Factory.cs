using System;
using System.Collections.Generic;
using System.Text;

namespace Bebbs.Mdc.Connection
{
    public static class Factory
    {
        public static IConnection ForSerialPort(string portName)
        {
            return new Serial(portName);
        }
    }
}
