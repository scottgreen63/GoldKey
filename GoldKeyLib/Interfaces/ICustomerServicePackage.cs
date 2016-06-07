using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoldKeyLib.Interfaces
{
    public interface ICustomerServicePackage
    {
        [DataMember]
        string CustomerID { get; set; }
        [DataMember]
        string VehicleID { get; set; }
        [DataMember]
        string ServicePackageID { get; set; }

    }
}
