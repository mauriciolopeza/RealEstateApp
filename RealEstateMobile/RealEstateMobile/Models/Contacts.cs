using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateMobile.Models
{
    class Contacts
    {
        public int contactsId { get; set; }
        public string contactNames { get; set; }
        public string contactLastNames { get; set; }
        public string completeName
        {
            get
            {
                return contactNames + " " + contactLastNames;
            }
        }
        public string idNumber { get; set; }
        public string phoneNumber { get; set; }
        public string alternateNumber { get; set; }
        public string emailAddress { get; set; }
        public string contactType { get; set; }
        public int fkCompaniesId { get; set; }
    }
}
