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
    public class VehicleActivity : BaseEntity, IVehicleActivity
    {

        #region Data

   
    //[tbld_VehicleActivity]
	//[fldc_Customer_ID]              [nchar](10) NULL,
	//[fldc_Vehicle_ID]               [nchar](10) NULL,
	//[fldd_ActivityDateTime]         [datetime]  NULL,
	//[fldc_VehicleActivityType_ID]   [nchar](10) NULL




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
        private const string cn_fActivityDateTime = "fldd_ActivityDateTime";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fVehicleActivityTypeID = "fldc_VehicleActivityType_ID";

        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spAdd = "pr_AddVehicleActivity";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spModify = "pr_ModifyVehicleActivity";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spDelete = "pr_DeleteVehicleActivity";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spGet = "pr_GetVehicleActivityType";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmUserID = "@pUserId";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmVehicleID = "@pVehicleID";

        
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCustomerID = "@pCustomerID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmVehicleActivityTypeID = "@pVehicleActivityTypeID";

        #endregion Data

        #region Private Attributes
        // private backing field
        /// <summary>
        private int _userid = 0;
        
        /// private backing field
        /// </summary>
        private string _vehicleid;

        /// private backing field
        /// </summary>
        private string _customerid;

        /// private backing field 
        /// <summary>
        /// ActivityDateTime
        /// </summary>
        private DateTime _activitydatetime;

        /// <summary>
        /// private backing field
        /// </summary>
        private string _vehicleactivitytypeid;

        //'Property Names'
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnVehicleID = "VehicleID";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCustomerID = "CustomerID";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnActivityDateTime = "ActivityDateTime";

        /// <summary>
        /// Const used to identify Entity Property Name
        /// </summary>
        private const string cn_pnVehicleActivityTypeID = "VehicleActivityTypeID";

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

        //ActivityDateTime
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnActivityDateTime)]
        [Required()]
        public DateTime ActivityDateTime
        {
            get
            { return _activitydatetime; }
            set
            {
                if (_activitydatetime != value)
                {
                    this._activitydatetime = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnActivityDateTime);
                }
            }
        }

        //VehicleActivityTypeID
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnVehicleActivityTypeID)]
        [Required()]
        public string VehicleActivityTypeID
        {
            get
            { return _vehicleactivitytypeid; }
            set
            {
                if (_vehicleactivitytypeid != value)
                {
                    this._vehicleactivitytypeid = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnVehicleActivityTypeID);
                }
            }
        }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Constructor ()
        /// </summary>
        public VehicleActivity()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }
        /// <summary>
        /// Constructor ()
        /// </summary>
        public VehicleActivity(int UserId)
        {
            //this.Validator = new Validation();
            _userid = UserId;
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public VehicleActivity(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._vehicleactivitytypeid = dr[cn_fVehicleActivityTypeID].ToString();
            this._vehicleid = dr[cn_fVehicleID].ToString();
            this._customerid = dr[cn_fCustomerID].ToString();



            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
        public void AddVehicleActivity(int UserId)
        {
            _userid = UserId;

            if ((_vehicleactivitytypeid == "0") && (this.Validator.Count == 0))
            {
                DABase.Instance.ExecSP(cn_spAdd,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
                DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
               DABase.Instance.Parameter(cn_pmVehicleActivityTypeID, _vehicleactivitytypeid));

                this.SetEntityState(EntityStateType.Added);
                Customers.Listing.List(_userid);
            }
            else
            {
                throw new System.Exception(Validator.ToString());
                ///MessageBox.Show(ValidationInstance.ToString());
            }



        }

        
        public void DeleteVehicleActivity(int UserId)
        {
            if (_vehicleactivitytypeid != "0")
            {
                DABase.Instance.ExecSP(cn_spDelete,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
                    DABase.Instance.Parameter(cn_pmCustomerID, _customerid));
                this.SetEntityState(EntityStateType.Deleted);
                Customers.Listing.List(_userid);
            }
        }

        

        #endregion Methods
    }
}
