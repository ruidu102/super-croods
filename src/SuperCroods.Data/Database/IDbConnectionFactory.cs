using System.Data;

namespace SuperCroods.Data.Database
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
