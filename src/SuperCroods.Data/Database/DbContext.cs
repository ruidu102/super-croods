using System.Collections.Generic;

namespace SuperCroods.Data.Database
{
    public class DbContext<TEntity>
    {
        private IDbServiceStrategy<TEntity> strategy;

        public void SetStrategy(IDbServiceStrategy<TEntity> arg)
        {
            strategy = arg;
        }

        public IEnumerable<TEntity> ReadAll(string table)
        {
            return strategy.ReadAll(table);
        }
        
        public IEnumerable<TEntity> ReadAll(string table, params object[] args)
        {
            return strategy.ReadAll(table, args);
        }

        public TEntity ReadOne(string table)
        {
            return strategy.ReadOne(table);
        }

        public void Save(TEntity entity)
        {
            strategy.Save(entity);
        }

        public void Save(IEnumerable<TEntity> entities)
        {
            strategy.Save(entities);
        }

        public void Delete(string table, TEntity entity)
        {
            strategy.Delete(table, entity);
        }

        public void DeleteAll(string table)
        {
            strategy.DeleteAll(table);
        }

        public string GetDbVersion => strategy.DbVersion();

        public bool IsConnected => strategy.IsConnected;
    }
}
