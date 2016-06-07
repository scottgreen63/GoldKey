using System.Data;

namespace GoldKeyLib.DA
{
    public interface IDAProviderFactory
    {
        IDbConnection CreateConnection();

        IDbCommand CreateCommand();

        IDbDataAdapter CreateAdapter();
    }
}