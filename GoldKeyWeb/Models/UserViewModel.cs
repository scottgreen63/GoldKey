using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldKeyLib.Entities;

namespace GoldKeyWeb.Models
{
    public class UserViewModel
    {
        public IEnumerable<User> User { get; set; }
        public UserGroupMenuItems Menu { get; set; }
    }
}
