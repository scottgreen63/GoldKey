using System;
using System.Data;
using GoldKeyLib.DA;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public class RecordLock : BaseEntity
    {
        #region Data

        //'Database Field Names
        private const string cn_fUseId = "fldi_User_Id";

        private const string cn_fTableName = "fldc_TableName";
        private const string cn_fKeyValue = "fldc_KeyValue";
        private const string cn_fLockTime = "fldd_LockTime";
        private const string cn_fLockedBy = "fldc_LockedBy";
        private const string cn_fLockedFrom = "fldc_LockedFrom";

        //'Lock StoredProcedure Names
        private const string cn_spAdd = "pr_AddLock";

        private const string cn_spDelete = "pr_DeleteLock";
        private const string cn_spGet = "pr_GetLock";
        private const string cn_spReleaseAllLocks = "pr_ReleaseAllLocks";
        private const string cn_spVerifyLock = "pr_VerifyLock";

        //'Lock Stored Proc Parameters'
        private const string cn_pmUserId = "@UserId";

        private const string cn_pmTableName = "@TableName";
        private const string cn_pmKeyValue = "@KeyValue";

        #endregion Data

        #region Private Attributes

        private User miUser = null;
        private string msTableName;
        private string msKeyValue;
        private DateTime mdLockTime;
        private string msLockedBy;
        private string msLockedFrom;

        //'Lock Property Names'
        private const string cn_pnUserId = "UserId";

        private const string cn_pnTableName = "TableName";
        private const string cn_pnKeyValue = "KeyValue";
        private const string cn_pnLockTime = "LockTime";
        private const string cn_pnLockedBy = "LockedBy";
        private const string cn_pnLockedFrom = "LockedFrom";

        #endregion Private Attributes

        #region Properties

        public User User
        {
            get
            { return miUser; }
            set
            {
                if (miUser != value)
                {
                    miUser = value;
                }
            }
        }

        public string TableName
        {
            get
            { return msTableName; }
            set
            {
                if (msTableName != value)
                {
                    //string sPropName = cn_pnTableName;
                    msTableName = value.ToUpper();
                }
            }
        }

        public string KeyValue
        {
            get
            { return msKeyValue; }
            set
            {
                if (msKeyValue != value)
                {
                    //string sPropName = cn_pnKeyValue;
                    msKeyValue = value.ToUpper();
                }
            }
        }

        public DateTime LockTime
        {
            get
            { return mdLockTime; }
            set
            {
                if (mdLockTime != value)
                {
                    //string sPropName = cn_pnLockTime;
                    mdLockTime = value;
                }
            }
        }

        public string LockedBy
        {
            get
            { return msLockedBy; }
            set
            {
                if (msLockedBy != value)
                {
                    //string sPropName = cn_pnLockedBy;
                    msLockedBy = value.ToUpper();
                }
            }
        }

        public string LockedFrom
        {
            get
            { return msLockedFrom; }
            set
            {
                if (msLockedFrom != value)
                {
                    //string sPropName = cn_pnLockedFrom;
                    msLockedFrom = value.ToUpper();
                }
            }
        }

        #endregion Properties

        #region Constructors

        public RecordLock()
        { }

        public RecordLock(DataRow dr)
        {
            this.Populate(dr);
        }

        private void Populate(DataRow dr)
        {
            this.User = Users.Find(Convert.ToInt32(dr[cn_fUseId].ToString()));
            this.TableName = dr[cn_fTableName].ToString();
            this.KeyValue = dr[cn_fKeyValue].ToString();
            this.LockTime = Convert.ToDateTime(dr[cn_fLockTime].ToString());
            this.LockedBy = dr[cn_fLockedBy].ToString();
            this.LockedFrom = dr[cn_fLockedFrom].ToString();
            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods

        public static bool AddLock(int iUserId, string sTableName, string sKeyValue)
        {
            try
            {
                int iLock = 999;
                DataTable dt = new DataTable();
                dt = DABase.Instance.ExecSP
            (cn_spAdd,
            DABase.Instance.Parameter(cn_pmUserId, iUserId),
            DABase.Instance.Parameter(cn_pmTableName, sTableName),
            DABase.Instance.Parameter(cn_pmKeyValue, sKeyValue));

                foreach (DataRow dRow in dt.Rows)
                {
                    iLock = Convert.ToInt32(dRow[0]);
                }
                return Convert.ToBoolean(iLock);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static void DeleteLock(int iUserId, string sTableName, string sKeyValue)
        {
            DABase.Instance.ExecSP(cn_spDelete,
            DABase.Instance.Parameter(cn_pmUserId, iUserId),
            DABase.Instance.Parameter(cn_pmTableName, sTableName),
            DABase.Instance.Parameter(cn_pmKeyValue, sKeyValue));
        }

        public static RecordLock GetLock(int iUserId, string sTableName, string sKeyValue)
        {
            DataTable dt;
            dt =
            DABase.Instance.ExecSP(cn_spGet,
            DABase.Instance.Parameter(cn_pmUserId, iUserId),
            DABase.Instance.Parameter(cn_pmTableName, sTableName),
            DABase.Instance.Parameter(cn_pmKeyValue, sKeyValue));

            RecordLock objlock = new RecordLock();
            foreach (DataRow dr in dt.Rows)
            {
                objlock = new RecordLock(dr);
            }
            return objlock;
        }

        #endregion Methods
    }
}