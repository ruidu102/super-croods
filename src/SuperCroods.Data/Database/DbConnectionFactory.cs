using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace SuperCroods.Data.Database
{
    public class DbConnectionFactory
    {
        private DbProvider _provider;

        private IDbProvider provider;

        public DbConnectionFactory(IDbProvider arg)
        {
            provider = arg;
        }

        private DbConnectionFactory(string arg)
        {
            _provider = DbProvider.GetSetting(arg);
        }

        [Obsolete("This method is deprecated")]
        public static DbConnectionFactory Provider(string arg)
        {
            return new DbConnectionFactory(arg);
        }

        [Obsolete("This method is deprecated")]
        public IDbConnection CreateConnection(bool deprecated = true)
        {
            IDbConnection connection = null;

            if (_provider == DbProvider.SqlServer)
                connection = new SqlConnection();
            if (_provider == DbProvider.SQLite)
                connection = new SQLiteConnection();

            return connection;
        }

        public IDbConnection CreateConnection()
        {
            return provider.Connection();
        }

    }
}
