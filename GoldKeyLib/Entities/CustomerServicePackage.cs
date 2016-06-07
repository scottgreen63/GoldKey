using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using GoldKeyLib.Interfaces;
using GoldKeyLib.Entities;
using GoldKeyLib.DA;

namespace GoldKeyLib.Entities
{
    public class CustomerServicePackage : BaseEntity, ICustomerServicePackage

    {
        //[DataMember]
        //string CustomerID { get; set; }
        //[DataMember]
        //string VehicleID { get; set; }
        //[DataMember]
        //string ServicePackageID { get; set; }
        

        #region Data

        //tbld_CustomerServicePackages
        //fldc_Customer_ID            NCHAR(10)   NOT NULL,
        //fldc_Vehicle_ID             NCHAR(10)   NOT NULL,
        //fldc_ServicePackage_ID      NCHAR(10)   NULL,
        

        //'Database Field Names
        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCustomerID = "fldc_Customer_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fVehicleID = "fldc_Vehicle_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServicePackageID = "fldc_ServicePackage_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServicePackageName = "fldc_ServicePackageName";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServicePackageFee = "fldd_ServicePackageFee";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fScheduleDate = "fldd_ScheduleDate";


        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spAdd = "pr_AddCustomerServicePackage";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spModify = "pr_Modify__";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spDelete = "pr_DeleteCustomerServicePackage";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spGet = "pr_GetCustomerVehicleServicePackage";
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spGetDetails = "pr_ListCustomerVehicleServicePackageDetails";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmUserID = "@pUserId";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmNewID = "@pNewID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCustomerID = "@pCustomerId";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmVehicleID = "@pVehicleId";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmServicePackageID = "@pServicePackageId";

       
        #endregion Data

        #region Private Attributes
        // private backing fields
        /// <summary>
        private int _userid = 0;
        /// private backing field
        /// </summary>
        private string _customerid;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _vehicleid;
        /// <summary>
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
        private decimal _servicepackagefee;

        private BaseEntityList<CustomerServicePackageDetail> _customerservicepackagedetails;



        //'Property Names'
        /// <summary>
        /// Const used to identify Entity Property Name
        /// </summary>
        private const string cn_pnCustomerID = "CustomerID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnVehicleID = "VehicleID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnServicePackageID = "ServicePackageID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnServicePackageName = "ServicePackageName";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnServicePackageFee = "ServicePackageFee";

        #endregion Private Attributes

        #region Properties

        //CustomerID
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnCustomerID)]
        [Required()]
        public string CustomerID
        {
            get
            { return _customerid; }
            set
            {
                if (_customerid != value)
                {
                    this._customerid = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnCustomerID);
                }
            }
        }

        //VehicleID
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnVehicleID)]
        [Required()]
        public string VehicleID
        {
            get
            { return _vehicleid; }
            set
            {
                if (_vehicleid != value)
                {
                    this._vehicleid = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnVehicleID);
                }
            }
        }

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

       
        public BaseEntityList<CustomerServicePackageDetail> CustomerServicePackageDetails
        {
            get { return _customerservicepackagedetails; }
            set { _customerservicepackagedetails = value; }
        }
        
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Constructor ()
        /// </summary>
        public CustomerServicePackage()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }
        /// <summary>
        /// Constructor ()
        /// </summary>
        public CustomerServicePackage(int UserId)
        {
            //this.Validator = new Validation();
            _userid = UserId;
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public CustomerServicePackage(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._customerid = dr[cn_fCustomerID].ToString();
            this._vehicleid = dr[cn_fVehicleID].ToString();
            this._servicepackageid = dr[cn_fServicePackageID].ToString();
            this._servicepackagename = dr[cn_fServicePackageName].ToString();
            this._servicepackagefee = Convert.ToDecimal(dr[cn_fServicePackageFee].ToString());
            
            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
        public void AddCustomerServicePackage(int UserId)
        {
            _userid = UserId;

            if ((_customerid == "0") && (this.Validator.Count == 0))
            {
                DABase.Instance.ExecSP(cn_spAdd,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
                DABase.Instance.Parameter(cn_pmServicePackageID, _servicepackageid),
                DABase.Instance.Parameter(cn_pmNewID, _userid)); ///this is not likely needed

                this.SetEntityState(EntityStateType.Added);
                Customers.Listing.List(_userid);
            }
            else
            {
                throw new System.Exception(Validator.ToString());
                ///MessageBox.Show(ValidationInstance.ToString());
            }



        }

        public void DeletedCustomerServicePackage(int UserId)
        {
            if (_customerid != "0")
            {
                DABase.Instance.ExecSP(cn_spDelete,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                    DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
                    DABase.Instance.Parameter(cn_pmServicePackageID, _servicepackageid)
                    );
                this.SetEntityState(EntityStateType.Deleted);
                CustomerServicePackages.Listing.List(_userid, _customerid);
            }
        }

        public CustomerServicePackage GetCustomerVehicleServicePackage(int UserID, string CustomerID, string VehicleID)
        {
            this._userid = UserID;
            this._customerid = CustomerID;
            this._vehicleid = VehicleID;
            
            

            DataTable dt;

            dt = DABase.Instance.ExecSP(cn_spGet,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid));

            CustomerServicePackage customerservicepackage = new CustomerServicePackage();

            foreach (DataRow dr in dt.Rows)
            {
                customerservicepackage = new CustomerServicePackage(dr);
            }

            this._servicepackageid = customerservicepackage.ServicePackageID;

            dt = DABase.Instance.ExecSP(cn_spGetDetails,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
                DABase.Instance.Parameter(cn_pnServicePackageID, _servicepackageid));

            CustomerServicePackageDetail customerservicepackagedetail = new CustomerServicePackageDetail();
            foreach (DataRow dr in dt.Rows)
            {
                customerservicepackagedetail = new CustomerServicePackageDetail(dr);
                this._customerservicepackagedetails.Add(customerservicepackagedetail);
            }

            return customerservicepackage;
        }



        #endregion Methods


    }
}
