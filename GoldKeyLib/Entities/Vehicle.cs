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
    public class Vehicle : BaseEntity, IVehicle
    {

        #region Data

        //[tbld_Vehicles]
        //[fldi_Vehicle_ID] [int] NOT NULL,
        //[fldc_Customer_ID] [nchar](10) NOT NULL,
        //[fldc_Vehicle_ID]  AS([dbo].[udf_ZeroPaddingAndConcat]('TK-', CONVERT([nvarchar](10),[fldi_Vehicle_ID]),(6),'0')),
        //[fldc_Year] [char](4) NULL,
        //[fldc_Make] [nvarchar](100) NULL,
        //[fldc_Model] [nvarchar](100) NULL,
        //[fldc_Color] [varchar](50) NULL,
        //[fldc_Trim] [varchar](50) NULL,
        //[fldc_Condition] [varchar](50) NULL,
        //[fldi_Mileage] [int] NULL,
        //[fldc_VINumber] [nvarchar](17) NULL,
        //[fldc_Note] [nvarchar](max) NULL

        



        //'Database Field Names
        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fVehicleID = "fldc_Vehicle_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fTrackingID = "fldc_Tracking_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCustomerID = "fldc_Customer_ID";

        ///// <summary>
        ///// Const used to identify DA Field Name
        ///// </summary>
        private const string cn_fYear = "fldc_Year";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fMake = "fldc_Make";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fModel = "fldc_Model";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fColor = "fldc_Color";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fTrim = "fldc_Trim";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCondition = "fldc_Condition";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fMileage = "fldi_Mileage";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fVINumber = "fldc_VINumber";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fNote = "fldc_Note";

        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spAdd = "pr_AddVehicle";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spModify = "pr_ModifyVehicle";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spDelete = "pr_DeleteVehicle";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spGet = "pr_GetVehicle";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spGetCustomerVehicle = "pr_GetCustomerVehicle";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spGetCustomerVehicleServicePackage = "pr_GetCustomerVehicleServicePackage";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmUserID = "@pUserId";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmNewVehicleID = "@pNewVehicleID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmVehicleID = "@pVehicleID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmTrackingID = "@pTrackingD";

        ///// <summary>
        ///// private var used for DA Stored Proc variable
        ///// </summary>
        private const string cn_pmCustomerID = "@pCustomerID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmYear = "@pYear";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmMake = "@pMake";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmModel = "@pModel";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmColor = "@pColor";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmTrim = "@pTrim";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCondition = "@pCondition";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmMileage = "@pMileage";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmVINumber = "@pVINumber";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmNote = "@pNote";

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
        private string _vehicleid;
        /// </summary>
        private string _trackingid;
        /// <summary>
        /// private backing field
        private string _year;
        /// </summary>

        /// <summary>
        /// private backing field
        /// </summary>
        private string _make;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _model;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _color;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _trim;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _condition;
        /// <summary>
        /// private backing field
        /// </summary>
        private int _mileage;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _vinumber;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _note;
        /// <summary>
        /// private backing field
        /// </summary>
        private List<VehicleImage> _vehicleimages = new List<VehicleImage>();

        /// <summary>
        /// private backing field
        /// </summary>
        private CustomerServicePackage _customervehicleservicepackage = new CustomerServicePackage();

        //'Property Names'
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnVehicleID = "VehicleID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnTrackingID = "TrackingID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCustomerID = "CustomerID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnYear = "Year";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnMake = "Make";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnModel = "Model";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnColor = "Color";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnTrim = "Trim";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCondition = "Condition";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnMileage = "Mileage";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnVINumber = "VINumber";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnNote = "Note";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCompanyName = "CompanyName";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnVehicleImages = "VehicleImages";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCustomerVehicleServicePackage = "CustomerVehicleServicePackage";





        #endregion Private Attributes

        #region Properties

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

        //TrackinghID
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnTrackingID)]
        [Required()]
        public string TrackingID
        {
            get
            { return _trackingid; }
            set
            {
                if (_trackingid != value)
                {
                    this._trackingid = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnTrackingID);
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

        //Year
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnYear)]
        [Required()]
        public string Year
        {
            get
            { return _year; }
            set
            {
                if (_year != value)
                {
                    this._year = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnYear);
                }
            }
        }

        //Make
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnMake)]
        [Required()]
        public string Make
        {
            get
            { return _make; }
            set
            {
                if (_make != value)
                {
                    this._make = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnMake);
                }
            }
        }

        //Model
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnModel)]
        [Required()]
        public string Model
        {
            get
            { return _model; }
            set
            {
                if (_model != value)
                {
                    this._model = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnModel);
                }
            }
        }

        //Color
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnColor)]
        [Required()]
        public string Color
        {
            get
            { return _color; }
            set
            {
                if (_color != value)
                {
                    this._color = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnColor);
                }
            }
        }

        //Trim
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnTrim)]
        [Required()]
        public string Trim
        {
            get
            { return _trim; }
            set
            {
                if (_trim != value)
                {
                    this._trim = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnTrim);
                }
            }
        }

        //Condition
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnCondition)]
        [Required()]
        public string Condition
        {
            get
            { return _condition; }
            set
            {
                if (_condition != value)
                {
                    this._condition = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnCondition);
                }
            }
        }

        //Mileage
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnMileage)]
        [Required()]
        public int Mileage
        {
            get
            { return _mileage; }
            set
            {
                if (_mileage != value)
                {
                    this._mileage = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnMileage);
                }
            }
        }

        //VINumber
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnVINumber)]
        [Required()]
        public string VINumber
        {
            get
            { return _vinumber; }
            set
            {
                if (_vinumber != value)
                {
                    this._vinumber = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnVINumber);
                }
            }
        }

        //Note
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnNote)]
        [Required()]
        public string Note
        {
            get
            { return _note; }
            set
            {
                if (_note != value)
                {
                    this._note = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnNote);
                }
            }
        }

        [DisplayName(cn_pnVehicleImages)]
        [Required()]
        public List<VehicleImage> VehicleImages
        {
            get
            { return _vehicleimages; }
            set
            {
                if (_vehicleimages != value)
                {
                    this._vehicleimages = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnNote);
                }
            }
        }
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor ()
        /// </summary>
        public Vehicle()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor ()
        /// </summary>
        public Vehicle(int UserId)
        {
            //this.Validator = new Validation();
            _userid = UserId;
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public Vehicle(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._vehicleid = dr[cn_fVehicleID].ToString();
            this._trackingid = dr[cn_fTrackingID].ToString();
            this._customerid = dr[cn_fCustomerID].ToString();
            this._year = dr[cn_fYear].ToString();
            this._make = dr[cn_fMake].ToString();
            this._model = dr[cn_fModel].ToString();
            this._color = dr[cn_fColor].ToString();
            this._trim = dr[cn_fTrim].ToString();
            this._condition = dr[cn_fCondition].ToString();
            this._mileage = Convert.ToInt32(dr[cn_fMileage].ToString());
            this._vinumber = dr[cn_fVINumber].ToString();
            this._note = dr[cn_fNote].ToString();

            DataTable dt;
            dt = DABase.Instance.ExecSP(cn_spGetCustomerVehicleServicePackage,
                 DABase.Instance.Parameter(cn_pmUserID, _userid),
                 DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                 DABase.Instance.Parameter(cn_pmVehicleID, this._vehicleid));

            CustomerServicePackage customervehicleservicepackage = new CustomerServicePackage();

            foreach (DataRow servicepackage in dt.Rows)
            {
                customervehicleservicepackage = new CustomerServicePackage(servicepackage);
                this._customervehicleservicepackage = customervehicleservicepackage;
            }



            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
        public void AddVehicle(int UserId)
        {
            _userid = UserId;

            if ((_customerid == "0") && (this.Validator.Count == 0))
            {
                DABase.Instance.ExecSP(cn_spAdd,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
                DABase.Instance.Parameter(cn_pmTrackingID, _trackingid),
                DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                DABase.Instance.Parameter(cn_pmYear, _year),
                DABase.Instance.Parameter(cn_pmMake, _make),
                DABase.Instance.Parameter(cn_pmModel, _model),
                DABase.Instance.Parameter(cn_pmColor, _color),
                DABase.Instance.Parameter(cn_pmTrim, _trim),
                DABase.Instance.Parameter(cn_pmCondition, _condition),
                DABase.Instance.Parameter(cn_pmMileage, _mileage),
                DABase.Instance.Parameter(cn_pmVINumber, _vinumber),
                DABase.Instance.Parameter(cn_pmNote, _note),
                DABase.Instance.Parameter(cn_pmNewVehicleID, _vehicleid));

                this.SetEntityState(EntityStateType.Added);
                //Vehicles.ListVehicles(_userid);
            }
            else
            {
                throw new System.Exception(Validator.ToString());
                ///MessageBox.Show(ValidationInstance.ToString());
            }

        }

        public void ModifyVehicle(int UserId)
        {
            _userid = UserId;

            if ((_customerid != "0") && (Validator.Count == 0))
            {
                try
                {
                    DABase.Instance.ExecSP(cn_spModify,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
                    DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                    DABase.Instance.Parameter(cn_pmYear, _year),
                    DABase.Instance.Parameter(cn_pmMake, _make),
                    DABase.Instance.Parameter(cn_pmModel, _model),
                    DABase.Instance.Parameter(cn_pmColor, _color),
                    DABase.Instance.Parameter(cn_pmTrim, _trim),
                    DABase.Instance.Parameter(cn_pmCondition, _condition),
                    DABase.Instance.Parameter(cn_pmMileage, _mileage),
                    DABase.Instance.Parameter(cn_pmVINumber, _vinumber),
                    DABase.Instance.Parameter(cn_pmNote, _note),
                    DABase.Instance.Parameter(cn_pmNewVehicleID, _vehicleid));

                    this.SetEntityState(EntityStateType.Modified);
                    Vehicles.ListVehicles(_userid);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            else
                throw new Exception(Validator.ToString());


        }

        public void DeleteVehicle(int UserId)
        {
            if (_customerid != "0")
            {
                try
                {
                    DABase.Instance.ExecSP(cn_spDelete,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
                    DABase.Instance.Parameter(cn_pmCustomerID, _customerid));

                    this.SetEntityState(EntityStateType.Deleted);
                    Vehicles.ListVehicles(_userid);
                }
                catch(DAException ex)
                {
                    if (ex.Message.Contains("images"))
                    {
                        this.Validator.ValidateVehicleImages(ex.Message, true);
                    }
                    if (ex.Message.Contains("Drop Off"))
                    {
                        this.Validator.ValidateVehicleDropOff(ex.Message, true);
                    }
                    if (ex.Message.Contains("Pick Up"))
                    {
                        this.Validator.ValidateVehiclePickUp(ex.Message, true);
                    }
                    
                    throw (ex);
                }
                
            }
        }

        public Vehicle GetVehicle(int UserID, string VehicleID)
        {
            _userid = UserID;
            _vehicleid = VehicleID;
            _customerid = CustomerID;
            DataTable dt;

            dt = DABase.Instance.ExecSP(cn_spGet,
                 DABase.Instance.Parameter(cn_pmUserID, _userid),
                 DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid));
                 

            Vehicle vehicle = new Vehicle(_userid);

            foreach (DataRow dr in dt.Rows)
            {
                vehicle = new Vehicle(dr);
            }

            return vehicle;
        }

        public Vehicle GetCustomerVehicle(int UserID, string CustomerID)
        {
            _userid = UserID;
            _customerid = CustomerID;
            DataTable dt;

            dt = DABase.Instance.ExecSP(cn_spGetCustomerVehicle,
                 DABase.Instance.Parameter(cn_pmUserID, _userid),
                 DABase.Instance.Parameter(cn_pmCustomerID, _customerid));


            Vehicle vehicle = new Vehicle(_userid);

            foreach (DataRow dr in dt.Rows)
            {
                vehicle = new Vehicle(dr);
            }


            dt = DABase.Instance.ExecSP(cn_spGetCustomerVehicleServicePackage,
                 DABase.Instance.Parameter(cn_pmUserID, _userid),
                 DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                 DABase.Instance.Parameter(cn_pmVehicleID, vehicle._vehicleid));

            CustomerServicePackage customervehicleservicepackage = new CustomerServicePackage();

            foreach (DataRow dr in dt.Rows)
            {
                customervehicleservicepackage = new CustomerServicePackage(dr);
                this._customervehicleservicepackage = customervehicleservicepackage;
            }


            return vehicle;
        }

        #endregion Methods
    }
}
