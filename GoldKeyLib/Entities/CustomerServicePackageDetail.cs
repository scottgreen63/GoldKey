using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using GoldKeyLib.Interfaces;
using GoldKeyLib.Entities;
using GoldKeyLib.DA;
using System.Collections.Generic;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public class CustomerServicePackageDetail : BaseEntity, ICustomerServicePackageDetail
    {

        #region Data

        //[tbld_CustomerServicePackageDetail]
        //[fldc_Customer_ID]
		//,[fldc_Vehicle_ID]
		//,csd.[fldc_ServicePackage_ID]
		//,s.fldc_Service_ID
		//,s.fldc_ServiceName
		//,s.fldc_ServiceProvider
		//,s.fldd_ServiceFee
		//,csd.fldd_ServiceStartDateTime
		//,csd.fldd_ServiceFinishDateTime
		//,csd.fldd_ServiceStatus

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
        private const string cn_fServiceID = "fldc_Service_ID";

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
        private const string cn_fServiceStartDateTime = "fldd_ServiceStartDateTime";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServiceFinishDateTime = "fldd_ServiceFinishDateTime";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fServiceStatus = "fldd_ServiceStatus";



        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spAdd = "pr_AddVehicle";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spModify = "pr_ModifyVehicle";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        // private const string cn_spDelete = "pr_DeleteVehicle";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spGet = "pr_GetVehicle";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spGetCustomerVehicle = "pr_GetCustomerVehicle";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        //private const string cn_pmUserID = "@pUserId";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        //private const string cn_pmNewVehicleID = "@pNewVehicleID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        //private const string cn_pmVehicleID = "@pVehicleID";

        ///// <summary>
        ///// private var used for DA Stored Proc variable
        ///// </summary>
        //private const string cn_pmCustomerID = "@pCustomerID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        //private const string cn_pmYear = "@pYear";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        //private const string cn_pmMake = "@pMake";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        //private const string cn_pmModel = "@pModel";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        //private const string cn_pmColor = "@pColor";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        //private const string cn_pmTrim = "@pTrim";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        //private const string cn_pmCondition = "@pCondition";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        //private const string cn_pmMileage = "@pMileage";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        //private const string cn_pmVINumber = "@pVINumber";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        //private const string cn_pmNote = "@pNote";

        #endregion Data

        #region Private Attributes
        /// <summary>
        /// private backing field
        /// </summary>
        private int _userid =0;
        /// <summary>
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
        private DateTime _servicestartdatetime;
        /// <summary>
        /// private backing field
        /// </summary>
        private DateTime _servicefinishdatetime;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _servicestatus;


        //'Property Names'
        
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnUserID = "UserID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCustomerID = "CustomerID";
        /// <summary>
        /// Const used to identify Entity Property Name
        /// </summary>
        private const string cn_pnVehicleID = "VehicleID";  
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnServicePackageID = "ServicePackageID";
        /// <summary>
        /// Const used to identify BO Property Name
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
        private const string cn_pnServiceStartDateTime = "ServiceStartDateTime";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnServiceFinishDateTime = "ServiceFinishDateTime";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnServiceStatus = "ServiceStatus";


        #endregion Private Attributes

        #region Properties
        //UserID
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnUserID)]
        [Required()]
        public int UserID
        {
            get
            { return _userid; }
            set
            {
                if (_userid != value)
                {
                    this._userid = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnUserID);
                }
            }
        }

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

        //ServiceStartDateTime
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnServiceStartDateTime)]
        [Required()]
        public DateTime ServiceStartDateTime
        {
            get
            { return _servicestartdatetime; }
            set
            {
                if (_servicestartdatetime != value)
                {
                    this._servicestartdatetime = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnServiceStartDateTime);
                }
            }
        }

        //ServiceFinishDateTime
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnServiceFinishDateTime)]
        [Required()]
        public DateTime ServiceFinishDateTime
        {
            get
            { return _servicefinishdatetime; }
            set
            {
                if (_servicefinishdatetime != value)
                {
                    this._servicefinishdatetime = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnServiceFinishDateTime);
                }
            }
        }

        //ServiceStatus
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnServiceStatus)]
        [Required()]
        public string ServiceStatus
        {
            get
            { return _servicestatus; }
            set
            {
                if (_servicestatus != value)
                {
                    this._servicestatus = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnServiceStatus);
                }
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor ()
        /// </summary>
        public CustomerServicePackageDetail()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor ()
        /// </summary>
        public CustomerServicePackageDetail(int UserId)
        {
            //this.Validator = new Validation();
            _userid = UserId;
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public CustomerServicePackageDetail(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._userid = 0;
            this._vehicleid = dr[cn_fVehicleID].ToString();
            this._customerid = dr[cn_fCustomerID].ToString();
            this._servicepackageid = dr[cn_fServicePackageID].ToString();
            this._serviceid = dr[cn_fServiceID].ToString();
            this._servicename = dr[cn_fServiceName].ToString();
            this._serviceprovider = dr[cn_fServiceProvider].ToString();
            this._servicefee = Convert.ToDecimal(dr[cn_fServiceFee].ToString());
            this._servicestartdatetime = Convert.ToDateTime(dr[cn_fServiceStartDateTime].ToString());
            this._servicefinishdatetime = Convert.ToDateTime(dr[cn_fServiceFinishDateTime].ToString());
            this._servicestatus = dr[cn_fServiceStatus].ToString();
           
            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
        //public void AddVehicle(int UserId)
        //{
        //    _userid = UserId;

        //    if ((_customerid == "0") && (this.Validator.Count == 0))
        //    {
        //        DABase.Instance.ExecSP(cn_spAdd,
        //        DABase.Instance.Parameter(cn_pmUserID, _userid),
        //        DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
        //        DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
        //        DABase.Instance.Parameter(cn_pmYear, _year),
        //        DABase.Instance.Parameter(cn_pmMake, _make),
        //        DABase.Instance.Parameter(cn_pmModel, _model),
        //        DABase.Instance.Parameter(cn_pmColor, _color),
        //        DABase.Instance.Parameter(cn_pmTrim, _trim),
        //        DABase.Instance.Parameter(cn_pmCondition, _condition),
        //        DABase.Instance.Parameter(cn_pmMileage, _mileage),
        //        DABase.Instance.Parameter(cn_pmVINumber, _vinumber),
        //        DABase.Instance.Parameter(cn_pmNote, _note),
        //        DABase.Instance.Parameter(cn_pmNewVehicleID, _vehicleid));

        //        this.SetEntityState(EntityStateType.Added);
        //        //Vehicles.ListVehicles(_userid);
        //    }
        //    else
        //    {
        //        throw new System.Exception(Validator.ToString());
        //        ///MessageBox.Show(ValidationInstance.ToString());
        //    }

        //}

        //public void ModifyVehicle(int UserId)
        //{
        //    _userid = UserId;

        //    if ((_customerid != "0") && (Validator.Count == 0))
        //    {
        //        try
        //        {
        //            DABase.Instance.ExecSP(cn_spModify,
        //            DABase.Instance.Parameter(cn_pmUserID, _userid),
        //            DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
        //            DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
        //            DABase.Instance.Parameter(cn_pmYear, _year),
        //            DABase.Instance.Parameter(cn_pmMake, _make),
        //            DABase.Instance.Parameter(cn_pmModel, _model),
        //            DABase.Instance.Parameter(cn_pmColor, _color),
        //            DABase.Instance.Parameter(cn_pmTrim, _trim),
        //            DABase.Instance.Parameter(cn_pmCondition, _condition),
        //            DABase.Instance.Parameter(cn_pmMileage, _mileage),
        //            DABase.Instance.Parameter(cn_pmVINumber, _vinumber),
        //            DABase.Instance.Parameter(cn_pmNote, _note),
        //            DABase.Instance.Parameter(cn_pmNewVehicleID, _vehicleid));

        //            this.SetEntityState(EntityStateType.Modified);
        //            Vehicles.ListVehicles(_userid);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message, ex.InnerException);
        //        }
        //    }
        //    else
        //        throw new Exception(Validator.ToString());


        //}

        //public void DeleteVehicle(int UserId)
        //{
        //    if (_customerid != "0")
        //    {
        //        DABase.Instance.ExecSP(cn_spDelete,
        //        DABase.Instance.Parameter(cn_pmUserID, _userid),
        //        DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
        //        DABase.Instance.Parameter(cn_pmCustomerID, _customerid));

        //        this.SetEntityState(EntityStateType.Deleted);
        //        Vehicles.ListVehicles(_userid);
        //    }
        //}

        //public Vehicle GetVehicle(int UserID, string VehicleID)
        //{
        //    _userid = UserID;
        //    _vehicleid = VehicleID;
        //    _customerid = CustomerID;
        //    DataTable dt;

        //    dt = DABase.Instance.ExecSP(cn_spGet,
        //         DABase.Instance.Parameter(cn_pmUserID, _userid),
        //         DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid));


        //    Vehicle vehicle = new Vehicle(_userid);

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        vehicle = new Vehicle(dr);
        //    }
        //    return vehicle;
        //}

        //public Vehicle GetCustomerVehicle(int UserID, string CustomerID)
        //{
        //    _userid = UserID;
        //    _customerid = CustomerID;
        //    DataTable dt;

        //    dt = DABase.Instance.ExecSP(cn_spGetCustomerVehicle,
        //         DABase.Instance.Parameter(cn_pmUserID, _userid),
        //         DABase.Instance.Parameter(cn_pmCustomerID, _customerid));


        //    Vehicle vehicle = new Vehicle(_userid);

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        vehicle = new Vehicle(dr);
        //    }
        //    return vehicle;
        //}

        #endregion Methods
    }
}
