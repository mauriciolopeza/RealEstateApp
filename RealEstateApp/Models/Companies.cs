using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public partial class Companies
    {
        public Companies()
        {
            Contacts = new HashSet<Contacts>();
            Interests = new HashSet<Interests>();
            Listings = new HashSet<Listings>();
            Networks = new HashSet<Networks>();
            Users = new HashSet<Users>();
        }

        public int CompaniesId { get; set; }
        public string CompanyName { get; set; }
        public string LogoUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternateNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Department { get; set; }

        public virtual ICollection<Contacts> Contacts { get; set; }
        public virtual ICollection<Interests> Interests { get; set; }
        public virtual ICollection<Listings> Listings { get; set; }
        public virtual ICollection<Networks> Networks { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
