using System;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace GoldKeyLib.Interfaces
{
    public interface ICustomerServicePackageDetail : ICustomerServicePackage
    {
        //[DataMember]
        //string CustomerID { get; set; }
        //[DataMember]
        //string VehicleID { get; set; }
        //[DataMember]
        //string ServicePackageID { get; set; }
        //[DataMember]
        string ServiceID { get; set; }
        //[DataMember]
        //string ServiceTransactionID   { get; set; }
        [DataMember]
        DateTime ServiceStartDateTime { get; set; }
        [DataMember]
        DateTime ServiceFinishDateTime { get; set; }
        [DataMember]
        string ServiceStatus { get; set; }

    }
}
