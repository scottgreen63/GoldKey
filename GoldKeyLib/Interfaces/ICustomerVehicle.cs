using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldKeyLib.Interfaces
{
    public interface ICustomerVehicle
    {
        ICustomer Customer { get; set; }
        IVehicle Vehicle { get; set; }

    }
}
