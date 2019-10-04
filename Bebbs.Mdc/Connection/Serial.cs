using System;
using System.Threading.Tasks;

namespace Bebbs.Mdc.Connection
{
    public class Serial : IConnection
    {
        private const int BaudRate = 9600;
        private const System.IO.Ports.Parity Parity = System.IO.Ports.Parity.None;
        private const int DataBits = 8;
        private const System.IO.Ports.StopBits StopBits = System.IO.Ports.StopBits.One;
        
        private string _portName;
        private System.IO.Ports.SerialPort _port;

        public Serial(string portName)
        {
            _portName = portName;
        }

        public ValueTask DisposeAsync()
        {
            return DisconnectAsync();
        }

        public ValueTask ConnectAsync()
        {
            _port = new System.IO.Ports.SerialPort(_portName, BaudRate, Parity, DataBits, StopBits);

            try
            {
                _port.Open();

                return default;
            }
            catch
            {
                _port.Dispose();
                _port = null;

                throw;
            }
        }

        public ValueTask DisconnectAsync()
        {
            if (_port != null)
            {
                try
                {
                    _port.Close();
                    _port.Dispose();
                }
                finally
                {
                    _port = null;
                }
            }

            return default;
        }

        private ValueTask IssueAsync(Command.IDeconstructor command)
        {
            var message = Message.Factory.Create(command.Id, command.Word, command.Data);
            _port.Write(message, 0, message.Length);

            var response = new byte[1024];
            int read = _port.Read(response, 0, 1024);

            return default;
        }

        public ValueTask IssueAsync(ICommand command)
        {
            if (_port == null)
            {
                throw new InvalidOperationException("Not yet connected. Call ConnectAsync() before IssueAsync()");
            }

            return command switch
            {
                Command.IDeconstructor deconstructor => IssueAsync(deconstructor),
                _ => throw new ArgumentException("Unknown command", nameof(command)),
            };
        }
    }
}
