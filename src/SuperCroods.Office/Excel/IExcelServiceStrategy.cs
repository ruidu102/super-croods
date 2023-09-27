namespace SuperCroods.Office.Excel
{
    public interface IExcelServiceStrategy<TEntity> : IRead<TEntity>, IWrite<TEntity>
    {
    }
}
