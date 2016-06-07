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
    public class VehicleActivityType : BaseEntity, IVehicleActivityType
    {

        #region Data

        //tbld_VehicleActivityTypes
        //[fldc_VehicleActivityType_ID]           NCHAR(10) NULL,
        //[fldc_VehicleActivityTypeName]         NCHAR(50)   NULL,

        //'Database Field Names
        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fVehicleActivityTypeID = "fldc_VehicleActivityType_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fVehicleActivityTypeName = "fldc_VehicleActivityTypeName";

        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spAdd = "pr_AddVehicleActivityType";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spModify = "pr_ModifyVehicleActivityType";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spDelete = "pr_DeleteVehicleActivityType";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spGet = "pr_GetVehicleActivityType";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmUserID = "@pUserId";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmNewVehicleActivityTypeID = "@pNewVehicleActivityTypeID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmVehicleActivityTypeID = "@pVehicleActivityTypeID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmVehicleActivityTypeName = "@pVehicleActivityTypeName";

        #endregion Data

        #region Private Attributes
        // private backing fields
        /// <summary>
        private int _userid = 0;
        /// private backing field
        /// </summary>
        private string _vehicleactivitytypeid;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _vehicleactivitytypename;
        
        //'Property Names'
        /// <summary>
        /// Const used to identify Entity Property Name
        /// </summary>
        private const string cn_pnVehicleActivityTypeID = "VehicleActivityTypeID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnVehicleActivityTypeName = "VehicleActivityTypeName";


        #endregion Private Attributes

        #region Properties

        //VehicleActivityType
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

        //VehicleActivityTypeName
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnVehicleActivityTypeName)]
        [Required()]
        public string VehicleActivityTypeName
        {
            get
            { return _vehicleactivitytypename; }
            set
            {
                if (_vehicleactivitytypename != value)
                {
                    this._vehicleactivitytypename = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnVehicleActivityTypeName);
                }
            }
        }

        
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Constructor ()
        /// </summary>
        public VehicleActivityType()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }
        /// <summary>
        /// Constructor ()
        /// </summary>
        public VehicleActivityType(int UserId)
        {
            //this.Validator = new Validation();
            _userid = UserId;
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public VehicleActivityType(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._vehicleactivitytypeid = dr[cn_fVehicleActivityTypeID].ToString();
            this._vehicleactivitytypename = dr[cn_fVehicleActivityTypeName].ToString();
           

            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
        public void AddVehicleActivityType(int UserId)
        {
            _userid = UserId;

            if ((_vehicleactivitytypeid == "0") && (this.Validator.Count == 0))
            {
                DABase.Instance.ExecSP(cn_spAdd,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmVehicleActivityTypeID, _vehicleactivitytypeid),
                DABase.Instance.Parameter(cn_pmVehicleActivityTypeName, _vehicleactivitytypename),
               DABase.Instance.Parameter(cn_pmNewVehicleActivityTypeID, _vehicleactivitytypeid));

                this.SetEntityState(EntityStateType.Added);
                Customers.Listing.List(_userid);
            }
            else
            {
                throw new System.Exception(Validator.ToString());
                ///MessageBox.Show(ValidationInstance.ToString());
            }



        }

        public void ModifyVehicleActivityType(int UserId)
        {
            _userid = UserId;

            if ((_vehicleactivitytypeid != "0") && (Validator.Count == 0))
            {
                try
                {
                    DABase.Instance.ExecSP(cn_spModify,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmVehicleActivityTypeID, _vehicleactivitytypeid),
                    DABase.Instance.Parameter(cn_pmVehicleActivityTypeName, _vehicleactivitytypename),
                    DABase.Instance.Parameter(cn_pmNewVehicleActivityTypeID, _vehicleactivitytypeid));

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

        public void DeleteVehicleActivityType(int UserId)
        {
            if (_vehicleactivitytypeid != "0")
            {
                DABase.Instance.ExecSP(cn_spDelete,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmVehicleActivityTypeID, _vehicleactivitytypeid));
                this.SetEntityState(EntityStateType.Deleted);
                Customers.Listing.List(_userid);
            }
        }

        public VehicleActivityType GetVehicleActivityType(int UserID, string VehicleActivityTypeID)
        {
            _userid = UserID;
            _vehicleactivitytypeid = VehicleActivityTypeID;
            DataTable dt;

            dt = DABase.Instance.ExecSP(cn_spGet,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmVehicleActivityTypeID, _vehicleactivitytypeid));

            VehicleActivityType vehicleactivitytype = new VehicleActivityType(_userid);

            foreach (DataRow dr in dt.Rows)
            {
                vehicleactivitytype = new VehicleActivityType(dr);
            }
            return vehicleactivitytype;
        }

       
        #endregion Methods
    }
}
