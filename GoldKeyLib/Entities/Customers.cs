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
    public sealed class Customers : BaseEntityList<Customer>
    {
        
        #region Data

        //'Database Field Names
        //private const string cn_fCarrierId = "fldc_CarrierID";
        //private const string cn_fCarrierName = "fldc_CarrierName";


        //'StoredProcedure Names

        //private const string cn_spGet = "pr_GetCarrier";
        private const string cn_spList = "pr_ListCustomers";

        //'Stored Proc Parameters'
        private const string cn_pmUserId = "@pUserId";



        #endregion Data

        #region Private Attributes

        private static Customers _instance = null;
        private static readonly object _lock = new object();
        private static int _userid = 0;

        #endregion Private Attributes

        #region Properties

        #endregion Properties

        #region Constructors

        public Customers(int UserId)
        {
            _userid = UserId;
        }
        private Customers() { }

        public static Customers Listing
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Customers(_userid);
                        Load(_userid);
                    }
                    return _instance;
                }
            }
        }

        private static Customers Load(int UserId)
        {
            _userid = UserId;
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spList, DABase.Instance.Parameter(cn_pmUserId, UserId));

            _instance.ClearItems();
            foreach (DataRow dr in dt.Rows)
            {
                Customer customer;
                customer = new Customer(dr);
                _instance.Add(customer);
            };
            return _instance;
            {
            }
        }
        #endregion Constructors

        #region Methods

        public Customers List(int UserId)
        {
            _userid = UserId;
            if (_instance == null)
            {
                _instance = new Customers(_userid);
            }
            Load(_userid);
            return _instance;
        }


        //public Customer Find(string CustomerID)
        //{

        //    Customer customer = new Customer(_userid);
        //    int i = 0;

        //    if (_instance == null)
        //    {
        //        _instance = new Customers();
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
        public Customer Find(string CustomerID)
        {

            Customer customer = new Customer(_userid);
           // int i = 0;

            if (_instance == null)
            {
                _instance = new Customers();
                Load(_userid);
            }
            else
            {
                _instance.ClearItems();
            }
            Load(_userid);
            customer = _instance.FirstOrDefault(p => p.CustomerID == CustomerID);
            //while (i < _instance.Count)
            //{
            //    if (_instance.Items[i].CustomerID == CustomerID.TrimEnd().TrimStart())
            //    {
            //        customer = _instance.Items[i];
            //        break;
            //    }
            //    i += 1;
            //}



            return customer;
        }
        #endregion Methods
    }
}
