using Microsoft.Extensions.Logging;

namespace Rainbow.Common
{
    public interface ILoggerExtendable
    {
        ILogger Logger { get; }
    }
}