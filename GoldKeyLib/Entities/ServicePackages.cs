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
    public sealed class ServicePackages : BaseEntityList<ServicePackage>
    {

        #region Data

        //'Database Field Names
        //private const string cn_fCarrierId = "fldc_CarrierID";
        //private const string cn_fCarrierName = "fldc_CarrierName";


        //'StoredProcedure Names

        //private const string cn_spGet = "pr_GetCarrier";
        private const string cn_spList = "pr_ListServicePackages";

        //'Stored Proc Parameters'
        private const string cn_pmUserId = "@pUserId";



        #endregion Data

        #region Private Attributes

        private static ServicePackages _instance = null;
        private static readonly object _lock = new object();
        private static int _userid = 0;

        #endregion Private Attributes

        #region Properties

        #endregion Properties

        #region Constructors

        public ServicePackages(int UserId)
        {
            _userid = UserId;
        }
        private ServicePackages() { }

        public static ServicePackages Listing
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ServicePackages(_userid);
                        Load(_userid);
                    }
                    return _instance;
                }
            }
        }

        private static ServicePackages Load(int UserId)
        {
            _userid = UserId;
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spList, DABase.Instance.Parameter(cn_pmUserId, UserId));

            _instance.ClearItems();
            foreach (DataRow dr in dt.Rows)
            {
                ServicePackage servicepackage;
                servicepackage = new ServicePackage(dr);
                _instance.Add(servicepackage);
            };
            return _instance;
            {
            }
        }
        #endregion Constructors

        #region Methods

        public ServicePackages List(int UserId)
        {
            _userid = UserId;
            if (_instance == null)
            {
                _instance = new ServicePackages(_userid);
            }
            Load(_userid);
            return _instance;
        }


        public ServicePackage Find(string ServicePackageID)
        {

            ServicePackage servicepackage = new ServicePackage(_userid);
            int i = 0;

            if (_instance == null)
            {
                _instance = new ServicePackages();
                Load(_userid);
            }
            else
            {
                _instance.ClearItems();
            }
            Load(_userid);
            while (i < _instance.Count)
            {
                if (_instance.Items[i].ServicePackageID == ServicePackageID.TrimEnd().TrimStart())
                {
                    servicepackage = _instance.Items[i];
                    break;
                }
                i += 1;
            }



            return servicepackage;
        }
        #endregion Methods
    }
}
