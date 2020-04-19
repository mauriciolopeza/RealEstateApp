using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public partial class ContactInterests
    {
        public int ContactInterestsId { get; set; }
        public int FkContactsId { get; set; }
        public int FkInterestId { get; set; }

        public virtual Contacts FkContacts { get; set; }
        public virtual Interests FkInterest { get; set; }
    }
}
