using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiliWeb2.Models
{
    public class UserRolesModel
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Role { get; set; }

        public TechnicianModel User { get; set; }
    }
}
