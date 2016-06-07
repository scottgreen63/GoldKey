using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoldKeyLib.Interfaces
{
    interface ICustomerActivityType
    {
        [DataMember]
        string CustomerActivityTypeID { get; set; }
        [DataMember]
        string CustomerActivityTypeName { get; set; }

    }
}
