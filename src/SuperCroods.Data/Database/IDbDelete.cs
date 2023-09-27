namespace SuperCroods.Data.Database
{
    public interface IDbDelete<TEntity>
    {
        void Delete(string table, TEntity entity);
        void DeleteAll(string table);
    }
}
