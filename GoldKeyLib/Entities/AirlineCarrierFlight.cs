using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using GoldKeyLib.Interfaces;
using GoldKeyLib.Entities;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public class AirlineCarrierFlight : BaseEntity, IAirlineCarrierFlight
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

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCarrierFlightNo = "fldc_CarrierFlightNo";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fScheduleDate = "fldd_ScheduleDate";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fScheduleDateTimeOfArrival = "fldd_ScheduleDateTimeOfArrival";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fActualDateTimeOfArrival = "fldd_ActualDateTimeOfArrival";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fFlightStatusCode = "fldc_FlightStatusCode";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fFlightStatus = "fldc_FlightStatus";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fScheduledGateAssignment = "fldc_ScheduledGateAssignment";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fActualGateAssignment = "fldc_ActualGateAssignment";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fScheduledBagClaimArea = "fldc_ScheduledBagClaimArea";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fActualBagClaimArea = "fldc_ActualBagClaimArea";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fBagsInDateTime = "fldd_BagsInDateTime";


        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spAdd = "";

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
        private const string cn_spSelect = "pr_GetCarrierFlight";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmUserID = "@pCarrierID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmFlightDate = "@pFlightDate";

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

        /// <summary>
        /// private backing field
        /// </summary>
        private string _carrierflightno;

        /// <summary>
        /// private backing field
        /// </summary>
        private DateTime _scheduledate;

        /// <summary>
        /// private backing field
        /// </summary>
        private DateTime _scheduledatetimeofarrival;

        /// <summary>
        /// private backing field
        /// </summary>
        private DateTime _actualdatetimeofarrival;

        /// <summary>
        /// private backing field
        /// </summary>
        private string _flightstatuscode;

        /// <summary>
        /// private backing field
        /// </summary>
        private string _flightstatus;

        /// <summary>
        /// private backing field
        /// </summary>
        private string _scheduledgateassignment;

        /// <summary>
        /// private backing field
        /// </summary>
        private string _actualgateassignment;

        /// <summary>
        /// private backing field
        /// </summary>
        private string _scheduledbagclaimarea;

        /// <summary>
        /// private backing field
        /// </summary>
        private string _actualbagclaimarea;

        /// <summary>
        /// private backing field
        /// </summary>
        private DateTime _bagsindatetime;

        //'Property Names'
        /// <summary>
        /// Const used to identify Entity Property Name
        /// </summary>
        private const string cn_pnCarrierID = "CarrierID";
        
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCarrierName = "CarrierName";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCarrierFlightNo = "CarrierFlightNo";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnScheduleDate = "ScheduleDate";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnScheduleDateTimeOfArrival = "ScheduleDateTimeOfArrival";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnActualDateTimeOfArrival = "ActualDateTimeOfArrival";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnFlightStatusCode = "FlightStatusCode";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnFlightStatus = "FlightStatus";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnScheduledGateAssignment = "ScheduledGateAssignment";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnActualGateAssignment = "ActualGateAssignment";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnScheduledBagClaimArea = "ScheduledBagClaimArea";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnActualBagClaimArea = "ActualBagClaimArea";

        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnBagsInDateTime = "BagsInDateTime";




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

        //CarrierFlightNo
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("CarrierFlightNo")]
        [Required]
        public string CarrierFlightNo
        {
            get
            { return _carrierflightno; }
            set
            {
                if (_carrierflightno != value)
                {
                    this._carrierflightno = value;
                    // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = sPropName });
                    this.SetEntityState(EntityStateType.Modified, _carrierflightno);
                }
            }
        }

        //ScheduleDate
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("ScheduleDate")]
        [Required]
        public DateTime ScheduleDate
        {
            get
            { return _scheduledate; }
            set
            {
                if (_scheduledate != value)
                {
                    this._scheduledate = value;
                    // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = sPropName });
                    this.SetEntityState(EntityStateType.Modified, _scheduledate.ToString());
                }
            }
        }

        //ScheduleDateTimeOfArrival
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("ScheduleDateTimeOfArrival")]
        [Required]
        public DateTime ScheduleDateTimeOfArrival
        {
            get
            { return _scheduledatetimeofarrival; }
            set
            {
                if (_scheduledatetimeofarrival != value)
                {
                    this._scheduledatetimeofarrival = value;
                    // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = sPropName });
                    this.SetEntityState(EntityStateType.Modified, _scheduledatetimeofarrival.ToString());
                }
            }
        }

        //ActualDateTimeOfArrival
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("ActualDateTimeOfArrival")]
        [Required]
        public DateTime ActualDateTimeOfArrival
        {
            get
            { return _actualdatetimeofarrival; }
            set
            {
                if (_actualdatetimeofarrival != value)
                {
                    this._actualdatetimeofarrival = value;
                    // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = sPropName });
                    this.SetEntityState(EntityStateType.Modified, _actualdatetimeofarrival.ToString());
                }
            }
        }

        //FlightStatusCode
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("FlightStatusCode")]
        [Required]
        public string FlightStatusCode
        {
            get
            { return _flightstatuscode; }
            set
            {
                if (_flightstatuscode != value)
                {
                    this._flightstatuscode = value;
                    // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = sPropName });
                    this.SetEntityState(EntityStateType.Modified, _flightstatuscode);
                }
            }
        }

        //FlightStatus
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("FlightStatus")]
        [Required]
        public string FlightStatus
        {
            get
            { return _flightstatus; }
            set
            {
                if (_flightstatus != value)
                {
                    this._flightstatus = value;
                    // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = sPropName });
                    this.SetEntityState(EntityStateType.Modified, _flightstatus);
                }
            }
        }

        //ScheduledGateAssignment
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("ScheduledGateAssignment")]
        [Required]
        public string ScheduledGateAssignment
        {
            get
            { return _scheduledgateassignment; }
            set
            {
                if (_scheduledgateassignment != value)
                {
                    this._scheduledgateassignment = value;
                    // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = sPropName });
                    this.SetEntityState(EntityStateType.Modified, _scheduledgateassignment);
                }
            }
        }

        //ActualGateAssignment
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("ActualGateAssignment")]
        [Required]
        public string ActualGateAssignment
        {
            get
            { return _actualgateassignment; }
            set
            {
                if (_actualgateassignment != value)
                {
                    this._actualgateassignment = value;
                    // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = sPropName });
                    this.SetEntityState(EntityStateType.Modified, _actualgateassignment);
                }
            }
        }

        //ScheduledBagClaimArea
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("ScheduledBagClaimArea")]
        [Required]
        public string ScheduledBagClaimArea
        {
            get
            { return _scheduledbagclaimarea; }
            set
            {
                if (_scheduledbagclaimarea != value)
                {
                    this._scheduledbagclaimarea = value;
                    // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = sPropName });
                    this.SetEntityState(EntityStateType.Modified, _scheduledbagclaimarea);
                }
            }
        }

        //ActualBagClaimArea
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("ActualBagClaimArea")]
        [Required]
        public string ActualBagClaimArea
        {
            get
            { return _actualbagclaimarea; }
            set
            {
                if (_actualbagclaimarea != value)
                {
                    this._actualbagclaimarea = value;
                    // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = sPropName });
                    this.SetEntityState(EntityStateType.Modified, _actualbagclaimarea);
                }
            }
        }

        //BagsInDateTime
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName("BagsInDateTime")]
        [Required]
        public DateTime BagsInDateTime
        {
            get
            { return _bagsindatetime; }
            set
            {
                if (_bagsindatetime != value)
                {
                    this._bagsindatetime = value;
                    // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = sPropName });
                    this.SetEntityState(EntityStateType.Modified, _bagsindatetime.ToString());
                }
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor ()
        /// </summary>
        public AirlineCarrierFlight()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public AirlineCarrierFlight(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            //CarrierID
            //CarrierName
            //CarrierFlightNo
            //ScheduleDate
            //ScheduleDateTimeOfArrival
            //ActualDateTimeOfArrival
            //FlightStatusCode
            //FlightStatus
            //ScheduledGateAssignment
            //ActualGateAssignment
            //ScheduledBagClaimArea
            //ActualBagClaimArea
            //BagsInDateTime

            this._carrierid = dr[cn_fCarrierID].ToString();
            this._carriername = dr[cn_fCarrierName].ToString();
            this._carrierflightno = dr[cn_fCarrierFlightNo].ToString();
            this._scheduledate = Convert.ToDateTime(dr[cn_fScheduleDate].ToString());
            this._scheduledatetimeofarrival = Convert.ToDateTime(dr[cn_fScheduleDateTimeOfArrival].ToString());
            this._actualdatetimeofarrival = Convert.ToDateTime(dr[cn_fActualDateTimeOfArrival].ToString());
            this._flightstatuscode = dr[cn_fFlightStatusCode].ToString();
            this._flightstatus = dr[cn_fFlightStatus].ToString();
            this._scheduledgateassignment = dr[cn_fScheduledGateAssignment].ToString();
            this._actualgateassignment = dr[cn_fActualGateAssignment].ToString();
            this._scheduledbagclaimarea = dr[cn_fScheduledBagClaimArea].ToString();
            this._actualbagclaimarea = dr[cn_fActualBagClaimArea].ToString();
            this._bagsindatetime = Convert.ToDateTime(dr[cn_fBagsInDateTime].ToString());

            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods

        #endregion Methods
    }
}
