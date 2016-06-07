using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoldKeyLib.Interfaces
{
    public interface IGKServicePackage
    {
        [DataMember]
        string ServicePackageID { get; set; }
        [DataMember]
        string ServicePackageName { get; set; }
        [DataMember]
        string ServicePackageContents { get; set; }
        [DataMember]
        decimal ServicePackageFee { get; set; }
    }
}
