using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoldKeyLib.Interfaces
{
    interface IVehicleActivityType
    {
        [DataMember]
        string VehicleActivityTypeID { get; set; }
        [DataMember]
        string VehicleActivityTypeName { get; set; }

    }
}
