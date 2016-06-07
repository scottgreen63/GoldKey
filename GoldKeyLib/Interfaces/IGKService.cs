using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoldKeyLib.Interfaces
{
    public interface IGKService
    {
        [DataMember]
        string ServiceID { get; set; }
        [DataMember]
        string ServiceName { get; set; }
        [DataMember]
        string ServiceProvider { get; set; }
        [DataMember]
        decimal ServiceFee { get; set; }
    }
}
