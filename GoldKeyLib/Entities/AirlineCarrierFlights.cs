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
    public sealed class AirlineCarrierFlights : BaseEntityList<AirlineCarrierFlight>
    {

        #region Data

        //'Database Field Names
        private const string cn_fCarrierId = "fldc_CarrierID";
        private const string cn_fCarrierName = "fldc_CarrierName";


        //'StoredProcedure Names

        private const string cn_spGetCarrierFlights = "pr_GetCarrierFlights";
        private const string cn_spGetCarrierFlight = "pr_GetCarrierFlight";
        private const string cn_spListAll = "pr_ListCarrierFlights";

        //'Stored Proc Parameters'
        private const string cn_pmFlightDate = "@pFlightDate";
        private const string cn_pmFlightNo = "@pFlightNo";
        private const string cn_pmCarrierID = "@pCarrierID";



        #endregion Data

        #region Private Attributes

        private static AirlineCarrierFlights _instance = null;
        private static readonly object _lock = new object();
        private static DateTime _flightdate = DateTime.Today;
        private static string _carrierid;
        private static string _flightno;

        #endregion Private Attributes

        #region Properties

        #endregion Properties

        #region Constructors

        public AirlineCarrierFlights(DateTime FlightDate)
        {
            _flightdate = FlightDate;
        }
        private AirlineCarrierFlights() { }

        public static AirlineCarrierFlights Listing
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AirlineCarrierFlights(_flightdate);
                        _instance.ListAllCarrierFlights(_flightdate);
                    }
                    return _instance;
                }
            }
        }






        #endregion Constructors

        #region Methods
        //public AirlineCarrierFlight GetCarrierFlight(string CarrierID, DateTime FlightDate, string FlightNo)
        //{
        //    _carrierid = CarrierID;
        //    _flightdate = FlightDate;
        //    _flightno = FlightNo;

        //    DataTable dt = new DataTable();
        //    dt = DABase.Instance.ExecSP(cn_spGetCarrierFlight, DABase.Instance.Parameter(cn_pmCarrierID, _carrierid), DABase.Instance.Parameter(cn_pmFlightDate, _flightdate.ToShortDateString()), DABase.Instance.Parameter(cn_pmFlightNo, _flightno));
        //    AirlineCarrierFlight flight;
        //    flight = new AirlineCarrierFlight(dt.Rows[0]);

        //    return flight;
           
        //}
        public AirlineCarrierFlights GetCarrierFlights(string CarrierID, DateTime FlightDate)
        {
            _carrierid = CarrierID;
            _flightdate = FlightDate;
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spGetCarrierFlights, DABase.Instance.Parameter(cn_pmCarrierID, _carrierid), DABase.Instance.Parameter(cn_pmFlightDate, _flightdate.ToShortDateString()));

            if (_instance == null)
            {
                _instance = new AirlineCarrierFlights();
                //_instance.GetCarrierFlights(_carrierid,_flightdate);
            }
            else
            {
                _instance.ClearItems();
            }
            foreach (DataRow dr in dt.Rows)
            {
                AirlineCarrierFlight flight;
                flight = new AirlineCarrierFlight(dr);
                _instance.Add(flight);
            };
            return _instance;
            {
            }
        }
        public AirlineCarrierFlights ListAllCarrierFlights(DateTime FlightDate)
        {
            _flightdate = FlightDate;
            DataTable dt = new DataTable();
            dt = DABase.Instance.ExecSP(cn_spListAll, DABase.Instance.Parameter(cn_pmFlightDate, _flightdate));

            if (_instance == null)
            {
                _instance = new AirlineCarrierFlights();
                //_instance.GetCarrierFlights(_carrierid,_flightdate);
            }
            else
            {
                _instance.ClearItems();
            }
            foreach (DataRow dr in dt.Rows)
            {
                AirlineCarrierFlight flight;
                flight = new AirlineCarrierFlight(dr);
                _instance.Add(flight);
            };
            return _instance;
            {
            }
        }

        public AirlineCarrierFlight Find(string CarrierId, DateTime FlightDate, string FlightNo )
        {
            _carrierid = CarrierId;
            _flightdate = FlightDate;
            _flightno = FlightNo;

            AirlineCarrierFlight flight = new AirlineCarrierFlight();
            int i = 0;

            if (_instance == null)
            {
                _instance = new AirlineCarrierFlights();
                //_instance.GetCarrierFlights(_carrierid,_flightdate);
                
            }
            else
            {
                _instance.ClearItems();
            }
            _instance.GetCarrierFlights(_carrierid, _flightdate); 
            while (i < _instance.Count)
            {
                if (_instance.Items[i].CarrierFlightNo == _flightno.TrimEnd().TrimStart())
                {
                    flight = _instance.Items[i];
                    break;
                }
                i += 1;
            }



            return flight;
        }
        #endregion Methods
    }
}
