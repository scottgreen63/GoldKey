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
    public sealed class AirlineCarriers : BaseEntityList<AirlineCarrier>
    {

        #region Data

        //'Database Field Names
        private const string cn_fCarrierId = "fldc_CarrierID";
        private const string cn_fCarrierName = "fldc_CarrierName";
        

        //'StoredProcedure Names

        private const string cn_spGet = "pr_GetCarrier";
        private const string cn_spList = "pr_ListCarriers";

        //'Stored Proc Parameters'
        private const string cn_pmUserId = "@pUserId";
       


        #endregion Data

        #region Private Attributes

        private static AirlineCarriers _instance = null;
        private static readonly object _lock = new object();
        private static int _userid = 0;

        #endregion Private Attributes

        #region Properties

        #endregion Properties

        #region Constructors

        public AirlineCarriers(int UserId)
        {
            _userid = UserId;
        }
        private AirlineCarriers() { }

        public static AirlineCarriers Listing
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AirlineCarriers(_userid);
                        Load(_userid);
                    }
                    return _instance;
                }
            }
        }

        

        private static AirlineCarriers Load(int UserId)
        {
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spList, DABase.Instance.Parameter(cn_pmUserId, UserId));

            _instance.ClearItems();
            foreach (DataRow dr in dt.Rows)
            {
                AirlineCarrier carrier;
                carrier = new AirlineCarrier(dr);
                _instance.Add(carrier);
            };
            return _instance;
            {
            }
        }
        #endregion Constructors

        #region Methods

        public AirlineCarriers List(int UserId)
        {
            if (_instance == null)
            {
                _instance = new AirlineCarriers(UserId);
            }
            Load(UserId);
            return _instance;
        }

       
        public AirlineCarrier Find(string CarrierId)
        {

            AirlineCarrier claim = new AirlineCarrier();
            int i = 0;

            if (_instance == null)
            {
                _instance = new AirlineCarriers();
                Load(_userid);
            }
            else
            {
                _instance.ClearItems();
            }
            Load(_userid);
            while (i < _instance.Count)
            {
                if (_instance.Items[i].CarrierID == CarrierId.TrimEnd().TrimStart())
                {
                    claim = _instance.Items[i];
                    break;
                }
                i += 1;
            }



            return claim;
        }
        #endregion Methods
    }
}
