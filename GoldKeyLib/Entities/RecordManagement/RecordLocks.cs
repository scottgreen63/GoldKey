using System;
using System.Data;
using GoldKeyLib.DA;
using System.Collections.Generic;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public sealed class RecordLocks : BaseEntityList<RecordLock>
    {
        #region Data

        private const string cn_fUseId = "fldi_User_Id";
        private const string cn_fTableName = "fldc_TableName";
        private const string cn_fKeyValue = "fldc_KeyValue";
        private const string cn_fLockTime = "fldd_LockTime";
        private const string cn_fLockedBy = "fldc_LockedBy";
        private const string cn_fLockedFrom = "fldc_LockedFrom";

        //'Lock StoredProcedure Names
        private const string cn_spList = "pr_ListLocks";

        private const string cn_spAdd = "pr_AddLock";
        private const string cn_spDelete = "pr_DeleteLock";
        private const string cn_spGet = "pr_GetLock";
        private const string cn_spReleaseAll = "pr_ReleaseAllLocks";
        private const string cn_spVerify = "pr_VerifyLock";

        //'Lock Stored Proc Parameters'
        private const string cn_pmUserId = "@UserId";

        private const string cn_pmTableName = "@TableName";
        private const string cn_pmKeyValue = "@KeyValue";

        #endregion Data

        #region Private Attributes

        private static RecordLocks mInstance = null;
        private static readonly object padlock = new object();

        #endregion Private Attributes

        #region Properties

        #endregion Properties

        #region Constructors

        private static RecordLocks Load()
        {
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spList);
            foreach (DataRow dr in dt.Rows)
            {
                RecordLock bld;
                bld = new RecordLock(dr);
                Instance.Add(bld);
            }

            return mInstance;
        }

        public static RecordLocks Instance
        {
            get
            {
                lock (padlock)
                {
                    if (mInstance == null)
                    {
                        mInstance = new RecordLocks();
                    }
                    return mInstance;
                }
            }
        }

        #endregion Constructors

        #region Methods

        public static RecordLocks List()
        {
            Instance.ClearItems();
            return Load();
        }

        public static void ReleaseAllLocks(int iUserID)
        {
            DABase.Instance.ExecSP(cn_spReleaseAll, DABase.Instance.Parameter(cn_pmUserId, iUserID));
        }

        public static int VerifyLock(int iUserId, string sTableName, string sKeyValue)
        {
            int iVerification = 1;
            try
            {
                DataTable dt = new DataTable();
                dt = DABase.Instance.ExecSP(cn_spVerify,
                        DABase.Instance.Parameter(cn_pmUserId, iUserId),
                        DABase.Instance.Parameter(cn_pmTableName, sTableName),
                        DABase.Instance.Parameter(cn_pmKeyValue, sKeyValue));

                foreach (DataRow dRow in dt.Rows)
                {
                    iVerification = Convert.ToInt32(dRow[0]);
                }
                return iVerification;
            }

            catch (Exception ex)
            { throw new Exception(ex.Message, ex.InnerException); }
        }

        #endregion Methods
    }
}