using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using GoldKeyLib.DA;

namespace GoldKeyLib.DA
{
    public class DAException : ApplicationException
    {
        private const bool mPermitLogging = false;

        public enum DAExceptionCode
        {
            Concurrency,
            ConnectionFailure,
            DatabaseFailure,
            LoginFailure,
            Constraint,
            Transaction,
            Locks,
            General
        }

        private DAExceptionCode mExceptionCode;
        private DataSet mDataSet = new DataSet();
        private DataRow[] mErrorRow;
        private StringBuilder mLog = new StringBuilder();

        public DAException(Exception exc)
            : base(exc.Message.ToString(), exc)
        {
            if (exc is SqlException)
            {
                //recast as sqlexception...
                SqlException sqlexc = (SqlException)exc;
                ProcessSQLException(sqlexc);
            }
        }

        public DAException(string msg, Exception exc, DataSet ds = null)
            : base(msg, exc)
        {
            mDataSet = ds;
            if (exc is ConstraintException)
            {
                mLog.Append("Constraint Exception occured\r\t");
                mExceptionCode = DAExceptionCode.Constraint;
            }
            else if (exc is DBConcurrencyException)
            {
                mLog.Append("Concurrency Exception occured\r\t");
                mExceptionCode = DAExceptionCode.Concurrency;
            }
            else if (exc is SqlException)
            {
                //mLog.Append("SQL Client Exception occured" & vbCrLf)
                //recast as sqlexception...
                SqlException sqlexc = (SqlException)exc;
                ProcessSQLException(sqlexc);
            }
            else if (exc is DataException)
            {
                mLog.Append("Constraint Exception occured\r\t");
                mExceptionCode = DAExceptionCode.Constraint;
            }
            else if (exc is Exception)
            {
                mExceptionCode = DAExceptionCode.General;
            }

            if (ds != null)
            {
                DetectRowErrors(ds);
                mLog.Append("Original Error:\r\t");
                mLog.Append(exc.ToString());
                mLog.Append(mExceptionCode.ToString());
                LogException(mLog.ToString());
            }

            //Dim str As String = ""
            //str = "Source : " & ex.Source & ControlChars.NewLine
            //str &= "Number : " & ex.Number & ControlChars.NewLine
            //str &= "Message : " & ex.Message & ControlChars.NewLine
            //str &= "Class : " & ex.Class.ToString() & ControlChars.NewLine
            //str &= "Procedure : " & ex.Procedure & ControlChars.NewLine
            //str &= "Line number : " & ex.LineNumber.ToString() & ControlChars.NewLine
            //str &= "Server : " & ex.Server
            //str &= "Database Exception" & str
            //'WriteToEventLog(str)

            //'Dim e As New BaseDAException(ex.Message, ex)
            //Catch ex As System.Exception
            //    Dim str As String
            //    Str = "Source : " & ex.Source
            //    Str &= ControlChars.NewLine
            //    Str &= "Exception Message : " & ex.Message
            //    str &= "General Exception" & str
            //    'WriteToEventLog(str)
            //    MsgBox(str)
        }

        //string str = "";
        //        str = "Source : " + exc.Source + "\r\t";
        //        ///str += "Number : " + exc.Number + "\r\t";
        //        str += "Message : " + exc.Message + "\r\t";
        //        //str += "Class : " + exc.Class.ToString() + "\r\t";
        //        //str += "Procedure : " + exc.Procedure + "\r\t";
        //        //str += "Line number : " + exc.LineNumber.ToString() + "\r\t";
        //        //str += "Server : " + exc.Server;
        //        str += "Database Exception" + str;

