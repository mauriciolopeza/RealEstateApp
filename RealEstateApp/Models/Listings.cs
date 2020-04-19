using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public partial class Listings
    {
        public Listings()
        {
            RouteListings = new HashSet<RouteListings>();
        }

        public int ListingsId { get; set; }
        public string ListingNumber { get; set; }
        public string SmlsNumber { get; set; }
        public string TuNuevoEspacioNumber { get; set; }
        public short? IsSmlsPromoted { get; set; }
        public short? IsTuNuevoEspacioPromoted { get; set; }
        public string ListingType { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public string BloqueNumber { get; set; }
        public string UnitNumber { get; set; }
        public string UrbanizationName { get; set; }
        public string Neighborhood { get; set; }
        public string Estrato { get; set; }
        public decimal? SalesPrice { get; set; }
        public decimal? AdministrationFee { get; set; }
        public int? ParkingSpaces { get; set; }
        public short? HasOpenKitchen { get; set; }
        public short? HasElevator { get; set; }
        public short? IsClosedUrbanization { get; set; }
        public int FkCompaniesId { get; set; }
        public short? HasBalcony { get; set; }
        public short? HasMaidRoom { get; set; }
        public short? IsStudio { get; set; }
        public short? HasPool { get; set; }
        public int? YearBuilt { get; set; }
        public int FkContactsId { get; set; }
        public int? AreaSize { get; set; }
        public int? NumberOfRooms { get; set; }
        public int? NumberOfBathrooms { get; set; }

        public virtual Companies FkCompanies { get; set; }
        public virtual Contacts FkContacts { get; set; }
        public virtual ICollection<RouteListings> RouteListings { get; set; }
    }
}
