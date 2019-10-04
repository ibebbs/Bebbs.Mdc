using System;
using System.Collections.Generic;
using System.Text;

namespace Bebbs.Mdc.Command
{
    internal interface IDeconstructor : ICommand
    {
        byte Id { get; }

        byte Word { get; }

        byte[] Data { get; }
    }
}
