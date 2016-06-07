using System;
using System.Data;
using GoldKeyLib.DA;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public class UserHistoryEntry : BaseEntity
    {
        #region Data

        //'Database Field Names
        private const string cn_fUserId = "fldi_User_Id";
        private const string cn_fUserAction = "fldc_UserAction";
        private const string cn_fEntryDate = "fldd_EntryDate";
        


        //'StoredProcedure Names
        private const string cn_spAdd = "pr_AddUserHistoryEntry";

        //private const string cn_spModify = "pr_ModifyState";
        //private const string cn_spDelete = "pr_DeleteState";
        //private const string cn_spList = "pr_ListStates";
        //private const string cn_spGet = "pr_GetState";

        //'Stored Proc Parameters'
        private const string cn_pmLoggedUserId = "@pLoggedUserId";
        private const string cn_pmUserId = "@pUserId";
        private const string cn_pmUserAction = "@pUserAction";


        private const string cn_pmNewId = "@pNewId";

        #endregion Data

        #region Private Attributes

        private int mUserId = 0;
        private string mUserAction;
        private DateTime mEntryDate;
        
        

        //'Property Names'
        private const string cn_pnUserId = "UserId";
        private const string cn_pnUserAction = "UserAction";
        private const string cn_pnEntryDate = "EntryDate";
        
        


        #endregion Private Attributes

        #region Properties

        public int UserId
        {
            get
            { return mUserId; }
            set
            {
                if (mUserId != value)
                {
                    mUserId = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnUserId);
                }
            }
        }



        public DateTime EntryDate
        {
            get { return Convert.ToDateTime(mEntryDate); }
            set { mEntryDate = value; }
        }

        public string UserAction
        {
            get
            { return mUserAction; }
            set
            {
                try
                {

                    Validator.ValidateNull(cn_pnUserAction, value);
                    mUserAction = value;
                    this.SetEntityState(EntityStateType.Modified);
                   
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

       

        #endregion Properties

        #region Constructors

        public UserHistoryEntry()
        {
            // this.EntityState = EntityStateType.Added;
        }

        public UserHistoryEntry(DataRow dr)
        {
            this.Populate(dr);
        }

        private void Populate(DataRow dr)
        {
            this.mUserId = Convert.ToInt32(dr[cn_fUserId].ToString());
            this.mUserAction = dr[cn_fUserAction].ToString();
            this.mEntryDate = Convert.ToDateTime(Convert.ToString(dr[cn_fEntryDate]));
            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods

        public void AddUserHistoryEntry(int iLoggedUserId)
        {
            if ((mUserId == 0) && (Validator.Count == 0))
            {
                try
                {
                    DABase.Instance.ExecSP(cn_spAdd,
                    DABase.Instance.Parameter(cn_pmLoggedUserId, iLoggedUserId),
                    DABase.Instance.Parameter(cn_pmUserId, mUserId),
                    DABase.Instance.Parameter(cn_pmUserAction, mUserAction));

                    this.SetEntityState(EntityStateType.Added);
                    //ItemHistory.Listing.List(iLoggedUserId, mItemId);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            else
            {
                throw new System.Exception(Validator.ToString());
                
            }
        }

    

        #endregion Methods
    }
}