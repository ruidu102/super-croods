namespace SuperCroods.Data.Database
{
    public interface IDbCommandText
    {
        IDbCommandText ReadOne();

        IDbCommandText ReadAll();

        IDbCommandText Save(bool arg);

        IDbCommandText DeleteOne();

        IDbCommandText DeleteAll();

        string Build();
    }
}
