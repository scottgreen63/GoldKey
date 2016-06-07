using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using GoldKeyLib.Interfaces;
using GoldKeyLib.Entities;
using GoldKeyLib.DA;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public class Service : BaseEntity, IGKService
    {

        #region Data

        //tbld_Services
        //[fldi_Service_ID]           INT NOT NULL,
        //[fldc_ServiceName]          NCHAR(50)   NULL,
        //[fldc_ServiceProvider]      NCHAR(50)   NULL,
        //[fldd_ServiceFee]           DECIMAL(8,2)NULL,
        
        //'Database Field Names
        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServiceID = "fldi_Service_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServiceName = "fldc_ServiceName";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServiceProvider = "fldc_ServiceProvider";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServiceFee = "fldd_ServiceFee";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fEnabled = "fldb_Enabled";

        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spAdd = "pr_AddService";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spModify = "pr_ModifyService";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spDelete = "pr_DeleteService";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spGet = "pr_GetService";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spEnable = "pr_EnableService";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmUserID = "@pUserId";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmNewServiceID = "@pNewServiceID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmServiceID = "@pServiceID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmServiceName = "@pServiceName";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmServiceProvider = "@pServiceProvider";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmServiceFee = "@pServiceFee";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmEnabled = "@pEnabled";



        #endregion Data

        #region Private Attributes
        // private backing fields
        /// <summary>
        private int _userid = 0;
        /// private backing field
        /// </summary>
        private string _serviceid;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _servicename;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _serviceprovider;
        /// <summary>
        /// private backing field
        /// </summary>
        private decimal _servicefee;
        /// <summary>
        /// private backing field
        /// </summary>
        private bool _enabled;


        //'Property Names'
        /// <summary>
        /// Const used to identify Entity Property Name
        /// </summary>
        private const string cn_pnServiceID = "ServiceID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnServiceName = "ServiceName";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnServiceProvider = "ServiceProvider";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnServiceFee = "ServiceFee";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnEnabled = "Enabled";



        #endregion Private Attributes

        #region Properties

        //ServiceID
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnServiceID)]
        [Required()]
        public string ServiceID
        {
            get
            { return _serviceid; }
            set
            {
                if (_serviceid != value)
                {
                    this._serviceid = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnServiceID);
                }
            }
        }

        //ServiceName
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnServiceName)]
        [Required()]
        public string ServiceName
        {
            get
            { return _servicename; }
            set
            {
                if (_servicename != value)
                {
                    this._servicename = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnServiceName);
                }
            }
        }

        //ServiceProvider
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnServiceProvider)]
        [Required()]
        public string ServiceProvider
        {
            get
            { return _serviceprovider; }
            set
            {
                if (_serviceprovider != value)
                {
                    this._serviceprovider = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnServiceProvider);
                }
            }
        }

        //ServiceFee
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnServiceFee)]
        [Required()]
        public decimal ServiceFee
        {
            get
            { return _servicefee; }
            set
            {
                if (_servicefee != value)
                {
                    this._servicefee = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnServiceFee);
                }
            }
        }

        //Enabled
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnEnabled)]
        [Required()]
        public bool Enabled
        {
            get
            { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    this._enabled = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnEnabled);
                }
            }
        }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Constructor ()
        /// </summary>
        public Service()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }
        /// <summary>
        /// Constructor ()
        /// </summary>
        public Service(int UserId)
        {
            //this.Validator = new Validation();
            _userid = UserId;
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public Service(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._serviceid = dr[cn_fServiceID].ToString();
            this._servicename = dr[cn_fServiceName].ToString();
            this._serviceprovider = dr[cn_fServiceProvider].ToString();
            this._servicefee = Convert.ToDecimal(dr[cn_fServiceFee].ToString());
            this._enabled = Convert.ToBoolean(dr[cn_fEnabled].ToString());
            
            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
        public void AddService(int UserId)
        {
            _userid = UserId;

            if ((_serviceid == "0") && (this.Validator.Count == 0))
            {
                DABase.Instance.ExecSP(cn_spAdd,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmServiceID, _serviceid),
                // DABase.Instance.Parameter(cn_pmCustomerCode, _customercode),
                DABase.Instance.Parameter(cn_pmServiceName, _servicename),
                DABase.Instance.Parameter(cn_pmServiceProvider, _serviceprovider),
                DABase.Instance.Parameter(cn_pmServiceFee, _servicefee),
                DABase.Instance.Parameter(cn_pmNewServiceID, _serviceid));

                this.SetEntityState(EntityStateType.Added);
                Customers.Listing.List(_userid);
            }
            else
            {
                throw new System.Exception(Validator.ToString());
                ///MessageBox.Show(ValidationInstance.ToString());
            }



        }

        public void ModifyService(int UserId)
        {
            _userid = UserId;

            if ((_serviceid != "0") && (Validator.Count == 0))
            {
                try
                {
                    DABase.Instance.ExecSP(cn_spModify,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmServiceID, _serviceid),
                    // DABase.Instance.Parameter(cn_pmCustomerCode, _customercode),
                    DABase.Instance.Parameter(cn_pmServiceName, _servicename),
                    DABase.Instance.Parameter(cn_pmServiceProvider, _serviceprovider),
                    DABase.Instance.Parameter(cn_pmServiceFee, _servicefee),
                    DABase.Instance.Parameter(cn_pmNewServiceID, _serviceid));

                    this.SetEntityState(EntityStateType.Modified);
                    // Items.Listing.List(iLoggedUserId);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            else
                throw new Exception(Validator.ToString());


        }

        public void DeleteService(int UserId)
        {
            if (_serviceid != "0")
            {
                DABase.Instance.ExecSP(cn_spDelete,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmServiceID, _serviceid));
                this.SetEntityState(EntityStateType.Deleted);
                Customers.Listing.List(_userid);
            }
        }

        public Customer GetService(int UserID, string ServiceID)
        {
            _userid = UserID;
            _serviceid = ServiceID;
            DataTable dt;

            dt = DABase.Instance.ExecSP(cn_spGet,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmServiceID, _serviceid));

            Customer customer = new Customer(_userid);

            foreach (DataRow dr in dt.Rows)
            {
                customer = new Customer(dr);
            }
            return customer;
        }

        public void EnableService(int UserId)
        {
            if (_serviceid != "0")
            {
                DABase.Instance.ExecSP(cn_spEnable,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmServiceID, _serviceid),
                    DABase.Instance.Parameter(cn_pmEnabled, _enabled));
                this.SetEntityState(EntityStateType.Deleted);
                Customers.Listing.List(_userid);
            }
        }


        #endregion Methods
    }
}
