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
    public class CustomerActivityType : BaseEntity, ICustomerActivityType
    {

        #region Data

        //tbld_VehicleActivityTypes
        //[fldc_VehicleActivityType_ID]           NCHAR(10) NULL,
        //[fldc_VehicleActivityTypeName]         NCHAR(50)   NULL,

        //'Database Field Names
        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCustomerActivityTypeID = "fldc_CustomerActivityType_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCustomerActivityTypeName = "fldc_CustomerActivityTypeName";

        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spAdd = "pr_AddCustomerActivityType";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spModify = "pr_ModifyCustomerActivityType";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spDelete = "pr_DeleteCustomerActivityType";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spGet = "pr_GetCustomerActivityType";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmUserID = "@pUserId";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmNewCustomerActivityTypeID = "@pNewCustomerActivityTypeID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCustomerActivityTypeID = "@pCustomerActivityTypeID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCustomerActivityTypeName = "@pCustomerActivityTypeName";

        #endregion Data

        #region Private Attributes
        // private backing fields
        /// <summary>
        private int _userid = 0;
        /// private backing field
        /// </summary>
        private string _customeractivitytypeid;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _customeractivitytypename;

        //'Property Names'
        /// <summary>
        /// Const used to identify Entity Property Name
        /// </summary>
        private const string cn_pnCustomerActivityTypeID = "CustomerActivityTypeID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCustomerActivityTypeName = "CustomerActivityTypeName";


        #endregion Private Attributes

        #region Properties

        //VehicleActivityType
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnCustomerActivityTypeID)]
        [Required()]
        public string CustomerActivityTypeID
        {
            get
            { return _customeractivitytypeid; }
            set
            {
                if (_customeractivitytypeid != value)
                {
                    this._customeractivitytypeid = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnCustomerActivityTypeID);
                }
            }
        }

        //VehicleActivityTypeName
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnCustomerActivityTypeName)]
        [Required()]
        public string CustomerActivityTypeName
        {
            get
            { return _customeractivitytypename; }
            set
            {
                if (_customeractivitytypename != value)
                {
                    this._customeractivitytypename = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnCustomerActivityTypeName);
                }
            }
        }


        #endregion Properties

        #region Constructors
        /// <summary>
        /// Constructor ()
        /// </summary>
        public CustomerActivityType()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }
        /// <summary>
        /// Constructor ()
        /// </summary>
        public CustomerActivityType(int UserId)
        {
            //this.Validator = new Validation();
            _userid = UserId;
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public CustomerActivityType(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._customeractivitytypeid = dr[cn_fCustomerActivityTypeID].ToString();
            this._customeractivitytypename = dr[cn_fCustomerActivityTypeName].ToString();


            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
        public void AddCustomerActivityType(int UserId)
        {
            _userid = UserId;

            if ((_customeractivitytypeid == "0") && (this.Validator.Count == 0))
            {
                DABase.Instance.ExecSP(cn_spAdd,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmCustomerActivityTypeID, _customeractivitytypeid),
                DABase.Instance.Parameter(cn_pmCustomerActivityTypeName, _customeractivitytypename),
               DABase.Instance.Parameter(cn_pmNewCustomerActivityTypeID, _customeractivitytypeid));

                this.SetEntityState(EntityStateType.Added);
                Customers.Listing.List(_userid);
            }
            else
            {
                throw new System.Exception(Validator.ToString());
                ///MessageBox.Show(ValidationInstance.ToString());
            }



        }

        public void ModifyCustomerActivityType(int UserId)
        {
            _userid = UserId;

            if ((_customeractivitytypeid != "0") && (Validator.Count == 0))
            {
                try
                {
                    DABase.Instance.ExecSP(cn_spModify,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmCustomerActivityTypeID, _customeractivitytypeid),
                    DABase.Instance.Parameter(cn_pmCustomerActivityTypeName, _customeractivitytypename),
                    DABase.Instance.Parameter(cn_pmNewCustomerActivityTypeID, _customeractivitytypeid));

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

        public void DeleteCustomerActivityType(int UserId)
        {
            if (_customeractivitytypeid != "0")
            {
                DABase.Instance.ExecSP(cn_spDelete,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmCustomerActivityTypeID, _customeractivitytypeid));
                this.SetEntityState(EntityStateType.Deleted);
                Customers.Listing.List(_userid);
            }
        }

        public VehicleActivityType GetCustomerActivityType(int UserID, string VehicleActivityTypeID)
        {
            _userid = UserID;
            _customeractivitytypeid = VehicleActivityTypeID;
            DataTable dt;

            dt = DABase.Instance.ExecSP(cn_spGet,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmCustomerActivityTypeID, _customeractivitytypeid));

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
