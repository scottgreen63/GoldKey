using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoldKeyLib.Interfaces
{
    public interface IAirlineCarrierFlight
    {
        [DataMember]
        string CarrierID  { get; set; }
        [DataMember]
        string CarrierName { get; set; }
        [DataMember]
        string CarrierFlightNo  { get; set; }
        [DataMember]
        DateTime ScheduleDate { get; set; }
        [DataMember]
        DateTime ScheduleDateTimeOfArrival { get; set; }
        [DataMember]
        DateTime ActualDateTimeOfArrival { get; set; }
        [DataMember]
        string FlightStatusCode { get; set; }
        [DataMember]
        string FlightStatus { get; set; }
        [DataMember]
        string ScheduledGateAssignment { get; set; }
        [DataMember]
        string ActualGateAssignment { get; set; }
        [DataMember]
        string ScheduledBagClaimArea { get; set; }
        [DataMember]
        string ActualBagClaimArea { get; set; }
        [DataMember]
        DateTime BagsInDateTime { get; set; }




    }
}
