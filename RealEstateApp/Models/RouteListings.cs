using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public partial class RouteListings
    {
        public int RouteListingsId { get; set; }
        public int FkListingsId { get; set; }
        public int FkRoutesId { get; set; }
        public TimeSpan? ScheduledTime { get; set; }

        public virtual Listings FkListings { get; set; }
        public virtual Routes FkRoutes { get; set; }
    }
}
