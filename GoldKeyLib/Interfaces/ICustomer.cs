using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoldKeyLib.Interfaces
{
    public interface ICustomer
    {
        [DataMember]
        string CustomerID { get; set; }
        //[DataMember]
        //string CustomerCode { get; set; }
        //// Customer_Code = 'GK-', CONVERT([nvarchar](10),[fldi_Customer_ID]),(6),'0')),
        [DataMember]
        string LastName { get; set; }
        [DataMember]
        string FirstName { get; set; }
        [DataMember]
        string Address1 { get; set; }
        [DataMember]
        string Address2 { get; set; }
        [DataMember]
        string City { get; set; }
        [DataMember]
        string State { get; set; }
        [DataMember]
        string ZipCode { get; set; }
        [DataMember]
        string ContactPhone { get; set; }
        [DataMember]
        string ContactEmail { get; set; }
        [DataMember]
        string CompanyName { get; set; }

    }
}
