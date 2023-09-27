namespace SuperCroods.Data.Database
{
    public interface IDbServiceStrategy<TEntity> : 
        IDbRead<TEntity>, IDbDelete<TEntity>, IDbSave<TEntity>
    {
        string DbVersion();
        bool IsConnected { get; }
    }
}
