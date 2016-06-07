using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace GoldKeyLib.Interfaces
{
    public interface ICustomerActivity
    {
        [DataMember]
        string CustomerID { get; set; }
        [DataMember]
        DateTime ActivityDateTime { get; set; }
        [DataMember]
        string CustomerActivityTypeID { get; set; }
    }
}
