using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateMobile.Models
{
    class Customers
    {
        public int CustomersId { get; set; }
        public string CustomersFirstNames { get; set; }
        public string CustomersLastNames { get; set; }
        public string CompleteName
        {
            get
            {
                return CustomersFirstNames + " " + CustomersLastNames;
            }
        }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternateNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PictureUrl { get; set; }
        public string Department { get; set; }
    }
}

