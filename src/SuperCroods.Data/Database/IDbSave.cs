using System.Collections.Generic;

namespace SuperCroods.Data.Database
{
    public interface IDbSave<TEntity>
    {
        void Save(TEntity entity);
        void Save(IEnumerable<TEntity> entities);
    }
}
