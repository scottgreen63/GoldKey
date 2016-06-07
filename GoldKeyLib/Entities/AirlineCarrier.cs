using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using GoldKeyLib.Interfaces;
using GoldKeyLib.Entities;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public class AirlineCarrier : BaseEntity, IAirlineCarrier
    {
       
        #region Data

        //tbld_Carriers
        //[fldc_Carrier_ID]  VARCHAR(3)  NOT NULL
        //[fldc_CarrierName] VARCHAR(20) NOT NULL

        //'Database Field Names
        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCarrierID = "fldc_Carrier_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCarrierName = "fldc_CarrierName";

        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spAdd = "pr_AddCarrier";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spModify = "";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spDelete = "";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spSelect = "pr_GetCarrier";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmUserID = "@pUserId";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCarrierID = "@pCarrierID";

        #endregion Data

        #region Private Attributes
        // private backing fields
        /// <summary>
        /// private backing field
        /// </summary>
        private string _carrierid;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _carriername;
      
        //'Property Names'
        /// <summary>
        /// Const used to identify Entity Property Name
        /// </summary>
        private const string cn_pnCarrierID = "CarrierID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCarrierName = "CarrierName";

       

       
        #endregion Private Attributes

        #region Properties

        //CarrierId
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("CarrierID")]
        [Required()]
        public string CarrierID
        {
            get
            { return _carrierid; }
            set
            {
                if (_carrierid != value)
                {
                    this._carrierid = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnCarrierID);
                }
            }
        }

        //CarrierName
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("CarrierName")]
        [Required]
        public string CarrierName
        {
            get
            { return _carriername; }
            set
            {
                if (_carriername != value)
                {
                    this._carriername = value;
                    // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = sPropName });
                    this.SetEntityState(EntityStateType.Modified, _carriername);
                }
            }
        }


        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor ()
        /// </summary>
        public AirlineCarrier()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public AirlineCarrier(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._carrierid = dr[cn_fCarrierID].ToString();
            this._carriername = dr[cn_fCarrierName].ToString();
            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
               
        #endregion Methods
    }
}
