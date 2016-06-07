using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace GoldKeyLib.Interfaces
{
    public interface IVehicleActivity
    {
        [DataMember]
        string CustomerID { get; set; }
        [DataMember]
        string VehicleID { get; set; }
        [DataMember]
        DateTime ActivityDateTime { get; set; }
        [DataMember]
        string VehicleActivityTypeID { get; set; }
    }
}
