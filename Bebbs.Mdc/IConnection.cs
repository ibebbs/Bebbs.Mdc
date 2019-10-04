using System;
using System.Threading.Tasks;

namespace Bebbs.Mdc
{
    public interface IConnection : IAsyncDisposable
    {
        ValueTask ConnectAsync();

        ValueTask IssueAsync(ICommand command);

        ValueTask DisconnectAsync();
    }
}
