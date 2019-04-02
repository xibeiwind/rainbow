using System.Data.Common;
using Microsoft.Extensions.Logging;
using Yunyong.Core;
using Yunyong.EventBus;

namespace Rainbow.Common
{
    /// <summary>
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// </summary>
        /// <param name="connectionSettings"></param>
        /// <param name="connectionFactory"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="eventBus"></param>
        protected ServiceBase(ConnectionSettings connectionSettings, IConnectionFactory connectionFactory,
            ILoggerFactory loggerFactory, IEventBus eventBus)
        {
            ConnectionSettings = connectionSettings;
            ConnectionFactory = connectionFactory;
            EventBus = eventBus;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        private ConnectionSettings ConnectionSettings { get; }
        private IConnectionFactory ConnectionFactory { get; }

        /// <summary>
        ///     EventBus
        /// </summary>
        protected IEventBus EventBus { get; }

        /// <summary>
        ///     Logger
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        ///     Gets the connection.
        /// </summary>
        /// <returns></returns>
        protected DbConnection GetConnection()
        {
            var conn = ConnectionFactory.GetConnection(ConnectionSettings.DbConnectionString);
            //conn.Open();
            return conn;
        }
    }
}