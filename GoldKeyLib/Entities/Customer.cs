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
    public partial class Customer : BaseEntity, ICustomer
    {

        #region Data

        //tbld_Customers
        //[fldi_Customer_ID]   INT NOT NULL,
        //[fldc_Customer_ID] AS([dbo].[udf_ZeroPaddingAndConcat]('GK-', CONVERT([nvarchar](10),[fldi_Customer_ID]),(6),'0')),
        //[fldc_LastName]      NCHAR(50)   NULL,
        //[fldc_FirstName]     NCHAR(50)   NULL,
        //[fldc_Address1]      NCHAR(200)  NULL,
        //[fldc_Address2]      NCHAR(200)  NULL,
        //[fldc_City]          NCHAR(200)  NULL,
        //[fldc_State]         NCHAR(2)    NULL,
        //[fldc_ZipCode]       NVARCHAR(5) NULL,
        //[fldc_ContactPhone]  NCHAR(12)   NULL,
        //[fldc_ContactEmail]  NCHAR(100)  NULL,
        //[fldc_CompanyName]   NCHAR(100)  NULL





        //'Database Field Names
        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCustomerID = "fldc_Customer_ID";

        ///// <summary>
        ///// Const used to identify DA Field Name
        ///// </summary>
        //private const string cn_fCustomerCode = "fldc_Customer_Code";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fLastName = "fldc_LastName";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fFirstName = "fldc_FirstName";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fAddress1 = "fldc_Address1";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fAddress2 = "fldc_Address2";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCity = "fldc_City";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fState = "fldc_State";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fZipCode = "fldc_ZipCode";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fContactPhone = "fldc_ContactPhone";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fContactEmail = "fldc_ContactEmail";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fCompanyName = "fldc_CompanyName";

        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spAdd = "pr_AddCustomer";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spModify = "pr_ModifyCustomer";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spDelete = "pr_DeleteCustomer";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spGet = "pr_GetCustomer";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spGetCustomerVehicle = "pr_GetCustomerVehicle";  

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

        ///// <summary>
        ///// private var used for DA Stored Proc variable
        ///// </summary>
        //private const string cn_pmCustomerCode = "@pCustomerCode";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmFirstName = "@pFirstName";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmLastName = "@pLastName";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmAddress1 = "@pAddress1";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmAddress2 = "@pAddress2";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCity = "@pCity";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmState = "@pState";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmZipCode = "@pZipCode";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmContactPhone = "@pContactPhone";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmContactEmail = "@pContactEmail";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmCompanyName = "@pCompanyName";

        
        #endregion Data

        #region Private Attributes
        // private backing fields
        /// <summary>
        private int _userid = 0;
        /// private backing field
        /// </summary>
        private string _customerid;
        ///// <summary>
        ///// private backing field
        ///// </summary>
        //private string _customercode;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _firstname;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _lastname;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _address1;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _address2;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _city;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _state;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _zipcode;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _contactphone;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _contactemail;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _companyname;

        /// <summary>
        /// private backing field
        /// </summary>
        private Vehicles _customervehicles ;



        //'Property Names'
        /// <summary>
        /// Const used to identify Entity Property Name
        /// </summary>
        private const string cn_pnCustomerID = "CustomerID";
        ///// <summary>
        ///// Const used to identify BO Property Name
        ///// </summary>
        //private const string cn_pnCustomerCode = "CustomerCode";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnFirstName = "FirstName";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnLastName = "LastName";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnAddress1 = "Address1";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnAddress2 = "Address2";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCity = "City";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnState = "State";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnZipCode = "ZipCode";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnContactPhone = "ContactPhone";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnContactEmail = "ContactEmail";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCompanyName = "CompanyName";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnCustomerVehicles = "CustomerVehicles";



        #endregion Private Attributes

        #region Properties

        //CustomerId
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

        ////CustomerId
        ///// <summary>
        ///// BO Property
        ///// </summary>
        //[DisplayName(cn_pnCustomerCode)]
        //[Required()]
        //public string CustomerCode
        //{
        //    get
        //    { return _customercode; }
        //    set
        //    {
        //        if (_customercode != value)
        //        {
        //            this._customercode = value;
        //            this.SetEntityState(EntityStateType.Modified, cn_pnCustomerCode);
        //        }
        //    }
        //}

        //FirstName
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnFirstName)]
        [Required()]
        public string FirstName
        {
            get
            { return _firstname; }
            set
            {
                if (_firstname != value)
                {
                    this._firstname = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnFirstName);
                }
            }
        }

        //LastName
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnLastName)]
        [Required()]
        public string LastName
        {
            get
            { return _lastname; }
            set
            {
                if (_lastname != value)
                {
                    this._lastname = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnLastName);
                }
            }
        }

        //Address1
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnAddress1)]
        [Required()]
        public string Address1
        {
            get
            { return _address1; }
            set
            {
                if (_address1 != value)
                {
                    this._address1 = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnAddress1);
                }
            }
        }

        //Address2
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnAddress2)]
        [Required()]
        public string Address2
        {
            get
            { return _address2; }
            set
            {
                if (_address2 != value)
                {
                    this._address2 = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnAddress2);
                }
            }
        }

        //City
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnCity)]
        [Required()]
        public string City
        {
            get
            { return _city; }
            set
            {
                if (_city != value)
                {
                    this._city = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnCity);
                }
            }
        }

        //State
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnState)]
        [Required()]
        public string State
        {
            get
            { return _state; }
            set
            {
                if (_state != value)
                {
                    this._state = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnState);
                }
            }
        }

        //ZipCode
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnZipCode)]
        [Required()]
        public string ZipCode
        {
            get
            { return _zipcode; }
            set
            {
                if (_zipcode != value)
                {
                    this._zipcode = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnZipCode);
                }
            }
        }

        //ContactPhone
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnContactPhone)]
        [Required()]
        public string ContactPhone
        {
            get
            { return _contactphone; }
            set
            {
                if (_contactphone != value)
                {
                    this._contactphone = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnContactPhone);
                }
            }
        }

        //ContactEmail
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnContactEmail)]
        [Required()]
        public string ContactEmail
        {
            get
            { return _contactemail; }
            set
            {
                if (_contactemail != value)
                {
                    this._contactemail = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnContactEmail);
                }
            }
        }

        //CompanyName
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnCompanyName)]
        [Required()]
        public string CompanyName
        {
            get
            { return _companyname; }
            set
            {
                if (_companyname != value)
                {
                    this._companyname = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnCompanyName);
                }
            }
        }


        //CustomerVehicles
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnCustomerVehicles)]
        [Required()]
        public Vehicles CustomerVehicles
        {
            get
            { return _customervehicles; }
            set
            {
                if (_customervehicles != value)
                {
                    this._customervehicles = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnCustomerVehicles);
                }
            }
        }
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor ()
        /// </summary>
        public Customer()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor ()
        /// </summary>
        public Customer(int UserId)
        {
            //this.Validator = new Validation();
            _userid = UserId;
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public Customer(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._customerid = dr[cn_fCustomerID].ToString();
            //this._customercode = dr[cn_fCustomerCode].ToString();
            this._firstname  = dr[cn_fFirstName].ToString();
            this._lastname = dr[cn_fLastName].ToString();
            this._address1 = dr[cn_fAddress1].ToString();
            this._address2 = dr[cn_fAddress2].ToString();
            this._city = dr[cn_fCity].ToString();
            this._state = dr[cn_fState].ToString();
            this._zipcode = dr[cn_fZipCode].ToString();
            this._contactphone = dr[cn_fContactPhone].ToString();
            this._contactemail = dr[cn_fContactEmail].ToString();
            this._companyname = dr[cn_fCompanyName].ToString();

            this._customervehicles = Vehicles.ListCustomerVehicles(0, _customerid);
            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
        public void AddCustomer(int UserId)
        {
            _userid = UserId;

            if ((_customerid == "0") && (this.Validator.Count == 0))
            {
                DABase.Instance.ExecSP(cn_spAdd,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
               // DABase.Instance.Parameter(cn_pmCustomerCode, _customercode),
                DABase.Instance.Parameter(cn_pmLastName, _lastname),
                DABase.Instance.Parameter(cn_pmFirstName, _firstname),
                DABase.Instance.Parameter(cn_pmAddress1, _address1),
                DABase.Instance.Parameter(cn_pmAddress2, _address2),
                DABase.Instance.Parameter(cn_pmCity, _city),
                DABase.Instance.Parameter(cn_pmState, _state),
                DABase.Instance.Parameter(cn_pmZipCode, _zipcode),
                DABase.Instance.Parameter(cn_pmContactPhone, _contactphone),
                DABase.Instance.Parameter(cn_pmContactEmail,_contactemail),
                DABase.Instance.Parameter(cn_pmCompanyName, _companyname),
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

        public void ModifyCustomer(int UserId)
        {
            _userid = UserId;

            if ((_customerid != "0") && (Validator.Count == 0))
            {
                try
                {
                    DABase.Instance.ExecSP(cn_spModify,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
                    // DABase.Instance.Parameter(cn_pmCustomerCode, _customercode),
                    DABase.Instance.Parameter(cn_pmLastName, _lastname),
                    DABase.Instance.Parameter(cn_pmFirstName, _firstname),
                    DABase.Instance.Parameter(cn_pmAddress1, _address1),
                    DABase.Instance.Parameter(cn_pmAddress2, _address2),
                    DABase.Instance.Parameter(cn_pmCity, _city),
                    DABase.Instance.Parameter(cn_pmState, _state),
                    DABase.Instance.Parameter(cn_pmZipCode, _zipcode),
                    DABase.Instance.Parameter(cn_pmContactPhone, _contactphone),
                    DABase.Instance.Parameter(cn_pmContactEmail, _contactemail),
                    DABase.Instance.Parameter(cn_pmCompanyName, _companyname),
                    DABase.Instance.Parameter(cn_pmNewCustomerID, _customerid));

                    this.SetEntityState(EntityStateType.Modified);
                    Customers.Listing.List(0);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            else
                throw new Exception(Validator.ToString());


        }

        public void DeleteCustomer(int UserId)
        {
            if (_customerid != "0")
            {
                DABase.Instance.ExecSP(cn_spDelete,
                    DABase.Instance.Parameter(cn_pmUserID, _userid),
                    DABase.Instance.Parameter(cn_pmCustomerID, _customerid));
                this.SetEntityState(EntityStateType.Deleted);
                Customers.Listing.List(_userid);
            }
        }

        public Customer GetCustomer(int UserID, string CustomerID)
        {
            _userid = UserID;
            _customerid = CustomerID;
            DataTable dt;

            dt = DABase.Instance.ExecSP(cn_spGet,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmCustomerID, _customerid));

            Customer customer = new Customer(_userid);

            foreach (DataRow dr in dt.Rows)
            {
                customer = new Customer(dr);
            }
            return customer;
        }

        #endregion Methods
    }

    
}
