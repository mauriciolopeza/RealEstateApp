using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public partial class Interests
    {
        public Interests()
        {
            ContactInterests = new HashSet<ContactInterests>();
        }

        public int InterestsId { get; set; }
        public string InterestName { get; set; }
        public string Description { get; set; }
        public int FkCompaniesId { get; set; }

        public virtual Companies FkCompanies { get; set; }
        public virtual ICollection<ContactInterests> ContactInterests { get; set; }
    }
}
