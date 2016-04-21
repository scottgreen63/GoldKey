using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldKey.Models
{
    /// <summary>
    /// Valet Customer 
    /// </summary>
    public class Customer
    {

        //[fldi_Customer_ID] INT NOT NULL,
        //[fldc_Customer_Code] AS([dbo].[udf_ZeroPaddingAndConcat]('GK-', CONVERT([nvarchar](10),[fldi_Customer_ID]),(6),'0')),
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

        private int customerid;
        private string lastname;
        private string firstname;
        private string address1;
        private string address2;
        private string city;
        private string state;
        private string zipcode;
        private string contactphone;
        private string contactemail;
        private string companyname;

        public int CustomerID
        {
            get { return customerid; }
            set { customerid = value; }
        }   
        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public string Address1
        {
            get { return address1; }
            set { address1 = value; }
        }

        public string Address2
        {
            get { return address2; }
            set { address2 = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string ZipCode
        {
            get { return zipcode; }
            set { zipcode = value; }
        }

        public string ContactPhone
        {
            get { return contactphone; }
            set { contactphone = value; }
        }

        public string ContactEmail
        {
            get { return contactemail; }
            set { contactemail = value; }
        }

        public string CompanyName
        {
            get { return companyname; }
            set { companyname = value; }
        }


    }
}