using System;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace GoldKeyLib.Interfaces
{
    public interface IAirlineCarrier
    {
        [DataMember]
        string CarrierID { get; set; }
        [DataMember]
        string CarrierName { get; set; }
       

    }
}
