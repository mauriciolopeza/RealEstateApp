using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public partial class Users
    {
        public Users()
        {
            Networks = new HashSet<Networks>();
            Notifications = new HashSet<Notifications>();
            Routes = new HashSet<Routes>();
            Tasks = new HashSet<Tasks>();
        }

        public int UsersId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternateNumber { get; set; }
        public int FkCompaniesId { get; set; }

        public virtual Companies FkCompanies { get; set; }
        public virtual ICollection<Networks> Networks { get; set; }
        public virtual ICollection<Notifications> Notifications { get; set; }
        public virtual ICollection<Routes> Routes { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
