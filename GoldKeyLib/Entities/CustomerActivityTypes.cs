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
    public sealed class CustomerActivityTypes : BaseEntityList<CustomerActivityType>
    {

        #region Data

        //'Database Field Names
        //private const string cn_fCustomerActivityTypeId = "fldc_CustomerActivityTypeID";
        //private const string cn_fCustomerActivityTypeName = "fldc_CustomerActivityTypeName";


        //'StoredProcedure Names

        //private const string cn_spGet = "pr_GetCustomerActivityType";
        private const string cn_spList = "pr_ListCustomerActivityTypes";

        //'Stored Proc Parameters'
        private const string cn_pmUserId = "@pUserId";



        #endregion Data

        #region Private Attributes

        private static CustomerActivityTypes _instance = null;
        private static readonly object _lock = new object();
        private static int _userid = 0;

        #endregion Private Attributes

        #region Properties

        #endregion Properties

        #region Constructors

        public CustomerActivityTypes(int UserId)
        {
            _userid = UserId;
        }
        private CustomerActivityTypes() { }

        public static CustomerActivityTypes Listing
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CustomerActivityTypes(_userid);
                        Load(_userid);
                    }
                    return _instance;
                }
            }
        }

        private static CustomerActivityTypes Load(int UserId)
        {
            _userid = UserId;
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spList, DABase.Instance.Parameter(cn_pmUserId, UserId));

            _instance.ClearItems();
            foreach (DataRow dr in dt.Rows)
            {
                CustomerActivityType customer;
                customer = new CustomerActivityType(dr);
                _instance.Add(customer);
            };
            return _instance;
            {
            }
        }
        #endregion Constructors

        #region Methods

        public CustomerActivityTypes List(int UserId)
        {
            _userid = UserId;
            if (_instance == null)
            {
                _instance = new CustomerActivityTypes(_userid);
            }
            Load(_userid);
            return _instance;
        }


        public CustomerActivityType Find(string CustomerActivityTypeID)
        {

            CustomerActivityType customer = new CustomerActivityType(_userid);
            int i = 0;

            if (_instance == null)
            {
                _instance = new CustomerActivityTypes();
                Load(_userid);
            }
            else
            {
                _instance.ClearItems();
            }
            Load(_userid);
            while (i < _instance.Count)
            {
                if (_instance.Items[i].CustomerActivityTypeID == CustomerActivityTypeID.TrimEnd().TrimStart())
                {
                    customer = _instance.Items[i];
                    break;
                }
                i += 1;
            }



            return customer;
        }
        #endregion Methods
    }
}
