using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public partial class Routes
    {
        public Routes()
        {
            RouteListings = new HashSet<RouteListings>();
        }

        public int RoutesId { get; set; }
        public string RouteName { get; set; }
        public DateTime? RouteDate { get; set; }
        public int FkContactsId { get; set; }
        public int FkUsersId { get; set; }

        public virtual Contacts FkContacts { get; set; }
        public virtual Users FkUsers { get; set; }
        public virtual ICollection<RouteListings> RouteListings { get; set; }
    }
}
