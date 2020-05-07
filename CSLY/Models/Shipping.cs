using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSLY.Models
{
    public class Shipping
    {
        public int ShippingId { get; set; }

        public int MemberId { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string ZipCode { get; set; }

        public int? OrderId { get; set; }

        public decimal AmountPaid { get; set; }

        [Required]
        public string PaymentType { get; set; }
    }
}