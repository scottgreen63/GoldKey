using GoldKeyLib.DA;
using GoldKeyLib.Entities;
using GoldKeyLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public sealed class Vehicles : BaseEntityList<Vehicle>
    {

        #region Data

        //'Database Field Names
        //private const string cn_fCarrierId = "fldc_CarrierID";
        //private const string cn_fCarrierName = "fldc_CarrierName";


        //'StoredProcedure Names

        //private const string cn_spGet = "pr_GetCarrier";
        private const string cn_spList = "pr_ListVehicles";

        private const string cn_spListCustomerVehicles = "pr_ListCustomerVehicles";

        //'Stored Proc Parameters'
        private const string cn_pmUserId = "@pUserId";

        private const string cn_pmCustomerId = "@pCustomerId";



        #endregion Data

        #region Private Attributes

        private static Vehicles _instance = null;
        private static readonly object _lock = new object();
        private static int _userid = 0;
        private static string _customerid = string.Empty;

        #endregion Private Attributes

        #region Properties

        #endregion Properties

        #region Constructors

        private Vehicles(int UserId)
        {
            _userid = UserId;
        }
        private Vehicles() { }

        //public static Vehicles Listing
        //{
        //    get
        //    {
        //        lock (_lock)
        //        {
        //            if (_instance == null)
        //            {
        //                _instance = new Vehicles(_userid);
        //                Load(_userid);
        //            }
        //            return _instance;
        //        }
        //    }
        //}

        //private static Vehicles Load(int UserId)
        //{
        //    _userid = UserId;
        //    DataTable dt = new DataTable();
        //    dt = DABase.Instance.ExecSP(cn_spList, DABase.Instance.Parameter(cn_pmUserId, UserId));

        //    _instance.ClearItems();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        Vehicle vehicle;
        //        vehicle = new Vehicle(dr);
        //        _instance.Add(vehicle);
        //    };
        //    return _instance;
        //    {
        //    }
        //}
        #endregion Constructors

        #region Methods

        public static Vehicles ListVehicles(int UserId)
        {
            _userid = UserId;
            if (_instance == null)
            {
                _instance = new Vehicles(_userid);
            }
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spList, DABase.Instance.Parameter(cn_pmUserId, UserId));

            _instance.ClearItems();
            foreach (DataRow dr in dt.Rows)
            {
                Vehicle vehicle;
                vehicle = new Vehicle(dr);
                _instance.Add(vehicle);
            };
            return _instance;
        }
        public static Vehicles ListCustomerVehicles(int UserId, string customerid)
        {
            _userid = UserId;
            _customerid = customerid;
            Vehicles vehicles;


            //if (_instance == null)
            //{
            //    _instance = new Vehicles(_userid);
            //}
            vehicles = new Vehicles();
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spListCustomerVehicles, 
                DABase.Instance.Parameter(cn_pmUserId, UserId),
                DABase.Instance.Parameter(cn_pmCustomerId,_customerid));

            //_instance.ClearItems();
            foreach (DataRow dr in dt.Rows)
            {
                Vehicle vehicle;
                vehicle = new Vehicle(dr);
                //_instance.Add(vehicle);


                vehicles.Add(vehicle);
            };
            return vehicles; //_instance;
        }
        //public Vehicle Find(string VehicleId)
        //{

        //    Vehicle vehicle = new Vehicle(_userid);
        //    int i = 0;

        //    if (_instance == null)
        //    {
        //        _instance = new Vehicles();
        //        Load(_userid);
        //    }
        //    else
        //    {
        //        _instance.ClearItems();
        //    }
        //    Load(_userid);
        //    while (i < _instance.Count)
        //    {
        //        if (_instance.Items[i].VehicleID == VehicleId.TrimEnd().TrimStart())
        //        {
        //            vehicle = _instance.Items[i];
        //            break;
        //        }
        //        i += 1;
        //    }

        //    return vehicle;
        //}

       

        #endregion Methods
    }
}
