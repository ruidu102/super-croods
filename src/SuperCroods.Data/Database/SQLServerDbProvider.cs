using System.Data;
using System.Data.SqlClient;

namespace SuperCroods.Data.Database
{
    public class SQLServerDbProvider : IDbProvider
    {

        public IDbConnection Connection()
        {
            return new SqlConnection();
        }
    }
}
