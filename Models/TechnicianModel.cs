using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiliWeb2.Models
{ /// <summary>
  ///  Technician Data Model
  /// </summary>
    public class TechnicianModel : IdentityUser
    {
        // TODO: Identity - modify BaseModel to IdentityUser 

        // When the record was created
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Mark as Sample data, used for data refresh
        public Boolean IsSampleData { get; set; } = false;

        // Mark as live Data, if true OK to use, if False, then it is pending Soft Delete Pending
        public Boolean IsLiveData { get; set; } = true;

        // Tech's first name
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        // Tech's last name
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Bio")]
        public string Bio { get; set; }

        // Tech's clinic
        [Display(Name = "Clinic")]
        public string ClinicID { get; set; }

        [Display(Name = "Organization")]
        public string OrganizationID { get; set; }

        // Track if the latest eula is accepted or not by this user
        [Display(Name = "EULA Accepted")]
        public bool EulaAccepted { get; set; } = false;

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}
