using System.Data;
using System.Data.SQLite;

namespace SuperCroods.Data.Database
{
    public class SQLiteDbProvider : IDbProvider
    {
        public IDbConnection Connection()
        {
            return new SQLiteConnection();
        }
    }
}
