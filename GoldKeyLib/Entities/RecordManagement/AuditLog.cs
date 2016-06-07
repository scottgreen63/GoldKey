using System;
using System.Data;
using GoldKeyLib.DA;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public class AuditLog : BaseEntityList<AuditLogEntry>
    {
        #region Data

        //Database Field Names
        private const string cn_fUserId = "fldi_User_Id";

        private const string cn_fWorkstation = "fldc_WorkStation";
        private const string cn_fEventDateTime = "fldd_EventDateTime";
        private const string cn_fMessageType = "fldc_MessageType";
        private const string cn_fSeverity = "fldc_Severity";
        private const string cn_fModule = "fldc_Module";
        private const string cn_fFunction = "fldc_Function";
        private const string cn_fDescription = "fldv_Description";

        //StoredProcedure Names
        private const string cn_spList = "pr_ListAuditLog";

        private const string cn_spPurge = "pr_PurgeAuditLog";

        //Parameters'
        private const string cn_pmLoggedUserId = "@pLoggedUserId";
        private const string cn_pmStartDate = "@pStartDate";

        private const string cn_pmEndDate = "@pEndDate";
        private const string cn_pmShowDebug = "@pShowDebug";
        private const string cn_pmDescription = "@pDescription";

        #endregion Data

        #region Private Attributes

        private static AuditLog mInstance = null;
        private static readonly object padlock = new object();
        private static DateTime mStartDate = System.DateTime.Now.AddMonths(-3);
        private static DateTime mEndDate = System.DateTime.Now;
        private static bool mShowDebug = false;
        private static string mSearchText = "%";

        #endregion Private Attributes

        #region Properties
            
            public DateTime StartDate
            {
                get
                { return mStartDate; }
                set
                {
                    if (mStartDate != value)
                    {
                        mStartDate = value;
                    }
                }
            }
            
            public DateTime EndDate
            {
                get
                { return mEndDate; }
                set
                {
                    if (mEndDate != value)
                    {
                        mEndDate = value;
                    }
                }
            }
            
            public bool ShowDebug
            {
                get
                { return mShowDebug; }
                set
                {
                    if (mShowDebug != value)
                    {
                        mShowDebug = value;
                    }
                }
            }
            
            public string SearchText
            {
                get
                { return mSearchText; }
                set
                {
                    if (mSearchText != value)
                    {
                        mSearchText = value;
                    }
                }
            }

        #endregion Properties

        #region Constructors

        private static AuditLog Load()
        {
            string sStartDate = mStartDate.Date.ToString("MM/dd/yyyy");
            string sEndDate = mEndDate.Date.ToString("MM/dd/yyyy");
            string sShowDebug = mShowDebug.ToString();
            string sSearchText = mSearchText;
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spList, DABase.Instance.Parameter(cn_pmStartDate, sStartDate), DABase.Instance.Parameter(cn_pmEndDate, sEndDate), DABase.Instance.Parameter(cn_pmShowDebug, sShowDebug), DABase.Instance.Parameter(cn_pmDescription, sSearchText));
            foreach (DataRow dRow in dt.Rows)
            {
                AuditLogEntry entry = new AuditLogEntry(dRow);
                mInstance.Add(entry);
            }
            return mInstance;
        }

        public static AuditLog Listing
        {
            get
            {
                lock (padlock)
                {
                    if (mInstance == null)
                    {
                        mInstance = new AuditLog();
                    }
                    return mInstance;
                }
            }
        }

        #endregion Constructors

        #region Methods

        public void Purge(int iUserId, System.DateTime dStartDate)
        {
            StartDate = dStartDate;
            DABase.Instance.ExecSP(cn_spPurge, 
                DABase.Instance.Parameter(cn_pmLoggedUserId, iUserId), 
                DABase.Instance.Parameter(cn_pmStartDate, mStartDate));
            Load();
        }

        public AuditLog List()
        {
            if (mInstance == null)
            {
                mInstance = new AuditLog();
            }

            mInstance.ClearItems();
            return Load();
        }

        #endregion Methods
    }
}