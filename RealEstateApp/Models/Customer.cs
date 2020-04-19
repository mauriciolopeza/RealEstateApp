using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateApp.Models
{
    public class Customer
    {
        [Key]
        [Column("customers_id")]
        public long CustomerID { get; set; }

        [Column("customer_name")]
        [Display(Name ="Customer Name")]
        [StringLength(100)]
        public String CustomerName { get; set; }

        [Column("address")]
        [Display(Name = "Address")]
        [StringLength(100)]
        public String Address { get; set; }

        [Column("phone_number")]
        [Display(Name = "Phone Number")]
        [StringLength(30)]
        public String PhoneNumber { get; set; }

    }
}
