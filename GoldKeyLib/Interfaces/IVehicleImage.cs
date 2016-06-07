using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace GoldKeyLib.Interfaces
{
    [ServiceContract]
    public interface IVehicleImage
    {
        [DataMember]
        string VehicleID { get; set; }
        [DataMember]
        DateTime DateTimeStamp { get; set; }
        [DataMember]
        string ImagePath { get; set; }
        [DataMember]
        string ImageCaption { get; set; }
        
    }
}
