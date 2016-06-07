using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoldKeyLib.Interfaces
{
    public interface IGKLocation
    {
        [DataMember]
        string LocationID { get; set; }
        [DataMember]
        string LocationTypeID { get; set; }
        [DataMember]
        string LocationName { get; set; }
    }
}
