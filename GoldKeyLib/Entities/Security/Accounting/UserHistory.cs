using System.Data;
using GoldKeyLib.DA;
using System;

namespace GoldKeyLib.Entities
{

    [Serializable]
    public sealed class UserHistory : BaseEntityList<UserHistoryEntry>
    {
        
        #region Data

        //'Database Field Names
        private const string cn_fItemId = "fldi_UserId";
        private const string cn_fUserAction = "fldi_UserAction";
        private const string cn_fEntryDate = "fldd_EntryDate";
        
        

        //'StoredProcedure Names
        private const string cn_spAdd = "pr_AddUserHistory";

        //private const string cn_spModify = "pr_ModifyState";
        //private const string cn_spDelete = "pr_DeleteState";
        private const string cn_spList = "pr_GetUserHistory";
        //private const string cn_spGet = "pr_GetState";

        //'Stored Proc Parameters'
        private const string cn_pmLoggedUserId = "@pLoggedUserId";
        private const string cn_pmUserId = "@pUserId";
        private const string cn_pmEntryDate = "@pEntryDate";
        private const string cn_pmEntryUserId = "@pUserAction";


        #endregion Data

        #region Private Attributes

        private static UserHistory mInstance = null;
        private static readonly object padlock = new object();
        private static int mLoggedUserId;

        #endregion Private Attributes

        #region Properties

        #endregion Properties

        #region Constructors

        public UserHistory(int iLoggedUserId)
        {
            mLoggedUserId = iLoggedUserId;
        }

        public static UserHistory Listing
        {
            get
            {
                lock (padlock)
                {
                    if (mInstance == null)
                    {
                        mInstance = new UserHistory(mLoggedUserId);
                    }
                    return mInstance;
                }
            }
        }

        private static UserHistory Load(int iLoggedUserId, int iUserId)
        {
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spList,
                DABase.Instance.Parameter(cn_pmLoggedUserId, iLoggedUserId),
                DABase.Instance.Parameter(cn_pmUserId, iUserId));
            mInstance.ClearItems();
            foreach (DataRow dr in dt.Rows)
            {
                UserHistoryEntry history;
                history = new UserHistoryEntry(dr);
                mInstance.Add(history);
            };

            return mInstance;
            {
            }
        }

        #endregion Constructors

        #region Methods

        

        public UserHistory List(int iLoggedUserId, int iItemId)
        {
            if (mInstance == null)
            {
                mInstance = new UserHistory(iLoggedUserId);
            }
            return Load(iLoggedUserId, iItemId);
        }

        #endregion Methods
    }
}