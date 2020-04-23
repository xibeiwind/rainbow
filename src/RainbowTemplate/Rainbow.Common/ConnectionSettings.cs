namespace Rainbow.Common
{
    public class ConnectionSettings
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConnectionSettings" /> class.
        /// </summary>
        /// <param name="connectionString"></param>
        public ConnectionSettings(string connectionString)
        {
            DbConnectionString = connectionString;
        }

        /// <summary>
        ///     Gets or sets the hyperion database connection.
        /// </summary>
        /// <value>
        ///     The hyperion database connection.
        /// </value>
        public string DbConnectionString { get; }
    }
}