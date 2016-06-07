using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using GoldKeyLib.Interfaces;
using GoldKeyLib.Entities;
using GoldKeyLib.DA;

namespace GoldKeyLib.Entities
{
    public class CustomerCarrierFlight : BaseEntity , ICustomerCarrierFlight

    {
        //[DataMember]
        //string CustomerID { get; set; }
        //[DataMember]
        //string CarrierID { get; set; }
        //[DataMember]
        //string CarrierFlightNo { get; set; }
        //[DataMember]
        //DateTime ScheduleDate { get; set; }

        #region Data

        //tbld_Services
        //[fldi_Service_ID]           INT NOT NULL,
        //[fldc_ServiceName]          NCHAR(50)   NULL,
        //[fldc_ServiceProvider]      NCHAR(50)   NULL,
        //[fldd_ServiceFee]           DECIMAL(8,2)NULL,

        //'Database Field Names
        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCustomerID = "fldc_Customer_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCarrierID = "fldc_Carrier_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCarrierFlightNo = "fldc_CarrierFlightNo";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fScheduleDate = "fldd_ScheduleDate";

       
        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spAdd = "pr_AddCustomerCarrierFlight";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        //private const string cn_spModify = "pr_Modify__";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spDelete = "pr_DeleteCustomerCarrierFlight";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spGet = "pr_Getpr_GetCustomerCarrierFlight";

       
        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmUserID = "@pUserId";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmNewCustomerID = "@pNewCustomerID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCustomerID = "@pCustomerID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCarrierID = "@pCarrierID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCarrierFlightNo = "@pCarrierFlightNo";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmScheduleDate = "@pScheduleDate";

        

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
        private string _carrierid;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _carrierflightno;
        /// <summary>
        /// private backing field
        /// </summary>
        private DateTime _scheduledate;
        

        //'Property Names'
        /// <summary>
        /// Const used to identify Entity Property Name
        /// </summary>
        private const string cn_pnCustomerID = "CustomerID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCarrierID = "CarrierID";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCarrierFlightNo = "CarrierFlightNo";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnScheduleDate = "ScheduleDate";


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

        //CarrierID
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnCarrierID)]
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

        //CarrierFlightNo
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnCarrierFlightNo)]
        [Required()]
        public string CarrierFlightNo
        {
            get
            { return _carrierflightno; }
            set
            {
                if (_carrierflightno != value)
                {
                    this._carrierflightno = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnCarrierFlightNo);
                }
            }
        }

        //ScheduleDate
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnScheduleDate)]
        [Required()]
        public DateTime ScheduleDate
        {
            get
            { return _scheduledate; }
            set
            {
                if (_scheduledate != value)
                {
                    this._scheduledate = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnScheduleDate);
                }
            }
        }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Constructor ()
        /// </summary>
        public CustomerCarrierFlight()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }
        /// <summary>
        /// Constructor ()
        /// </summary>
        public CustomerCarrierFlight(int UserId)
        {
            //this.Validator = new Validation();
            _userid = UserId;
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public CustomerCarrierFlight(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._customerid = dr[cn_fCustomerID].ToString();
            this._carrierid = dr[cn_fCarrierID].ToString();
            this._carrierflightno = dr[cn_fCarrierFlightNo].ToString();
            this._scheduledate = Convert.ToDateTime(dr[cn_fScheduleDate].ToString());
           

            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
        public void AddCustomerCarrierFlight(int UserId)
        {
            _userid = UserId;

            if ((_customerid == "0") && (this.Validator.Count == 0))
            {
                DABase.Instance.ExecSP(cn_spAdd,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                // DABase.Instance.Parameter(cn_pmCustomerCode, _customercode),
                DABase.Instance.Parameter(cn_pmCarrierID, _carrierid),
                DABase.Instance.Parameter(cn_pmCarrierFlightNo, _carrierflightno),
                DABase.Instance.Parameter(cn_pmScheduleDate, _scheduledate),
                DABase.Instance.Parameter(cn_pmNewCustomerID, _customerid));

                this.SetEntityState(EntityStateType.Added);
                Customers.Listing.List(_userid);
            }
            else
            {
                throw new System.Exception(Validator.ToString());
                ///MessageBox.Show(ValidationInstance.ToString());
            }



        }

        //public void DeletedCustomerCarrierFlight(int UserId)
        //{
        //    if (_customerid != "0")
        //    {
        //        DABase.Instance.ExecSP(cn_spDelete,
        //            DABase.Instance.Parameter(cn_pmUserID, _userid),
        //            DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
        //            DABase.Instance.Parameter(cn_pmCarrierID, _carrierid),
        //            DABase.Instance.Parameter(cn_pmCarrierFlightNo, _carrierflightno),
        //            DABase.Instance.Parameter(cn_pmScheduleDate, _scheduledate)
        //            );
        //        this.SetEntityState(EntityStateType.Deleted);
        //        CustomerCarrierFlights.Listing.List(_userid);
        //    }
        //}

        public AirlineCarrierFlight GetCustomerCarrierFlight(int UserID, string CustomerID, string CarrierID, string CarrierFlightNo, DateTime ScheduleDate)
        {
            _userid = UserID;
            _customerid = CustomerID;
            _carrierid = CarrierID;
            _carrierflightno = CarrierFlightNo;
            _scheduledate = ScheduleDate;

            DataTable dt;

            dt = DABase.Instance.ExecSP(cn_spGet,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                DABase.Instance.Parameter(cn_pmCarrierID, _carrierid),
                DABase.Instance.Parameter(cn_pmCarrierFlightNo, _carrierflightno),
                DABase.Instance.Parameter(cn_pmScheduleDate, _scheduledate)
                );

            AirlineCarrierFlight carrierflight = new AirlineCarrierFlight();

            foreach (DataRow dr in dt.Rows)
            {
                carrierflight = new AirlineCarrierFlight(dr);
            }
            return carrierflight;
        }

     

        #endregion Methods


    }
}