        private void ProcessSQLException(SqlException exc)
        {
            /*************************************************
            *
            * 4060-4 could not open database
            * 18450 - 18461 - Login Failure
            * 18482, 3, 5 - could not connect to server
            * 547 - foreign key
            * 2627 - Index / Constraint
            * 2601 - Index/Constraint
            * 1201 1223 Locks
            * 2502 transaction startup
            * 2520-5 could not find database
            *
            ***************************************************/

            try
            {
                if (exc.Number >= 4060 & exc.Number <= 4064)
                {
                    mLog.Append("Database Failure occured\r\t");
                    mExceptionCode = DAExceptionCode.DatabaseFailure;
                }
                else if (exc.Number >= 18450 & exc.Number <= 18461)
                {
                    mLog.Append("Login Failure occured\r\t");
                    mExceptionCode = DAExceptionCode.LoginFailure;
                }
                else if (exc.Number == 18482 | exc.Number == 18483 | exc.Number == 18485)
                {
                    mLog.Append("Connection Failure occured\r\t");
                    mExceptionCode = DAExceptionCode.ConnectionFailure;
                }
                else if (exc.Number == 2627 | exc.Number == 2601 | exc.Number == 547)
                {
                    mLog.Append("Constraint Exception occured\r\t");
                    mExceptionCode = DAExceptionCode.Constraint;
                }
                else if (exc.Number == 1201 & exc.Number <= 1223)
                {
                    mLog.Append("Lock Exception occured\r\t");
                    mExceptionCode = DAExceptionCode.Locks;
                }
                else if (exc.Number == 2502)
                {
                    mLog.Append("Transaction Exception occured\r\t");
                    mExceptionCode = DAExceptionCode.Transaction;
                }
                else if (exc.Number == 2520 & exc.Number <= 2525)
                {
                    mLog.Append("Database Failure occured\r\t");
                    mExceptionCode = DAExceptionCode.DatabaseFailure;
                }
                else if (exc.Number >= 50000)
                {
                    mLog.Append("Primary Key issue\r\t");
                    mExceptionCode = DAExceptionCode.General;
                }

                WriteToEventLog(mLog.ToString());
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LogException(string sMsg)
        {
            Debug.WriteLine(sMsg);
            string appName = "";
            try
            {
                appName = System.Configuration.ConfigurationManager.GetSection("Application Name").ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            try
            {
                if (appName != "")
                    if (!EventLog.SourceExists(appName))
                    {
                        EventLog.CreateEventSource(appName, appName);
                        EventLog.WriteEntry(appName, sMsg, EventLogEntryType.Error);
                    }
                    else
                    {
                        EventLog.WriteEntry(appName, sMsg, EventLogEntryType.Error);
                    }
                else
                {
                    EventLog.WriteEntry("DAException", sMsg, EventLogEntryType.Error);
                }

                string LogName = "Application";
                if (!EventLog.SourceExists(LogName))
                {
                    EventLog.CreateEventSource(LogName, LogName);
                }

                EventLog Log = new EventLog();
                Log.Source = LogName;
                Log.WriteEntry(sMsg, EventLogEntryType.Error);

                {
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static void WriteToEventLog(string sMsg)
        {
            EventLog log = new EventLog("Application", System.Net.Dns.GetHostName().ToString(), "PWS");
            log.WriteEntry(sMsg, EventLogEntryType.Warning, 123);
        }

        private void DetectRowErrors(DataSet ds)
        {
            DataRow row = null;
            DataTable table = null;
            DataColumn column = null;

            if ((ds == null))
                return;
            if (ds.HasErrors)
            {
                mLog.Append("**************************************** \r\t");
                mLog.Append(" Errors Detected in Dataset: \r\t");
                mLog.Append("\r\t");
                foreach (DataTable tbl in ds.Tables)
                {
                    table = tbl;
                    if (table.HasErrors)
                    {
                        mErrorRow = table.GetErrors();
                        mLog.Append("Entity: " + table.TableName + "\r\t");
                        foreach (DataRow row_loopVariable in table.Rows)
                        {
                            row = row_loopVariable;
                            if (row.HasErrors)
                            {
                                mLog.Append("\tKey: " + row[table.PrimaryKey[0]] + "\r\t");
                                mLog.Append("\t" + row.RowError + "\r\t");
                                mLog.Append("\r\t");
                                foreach (DataColumn column_loopVariable in row.GetColumnsInError())
                                {
                                    column = column_loopVariable;
                                    mLog.Append("\t\tColumnName: " + column.ColumnName + "\r\t");
                                    mLog.Append("\t\tError: " + row.GetColumnError(column) + "\r\t");
                                    mLog.Append("\r\t");
                                }
                            }
                        }
                    }
                }
            }
        }

        public DataSet OriginalDataSet
        {
            get { return mDataSet; }
        }

        public DataRow[] RowsWithErrors
        {
            get { return mErrorRow; }
        }
    }

    public class DAAuditLogException
    {
        #region "Database Field Names, StoredProcedures & Parameters"

        //'Database Field Names
        private const string cn_fErrorId = "fldi_Error_Id";

        private const string cn_fUserName = "fldc_UserName";
        private const string cn_fErrorMessage = "fldc_ErrorMessage";
        private const string cn_fErrorDateTime = "fldd_ErrorDateTime";
        private const string cn_fStackTrace = "fldc_StackTrace";

        // StoredProcedure Names
        private const string cn_spAdd = "pr_AddAuditLogEntry";

        //private const string cn_spModify = "pr_"
        //private const string cn_spDelete = "pr_"   SAVED FOR LATER USE
        //private const string cn_spList = "pr_"
        //private const string cn_spGet = "pr_"

        //'Parameters'

        private const string cn_pmUserName = "@pUserId";
        private const string cn_pmMessageType = "@pMessageType";
        private const string cn_pmSeverity = "@pSeverity";
        private const string cn_pmModule = "@pModule";
        private const string cn_pmFunction = "@pFunction";
        private const string cn_pmDescription = "@pDescription";

        #endregion "Database Field Names, StoredProcedures & Parameters"

        #region "Private Attributes"

        private string msUserName;
        private string msMessageType;
        private string msSeverity;
        private string msModule;
        private string msFunction;
        private string msDescription;

        #endregion "Private Attributes"

        #region "Public Attributes"

        public string UserName
        {
            get { return msUserName; }
            set { msUserName = value; }
        }

        public string MessageType
        {
            get { return msMessageType; }
            set { msMessageType = value; }
        }

        public string Severity
        {
            get { return msSeverity; }
            set { msSeverity = value; }
        }

        public string Module
        {
            get { return msModule; }
            set { msModule = value; }
        }

        public string Function
        {
            get { return msFunction; }
            set { msFunction = value; }
        }

        public string Description
        {
            get { return msDescription; }
            set { msDescription = value; }
        }

        #endregion "Public Attributes"

        #region "Methods Etc."

        public void AddAuditLogEntry(Exception ex)
        {
            DataSet ds = new DataSet();
            try
            {
                UserName = System.Environment.UserName;
                MessageType = ex.Message;
                Severity = ex.StackTrace;
                Module = ex.Source;
                Function = ex.StackTrace.ToString();
                Description = ex.Message;

                DABase.Instance.ExecSP(cn_spAdd,
                    DABase.Instance.Parameter(cn_pmUserName, msUserName),
                    DABase.Instance.Parameter(cn_pmMessageType, msMessageType),
                    DABase.Instance.Parameter(cn_pmSeverity, msSeverity),
                    DABase.Instance.Parameter(cn_pmModule, msModule),
                    DABase.Instance.Parameter(cn_pmFunction, msFunction),
                    DABase.Instance.Parameter(cn_pmDescription, msDescription));
            }
            catch
            {
                DAException.WriteToEventLog(ex.Message);
            }
        }

        #endregion "Methods Etc."
    }

    //#Region "Methods Etc"
    //    Public Sub AddAuditLogEntry(ByVal ex As Exception)
    //        Dim ds As New DataSet

    //        Try
    //            UserName = My.User.Name
    //            ErrorMessage = ex.Message
    //            ErrorDateTime = Now
    //            StackTrace = ex.StackTrace

    //            BaseDA.ExecSP(cn_spAdd, BaseDA.Parameter(cn_pmUserName, msUserName) _
    //                            , BaseDA.Parameter(cn_pmErrorMessage, msErrorMessage) _
    //                            , BaseDA.Parameter(cn_pmErrorDateTime, mdErrorDateTime) _
    //                            , BaseDA.Parameter(cn_pmStackTrace, msStackTrace))
    //        Catch ext As Exception
    //            Dim msg As String = "AppError"

    //            Throw New BaseDAException(msg, ex, ds)
    //        End Try
    //    End Sub
    //#End Region

}