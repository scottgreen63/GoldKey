/***********************************************************************************************
*
*Author      : Scott Green
*Date        :
*Version     :
*Description : DABase in C#
*
*
************************************************************************************************/

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace GoldKeyLib.DA
{
    public sealed class DABase
    {
        private static DABase _instance = null;
        private static readonly object mlock = new object();

        private DABase()
        { }

        public static DABase Instance
        {
            get
            {
                lock (mlock)
                {
                    if (_instance == null)
                    {
                        _instance = new DABase();
                    }
                    return _instance;
                }
            }
        }

        private DABase(IDAProviderFactory providerfactory)
        {
            _instance = new DABase(DbProviderFactory.Instance);
        }

        //public DataTable ExecInline(string sQuery, params IDbDataParameter[] arrParams)
        //{
        //    DataTable dt = null;
        //    using (IDbConnection cnn = DbProviderFactory.Instance.CreateConnection())
        //    {
        //        IDbCommand cmd = DbProviderFactory.Instance.CreateCommand();
        //        cmd.Connection = cnn;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = sQuery;
        //        if (arrParams != null)
        //        {
        //            foreach (IDbDataParameter param in arrParams)
        //            {
        //                cmd.Parameters.Add(param);
        //            }
        //        }

        //        IDataReader reader = cmd.ExecuteReader();

        //        dt = new DataTable();

        //        try
        //        {
        //            dt.Load(reader);
        //        }
        //        catch (DAException ex)
        //        {
        //            throw ex;
        //            //int i = 0;
        //            //StringBuilder errorMessages = new StringBuilder();

        //            //for (i = 0; i <= ex.Errors.Count - 1; i++)
        //            //{
        //            //    errorMessages.Append("Index #" + i.ToString() + "\r" + "Message: " + ex.Errors(i).Message + "\r" + "LineNumber: " + ex.Errors.this.LineNumber + "\r" + "Source: " + ex.Errors(i).Source + "\r" + "Procedure: " + ex.Errors(i).Procedure + "\r");
        //            //}
        //            ////Console.WriteLine(errorMessages.ToString())
        //            //EventLog log = new EventLog("Application", Computer.Name, "PWS");
        //            //log.WriteEntry(errorMessages.ToString(), EventLogEntryType.Warning, 123);
        //        }
        //    }
        //    return dt;
        //}

        public DataTable ExecSP(string sSProc, params IDbDataParameter[] arrParams)
        {
            DataTable dt = null;

            using (IDbConnection cnn = DbProviderFactory.Instance.CreateConnection())
            {
                try
                {
                    cnn.Open();

                    IDbCommand cmd = DbProviderFactory.Instance.CreateCommand();

                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sSProc;

                    if (arrParams != null)
                    {
                        foreach (IDbDataParameter param in arrParams)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    IDataReader reader = cmd.ExecuteReader();

                    dt = new DataTable();
                    dt.Load(reader);
                }
                catch (Exception ex)
                {
                    throw new DAException(ex);
                }

                return dt;
            }
        }

        public DbParameter Parameter(string parmName, object parmValue)
        {
            DbParameter parm = new SqlParameter();
            parm.ParameterName = parmName;
            parm.Value = parmValue;
            return parm;
        }
    }
}