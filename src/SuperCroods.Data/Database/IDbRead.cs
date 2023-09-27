using System.Collections.Generic;

namespace SuperCroods.Data.Database
{
    public interface IDbRead<TEntity>
    {
        IEnumerable<TEntity> ReadAll(string table);
        IEnumerable<TEntity> ReadAll(string table, params object[] args);
        TEntity ReadOne(string table);
        TEntity ReadOne(string table, params object[] args);
    }
}
