using GoldKeyLib.DA;
using GoldKeyLib.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public sealed class CustomerServicePackages : BaseEntityList<CustomerServicePackage>
    {

        #region Data

        //'Database Field Names
        //private const string cn_fCarrierId = "fldc_CarrierID";
        //private const string cn_fCarrierName = "fldc_CarrierName";


        //'StoredProcedure Names

        //private const string cn_spGet = "pr_GetCarrier";
        private const string cn_spList = "pr_ListCustomerServicePackages";

        //'Stored Proc Parameters'
        private const string cn_pmUserId = "@pUserId";
        private const string cn_pmCustomerId = "@pCustomerId";



        #endregion Data

        #region Private Attributes

        private static CustomerServicePackages _instance = null;
        private static readonly object _lock = new object();
        private static int _userid = 0;
        private static string _customerid = String.Empty;

        #endregion Private Attributes

        #region Properties

        #endregion Properties

        #region Constructors

        public CustomerServicePackages(int UserId, string CustomerId)
        {
            _userid = UserId;
            _customerid = CustomerId;

        }
        private CustomerServicePackages() { }

        public static CustomerServicePackages Listing
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CustomerServicePackages(_userid, _customerid);
                        Load(_userid, _customerid);
                    }
                    return _instance;
                }
            }
        }

        private static CustomerServicePackages Load(int UserId, string CustomerId)
        {
            _userid = UserId;
            _customerid = CustomerId;
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spList, 
                DABase.Instance.Parameter(cn_pmUserId, _userid),
                DABase.Instance.Parameter(cn_pmCustomerId, _customerid));

            _instance.ClearItems();
            foreach (DataRow dr in dt.Rows)
            {
                CustomerServicePackage customer;
                customer = new CustomerServicePackage(dr);
                _instance.Add(customer);
            };
            return _instance;
            {
            }
        }
        #endregion Constructors

        #region Methods

        public CustomerServicePackages List(int UserId, string CustomerId)
        {
            _userid = UserId;
            _customerid = CustomerId;

            if (_instance == null)
            {
                _instance = new CustomerServicePackages(_userid, _customerid);
            }
            Load(_userid, _customerid);
            return _instance;
        }


        //public CustomerServicePackage Find(string CustomerId)
        //{
        //    _customerid = CustomerId;
        //    CustomerServicePackage customer = new CustomerServicePackage(_userid);
        //    int i = 0;

        //    if (_instance == null)
        //    {
        //        _instance = new CustomerServicePackages();
        //        Load(_userid);
        //    }
        //    else
        //    {
        //        _instance.ClearItems();
        //    }
        //    Load(_userid);
        //    while (i < _instance.Count)
        //    {
        //        if (_instance.Items[i].CustomerID == CustomerID.TrimEnd().TrimStart())
        //        {
        //            customer = _instance.Items[i];
        //            break;
        //        }
        //        i += 1;
        //    }



        //    return customer;
        //}
        #endregion Methods
    }
}
