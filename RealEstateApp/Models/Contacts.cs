using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public partial class Contacts
    {
        public Contacts()
        {
            ContactInterests = new HashSet<ContactInterests>();
            Listings = new HashSet<Listings>();
            Routes = new HashSet<Routes>();
        }

        public int ContactsId { get; set; }
        public string ContactNames { get; set; }
        public string ContactLastNames { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternateNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ContactType { get; set; }
        public int FkCompaniesId { get; set; }

        public virtual Companies FkCompanies { get; set; }
        public virtual ICollection<ContactInterests> ContactInterests { get; set; }
        public virtual ICollection<Listings> Listings { get; set; }
        public virtual ICollection<Routes> Routes { get; set; }
    }
}
