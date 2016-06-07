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
    public class CustomerActivity : BaseEntity, ICustomerActivity
    {

        #region Data

        //[tbld_CustomerActivity]
        //[fldc_Customer_ID]                  [nchar](10) NOT NULL,
        //[fldd_ActivityDateTime]             [datetime]  NULL,
        //[fldc_CustomerActivityType_ID]      [nchar](10) NULL

        //'Database Field Names
        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCustomerID = "fldc_Customer_ID";

        

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fActivityDateTime = "fldd_ActivityDateTime";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCustomerActivityTypeID = "fldc_CustomerActivityType_ID";

        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spAdd = "pr_AddCustomerActivity";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spModify = "pr_ModifyVehicleActivity";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spDelete = "pr_DeleteCustomerActivity";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spGet = "pr_GetVehicleActivityType";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmUserID = "@pUserId";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCustomerID = "@pCustomerID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmActivityDateTime = "@pActivityDateTime";
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCustomerActivityTypeID = "@pCustomerActivityTypeID";

        #endregion Data

        #region Private Attributes
        // private backing field
        /// <summary>
        private int _userid = 0;

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
        private string _customeractivitytypeid;

        //'Property Names'
        
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
        private const string cn_pnCustomerActivityTypeID = "CustomerActivityTypeID";

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

        //CustomerActivityTypeID
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

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Constructor ()
        /// </summary>
        public CustomerActivity()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }
        /// <summary>
        /// Constructor ()
        /// </summary>
        public CustomerActivity(int UserId)
        {
            //this.Validator = new Validation();
            _userid = UserId;
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public CustomerActivity(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._customerid = dr[cn_fCustomerID].ToString();
            this._customeractivitytypeid = dr[cn_fCustomerActivityTypeID].ToString();
            this._activitydatetime = Convert.ToDateTime(dr[cn_pnActivityDateTime].ToString());


            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
        public void AddCustomerActivity(int UserId)
        {
            _userid = UserId;

            if ((_customeractivitytypeid == "0") && (this.Validator.Count == 0))
            {
                DABase.Instance.ExecSP(cn_spAdd,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                DABase.Instance.Parameter(cn_pmCustomerActivityTypeID, _customeractivitytypeid));

                this.SetEntityState(EntityStateType.Added);
                Customers.Listing.List(_userid);
            }
            else
            {
                throw new System.Exception(Validator.ToString());
                ///MessageBox.Show(ValidationInstance.ToString());
            }



        }


        public void DeleteCustomerActivity(int UserId)
        {
            if (_customeractivitytypeid != "0")
            {
                DABase.Instance.ExecSP(cn_spDelete,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                    DABase.Instance.Parameter(cn_pmActivityDateTime, _activitydatetime),
                    DABase.Instance.Parameter(cn_pmCustomerActivityTypeID, _customeractivitytypeid)
                    );
                this.SetEntityState(EntityStateType.Deleted);
                Customers.Listing.List(_userid);
            }
        }



        #endregion Methods
    }
}
