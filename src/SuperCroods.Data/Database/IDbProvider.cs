using System.Data;

namespace SuperCroods.Data.Database
{
    public interface IDbProvider
    {
        IDbConnection Connection();
    }
}
