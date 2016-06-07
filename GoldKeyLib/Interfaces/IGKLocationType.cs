using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoldKeyLib.Interfaces
{
    public interface IGKLocationType
    {
        [DataMember]
        string LocationTypeID { get; set; }
        [DataMember]
        string LocationTypeName { get; set; }
    }
}
