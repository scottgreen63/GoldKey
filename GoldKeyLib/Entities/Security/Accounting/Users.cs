using System;
using System.Data;
using GoldKeyLib.DA;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public sealed class Users : BaseEntityList<User>
    {
        #region Data

        //Database Field Names
        private const string cn_fUserId = "fldi_User_Id";

        private const string cn_fUserName = "fldc_UserName";
        private const string cn_fUserFirstName = "fldc_UserFirstName";
        private const string cn_fUserLastName = "fldc_UserLastName";
        private const string cn_fUserPassword = "fldc_UserPassword";
        private const string cn_fUserGroupId = "fldi_UserGroup_Id";

        //Parameters'
        private const string cn_pmCurrentUserId = "@pUserId";

        //StoredProcedure Names
        private const string cn_spList = "pr_ListUsers";

        #endregion Data

        #region Private Attributes

        private static Users _instance = null;
        private static readonly object _padlock = new object();
        private static int _currentuserid;
        
        

        #endregion Private Attributes

        #region Properties

        #endregion Properties

        #region Constructors
        private Users() { }
        public Users(int currentuserid)
        {
            _currentuserid = currentuserid;
            
        }

        
        public static Users Listing
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Users();
                        Load(_currentuserid);
                    }
                    return _instance;
                }
            }
        }

        private static Users Load(int iLoggedInUser)
        {
            DataTable dt = null;
            dt = DABase.Instance.ExecSP(cn_spList,DABase.Instance.Parameter(cn_pmCurrentUserId, iLoggedInUser));

            foreach (DataRow dRow in dt.Rows)
            {
                User oUser = new User();
                oUser.UserId = Convert.ToInt32(dRow[cn_fUserId]);
                oUser.UserName = Convert.ToString(dRow[cn_fUserName]);
                oUser.UserFirstName = Convert.ToString(dRow[cn_fUserFirstName]);
                oUser.UserLastName = Convert.ToString(dRow[cn_fUserLastName]);
                oUser.UserPassword = Convert.ToString(dRow[cn_fUserPassword]);
                oUser.UserGroup = UserGroups.Listing.Find(Convert.ToInt32(dRow[cn_fUserGroupId]));
                _instance.Add(oUser);
            }
            return _instance;
        }

        #endregion Constructors

        #region Methods

        public static Users List(int iLoggedUsedId)
        {
            if (_instance == null)
            { _instance = new Users(); }
            else
            { _instance.Clear(); }

            return Load(iLoggedUsedId);
        }

        public static User Find(int iUserId)
        {
            User usr = new User();
            int i = 0;
            if (_instance == null)
            {
                List(_currentuserid);
            }

            while (i < _instance.Count)
            {
                if (_instance.Items[i].UserId == System.Convert.ToInt32(iUserId))
                {
                    usr = _instance.Items[i];
                }
                i += 1;
            }
            return usr;
        }
        public static User Find(string username)
        {
            User usr = new User();
            int i = 0;
            if (_instance == null)
            {
                List(_currentuserid);
            }

            while (i < _instance.Count)
            {
                if (_instance.Items[i].UserName == username)
                {
                    usr = _instance.Items[i];
                }
                i += 1;
            }
            return usr;
        }

        #endregion Methods
    }
}