using System;
using System.Data;
using System.Linq;
using GoldKeyLib.DA;
using System.Windows.Forms;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public class UserGroup : BaseEntity
    {
        #region Data

        //'Database Field Names
        private const string cn_fUserGroupId = "fldi_UserGroup_Id";

        private const string cn_fUserGroupCode = "fldc_UserGroupCode";
        private const string cn_fUserGroupName = "fldc_UserGroupName";

        //'StoredProcedure Names
        private const string cn_spAdd = "pr_AddUserGroup";

        private const string cn_spModify = "pr_ModifyUserGroup";
        private const string cn_spDelete = "pr_DeleteUserGroup";
        private const string cn_spList = "pr_ListUserGroup";
        private const string cn_spGet = "pr_GetUserGroup";

        //'Stored Proc Parameters'
        private const string cn_pmLoggedUserId = "@pLoggedUserId";
        private const string cn_pmUserGroupId = "@pUserGroupId";

        private const string cn_pmUserGroupCode = "@pUserGroupCode";
        private const string cn_pmUserGroupName = "@pUserGroupName";
        private const string cn_pmNewId = "@pNewId";

        #endregion Data

        #region Private Attributes
        private int miLoggedUserId =0;
        private int miUserGroupId = 0;
        private string msUserGroupCode;
        private string msUserGroupName;
        private UserGroupPermissions moUserGroupPermissions;
        private UserGroupMenuItems moUserMenu;

        //'Property Names'
        private const string cn_pnUserGroupId = "UserGroupId";

        private const string cn_pnUserGroupCode = "UserGroupCode";
        private const string cn_pnUserGroupName = "UserGroupName";
        private const string cn_pnUserGroupPermissions = "UserPermissions";
        private const string cn_pnUserPermissions = "UserPermissions";
        private const string cn_pnUserMenu = "UserMenu";

        #endregion Private Attributes

        #region Properties

        public int UserGroupId
        {
            get
            { return miUserGroupId; }
            set
            {
                if (miUserGroupId != value)
                {
                    miUserGroupId = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnUserGroupId);
                }
            }
        }

        public string UserGroupCode
        {
            get
            { return msUserGroupCode; }
            set
            {
                if (msUserGroupCode != value)
                {
                    // Validate the first name
                    Validator.ValidateClear(cn_pnUserGroupCode);
                    Validator.ValidateRequired(cn_pnUserGroupCode, value);
                    Validator.ValidateZeroLength(cn_pnUserGroupCode, value);
                    Validator.ValidateMinLength(cn_pnUserGroupCode, value, 2);
                    Validator.ValidateMaxLength(cn_pnUserGroupCode, value, 20);
                    msUserGroupCode = value.ToUpper();
                    this.SetEntityState(EntityStateType.Modified, cn_pnUserGroupCode);
                }
            }
        }

        public string UserGroupName
        {
            get
            { return msUserGroupName; }
            set
            {
                if (msUserGroupName != value)
                {
                    // Validate the first name
                    Validator.ValidateClear(cn_pnUserGroupName);
                    Validator.ValidateRequired(cn_pnUserGroupName, value);
                    Validator.ValidateZeroLength(cn_pnUserGroupName, value);
                    Validator.ValidateMinLength(cn_pnUserGroupName, value, 2);
                    Validator.ValidateMaxLength(cn_pnUserGroupName, value, 20);
                    msUserGroupName = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnUserGroupName);
                }
            }
        }

        public UserGroupPermissions UserGroupPermissions
        {
            get { return moUserGroupPermissions; }

            set
            {
                try
                {
                    if ((!object.ReferenceEquals(value, moUserGroupPermissions)))
                    {
                        moUserGroupPermissions = value;
                        Validator.ValidateClear(cn_pnUserGroupPermissions);
                        Validator.ValidateNull(cn_pnUserGroupPermissions, moUserGroupPermissions);
                        this.SetEntityState(EntityStateType.Modified, cn_pnUserGroupPermissions);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public UserGroupMenuItems UserGroupMenu
        {
            get
            {
                moUserMenu = UserGroupMenuItems.List(miLoggedUserId,miUserGroupId);
                return moUserMenu;
            }
            set
            {
                try
                {
                    if ((!object.ReferenceEquals(value, moUserMenu)))
                    {
                        moUserMenu = value;
                        Validator.ValidateClear(cn_pnUserMenu);
                        Validator.ValidateNull(cn_pnUserMenu, moUserMenu);
                        this.SetEntityState(EntityStateType.Modified, cn_pnUserMenu);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        #endregion Properties

        #region Constructors

        public UserGroup()
        {
            this.UserGroupPermissions = new UserGroupPermissions(this.miLoggedUserId, this.miUserGroupId);
            // this.UserGroupPermissions = UserGroupPermissions.GetUserGroupPermissions(iNewUser, false);
        }

        public UserGroup(int iLoggedUserId,DataRow dr)
        {
            miLoggedUserId = iLoggedUserId;
            this.Populate(dr);
        }

        private void Populate(DataRow dr)
        {
            this.UserGroupId = Convert.ToInt32(dr[cn_fUserGroupId].ToString());
            this.UserGroupCode = dr[cn_fUserGroupCode].ToString();
            this.UserGroupName = dr[cn_fUserGroupName].ToString();
            this.UserGroupPermissions = UserGroupPermissions.GetUserGroupPermissions(miLoggedUserId, this.UserGroupId, true);
            this.UserGroupMenu = UserGroupMenuItems.List(miLoggedUserId, this.UserGroupId);
            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Public Methods

        public void AddUserGroup()
        {
            if ((miUserGroupId == 0) && (Validator.Count == 0))
            {
                DataTable dt = new DataTable();

                dt = DABase.Instance.ExecSP(cn_spAdd,
                    DABase.Instance.Parameter(cn_pmLoggedUserId, miLoggedUserId),
                    DABase.Instance.Parameter(cn_pmUserGroupId, miUserGroupId),
                    DABase.Instance.Parameter(cn_pmUserGroupCode, msUserGroupCode),
                    DABase.Instance.Parameter(cn_pmUserGroupName, msUserGroupName),
                    DABase.Instance.Parameter(cn_pmNewId, miUserGroupId));

                int oNewUserGroupId;
                UserGroupPermissions oNewPermissions = null; ;
                foreach (DataRow dRow in dt.Rows)
                {
                    oNewUserGroupId = Convert.ToInt32(dRow[0]);
                    oNewPermissions = UserGroupPermissions.GetUserGroupPermissions(miLoggedUserId, oNewUserGroupId, true);
                }

                foreach (UserGroupPermission p in this.UserGroupPermissions)
                {
                    UserGroupPermission n = oNewPermissions.Where(x => x.FormName == p.FormName).First();

                    n.CanCreate = p.CanCreate;
                    n.CanRead = p.CanRead;
                    n.CanUpdate = p.CanUpdate;
                    n.CanDelete = p.CanDelete;
                    n.SetUserGroupPermission(miLoggedUserId);
                }

                this.SetEntityState(EntityStateType.Added);
                UserGroups.Listing.List(miLoggedUserId);
            }
            else
            {
                MessageBox.Show(Validator.ToString());
            }
        }

        public void ModifyUserGroup()
        {
            if ((miUserGroupId != 0) && (Validator.Count == 0))
            {
                DABase.Instance.ExecSP(cn_spModify,
                DABase.Instance.Parameter(cn_pmLoggedUserId, miLoggedUserId),
                DABase.Instance.Parameter(cn_pmUserGroupId, miUserGroupId),
                DABase.Instance.Parameter(cn_pmUserGroupCode, msUserGroupCode),
                DABase.Instance.Parameter(cn_pmUserGroupName, msUserGroupName),
                DABase.Instance.Parameter(cn_pmNewId, miUserGroupId));

                foreach (UserGroupPermission perm in this.UserGroupPermissions)
                {
                    perm.SetUserGroupPermission(miLoggedUserId);
                }

                this.SetEntityState(EntityStateType.Modified);
                UserGroups.Listing.List(miLoggedUserId);
            }
            else
                MessageBox.Show(Validator.ToString());
        }

        public void DeleteUserGroup()
        {
            if (miUserGroupId != 0)
            {
                DABase.Instance.ExecSP(cn_spDelete,
                DABase.Instance.Parameter(cn_pmLoggedUserId, miLoggedUserId),
                DABase.Instance.Parameter(cn_pmUserGroupId, miUserGroupId));

                this.SetEntityState(EntityStateType.Deleted);
                UserGroups.Listing.List(miLoggedUserId);
            }
        }

        public UserGroup GetUserGroup(int iUserGroupId)
        {
            DataTable dt;

            dt = DABase.Instance.ExecSP(cn_spGet,
                DABase.Instance.Parameter(cn_pmLoggedUserId, miLoggedUserId),
                DABase.Instance.Parameter(cn_pmUserGroupId, iUserGroupId));
            UserGroup ugrp = new UserGroup();
            foreach (DataRow dr in dt.Rows)
            {
                ugrp = new UserGroup(miLoggedUserId, dr);
            }
            return ugrp;
        }

        public override string ToString()
        {
            return this.UserGroupName;
        }

        #endregion Public Methods
    }

    [Serializable]
    public class UserGroupPermissions : BaseEntityList<UserGroupPermission>
    {
        #region Data

        private const string cn_fUserGroupId = "fldi_UserGroup_Id";
        private const string cn_fFormName = "fldc_FormName";
        private const string cn_fCreate = "fldb_Create";
        private const string cn_fRead = "fldb_Read";
        private const string cn_fUpdate = "fldb_Update";
        private const string cn_fDelete = "fldb_Delete";

        //StoredProcedure Names
        private const string cn_spList = "pr_GetUserGroupPermissions";

        private const string cn_spSet = "pr_SetUserGroupPermissions";
        private const string cn_pmLoggedUserId = "@pUserId";
        private const string cn_pmUserGroupId = "@pUserGroupId";
        private const string cn_pmFormName = "@pFormName";
        private const string cn_pmCreate = "@pCreate";
        private const string cn_pmRead = "@pRead";
        private const string cn_pmUpdate = "@pUpdate";
        private const string cn_pmDelete = "@pDelete";

        #endregion Data

        #region Private Attributes

        private static UserGroupPermissions m_PermissionInstance;
        private int miUserGroupId = 0;
        private static int miLoggedUserId = 0;

        #endregion Private Attributes

        #region Properties

        public int UserGroupId
        {
            get { return miUserGroupId; }
            set { miUserGroupId = value; }
        }

        #endregion Properties

        #region Constructors

        public UserGroupPermissions(int iLoggedUserId, int iUserGroupId)
        {
            miLoggedUserId = iLoggedUserId;
        }

        private static void Load(int iLoggedUserId,int iUserGroupId)
        {
            DataTable dt = null;
            dt = DABase.Instance.ExecSP(cn_spList,
                DABase.Instance.Parameter(cn_pmLoggedUserId, miLoggedUserId),
                DABase.Instance.Parameter(cn_pmUserGroupId, iUserGroupId));
            foreach (DataRow dRow in dt.Rows)
            {
                UserGroupPermission oUserGroupPermission = new UserGroupPermission();

                oUserGroupPermission.UserGroupId = Convert.ToInt32(dRow[cn_fUserGroupId]);
                oUserGroupPermission.FormName = Convert.ToString(dRow[cn_fFormName]);
                oUserGroupPermission.CanCreate = Convert.ToBoolean(dRow[cn_fCreate]);
                oUserGroupPermission.CanRead = Convert.ToBoolean(dRow[cn_fRead]);
                oUserGroupPermission.CanUpdate = Convert.ToBoolean(dRow[cn_fUpdate]);
                oUserGroupPermission.CanDelete = Convert.ToBoolean(dRow[cn_fDelete]);

                m_PermissionInstance.Add(oUserGroupPermission);
            }
            dt = null;
        }

        #endregion Constructors

        #region Methods

        public static UserGroupPermissions GetUserGroupPermissions(int iLoggedUserId,int iUserGroupId, bool bRefresh)
        {
            m_PermissionInstance = new UserGroupPermissions(iLoggedUserId,iUserGroupId);
            Load(iLoggedUserId,iUserGroupId);
            return m_PermissionInstance;
        }

        #endregion Methods
    }

    [Serializable]
    public class UserGroupPermission : BaseEntity
    {
        #region Data

        private const string cn_fUserGroupId = "fldi_UserGroup_Id";
        private const string cn_fFormName = "fldc_FormName";
        private const string cn_fCreate = "fldb_Create";
        private const string cn_fRead = "fldb_Read";
        private const string cn_fUpdate = "fldb_Update";
        private const string cn_fDelete = "fldb_Delete";

        //StoredProcedure Names
        private const string cn_spList = "pr_ListUserGroupPermissions";

        private const string cn_spSet = "pr_SetUserGroupPermissions";
        private const string cn_pmLoggedUserId = "@pLoggedUserId";
        private const string cn_pmUserGroupId = "@pUserGroupId";
        private const string cn_pmFormName = "@pFormName";
        private const string cn_pmCreate = "@pCreate";
        private const string cn_pmRead = "@pRead";
        private const string cn_pmUpdate = "@pUpdate";
        private const string cn_pmDelete = "@pDelete";

        #endregion Data

        #region Private Attributes

        private int miUserGroupId = 0;
        private string msFormName = "";
        private bool mbCreate = false;
        private bool mbRead = false;
        private bool mbUpdate = false;
        private bool mbDelete = false;

        #endregion Private Attributes

        #region Properties

        public int UserGroupId
        {
            get { return miUserGroupId; }

            set
            {
                miUserGroupId = value;
                this.SetEntityState(EntityStateType.Modified);
            }
        }

        public string FormName
        {
            get { return msFormName; }
            set
            {
                try
                {
                    msFormName = value.Trim();
                    this.SetEntityState(EntityStateType.Modified);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public bool CanCreate
        {
            get { return mbCreate; }
            set
            {
                try
                {
                    mbCreate = value;
                    this.SetEntityState(EntityStateType.Modified);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public bool CanRead
        {
            get { return mbRead; }
            set
            {
                try
                {
                    mbRead = value;
                    this.SetEntityState(EntityStateType.Modified);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public bool CanUpdate
        {
            get { return mbUpdate; }
            set
            {
                try
                {
                    mbUpdate = value;
                    this.SetEntityState(EntityStateType.Modified);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public bool CanDelete
        {
            get { return mbDelete; }
            set
            {
                try
                {
                    mbDelete = value;
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

        public UserGroupPermission()
        {
        }

        #endregion Constructors

        #region Methods

        public void SetUserGroupPermission(int iLoggedUserId)
        {
            //if (miUserGroupId != 0)
            //{
                DABase.Instance.ExecSP(cn_spSet,
                    DABase.Instance.Parameter(cn_pmLoggedUserId, iLoggedUserId),
                    DABase.Instance.Parameter(cn_pmUserGroupId, miUserGroupId), DABase.Instance.Parameter(cn_pmFormName, msFormName), DABase.Instance.Parameter(cn_pmCreate, mbCreate), DABase.Instance.Parameter(cn_pmRead, mbRead), DABase.Instance.Parameter(cn_pmUpdate, mbUpdate), DABase.Instance.Parameter(cn_pmDelete, mbDelete));
           // }
        }

        #endregion Methods
    }

    [Serializable]
    public class UserGroupMenuItems : BaseEntityList<UserMenuItem>
    {
        #region Data

        //StoredProcedure Names
        private const string cn_spList = "pr_GetUserMenu";

        //Parameters'
        private const string cn_pmLoggedUserId = "@pLoggedUserId";
        private const string cn_pmUserGroupId = "@pUserGroupId";

        //Database Field Names
        private const string cn_fMenuId = "fldi_Menu_Id";

        private const string cn_fMenuUnderId = "fldi_MenuUnder_Id";
        private const string cn_fMenuOrder = "fldi_MenuOrder";
        private const string cn_fMenuCaption = "fldc_MenuCaption";
        private const string cn_fMenuShortcut = "fldc_MenuShortcut";
        private const string cn_fMenuEnabled = "fldi_MenuEnabled";
        private const string cn_fMenuChecked = "fldi_MenuChecked";
        private const string cn_fUserGroupId = "fldi_UserGroup_Id";

        #endregion Data

        #region Private Attributes

        private static UserGroupMenuItems mGroupMenuInstance;
        private int miUserGroupId;
        private static int miLoggedUserId = 0;

        #endregion Private Attributes

        #region Properties

        public int UserGroupId
        {
            get { return miUserGroupId; }
            set { miUserGroupId = value; }
        }

        #endregion Properties

        #region Constructors

        protected UserGroupMenuItems(int iLoggedUserId,int iUserGroupId)
        {
            
            miLoggedUserId = iLoggedUserId;
        }

        private static void Load(int iUserGroupId)
        {
            Validation val = new Validation();
            DataTable dt = null;
            dt = DABase.Instance.ExecSP(cn_spList,
                DABase.Instance.Parameter(cn_pmLoggedUserId, miLoggedUserId),
                DABase.Instance.Parameter(cn_pmUserGroupId, iUserGroupId));
            foreach (DataRow dRow in dt.Rows)
            {
                UserMenuItem oUserMenuItem = new UserMenuItem();
                oUserMenuItem.UserMenuId = Convert.ToInt32(dRow[cn_fMenuId]);
                oUserMenuItem.UserMenuUnderId = Convert.ToInt32(dRow[cn_fMenuUnderId]);
                oUserMenuItem.UserMenuOrder = Convert.ToInt32(dRow[cn_fMenuOrder]);
                oUserMenuItem.UserMenuCaption = Convert.ToString(dRow[cn_fMenuCaption]);
                oUserMenuItem.UserMenuShortCut = Convert.ToString(dRow[cn_fMenuShortcut]);
                oUserMenuItem.UserMenuEnabled = Convert.ToInt32(dRow[cn_fMenuEnabled]);
                oUserMenuItem.UserMenuChecked = Convert.ToBoolean(dRow[cn_fMenuChecked]);

                mGroupMenuInstance.Add(oUserMenuItem);
            }
            dt = null;
        }

        #endregion Constructors

        #region Methods

        public static UserGroupMenuItems List(int iLoggedUserId,int iUserGroupId)
        {
            mGroupMenuInstance = new UserGroupMenuItems(iLoggedUserId,iUserGroupId);
            Load(iUserGroupId);
            return mGroupMenuInstance;
        }

        //public void Refresh(int iUserRoleId)
        //{
        //    this.Clear();
        //    Load(iUserRoleId);
        //}

        #endregion Methods
    }

    [Serializable]
    public class UserMenuItem : BaseEntity
    {
        #region Private Attributes

        private int miUserMenuId;
        private int miUserMenuUnderId;
        private int miUserMenuOrder;
        private string msUserMenuCaption;
        private string msUserMenuShortCut;
        private int miUserMenuEnabled;
        private bool miUserMenuChecked;

        //'Property Names'
        private const string cn_pnUserMenuId = "UserMenuId";

        private const string cn_pnUserMenuUnderId = "UserMenuUnderId";
        private const string cn_pnUserMenuOrder = "UserMenuOrder";
        private const string cn_pnUserMenuCaption = "UserMenuCaption";
        private const string cn_pnUserMenuShortCut = "UserMenuShortCut";
        private const string cn_pnUserMenuEnabled = "UserMenuEnabled";
        private const string cn_pnUserMenuChecked = "UserMenuChecked";

        #endregion Private Attributes

        #region Properties

        public int UserMenuId
        {
            get { return miUserMenuId; }
            set
            {
                try
                {
                    if (miUserMenuId != value)
                    {
                        miUserMenuId = value;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public int UserMenuUnderId
        {
            get { return miUserMenuUnderId; }
            set
            {
                try
                {
                    if (miUserMenuUnderId != value)
                    {
                        miUserMenuUnderId = value;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public int UserMenuOrder
        {
            get { return miUserMenuOrder; }
            set
            {
                try
                {
                    if (miUserMenuOrder != value)
                    {
                        miUserMenuOrder = value;
                        this.SetEntityState(EntityStateType.Modified, cn_pnUserMenuOrder);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public string UserMenuCaption
        {
            get { return msUserMenuCaption; }
            set
            {
                try
                {
                    if (msUserMenuCaption != value.Trim())
                    { msUserMenuCaption = value.Trim(); }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public string UserMenuShortCut
        {
            get { return msUserMenuShortCut; }
            set
            {
                try
                {
                    if (msUserMenuShortCut != value.Trim())
                    {
                        msUserMenuShortCut = value.Trim();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public int UserMenuEnabled
        {
            get { return miUserMenuEnabled; }
            set
            {
                try
                {
                    if (miUserMenuEnabled != value)
                    {
                        miUserMenuEnabled = value;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public bool UserMenuChecked
        {
            get { return miUserMenuChecked; }
            set
            {
                try
                {
                    if (miUserMenuChecked != value)
                    {
                        miUserMenuChecked = value;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        #endregion Properties
    }
}