using BiliWeb2.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiliWeb2.Models
{
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

        // Tech's street 
        [Display(Name = "Street")]
        public string Street { get; set; }

        // Tech's City
        [Display(Name = "City")]
        public string City { get; set; }

        // Tech's State
        [Display(Name = "State")]
        public string State { get; set; }

        // Tech's Country
        [Display(Name = "Country")]
        public string Country { get; set; }

        // Tech's Postal Code
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        // Tech's Bio
        [Display(Name = "Bio")]
        public string Bio { get; set; }

        // Tech's clinic
        [Display(Name = "Clinic")]
        public string ClinicID { get; set; }

        // Tech's Organization
        [Display(Name = "Organization")]
        public string OrganizationID { get; set; }

        // Track if the user is active 
        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        // Track if the latest eula is accepted or not by this user
        [Display(Name = "EULA Accepted")]
        public bool EulaAccepted { get; set; } = false;
        /// <summary>
        /// Simple Constructor
        /// </summary>
        /// <param name="data"></param>
        public TechnicianModel()
        {
        }

        /// <summary>
        /// Makes a copy of the data
        /// </summary>
        /// <param name="data"></param>
        public TechnicianModel(TechnicianModel data)
        {
            // Because this is a copy, let it have a new ID
            Update(data);
        }

        /// <summary>
        /// Update fields passed in
        /// Updates all fields to be the values passed in
        /// Does NOT update the ID field, this allows for the method to be used as part of a copy.
        /// Does NOT update the Date field, this allows for the method to be used as part of a copy.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(TechnicianModel data)
        {
            if (data == null)
            {
                return false;
            }

            // Update all the other fields
            FirstName = data.FirstName;
            LastName = data.LastName;
            Street = data.Street;
            City = data.City;
            State = data.State;
            Country = data.Country;
            PostalCode = data.PostalCode;
            Bio = data.Bio;
            ClinicID = data.ClinicID;
            OrganizationID = data.OrganizationID;
            EulaAccepted = data.EulaAccepted;
            IsActive = data.IsActive;

            return true;
        }
    }
}
