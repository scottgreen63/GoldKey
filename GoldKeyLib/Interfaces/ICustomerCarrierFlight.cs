using System;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace GoldKeyLib.Interfaces
{
    public interface ICustomerCarrierFlight
    {
        [DataMember]
        string CustomerID { get; set; }
        [DataMember]
        string CarrierID { get; set; }
        [DataMember]
        string CarrierFlightNo { get; set; }
        [DataMember]
        DateTime ScheduleDate { get; set; }

    }
}
