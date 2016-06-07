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
    public sealed class VehicleImages : BaseEntityList<VehicleImage>
    {

        #region Data

        //'Database Field Names
        //private const string cn_fCarrierId = "fldc_CarrierID";
        //private const string cn_fCarrierName = "fldc_CarrierName";


        //'StoredProcedure Names

        //private const string cn_spGet = "pr_GetCarrier";
        private const string cn_spList = "pr_ListVehicleImages";

        //'Stored Proc Parameters'
        private const string cn_pmUserId = "@pUserId";
        private const string cn_pmVehicleId = "@pVehicleID";


        #endregion Data

        #region Private Attributes

        private static VehicleImages _instance = null;
        private static readonly object _lock = new object();
        private static int _userid = 0;
        private static string _vehicleid;

        #endregion Private Attributes

        #region Properties
       

        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }
       

        public string VehicleID
        {
            get { return _vehicleid; }
            set { _vehicleid = value; }
        }

        #endregion Properties

        #region Constructors

        public VehicleImages(int UserId, string VehicleId)
        {
            _userid = UserId;
            _vehicleid = VehicleId;
        }
        private VehicleImages() { }

        //public static VehicleImages Listing
        //{
        //    get
        //    {
        //        lock (_lock)
        //        {
        //            if (_instance == null)
        //            {
        //                _instance = new VehicleImages(_userid, _vehicleid);
        //                Load(_userid,_vehicleid);
        //            }
        //            return _instance;
        //        }
        //    }
        //}

        private static VehicleImages Load(int UserId, string VehicleId)
        {
            _userid = UserId;
            _vehicleid = VehicleId;
            
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spList, 
                DABase.Instance.Parameter(cn_pmUserId, _userid),
                DABase.Instance.Parameter(cn_pmVehicleId, _vehicleid));

            _instance.ClearItems();
            foreach (DataRow dr in dt.Rows)
            {
                VehicleImage vehicleimage;
                vehicleimage = new VehicleImage(dr);
                _instance.Add(vehicleimage);
            };
            return _instance;
            {
            }
        }
        #endregion Constructors

        #region Methods

        public VehicleImages List()
        {
            if (_instance == null)
            {
                _instance = new VehicleImages(this.UserId, this.VehicleID);
            }
            Load(_userid, _vehicleid);
            return _instance;
        }

        public VehicleImage Find(string VehicleId)
        {
            _vehicleid = VehicleId;
            VehicleImage vehicleimage = new VehicleImage(_userid, _vehicleid);
            int i = 0;

            if (_instance == null)
            {
                _instance = new VehicleImages();
                Load(_userid, _vehicleid);
            }
            else
            {
                _instance.ClearItems();
            }
            Load(_userid, _vehicleid);
            while (i < _instance.Count)
            {
                if (_instance.Items[i].VehicleID == _vehicleid.TrimEnd().TrimStart())
                {
                    vehicleimage = _instance.Items[i];
                    break;
                }
                i += 1;
            }



            return vehicleimage;
        }

        #endregion Methods
    }
}
