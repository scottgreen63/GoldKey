using System.Data;
using GoldKeyLib.DA;
using System;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public sealed class UserGroups : BaseEntityList<UserGroup>
    {
        #region Data

        //Database Field Names
        private const string cn_fUserGroupId = "fldi_UserGroup_Id";

        private const string cn_fUserGroupCode = "fldc_UserGroupCode";
        private const string cn_fUserGroupName = "fldc_UserGroupName";

        //StoredProcedure Names
        private const string cn_spList = "pr_ListUserGroups";

        //Parameters'
        private const string cn_pmLoggedUserId = "@pUserId";

        #endregion Data

        #region Pivate Attributes

        private static UserGroups mInstance = null;
        private static readonly object padlock = new object();

        #endregion Pivate Attributes

        #region Properties

        #endregion Properties

        #region Constructors

        public static UserGroups Listing
        {
            get
            {
                lock (padlock)
                {
                    if (mInstance == null)
                    {
                        mInstance = new UserGroups();
                    }
                    return mInstance;
                }
            }
        }

        private static UserGroups Load(int iLoggedUserId)
        {
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spList,DABase.Instance.Parameter(cn_pmLoggedUserId,iLoggedUserId));
            foreach (DataRow dr in dt.Rows)
            {
                UserGroup ugrp;
                ugrp = new UserGroup(iLoggedUserId,dr);

                Listing.Add(ugrp);
            };

            return mInstance;
            {
            }
        }

        #endregion Constructors

        #region Methods

        public UserGroup Find(int iUserGroupId)
        {
            UserGroup ugrp = new UserGroup();
            int i = 0;
            if (mInstance == null || mInstance.Count == 0)
            {
                List(0);
            }

            while (i < mInstance.Count)
            {
                if (mInstance.Items[i].UserGroupId == System.Convert.ToInt32(iUserGroupId))
                {
                    ugrp = mInstance.Items[i];
                }
                i += 1;
            }
            return ugrp;
        }

        public UserGroups List(int iLoggedUserId)
        {
            mInstance.Clear();
            if (mInstance == null || mInstance.Count == 0)
            { mInstance = new UserGroups(); }
            else
            { mInstance.Clear(); }
            return Load(iLoggedUserId);
        }

        #endregion Methods
    }
}