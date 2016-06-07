using System;
using System.ComponentModel;
using System.Data;
using GoldKeyLib.DA;
using System.Security.Principal;
using System.Collections;
using System.Security;
using System.Security.Cryptography;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public class User :  BaseEntity
    {
        #region Data

        //Database Field Names
        private const string cn_fUserId = "fldi_User_Id";

        private const string cn_fUserName = "fldc_UserName";
        private const string cn_fUserFirstName = "fldc_UserFirstName";
        private const string cn_fUserLastName = "fldc_UserLastName";
        private const string cn_fUserPassword = "fldc_UserPassword";
        private const string cn_fUserGroupId = "fldi_UserGroup_Id";

        //StoredProcedure Names
        private const string cn_spAdd = "pr_AddUser";

        private const string cn_spModify = "pr_ModifyUser";
        private const string cn_spDelete = "pr_DeleteUser";
        private const string cn_spGet = "pr_GetUser";
        private const string cn_spAction = "pr_AddUserHistoryEntry";


        //Parameters'
        private const string cn_pmLoggedUserId = "@pLoggedUserId";
        private const string cn_pmUserId = "@pUserId";
        private const string cn_pmAction = "@pUserAction";
        private const string cn_pmUserName = "@pUserName";
        private const string cn_pmUserFirstName = "@pUserFirstName";
        private const string cn_pmUserLastName = "@pUserLastName";
        private const string cn_pmUserPassword = "@pUserPassword";
        private const string cn_pmUserGroupId = "@pUserGroupId";

        private const string cn_pmNewId = "@pNewId";

        #endregion Data

        #region Private Attributes

        private int _userId = 0;
        private string _userName;
        private string _userFirstName;
        private string _userLastName;
        private string _userPassword;
        private UserGroup _userGroup = new UserGroup();

        private const string cn_pnUserName = "UserName";
        private const string cn_pnUserFirstName = "UserFirstName";
        private const string cn_pnUserLastName = "UserLastName";
        private const string cn_pnUserPassword = "UserPassword";

        private const string cn_pnUserGroup = "UserGroup";

        #endregion Private Attributes

        #region Properties

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                try
                {
                    if (_userName == null || _userName.Trim() != value.Trim())
                    {
                        string sPropName = cn_pnUserName;
                        Validator.ValidateClear(sPropName);
                        Validator.ValidateRequired(sPropName, value);
                        Validator.ValidateZeroLength(sPropName, value);
                        Validator.ValidateMinLength(sPropName, value, 4);
                        Validator.ValidateMaxLength(sPropName, value, 10);
                        this.SetEntityState(EntityStateType.Modified);
                        _userName = value.Trim();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public string UserFirstName
        {
            get { return _userFirstName; }
            set
            {
                try
                {
                    if (_userFirstName == null || _userFirstName.Trim() != value.Trim())
                    {
                        string sPropName = cn_pnUserFirstName;
                        Validator.ValidateClear(sPropName);
                        Validator.ValidateRequired(sPropName, value);
                        Validator.ValidateZeroLength(sPropName, value);
                        Validator.ValidateMinLength(sPropName, value, 2);
                        Validator.ValidateMaxLength(sPropName, value, 10);
                        this.SetEntityState(EntityStateType.Modified);
                        _userFirstName = value.Trim().ToUpper();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public string UserLastName
        {
            get { return _userLastName; }
            set
            {
                try
                {
                    if (_userLastName == null || _userLastName.Trim() != value.Trim())
                    {
                        string sPropName = cn_pnUserLastName;
                        Validator.ValidateClear(sPropName);
                        Validator.ValidateRequired(sPropName, value);
                        Validator.ValidateZeroLength(sPropName, value);
                        Validator.ValidateMinLength(sPropName, value, 2);
                        Validator.ValidateMaxLength(sPropName, value, 20);
                        this.SetEntityState(EntityStateType.Modified);
                        _userLastName = value.Trim().ToUpper();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        [Bindable(false)]
        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                try
                {
                    if (_userPassword == null || _userPassword.Trim() != value.Trim())
                    {
                        string sPropName = cn_pnUserPassword;
                        Validator.ValidateClear(sPropName);
                        Validator.ValidateRequired(sPropName, value);
                        Validator.ValidateZeroLength(sPropName, value);
                        //ValidationInstance.ValidateMinLength(sPropName, value, 8);
                        //ValidationInstance.ValidateMaxLength(sPropName, value, 8);
                        this.SetEntityState(EntityStateType.Modified);
                        _userPassword = value.Trim();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public UserGroup UserGroup
        {
            get { return _userGroup; }
            set
            {
                try
                {
                    if ((!object.ReferenceEquals(value, _userGroup)))
                    {
                        string sPropName = cn_pnUserGroup;
                        Validator.ValidateNull(sPropName, _userGroup);
                        _userGroup = value;
                        this.SetEntityState(EntityStateType.Modified);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public UserHistory UserHistory
        {
            get
            {
                UserHistory mHist = new UserHistory(this.UserId);
                mHist = UserHistory.List(0, this.UserId);
                return mHist;


            }
        }

        #endregion Properties

        #region Constructors

        public User()
        {
        }

        public User(DataRow dr)
        {
            this.Populate(dr);
        }

        private void Populate(DataRow dr)
        {
            this._userId = Convert.ToInt32(dr[cn_fUserId]);
            this._userName = Convert.ToString(dr[cn_fUserName]);
            this._userFirstName = Convert.ToString(dr[cn_fUserFirstName]);
            this._userLastName = Convert.ToString(dr[cn_fUserLastName]);
            this._userPassword = Convert.ToString(dr[cn_fUserPassword]);
            this._userGroup = UserGroups.Listing.Find(Convert.ToInt32(dr[cn_fUserGroupId]));
        }

        #endregion Constructors

        #region Methods

        public Boolean AuthenticateUser(string sUserName, string sUserPassword)
        {
            string username = sUserName;
            string password = sUserPassword;

            //Look up the stored hashed password and salt for the username.
            string HashedPW = Crypto.Encrypto(password);
            User user = Users.Find(sUserName);
            //' Compare the hashed password with the stored password.
            return (HashedPW == user.UserPassword);

        }


        public void UserAction(int iLoggedUserId, int iUserId, string sUserAction)
        {
            DABase.Instance.ExecSP(cn_spAction, 
                DABase.Instance.Parameter(cn_pmLoggedUserId, iLoggedUserId), 
                DABase.Instance.Parameter(cn_pmUserId, iUserId), 
                DABase.Instance.Parameter(cn_pmAction, sUserAction));


        }

        public void AddUser(int iLoggedUserId)
        {
            if (_userId == 0)
            {
                DABase.Instance.ExecSP(cn_spAdd,
                    DABase.Instance.Parameter(cn_pmLoggedUserId, iLoggedUserId),
                    DABase.Instance.Parameter(cn_pmUserId, _userId),
                    DABase.Instance.Parameter(cn_pmUserName, _userName),
                    DABase.Instance.Parameter(cn_pmUserFirstName, _userFirstName),
                    DABase.Instance.Parameter(cn_pmUserLastName, _userLastName),
                    DABase.Instance.Parameter(cn_pmUserPassword, Crypto.Encrypto(_userPassword)),
                    DABase.Instance.Parameter(cn_pmUserGroupId, _userGroup.UserGroupId),
                    DABase.Instance.Parameter(cn_pmNewId, _userId));
                //This was an Add
                this.SetEntityState(EntityStateType.Added);
            }
        }

        public void ModifyUser(int iLoggedUserId)
        {
           // if (_userId != 0)
           // {
                DABase.Instance.ExecSP(cn_spModify, 
                    DABase.Instance.Parameter(cn_pmLoggedUserId, iLoggedUserId), 
                    DABase.Instance.Parameter(cn_pmUserId, _userId), 
                    DABase.Instance.Parameter(cn_pmUserName, _userName), 
                    DABase.Instance.Parameter(cn_pmUserFirstName, _userFirstName), 
                    DABase.Instance.Parameter(cn_pmUserLastName, _userLastName), 
                    DABase.Instance.Parameter(cn_pmUserPassword, Crypto.Encrypto(_userPassword)), 
                    DABase.Instance.Parameter(cn_pmUserGroupId, _userGroup.UserGroupId), 
                    DABase.Instance.Parameter(cn_pmNewId, _userId));

                //This was an Modify
                //this.SetEntityState(EntityStateType.Modified);
           // }
        }

        public void DeleteUser(int iLoggedUserId)
        {
            DABase.Instance.ExecSP(cn_spDelete, 
                DABase.Instance.Parameter(cn_pmLoggedUserId, iLoggedUserId), 
                DABase.Instance.Parameter(cn_pmUserId, _userId));
        }

        public User GetUser(int iLoggedUserId, int iUserId)
        {
            DataTable dt;

            dt = DABase.Instance.ExecSP(cn_spGet, 
                DABase.Instance.Parameter(cn_pmLoggedUserId, iLoggedUserId), 
                DABase.Instance.Parameter(cn_pmUserId, iUserId));
            User bld = new User();
            foreach (DataRow dr in dt.Rows)
            {
                bld = new User(dr);
            }
            return bld;
            

        }

        public override string ToString()
        {
            // return msUserLastName.Trim().ToUpper() + "/" + msUserFirstName.Trim().ToUpper();
            return _userName;
        }

        
        #endregion Methods
    }
}