using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace GoldKeyLib.Interfaces
{
    [ServiceContract]
    public interface IVehicle
    {
        [DataMember]
        string VehicleID { get; set; }
        [DataMember]
        string CustomerID { get; set; }
        [DataMember]
        string Year { get; set; }
        [DataMember]
        string Make { get; set; }
        [DataMember]
        string Model { get; set; }
        [DataMember]
        string Color { get; set; }
        [DataMember]
        string Trim { get; set; }
        [DataMember]
        string Condition { get; set; }
        [DataMember]
        int Mileage { get; set; }
        [DataMember]
        string VINumber { get; set; }
        [DataMember]
        string Note { get; set;}

    }
}
