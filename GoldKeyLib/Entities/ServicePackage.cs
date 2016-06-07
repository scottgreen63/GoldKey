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
    public class ServicePackage : BaseEntity, IGKServicePackage
    {

        #region Data

        //tbld_ServicePackages
        //[fldi_ServicePackage_ID]           INT NOT NULL,
        //[fldc_ServicePackageName]          NCHAR(50)   NULL,
        //[fldc_ServicePackageContents]      NCHAR(50)   NULL,
        //[fldd_ServicePackageFee]           DECIMAL(8,2)NULL,

        //'Database Field Names
        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServicePackageID = "fldi_ServicePackage_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServicePackageName = "fldc_ServicePackageName";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServicePackageContents = "fldc_ServicePackageContents";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServicePackageFee = "fldd_ServicePackageFee";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fEnabled = "fldb_Enabled";

        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spAdd = "pr_AddServicePackage";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spModify = "pr_ModifyServicePackage";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
       // private const string cn_spDelete = "pr_DeleteServicePackage";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spGet = "pr_GetServicePackage";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spEnable = "pr_EnableServicePackage";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmUserID = "@pUserId";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmNewServicePackageID = "@pNewServicePackageID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmServicePackageID = "@pServicePackageID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmServicePackageName = "@pServicePackageName";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmServicePackageContents = "@pServicePackageContents";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmServicePackageFee = "@pServicePackageFee";

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
        private string _servicepackageid;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _servicepackagename;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _servicepackagecontents;
        /// <summary>
        /// private backing field
        /// </summary>
        private decimal _servicepackagefee;
        /// <summary>
        /// private backing field
        /// </summary>
        private bool _enabled;


        //'Property Names'
        /// <summary>
        /// Const used to identify Entity Property Name
        /// </summary>
        private const string cn_pnServicePackageID = "ServicePackageID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnServicePackageName = "ServicePackageName";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnServicePackageContents = "ServicePackageContents";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnServicePackageFee = "ServicePackageFee";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnEnabled = "Enabled";



        #endregion Private Attributes

        #region Properties

        //ServicePackageID
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnServicePackageID)]
        [Required()]
        public string ServicePackageID
        {
            get
            { return _servicepackageid; }
            set
            {
                if (_servicepackageid != value)
                {
                    this._servicepackageid = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnServicePackageID);
                }
            }
        }

        //ServicePackageName
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnServicePackageName)]
        [Required()]
        public string ServicePackageName
        {
            get
            { return _servicepackagename; }
            set
            {
                if (_servicepackagename != value)
                {
                    this._servicepackagename = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnServicePackageName);
                }
            }
        }

        //ServicePackageContents
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnServicePackageContents)]
        [Required()]
        public string ServicePackageContents
        {
            get
            { return _servicepackagecontents; }
            set
            {
                if (_servicepackagecontents != value)
                {
                    this._servicepackagecontents = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnServicePackageContents);
                }
            }
        }

        //ServicePackageFee
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnServicePackageFee)]
        [Required()]
        public decimal ServicePackageFee
        {
            get
            { return _servicepackagefee; }
            set
            {
                if (_servicepackagefee != value)
                {
                    this._servicepackagefee = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnServicePackageFee);
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
        public ServicePackage()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }
        /// <summary>
        /// Constructor ()
        /// </summary>
        public ServicePackage(int UserId)
        {
            //this.Validator = new Validation();
            _userid = UserId;
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public ServicePackage(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._servicepackageid = dr[cn_fServicePackageID].ToString();
            this._servicepackagename = dr[cn_fServicePackageName].ToString();
            this._servicepackagecontents = dr[cn_fServicePackageContents].ToString();
            this._servicepackagefee = Convert.ToDecimal(dr[cn_fServicePackageFee].ToString());

            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
        //public void AddServicePackage(int UserId)
        //{
        //    _userid = UserId;

        //    if ((_servicepackageid == "0") && (this.Validator.Count == 0))
        //    {
        //        DABase.Instance.ExecSP(cn_spAdd,
        //        DABase.Instance.Parameter(cn_pmUserID, _userid),
        //        DABase.Instance.Parameter(cn_pmServicePackageID, _servicepackageid),
        //        // DABase.Instance.Parameter(cn_pmCustomerCode, _customercode),
        //        DABase.Instance.Parameter(cn_pmServicePackageName, _servicepackagename),
        //        DABase.Instance.Parameter(cn_pmServicePackageContents, _servicepackagecontents),
        //        DABase.Instance.Parameter(cn_pmServicePackageFee, _servicepackagefee),
        //        DABase.Instance.Parameter(cn_pmNewServicePackageID, _servicepackageid));

        //        this.SetEntityState(EntityStateType.Added);
        //        Customers.Listing.List(_userid);
        //    }
        //    else
        //    {
        //        throw new System.Exception(Validator.ToString());
        //        ///MessageBox.Show(ValidationInstance.ToString());
        //    }



        //}

        //public void ModifyServicePackage(int UserId)
        //{
        //    _userid = UserId;

        //    if ((_servicepackageid != "0") && (Validator.Count == 0))
        //    {
        //        try
        //        {
        //            DABase.Instance.ExecSP(cn_spModify,
        //            DABase.Instance.Parameter(cn_pmUserID, _userid),
        //            DABase.Instance.Parameter(cn_pmServicePackageID, _servicepackageid),
        //            // DABase.Instance.Parameter(cn_pmCustomerCode, _customercode),
        //            DABase.Instance.Parameter(cn_pmServicePackageName, _servicepackagename),
        //            DABase.Instance.Parameter(cn_pmServicePackageContents, _servicepackagecontents),
        //            DABase.Instance.Parameter(cn_pmServicePackageFee, _servicepackagefee),
        //            DABase.Instance.Parameter(cn_pmNewServicePackageID, _servicepackageid));

        //            this.SetEntityState(EntityStateType.Modified);
        //            // Items.Listing.List(iLoggedUserId);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message, ex.InnerException);
        //        }
        //    }
        //    else
        //        throw new Exception(Validator.ToString());


        //}

        //public void DeleteServicePackage(int UserId)
        //{
        //    if (_servicepackageid != "0")
        //    {
        //        DABase.Instance.ExecSP(cn_spDelete,
        //            DABase.Instance.Parameter(cn_pmUserID, _userid),
        //            DABase.Instance.Parameter(cn_pmServicePackageID, _servicepackageid));
        //        this.SetEntityState(EntityStateType.Deleted);
        //        Customers.Listing.List(_userid);
        //    }
        //}

        //public Customer GetService(int UserID, string ServiceID)
        //{
        //    _userid = UserID;
        //    _serviceid = ServiceID;
        //    DataTable dt;

        //    dt = DABase.Instance.ExecSP(cn_spGet,
        //        DABase.Instance.Parameter(cn_pmUserID, _userid),
        //        DABase.Instance.Parameter(cn_pmServiceID, _serviceid));

        //    Customer customer = new Customer(_userid);

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        customer = new Customer(dr);
        //    }
        //    return customer;
        //}

        public void EnableServicePackage(int UserId)
        {
            if (_servicepackageid != "0")
            {
                DABase.Instance.ExecSP(cn_spEnable,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmServicePackageID, _servicepackageid),
                    DABase.Instance.Parameter(cn_pmEnabled,_enabled));
                this.SetEntityState(EntityStateType.Deleted);
                Customers.Listing.List(_userid);
            }
        }

        #endregion Methods
    }
}
