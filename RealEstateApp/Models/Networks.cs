using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public partial class Networks
    {
        public int NetworksId { get; set; }
        public int FkCompaniesId { get; set; }
        public int FkUsersId { get; set; }

        public virtual Companies FkCompanies { get; set; }
        public virtual Users FkUsers { get; set; }
    }
}
