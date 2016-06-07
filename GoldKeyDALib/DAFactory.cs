using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Permissions;


namespace GoldKeyLib.DA
{
    internal class DbProviderFactory : IDAProviderFactory
    {
        private System.Data.Common.DbProviderFactory _instance;

        public static readonly IDAProviderFactory Instance = new DbProviderFactory(ConfigurationManager.ConnectionStrings["DA"]);

        private ConnectionStringSettings settings;

        private DbProviderFactory(ConnectionStringSettings settings)
        {
            if (settings != null)
            {
                _instance = System.Data.Common.DbProviderFactories.GetFactory(settings.ProviderName);//(settings.ProviderName);

                this.settings = settings;
            }
        }

        public IDbConnection CreateConnection()
        {
            string connstr = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["DA"];

            if (settings != null)
                connstr = settings.ConnectionString;



            IDbConnection conn = _instance.CreateConnection();
            conn.ConnectionString = connstr;//settings.ConnectionString;
            return conn;
        }

        public IDbCommand CreateCommand()
        {
            IDbCommand cmd = _instance.CreateCommand();
            return cmd;
        }

        public IDbDataAdapter CreateAdapter()
        {
            IDbDataAdapter adr = _instance.CreateDataAdapter();
            return adr;
        }

        public IDbDataParameter CreateParameter()
        {
            IDbDataParameter prm = _instance.CreateParameter();
            return prm;
        }

        private bool CanRequestNotifications()
        {
            // In order to use the callback feature of the
            // SqlDependency, the application must have
            // the SqlClientPermission permission.
            try
            {
                SqlClientPermission perm =
                    new SqlClientPermission(
                    PermissionState.Unrestricted);

                perm.Demand();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}