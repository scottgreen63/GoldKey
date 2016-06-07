using System;
using System.Data;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public class AuditLogEntry : BaseEntity
    {
        #region Data

        //Database Field Names
        private const string cn_fUserId = "fldi_User_Id";

        private const string cn_fEventDate = "fldd_Date";
        private const string cn_fEventTime = "fldd_Time";
        private const string cn_fSeverity = "fldc_Severity";
        private const string cn_fMessageType = "fldc_MessageType";
        private const string cn_fUser = "fldc_User";
        private const string cn_fModule = "fldc_Module";
        private const string cn_fDescription = "fldv_Description";
        private const string cn_fWorkstation = "fldc_WorkStation";
        private const string cn_fFunction = "fldc_Function";
        //StoredProcedure Names

        private const string cn_spList = "pr_ListAuditLog";

        //Parameters'
        private const string cn_pmLoggedUserId = "@pLoggedUserId";
        private const string cn_pmStartDate = "@pStartDate";

        private const string cn_pmEndDate = "@pEndDate";
        private const string cn_pmShowDebug = "@pShowDebug";

        private const string cn_pmDescription = "@pDescription";

        #endregion Data

        #region Private Attributes

        private User _user = null;
        private string _eventdate;
        private string _eventtime;
        private string _severity;
        private string _messagetype;
        private string _module;
        private string _description;
        private string _workstation;
        private string _function;

        
        private const string cn_pnUserId = "UserId";
        private const string cn_pnDate = "EventDate";
        private const string cn_pnTime = "EventTime";
        private const string cn_pnSeverity = "Severity";
        private const string cn_pnMessageType = "MessageType";
        private const string cn_pnUser = "User";
        private const string cn_pnModule = "Module";
        private const string cn_pnDescription = "Description";
        private const string cn_pnWorkstation = "Workstation";

        private const string cn_pnFunction = "Function";

        #endregion Private Attributes

        #region Properties

        public User User
        {
            get { return _user; }
        }

        public string EventDate
        {
            get { return _eventdate; }
        }

        public string EventTime
        {
            get { return _eventtime; }
        }

        public string Severity
        {
            get { return _severity; }
        }

        public string MessageType
        {
            get { return _messagetype; }
        }

        public string UserName
        {
            get { return this._user.UserName; }
        }

        public string Module
        {
            get { return _module; }
        }

        public string Description
        {
            get { return _description; }
        }

        public string Workstation
        {
            get { return _workstation; }
        }

        public string Function
        {
            get { return _function; }
        }

        #endregion Properties

        #region Constructors

        public AuditLogEntry()
        {
        }

        public AuditLogEntry(DataRow dr)
        {
            this.Populate(dr);
        }

        private void Populate(DataRow dr)
        {
            User user = Users.Find(Convert.ToInt32(dr[cn_fUserId]));
            this._user = user;
            this._eventdate = Convert.ToString(dr[cn_fEventDate]);
            this._eventtime = String.Format("{0:HH:mm}", Convert.ToString(dr[cn_fEventTime]));
            this._severity = Convert.ToString(dr[cn_fSeverity]);
            this._messagetype = Convert.ToString(dr[cn_fMessageType]);
            this._module = Convert.ToString(dr[cn_fModule]);
            this._description = Convert.ToString(dr[cn_fDescription]);
            this._workstation = Convert.ToString(dr[cn_fWorkstation]);
            this._function = Convert.ToString(dr[cn_fFunction]);

            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors
    }
}